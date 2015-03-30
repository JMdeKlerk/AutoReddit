using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

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
            if (Properties.Settings.Default["username"].ToString().Equals(""))
            {
                formConsole("Login failed: You must set up your account first.");
            }
            else
            {
                formConsole("Logging in as user " + Properties.Settings.Default["username"].ToString() + "...");
                loginWorker.RunWorkerAsync();
            }
        }

        public void formConsole(string data)
        {
            outputBox.Invoke((MethodInvoker)delegate
            {
                outputBox.Text += data + "\n";
                outputBox.SelectionStart = outputBox.Text.Length;
                outputBox.ScrollToCaret();
            });
        }

        private void loginWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            user = new User(Properties.Settings.Default["username"].ToString(), Properties.Settings.Default["password"].ToString(),
                Properties.Settings.Default["appkey"].ToString(), Properties.Settings.Default["appsecret"].ToString(), this);
            if (!user.getToken().Equals(""))
            {
                login.Invoke((MethodInvoker)delegate { login.Text = "Logout"; });
                username.Invoke((MethodInvoker)delegate { username.Text = user.getUsername(); });
                loggedin.Invoke((MethodInvoker)delegate { loggedin.Text = "Yes"; });
                ApiRequest request = new ApiRequest(user, "https://oauth.reddit.com/api/v1/me", "GET");
                Dictionary<string, string> userinfo = request.getResponse();
                string value;
                if (userinfo.TryGetValue("link_karma", out value)) { lkarma.Invoke((MethodInvoker)delegate { lkarma.Text = value; }); }
                if (userinfo.TryGetValue("comment_karma", out value)) { ckarma.Invoke((MethodInvoker)delegate { ckarma.Text = value; }); }
            }
        }

        private void scanWorker_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void accountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountForm account = new AccountForm(this);
            account.ShowDialog();
        }

        //private void test_Click(object sender, EventArgs e)
        //{
        //    Hashtable args = new Hashtable();
        //    args.Add("title", "Test Post");
        //    args.Add("text", "pls ignore");
        //    args.Add("sr", "Shindogo");
        //    args.Add("kind", "self");

        //    ApiRequest request = new ApiRequest(user, "https://oauth.reddit.com/api/submit", "POST", args);
        //}
    }
}
