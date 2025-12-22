using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TcpServerApp
{
    public partial class TcpServerForm : Form
    {
        private TcpListener? listener;
        bool running = false;
        private readonly List<Action<Graphics>> drawActions = new();
        private readonly object drawLock = new();

        public TcpServerForm()
        {
            InitializeComponent();
            pictureBox1.Paint += PictureBox1_Paint;
            this.FormClosing += Form1_FormClosing;
        }

        private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
        {
            running = false;
            try {listener?.Stop();} catch { }

            if (lblStatus != null)
            {
                lblStatus.Text = "Stopped";
                lblStatus.ForeColor = Color.Red;
            }
        }

        private void PictureBox1_Paint(object? sender, PaintEventArgs e)
        {
            lock (drawLock)
            {
                foreach (var act in drawActions)
                {
                    try { act(e.Graphics); } catch { }
                }
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (running)
            {
                running = false;
                try { listener?.Stop(); } catch { }

                btnStart.Text = "Старт";
                if (lblStatus != null)
                {
                    lblStatus.Text = "Stopped";
                    lblStatus.ForeColor = Color.Red;
                }

                return;
            }

            if (!int.TryParse(txtPort.Text, out int port))
            {
                MessageBox.Show("Неверный порт", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            running = true;

            btnStart.Text = "Стоп";
            if (lblStatus != null)
            {
                lblStatus.Text = "Running";
                lblStatus.ForeColor = Color.Green;
            }

            Thread t = new Thread(Listen);
            t.IsBackground = true;
            t.Start();
        }

        void Listen()
        {
            while (running)
            {
                TcpClient? client = null;

                try
                {
                    if (listener == null)
                        break;

                    // Ожидаем подключение клиента
                    client = listener.AcceptTcpClient();
                    using var stream = client.GetStream();

                    // Проверка, есть ли данные сразу после подключения
                    bool sendWelcome = true;
                    if (stream.DataAvailable)
                    {
                        byte[] peekBuffer = new byte[256];
                        int peekBytes = stream.Read(peekBuffer, 0, peekBuffer.Length);
                        string peekText = Encoding.UTF8.GetString(peekBuffer, 0, peekBytes);

                        if (!string.IsNullOrWhiteSpace(peekText))
                        {
                            // Клиент UI: сразу обрабатываем первую команду
                            string result = ProcessCommand(peekText.Trim());
                            byte[] response = Encoding.UTF8.GetBytes(result + Environment.NewLine);
                            stream.Write(response, 0, response.Length);

                            sendWelcome = false;
                        }
                    }

                    // Если это Telnet/Putty-подобный клиент, отправляем welcome
                    if (sendWelcome)
                    {
                        string welcome =
                            "Connected to TCP Drawing Server" + Environment.NewLine +
                            "Type HELP to see available commands" + Environment.NewLine;
                        byte[] welcomeBytes = Encoding.UTF8.GetBytes(welcome);
                        stream.Write(welcomeBytes, 0, welcomeBytes.Length);
                    }

                    // Буфер для чтения команд
                    byte[] buffer = new byte[1024];

                    // Цикл для обработки команд от клиента
                    while (running && client.Connected)
                    {
                        int bytesRead;
                        try
                        {
                            bytesRead = stream.Read(buffer, 0, buffer.Length);
                            if (bytesRead <= 0)
                                break; // клиент закрыл соединение
                        }
                        catch
                        {
                            break; // ошибка чтения — выходим
                        }

                        string cmd = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                        if (string.IsNullOrWhiteSpace(cmd))
                            continue;

                        string result = ProcessCommand(cmd);
                        byte[] response = Encoding.UTF8.GetBytes(result + Environment.NewLine);

                        try
                        {
                            stream.Write(response, 0, response.Length);
                        }
                        catch
                        {
                            break; // ошибка записи — выходим
                        }
                    }
                }
                catch (SocketException)
                {
                    break; // слушатель был остановлен
                }
                catch (Exception)
                {
                    if (!running) break;
                    continue;
                }
                finally
                {
                    try { client?.Close(); } catch { }
                }
            }

            // Обновляем UI при остановке сервера
            if (this.IsHandleCreated && !this.IsDisposed)
            {
                try
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        btnStart.Text = "Старт";
                        if (lblStatus != null)
                        {
                            lblStatus.Text = "Stopped";
                            lblStatus.ForeColor = Color.Red;
                        }
                    }));
                }
                catch { }
            }
        }

        string ProcessCommand(string cmd)
        {
            if (string.IsNullOrWhiteSpace(cmd)) return "Unknown command";

            string[] p = cmd.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            try
            {
                if (p.Length == 0) return "Unknown command";

                if (string.Equals(p[0], "HELP", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(p[0], "H", StringComparison.OrdinalIgnoreCase))
                {
                    string nl = Environment.NewLine;

                    string help =
                        "Available commands:" + nl +
                        "HELP or H                      - show this help" + nl +
                        "CLEAR                          - clear the canvas" + nl +
                        "LINE x1 y1 x2 y2               - draw a line" + nl +
                        "CIRCLE x y r                   - draw a circle" + nl +
                        "RECTANGLE x y w h              - draw a rectangle" + nl +
                        "TEXT text x y                  - output text" + nl;

                    return help;
                }

                if (string.Equals(p[0], "CLEAR", StringComparison.OrdinalIgnoreCase))
                {
                    ClearDrawActions();
                    return "Ok";
                }

                if (string.Equals(p[0], "LINE", StringComparison.OrdinalIgnoreCase) && p.Length >= 5)
                {
                    int x1 = int.Parse(p[1]);
                    int y1 = int.Parse(p[2]);
                    int x2 = int.Parse(p[3]);
                    int y2 = int.Parse(p[4]);

                    AddDrawAction(g => g.DrawLine(Pens.Black, x1, y1, x2, y2));
                    return "Ok";
                }

                if (string.Equals(p[0], "CIRCLE", StringComparison.OrdinalIgnoreCase) && p.Length >= 4)
                {
                    int x = int.Parse(p[1]);
                    int y = int.Parse(p[2]);
                    int r = int.Parse(p[3]);

                    AddDrawAction(g => g.DrawEllipse(Pens.Black, x - r, y - r, r * 2, r * 2));
                    return "Ok";
                }

                if (string.Equals(p[0], "RECTANGLE", StringComparison.OrdinalIgnoreCase) && p.Length >= 5)
                {
                    int x = int.Parse(p[1]);
                    int y = int.Parse(p[2]);
                    int w = int.Parse(p[3]);
                    int h = int.Parse(p[4]);

                    AddDrawAction(g => g.DrawRectangle(Pens.Black, x, y, w, h));
                    return "Ok";
                }

                if (string.Equals(p[0], "TEXT", StringComparison.OrdinalIgnoreCase) && p.Length >= 4)
                {
                    int x = int.Parse(p[p.Length - 2]);
                    int y = int.Parse(p[p.Length - 1]);
                    string text = string.Join(" ", p.Skip(1).Take(p.Length - 3));

                    AddDrawAction(g => g.DrawString(text, this.Font, Brushes.Black, x, y));
                    return "Ok";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }

            return "Unknown command";
        }

        // Функция очистки холста
        private void ClearDrawActions()
        {
            if (pictureBox1.InvokeRequired)
            {
                pictureBox1.Invoke(new Action(ClearDrawActions));
                return;
            }

            lock (drawLock)
            {
                drawActions.Clear();
            }

            pictureBox1.Invalidate();
        }

        // Функция рисования
        private void AddDrawAction(Action<Graphics> action)
        {
            if (pictureBox1.InvokeRequired)
            {
                pictureBox1.Invoke(new Action(() => AddDrawAction(action)));
                return;
            }

            lock (drawLock)
            {
                drawActions.Add(action);
            }

            pictureBox1.Invalidate();
        }
    }
}
