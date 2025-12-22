namespace UdpChat
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Очистка ресурсов
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.txtMyPort = new System.Windows.Forms.TextBox();
            this.txtRemoteIP = new System.Windows.Forms.TextBox();
            this.txtRemotePort = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.lstChat = new System.Windows.Forms.ListBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.lblMyPort = new System.Windows.Forms.Label();
            this.lblRemoteIP = new System.Windows.Forms.Label();
            this.lblRemotePort = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtMyPort
            // 
            this.txtMyPort.Location = new System.Drawing.Point(140, 15);
            this.txtMyPort.Name = "txtMyPort";
            this.txtMyPort.Size = new System.Drawing.Size(100, 20);
            this.txtMyPort.TabIndex = 0;
            this.txtMyPort.Text = "5000";
            // 
            // txtRemoteIP
            // 
            this.txtRemoteIP.Location = new System.Drawing.Point(140, 45);
            this.txtRemoteIP.Name = "txtRemoteIP";
            this.txtRemoteIP.Size = new System.Drawing.Size(150, 20);
            this.txtRemoteIP.TabIndex = 1;
            this.txtRemoteIP.Text = "127.0.0.1";
            // 
            // txtRemotePort
            // 
            this.txtRemotePort.Location = new System.Drawing.Point(140, 75);
            this.txtRemotePort.Name = "txtRemotePort";
            this.txtRemotePort.Size = new System.Drawing.Size(100, 20);
            this.txtRemotePort.TabIndex = 2;
            this.txtRemotePort.Text = "5001";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(140, 110);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(150, 20);
            this.txtName.TabIndex = 3;
            this.txtName.Text = "User";
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(140, 145);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(300, 20);
            this.txtMessage.TabIndex = 4;
            // 
            // lstChat
            // 
            this.lstChat.FormattingEnabled = true;
            this.lstChat.HorizontalScrollbar = true;
            this.lstChat.Location = new System.Drawing.Point(15, 200);
            this.lstChat.Name = "lstChat";
            this.lstChat.Size = new System.Drawing.Size(545, 200);
            this.lstChat.TabIndex = 6;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(460, 140);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(100, 30);
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "Отправить";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // lblMyPort
            // 
            this.lblMyPort.AutoSize = true;
            this.lblMyPort.Location = new System.Drawing.Point(15, 18);
            this.lblMyPort.Name = "lblMyPort";
            this.lblMyPort.Size = new System.Drawing.Size(104, 13);
            this.lblMyPort.TabIndex = 7;
            this.lblMyPort.Text = "Мой порт (приём):";
            // 
            // lblRemoteIP
            // 
            this.lblRemoteIP.AutoSize = true;
            this.lblRemoteIP.Location = new System.Drawing.Point(15, 48);
            this.lblRemoteIP.Name = "lblRemoteIP";
            this.lblRemoteIP.Size = new System.Drawing.Size(83, 13);
            this.lblRemoteIP.TabIndex = 8;
            this.lblRemoteIP.Text = "IP получателя:";
            // 
            // lblRemotePort
            // 
            this.lblRemotePort.AutoSize = true;
            this.lblRemotePort.Location = new System.Drawing.Point(15, 78);
            this.lblRemotePort.Name = "lblRemotePort";
            this.lblRemotePort.Size = new System.Drawing.Size(96, 13);
            this.lblRemotePort.TabIndex = 9;
            this.lblRemotePort.Text = "Порт получателя:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(15, 113);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 10;
            this.lblName.Text = "Имя:";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(15, 148);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(71, 13);
            this.lblMessage.TabIndex = 11;
            this.lblMessage.Text = "Сообщение:";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(580, 420);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblRemotePort);
            this.Controls.Add(this.lblRemoteIP);
            this.Controls.Add(this.lblMyPort);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.lstChat);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtRemotePort);
            this.Controls.Add(this.txtRemoteIP);
            this.Controls.Add(this.txtMyPort);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "UDP Chat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtMyPort;
        private System.Windows.Forms.TextBox txtRemoteIP;
        private System.Windows.Forms.TextBox txtRemotePort;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.ListBox lstChat;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label lblMyPort;
        private System.Windows.Forms.Label lblRemoteIP;
        private System.Windows.Forms.Label lblRemotePort;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblMessage;
    }
}