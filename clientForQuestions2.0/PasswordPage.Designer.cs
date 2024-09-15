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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.hintPassLabel = new System.Windows.Forms.Label();
            this.macLabel = new System.Windows.Forms.Label();
            this.continueButton = new clientForQuestions2._0.RJButtons2();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(282, 282);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(228, 26);
            this.textBox1.TabIndex = 0;
            // 
            // hintPassLabel
            // 
            this.hintPassLabel.AutoSize = true;
            this.hintPassLabel.Location = new System.Drawing.Point(54, 164);
            this.hintPassLabel.Name = "hintPassLabel";
            this.hintPassLabel.Size = new System.Drawing.Size(109, 20);
            this.hintPassLabel.TabIndex = 1;
            this.hintPassLabel.Text = "hintPassLabel";
            // 
            // macLabel
            // 
            this.macLabel.AutoSize = true;
            this.macLabel.Location = new System.Drawing.Point(562, 164);
            this.macLabel.Name = "macLabel";
            this.macLabel.Size = new System.Drawing.Size(78, 20);
            this.macLabel.TabIndex = 3;
            this.macLabel.Text = "macLabel";
            // 
            // continueButton
            // 
            this.continueButton.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.continueButton.BorderRadiuos = 40;
            this.continueButton.BorderSize = 1;
            this.continueButton.FlatAppearance.BorderSize = 0;
            this.continueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.continueButton.ForeColor = System.Drawing.Color.White;
            this.continueButton.Location = new System.Drawing.Point(308, 367);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(150, 40);
            this.continueButton.TabIndex = 2;
            this.continueButton.Text = "continue";
            this.continueButton.UseVisualStyleBackColor = false;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // PasswordPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 525);
            this.Controls.Add(this.macLabel);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.hintPassLabel);
            this.Controls.Add(this.textBox1);
            this.Name = "PasswordPage";
            this.Text = "PasswordPage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label hintPassLabel;
        private RJButtons2 continueButton;
        private System.Windows.Forms.Label macLabel;
    }
}