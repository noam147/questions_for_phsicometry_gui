namespace clientForQuestions2._0
{
    partial class AnswerTestForDowloadQuestionsPage
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
            this.button1 = new System.Windows.Forms.Button();
            this.chapters_comboBox = new System.Windows.Forms.ComboBox();
            this.nextQuestionsButton = new clientForQuestions2._0.RJButtons2();
            this.previousQuestionsButton = new clientForQuestions2._0.RJButtons2();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.button1.Location = new System.Drawing.Point(411, 364);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 50);
            this.button1.TabIndex = 0;
            this.button1.Text = "סיום";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chapters_comboBox
            // 
            this.chapters_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.chapters_comboBox.FormattingEnabled = true;
            this.chapters_comboBox.Location = new System.Drawing.Point(13, 3);
            this.chapters_comboBox.Name = "chapters_comboBox";
            this.chapters_comboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chapters_comboBox.Size = new System.Drawing.Size(250, 24);
            this.chapters_comboBox.TabIndex = 15;
            this.chapters_comboBox.Visible = false;
            this.chapters_comboBox.SelectedIndexChanged += new System.EventHandler(this.chapters_comboBox_SelectedIndexChanged);
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
            this.nextQuestionsButton.Location = new System.Drawing.Point(783, 28);
            this.nextQuestionsButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nextQuestionsButton.Name = "nextQuestionsButton";
            this.nextQuestionsButton.Size = new System.Drawing.Size(32, 32);
            this.nextQuestionsButton.TabIndex = 14;
            this.nextQuestionsButton.Text = "🢂";
            this.nextQuestionsButton.UseVisualStyleBackColor = false;
            this.nextQuestionsButton.Click += new System.EventHandler(this.nextQuestionsButton_Click);
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
            this.previousQuestionsButton.Location = new System.Drawing.Point(156, 28);
            this.previousQuestionsButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.previousQuestionsButton.Name = "previousQuestionsButton";
            this.previousQuestionsButton.Size = new System.Drawing.Size(32, 32);
            this.previousQuestionsButton.TabIndex = 13;
            this.previousQuestionsButton.Text = "🢀";
            this.previousQuestionsButton.UseVisualStyleBackColor = false;
            this.previousQuestionsButton.Click += new System.EventHandler(this.previousQuestionsButton_Click);
            // 
            // AnswerTestForDowloadQuestionsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 478);
            this.Controls.Add(this.chapters_comboBox);
            this.Controls.Add(this.nextQuestionsButton);
            this.Controls.Add(this.previousQuestionsButton);
            this.Controls.Add(this.button1);
            this.Name = "AnswerTestForDowloadQuestionsPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AnswerTestForDowloadQuestionsPage";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private RJButtons2 nextQuestionsButton;
        private RJButtons2 previousQuestionsButton;
        private System.Windows.Forms.ComboBox chapters_comboBox;
    }
}