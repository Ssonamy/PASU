namespace Lab_6
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnLoadFile;

        private System.Windows.Forms.Label lblServerStatus;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Panel pnlStatusIndicator;
        private System.Windows.Forms.GroupBox grpViewMode;
        private System.Windows.Forms.RadioButton rbPoints;
        private System.Windows.Forms.RadioButton rbClusters;
        private System.Windows.Forms.RadioButton rbWalls;

        private System.Windows.Forms.Label lblFrame;
        private System.Windows.Forms.TrackBar trackFrame;

        private LidarRender lidarRender;
        private TextBox txtLog;

        // Новые элементы
        private System.Windows.Forms.ComboBox comboSource;
        private System.Windows.Forms.ComboBox comboFormat;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.Button btnPause;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dgvData = new DataGridView();
            btnStart = new Button();
            btnStop = new Button();
            btnLoadFile = new Button();
            lblServerStatus = new Label();
            lblPort = new Label();
            pnlStatusIndicator = new Panel();
            grpViewMode = new GroupBox();
            rbPoints = new RadioButton();
            rbClusters = new RadioButton();
            rbWalls = new RadioButton();
            lblFrame = new Label();
            trackFrame = new TrackBar();
            lidarRender = new LidarRender();
            txtLog = new TextBox();
            comboSource = new ComboBox();
            comboFormat = new ComboBox();
            txtInterval = new TextBox();
            btnPause = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvData).BeginInit();
            grpViewMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackFrame).BeginInit();
            SuspendLayout();
            // 
            // dgvData
            // 
            dgvData.AllowUserToAddRows = false;
            dgvData.AllowUserToDeleteRows = false;
            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvData.Location = new Point(12, 12);
            dgvData.Name = "dgvData";
            dgvData.ReadOnly = true;
            dgvData.RowHeadersVisible = false;
            dgvData.Size = new Size(260, 350);
            dgvData.TabIndex = 0;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(12, 370);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(80, 30);
            btnStart.TabIndex = 1;
            btnStart.Text = "Start";
            btnStart.Click += btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.Location = new Point(100, 370);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(80, 30);
            btnStop.TabIndex = 2;
            btnStop.Text = "Stop";
            btnStop.Click += btnStop_Click;
            // 
            // btnLoadFile
            // 
            btnLoadFile.Location = new Point(190, 370);
            btnLoadFile.Name = "btnLoadFile";
            btnLoadFile.Size = new Size(80, 30);
            btnLoadFile.TabIndex = 3;
            btnLoadFile.Text = "File";
            btnLoadFile.Click += btnLoadFile_Click;
            // 
            // lblServerStatus
            // 
            lblServerStatus.Location = new Point(40, 410);
            lblServerStatus.Name = "lblServerStatus";
            lblServerStatus.Size = new Size(160, 20);
            lblServerStatus.TabIndex = 5;
            lblServerStatus.Text = "Server: STOPPED";
            // 
            // lblPort
            // 
            lblPort.Location = new Point(40, 430);
            lblPort.Name = "lblPort";
            lblPort.Size = new Size(160, 20);
            lblPort.TabIndex = 6;
            lblPort.Text = "Port: ---";
            // 
            // pnlStatusIndicator
            // 
            pnlStatusIndicator.BackColor = Color.Red;
            pnlStatusIndicator.Location = new Point(12, 410);
            pnlStatusIndicator.Name = "pnlStatusIndicator";
            pnlStatusIndicator.Size = new Size(20, 20);
            pnlStatusIndicator.TabIndex = 4;
            // 
            // grpViewMode
            // 
            grpViewMode.Controls.Add(rbPoints);
            grpViewMode.Controls.Add(rbClusters);
            grpViewMode.Controls.Add(rbWalls);
            grpViewMode.Location = new Point(12, 460);
            grpViewMode.Name = "grpViewMode";
            grpViewMode.Size = new Size(121, 110);
            grpViewMode.TabIndex = 7;
            grpViewMode.TabStop = false;
            grpViewMode.Text = "View mode";
            // 
            // rbPoints
            // 
            rbPoints.Checked = true;
            rbPoints.Location = new Point(10, 20);
            rbPoints.Name = "rbPoints";
            rbPoints.Size = new Size(104, 24);
            rbPoints.TabIndex = 0;
            rbPoints.TabStop = true;
            rbPoints.Text = "Points";
            // 
            // rbClusters
            // 
            rbClusters.Location = new Point(10, 49);
            rbClusters.Name = "rbClusters";
            rbClusters.Size = new Size(104, 24);
            rbClusters.TabIndex = 1;
            rbClusters.Text = "Clusters";
            // 
            // rbWalls
            // 
            rbWalls.Location = new Point(10, 78);
            rbWalls.Name = "rbWalls";
            rbWalls.Size = new Size(104, 24);
            rbWalls.TabIndex = 2;
            rbWalls.Text = "Walls";
            // 
            // lblFrame
            // 
            lblFrame.Location = new Point(139, 550);
            lblFrame.Name = "lblFrame";
            lblFrame.Size = new Size(136, 20);
            lblFrame.TabIndex = 8;
            lblFrame.Text = "Frame: 0";
            // 
            // trackFrame
            // 
            trackFrame.Location = new Point(280, 538);
            trackFrame.Maximum = 100;
            trackFrame.Name = "trackFrame";
            trackFrame.Size = new Size(608, 45);
            trackFrame.TabIndex = 9;
            // 
            // lidarRender
            // 
            lidarRender.BackColor = Color.White;
            lidarRender.BorderStyle = BorderStyle.FixedSingle;
            lidarRender.Location = new Point(280, 12);
            lidarRender.Name = "lidarRender";
            lidarRender.Size = new Size(608, 520);
            lidarRender.TabIndex = 10;
            // 
            // txtLog
            // 
            txtLog.Dock = DockStyle.Bottom;
            txtLog.Location = new Point(0, 589);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(900, 126);
            txtLog.TabIndex = 0;
            // 
            // comboSource
            // 
            comboSource.Items.AddRange(new object[] { "File", "UDP" });
            comboSource.Location = new Point(139, 460);
            comboSource.Name = "comboSource";
            comboSource.Size = new Size(136, 23);
            comboSource.TabIndex = 1;
            // 
            // comboFormat
            // 
            comboFormat.Items.AddRange(new object[] { "RawText/CSV", "JSON" });
            comboFormat.Location = new Point(139, 489);
            comboFormat.Name = "comboFormat";
            comboFormat.Size = new Size(136, 23);
            comboFormat.TabIndex = 2;
            // 
            // txtInterval
            // 
            txtInterval.Location = new Point(139, 518);
            txtInterval.Name = "txtInterval";
            txtInterval.Size = new Size(50, 23);
            txtInterval.TabIndex = 3;
            txtInterval.Text = "100";
            // 
            // btnPause
            // 
            btnPause.Location = new Point(195, 518);
            btnPause.Name = "btnPause";
            btnPause.Size = new Size(80, 23);
            btnPause.TabIndex = 4;
            btnPause.Text = "Pause";
            // 
            // MainForm
            // 
            AutoScaleMode = AutoScaleMode.Inherit;
            ClientSize = new Size(900, 715);
            Controls.Add(txtLog);
            Controls.Add(comboSource);
            Controls.Add(comboFormat);
            Controls.Add(txtInterval);
            Controls.Add(btnPause);
            Controls.Add(dgvData);
            Controls.Add(btnStart);
            Controls.Add(btnStop);
            Controls.Add(btnLoadFile);
            Controls.Add(pnlStatusIndicator);
            Controls.Add(lblServerStatus);
            Controls.Add(lblPort);
            Controls.Add(grpViewMode);
            Controls.Add(lblFrame);
            Controls.Add(trackFrame);
            Controls.Add(lidarRender);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainForm";
            Text = "LIDAR Visualizer";
            ((System.ComponentModel.ISupportInitialize)dgvData).EndInit();
            grpViewMode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)trackFrame).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
