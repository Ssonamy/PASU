using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace Lab_1
{
    public partial class MainForm : Form
    {
        private string buffer = "";
        private readonly SerialPort serialPort;


        public MainForm()
        {
            InitializeComponent();
            serialPort = new SerialPort();
            serialPort.DataReceived += SerialPort_DataReceived;

            comboBoxPorts.Items.AddRange(SerialPort.GetPortNames());
            if (comboBoxPorts.Items.Count > 0)
                comboBoxPorts.SelectedIndex = 0;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (!serialPort.IsOpen)
                { 
                    if (comboBoxPorts.SelectedItem != null)
                    {
                        serialPort.PortName = comboBoxPorts.SelectedItem.ToString();
                        serialPort.BaudRate = 9600;
                        serialPort.Open();
                        txtLog.AppendText("Подключено к " + serialPort.PortName + Environment.NewLine);
                        btnConnect.Text = "Отключить";
                    }
                    else
                    {
                        MessageBox.Show("Порт не выбран!");
                    }
                }
                else
                {
                    serialPort.Close();
                    txtLog.AppendText("Отключено" + Environment.NewLine);
                    btnConnect.Text = "Подключить";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения: " + ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string? previous = comboBoxPorts.SelectedItem as string;

            comboBoxPorts.Items.Clear();
            comboBoxPorts.Items.AddRange(SerialPort.GetPortNames());

            if (!string.IsNullOrEmpty(previous) && comboBoxPorts.Items.Contains(previous))
            {
                comboBoxPorts.SelectedItem = previous;
            }
            else if (comboBoxPorts.Items.Count > 0)
            {
                comboBoxPorts.SelectedIndex = 0;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.WriteLine(txtMessage.Text);
                txtLog.AppendText("Вы: " + txtMessage.Text + Environment.NewLine);
                txtMessage.Clear();
            }
            else
            {
                MessageBox.Show("Сначала подключитесь к порту!");
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            buffer += serialPort.ReadExisting();

            int index;
            while ((index = buffer.IndexOf('\n')) >= 0)
            {
                string message = buffer.Substring(0, index).Trim();
                buffer = buffer.Substring(index + 1);

                this.Invoke(() =>
                {
                    txtLog.AppendText("Arduino: " + message + Environment.NewLine);
                });
            }
        }
    }
}
