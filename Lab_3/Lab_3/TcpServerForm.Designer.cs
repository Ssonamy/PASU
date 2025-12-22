using System.Windows.Forms;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace TcpServerApp
{
    partial class TcpServerForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cmbMode;
        private System.Windows.Forms.Label lblStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtPort = new TextBox();
            btnStart = new Button();
            pictureBox1 = new PictureBox();
            cmbMode = new ComboBox();
            lblStatus = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // txtPort
            // 
            txtPort.Location = new Point(15, 15);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(100, 23);
            txtPort.TabIndex = 0;
            txtPort.Text = "9000";
            // 
            // btnStart
            // 
            btnStart.Location = new Point(132, 15);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(75, 23);
            btnStart.TabIndex = 1;
            btnStart.Text = "Старт";
            // привязка обработчика к существующему в коде методу BtnStart_Click
            btnStart.Click += BtnStart_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(15, 50);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(400, 300);
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // cmbMode
            // 
            cmbMode.Items.AddRange(new object[] { "TCP", "Telnet" });
            cmbMode.Location = new Point(224, 15);
            cmbMode.Name = "cmbMode";
            cmbMode.Size = new Size(121, 23);
            cmbMode.TabIndex = 2;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.ForeColor = Color.Red;
            lblStatus.Location = new Point(362, 19);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(51, 15);
            lblStatus.TabIndex = 4;
            lblStatus.Text = "Stopped";
            // 
            // TcpServerForm
            // 
            ClientSize = new Size(430, 370);
            Controls.Add(txtPort);
            Controls.Add(btnStart);
            Controls.Add(cmbMode);
            Controls.Add(pictureBox1);
            Controls.Add(lblStatus);
            Name = "TcpServerForm";
            Text = "TCP Сервер";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
