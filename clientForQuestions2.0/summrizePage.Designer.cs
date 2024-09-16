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
            this.previousQuestionsButton = new clientForQuestions2._0.RJButtons2();
            this.nextQuestionsButton = new clientForQuestions2._0.RJButtons2();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Cyan;
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(6, 28);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 75);
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
            this.timeTookForQLabel.Location = new System.Drawing.Point(53, 139);
            this.timeTookForQLabel.Name = "timeTookForQLabel";
            this.timeTookForQLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.timeTookForQLabel.Size = new System.Drawing.Size(176, 22);
            this.timeTookForQLabel.TabIndex = 2;
            this.timeTookForQLabel.Text = "Time took for question: ";
            this.timeTookForQLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // category_of_q
            // 
            this.category_of_q.AutoSize = true;
            this.category_of_q.BackColor = System.Drawing.Color.PaleTurquoise;
            this.category_of_q.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.category_of_q.Location = new System.Drawing.Point(368, 141);
            this.category_of_q.Name = "category_of_q";
            this.category_of_q.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.category_of_q.Size = new System.Drawing.Size(72, 22);
            this.category_of_q.TabIndex = 3;
            this.category_of_q.Text = "category";
            this.category_of_q.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // total_time
            // 
            this.total_time.AutoSize = true;
            this.total_time.BackColor = System.Drawing.Color.Gold;
            this.total_time.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.total_time.Location = new System.Drawing.Point(53, 108);
            this.total_time.Name = "total_time";
            this.total_time.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.total_time.Size = new System.Drawing.Size(80, 22);
            this.total_time.TabIndex = 4;
            this.total_time.Text = "total time:";
            this.total_time.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // avrage_time
            // 
            this.avrage_time.AutoSize = true;
            this.avrage_time.BackColor = System.Drawing.Color.Gold;
            this.avrage_time.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.avrage_time.Location = new System.Drawing.Point(367, 108);
            this.avrage_time.Name = "avrage_time";
            this.avrage_time.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.avrage_time.Size = new System.Drawing.Size(97, 22);
            this.avrage_time.TabIndex = 5;
            this.avrage_time.Text = "avrage time:";
            this.avrage_time.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // diffic_level
            // 
            this.diffic_level.AutoSize = true;
            this.diffic_level.BackColor = System.Drawing.Color.PaleTurquoise;
            this.diffic_level.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.diffic_level.Location = new System.Drawing.Point(706, 141);
            this.diffic_level.Name = "diffic_level";
            this.diffic_level.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.diffic_level.Size = new System.Drawing.Size(84, 22);
            this.diffic_level.TabIndex = 6;
            this.diffic_level.Text = "diffic_level";
            this.diffic_level.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // curr_q
            // 
            this.curr_q.AutoSize = true;
            this.curr_q.BackColor = System.Drawing.SystemColors.HighlightText;
            this.curr_q.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.curr_q.Location = new System.Drawing.Point(133, 2);
            this.curr_q.Name = "curr_q";
            this.curr_q.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.curr_q.Size = new System.Drawing.Size(130, 22);
            this.curr_q.TabIndex = 7;
            this.curr_q.Text = "current question:";
            this.curr_q.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // curr_q_id
            // 
            this.curr_q_id.AutoSize = true;
            this.curr_q_id.BackColor = System.Drawing.SystemColors.HighlightText;
            this.curr_q_id.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.curr_q_id.Location = new System.Drawing.Point(15, 2);
            this.curr_q_id.Name = "curr_q_id";
            this.curr_q_id.Size = new System.Drawing.Size(31, 22);
            this.curr_q_id.TabIndex = 8;
            this.curr_q_id.Text = "id: ";
            // 
            // correct_answers
            // 
            this.correct_answers.AutoSize = true;
            this.correct_answers.BackColor = System.Drawing.Color.Gold;
            this.correct_answers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.correct_answers.Location = new System.Drawing.Point(654, 108);
            this.correct_answers.Name = "correct_answers";
            this.correct_answers.Size = new System.Drawing.Size(113, 22);
            this.correct_answers.TabIndex = 9;
            this.correct_answers.Text = "תשובות נכונות: ";
            this.correct_answers.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.previousQuestionsButton.Location = new System.Drawing.Point(273, 45);
            this.previousQuestionsButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.previousQuestionsButton.Name = "previousQuestionsButton";
            this.previousQuestionsButton.Size = new System.Drawing.Size(36, 40);
            this.previousQuestionsButton.TabIndex = 11;
            this.previousQuestionsButton.Text = "🢀";
            this.previousQuestionsButton.UseVisualStyleBackColor = false;
            this.previousQuestionsButton.Click += new System.EventHandler(this.previousQuestionsButton_Click);
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
            this.nextQuestionsButton.Location = new System.Drawing.Point(979, 45);
            this.nextQuestionsButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nextQuestionsButton.Name = "nextQuestionsButton";
            this.nextQuestionsButton.Size = new System.Drawing.Size(36, 40);
            this.nextQuestionsButton.TabIndex = 12;
            this.nextQuestionsButton.Text = "🢂";
            this.nextQuestionsButton.UseVisualStyleBackColor = false;
            this.nextQuestionsButton.Click += new System.EventHandler(this.nextQuestionsButton_Click);
            // 
            // summrizePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 924);
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
            this.Text = "summrizePage";
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
    }
}