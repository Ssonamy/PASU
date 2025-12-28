using System;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Lab_6
{
    public partial class MainForm : Form
    {
        private FileLidarReader fileReader;
        private UdpLidarReceiver udpReceiver;
        private Timer timer;

        public MainForm()
        {
            InitializeComponent();

            dgvData.Columns.Add("Angle", "Angle");
            dgvData.Columns.Add("Value", "Distance (mm)");

            timer = new Timer { Interval = 100 };
            timer.Tick += Timer_Tick;

            Log("Application started");
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() != DialogResult.OK) return;

            fileReader = new FileLidarReader(dlg.FileName);
            timer.Start();

            Log("File loaded");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            udpReceiver = new UdpLidarReceiver();
            udpReceiver.FrameReceived += ProcessFrame;
            udpReceiver.Start(9000);

            Log("UDP started on port 9000");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer.Stop();
            udpReceiver?.Stop();

            Log("Stopped");
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var frame = fileReader?.GetNextFrame();
            ProcessFrame(frame);
        }

        private void ProcessFrame(LidarFrame frame)
        {
            if (frame == null) return;

            if (InvokeRequired)
            {
                Invoke(new Action(() => ProcessFrame(frame)));
                return;
            }

            dgvData.Rows.Clear();
            for (int i = 0; i < frame.Distances.Count; i++)
                dgvData.Rows.Add(i, frame.Distances[i]);

            lidarRender.UpdateFrame(frame.Distances);

            Log($"Frame: {frame.Distances.Count} points");
        }

        public void Log(string text)
        {
            txtLog.AppendText(
                $"[{DateTime.Now:HH:mm:ss.fff}] {text}\r\n");
        }
    }
}
