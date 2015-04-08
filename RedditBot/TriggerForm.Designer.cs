namespace RedditBot
{
    partial class TriggerForm
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
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.subredditTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.postSearch = new System.Windows.Forms.CheckBox();
            this.commentSearch = new System.Windows.Forms.CheckBox();
            this.confirmButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.advanced = new System.Windows.Forms.Button();
            this.titleSearch = new System.Windows.Forms.CheckBox();
            this.messageSearch = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(97, 12);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(170, 20);
            this.searchTextBox.TabIndex = 0;
            // 
            // subredditTextBox
            // 
            this.subredditTextBox.Location = new System.Drawing.Point(97, 38);
            this.subredditTextBox.Name = "subredditTextBox";
            this.subredditTextBox.Size = new System.Drawing.Size(170, 20);
            this.subredditTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Trigger:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Search in:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Subreddit:";
            // 
            // postSearch
            // 
            this.postSearch.AutoSize = true;
            this.postSearch.Location = new System.Drawing.Point(186, 66);
            this.postSearch.Name = "postSearch";
            this.postSearch.Size = new System.Drawing.Size(52, 17);
            this.postSearch.TabIndex = 9;
            this.postSearch.Text = "Posts";
            this.postSearch.UseVisualStyleBackColor = true;
            // 
            // commentSearch
            // 
            this.commentSearch.AutoSize = true;
            this.commentSearch.Location = new System.Drawing.Point(98, 89);
            this.commentSearch.Name = "commentSearch";
            this.commentSearch.Size = new System.Drawing.Size(75, 17);
            this.commentSearch.TabIndex = 11;
            this.commentSearch.Text = "Comments";
            this.commentSearch.UseVisualStyleBackColor = true;
            // 
            // confirmButton
            // 
            this.confirmButton.Location = new System.Drawing.Point(97, 122);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(82, 23);
            this.confirmButton.TabIndex = 12;
            this.confirmButton.Text = "Confirm";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(185, 122);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(82, 23);
            this.cancelButton.TabIndex = 13;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // advanced
            // 
            this.advanced.Location = new System.Drawing.Point(9, 122);
            this.advanced.Name = "advanced";
            this.advanced.Size = new System.Drawing.Size(82, 23);
            this.advanced.TabIndex = 15;
            this.advanced.Text = "Advanced";
            this.advanced.UseVisualStyleBackColor = true;
            this.advanced.Click += new System.EventHandler(this.advanced_Click);
            // 
            // titleSearch
            // 
            this.titleSearch.AutoSize = true;
            this.titleSearch.Location = new System.Drawing.Point(98, 66);
            this.titleSearch.Name = "titleSearch";
            this.titleSearch.Size = new System.Drawing.Size(51, 17);
            this.titleSearch.TabIndex = 16;
            this.titleSearch.Text = "Titles";
            this.titleSearch.UseVisualStyleBackColor = true;
            // 
            // messageSearch
            // 
            this.messageSearch.AutoSize = true;
            this.messageSearch.Location = new System.Drawing.Point(186, 89);
            this.messageSearch.Name = "messageSearch";
            this.messageSearch.Size = new System.Drawing.Size(74, 17);
            this.messageSearch.TabIndex = 17;
            this.messageSearch.Text = "Messages";
            this.messageSearch.UseVisualStyleBackColor = true;
            // 
            // TriggerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 157);
            this.Controls.Add(this.messageSearch);
            this.Controls.Add(this.titleSearch);
            this.Controls.Add(this.advanced);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.commentSearch);
            this.Controls.Add(this.postSearch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.subredditTextBox);
            this.Controls.Add(this.searchTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "TriggerForm";
            this.Text = "Trigger Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.TextBox subredditTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox postSearch;
        private System.Windows.Forms.CheckBox commentSearch;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button advanced;
        private System.Windows.Forms.CheckBox titleSearch;
        private System.Windows.Forms.CheckBox messageSearch;
    }
}