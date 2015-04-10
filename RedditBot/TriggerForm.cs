using System;
using System.Windows.Forms;

namespace RedditBot
{
    // Form for entering trigger settings.
    public partial class TriggerForm : Form
    {
        private Main parent;

        public TriggerForm(Main parent)
        {
            InitializeComponent();
            this.parent = parent;
            // If we have saved settings, put them in our boxes.
            searchTextBox.Text = Properties.Settings.Default["trigger"].ToString();
            subredditTextBox.Text = Properties.Settings.Default["subreddit"].ToString();
            titleSearch.Checked = (bool)Properties.Settings.Default["searchTitles"];
            postSearch.Checked = (bool)Properties.Settings.Default["searchPosts"];
            commentSearch.Checked = (bool)Properties.Settings.Default["searchComments"];
            messageSearch.Checked = (bool)Properties.Settings.Default["searchMessages"];
        }

        // On confirm, save settings and close the form.
        private void confirmButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["trigger"] = searchTextBox.Text;
            Properties.Settings.Default["subreddit"] = subredditTextBox.Text;
            Properties.Settings.Default["searchPosts"] = postSearch.Checked;
            Properties.Settings.Default["searchComments"] = commentSearch.Checked;
            Properties.Settings.Default["searchTitles"] = titleSearch.Checked;
            Properties.Settings.Default["searchMessages"] = messageSearch.Checked;
            Properties.Settings.Default.Save();
            parent.formConsole("Trigger settings saved.");
            this.Close();
        }

        // On cancel, just close.
        private void cancelButton_Click(object sender, EventArgs e)
        {
            parent.formConsole("Trigger settings not saved.");
            this.Close();
        }

        private void advanced_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Currently in development.");
        }
    }
}
