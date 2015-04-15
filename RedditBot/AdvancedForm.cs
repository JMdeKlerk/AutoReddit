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
            subredditTextBox.Text = Properties.Settings.Default["subreddit"].ToString();
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
            Properties.Settings.Default["subreddit"] = subredditTextBox.Text;
            Properties.Settings.Default.Save();
            parent.formConsole("Script saved.");
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            parent.formConsole("Script not saved.");
            this.Close();
        }

        private void help_Click(object sender, EventArgs e)
        {
            string help = String.Join(Environment.NewLine + Environment.NewLine,
                "You can use this editor to create a python script that will run on each search result and determine your response. You must have python installed on your computer.",
                "Input: The script will be given arguments containing the type of result (title, post, comment or message), the username of the author, and the full text of the result.",
                "Output: The script should print one of 'alert', 'reply' or 'message' followed by the contents of the response.",
                "An example script has been given for you to start.");
            string script = String.Join(Environment.NewLine,
                "import sys\n",
                "type = str(sys.argv[1])",
                "author = str(sys.argv[2])",
                "text = \"\"\n", 
                "for eacharg in sys.argv[3:]:",
                "\ttext += eacharg + \" \"\n",
                "if (type == \"title\" and author == \"Snoo\"):",
                "\tif (\"Anyone out there?\" in text):",
                "\t\tprint(\"reply Hello World!\")");
            scriptTextBox.Text = script;
            MessageBox.Show(help);
        }
    }
}
