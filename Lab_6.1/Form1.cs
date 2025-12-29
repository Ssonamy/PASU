using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Recieving_data
{
    public partial class Form1 : Form
    {
        private int portServer;
        private UdpClient udpServer;
        private CancellationTokenSource udpCancellation;
        private Task udpTask;

        private List<KeyValuePair<List<int>, DateTime>> allFrames;
        private object queueLock;

        private StreamReader reader;
        private bool isPaused;
        private bool stopRequested;

        private List<int> currentFrame;
        private DateTime currentTimeOfFrame;
        private bool redrawRequested;

        private int fileFrameCounter = 0;
        private static readonly Color[] FrameColorPalette = {
            Color.Blue, Color.Cyan, Color.LimeGreen, Color.Yellow,
            Color.Orange, Color.Red, Color.Magenta, Color.Violet
        };

        public Form1()
        {
            InitializeComponent();

            currentFrame = new List<int>();
            allFrames = new List<KeyValuePair<List<int>, DateTime>>();
            queueLock = new object();

            isPaused = false;
            redrawRequested = false;
            stopRequested = false;

            txt_name_file.Text = "C:\\work\\Проектирование_алгоритмов_систем_управления\\test.txt";
            txt_freq.Text = "200";
            txt_port.Text = "8080";

            fileTimer.Tick += fileTimer_Tick;
            scrollBarHistory.Scroll += scrollBarHistory_Scroll;
        }

        // ----------------- Кнопки -----------------
        private void start_btn_Click(object sender, EventArgs e)
        {
            if (cbx_choose_mode.SelectedIndex == 0)
                StartFileReading();
            else
                StartUdpReceiving();
        }

        private void stop_btn_Click(object sender, EventArgs e)
        {
            StopAll();
        }

        private void pause_btn_Click(object sender, EventArgs e)
        {
            isPaused = !isPaused;
            pause_btn.Text = isPaused ? "Continue" : "Pause";
            Log(isPaused ? "Приём приостановлен." : "Приём возобновлён.");
        }

        private void Btn_selectFile_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txt_name_file.Text = ofd.FileName;
            }
        }

        // ----------------- UDP -----------------
        private void StartUdpReceiving()
        {
            try
            {
                portServer = int.Parse(txt_port.Text);
                udpServer?.Close();
                udpServer = new UdpClient(portServer);

                udpCancellation?.Cancel();
                udpCancellation = new CancellationTokenSource();
                CancellationToken token = udpCancellation.Token;

                udpTask = Task.Run(async () =>
                {
                    try
                    {
                        while (!token.IsCancellationRequested)
                        {
                            var result = await udpServer.ReceiveAsync();
                            string text = Encoding.ASCII.GetString(result.Buffer).Trim();

                            if (text.StartsWith(">"))
                                text = text.Substring(1).TrimStart();

                            string[] parts = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if (parts.Length != 360)
                            {
                                Log($"Получен некорректный UDP-фрейм: {parts.Length} значений (ожидается 360)");
                                continue;
                            }

                            List<int> distances = new List<int>();
                            foreach (var s in parts)
                                distances.Add(int.Parse(s));

                            DateTime timeNow = DateTime.Now;
                            EnqueueFrame(distances, timeNow);

                            if (!isPaused)
                                this.Invoke(new MethodInvoker(PreparingForDisplay));
                        }
                    }
                    catch (ObjectDisposedException) { /* UDP закрыт */ }
                    catch (Exception ex)
                    {
                        Log("Ошибка приёма UDP: " + ex.Message);
                    }
                }, token);

                Log("UDP-приём запущен на порту " + portServer);
            }
            catch (Exception ex)
            {
                Log("Ошибка запуска UDP: " + ex.Message);
            }
        }

        // ----------------- Файл -----------------
        private void StartFileReading()
        {
            string file = txt_name_file.Text;
            if (string.IsNullOrEmpty(file) || !File.Exists(file))
            {
                Log("Файл не выбран или не существует!");
                return;
            }

            try
            {
                reader?.Close();
                reader = new StreamReader(file);
                isPaused = false;
                stopRequested = false;
                fileFrameCounter = 0;

                int interval = int.TryParse(txt_freq.Text, out int freq) && freq > 0 ? freq : 100;
                fileTimer.Interval = interval;
                fileTimer.Enabled = true;

                Log("Чтение файла начато. Интервал: " + interval + " мс");
            }
            catch (Exception ex)
            {
                Log("Ошибка открытия файла: " + ex.Message);
            }
        }

        private void fileTimer_Tick(object sender, EventArgs e)
        {
            if (isPaused || stopRequested || reader == null || reader.EndOfStream)
            {
                if (reader?.EndOfStream ?? false)
                {
                    fileTimer.Enabled = false;
                    Log("Достигнут конец файла.");
                }
                return;
            }

            string line = reader.ReadLine();
            if (string.IsNullOrEmpty(line)) return;

            try
            {
                string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 361)
                {
                    Log($"Недостаточно данных в строке: {parts.Length} элементов (ожидается ≥361)");
                    return;
                }

                DateTime timeNow = DateTime.Today.AddHours(12);
                if (DateTime.TryParse(parts[0], out DateTime parsedTime))
                    timeNow = parsedTime;

                List<int> distances = new List<int>();
                for (int i = 1; i <= 360; i++)
                    distances.Add(int.Parse(parts[i]));

                fileFrameCounter++;
                EnqueueFrame(distances, timeNow);
                redrawRequested = true;
                PreparingForDisplay();
            }
            catch (Exception ex)
            {
                Log($"Ошибка обработки строки: {ex.Message}. Строка: \"{line}\"");
            }
        }

        // ----------------- Общие функции -----------------
        private void EnqueueFrame(List<int> distances, DateTime timeNow)
        {
            lock (queueLock)
            {
                allFrames.Add(new KeyValuePair<List<int>, DateTime>(new List<int>(distances), timeNow));
                const int MAX_FRAMES = 10000;
                if (allFrames.Count > MAX_FRAMES)
                    allFrames.RemoveRange(0, allFrames.Count - MAX_FRAMES);
            }
            Log("Фрейм добавлен. Всего: " + allFrames.Count);
        }

        private void PreparingForDisplay()
        {
            lock (queueLock)
            {
                if (allFrames.Count > 0)
                {
                    var pair = allFrames[allFrames.Count - 1];
                    currentFrame = new List<int>(pair.Key);
                    currentTimeOfFrame = pair.Value;
                }
            }
            UpdateDisplay();
        }

        private void StopAll()
        {
            isPaused = false;
            fileTimer.Enabled = false;
            reader?.Close();
            reader = null;

            udpCancellation?.Cancel();
            udpServer?.Close();
        }

        private void Log(string message)
        {
            if (lst_data.InvokeRequired)
            {
                lst_data.Invoke(new Action<string>(Log), message);
            }
            else
            {
                lst_data.Items.Add($"{DateTime.Now:HH:mm:ss} - {message}");
                lst_data.SelectedIndex = lst_data.Items.Count - 1;
            }
        }

        private void UpdateDisplay()
        {
            if (checkBox_show.Checked)
            {
                lst_data.Items.Add("[" + currentTimeOfFrame.ToString("HH:mm:ss.fff") + "] ");
                const int VALUES_PER_LINE = 15;
                for (int start = 0; start < currentFrame.Count; start += VALUES_PER_LINE)
                {
                    string line = "  ";
                    for (int i = start; i < start + VALUES_PER_LINE && i < currentFrame.Count; i++)
                    {
                        line += currentFrame[i].ToString().PadLeft(5);
                        if ((i - start + 1) % 5 == 0 && i < start + VALUES_PER_LINE - 1)
                            line += "  ";
                    }
                    lst_data.Items.Add(line);
                }
                lst_data.Items.Add("");
                lst_data.SelectedIndex = lst_data.Items.Count - 1;
            }
            DrawDisplay();
        }

        private void DrawDisplay()
        {
            List<int> frameToDraw;
            lock (queueLock)
            {
                if (allFrames.Count == 0) return;
                frameToDraw = new List<int>(allFrames[allFrames.Count - 1].Key);
            }

            const int WIDTH = 700;
            const int HEIGHT = 520;
            const double SCALE = 50.0;
            const int CENTER_X = 200;
            const int CENTER_Y = 250;

            Bitmap bitmap = new Bitmap(WIDTH, HEIGHT);
            using Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);

            DrawGrid(g, CENTER_X, CENTER_Y, SCALE, WIDTH, HEIGHT);

            using (SolidBrush robotBrush = new SolidBrush(Color.Black))
            {
                g.FillEllipse(robotBrush, CENTER_X - 3, CENTER_Y - 3, 6, 6);
                g.DrawEllipse(Pens.Black, CENTER_X - 3, CENTER_Y - 3, 6, 6);
            }

            Color pointColor;
            int radius;

            if (cbx_choose_mode.SelectedIndex == 0)
            {
                int colorIndex = (fileFrameCounter - 1) % FrameColorPalette.Length;
                pointColor = FrameColorPalette[colorIndex];
                radius = 4;
            }
            else
            {
                pointColor = Color.Red;
                radius = 2;
            }

            using (SolidBrush pointBrush = new SolidBrush(pointColor))
            {
                for (int angle = 0; angle < frameToDraw.Count && angle < 360; angle++)
                {
                    int distMm = frameToDraw[angle];
                    if (distMm <= 0 || distMm > 10000) continue;

                    double distM = distMm / 1000.0;
                    double rad = angle * Math.PI / 180.0;

                    double x = -distM * Math.Sin(rad);
                    double y = -distM * Math.Cos(rad);

                    int px = (int)(CENTER_X + x * SCALE);
                    int py = (int)(CENTER_Y - y * SCALE);

                    if (px >= -radius && px < WIDTH + radius && py >= -radius && py < HEIGHT + radius)
                        g.FillEllipse(pointBrush, px - radius, py - radius, 2 * radius + 1, 2 * radius + 1);
                }
            }

            UpdatePictureBox(bitmap);
        }

        private void UpdatePictureBox(Bitmap bmp)
        {
            if (pictureBox1.InvokeRequired)
            {
                pictureBox1.Invoke(new Action<Bitmap>(UpdatePictureBox), bmp);
                return;
            }

            pictureBox1.Image?.Dispose();
            pictureBox1.Image = bmp;
        }

        private void DrawGrid(Graphics g, int cx, int cy, double scale, int width, int height)
        {
            using Pen gridPen = new Pen(Color.LightGray, 0.5f) { DashStyle = DashStyle.Dot };
            for (int m = -10; m <= 10; m++)
            {
                int x = cx + (int)(m * scale);
                int y = cy + (int)(m * scale);
                if (x >= 0 && x < width) g.DrawLine(gridPen, x, 0, x, height);
                if (y >= 0 && y < height) g.DrawLine(gridPen, 0, y, width, y);
            }
        }

        private void scrollBarHistory_Scroll(object sender, ScrollEventArgs e)
        {
            // Можно реализовать прокрутку истории по кадрам
        }

        private void btn_selectFile_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
                txt_name_file.Text = ofd.FileName;
        }

        private Color HsvToRgb(double h, double s, double v)
        {
            int hi = ((int)Math.Floor(h / 60)) % 6;
            double f = h / 60 - Math.Floor(h / 60);
            double p = v * (1 - s);
            double q = v * (1 - f * s);
            double t = v * (1 - (1 - f) * s);

            double r = 0, g = 0, b = 0;
            switch (hi)
            {
                case 0: r = v; g = t; b = p; break;
                case 1: r = q; g = v; b = p; break;
                case 2: r = p; g = v; b = t; break;
                case 3: r = p; g = q; b = v; break;
                case 4: r = t; g = p; b = v; break;
                case 5: r = v; g = p; b = q; break;
            }

            return Color.FromArgb(
                Math.Clamp((int)Math.Round(r * 255), 0, 255),
                Math.Clamp((int)Math.Round(g * 255), 0, 255),
                Math.Clamp((int)Math.Round(b * 255), 0, 255));
        }
    }
}
