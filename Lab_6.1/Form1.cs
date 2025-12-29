using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Recieving_data
{
    public partial class Form1 : Form
    {
        // ===== МОДЕЛИ =====
        private class LidarFrame
        {
            public IReadOnlyList<int> Distances { get; }
            public DateTime Timestamp { get; }
            public double Azimuth { get; }

            public LidarFrame(List<int> distances, DateTime time, double azimuth)
            {
                Distances = distances;
                Timestamp = time;
                Azimuth = azimuth;
            }
        }

        private class LidarFrameWithPos : LidarFrame
        {
            public double X { get; }
            public double Y { get; }

            public LidarFrameWithPos(
                List<int> distances,
                DateTime time,
                double x,
                double y,
                double azimuth)
                : base(distances, time, azimuth)
            {
                X = x;
                Y = y;
            }
        }

        // ===== ПОЛЯ =====
        private readonly ConcurrentQueue<LidarFrame> frameQueue = new();
        private readonly List<LidarFrame> historyFrames = new();
        private int historyIndex = -1;

        private CancellationTokenSource cts;
        private UdpClient udpServer;
        private StreamReader reader;

        private bool isPaused;
        private List<int> currentFrame = new();
        private DateTime currentTimeOfFrame;

        // ===== МАСШТАБЫ =====
        private const double DIST_SCALE = 50;
        private const double POS_SCALE = 10;

        public Form1()
        {
            InitializeComponent();

            cbx_choose_mode.SelectedIndex = 0;
            cbx_choose_mode.DropDownStyle = ComboBoxStyle.DropDownList;

            txt_port.Text = "8080";
            txt_freq.Text = "200";

            btn_selectFile.Click += btn_selectFile_Click;
            scrollBarHistory.Scroll += scrollBarHistory_Scroll;
        }

        // ===== КНОПКИ =====
        private void start_btn_Click(object sender, EventArgs e)
        {
            if (cbx_choose_mode.SelectedIndex == 0)
                StartFileReading();
            else
                StartUdpReceiving();
        }

        private void stop_btn_Click(object sender, EventArgs e) => StopAll();

        private void pause_btn_Click(object sender, EventArgs e)
        {
            isPaused = !isPaused;
            pause_btn.Text = isPaused ? "Continue" : "Pause";
        }

        // ===== ФАЙЛ =====
        private void btn_selectFile_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
                txt_name_file.Text = ofd.FileName;
        }

        private void StartFileReading()
        {
            if (!File.Exists(txt_name_file.Text))
                return;

            StopAll();

            reader = new StreamReader(txt_name_file.Text);
            fileTimer.Interval = int.TryParse(txt_freq.Text, out int f) ? f : 200;
            fileTimer.Start();
        }

        private void fileTimer_Tick(object sender, EventArgs e)
        {
            if (isPaused || reader == null || reader.EndOfStream)
                return;

            string json = reader.ReadLine();
            if (TryParseJsonFrame(json, out var frame))
            {
                frameQueue.Enqueue(frame);
                UpdateFromQueue();
            }
        }

        // ===== UDP =====
        private void StartUdpReceiving()
        {
            if (!int.TryParse(txt_port.Text, out int port))
                return;

            StopAll();

            cts = new CancellationTokenSource();
            udpServer = new UdpClient(port);

            Task.Run(() => ReceiveUdpAsync(cts.Token));
        }

        private async Task ReceiveUdpAsync(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    var result = await udpServer.ReceiveAsync();
                    string json = Encoding.UTF8.GetString(result.Buffer).Trim();

                    if (TryParseJsonFrame(json, out var frame))
                    {
                        frameQueue.Enqueue(frame);
                        if (!isPaused)
                            BeginInvoke(new Action(UpdateFromQueue));
                    }
                }
            }
            catch { }
        }

        // ===== JSON =====
        private bool TryParseJsonFrame(string json, out LidarFrame frame)
        {
            frame = null;

            try
            {
                var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                if (dict == null) return false;

                var distances = dict
                    .Where(k => k.Key.StartsWith("d"))
                    .OrderBy(k => int.Parse(k.Key[1..]))
                    .Select(k => int.TryParse(k.Value, out int v) ? v : -1)
                    .Where(v => v > 0)
                    .ToList();

                if (distances.Count < 2) return false;

                double x = dict.TryGetValue("x", out var xs) && double.TryParse(xs, out var xv) ? xv : 0;
                double y = dict.TryGetValue("y", out var ys) && double.TryParse(ys, out var yv) ? yv : 0;
                double az = dict.TryGetValue("azimuth", out var azs) && double.TryParse(azs, out var azv) ? azv : 0;

                frame = new LidarFrameWithPos(distances, DateTime.Now, x, y, az);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // ===== ОБНОВЛЕНИЕ =====
        private void UpdateFromQueue()
        {
            while (frameQueue.TryDequeue(out var frame))
            {
                currentFrame = frame.Distances.ToList();
                currentTimeOfFrame = frame.Timestamp;

                historyFrames.Add(frame);
                historyIndex = historyFrames.Count - 1;
            }

            UpdateScrollbar();
            UpdateDisplay();
        }

        private void UpdateScrollbar()
        {
            scrollBarHistory.Minimum = 0;
            scrollBarHistory.Maximum = Math.Max(0, historyFrames.Count - 1);
            scrollBarHistory.Value = historyIndex;
        }

        private void scrollBarHistory_Scroll(object sender, ScrollEventArgs e)
        {
            historyIndex = scrollBarHistory.Value;
            DrawLidar();
        }

        private void UpdateDisplay()
        {
            if (checkBox_show.Checked)
            {
                lst_data.Items.Add(
                    $"[{currentTimeOfFrame:HH:mm:ss.fff}] {currentFrame.Count} точек");
                lst_data.SelectedIndex = lst_data.Items.Count - 1;
            }

            DrawLidar();
        }

        // ===== ОТРИСОВКА =====
        private void DrawLidar()
        {
            if (historyFrames.Count == 0) return;

            int w = pictureBox1.Width;
            int h = pictureBox1.Height;

            Bitmap bmp = new Bitmap(w, h);
            using Graphics g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);

            int cx = w / 2;
            int cy = h / 2;

            using Pen dashedPen = new Pen(Color.LightGray, 1)
            {
                DashStyle = DashStyle.Dash
            };

            using SolidBrush pointBrush = new(Color.Red);

            int startIndex = Math.Max(0, historyIndex - 20);

            for (int f = startIndex; f <= historyIndex; f++)
            {
                if (historyFrames[f] is not LidarFrameWithPos frame)
                    continue;

                double step = 360.0 / frame.Distances.Count;
                double azRad = frame.Azimuth * Math.PI / 180.0;

                List<PointF> points = new();

                for (int i = 0; i < frame.Distances.Count; i++)
                {
                    int d = frame.Distances[i];
                    if (d <= 0) continue;

                    double angle = (i * step) * Math.PI / 180.0 + azRad;
                    double dist = d / 2000.0;

                    // === ИСПРАВЛЕНИЕ ОРИЕНТАЦИИ X ===
                    double wx = frame.X - Math.Cos(angle) * dist;
                    double wy = frame.Y + Math.Sin(angle) * dist;

                    float px = (float)(cx + wx * POS_SCALE * DIST_SCALE);
                    float py = (float)(cy - wy * POS_SCALE * DIST_SCALE);

                    points.Add(new PointF(px, py));
                }

                for (int i = 1; i < points.Count; i++)
                    g.DrawLine(dashedPen, points[i - 1], points[i]);

                foreach (var p in points)
                    g.FillEllipse(pointBrush, p.X - 2, p.Y - 2, 4, 4);
            }

            pictureBox1.Image?.Dispose();
            pictureBox1.Image = bmp;
        }

        // ===== STOP =====
        private void StopAll()
        {
            isPaused = false;
            fileTimer.Stop();

            reader?.Dispose();
            reader = null;

            cts?.Cancel();
            udpServer?.Close();
            udpServer = null;
        }
    }
}
