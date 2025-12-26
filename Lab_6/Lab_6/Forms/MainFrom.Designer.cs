namespace Lab_6.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.PictureBox pictureBoxLidar;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.CheckBox checkBoxShowText;
        private System.Windows.Forms.CheckBox checkBoxPause;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.NumericUpDown numericUpDownInterval;
        private System.Windows.Forms.Label labelInterval;
        private System.Windows.Forms.RadioButton radioButtonFile;
        private System.Windows.Forms.RadioButton radioButtonUdp;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pictureBoxLidar = new PictureBox();
            textBoxLog = new TextBox();
            checkBoxShowText = new CheckBox();
            checkBoxPause = new CheckBox();
            buttonStart = new Button();
            buttonStop = new Button();
            numericUpDownInterval = new NumericUpDown();
            labelInterval = new Label();
            radioButtonFile = new RadioButton();
            radioButtonUdp = new RadioButton();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLidar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownInterval).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxLidar
            // 
            pictureBoxLidar.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxLidar.Location = new Point(12, 12);
            pictureBoxLidar.Name = "pictureBoxLidar";
            pictureBoxLidar.Size = new Size(500, 500);
            pictureBoxLidar.TabIndex = 0;
            pictureBoxLidar.TabStop = false;
            // 
            // textBoxLog
            // 
            textBoxLog.Location = new Point(530, 12);
            textBoxLog.Multiline = true;
            textBoxLog.Name = "textBoxLog";
            textBoxLog.ScrollBars = ScrollBars.Vertical;
            textBoxLog.Size = new Size(350, 400);
            textBoxLog.TabIndex = 1;
            // 
            // checkBoxShowText
            // 
            checkBoxShowText.AutoSize = true;
            checkBoxShowText.Location = new Point(530, 420);
            checkBoxShowText.Name = "checkBoxShowText";
            checkBoxShowText.Size = new Size(194, 19);
            checkBoxShowText.TabIndex = 2;
            checkBoxShowText.Text = "Показывать текстовые данные";
            // 
            // checkBoxPause
            // 
            checkBoxPause.AutoSize = true;
            checkBoxPause.Location = new Point(530, 450);
            checkBoxPause.Name = "checkBoxPause";
            checkBoxPause.Size = new Size(58, 19);
            checkBoxPause.TabIndex = 3;
            checkBoxPause.Text = "Пауза";
            checkBoxPause.CheckedChanged += checkBoxPause_CheckedChanged;
            // 
            // buttonStart
            // 
            buttonStart.Location = new Point(12, 520);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(120, 30);
            buttonStart.TabIndex = 4;
            buttonStart.Text = "Старт";
            buttonStart.Click += buttonStart_Click;
            // 
            // buttonStop
            // 
            buttonStop.Location = new Point(150, 520);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new Size(120, 30);
            buttonStop.TabIndex = 5;
            buttonStop.Text = "Стоп";
            buttonStop.Click += buttonStop_Click;
            // 
            // numericUpDownInterval
            // 
            numericUpDownInterval.Location = new Point(400, 523);
            numericUpDownInterval.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDownInterval.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownInterval.Name = "numericUpDownInterval";
            numericUpDownInterval.Size = new Size(120, 23);
            numericUpDownInterval.TabIndex = 7;
            numericUpDownInterval.Value = new decimal(new int[] { 100, 0, 0, 0 });
            numericUpDownInterval.ValueChanged += numericUpDownInterval_ValueChanged;
            // 
            // labelInterval
            // 
            labelInterval.Location = new Point(300, 525);
            labelInterval.Name = "labelInterval";
            labelInterval.Size = new Size(100, 23);
            labelInterval.TabIndex = 6;
            labelInterval.Text = "Интервал, мс:";
            // 
            // radioButtonFile
            // 
            radioButtonFile.Checked = true;
            radioButtonFile.Location = new Point(530, 480);
            radioButtonFile.Name = "radioButtonFile";
            radioButtonFile.Size = new Size(194, 24);
            radioButtonFile.TabIndex = 8;
            radioButtonFile.TabStop = true;
            radioButtonFile.Text = "Чтение из файла";
            // 
            // radioButtonUdp
            // 
            radioButtonUdp.Location = new Point(530, 505);
            radioButtonUdp.Name = "radioButtonUdp";
            radioButtonUdp.Size = new Size(194, 24);
            radioButtonUdp.TabIndex = 9;
            radioButtonUdp.Text = "Приём по UDP";
            // 
            // MainForm
            // 
            ClientSize = new Size(900, 570);
            Controls.Add(pictureBoxLidar);
            Controls.Add(textBoxLog);
            Controls.Add(checkBoxShowText);
            Controls.Add(checkBoxPause);
            Controls.Add(buttonStart);
            Controls.Add(buttonStop);
            Controls.Add(labelInterval);
            Controls.Add(numericUpDownInterval);
            Controls.Add(radioButtonFile);
            Controls.Add(radioButtonUdp);
            Name = "MainForm";
            Text = "2D Lidar Viewer";
            ((System.ComponentModel.ISupportInitialize)pictureBoxLidar).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownInterval).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
