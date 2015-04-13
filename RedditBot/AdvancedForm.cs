using System;
using System.IO;
using System.Windows.Forms;

namespace RedditBot
{
    public partial class AdvancedForm : Form
    {
        private Main parent;

        public AdvancedForm(Main parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void AdvancedForm_Load(object sender, EventArgs e)
        {
            if (File.Exists("script.py"))
            {
                scriptTextBox.LoadFile("script.py", RichTextBoxStreamType.PlainText);
            }
        }

        private void simple_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["mode"] = "simple";
            Properties.Settings.Default.Save();
            parent.formConsole("Returned to simple mode.");
            this.Close();
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            scriptTextBox.SaveFile("script.py", RichTextBoxStreamType.PlainText);
            parent.formConsole("Script saved.");
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            parent.formConsole("Script not saved.");
            this.Close();
        }
    }
}
