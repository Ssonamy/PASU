using Lab_6.Models;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6.Services
{
    public class UdpLidarReceiver : IDisposable
    {
        private UdpClient udpClient;
        private IPEndPoint endPoint;
        private bool isRunning;

        public event Action<LidarFrame> FrameReceived;

        public UdpLidarReceiver(int port)
        {
            endPoint = new IPEndPoint(IPAddress.Any, port);
            udpClient = new UdpClient(port);
        }

        public void Start()
        {
            isRunning = true;
            ReceiveLoop();
        }

        public void Stop()
        {
            isRunning = false;
        }

        private async void ReceiveLoop()
        {
            while (isRunning)
            {
                try
                {
                    UdpReceiveResult result = await udpClient.ReceiveAsync();
                    ProcessPacket(result.Buffer);
                }
                catch (ObjectDisposedException)
                {
                    break;
                }
                catch
                {
                    // Игнор сетевых ошибок
                }
            }
        }

        private void ProcessPacket(byte[] buffer)
        {
            string message = Encoding.ASCII.GetString(buffer);

            // В симуляторе каждый пакет = один фрейм
            string[] tokens = message.Split(
                new[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries
            );

            int[] distances = new int[tokens.Length];
            for (int i = 0; i < tokens.Length; i++)
            {
                if (int.TryParse(tokens[i], out int value))
                    distances[i] = value;
            }

            LidarFrame frame = new LidarFrame(DateTime.Now, distances);
            FrameReceived?.Invoke(frame);
        }

        public void Dispose()
        {
            udpClient?.Close();
            udpClient = null;
        }
    }
}
