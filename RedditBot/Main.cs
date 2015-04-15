using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace RedditBot
{
    // Main form of the application.
    public partial class Main : Form
    {
        // Variables that contain the current state of the application.
        private User user = null;
        private bool connected, started, cleanup = false;

        public Main()
        {
            InitializeComponent();
        }

        // Event handlers to create the Account, Trigger and Response child forms.
        private void accountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountForm account = new AccountForm(this);
            account.StartPosition = FormStartPosition.CenterParent;
            account.ShowDialog();
        }

        private void triggerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string mode = Properties.Settings.Default["mode"].ToString();
            if (mode.Equals("simple"))
            {
                TriggerForm trigger = new TriggerForm(this);
                trigger.StartPosition = FormStartPosition.CenterParent;
                trigger.ShowDialog();
            }
            else
            {
                AdvancedForm trigger = new AdvancedForm(this);
                trigger.StartPosition = FormStartPosition.CenterParent;
                trigger.ShowDialog();
            }
        }

        private void responseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string mode = Properties.Settings.Default["mode"].ToString();
            if (mode.Equals("simple"))
            {
                ResponseForm response = new ResponseForm(this);
                response.StartPosition = FormStartPosition.CenterParent;
                response.ShowDialog();
            }
            else
            {
                AdvancedForm trigger = new AdvancedForm(this);
                trigger.StartPosition = FormStartPosition.CenterParent;
                trigger.ShowDialog();
            }
        }

        // Login button event handler.
        private void login_Click(object sender, EventArgs e)
        {
            // If we aren't already logged in, attempt to log in with the given credentials (via a background worker to prevent locking up).
            if (!connected)
            {
                formConsole("Logging in as user " + Properties.Settings.Default["username"].ToString() + "...");
                loginWorker.RunWorkerAsync();
            }

            // Otherwise, logout by deleting our user object.
            else
            {
                // Quit the scan first if need be.
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

        // Run button event handler.
        private void run_Click(object sender, EventArgs e)
        {
            // Get all of our settings into variables.
            bool searchTitles = (bool)Properties.Settings.Default["searchTitles"];
            bool searchPosts = (bool)Properties.Settings.Default["searchPosts"];
            bool searchComments = (bool)Properties.Settings.Default["searchComments"];
            bool searchMessages = (bool)Properties.Settings.Default["searchMessages"];
            string trigger = Properties.Settings.Default["trigger"].ToString();
            string subreddit = Properties.Settings.Default["subreddit"].ToString();
            string mode = Properties.Settings.Default["mode"].ToString();

            // If any required fields aren't set, abort.
            if (!searchTitles && !searchPosts && !searchComments && !searchMessages && !mode.Equals("advanced")) { formConsole("Run failed: You must select search locations."); }
            else if (String.IsNullOrEmpty(trigger) && !mode.Equals("advanced")) { formConsole("Run failed: Select a trigger to search for."); }
            else if (String.IsNullOrEmpty(subreddit) && (searchTitles || searchPosts || searchComments || mode.Equals("advanced"))) { formConsole("Run failed: Select a subreddit to search in."); }
            else
            {
                // If we aren't scanning already, begin.
                if (!started)
                {
                    if (mode == "simple")
                    {
                        // Build a string for human-readable console output.
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
                    }
                    else { formConsole("Running custom script on /r/" + subreddit + " titles, posts, comments and messages"); }
                    // Begin run (in a background worker).
                    started = true;
                    scanWorker.RunWorkerAsync();
                    formUpdate();
                }
                // If we are already scanning, stop.
                else
                {
                    // We turn off our controlling vars, but the scanner must complete its current run or it will throw an error.
                    // The formUpdate method will disable the run button until cleanup has completed.
                    formConsole("Run stopped. Cleaning up...");
                    run.Enabled = false;
                    started = false;
                    cleanup = true;
                    formUpdate();
                }
            }
        }

        // Our login background worker creates a user object and checks its validity.
        private void loginWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            user = new User(Properties.Settings.Default["username"].ToString(), Properties.Settings.Default["password"].ToString(),
                Properties.Settings.Default["appkey"].ToString(), Properties.Settings.Default["appsecret"].ToString(), this);
            if (!String.IsNullOrEmpty(user.getToken())) { connected = true; }
            formUpdate();
        }

        // The scanner background worker creates a scanner then scans for new posts every 20 seconds until the run is cancelled.
        private void scanWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string trigger = Properties.Settings.Default["trigger"].ToString();
            string subreddit = Properties.Settings.Default["subreddit"].ToString();
            string mode = Properties.Settings.Default["mode"].ToString();
            bool searchTitles = (bool)Properties.Settings.Default["searchTitles"];
            bool searchPosts = (bool)Properties.Settings.Default["searchPosts"];
            bool searchComments = (bool)Properties.Settings.Default["searchComments"];
            bool searchMessages = (bool)Properties.Settings.Default["searchMessages"];

            Scanner scanner = new Scanner(this, user, mode, trigger, subreddit, searchTitles, searchPosts, searchComments, searchMessages);
            while (started)
            {
                scanner.scan();
                System.Threading.Thread.Sleep(20000);
            }
            formConsole("Cleanup finished.");
            cleanup = false;
            formUpdate();
        }

        // This method is used to output text to the console box on the form.
        public void formConsole(string data)
        {
            // Make it thread-safe.
            outputBox.Invoke((MethodInvoker)delegate
            {
                string output = String.Format("\n[{0:HH:mm:ss}] {1}", DateTime.Now, data);
                if (outputBox.Text.Equals("")) { output = String.Format("[{0:HH:mm:ss}] {1}", DateTime.Now, data); }
                outputBox.Text += output;
                outputBox.SelectionStart = outputBox.Text.Length;
                outputBox.ScrollToCaret();
            });
        }

        // This method checks all our state variables and enables, disables, or changes text on controls accordingly.
        private void formUpdate()
        {
            // Make it thread-safe.
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

        // Set all our UI items properly as soon as the form is loaded.
        private void Main_Load(object sender, EventArgs e)
        {
            formUpdate();
        }

        // When the form is closed while the bot is running, ask to minimize to system tray.
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (started)
                {
                    var confirmResult = MessageBox.Show("Bot is running. Minimize to tray?", "Quit", MessageBoxButtons.YesNoCancel);
                    if (confirmResult == DialogResult.Yes)
                    {
                        this.Hide();
                        e.Cancel = true;
                    }
                    else if (confirmResult == DialogResult.No)
                    {
                        this.FormClosing -= Main_FormClosing;
                        this.Close();
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        // Restore from tray on double-click.
        private void trayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        // Restore from tray with context menu.
        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        // Exit application without confirmation.
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.FormClosing -= Main_FormClosing;
            this.Close();
        }
    }
}
