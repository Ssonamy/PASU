namespace Recieving_data
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lst_data = new System.Windows.Forms.ListBox();
            this.start_btn = new System.Windows.Forms.Button();
            this.stop_btn = new System.Windows.Forms.Button();
            this.pause_btn = new System.Windows.Forms.Button();
            this.cbx_choose_mode = new System.Windows.Forms.ComboBox();
            this.txt_freq = new System.Windows.Forms.TextBox();
            this.txt_port = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox_show = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_selectFile = new System.Windows.Forms.Button();
            this.txt_name_file = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.fileTimer = new System.Windows.Forms.Timer(this.components);
            this.scrollBarHistory = new System.Windows.Forms.HScrollBar();
            this.labelHistory = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();

            // pictureBox1
            this.pictureBox1.Location = new System.Drawing.Point(560, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(700, 520);
            this.pictureBox1.TabStop = false;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // lst_data
            this.lst_data.FormattingEnabled = true;
            this.lst_data.ItemHeight = 15;
            this.lst_data.Location = new System.Drawing.Point(10, 20);
            this.lst_data.Name = "lst_data";
            this.lst_data.Size = new System.Drawing.Size(540, 319);

            // start_btn
            this.start_btn.Location = new System.Drawing.Point(445, 460);
            this.start_btn.Name = "start_btn";
            this.start_btn.Size = new System.Drawing.Size(95, 40);
            this.start_btn.Text = "Start";
            this.start_btn.UseVisualStyleBackColor = true;
            this.start_btn.Click += new System.EventHandler(this.start_btn_Click);

            // stop_btn
            this.stop_btn.Location = new System.Drawing.Point(445, 505);
            this.stop_btn.Name = "stop_btn";
            this.stop_btn.Size = new System.Drawing.Size(95, 40);
            this.stop_btn.Text = "Stop";
            this.stop_btn.UseVisualStyleBackColor = true;
            this.stop_btn.Click += new System.EventHandler(this.stop_btn_Click);

            // pause_btn
            this.pause_btn.Location = new System.Drawing.Point(335, 395);
            this.pause_btn.Name = "pause_btn";
            this.pause_btn.Size = new System.Drawing.Size(95, 40);
            this.pause_btn.Text = "Pause";
            this.pause_btn.UseVisualStyleBackColor = true;
            this.pause_btn.Click += new System.EventHandler(this.pause_btn_Click);

            // cbx_choose_mode
            this.cbx_choose_mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_choose_mode.FormattingEnabled = true;
            this.cbx_choose_mode.Items.AddRange(new object[] {
                "считывание из файла",
                "считывание из сети по UDP"});
            this.cbx_choose_mode.Location = new System.Drawing.Point(35, 387);
            this.cbx_choose_mode.Name = "cbx_choose_mode";
            this.cbx_choose_mode.Size = new System.Drawing.Size(245, 23);

            // txt_freq
            this.txt_freq.Location = new System.Drawing.Point(16, 90);
            this.txt_freq.Name = "txt_freq";
            this.txt_freq.Size = new System.Drawing.Size(130, 23);

            // txt_port
            this.txt_port.Location = new System.Drawing.Point(15, 45);
            this.txt_port.Name = "txt_port";
            this.txt_port.Size = new System.Drawing.Size(90, 23);

            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Text = "Порт:";

            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 65);
            this.label2.Text = "Интервал чтения (мс):";

            // label3
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 368);
            this.label3.Text = "Режим чтения:";

            // checkBox_show
            this.checkBox_show.AutoSize = true;
            this.checkBox_show.Location = new System.Drawing.Point(335, 360);
            this.checkBox_show.Text = "Показывать данные таблицы";

            // groupBox2
            this.groupBox2.Controls.Add(this.btn_selectFile);
            this.groupBox2.Controls.Add(this.txt_name_file);
            this.groupBox2.Controls.Add(this.txt_freq);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(20, 415);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(285, 130);
            this.groupBox2.Text = "Работа с файлом";

            // btn_selectFile
            this.btn_selectFile.Location = new System.Drawing.Point(170, 85);
            this.btn_selectFile.Name = "btn_selectFile";
            this.btn_selectFile.Size = new System.Drawing.Size(95, 35);
            this.btn_selectFile.Text = "Select File";
            this.btn_selectFile.UseVisualStyleBackColor = true;
            this.btn_selectFile.Click += new System.EventHandler(this.btn_selectFile_Click);

            // txt_name_file
            this.txt_name_file.Location = new System.Drawing.Point(16, 30);
            this.txt_name_file.Name = "txt_name_file";
            this.txt_name_file.Size = new System.Drawing.Size(250, 23);

            // groupBox3
            this.groupBox3.Controls.Add(this.txt_port);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(315, 455);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(120, 90);
            this.groupBox3.Text = "Работа с UDP";

            // scrollBarHistory
            this.scrollBarHistory.Location = new System.Drawing.Point(560, 545);
            this.scrollBarHistory.Name = "scrollBarHistory";
            this.scrollBarHistory.Size = new System.Drawing.Size(700, 17);

            // labelHistory
            this.labelHistory.AutoSize = true;
            this.labelHistory.Location = new System.Drawing.Point(560, 528);
            this.labelHistory.Text = "История:";

            // Form1
            this.ClientSize = new System.Drawing.Size(1275, 600);
            this.Controls.Add(this.labelHistory);
            this.Controls.Add(this.scrollBarHistory);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.checkBox_show);
            this.Controls.Add(this.cbx_choose_mode);
            this.Controls.Add(this.pause_btn);
            this.Controls.Add(this.stop_btn);
            this.Controls.Add(this.start_btn);
            this.Controls.Add(this.lst_data);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Lidar visualizer";

            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListBox lst_data;
        private System.Windows.Forms.Button start_btn;
        private System.Windows.Forms.Button stop_btn;
        private System.Windows.Forms.Button pause_btn;
        private System.Windows.Forms.ComboBox cbx_choose_mode;
        private System.Windows.Forms.TextBox txt_freq;
        private System.Windows.Forms.TextBox txt_port;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox_show;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_selectFile;
        private System.Windows.Forms.TextBox txt_name_file;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Timer fileTimer;
        private System.Windows.Forms.HScrollBar scrollBarHistory;
        private System.Windows.Forms.Label labelHistory;
    }
}
