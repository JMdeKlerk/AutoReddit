using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.InteropServices;

namespace RedditBot
{
    public partial class Form1 : Form
    {
        User user;

        public Form1()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, EventArgs e)
        {
            formConsole("Attempting to login as user " + userTextBox.Text + "...");
            user = new User();
            user.loginUser(userTextBox.Text, passTextBox.Text, appKeyBox.Text, appSecretBox.Text);
        }

        private void start_Click(object sender, EventArgs e)
        {
            string url = "https://oauth.reddit.com/api/v1/me";
        }

        private void loginUser(string name, string pass, string key, string secret)
        {
            string url = "https://www.reddit.com/api/v1/access_token";
            string postData = "username=" + name + "&password=" + pass + "&grant_type=password";
            ASCIIEncoding encode = new ASCIIEncoding();
            byte[] byteData = encode.GetBytes(postData);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteData.Length;

            string auth = key + ":" + secret;
            byte[] authBin = System.Text.Encoding.UTF8.GetBytes(auth);
            auth = Convert.ToBase64String(authBin);
            auth = "Basic " + auth;
            request.Headers.Add("AUTHORIZATION", auth);

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteData, 0, byteData.Length);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader read = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            Object json = JsonConvert.DeserializeObject(read.ReadToEnd());

            if (json.ToString().Contains("access_token"))
            {
                formConsole("Login successful.");
                userTextBox.Enabled = false;
                passTextBox.Enabled = false;
                appKeyBox.Enabled = false;
                appSecretBox.Enabled = false;
            }
            else
            {
                formConsole("Login failed with error: " + json.ToString());
            }
        }

        public void formConsole(string data)
        {
            outputBox.Text += data + "\n";
            outputBox.SelectionStart = userTextBox.Text.Length;
            outputBox.ScrollToCaret();
        }
    }
}
