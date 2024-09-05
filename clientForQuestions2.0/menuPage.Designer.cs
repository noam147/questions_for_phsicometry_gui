namespace clientForQuestions2._0
{
    partial class menuPage
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
            this.label2 = new System.Windows.Forms.Label();
            this.amountOfQuestionTextBox = new System.Windows.Forms.TextBox();
            this.analogyotButton = new System.Windows.Forms.Button();
            this.hestabrotButton = new System.Windows.Forms.Button();
            this.continueButton = new System.Windows.Forms.Button();
            this.topicsLabel = new System.Windows.Forms.Label();
            this.hociotButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(147, 268);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "amount of questions:";
            // 
            // amountOfQuestionTextBox
            // 
            this.amountOfQuestionTextBox.Location = new System.Drawing.Point(151, 313);
            this.amountOfQuestionTextBox.Name = "amountOfQuestionTextBox";
            this.amountOfQuestionTextBox.Size = new System.Drawing.Size(100, 26);
            this.amountOfQuestionTextBox.TabIndex = 11;
            // 
            // analogyotButton
            // 
            this.analogyotButton.Location = new System.Drawing.Point(538, 49);
            this.analogyotButton.Name = "analogyotButton";
            this.analogyotButton.Size = new System.Drawing.Size(115, 50);
            this.analogyotButton.TabIndex = 10;
            this.analogyotButton.Text = "אנלוגיות";
            this.analogyotButton.UseVisualStyleBackColor = true;
            this.analogyotButton.Click += new System.EventHandler(this.analogyotButton_Click);
            // 
            // hestabrotButton
            // 
            this.hestabrotButton.Location = new System.Drawing.Point(290, 46);
            this.hestabrotButton.Name = "hestabrotButton";
            this.hestabrotButton.Size = new System.Drawing.Size(146, 49);
            this.hestabrotButton.TabIndex = 9;
            this.hestabrotButton.Text = "הסתברות";
            this.hestabrotButton.UseVisualStyleBackColor = true;
            this.hestabrotButton.Click += new System.EventHandler(this.hestabrotButton_Click);
            // 
            // continueButton
            // 
            this.continueButton.Location = new System.Drawing.Point(473, 289);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(141, 113);
            this.continueButton.TabIndex = 8;
            this.continueButton.Text = "continue";
            this.continueButton.UseVisualStyleBackColor = true;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // topicsLabel
            // 
            this.topicsLabel.AutoSize = true;
            this.topicsLabel.Location = new System.Drawing.Point(823, 60);
            this.topicsLabel.Name = "topicsLabel";
            this.topicsLabel.Size = new System.Drawing.Size(51, 20);
            this.topicsLabel.TabIndex = 13;
            this.topicsLabel.Text = "label1";
            // 
            // hociotButton
            // 
            this.hociotButton.Location = new System.Drawing.Point(601, 203);
            this.hociotButton.Name = "hociotButton";
            this.hociotButton.Size = new System.Drawing.Size(167, 45);
            this.hociotButton.TabIndex = 14;
            this.hociotButton.Text = "תלת-ממד";
            this.hociotButton.UseVisualStyleBackColor = true;
            this.hociotButton.Click += new System.EventHandler(this.hociotButton_Click);
            // 
            // menuPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 944);
            this.Controls.Add(this.hociotButton);
            this.Controls.Add(this.topicsLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.amountOfQuestionTextBox);
            this.Controls.Add(this.analogyotButton);
            this.Controls.Add(this.hestabrotButton);
            this.Controls.Add(this.continueButton);
            this.Name = "menuPage";
            this.Text = "menuPage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox amountOfQuestionTextBox;
        private System.Windows.Forms.Button analogyotButton;
        private System.Windows.Forms.Button hestabrotButton;
        private System.Windows.Forms.Button continueButton;
        private System.Windows.Forms.Label topicsLabel;
        private System.Windows.Forms.Button hociotButton;
    }
}