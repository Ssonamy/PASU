using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Lab_6
{
    public class UdpLidarReceiver
    {
        public event Action<LidarFrame>? FrameReceived;
        public event Action<string>? Log;

        private UdpClient? udpClient;
        private CancellationTokenSource? cts;
        private Task? receiveTask;

        public bool IsRunning => running;
        private volatile bool running;

        public void Start(int port)
        {
            if (running)
            {
                Log?.Invoke("UDP receiver already running");
                return;
            }

            try
            {
                udpClient = new UdpClient(port);
                cts = new CancellationTokenSource();
                running = true;

                receiveTask = Task.Run(() => ReceiveLoop(cts.Token));

                Log?.Invoke($"UDP receiver started on port {port}");
            }
            catch (Exception ex)
            {
                Log?.Invoke($"UDP start error: {ex.Message}");
                running = false;
                Stop();
            }
        }

        public void Stop()
        {
            if (!running)
                return;

            running = false;

            try
            {
                cts?.Cancel();
                udpClient?.Close();
            }
            catch { }

            udpClient = null;
            cts = null;
            receiveTask = null;

            Log?.Invoke("UDP receiver stopped");
        }

        private async Task ReceiveLoop(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    if (udpClient == null)
                        break;

                    UdpReceiveResult result;
                    try
                    {
                        result = await udpClient.ReceiveAsync(token);
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                    catch (ObjectDisposedException)
                    {
                        break;
                    }
                    catch (Exception ex)
                    {
                        Log?.Invoke($"Receive error: {ex.Message}");
                        continue;
                    }

                    string text = Encoding.UTF8.GetString(result.Buffer);

                    // поддержка нескольких JSON в одном пакете
                    var messages = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var msg in messages)
                    {
                        TryParseFrame(msg);
                    }
                }
            }
            finally
            {
                running = false;
                Log?.Invoke("UDP receiver loop stopped");
            }
        }
        private void TryParseFrame(string json)
        {
            try
            {
                var data = JsonSerializer.Deserialize<LidarData>(json);
                if (data == null)
                    return;

                var frame = new LidarFrame
                {
                    Timestamp = DateTime.Now,
                    Distances = new List<int>
                    {
                        data.d0, data.d1, data.d2, data.d3,
                        data.d4, data.d5, data.d6, data.d7
                    }
                };

                FrameReceived?.Invoke(frame);
            }
            catch (JsonException ex)
            {
                Log?.Invoke($"JSON parse error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Log?.Invoke($"Frame error: {ex.Message}");
            }
        }

        private class LidarData
        {
            public int d0 { get; set; }
            public int d1 { get; set; }
            public int d2 { get; set; }
            public int d3 { get; set; }
            public int d4 { get; set; }
            public int d5 { get; set; }
            public int d6 { get; set; }
            public int d7 { get; set; }
        }
    }
}
