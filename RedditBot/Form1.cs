using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace RedditBot
{
    public partial class Form1 : Form
    {
        User user;

        public Form1()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, EventArgs e)
        {
            formConsole("Attempting to login as user " + userTextBox.Text + "...");
            user = new User(userTextBox.Text, passTextBox.Text, appKeyBox.Text, appSecretBox.Text);
        }

        private void start_Click(object sender, EventArgs e)
        {
            string url = "https://oauth.reddit.com/api/v1/me";
        }

        public void formConsole(string data)
        {
            outputBox.Text += data + "\n";
            outputBox.SelectionStart = userTextBox.Text.Length;
            outputBox.ScrollToCaret();
        }
    }
}
