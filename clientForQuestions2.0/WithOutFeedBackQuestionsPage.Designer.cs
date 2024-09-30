namespace clientForQuestions2._0
{
    partial class WithOutFeedBackQuestionsPage
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
            this.previousQuestionsButton = new clientForQuestions2._0.RJButtons2();
            this.nextQuestionsButton = new clientForQuestions2._0.RJButtons2();
            this.SuspendLayout();
            // 
            // previousQuestionsButton
            // 
            this.previousQuestionsButton.BackColor = System.Drawing.Color.Chocolate;
            this.previousQuestionsButton.BorderRadiuos = 32;
            this.previousQuestionsButton.BorderSize = 1;
            this.previousQuestionsButton.FlatAppearance.BorderSize = 0;
            this.previousQuestionsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.previousQuestionsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.previousQuestionsButton.ForeColor = System.Drawing.Color.White;
            this.previousQuestionsButton.Location = new System.Drawing.Point(144, 51);
            this.previousQuestionsButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.previousQuestionsButton.Name = "previousQuestionsButton";
            this.previousQuestionsButton.Size = new System.Drawing.Size(36, 40);
            this.previousQuestionsButton.TabIndex = 21;
            this.previousQuestionsButton.Text = "🢀";
            this.previousQuestionsButton.UseVisualStyleBackColor = false;
            // 
            // nextQuestionsButton
            // 
            this.nextQuestionsButton.BackColor = System.Drawing.Color.Chocolate;
            this.nextQuestionsButton.BorderRadiuos = 32;
            this.nextQuestionsButton.BorderSize = 1;
            this.nextQuestionsButton.FlatAppearance.BorderSize = 0;
            this.nextQuestionsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nextQuestionsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.nextQuestionsButton.ForeColor = System.Drawing.Color.White;
            this.nextQuestionsButton.Location = new System.Drawing.Point(901, 51);
            this.nextQuestionsButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nextQuestionsButton.Name = "nextQuestionsButton";
            this.nextQuestionsButton.Size = new System.Drawing.Size(36, 40);
            this.nextQuestionsButton.TabIndex = 22;
            this.nextQuestionsButton.Text = "🢂";
            this.nextQuestionsButton.UseVisualStyleBackColor = false;
            // 
            // WithOutFeedBackQuestionsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 450);
            this.Controls.Add(this.nextQuestionsButton);
            this.Controls.Add(this.previousQuestionsButton);
            this.Name = "WithOutFeedBackQuestionsPage";
            this.Text = "WithOutFeedBackQuestionsPage";
            this.Controls.SetChildIndex(this.previousQuestionsButton, 0);
            this.Controls.SetChildIndex(this.nextQuestionsButton, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RJButtons2 previousQuestionsButton;
        private RJButtons2 nextQuestionsButton;
    }
}