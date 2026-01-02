namespace Server
{
    partial class Server
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnStart, btnStop;
        private System.Windows.Forms.ListBox lstLog;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtPort = new TextBox();
            this.txtA = new TextBox();
            this.txtB = new TextBox();
            this.txtC = new TextBox();
            this.txtD = new TextBox();
            this.txtE = new TextBox();
            this.txtF = new TextBox();
            btnStart = new Button();
            btnStop = new Button();
            lstLog = new ListBox();
            SuspendLayout();

            // txtPort
            this.txtPort.Location = new Point(12, 12);
            this.txtPort.Size = new Size(50, 23);
            this.txtPort.Text = "8080";

            // txtA, txtB, txtC (только отображение)
            this.txtA.Location = new Point(12, 50); this.txtA.Size = new Size(212, 23); this.txtA.ReadOnly = true;
            this.txtB.Location = new Point(12, 80); this.txtB.Size = new Size(212, 23); this.txtB.ReadOnly = true;
            this.txtC.Location = new Point(12, 110); this.txtC.Size = new Size(212, 23); this.txtC.ReadOnly = true;

            // txtD, txtE, txtF (ввод)
            this.txtD.Location = new Point(12, 140); this.txtD.Size = new Size(212, 23);
            this.txtE.Location = new Point(12, 170); this.txtE.Size = new Size(212, 23);
            this.txtF.Location = new Point(12, 203); this.txtF.Size = new Size(212, 23);

            // btnStart
            btnStart.Location = new Point(68, 10); btnStart.Size = new Size(75, 25); btnStart.Text = "Start";
            btnStart.Click += btnStart_Click;

            // btnStop
            btnStop.Location = new Point(149, 10); btnStop.Size = new Size(75, 25); btnStop.Text = "Stop";
            btnStop.Click += btnStop_Click;

            // lstLog
            lstLog.Location = new Point(231, 12); lstLog.Size = new Size(297, 214);

            // Server form
            ClientSize = new Size(540, 250);
            Controls.Add(this.txtPort);
            Controls.Add(btnStart);
            Controls.Add(btnStop);
            Controls.Add(this.txtA);
            Controls.Add(this.txtB);
            Controls.Add(this.txtC);
            Controls.Add(this.txtD);
            Controls.Add(this.txtE);
            Controls.Add(this.txtF);
            Controls.Add(lstLog);
            Name = "Server";
            Text = "Server";
            ResumeLayout(false);
            PerformLayout();
        }

        private TextBox txtPort;
        private TextBox txtA, txtB, txtC;
        private TextBox txtD, txtE, txtF;
    }
}
