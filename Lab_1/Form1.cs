using System;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;

namespace Rs232Chat
{
    public partial class Form1 : Form
    {
        private StringBuilder rxBuffer = new StringBuilder();

        public Form1()
        {
            InitializeComponent();
            RefreshPorts();
        }

        private void RefreshPorts()
        {
            cbPorts.Items.Clear();
            foreach (var p in SerialPort.GetPortNames())
                cbPorts.Items.Add(p);
            if (cbPorts.Items.Count > 0)
                cbPorts.SelectedIndex = 0;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshPorts();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!serial.IsOpen)
            {
                if (cbPorts.SelectedItem == null)
                {
                    MessageBox.Show("Выберите порт.");
                    return;
                }
                try
                {
                    serial.PortName = cbPorts.SelectedItem.ToString();
                    serial.Open();
                    lblStatus.Text = $"Status: Connected ({serial.PortName})";
                    btnConnect.Text = "Disconnect";
                    Log("Connected.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
            else
            {
                serial.Close();
                lblStatus.Text = "Status: Disconnected";
                btnConnect.Text = "Connect";
                Log("Disconnected.");
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void tbSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendMessage();
            }
        }

        private void SendMessage()
        {
            if (!serial.IsOpen)
            {
                MessageBox.Show("Порт не открыт.");
                return;
            }

            string msg = tbSend.Text;
            if (msg.Length == 0) return;

            serial.Write(msg);
            Log("TX: " + msg);
            tbSend.Clear();
        }

        private void serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = serial.ReadExisting();
            if (data.Length == 0) return;

            lock (rxBuffer)
            {
                rxBuffer.Append(data);

                string buf = rxBuffer.ToString();
                int idx;
                while ((idx = buf.IndexOf('\n')) >= 0)
                {
                    string line = buf.Substring(0, idx).Trim();
                    buf = buf.Substring(idx + 1);
                    HandleLine(line);
                }

                rxBuffer.Clear();
                rxBuffer.Append(buf);
            }
        }

        private void HandleLine(string line)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<string>(HandleLine), line);
                return;
            }

            if (line.Contains("#"))
            {
                var arr = line.Split('#');
                string msg = "";
                for (int i = 0; i < arr.Length; i++)
                    msg += $"A{i}: {arr[i]}  ";
                Log("RX: " + msg);
            }
            else
            {
                Log("RX: " + line);
            }
        }

        private void Log(string text)
        {
            tbLog.AppendText($"{DateTime.Now:HH:mm:ss.fff} | {text}\r\n");
            tbLog.SelectionStart = tbLog.Text.Length;
            tbLog.ScrollToCaret();
        }
    }
}
