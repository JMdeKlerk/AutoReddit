using System;
using System.Windows.Forms;

namespace RedditBot
{
    // Form for entering account details.
    public partial class AccountForm : Form
    {
        private Main parent = null;

        public AccountForm(Main parent)
        {
            InitializeComponent();
            this.parent = parent;
            // If we have saved details, put them in our boxes.
            userTextBox.Text = Properties.Settings.Default["username"].ToString();
            passTextBox.Text = Properties.Settings.Default["password"].ToString();
            keyTextBox.Text = Properties.Settings.Default["appkey"].ToString();
            secretTextBox.Text = Properties.Settings.Default["appsecret"].ToString();
        }

        // On confirm, save our new information and close the form.
        private void confirmButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["username"] = userTextBox.Text;
            Properties.Settings.Default["password"] = passTextBox.Text;
            Properties.Settings.Default["appkey"] = keyTextBox.Text;
            Properties.Settings.Default["appsecret"] = secretTextBox.Text;
            if (remember.Checked) { Properties.Settings.Default.Save(); }
            parent.username.Text = userTextBox.Text;
            parent.formConsole("Account settings saved.");
            this.Close();
        }

        // On cancel, just close.
        private void cancelButton_Click(object sender, EventArgs e)
        {
            parent.formConsole("Account settings not saved.");
            this.Close();
        }
    }
}
