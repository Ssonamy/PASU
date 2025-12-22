using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace UdpChat
{
    public partial class Form1 : Form
    {
        private UdpClient receiver = null!;
        private Thread receiveThread = null!;
        private bool running = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void StartReceiver()
        {
            if (running) return;

            int myPort = int.Parse(txtMyPort.Text);
            receiver = new UdpClient(myPort);

            running = true;
            receiveThread = new(ReceiveLoop)
            {
                IsBackground = true
            };
            receiveThread.Start();
        }

        private void ReceiveLoop()
        {
            IPEndPoint remote = new IPEndPoint(IPAddress.Any, 0);

            while (running)
            {
                try
                {
                    byte[] data = receiver.Receive(ref remote);
                    string msg = Encoding.UTF8.GetString(data);

                    HandleIncomingMessage(msg);
                }
                catch
                {
                }
            }
        }

        private void HandleIncomingMessage(string msg)
        {
            // Перенос в UI-поток
            if (InvokeRequired)
            {
                Invoke(new Action<string>(HandleIncomingMessage), msg);
                return;
            }

            lstChat.Items.Add("RX: " + msg);

            // Контроль ключевых слов
            string[] commands = 
            [
                "CLEAR",
                "TEXT",
                "LINE",
                "RECTANGLE",
                "CIRCLE"
            ];

            foreach (var cmd in commands)
            {
                if (msg.Contains(cmd, StringComparison.OrdinalIgnoreCase))
                {
                    lstChat.Items.Add("Получена команда: " + cmd);
                }
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            StartReceiver(); // Гарантируем, что приём включён

            string ip = txtRemoteIP.Text;
            int port = int.Parse(txtRemotePort.Text);

            string fullMessage = $"{txtName.Text}:{txtMessage.Text}\n";
            byte[] data = Encoding.UTF8.GetBytes(fullMessage);

            using (UdpClient senderClient = new UdpClient())
            {
                senderClient.Send(data, data.Length, ip, port);
            }

            lstChat.Items.Add("TX: " + fullMessage);
            txtMessage.Clear();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            running = false;
            receiver?.Close();
        }
    }
}
