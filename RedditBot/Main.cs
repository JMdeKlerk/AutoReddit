using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace RedditBot
{
    public partial class Main : Form
    {
        private User user = null;
        private bool connected, started = false;

        public Main()
        {
            InitializeComponent();
            username.Text = Properties.Settings.Default["username"].ToString();
        }

        private void login_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default["username"].ToString().Equals("")) { formConsole("Login failed: You must set up your account first."); }
            else
            {
                formConsole("Logging in as user " + Properties.Settings.Default["username"].ToString() + "...");
                loginWorker.RunWorkerAsync();
            }
        }

        private void run_Click(object sender, EventArgs e)
        {
            if (!connected) { formConsole("Run failed: You must log in first. "); }
            else if (Properties.Settings.Default["trigger"].ToString().Equals("")) { formConsole("Run failed: Set your trigger and response."); }
            else
            {
                started = true;
                formConsole("Run started. Searching for \'" + Properties.Settings.Default["trigger"].ToString() + "\' in /r/" + Properties.Settings.Default["subreddit"].ToString());
                run.Text = "Stop";
                running.Text = "Yes";
                started = true;
                scanWorker.RunWorkerAsync();
            }
        }

        private void accountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountForm account = new AccountForm(this);
            account.ShowDialog();
        }

        private void triggerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TriggerForm trigger = new TriggerForm(this);
            trigger.ShowDialog();
        }

        private void loginWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            user = new User(Properties.Settings.Default["username"].ToString(), Properties.Settings.Default["password"].ToString(),
                Properties.Settings.Default["appkey"].ToString(), Properties.Settings.Default["appsecret"].ToString(), this);
            if (!user.getToken().Equals(""))
            {
                connected = true;
                login.Invoke((MethodInvoker)delegate { login.Text = "Logout"; });
                username.Invoke((MethodInvoker)delegate { username.Text = user.getUsername(); });
                loggedin.Invoke((MethodInvoker)delegate { loggedin.Text = "Yes"; });
                ApiRequest request = new ApiRequest(user, "https://oauth.reddit.com/api/v1/me", "GET");
                dynamic userinfo = request.getResponse();
                lkarma.Invoke((MethodInvoker)delegate { lkarma.Text = userinfo.link_karma; });
                ckarma.Invoke((MethodInvoker)delegate { ckarma.Text = userinfo.comment_karma; });
                messages.Invoke((MethodInvoker)delegate { messages.Text = userinfo.has_mail; });
            }
        }

        private void scanWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string after = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds.ToString();
            while (started)
            {
                string url = "https://www.reddit.com/r/" + Properties.Settings.Default["subreddit"].ToString() + "/search/.json";
                url += "?q=" + Properties.Settings.Default["trigger"].ToString() + "&restrict_sr=true&limit=1&sort=new";
                ApiRequest request = new ApiRequest(url, "GET");
                dynamic listresults = request.getResponse();
                if (listresults.data.children[0].data.created_utc > after)
                {
                    formConsole("Found match: \'" + listresults.data.children[0].data.title + "\' by /u/" + listresults.data.children[0].data.author);
                    after = listresults.data.children[0].data.created_utc;
                }
                System.Threading.Thread.Sleep(10000);
            }
        }

        public void formConsole(string data)
        {
            outputBox.Invoke((MethodInvoker)delegate
            {
                outputBox.Text += "\n" + data;
                outputBox.SelectionStart = outputBox.Text.Length;
                outputBox.ScrollToCaret();
            });
        }

        private void outputBox_LinkClicked(object sender, System.Windows.Forms.LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }
    }
}
