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
            this.continueToQuestionButton = new System.Windows.Forms.Button();
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
            // continueToQuestionButton
            // 
            this.continueToQuestionButton.BackColor = System.Drawing.Color.Transparent;
            this.continueToQuestionButton.Location = new System.Drawing.Point(23, 295);
            this.continueToQuestionButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.continueToQuestionButton.Name = "continueToQuestionButton";
            this.continueToQuestionButton.Size = new System.Drawing.Size(129, 69);
            this.continueToQuestionButton.TabIndex = 0;
            this.continueToQuestionButton.Text = "המשך";
            this.continueToQuestionButton.UseVisualStyleBackColor = false;
            this.continueToQuestionButton.Click += new System.EventHandler(this.nextQuestionButtonClick);
            // 
            // answer1Button
            // 
            this.answer1Button.BackColor = System.Drawing.SystemColors.Menu;
            this.answer1Button.Location = new System.Drawing.Point(62, 385);
            this.answer1Button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.answer1Button.Name = "answer1Button";
            this.answer1Button.Size = new System.Drawing.Size(90, 70);
            this.answer1Button.TabIndex = 1;
            this.answer1Button.Text = "אפשרות 1";
            this.answer1Button.UseVisualStyleBackColor = false;
            this.answer1Button.Click += new System.EventHandler(this.answerButton_Click);
            // 
            // answer2Button
            // 
            this.answer2Button.BackColor = System.Drawing.SystemColors.Menu;
            this.answer2Button.Location = new System.Drawing.Point(62, 459);
            this.answer2Button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.answer2Button.Name = "answer2Button";
            this.answer2Button.Size = new System.Drawing.Size(90, 70);
            this.answer2Button.TabIndex = 2;
            this.answer2Button.Text = "אפשרות 2";
            this.answer2Button.UseVisualStyleBackColor = false;
            this.answer2Button.Click += new System.EventHandler(this.answerButton_Click);
            // 
            // answer3Button
            // 
            this.answer3Button.BackColor = System.Drawing.SystemColors.Menu;
            this.answer3Button.Location = new System.Drawing.Point(62, 533);
            this.answer3Button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.answer3Button.Name = "answer3Button";
            this.answer3Button.Size = new System.Drawing.Size(90, 70);
            this.answer3Button.TabIndex = 3;
            this.answer3Button.Text = "אפשרות 3";
            this.answer3Button.UseVisualStyleBackColor = false;
            this.answer3Button.Click += new System.EventHandler(this.answerButton_Click);
            // 
            // answer4Button
            // 
            this.answer4Button.BackColor = System.Drawing.SystemColors.Menu;
            this.answer4Button.Location = new System.Drawing.Point(62, 607);
            this.answer4Button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.answer4Button.Name = "answer4Button";
            this.answer4Button.Size = new System.Drawing.Size(90, 70);
            this.answer4Button.TabIndex = 4;
            this.answer4Button.Text = "אפשרות 4";
            this.answer4Button.UseVisualStyleBackColor = false;
            this.answer4Button.Click += new System.EventHandler(this.answerButton_Click);
            // 
            // stopTestButton
            // 
            this.stopTestButton.AutoSize = true;
            this.stopTestButton.BackColor = System.Drawing.Color.Tomato;
            this.stopTestButton.Location = new System.Drawing.Point(22, 742);
            this.stopTestButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.stopTestButton.Name = "stopTestButton";
            this.stopTestButton.Size = new System.Drawing.Size(103, 37);
            this.stopTestButton.TabIndex = 4;
            this.stopTestButton.Text = "סיום התרגול";
            this.stopTestButton.UseVisualStyleBackColor = false;
            this.stopTestButton.Click += new System.EventHandler(this.stopTestButtonClick);
            // 
            // isUserRightLabel
            // 
            this.isUserRightLabel.AutoSize = true;
            this.isUserRightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.isUserRightLabel.Location = new System.Drawing.Point(23, 234);
            this.isUserRightLabel.Name = "isUserRightLabel";
            this.isUserRightLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.isUserRightLabel.Size = new System.Drawing.Size(86, 31);
            this.isUserRightLabel.TabIndex = 5;
            this.isUserRightLabel.Text = "label1";
            // 
            // answersTrackLabel
            // 
            this.answersTrackLabel.AutoSize = true;
            this.answersTrackLabel.Location = new System.Drawing.Point(11, 109);
            this.answersTrackLabel.Name = "answersTrackLabel";
            this.answersTrackLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.answersTrackLabel.Size = new System.Drawing.Size(126, 16);
            this.answersTrackLabel.TabIndex = 6;
            this.answersTrackLabel.Text = "answersTrackLabel";
            this.answersTrackLabel.Click += new System.EventHandler(this.answersTrackLabel_Click);
            // 
            // questionTrackLabel
            // 
            this.questionTrackLabel.AutoSize = true;
            this.questionTrackLabel.Location = new System.Drawing.Point(11, 91);
            this.questionTrackLabel.Name = "questionTrackLabel";
            this.questionTrackLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.questionTrackLabel.Size = new System.Drawing.Size(93, 16);
            this.questionTrackLabel.TabIndex = 7;
            this.questionTrackLabel.Text = "questionTrack";
            this.questionTrackLabel.Click += new System.EventHandler(this.questionTrackLabel_Click);
            // 
            // timer
            // 
            this.timer.AutoSize = true;
            this.timer.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.timer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.timer.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.timer.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.timer.Location = new System.Drawing.Point(43, 147);
            this.timer.Name = "timer";
            this.timer.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.timer.Size = new System.Drawing.Size(89, 41);
            this.timer.TabIndex = 8;
            this.timer.Text = "0:00";
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
            this.nextQuestionsButton.Location = new System.Drawing.Point(801, 41);
            this.nextQuestionsButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nextQuestionsButton.Name = "nextQuestionsButton";
            this.nextQuestionsButton.Size = new System.Drawing.Size(32, 32);
            this.nextQuestionsButton.TabIndex = 9;
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
            this.previousQuestionsButton.Location = new System.Drawing.Point(128, 41);
            this.previousQuestionsButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.previousQuestionsButton.Name = "previousQuestionsButton";
            this.previousQuestionsButton.Size = new System.Drawing.Size(32, 32);
            this.previousQuestionsButton.TabIndex = 10;
            this.previousQuestionsButton.Text = "🢀";
            this.previousQuestionsButton.UseVisualStyleBackColor = false;
            this.previousQuestionsButton.Click += new System.EventHandler(this.previousQuestionsButton_Click);
            // 
            // questionsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 795);
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
            this.Controls.Add(this.continueToQuestionButton);
            this.Controls.Add(this.stopTestButton);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "questionsPage";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button continueToQuestionButton;
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

