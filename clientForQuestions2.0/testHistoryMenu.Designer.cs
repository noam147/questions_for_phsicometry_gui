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
            this.previousExreciseButton = new clientForQuestions2._0.RJButtons2();
            this.nextExreciseButton = new clientForQuestions2._0.RJButtons2();
            resetHistory_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // resetHistory_button
            // 
            resetHistory_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            resetHistory_button.Location = new System.Drawing.Point(798, 1);
            resetHistory_button.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            resetHistory_button.Name = "resetHistory_button";
            resetHistory_button.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            resetHistory_button.Size = new System.Drawing.Size(99, 55);
            resetHistory_button.TabIndex = 3;
            resetHistory_button.Text = "איפוס היסטוריה";
            resetHistory_button.UseVisualStyleBackColor = false;
            resetHistory_button.Click += new System.EventHandler(this.resetHistory_button_Click);
            // 
            // backToMainMenu_button
            // 
            this.backToMainMenu_button.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.backToMainMenu_button.ForeColor = System.Drawing.Color.Black;
            this.backToMainMenu_button.Location = new System.Drawing.Point(3, 1);
            this.backToMainMenu_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.backToMainMenu_button.Name = "backToMainMenu_button";
            this.backToMainMenu_button.Size = new System.Drawing.Size(127, 75);
            this.backToMainMenu_button.TabIndex = 2;
            this.backToMainMenu_button.Text = "חזרה לתפריט הראשי";
            this.backToMainMenu_button.UseVisualStyleBackColor = false;
            this.backToMainMenu_button.Click += new System.EventHandler(this.backToMainMenu_button_Click);
            // 
            // previousExreciseButton
            // 
            this.previousExreciseButton.BackColor = System.Drawing.Color.Chocolate;
            this.previousExreciseButton.BorderRadiuos = 32;
            this.previousExreciseButton.BorderSize = 1;
            this.previousExreciseButton.FlatAppearance.BorderSize = 0;
            this.previousExreciseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.previousExreciseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.previousExreciseButton.ForeColor = System.Drawing.Color.White;
            this.previousExreciseButton.Location = new System.Drawing.Point(94, 104);
            this.previousExreciseButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.previousExreciseButton.Name = "previousExreciseButton";
            this.previousExreciseButton.Size = new System.Drawing.Size(36, 40);
            this.previousExreciseButton.TabIndex = 12;
            this.previousExreciseButton.Text = "🢀";
            this.previousExreciseButton.UseVisualStyleBackColor = false;
            this.previousExreciseButton.Click += new System.EventHandler(this.previousExreciseButton_Click);
            // 
            // nextExreciseButton
            // 
            this.nextExreciseButton.BackColor = System.Drawing.Color.Chocolate;
            this.nextExreciseButton.BorderRadiuos = 32;
            this.nextExreciseButton.BorderSize = 1;
            this.nextExreciseButton.FlatAppearance.BorderSize = 0;
            this.nextExreciseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nextExreciseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.nextExreciseButton.ForeColor = System.Drawing.Color.White;
            this.nextExreciseButton.Location = new System.Drawing.Point(726, 104);
            this.nextExreciseButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nextExreciseButton.Name = "nextExreciseButton";
            this.nextExreciseButton.Size = new System.Drawing.Size(36, 40);
            this.nextExreciseButton.TabIndex = 13;
            this.nextExreciseButton.Text = "🢂";
            this.nextExreciseButton.UseVisualStyleBackColor = false;
            this.nextExreciseButton.Click += new System.EventHandler(this.nextExreciseButton_Click);
            // 
            // testHistoryMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 562);
            this.Controls.Add(this.nextExreciseButton);
            this.Controls.Add(this.previousExreciseButton);
            this.Controls.Add(resetHistory_button);
            this.Controls.Add(this.backToMainMenu_button);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "testHistoryMenu";
            this.Text = "testHistoryMenu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button backToMainMenu_button;
        private RJButtons2 previousExreciseButton;
        private RJButtons2 nextExreciseButton;
    }
}