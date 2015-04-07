using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedditBot
{
    public partial class ResponseForm : Form
    {
        private Main parent;

        public ResponseForm(Main parent)
        {
            InitializeComponent();
            this.parent = parent;
            action.Text = Properties.Settings.Default["action"].ToString();
            contentBox.Text = Properties.Settings.Default["content"].ToString();
            if (action.Text == "Alert") { contentBox.Enabled = false; }
            else { contentBox.Enabled = true; }
        }

        private void action_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (action.Text == "Alert") { contentBox.Enabled = false; }
            else { contentBox.Enabled = true; }
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["action"] = action.Text;
            Properties.Settings.Default["content"] = contentBox.Text;
            Properties.Settings.Default.Save();
            parent.formConsole("Response settings saved.");
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            parent.formConsole("Response settings not saved.");
            this.Close();
        }

        private void advanced_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Currently in development.");
        }
    }
}
