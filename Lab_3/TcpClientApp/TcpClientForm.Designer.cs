namespace TcpClientApp
{
    partial class TcpClientForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtCommand;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtIp = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();

            this.txtIp.Location = new System.Drawing.Point(90, 15);
            this.txtIp.Text = "127.0.0.1";

            this.txtPort.Location = new System.Drawing.Point(90, 45);
            this.txtPort.Text = "9000";

            this.txtCommand.Location = new System.Drawing.Point(15, 80);
            this.txtCommand.Width = 260;

            this.btnSend.Location = new System.Drawing.Point(290, 78);
            this.btnSend.Text = "Отправить";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);

            this.txtLog.Location = new System.Drawing.Point(15, 120);
            this.txtLog.Multiline = true;
            this.txtLog.Width = 360;
            this.txtLog.Height = 200;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;

            this.label1.Text = "IP:";
            this.label1.Location = new System.Drawing.Point(15, 18);

            this.label2.Text = "Порт:";
            this.label2.Location = new System.Drawing.Point(15, 48);

            this.ClientSize = new System.Drawing.Size(400, 340);
            this.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                txtIp, txtPort, txtCommand, txtLog, btnSend, label1, label2
            });

            this.Text = "TCP Клиент";
            this.ResumeLayout(false);
        }
    }
}
