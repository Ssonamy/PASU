using System;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using System.Net;
using System.Threading.Tasks;

namespace Server
{
    public partial class Server : Form
    {
        private HttpListener listener;
        private bool isRunning = false;
        private int requestCount = 0;

        public Server()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                Log("Server already running.");
                return;
            }

            listener = new HttpListener();
            listener.Prefixes.Add($"http://*:{txtPort.Text}/");

            try
            {
                listener.Start();
                isRunning = true;
                Log("Server started.");

                Task.Run(ListenLoop);
            }
            catch (Exception ex)
            {
                Log("Start error: " + ex.Message);
                isRunning = false;
            }
        }

        private void ListenLoop()
        {
            try
            {
                while (listener.IsListening)
                {
                    var context = listener.GetContext();
                    HandleRequest(context);
                }
            }
            catch (HttpListenerException)
            {
                // корректное завершение
            }
            catch (ObjectDisposedException)
            {
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                Log("Server already stopped.");
                return;
            }

            isRunning = false;

            listener.Stop();
            listener.Close();
            listener = null;

            Log("Server stopped.");
        }


        private void HandleRequest(HttpListenerContext context)
        {
            try
            {
                if (context.Request.HttpMethod == "POST")
                {
                    using var reader = new System.IO.StreamReader(context.Request.InputStream);
                    string body = reader.ReadToEnd();

                    // Разбор JSON с Base64
                    var jsonDoc = JsonDocument.Parse(body);
                    txtA.Invoke(new Action(() => txtA.Text = Encoding.UTF8.GetString(Convert.FromBase64String(jsonDoc.RootElement.GetProperty("A").GetString()))));
                    txtB.Invoke(new Action(() => txtB.Text = Encoding.UTF8.GetString(Convert.FromBase64String(jsonDoc.RootElement.GetProperty("B").GetString()))));
                    txtC.Invoke(new Action(() => txtC.Text = Encoding.UTF8.GetString(Convert.FromBase64String(jsonDoc.RootElement.GetProperty("C").GetString()))));

                    // Формирование ответа
                    var responseJson = JsonSerializer.Serialize(new
                    {
                        D = Convert.ToBase64String(Encoding.UTF8.GetBytes(txtD.Text)),
                        E = Convert.ToBase64String(Encoding.UTF8.GetBytes(txtE.Text)),
                        F = Convert.ToBase64String(Encoding.UTF8.GetBytes(txtF.Text))
                    });

                    byte[] buffer = Encoding.UTF8.GetBytes(responseJson);
                    context.Response.ContentType = "application/json";
                    context.Response.ContentLength64 = buffer.Length;
                    context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                    context.Response.OutputStream.Close();

                    requestCount++;
                    Log($"Handled request #{requestCount}");
                }
                else if (context.Request.HttpMethod == "GET")
                {
                    string html = $"<html><body><h2>Processed JSON requests: {requestCount}</h2>" +
                                    $"<p>Last D: {txtD.Text}, E: {txtE.Text}, F: {txtF.Text}</p></body></html>";
                    byte[] buffer = Encoding.UTF8.GetBytes(html);
                    context.Response.ContentType = "text/html";
                    context.Response.ContentLength64 = buffer.Length;
                    context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                    context.Response.OutputStream.Close();
                }
            }
            catch (Exception ex)
            {
                Log("Error: " + ex.Message);
            }
        }

        private void Log(string message)
        {
            if (lstLog.InvokeRequired)
                lstLog.Invoke(new Action(() => lstLog.Items.Add(message)));
            else
                lstLog.Items.Add(message);
        }
    }
}
