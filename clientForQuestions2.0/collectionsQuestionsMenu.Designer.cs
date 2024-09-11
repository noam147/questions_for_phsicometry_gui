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
            this.SuspendLayout();
            // 
            // colButton1
            // 
            this.colButton1.Location = new System.Drawing.Point(363, 185);
            this.colButton1.Name = "colButton1";
            this.colButton1.Size = new System.Drawing.Size(100, 71);
            this.colButton1.TabIndex = 0;
            this.colButton1.Text = "הסקה מתרשים";
            this.colButton1.UseVisualStyleBackColor = true;
            this.colButton1.Click += new System.EventHandler(this.colButton_Click);
            // 
            // colButton2
            // 
            this.colButton2.Location = new System.Drawing.Point(577, 215);
            this.colButton2.Name = "colButton2";
            this.colButton2.Size = new System.Drawing.Size(116, 96);
            this.colButton2.TabIndex = 1;
            this.colButton2.Text = "קטע קריאה";
            this.colButton2.UseVisualStyleBackColor = true;
            this.colButton2.Click += new System.EventHandler(this.colButton_Click);

            // 
            // colButton3
            // 
            this.colButton3.Location = new System.Drawing.Point(363, 262);
            this.colButton3.Name = "colButton3";
            this.colButton3.Size = new System.Drawing.Size(112, 83);
            this.colButton3.TabIndex = 2;
            this.colButton3.Text = "Reading Comprehension";
            this.colButton3.UseVisualStyleBackColor = true;
            this.colButton3.Click += new System.EventHandler(this.colButton_Click);

            // 
            // collectionsQuestionsMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
    }
}