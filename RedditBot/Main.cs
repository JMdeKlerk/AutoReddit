using Newtonsoft.Json.Linq;
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
        }

        private void login_Click(object sender, EventArgs e)
        {
            if (!connected)
            {
                formConsole("Logging in as user " + Properties.Settings.Default["username"].ToString() + "...");
                loginWorker.RunWorkerAsync();
            }

            if (connected)
            {
                formConsole("Logging out.");
                connected = false;
                user = null;
                formUpdate();
            }
        }

        private void run_Click(object sender, EventArgs e)
        {
            if (!connected) { formConsole("Run failed: You must log in first. "); }
            else
            {
                formConsole("Run started. Searching for \'" + Properties.Settings.Default["trigger"].ToString() + "\' in /r/" + Properties.Settings.Default["subreddit"].ToString());
                started = true;
                scanWorker.RunWorkerAsync();
                formUpdate();
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
            if (!user.getToken().Equals("")) { connected = true; }
            formUpdate();
        }

        private void scanWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string trigger = Properties.Settings.Default["trigger"].ToString();
            string subreddit = Properties.Settings.Default["subreddit"].ToString();
            bool searchPosts = (bool)Properties.Settings.Default["searchPosts"];
            bool searchComments = (bool)Properties.Settings.Default["searchComments"];

            Scanner scanner = new Scanner(this, trigger, subreddit, searchPosts, searchComments);
            while (started)
            {
                scanner.scanPosts();
                scanner.scanComments();
                System.Threading.Thread.Sleep(10000);
            }
        }

        public void formConsole(string data)
        {
            outputBox.Invoke((MethodInvoker)delegate
            {
                string output = String.Format("\n[{0:HH:mm:ss}] {1}", DateTime.Now, data);
                if (outputBox.Text.Equals("")) { output = String.Format("[{0:HH:mm:ss}] {1}", DateTime.Now, data); }
                outputBox.Text += output;
                outputBox.SelectionStart = outputBox.Text.Length;
                outputBox.ScrollToCaret();
            });
        }

        private void formUpdate()
        {
            this.Invoke((MethodInvoker)delegate
            {
                username.Text = Properties.Settings.Default["username"].ToString();
                if (started) { running.Text = "Yes"; } else { running.Text = "No"; }
                if (connected)
                {
                    loggedin.Text = "Yes";
                    lkarma.Text = Convert.ToString(user.getLKarma());
                    ckarma.Text = Convert.ToString(user.getCKarma());
                    messages.Text = user.hasMessages();
                }
                else
                {
                    loggedin.Text = "No";
                    lkarma.Text = "-";
                    ckarma.Text = "-";
                    messages.Text = "-";
                }
            });
        }
    }
}
