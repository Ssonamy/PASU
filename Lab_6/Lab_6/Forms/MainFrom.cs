using Lab_6.Models;
using Lab_6.Rendering;
using Lab_6.Services;
using System;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Lab_6.Forms
{
    public partial class MainForm : Form
    {
        private Timer fileTimer;
        private Timer renderTimer;

        private FileLidarReader fileReader;
        private UdpLidarReceiver udpReceiver;
        private LidarRenderer renderer;

        private LidarFrame currentFrame;
        private readonly object frameLock = new object();

        private bool isPaused = false;

        public MainForm()
        {
            InitializeComponent();
            InitializeLogic();
        }

        private void InitializeLogic()
        {
            // Òàéìåð ÷òåíèÿ ôàéëà
            fileTimer = new Timer();
            fileTimer.Interval = (int)numericUpDownInterval.Value;
            fileTimer.Tick += FileTimer_Tick;

            // Òàéìåð îòðèñîâêè
            renderTimer = new Timer();
            renderTimer.Interval = 33; // ~30 FPS
            renderTimer.Tick += RenderTimer_Tick;

            // Ðåíäåðåð (500x500, ìàñøòàá ~ 1px = 20ìì)
            renderer = new LidarRenderer(500, 20f);
        }

        // ==========================
        // ×ÒÅÍÈÅ ÈÇ ÔÀÉËÀ
        // ==========================
        private void FileTimer_Tick(object sender, EventArgs e)
        {
            if (isPaused || fileReader == null)
                return;

            if (fileReader.EndOfFile)
            {
                fileTimer.Stop();
                return;
            }

            LidarFrame frame = fileReader.ReadNextFrame();
            UpdateFrame(frame);
        }

        // ==========================
        // UDP ÏÐÈ¨Ì
        // ==========================
        private void OnUdpFrameReceived(LidarFrame frame)
        {
            if (isPaused)
                return;

            UpdateFrame(frame);
        }

        // ==========================
        // ÎÁÍÎÂËÅÍÈÅ ÒÅÊÓÙÅÃÎ ÔÐÅÉÌÀ
        // ==========================
        private void UpdateFrame(LidarFrame frame)
        {
            lock (frameLock)
            {
                currentFrame = frame;
            }

            if (checkBoxShowText.Checked)
                ShowFrameText(frame);
        }

        // ==========================
        // ÂÈÇÓÀËÈÇÀÖÈß
        // ==========================
        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            LidarFrame frameCopy;

            lock (frameLock)
            {
                frameCopy = currentFrame;
            }

            if (frameCopy == null)
                return;

            Bitmap bmp = renderer.Render(frameCopy);

            pictureBoxLidar.Image?.Dispose();
            pictureBoxLidar.Image = bmp;
        }

        // ==========================
        // ÒÅÊÑÒÎÂÛÉ ÂÛÂÎÄ
        // ==========================
        private void ShowFrameText(LidarFrame frame)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ShowFrameText(frame)));
                return;
            }

            textBoxLog.Clear();

            for (int i = 0; i < frame.Distances.Length; i++)
            {
                textBoxLog.AppendText(
                    $"{frame.Timestamp:HH:mm:ss.fff} | " +
                    $"Angle: {i}° | " +
                    $"Distance: {frame.Distances[i]} mm{Environment.NewLine}"
                );
            }
        }

        // ==========================
        // ÊÍÎÏÊÈ ÓÏÐÀÂËÅÍÈß
        // ==========================
        private void buttonStart_Click(object sender, EventArgs e)
        {
            StopAll();

            if (radioButtonFile.Checked)
                StartFileMode();
            else
                StartUdpMode();

            renderTimer.Start();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            StopAll();
        }

        private void StartFileMode()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Lidar data (*.txt)|*.txt|All files (*.*)|*.*";

            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            fileReader = new FileLidarReader(dlg.FileName);
            fileTimer.Start();
        }

        private void StartUdpMode()
        {
            udpReceiver = new UdpLidarReceiver(9000);
            udpReceiver.FrameReceived += OnUdpFrameReceived;
            udpReceiver.Start();
        }

        private void StopAll()
        {
            fileTimer.Stop();

            udpReceiver?.Stop();
            udpReceiver?.Dispose();
            udpReceiver = null;

            currentFrame = null;
        }

        // ==========================
        // UI ÑÎÁÛÒÈß
        // ==========================
        private void checkBoxPause_CheckedChanged(object sender, EventArgs e)
        {
            isPaused = checkBoxPause.Checked;
        }

        private void numericUpDownInterval_ValueChanged(object sender, EventArgs e)
        {
            fileTimer.Interval = (int)numericUpDownInterval.Value;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            StopAll();
            base.OnFormClosing(e);
        }
    }
}
