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
            double postAfter = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds, commentAfter = postAfter;
            bool searchPosts = (bool)Properties.Settings.Default["searchPosts"];
            bool searchComments = (bool)Properties.Settings.Default["searchComments"];
            while (started)
            {
                string url = "https://www.reddit.com/r/" + Properties.Settings.Default["subreddit"].ToString() + "/search/.json?restrict_sr=true&limit=25&sort=new";
                ApiRequest requestPosts = new ApiRequest(url, "GET");
                dynamic postResults = requestPosts.getResponse();

                url = "https://www.reddit.com/r/" + Properties.Settings.Default["subreddit"].ToString() + "/comments/.json";
                ApiRequest requestComments = new ApiRequest(url, "GET");
                dynamic commentResults = requestComments.getResponse();

                for (int i = 24; i >= 0; i--)
                {
                    string title = postResults.data.children[i].data.title;
                    string body = postResults.data.children[i].data.selftext;
                    string comment = commentResults.data.children[i].data.body;
                    string postAuthor = postResults.data.children[i].data.author;
                    string commentAuthor = commentResults.data.children[i].data.author;
                    string trigger = Properties.Settings.Default["trigger"].ToString();
                    double postCreated = postResults.data.children[i].data.created_utc;
                    double commentCreated = commentResults.data.children[i].data.created_utc;

                    if (postCreated > postAfter)
                    {
                        if (title.ToLower().Contains(trigger.ToLower()) && searchPosts)
                        {
                            string preview;
                            if (title.Length > 25) { preview = title.Remove(24).Replace("\n", " ") + "..."; }
                            else { preview = title.Replace("\n", " "); }
                            formConsole("Title: \'" + preview + "\' by /u/" + postAuthor);
                        }
                        if (body.ToLower().Contains(trigger.ToLower()) && searchPosts)
                        {
                            string preview;
                            if (body.Length > 25) { preview = body.Remove(24).Replace("\n", " ") + "..."; }
                            else { preview = body.Replace("\n", " "); }
                            formConsole("Body: \'" + body + "\' by /u/" + postAuthor);
                        }
                        postAfter = postCreated;
                    }

                    if (commentCreated > commentAfter)
                    {
                        if (comment.ToLower().Contains(trigger.ToLower()) && searchComments)
                        {
                            string preview;
                            if (comment.Length > 25) { preview = comment.Remove(24).Replace("\n", " ") + "..."; }
                            else { preview = comment.Replace("\n", " "); }
                            formConsole("Comment: \'" + preview + "\' by /u/" + commentAuthor);
                        }
                        commentAfter = commentCreated;
                    }
                }

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

        private void outputBox_LinkClicked(object sender, System.Windows.Forms.LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }
    }
}
