namespace clientForQuestions2._0
{
    partial class BaseQuestionsPage
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
            this.components = new System.ComponentModel.Container();
            this.isUserRightLabel = new System.Windows.Forms.Label();
            this.answer4Button = new System.Windows.Forms.Button();
            this.answer3Button = new System.Windows.Forms.Button();
            this.answer2Button = new System.Windows.Forms.Button();
            this.answer1Button = new System.Windows.Forms.Button();
            this.continueToQuestionButton = new System.Windows.Forms.Button();
            this.stopTestButton = new System.Windows.Forms.Button();
            this.timerLabel = new System.Windows.Forms.Label();
            this.questionTrackLabel = new System.Windows.Forms.Label();
            this.answersTrackLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // isUserRightLabel
            // 
            this.isUserRightLabel.AutoSize = true;
            this.isUserRightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.isUserRightLabel.Location = new System.Drawing.Point(36, 285);
            this.isUserRightLabel.Name = "isUserRightLabel";
            this.isUserRightLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.isUserRightLabel.Size = new System.Drawing.Size(100, 37);
            this.isUserRightLabel.TabIndex = 12;
            this.isUserRightLabel.Text = "label1";
            // 
            // answer4Button
            // 
            this.answer4Button.BackColor = System.Drawing.SystemColors.Menu;
            this.answer4Button.Location = new System.Drawing.Point(80, 752);
            this.answer4Button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.answer4Button.Name = "answer4Button";
            this.answer4Button.Size = new System.Drawing.Size(101, 88);
            this.answer4Button.TabIndex = 10;
            this.answer4Button.Text = "אפשרות 4";
            this.answer4Button.UseVisualStyleBackColor = false;
            this.answer4Button.Click += new System.EventHandler(this.answerButton_Click);
            // 
            // answer3Button
            // 
            this.answer3Button.BackColor = System.Drawing.SystemColors.Menu;
            this.answer3Button.Location = new System.Drawing.Point(80, 659);
            this.answer3Button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.answer3Button.Name = "answer3Button";
            this.answer3Button.Size = new System.Drawing.Size(101, 88);
            this.answer3Button.TabIndex = 9;
            this.answer3Button.Text = "אפשרות 3";
            this.answer3Button.UseVisualStyleBackColor = false;
            this.answer3Button.Click += new System.EventHandler(this.answerButton_Click);
            // 
            // answer2Button
            // 
            this.answer2Button.BackColor = System.Drawing.SystemColors.Menu;
            this.answer2Button.Location = new System.Drawing.Point(80, 567);
            this.answer2Button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.answer2Button.Name = "answer2Button";
            this.answer2Button.Size = new System.Drawing.Size(101, 88);
            this.answer2Button.TabIndex = 8;
            this.answer2Button.Text = "אפשרות 2";
            this.answer2Button.UseVisualStyleBackColor = false;
            this.answer2Button.Click += new System.EventHandler(this.answerButton_Click);
            // 
            // answer1Button
            // 
            this.answer1Button.BackColor = System.Drawing.SystemColors.Menu;
            this.answer1Button.Location = new System.Drawing.Point(80, 474);
            this.answer1Button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.answer1Button.Name = "answer1Button";
            this.answer1Button.Size = new System.Drawing.Size(101, 88);
            this.answer1Button.TabIndex = 7;
            this.answer1Button.Text = "אפשרות 1";
            this.answer1Button.UseVisualStyleBackColor = false;
            this.answer1Button.Click += new System.EventHandler(this.answerButton_Click);
            // 
            // continueToQuestionButton
            // 
            this.continueToQuestionButton.BackColor = System.Drawing.Color.Transparent;
            this.continueToQuestionButton.Location = new System.Drawing.Point(36, 362);
            this.continueToQuestionButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.continueToQuestionButton.Name = "continueToQuestionButton";
            this.continueToQuestionButton.Size = new System.Drawing.Size(145, 86);
            this.continueToQuestionButton.TabIndex = 6;
            this.continueToQuestionButton.Text = "המשך";
            this.continueToQuestionButton.UseVisualStyleBackColor = false;
            this.continueToQuestionButton.Click += new System.EventHandler(this.nextQuestionButtonClick);
            // 
            // stopTestButton
            // 
            this.stopTestButton.AutoSize = true;
            this.stopTestButton.BackColor = System.Drawing.Color.Tomato;
            this.stopTestButton.Location = new System.Drawing.Point(35, 921);
            this.stopTestButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.stopTestButton.Name = "stopTestButton";
            this.stopTestButton.Size = new System.Drawing.Size(116, 46);
            this.stopTestButton.TabIndex = 11;
            this.stopTestButton.Text = "סיום התרגול";
            this.stopTestButton.UseVisualStyleBackColor = false;
            this.stopTestButton.Click += new System.EventHandler(this.stopTestButtonClick);
            // 
            // timerLabel
            // 
            this.timerLabel.AutoSize = true;
            this.timerLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.timerLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.timerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.timerLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.timerLabel.Location = new System.Drawing.Point(50, 188);
            this.timerLabel.Name = "timerLabel";
            this.timerLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.timerLabel.Size = new System.Drawing.Size(103, 48);
            this.timerLabel.TabIndex = 21;
            this.timerLabel.Text = "0:00";
            // 
            // questionTrackLabel
            // 
            this.questionTrackLabel.AutoSize = true;
            this.questionTrackLabel.Location = new System.Drawing.Point(48, 127);
            this.questionTrackLabel.Name = "questionTrackLabel";
            this.questionTrackLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.questionTrackLabel.Size = new System.Drawing.Size(109, 20);
            this.questionTrackLabel.TabIndex = 23;
            this.questionTrackLabel.Text = "questionTrack";
            // 
            // answersTrackLabel
            // 
            this.answersTrackLabel.AutoSize = true;
            this.answersTrackLabel.Location = new System.Drawing.Point(48, 149);
            this.answersTrackLabel.Name = "answersTrackLabel";
            this.answersTrackLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.answersTrackLabel.Size = new System.Drawing.Size(146, 20);
            this.answersTrackLabel.TabIndex = 22;
            this.answersTrackLabel.Text = "answersTrackLabel";
            // 
            // BaseQuestionsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1181, 994);
            this.Controls.Add(this.questionTrackLabel);
            this.Controls.Add(this.answersTrackLabel);
            this.Controls.Add(this.timerLabel);
            this.Controls.Add(this.isUserRightLabel);
            this.Controls.Add(this.answer4Button);
            this.Controls.Add(this.answer3Button);
            this.Controls.Add(this.answer2Button);
            this.Controls.Add(this.answer1Button);
            this.Controls.Add(this.continueToQuestionButton);
            this.Controls.Add(this.stopTestButton);
            this.Name = "BaseQuestionsPage";
            this.Text = "BaseQuestionsPage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label isUserRightLabel;
        protected System.Windows.Forms.Button answer4Button;
        protected System.Windows.Forms.Button answer3Button;
        protected System.Windows.Forms.Button answer2Button;
        protected System.Windows.Forms.Button answer1Button;
        protected System.Windows.Forms.Button continueToQuestionButton;
        private System.Windows.Forms.Button stopTestButton;
        protected System.Windows.Forms.Label timerLabel;
        protected System.Windows.Forms.Label questionTrackLabel;
        protected System.Windows.Forms.Label answersTrackLabel;
        private System.Windows.Forms.Timer timer1;
    }
}