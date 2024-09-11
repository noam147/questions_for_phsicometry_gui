using System;
using System.Drawing;

namespace clientForQuestions2._0
{
    partial class menuPage
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
            this.normalQuestions = new System.Windows.Forms.Button();
            this.questionByIDtextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.questionByIdButton = new clientForQuestions2._0.RJButtons2();
            this.SuspendLayout();
            // 
            // normalQuestions
            // 
            this.normalQuestions.Location = new System.Drawing.Point(555, 293);
            this.normalQuestions.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.normalQuestions.Name = "normalQuestions";
            this.normalQuestions.Size = new System.Drawing.Size(141, 112);
            this.normalQuestions.TabIndex = 8;
            this.normalQuestions.Text = "תרגול";
            this.normalQuestions.UseVisualStyleBackColor = true;
            this.normalQuestions.Click += new System.EventHandler(this.normalQuestions_Clicked);
            // 
            // questionByIDtextBox
            // 
            this.questionByIDtextBox.Location = new System.Drawing.Point(94, 321);
            this.questionByIDtextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.questionByIDtextBox.Name = "questionByIDtextBox";
            this.questionByIDtextBox.Size = new System.Drawing.Size(100, 26);
            this.questionByIDtextBox.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(91, 278);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 20);
            this.label1.TabIndex = 15;
            this.label1.Text = "שאלה לפי מספר מזהה";
            // 
            // questionByIdButton
            // 
            this.questionByIdButton.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.questionByIdButton.BorderRadiuos = 40;
            this.questionByIdButton.BorderSize = 1;
            this.questionByIdButton.FlatAppearance.BorderSize = 0;
            this.questionByIdButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.questionByIdButton.ForeColor = System.Drawing.Color.White;
            this.questionByIdButton.Location = new System.Drawing.Point(49, 376);
            this.questionByIdButton.Name = "questionByIdButton";
            this.questionByIdButton.Size = new System.Drawing.Size(176, 60);
            this.questionByIdButton.TabIndex = 18;
            this.questionByIdButton.Text = "הצג שאלה";
            this.questionByIdButton.UseVisualStyleBackColor = false;
            this.questionByIdButton.Click += new System.EventHandler(this.questionbyIdButton_Click);
            // 
            // menuPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1243, 1050);
            this.Controls.Add(this.questionByIdButton);
            this.Controls.Add(this.normalQuestions);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.questionByIDtextBox);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "menuPage";
            this.Text = "menuPage";
            this.Load += new System.EventHandler(this.menuPage_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button normalQuestions;

        private System.Windows.Forms.TextBox questionByIDtextBox;
        private System.Windows.Forms.Label label1;
        private RJButtons2 questionByIdButton;
    }
}