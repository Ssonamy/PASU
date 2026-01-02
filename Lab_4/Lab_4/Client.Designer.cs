namespace Client
{
    partial class Client
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ListBox lstLog;

        private TextBox txtIP;
        private TextBox txtPort;
        private TextBox txtA;
        private TextBox txtB;
        private TextBox txtC;
        private TextBox txtD;
        private TextBox txtE;
        private TextBox txtF;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtIP = new TextBox();
            this.txtPort = new TextBox();
            this.txtA = new TextBox();
            this.txtB = new TextBox();
            this.txtC = new TextBox();
            this.txtD = new TextBox();
            this.txtE = new TextBox();
            this.txtF = new TextBox();
            btnSend = new Button();
            lstLog = new ListBox();
            SuspendLayout();

            // txtIP
            this.txtIP.Location = new Point(12, 12);
            this.txtIP.Size = new Size(100, 23);
            this.txtIP.Text = "127.0.0.1";

            // txtPort
            this.txtPort.Location = new Point(118, 12);
            this.txtPort.Size = new Size(50, 23);
            this.txtPort.Text = "8080";

            // txtA, txtB, txtC (ввод)
            this.txtA.Location = new Point(12, 50); this.txtA.Size = new Size(237, 23);
            this.txtB.Location = new Point(12, 80); this.txtB.Size = new Size(237, 23);
            this.txtC.Location = new Point(12, 110); this.txtC.Size = new Size(237, 23);

            // txtD, txtE, txtF (только отображение)
            this.txtD.Location = new Point(12, 140); this.txtD.Size = new Size(237, 23); this.txtD.ReadOnly = true;
            this.txtE.Location = new Point(12, 170); this.txtE.Size = new Size(237, 23); this.txtE.ReadOnly = true;
            this.txtF.Location = new Point(12, 200); this.txtF.Size = new Size(237, 23); this.txtF.ReadOnly = true;

            // btnSend
            btnSend.Location = new Point(174, 11);
            btnSend.Size = new Size(75, 23);
            btnSend.Text = "Send";
            btnSend.Click += btnSend_Click;

            // lstLog
            lstLog.Location = new Point(255, 12);
            lstLog.Size = new Size(273, 214);

            // Client form
            ClientSize = new Size(540, 250);
            Controls.Add(this.txtIP);
            Controls.Add(this.txtPort);
            Controls.Add(btnSend);
            Controls.Add(this.txtA);
            Controls.Add(this.txtB);
            Controls.Add(this.txtC);
            Controls.Add(this.txtD);
            Controls.Add(this.txtE);
            Controls.Add(this.txtF);
            Controls.Add(lstLog);
            Name = "Client";
            Text = "Client";
            ResumeLayout(false);
            PerformLayout();
        }

        //private TextBox txtIP, txtPort;
        //private TextBox txtA, txtB, txtC;
        //private TextBox txtD, txtE, txtF;
    }
}
