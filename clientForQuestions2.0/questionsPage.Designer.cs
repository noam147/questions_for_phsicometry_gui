using System.Drawing;

namespace clientForQuestions2._0
{
    partial class questionsPage
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
            this.nextQuestionButton = new System.Windows.Forms.Button();
            this.answer1Button = new System.Windows.Forms.Button();
            this.answer2Button = new System.Windows.Forms.Button();
            this.answer3Button = new System.Windows.Forms.Button();
            this.answer4Button = new System.Windows.Forms.Button();
            this.stopTestButton = new System.Windows.Forms.Button();
            this.isUserRightLabel = new System.Windows.Forms.Label();
            this.answersTrackLabel = new System.Windows.Forms.Label();
            this.questionTrackLabel = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Label();
            this.nextQuestionsButton = new clientForQuestions2._0.RJButtons2();
            this.previousQuestionsButton = new clientForQuestions2._0.RJButtons2();
            this.SuspendLayout();
            // 
            // nextQuestionButton
            // 
            this.nextQuestionButton.BackColor = System.Drawing.Color.Transparent;
            this.nextQuestionButton.Location = new System.Drawing.Point(997, 360);
            this.nextQuestionButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nextQuestionButton.Name = "nextQuestionButton";
            this.nextQuestionButton.Size = new System.Drawing.Size(145, 86);
            this.nextQuestionButton.TabIndex = 0;
            this.nextQuestionButton.Text = "המשך";
            this.nextQuestionButton.UseVisualStyleBackColor = false;
            this.nextQuestionButton.Click += new System.EventHandler(this.nextQuestionButtonClick);
            // 
            // answer1Button
            // 
            this.answer1Button.Location = new System.Drawing.Point(1029, 472);
            this.answer1Button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.answer1Button.Name = "answer1Button";
            this.answer1Button.Size = new System.Drawing.Size(101, 88);
            this.answer1Button.TabIndex = 1;
            this.answer1Button.Text = "אפשרות 1";
            this.answer1Button.UseVisualStyleBackColor = true;
            this.answer1Button.Click += new System.EventHandler(this.answerButton_Click);
            // 
            // answer2Button
            // 
            this.answer2Button.Location = new System.Drawing.Point(1029, 565);
            this.answer2Button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.answer2Button.Name = "answer2Button";
            this.answer2Button.Size = new System.Drawing.Size(101, 88);
            this.answer2Button.TabIndex = 2;
            this.answer2Button.Text = "אפשרות 2";
            this.answer2Button.UseVisualStyleBackColor = true;
            this.answer2Button.Click += new System.EventHandler(this.answerButton_Click);
            // 
            // answer3Button
            // 
            this.answer3Button.Location = new System.Drawing.Point(1029, 658);
            this.answer3Button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.answer3Button.Name = "answer3Button";
            this.answer3Button.Size = new System.Drawing.Size(101, 88);
            this.answer3Button.TabIndex = 3;
            this.answer3Button.Text = "אפשרות 3";
            this.answer3Button.UseVisualStyleBackColor = true;
            this.answer3Button.Click += new System.EventHandler(this.answerButton_Click);
            // 
            // answer4Button
            // 
            this.answer4Button.Location = new System.Drawing.Point(1029, 750);
            this.answer4Button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.answer4Button.Name = "answer4Button";
            this.answer4Button.Size = new System.Drawing.Size(101, 88);
            this.answer4Button.TabIndex = 4;
            this.answer4Button.Text = "אפשרות 4";
            this.answer4Button.UseVisualStyleBackColor = true;
            this.answer4Button.Click += new System.EventHandler(this.answerButton_Click);
            // 
            // stopTestButton
            // 
            this.stopTestButton.AutoSize = true;
            this.stopTestButton.BackColor = System.Drawing.Color.Tomato;
            this.stopTestButton.Location = new System.Drawing.Point(1015, 881);
            this.stopTestButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.stopTestButton.Name = "stopTestButton";
            this.stopTestButton.Size = new System.Drawing.Size(116, 46);
            this.stopTestButton.TabIndex = 4;
            this.stopTestButton.Text = "סיום התרגול";
            this.stopTestButton.UseVisualStyleBackColor = false;
            this.stopTestButton.Click += new System.EventHandler(this.stopTestButtonClick);
            // 
            // isUserRightLabel
            // 
            this.isUserRightLabel.AutoSize = true;
            this.isUserRightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.isUserRightLabel.Location = new System.Drawing.Point(975, 282);
            this.isUserRightLabel.Name = "isUserRightLabel";
            this.isUserRightLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.isUserRightLabel.Size = new System.Drawing.Size(100, 37);
            this.isUserRightLabel.TabIndex = 5;
            this.isUserRightLabel.Text = "label1";
            this.isUserRightLabel.Click += new System.EventHandler(this.isUserRightLabel_Click);
            // 
            // answersTrackLabel
            // 
            this.answersTrackLabel.AutoSize = true;
            this.answersTrackLabel.Location = new System.Drawing.Point(942, 132);
            this.answersTrackLabel.Name = "answersTrackLabel";
            this.answersTrackLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.answersTrackLabel.Size = new System.Drawing.Size(146, 20);
            this.answersTrackLabel.TabIndex = 6;
            this.answersTrackLabel.Text = "answersTrackLabel";
            // 
            // questionTrackLabel
            // 
            this.questionTrackLabel.AutoSize = true;
            this.questionTrackLabel.Location = new System.Drawing.Point(946, 105);
            this.questionTrackLabel.Name = "questionTrackLabel";
            this.questionTrackLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.questionTrackLabel.Size = new System.Drawing.Size(109, 20);
            this.questionTrackLabel.TabIndex = 7;
            this.questionTrackLabel.Text = "questionTrack";
            // 
            // timer
            // 
            this.timer.AutoSize = true;
            this.timer.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.timer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.timer.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.timer.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.timer.Location = new System.Drawing.Point(982, 175);
            this.timer.Name = "timer";
            this.timer.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.timer.Size = new System.Drawing.Size(103, 48);
            this.timer.TabIndex = 8;
            this.timer.Text = "0:00";
            this.timer.Click += new System.EventHandler(this.label1_Click);
            // 
            // nextQuestionsButton
            // 
            this.nextQuestionsButton.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.nextQuestionsButton.BorderRadiuos = 400;
            this.nextQuestionsButton.BorderSize = 1;
            this.nextQuestionsButton.FlatAppearance.BorderSize = 0;
            this.nextQuestionsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nextQuestionsButton.ForeColor = System.Drawing.Color.White;
            this.nextQuestionsButton.Location = new System.Drawing.Point(901, 51);
            this.nextQuestionsButton.Name = "nextQuestionsButton";
            this.nextQuestionsButton.Size = new System.Drawing.Size(150, 40);
            this.nextQuestionsButton.TabIndex = 9;
            this.nextQuestionsButton.Text = "next";
            this.nextQuestionsButton.UseVisualStyleBackColor = false;
            this.nextQuestionsButton.Click += new System.EventHandler(this.nextQuestionsButton_Click);
            // 
            // previousQuestionsButton
            // 
            this.previousQuestionsButton.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.previousQuestionsButton.BorderRadiuos = 400;
            this.previousQuestionsButton.BorderSize = 1;
            this.previousQuestionsButton.FlatAppearance.BorderSize = 0;
            this.previousQuestionsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.previousQuestionsButton.ForeColor = System.Drawing.Color.White;
            this.previousQuestionsButton.Location = new System.Drawing.Point(31, 51);
            this.previousQuestionsButton.Name = "previousQuestionsButton";
            this.previousQuestionsButton.Size = new System.Drawing.Size(150, 40);
            this.previousQuestionsButton.TabIndex = 10;
            this.previousQuestionsButton.Text = "previous";
            this.previousQuestionsButton.UseVisualStyleBackColor = false;
            this.previousQuestionsButton.Click += new System.EventHandler(this.previousQuestionsButton_Click);
            // 
            // questionsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1181, 1050);
            this.Controls.Add(this.previousQuestionsButton);
            this.Controls.Add(this.nextQuestionsButton);
            this.Controls.Add(this.timer);
            this.Controls.Add(this.questionTrackLabel);
            this.Controls.Add(this.answersTrackLabel);
            this.Controls.Add(this.isUserRightLabel);
            this.Controls.Add(this.answer4Button);
            this.Controls.Add(this.answer3Button);
            this.Controls.Add(this.answer2Button);
            this.Controls.Add(this.answer1Button);
            this.Controls.Add(this.nextQuestionButton);
            this.Controls.Add(this.stopTestButton);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "questionsPage";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.questionsPage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button nextQuestionButton;
        private System.Windows.Forms.Button answer1Button;
        private System.Windows.Forms.Button answer2Button;
        private System.Windows.Forms.Button answer3Button;
        private System.Windows.Forms.Button answer4Button;
        private System.Windows.Forms.Button stopTestButton;
        private System.Windows.Forms.Label isUserRightLabel;
        private System.Windows.Forms.Label answersTrackLabel;
        private System.Windows.Forms.Label questionTrackLabel;
        private System.Windows.Forms.Label timer;
        private RJButtons2 nextQuestionsButton;
        private RJButtons2 previousQuestionsButton;
    }
}

