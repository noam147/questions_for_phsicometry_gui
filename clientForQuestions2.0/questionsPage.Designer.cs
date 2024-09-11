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
            this.SuspendLayout();
            // 
            // nextQuestionButton
            // 
            this.nextQuestionButton.Location = new System.Drawing.Point(296, 441);
            this.nextQuestionButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nextQuestionButton.Name = "nextQuestionButton";
            this.nextQuestionButton.Size = new System.Drawing.Size(174, 109);
            this.nextQuestionButton.TabIndex = 0;
            this.nextQuestionButton.Text = "continue";
            this.nextQuestionButton.UseVisualStyleBackColor = true;
            this.nextQuestionButton.Click += new System.EventHandler(this.nextQuestionButtonClick);
            // 
            // answer1Button
            // 
            this.answer1Button.Location = new System.Drawing.Point(140, 474);
            this.answer1Button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.answer1Button.Name = "answer1Button";
            this.answer1Button.Size = new System.Drawing.Size(111, 67);
            this.answer1Button.TabIndex = 1;
            this.answer1Button.Text = "option1";
            this.answer1Button.UseVisualStyleBackColor = true;
            this.answer1Button.Click += new System.EventHandler(this.answer1Button_Click);
            // 
            // answer2Button
            // 
            this.answer2Button.Location = new System.Drawing.Point(296, 474);
            this.answer2Button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.answer2Button.Name = "answer2Button";
            this.answer2Button.Size = new System.Drawing.Size(97, 67);
            this.answer2Button.TabIndex = 2;
            this.answer2Button.Text = "option2";
            this.answer2Button.UseVisualStyleBackColor = true;
            this.answer2Button.Click += new System.EventHandler(this.answer2Button_Click);
            // 
            // answer3Button
            // 
            this.answer3Button.Location = new System.Drawing.Point(419, 474);
            this.answer3Button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.answer3Button.Name = "answer3Button";
            this.answer3Button.Size = new System.Drawing.Size(88, 67);
            this.answer3Button.TabIndex = 3;
            this.answer3Button.Text = "option3";
            this.answer3Button.UseVisualStyleBackColor = true;
            this.answer3Button.Click += new System.EventHandler(this.answer3Button_Click);
            // 
            // answer4Button
            // 
            this.answer4Button.Location = new System.Drawing.Point(523, 474);
            this.answer4Button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.answer4Button.Name = "answer4Button";
            this.answer4Button.Size = new System.Drawing.Size(103, 66);
            this.answer4Button.TabIndex = 4;
            this.answer4Button.Text = "option4";
            this.answer4Button.UseVisualStyleBackColor = true;
            this.answer4Button.Click += new System.EventHandler(this.answer4Button_Click);
            // 
            // stopTestButton
            // 
            this.stopTestButton.AutoSize = true;
            this.stopTestButton.Location = new System.Drawing.Point(764, 477);
            this.stopTestButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.stopTestButton.Name = "stopTestButton";
            this.stopTestButton.Size = new System.Drawing.Size(103, 37);
            this.stopTestButton.TabIndex = 4;
            this.stopTestButton.Text = "stop test";
            this.stopTestButton.UseVisualStyleBackColor = true;
            this.stopTestButton.Click += new System.EventHandler(this.stopTestButtonClick);
            // 
            // isUserRightLabel
            // 
            this.isUserRightLabel.AutoSize = true;
            this.isUserRightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.isUserRightLabel.Location = new System.Drawing.Point(636, 267);
            this.isUserRightLabel.Name = "isUserRightLabel";
            this.isUserRightLabel.Size = new System.Drawing.Size(86, 31);
            this.isUserRightLabel.TabIndex = 5;
            this.isUserRightLabel.Text = "label1";
            this.isUserRightLabel.Click += new System.EventHandler(this.isUserRightLabel_Click);
            // 
            // answersTrackLabel
            // 
            this.answersTrackLabel.AutoSize = true;
            this.answersTrackLabel.Location = new System.Drawing.Point(647, 132);
            this.answersTrackLabel.Name = "answersTrackLabel";
            this.answersTrackLabel.Size = new System.Drawing.Size(126, 16);
            this.answersTrackLabel.TabIndex = 6;
            this.answersTrackLabel.Text = "answersTrackLabel";
            // 
            // questionTrackLabel
            // 
            this.questionTrackLabel.AutoSize = true;
            this.questionTrackLabel.Location = new System.Drawing.Point(651, 98);
            this.questionTrackLabel.Name = "questionTrackLabel";
            this.questionTrackLabel.Size = new System.Drawing.Size(93, 16);
            this.questionTrackLabel.TabIndex = 7;
            this.questionTrackLabel.Text = "questionTrack";
            // 
            // questionsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 755);
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
    }
}

