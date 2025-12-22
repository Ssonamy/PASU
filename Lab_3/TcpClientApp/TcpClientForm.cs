using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace TcpClientApp
{
    public partial class TcpClientForm : Form
    {
        public TcpClientForm()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                try
                {
                    using (TcpClient client = new TcpClient())
                    {
                        client.Connect(txtIp.Text, int.Parse(txtPort.Text));
                        using NetworkStream stream = client.GetStream();

                        byte[] buffer = new byte[1024];
                        int bytesRead;
                        StringBuilder response = new StringBuilder();

                        // Сначала читаем приветственное сообщение
                        if ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            response.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
                        }

                        txtLog.Invoke(() =>
                        {
                            txtLog.AppendText(response.ToString() + Environment.NewLine);
                        });

                        // Отправляем команду серверу
                        byte[] request = Encoding.UTF8.GetBytes(txtCommand.Text + "\n");
                        stream.Write(request, 0, request.Length);

                        // Очищаем StringBuilder для нового ответа
                        response.Clear();

                        // Читаем ответ на команду
                        if ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            response.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
                        }

                        txtLog.Invoke(() =>
                        {
                            txtLog.AppendText("Сервер: " + response.ToString() + Environment.NewLine);
                        });
                    }
                }
                catch (Exception ex)
                {
                    txtLog.Invoke(() =>
                    {
                        txtLog.AppendText("Ошибка: " + ex.Message + Environment.NewLine);
                    });
                }
            });
        }

    }
}

