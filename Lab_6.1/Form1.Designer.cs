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
            components = new System.ComponentModel.Container();
            pictureBox1 = new PictureBox();
            lst_data = new ListBox();
            start_btn = new Button();
            stop_btn = new Button();
            pause_btn = new Button();
            cbx_choose_mode = new ComboBox();
            txt_freq = new TextBox();
            txt_port = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            checkBox_show = new CheckBox();
            groupBox2 = new GroupBox();
            btn_selectFile = new Button();
            txt_name_file = new TextBox();
            groupBox3 = new GroupBox();
            fileTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(560, 20);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(700, 525);
            pictureBox1.TabIndex = 10;
            pictureBox1.TabStop = false;
            // 
            // lst_data
            // 
            lst_data.FormattingEnabled = true;
            lst_data.Location = new Point(10, 20);
            lst_data.Name = "lst_data";
            lst_data.Size = new Size(540, 364);
            lst_data.TabIndex = 9;
            // 
            // start_btn
            // 
            start_btn.Location = new Point(459, 447);
            start_btn.Name = "start_btn";
            start_btn.Size = new Size(95, 40);
            start_btn.TabIndex = 8;
            start_btn.Text = "Start";
            start_btn.UseVisualStyleBackColor = true;
            start_btn.Click += start_btn_Click;
            // 
            // stop_btn
            // 
            stop_btn.Location = new Point(459, 504);
            stop_btn.Name = "stop_btn";
            stop_btn.Size = new Size(95, 40);
            stop_btn.TabIndex = 7;
            stop_btn.Text = "Stop";
            stop_btn.UseVisualStyleBackColor = true;
            stop_btn.Click += stop_btn_Click;
            // 
            // pause_btn
            // 
            pause_btn.Location = new Point(459, 390);
            pause_btn.Name = "pause_btn";
            pause_btn.Size = new Size(95, 40);
            pause_btn.TabIndex = 6;
            pause_btn.Text = "Pause";
            pause_btn.UseVisualStyleBackColor = true;
            pause_btn.Click += pause_btn_Click;
            // 
            // cbx_choose_mode
            // 
            cbx_choose_mode.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_choose_mode.FormattingEnabled = true;
            cbx_choose_mode.Items.AddRange(new object[] { "считывание из файла", "считывание из сети по UDP" });
            cbx_choose_mode.Location = new Point(10, 386);
            cbx_choose_mode.Name = "cbx_choose_mode";
            cbx_choose_mode.Size = new Size(245, 23);
            cbx_choose_mode.TabIndex = 5;
            // 
            // txt_freq
            // 
            txt_freq.Location = new Point(16, 90);
            txt_freq.Name = "txt_freq";
            txt_freq.Size = new Size(130, 23);
            txt_freq.TabIndex = 2;
            // 
            // txt_port
            // 
            txt_port.Location = new Point(15, 45);
            txt_port.Name = "txt_port";
            txt_port.Size = new Size(90, 23);
            txt_port.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 28);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 1;
            label1.Text = "Порт:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 65);
            label2.Name = "label2";
            label2.Size = new Size(130, 15);
            label2.TabIndex = 3;
            label2.Text = "Интервал чтения (мс):";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(35, 368);
            label3.Name = "label3";
            label3.Size = new Size(100, 23);
            label3.TabIndex = 0;
            label3.Text = "Режим чтения:";
            // 
            // checkBox_show
            // 
            checkBox_show.AutoSize = true;
            checkBox_show.Location = new Point(267, 390);
            checkBox_show.Name = "checkBox_show";
            checkBox_show.Size = new Size(186, 19);
            checkBox_show.TabIndex = 4;
            checkBox_show.Text = "Показывать данные таблицы";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btn_selectFile);
            groupBox2.Controls.Add(txt_name_file);
            groupBox2.Controls.Add(txt_freq);
            groupBox2.Controls.Add(label2);
            groupBox2.Location = new Point(10, 415);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(278, 130);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Работа с файлом";
            // 
            // btn_selectFile
            // 
            btn_selectFile.Location = new Point(170, 85);
            btn_selectFile.Name = "btn_selectFile";
            btn_selectFile.Size = new Size(95, 35);
            btn_selectFile.TabIndex = 0;
            btn_selectFile.Text = "Select File";
            btn_selectFile.UseVisualStyleBackColor = true;
            // 
            // txt_name_file
            // 
            txt_name_file.Location = new Point(16, 30);
            txt_name_file.Name = "txt_name_file";
            txt_name_file.Size = new Size(250, 23);
            txt_name_file.TabIndex = 1;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(txt_port);
            groupBox3.Controls.Add(label1);
            groupBox3.Location = new Point(294, 415);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(159, 130);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Работа с UDP";
            // 
            // Form1
            // 
            ClientSize = new Size(1275, 553);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(checkBox_show);
            Controls.Add(cbx_choose_mode);
            Controls.Add(pause_btn);
            Controls.Add(stop_btn);
            Controls.Add(start_btn);
            Controls.Add(lst_data);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Lidar visualizer";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
    }
}
