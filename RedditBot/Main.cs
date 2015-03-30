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
using System.IO;
using System.Runtime.InteropServices;

namespace RedditBot
{
    public partial class Main : Form
    {
        private User user = null;

        public Main()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, EventArgs e)
        {
            formConsole("Logging in as user " + userTextBox.Text + "...");
            loginWorker.RunWorkerAsync();
        }

        public void formConsole(string data)
        {
            outputBox.Invoke((MethodInvoker) delegate
            {
                outputBox.Text += data + "\n";
                outputBox.SelectionStart = outputBox.Text.Length;
                outputBox.ScrollToCaret();
            });
        }

        private void loginWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            user = new User(userTextBox.Text, passTextBox.Text, appKeyBox.Text, appSecretBox.Text, this);
            if (!user.getToken().Equals(""))
            {
                userTextBox.Invoke((MethodInvoker)delegate { userTextBox.Enabled = false; });
                passTextBox.Invoke((MethodInvoker)delegate { passTextBox.Enabled = false; });
                appKeyBox.Invoke((MethodInvoker)delegate { appKeyBox.Enabled = false; });
                appSecretBox.Invoke((MethodInvoker)delegate { appSecretBox.Enabled = false; });
            }
        }

        private void test_Click(object sender, EventArgs e)
        {
            string url = "https://oauth.reddit.com/api/submit";
            string postData = "sr=Shindogo&title=Test post&kind=text&text=pls ignore";
            ASCIIEncoding encode = new ASCIIEncoding();
            byte[] byteData = encode.GetBytes(postData);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteData.Length;

            string auth = user.getToken();
            auth = "bearer " + auth;
            formConsole("Using auth " + auth);
            request.Headers.Add("AUTHORIZATION", auth);

            request.UserAgent = "Shinborg/0.1 by /u/Shindogo";
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteData, 0, byteData.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader read = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            Console.WriteLine(read.ReadToEnd());
        }
    }
}
