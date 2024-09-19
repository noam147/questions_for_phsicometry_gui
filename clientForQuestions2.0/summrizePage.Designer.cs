namespace clientForQuestions2._0
{
    partial class summrizePage
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
            this.timeTookForQLabel = new System.Windows.Forms.Label();
            this.category_of_q = new System.Windows.Forms.Label();
            this.total_time = new System.Windows.Forms.Label();
            this.avrage_time = new System.Windows.Forms.Label();
            this.diffic_level = new System.Windows.Forms.Label();
            this.curr_q = new System.Windows.Forms.Label();
            this.curr_q_id = new System.Windows.Forms.Label();
            this.correct_answers = new System.Windows.Forms.Label();
            this.diffic_level_col = new System.Windows.Forms.Label();
            this.curr_col_id = new System.Windows.Forms.Label();
            this.nextQuestionsButton = new clientForQuestions2._0.RJButtons2();
            this.previousQuestionsButton = new clientForQuestions2._0.RJButtons2();
            this.avrage_difficultyLevel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(5, 24);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 60);
            this.button1.TabIndex = 1;
            this.button1.Text = "חזרה לתפריט הראשי";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timeTookForQLabel
            // 
            this.timeTookForQLabel.AutoSize = true;
            this.timeTookForQLabel.BackColor = System.Drawing.Color.PaleTurquoise;
            this.timeTookForQLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.timeTookForQLabel.Location = new System.Drawing.Point(47, 109);
            this.timeTookForQLabel.Name = "timeTookForQLabel";
            this.timeTookForQLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.timeTookForQLabel.Size = new System.Drawing.Size(147, 18);
            this.timeTookForQLabel.TabIndex = 2;
            this.timeTookForQLabel.Text = "Time took for question: ";
            this.timeTookForQLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // category_of_q
            // 
            this.category_of_q.AutoSize = true;
            this.category_of_q.BackColor = System.Drawing.Color.PaleTurquoise;
            this.category_of_q.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.category_of_q.Location = new System.Drawing.Point(327, 111);
            this.category_of_q.Name = "category_of_q";
            this.category_of_q.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.category_of_q.Size = new System.Drawing.Size(62, 18);
            this.category_of_q.TabIndex = 3;
            this.category_of_q.Text = "category";
            this.category_of_q.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // total_time
            // 
            this.total_time.AutoSize = true;
            this.total_time.BackColor = System.Drawing.Color.Gold;
            this.total_time.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.total_time.Location = new System.Drawing.Point(47, 86);
            this.total_time.Name = "total_time";
            this.total_time.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.total_time.Size = new System.Drawing.Size(65, 18);
            this.total_time.TabIndex = 4;
            this.total_time.Text = "total time:";
            this.total_time.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // avrage_time
            // 
            this.avrage_time.AutoSize = true;
            this.avrage_time.BackColor = System.Drawing.Color.Gold;
            this.avrage_time.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.avrage_time.Location = new System.Drawing.Point(326, 86);
            this.avrage_time.Name = "avrage_time";
            this.avrage_time.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.avrage_time.Size = new System.Drawing.Size(83, 18);
            this.avrage_time.TabIndex = 5;
            this.avrage_time.Text = "avrage time:";
            this.avrage_time.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // diffic_level
            // 
            this.diffic_level.AutoSize = true;
            this.diffic_level.BackColor = System.Drawing.Color.PaleTurquoise;
            this.diffic_level.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.diffic_level.Location = new System.Drawing.Point(628, 111);
            this.diffic_level.Name = "diffic_level";
            this.diffic_level.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.diffic_level.Size = new System.Drawing.Size(72, 18);
            this.diffic_level.TabIndex = 6;
            this.diffic_level.Text = "diffic_level";
            this.diffic_level.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // curr_q
            // 
            this.curr_q.AutoSize = true;
            this.curr_q.BackColor = System.Drawing.SystemColors.HighlightText;
            this.curr_q.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.curr_q.Location = new System.Drawing.Point(118, 5);
            this.curr_q.Name = "curr_q";
            this.curr_q.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.curr_q.Size = new System.Drawing.Size(106, 18);
            this.curr_q.TabIndex = 7;
            this.curr_q.Text = "current question:";
            this.curr_q.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // curr_q_id
            // 
            this.curr_q_id.AutoSize = true;
            this.curr_q_id.BackColor = System.Drawing.SystemColors.HighlightText;
            this.curr_q_id.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.curr_q_id.Location = new System.Drawing.Point(13, 5);
            this.curr_q_id.Name = "curr_q_id";
            this.curr_q_id.Size = new System.Drawing.Size(26, 18);
            this.curr_q_id.TabIndex = 8;
            this.curr_q_id.Text = "Id: ";
            this.curr_q_id.Click += new System.EventHandler(this.curr_q_id_Click);
            // 
            // correct_answers
            // 
            this.correct_answers.AutoSize = true;
            this.correct_answers.BackColor = System.Drawing.Color.Gold;
            this.correct_answers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.correct_answers.Location = new System.Drawing.Point(581, 86);
            this.correct_answers.Name = "correct_answers";
            this.correct_answers.Size = new System.Drawing.Size(99, 18);
            this.correct_answers.TabIndex = 9;
            this.correct_answers.Text = "תשובות נכונות: ";
            this.correct_answers.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // diffic_level_col
            // 
            this.diffic_level_col.AutoSize = true;
            this.diffic_level_col.BackColor = System.Drawing.Color.PaleTurquoise;
            this.diffic_level_col.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.diffic_level_col.Location = new System.Drawing.Point(956, 111);
            this.diffic_level_col.Name = "diffic_level_col";
            this.diffic_level_col.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.diffic_level_col.Size = new System.Drawing.Size(97, 18);
            this.diffic_level_col.TabIndex = 13;
            this.diffic_level_col.Text = "diffic_level_col";
            this.diffic_level_col.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // curr_col_id
            // 
            this.curr_col_id.AllowDrop = true;
            this.curr_col_id.AutoSize = true;
            this.curr_col_id.BackColor = System.Drawing.SystemColors.HighlightText;
            this.curr_col_id.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.curr_col_id.Location = new System.Drawing.Point(956, 5);
            this.curr_col_id.Name = "curr_col_id";
            this.curr_col_id.Size = new System.Drawing.Size(88, 18);
            this.curr_col_id.TabIndex = 14;
            this.curr_col_id.Text = "Collection Id: ";
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
            this.nextQuestionsButton.Location = new System.Drawing.Point(870, 36);
            this.nextQuestionsButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nextQuestionsButton.Name = "nextQuestionsButton";
            this.nextQuestionsButton.Size = new System.Drawing.Size(32, 32);
            this.nextQuestionsButton.TabIndex = 12;
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
            this.previousQuestionsButton.Location = new System.Drawing.Point(243, 36);
            this.previousQuestionsButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.previousQuestionsButton.Name = "previousQuestionsButton";
            this.previousQuestionsButton.Size = new System.Drawing.Size(32, 32);
            this.previousQuestionsButton.TabIndex = 11;
            this.previousQuestionsButton.Text = "🢀";
            this.previousQuestionsButton.UseVisualStyleBackColor = false;
            this.previousQuestionsButton.Click += new System.EventHandler(this.previousQuestionsButton_Click);
            // 
            // avrage_difficultyLevel
            // 
            this.avrage_difficultyLevel.AutoSize = true;
            this.avrage_difficultyLevel.BackColor = System.Drawing.Color.Gold;
            this.avrage_difficultyLevel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.avrage_difficultyLevel.Location = new System.Drawing.Point(798, 86);
            this.avrage_difficultyLevel.Name = "avrage_difficultyLevel";
            this.avrage_difficultyLevel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.avrage_difficultyLevel.Size = new System.Drawing.Size(113, 18);
            this.avrage_difficultyLevel.TabIndex = 15;
            this.avrage_difficultyLevel.Text = "רמת קושי ממוצעת:";
            this.avrage_difficultyLevel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // summrizePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1047, 739);
            this.Controls.Add(this.avrage_difficultyLevel);
            this.Controls.Add(this.curr_col_id);
            this.Controls.Add(this.diffic_level_col);
            this.Controls.Add(this.nextQuestionsButton);
            this.Controls.Add(this.previousQuestionsButton);
            this.Controls.Add(this.correct_answers);
            this.Controls.Add(this.curr_q_id);
            this.Controls.Add(this.curr_q);
            this.Controls.Add(this.diffic_level);
            this.Controls.Add(this.avrage_time);
            this.Controls.Add(this.total_time);
            this.Controls.Add(this.category_of_q);
            this.Controls.Add(this.timeTookForQLabel);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "summrizePage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "summrizePage";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.summrizePage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label timeTookForQLabel;
        private System.Windows.Forms.Label category_of_q;
        private System.Windows.Forms.Label total_time;
        private System.Windows.Forms.Label avrage_time;
        private System.Windows.Forms.Label diffic_level;
        private System.Windows.Forms.Label curr_q;
        private System.Windows.Forms.Label curr_q_id;
        private System.Windows.Forms.Label correct_answers;
        private RJButtons2 previousQuestionsButton;
        private RJButtons2 nextQuestionsButton;
        private System.Windows.Forms.Label diffic_level_col;
        private System.Windows.Forms.Label curr_col_id;
        private System.Windows.Forms.Label avrage_difficultyLevel;
    }
}