namespace clientForQuestions2._0
{
    partial class collectionsQuestionsMenu
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
            this.colButton1 = new System.Windows.Forms.Button();
            this.colButton2 = new System.Windows.Forms.Button();
            this.colButton3 = new System.Windows.Forms.Button();
            this.backToMainMenu = new System.Windows.Forms.Button();
            this.continueButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // colButton1
            // 
            this.colButton1.Location = new System.Drawing.Point(350, 107);
            this.colButton1.Name = "colButton1";
            this.colButton1.Size = new System.Drawing.Size(100, 71);
            this.colButton1.TabIndex = 0;
            this.colButton1.Text = "הסקה מתרשים";
            this.colButton1.UseVisualStyleBackColor = true;
            this.colButton1.Click += new System.EventHandler(this.colButton_Click);
            // 
            // colButton2
            // 
            this.colButton2.Location = new System.Drawing.Point(350, 184);
            this.colButton2.Name = "colButton2";
            this.colButton2.Size = new System.Drawing.Size(116, 96);
            this.colButton2.TabIndex = 1;
            this.colButton2.Text = "קטע קריאה";
            this.colButton2.UseVisualStyleBackColor = true;
            this.colButton2.Click += new System.EventHandler(this.colButton_Click);
            // 
            // colButton3
            // 
            this.colButton3.Location = new System.Drawing.Point(350, 286);
            this.colButton3.Name = "colButton3";
            this.colButton3.Size = new System.Drawing.Size(112, 83);
            this.colButton3.TabIndex = 2;
            this.colButton3.Text = "Reading Comprehension";
            this.colButton3.UseVisualStyleBackColor = true;
            this.colButton3.Click += new System.EventHandler(this.colButton_Click);
            // 
            // backToMainMenu
            // 
            this.backToMainMenu.Location = new System.Drawing.Point(13, 13);
            this.backToMainMenu.Name = "backToMainMenu";
            this.backToMainMenu.Size = new System.Drawing.Size(94, 50);
            this.backToMainMenu.TabIndex = 3;
            this.backToMainMenu.Text = "חזרה לתפריט הראשי";
            this.backToMainMenu.UseVisualStyleBackColor = true;
            this.backToMainMenu.Click += new System.EventHandler(this.backToMainMenu_Click);
            // 
            // continueButton
            // 
            this.continueButton.Enabled = false;
            this.continueButton.Location = new System.Drawing.Point(124, 326);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(99, 61);
            this.continueButton.TabIndex = 4;
            this.continueButton.Text = "התחלת תרגול";
            this.continueButton.UseVisualStyleBackColor = true;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // collectionsQuestionsMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.backToMainMenu);
            this.Controls.Add(this.colButton3);
            this.Controls.Add(this.colButton2);
            this.Controls.Add(this.colButton1);
            this.Name = "collectionsQuestionsMenu";
            this.Text = "collectionsQuestionsMenu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button colButton1;
        private System.Windows.Forms.Button colButton2;
        private System.Windows.Forms.Button colButton3;
        private System.Windows.Forms.Button backToMainMenu;
        private System.Windows.Forms.Button continueButton;
    }
}