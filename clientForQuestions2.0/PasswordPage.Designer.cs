namespace clientForQuestions2._0
{
    partial class PasswordPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PasswordPage));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.hintPassLabel = new System.Windows.Forms.Label();
            this.macLabel = new System.Windows.Forms.Label();
            this.continueButton = new clientForQuestions2._0.RJButtons2();
            this.loggedInButton = new clientForQuestions2._0.RJButtons2();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(251, 226);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(203, 22);
            this.textBox1.TabIndex = 0;
            // 
            // hintPassLabel
            // 
            this.hintPassLabel.AutoSize = true;
            this.hintPassLabel.Location = new System.Drawing.Point(48, 131);
            this.hintPassLabel.Name = "hintPassLabel";
            this.hintPassLabel.Size = new System.Drawing.Size(92, 16);
            this.hintPassLabel.TabIndex = 1;
            this.hintPassLabel.Text = "hintPassLabel";
            // 
            // macLabel
            // 
            this.macLabel.AutoSize = true;
            this.macLabel.Location = new System.Drawing.Point(500, 131);
            this.macLabel.Name = "macLabel";
            this.macLabel.Size = new System.Drawing.Size(67, 16);
            this.macLabel.TabIndex = 3;
            this.macLabel.Text = "macLabel";
            this.macLabel.Click += new System.EventHandler(this.macLabel_Click);
            // 
            // continueButton
            // 
            this.continueButton.BackColor = System.Drawing.Color.Black;
            this.continueButton.BorderRadiuos = 32;
            this.continueButton.BorderSize = 1;
            this.continueButton.FlatAppearance.BorderSize = 0;
            this.continueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.continueButton.ForeColor = System.Drawing.Color.White;
            this.continueButton.Location = new System.Drawing.Point(289, 269);
            this.continueButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(133, 32);
            this.continueButton.TabIndex = 2;
            this.continueButton.Text = "continue";
            this.continueButton.UseVisualStyleBackColor = false;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // loggedInButton
            // 
            this.loggedInButton.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.loggedInButton.BorderRadiuos = 40;
            this.loggedInButton.BorderSize = 1;
            this.loggedInButton.FlatAppearance.BorderSize = 0;
            this.loggedInButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loggedInButton.ForeColor = System.Drawing.Color.White;
            this.loggedInButton.Location = new System.Drawing.Point(76, 37);
            this.loggedInButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.loggedInButton.Name = "loggedInButton";
            this.loggedInButton.Size = new System.Drawing.Size(144, 59);
            this.loggedInButton.TabIndex = 4;
            this.loggedInButton.Text = "Already logged in?\nClick here";
            this.loggedInButton.UseVisualStyleBackColor = false;
            this.loggedInButton.Click += new System.EventHandler(this.loggedInButton_Click);
            // 
            // PasswordPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 420);
            this.Controls.Add(this.loggedInButton);
            this.Controls.Add(this.macLabel);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.hintPassLabel);
            this.Controls.Add(this.textBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "PasswordPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "התחברות";
            this.Load += new System.EventHandler(this.PasswordPage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label hintPassLabel;
        private RJButtons2 continueButton;
        private System.Windows.Forms.Label macLabel;
        private RJButtons2 loggedInButton;
    }
}