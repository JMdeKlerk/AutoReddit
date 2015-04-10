using System;
using System.Windows.Forms;

namespace RedditBot
{
    // Form for entering response settings.
    public partial class ResponseForm : Form
    {
        private Main parent;

        public ResponseForm(Main parent)
        {
            InitializeComponent();
            this.parent = parent;
            // If we have saved settings, put them in our boxes.
            action.Text = Properties.Settings.Default["action"].ToString();
            contentBox.Text = Properties.Settings.Default["content"].ToString();
            if (action.Text == "Alert") { contentBox.Enabled = false; }
            else { contentBox.Enabled = true; }
        }

        // Enable/disable content box based on selected action.
        private void action_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (action.Text == "Alert") { contentBox.Enabled = false; contentBox.Text = ""; }
            else { contentBox.Enabled = true; contentBox.Text = Properties.Settings.Default["content"].ToString(); }
        }

        // On confirm, save settings and close form.
        private void confirmButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["action"] = action.Text;
            if (action.Text != "Alert") { Properties.Settings.Default["content"] = contentBox.Text; }
            Properties.Settings.Default.Save();
            parent.formConsole("Response settings saved.");
            this.Close();
        }

        // On cancel, just close.
        private void cancelButton_Click(object sender, EventArgs e)
        {
            parent.formConsole("Response settings not saved.");
            this.Close();
        }

        private void advanced_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Currently in development.");
        }

        // Enable/disable content box based on selected action.
        private void ResponseForm_Load(object sender, EventArgs e)
        {
            if (action.Text == "Alert") { contentBox.Enabled = false; contentBox.Text = ""; }
            else { contentBox.Enabled = true; contentBox.Text = Properties.Settings.Default["content"].ToString(); }
        }
    }
}
