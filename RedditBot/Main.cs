using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace RedditBot
{
    public partial class Main : Form
    {
        private User user = null;
        private bool connected, started, cleanup = false;

        public Main()
        {
            InitializeComponent();
        }

        private void accountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountForm account = new AccountForm(this);
            account.StartPosition = FormStartPosition.CenterParent;
            account.ShowDialog();
        }

        private void triggerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TriggerForm trigger = new TriggerForm(this);
            trigger.StartPosition = FormStartPosition.CenterParent;
            trigger.ShowDialog();
        }

        private void responseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResponseForm response = new ResponseForm(this);
            response.StartPosition = FormStartPosition.CenterParent;
            response.ShowDialog();
        }

        private void login_Click(object sender, EventArgs e)
        {
            if (!connected)
            {
                formConsole("Logging in as user " + Properties.Settings.Default["username"].ToString() + "...");
                loginWorker.RunWorkerAsync();
            }

            else
            {
                if (started)
                {
                    formConsole("Run stopped. Cleaning up...");
                    run.Enabled = false;
                    started = false;
                    cleanup = true;
                }
                formConsole("Logged out.");
                connected = false;
                user = null;
                formUpdate();
            }
        }

        private void run_Click(object sender, EventArgs e)
        {
            bool searchTitles = (bool)Properties.Settings.Default["searchTitles"];
            bool searchPosts = (bool)Properties.Settings.Default["searchPosts"];
            bool searchComments = (bool)Properties.Settings.Default["searchComments"];
            bool searchMessages = (bool)Properties.Settings.Default["searchMessages"];
            string trigger = Properties.Settings.Default["trigger"].ToString();
            string subreddit = Properties.Settings.Default["subreddit"].ToString();

            if (!searchTitles && !searchPosts && !searchComments && !searchMessages) { formConsole("Run failed: You must select search locations."); }
            else if (String.IsNullOrEmpty(trigger)) { formConsole("Run failed: Select a trigger to search for."); }
            else if (String.IsNullOrEmpty(subreddit) && (searchTitles || searchPosts || searchComments)) { formConsole("Run failed: Select a subreddit to search in."); }
            else
            {
                if (!started)
                {
                    string searchin = " (";
                    if (searchTitles) { searchin += "titles, "; }
                    if (searchPosts) { searchin += "posts, "; }
                    if (searchComments) { searchin += "comments, "; }
                    if (searchMessages) { searchin += "messages, "; }
                    searchin = searchin.Remove(searchin.Length - 2);
                    searchin += ")";
                    searchin = "/r/" + subreddit + searchin;
                    if (!searchComments && !searchPosts && !searchTitles) { searchin = "private messages"; }
                    formConsole("Run started. Searching for \'" + trigger + "\' in " + searchin);
                    started = true;
                    scanWorker.RunWorkerAsync();
                    formUpdate();
                }
                else
                {
                    formConsole("Run stopped. Cleaning up...");
                    run.Enabled = false;
                    started = false;
                    cleanup = true;
                    formUpdate();
                }
            }
        }

        private void loginWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            user = new User(Properties.Settings.Default["username"].ToString(), Properties.Settings.Default["password"].ToString(),
                Properties.Settings.Default["appkey"].ToString(), Properties.Settings.Default["appsecret"].ToString(), this);
            if (!String.IsNullOrEmpty(user.getToken())) { connected = true; }
            formUpdate();
        }

        private void scanWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string trigger = Properties.Settings.Default["trigger"].ToString();
            string subreddit = Properties.Settings.Default["subreddit"].ToString();
            bool searchTitles = (bool)Properties.Settings.Default["searchTitles"];
            bool searchPosts = (bool)Properties.Settings.Default["searchPosts"];
            bool searchComments = (bool)Properties.Settings.Default["searchComments"];
            bool searchMessages = (bool)Properties.Settings.Default["searchMessages"];

            Scanner scanner = new Scanner(this, user, trigger, subreddit, searchTitles, searchPosts, searchComments, searchMessages);
            while (started)
            {
                scanner.scan();
                System.Threading.Thread.Sleep(20000);
            }
            formConsole("Cleanup finished.");
            cleanup = false;
            formUpdate();
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
                if (connected)
                {
                    loggedin.Text = "Yes";
                    login.Text = "Logout";
                    if (!cleanup) { run.Enabled = true; }
                    lkarma.Text = Convert.ToString(user.getLKarma());
                    ckarma.Text = Convert.ToString(user.getCKarma());
                    messages.Text = user.hasMessages();
                }
                else
                {
                    run.Enabled = false;
                    loggedin.Text = "No";
                    login.Text = "Login";
                    lkarma.Text = "-";
                    ckarma.Text = "-";
                    messages.Text = "-";
                }
                if (started)
                {
                    running.Text = "Yes";
                    run.Text = "Stop";
                }
                else
                {
                    running.Text = "No";
                    run.Text = "Run";
                }
            });
        }

        private void Main_Load(object sender, EventArgs e)
        {
            formUpdate();
        }
    }
}
