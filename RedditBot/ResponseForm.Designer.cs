namespace RedditBot
{
    partial class ResponseForm
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
            this.action = new System.Windows.Forms.ComboBox();
            this.advanced = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.confirmButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.contentBox = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // action
            // 
            this.action.FormattingEnabled = true;
            this.action.Items.AddRange(new object[] {
            "Alert",
            "Reply",
            "Message"});
            this.action.Location = new System.Drawing.Point(97, 12);
            this.action.Name = "action";
            this.action.Size = new System.Drawing.Size(170, 21);
            this.action.TabIndex = 1;
            this.action.Text = "Alert";
            this.action.SelectedIndexChanged += new System.EventHandler(this.action_SelectedIndexChanged);
            // 
            // advanced
            // 
            this.advanced.Location = new System.Drawing.Point(9, 122);
            this.advanced.Name = "advanced";
            this.advanced.Size = new System.Drawing.Size(82, 23);
            this.advanced.TabIndex = 19;
            this.advanced.Text = "Advanced";
            this.advanced.UseVisualStyleBackColor = true;
            this.advanced.Click += new System.EventHandler(this.advanced_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(185, 122);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(82, 23);
            this.cancelButton.TabIndex = 17;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // confirmButton
            // 
            this.confirmButton.Location = new System.Drawing.Point(97, 122);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(82, 23);
            this.confirmButton.TabIndex = 16;
            this.confirmButton.Text = "Confirm";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Action:";
            // 
            // contentBox
            // 
            this.contentBox.Enabled = false;
            this.contentBox.Location = new System.Drawing.Point(97, 40);
            this.contentBox.Name = "contentBox";
            this.contentBox.Size = new System.Drawing.Size(170, 76);
            this.contentBox.TabIndex = 21;
            this.contentBox.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Content:";
            // 
            // ResponseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 157);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.contentBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.advanced);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.action);
            this.Name = "ResponseForm";
            this.Text = "Response Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox action;
        private System.Windows.Forms.Button advanced;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox contentBox;
        private System.Windows.Forms.Label label2;
    }
}