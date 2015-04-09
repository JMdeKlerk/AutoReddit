namespace RedditBot
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.login = new System.Windows.Forms.Button();
            this.outputBox = new System.Windows.Forms.RichTextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.loginWorker = new System.ComponentModel.BackgroundWorker();
            this.scanWorker = new System.ComponentModel.BackgroundWorker();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.accountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.triggerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.responseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.messages = new System.Windows.Forms.TextBox();
            this.ckarma = new System.Windows.Forms.TextBox();
            this.loggedin = new System.Windows.Forms.TextBox();
            this.running = new System.Windows.Forms.TextBox();
            this.lkarma = new System.Windows.Forms.TextBox();
            this.username = new System.Windows.Forms.TextBox();
            this.run = new System.Windows.Forms.Button();
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.groupBox4.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // login
            // 
            this.login.Location = new System.Drawing.Point(16, 208);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(87, 23);
            this.login.TabIndex = 0;
            this.login.Text = "Login";
            this.login.UseVisualStyleBackColor = true;
            this.login.Click += new System.EventHandler(this.login_Click);
            // 
            // outputBox
            // 
            this.outputBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputBox.BackColor = System.Drawing.SystemColors.Control;
            this.outputBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.outputBox.DetectUrls = false;
            this.outputBox.Font = new System.Drawing.Font("Consolas", 8F);
            this.outputBox.ForeColor = System.Drawing.SystemColors.InfoText;
            this.outputBox.Location = new System.Drawing.Point(6, 15);
            this.outputBox.Name = "outputBox";
            this.outputBox.ReadOnly = true;
            this.outputBox.Size = new System.Drawing.Size(498, 179);
            this.outputBox.TabIndex = 6;
            this.outputBox.Text = "";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.outputBox);
            this.groupBox4.Location = new System.Drawing.Point(208, 27);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(510, 204);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            // 
            // loginWorker
            // 
            this.loginWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.loginWorker_DoWork);
            // 
            // scanWorker
            // 
            this.scanWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.scanWorker_DoWork);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accountToolStripMenuItem,
            this.triggerToolStripMenuItem,
            this.responseToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(730, 24);
            this.menuStrip.TabIndex = 15;
            this.menuStrip.Text = "menuStrip";
            // 
            // accountToolStripMenuItem
            // 
            this.accountToolStripMenuItem.Name = "accountToolStripMenuItem";
            this.accountToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.accountToolStripMenuItem.Text = "Account";
            this.accountToolStripMenuItem.Click += new System.EventHandler(this.accountToolStripMenuItem_Click);
            // 
            // triggerToolStripMenuItem
            // 
            this.triggerToolStripMenuItem.Name = "triggerToolStripMenuItem";
            this.triggerToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.triggerToolStripMenuItem.Text = "Trigger";
            this.triggerToolStripMenuItem.Click += new System.EventHandler(this.triggerToolStripMenuItem_Click);
            // 
            // responseToolStripMenuItem
            // 
            this.responseToolStripMenuItem.Name = "responseToolStripMenuItem";
            this.responseToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.responseToolStripMenuItem.Text = "Response";
            this.responseToolStripMenuItem.Click += new System.EventHandler(this.responseToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.messages);
            this.groupBox1.Controls.Add(this.ckarma);
            this.groupBox1.Controls.Add(this.loggedin);
            this.groupBox1.Controls.Add(this.running);
            this.groupBox1.Controls.Add(this.lkarma);
            this.groupBox1.Controls.Add(this.username);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(190, 175);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Messages:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Comment Karma:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Link Karma:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Running:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Logged in:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Username:";
            // 
            // messages
            // 
            this.messages.Enabled = false;
            this.messages.Location = new System.Drawing.Point(97, 145);
            this.messages.Name = "messages";
            this.messages.Size = new System.Drawing.Size(87, 20);
            this.messages.TabIndex = 5;
            this.messages.Text = "-";
            this.messages.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ckarma
            // 
            this.ckarma.Enabled = false;
            this.ckarma.Location = new System.Drawing.Point(97, 119);
            this.ckarma.Name = "ckarma";
            this.ckarma.Size = new System.Drawing.Size(87, 20);
            this.ckarma.TabIndex = 4;
            this.ckarma.Text = "-";
            this.ckarma.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // loggedin
            // 
            this.loggedin.Enabled = false;
            this.loggedin.Location = new System.Drawing.Point(97, 41);
            this.loggedin.Name = "loggedin";
            this.loggedin.Size = new System.Drawing.Size(87, 20);
            this.loggedin.TabIndex = 3;
            this.loggedin.Text = "No";
            this.loggedin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // running
            // 
            this.running.Enabled = false;
            this.running.Location = new System.Drawing.Point(97, 67);
            this.running.Name = "running";
            this.running.Size = new System.Drawing.Size(87, 20);
            this.running.TabIndex = 2;
            this.running.Text = "No";
            this.running.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lkarma
            // 
            this.lkarma.Enabled = false;
            this.lkarma.Location = new System.Drawing.Point(97, 93);
            this.lkarma.Name = "lkarma";
            this.lkarma.Size = new System.Drawing.Size(87, 20);
            this.lkarma.TabIndex = 1;
            this.lkarma.Text = "-";
            this.lkarma.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // username
            // 
            this.username.Enabled = false;
            this.username.Location = new System.Drawing.Point(97, 15);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(87, 20);
            this.username.TabIndex = 0;
            this.username.Text = "-";
            this.username.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // run
            // 
            this.run.Enabled = false;
            this.run.Location = new System.Drawing.Point(109, 208);
            this.run.Name = "run";
            this.run.Size = new System.Drawing.Size(87, 23);
            this.run.TabIndex = 1;
            this.run.Text = "Run";
            this.run.UseVisualStyleBackColor = true;
            this.run.Click += new System.EventHandler(this.run_Click);
            // 
            // trayIcon
            // 
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "Reddit Bot";
            this.trayIcon.Visible = true;
            this.trayIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.trayIcon_MouseDoubleClick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 243);
            this.Controls.Add(this.login);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.run);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(746, 281);
            this.Name = "Main";
            this.Text = "C# Reddit Bot 0.1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox4.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button login;
        private System.Windows.Forms.RichTextBox outputBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.ComponentModel.BackgroundWorker loginWorker;
        private System.ComponentModel.BackgroundWorker scanWorker;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem accountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem triggerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem responseToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button run;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox messages;
        private System.Windows.Forms.TextBox ckarma;
        private System.Windows.Forms.TextBox loggedin;
        private System.Windows.Forms.TextBox running;
        private System.Windows.Forms.TextBox lkarma;
        public System.Windows.Forms.TextBox username;
        private System.Windows.Forms.NotifyIcon trayIcon;
    }
}

