namespace clientForQuestions2._0
{
    partial class testHistoryMenu
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
            System.Windows.Forms.Button resetHistory_button;
            this.backToMainMenu_button = new System.Windows.Forms.Button();
            resetHistory_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // backToMainMenu_button
            // 
            this.backToMainMenu_button.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.backToMainMenu_button.ForeColor = System.Drawing.Color.Black;
            this.backToMainMenu_button.Location = new System.Drawing.Point(3, 1);
            this.backToMainMenu_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.backToMainMenu_button.Name = "backToMainMenu_button";
            this.backToMainMenu_button.Size = new System.Drawing.Size(113, 60);
            this.backToMainMenu_button.TabIndex = 2;
            this.backToMainMenu_button.Text = "חזרה לתפריט הראשי";
            this.backToMainMenu_button.UseVisualStyleBackColor = false;
            this.backToMainMenu_button.Click += new System.EventHandler(this.backToMainMenu_button_Click);
            // 
            // resetHistory_button
            // 
            resetHistory_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            resetHistory_button.Location = new System.Drawing.Point(709, 1);
            resetHistory_button.Name = "resetHistory_button";
            resetHistory_button.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            resetHistory_button.Size = new System.Drawing.Size(88, 44);
            resetHistory_button.TabIndex = 3;
            resetHistory_button.Text = "איפוס היסטוריה";
            resetHistory_button.UseVisualStyleBackColor = false;
            resetHistory_button.Click += new System.EventHandler(this.resetHistory_button_Click);
            // 
            // testHistoryMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(resetHistory_button);
            this.Controls.Add(this.backToMainMenu_button);
            this.Name = "testHistoryMenu";
            this.Text = "testHistoryMenu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button backToMainMenu_button;
    }
}