using System;
using System.Text;
using System.Text.Json;
using System.Net.Http;
using System.Windows.Forms;

namespace Client
{
    public partial class Client : Form
    {
        private readonly HttpClient client = new HttpClient();

        public Client()
        {
            InitializeComponent();
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                var data = new
                {
                    A = Convert.ToBase64String(Encoding.UTF8.GetBytes(txtA.Text)),
                    B = Convert.ToBase64String(Encoding.UTF8.GetBytes(txtB.Text)),
                    C = Convert.ToBase64String(Encoding.UTF8.GetBytes(txtC.Text))
                };

                string json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                string url = $"http://{txtIP.Text}:{txtPort.Text}/";
                var response = await client.PostAsync(url, content);
                string responseJson = await response.Content.ReadAsStringAsync();

                var jsonDoc = JsonDocument.Parse(responseJson);
                txtD.Text = Encoding.UTF8.GetString(Convert.FromBase64String(jsonDoc.RootElement.GetProperty("D").GetString()));
                txtE.Text = Encoding.UTF8.GetString(Convert.FromBase64String(jsonDoc.RootElement.GetProperty("E").GetString()));
                txtF.Text = Encoding.UTF8.GetString(Convert.FromBase64String(jsonDoc.RootElement.GetProperty("F").GetString()));

                lstLog.Items.Add("Request sent successfully");
            }
            catch (Exception ex)
            {
                lstLog.Items.Add("Error: " + ex.Message);
            }
        }
    }
}
