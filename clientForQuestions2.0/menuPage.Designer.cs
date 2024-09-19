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
            this.collectionsQuestions = new System.Windows.Forms.Button();
            this.questionByIDtextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chaptersQuestions = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.collectionbyIdtextBox = new System.Windows.Forms.TextBox();
            this.error_qById_label = new System.Windows.Forms.Label();
            this.error_colById_label = new System.Windows.Forms.Label();
            this.collectionbyIdButton = new clientForQuestions2._0.RJButtons2();
            this.questionByIdButton = new clientForQuestions2._0.RJButtons2();
            this.SuspendLayout();
            // 
            // normalQuestions
            // 
            this.normalQuestions.Location = new System.Drawing.Point(493, 140);
            this.normalQuestions.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.normalQuestions.Name = "normalQuestions";
            this.normalQuestions.Size = new System.Drawing.Size(125, 90);
            this.normalQuestions.TabIndex = 8;
            this.normalQuestions.Text = "תרגול";
            this.normalQuestions.UseVisualStyleBackColor = true;
            this.normalQuestions.Click += new System.EventHandler(this.normalQuestions_Clicked);
            // 
            // collectionsQuestions
            // 
            this.collectionsQuestions.Location = new System.Drawing.Point(493, 234);
            this.collectionsQuestions.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.collectionsQuestions.Name = "collectionsQuestions";
            this.collectionsQuestions.Size = new System.Drawing.Size(125, 90);
            this.collectionsQuestions.TabIndex = 8;
            this.collectionsQuestions.Text = "תרגול קטעי קריאה ותרשימים";
            this.collectionsQuestions.UseVisualStyleBackColor = true;
            this.collectionsQuestions.Click += new System.EventHandler(this.collectionsQuestions_Clicked);
            // 
            // questionByIDtextBox
            // 
            this.questionByIDtextBox.Location = new System.Drawing.Point(84, 115);
            this.questionByIDtextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.questionByIDtextBox.Name = "questionByIDtextBox";
            this.questionByIDtextBox.Size = new System.Drawing.Size(89, 22);
            this.questionByIDtextBox.TabIndex = 14;
            this.questionByIDtextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.questionByIDtextBox_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(81, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 16);
            this.label1.TabIndex = 15;
            this.label1.Text = "שאלה לפי מספר מזהה";
            // 
            // chaptersQuestions
            // 
            this.chaptersQuestions.Location = new System.Drawing.Point(493, 328);
            this.chaptersQuestions.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chaptersQuestions.Name = "chaptersQuestions";
            this.chaptersQuestions.Size = new System.Drawing.Size(125, 90);
            this.chaptersQuestions.TabIndex = 19;
            this.chaptersQuestions.Text = "תרגול פרקים מלאים";
            this.chaptersQuestions.UseVisualStyleBackColor = true;
            this.chaptersQuestions.Click += new System.EventHandler(this.chaptersQuestions_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 365);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(213, 16);
            this.label2.TabIndex = 20;
            this.label2.Text = "תרשים או קטע קריאה לפי מספר מזהה";
            // 
            // collectionbyIdtextBox
            // 
            this.collectionbyIdtextBox.Location = new System.Drawing.Point(84, 406);
            this.collectionbyIdtextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.collectionbyIdtextBox.Name = "collectionbyIdtextBox";
            this.collectionbyIdtextBox.Size = new System.Drawing.Size(89, 22);
            this.collectionbyIdtextBox.TabIndex = 21;
            this.collectionbyIdtextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.collectionbyIdtextBox_KeyDown);
            // 
            // error_qById_label
            // 
            this.error_qById_label.AutoSize = true;
            this.error_qById_label.ForeColor = System.Drawing.Color.Red;
            this.error_qById_label.Location = new System.Drawing.Point(55, 218);
            this.error_qById_label.Name = "error_qById_label";
            this.error_qById_label.Size = new System.Drawing.Size(0, 16);
            this.error_qById_label.TabIndex = 23;
            // 
            // error_colById_label
            // 
            this.error_colById_label.AutoSize = true;
            this.error_colById_label.ForeColor = System.Drawing.Color.Red;
            this.error_colById_label.Location = new System.Drawing.Point(55, 505);
            this.error_colById_label.Name = "error_colById_label";
            this.error_colById_label.Size = new System.Drawing.Size(0, 16);
            this.error_colById_label.TabIndex = 24;
            // 
            // collectionbyIdButton
            // 
            this.collectionbyIdButton.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.collectionbyIdButton.BorderRadiuos = 40;
            this.collectionbyIdButton.BorderSize = 1;
            this.collectionbyIdButton.FlatAppearance.BorderSize = 0;
            this.collectionbyIdButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.collectionbyIdButton.ForeColor = System.Drawing.Color.White;
            this.collectionbyIdButton.Location = new System.Drawing.Point(44, 444);
            this.collectionbyIdButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.collectionbyIdButton.Name = "collectionbyIdButton";
            this.collectionbyIdButton.Size = new System.Drawing.Size(156, 48);
            this.collectionbyIdButton.TabIndex = 22;
            this.collectionbyIdButton.Text = "הצג תרשים או קטע קריאה";
            this.collectionbyIdButton.UseVisualStyleBackColor = false;
            this.collectionbyIdButton.Click += new System.EventHandler(this.collectionbyIdButton_Click);
            // 
            // questionByIdButton
            // 
            this.questionByIdButton.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.questionByIdButton.BorderRadiuos = 40;
            this.questionByIdButton.BorderSize = 1;
            this.questionByIdButton.FlatAppearance.BorderSize = 0;
            this.questionByIdButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.questionByIdButton.ForeColor = System.Drawing.Color.White;
            this.questionByIdButton.Location = new System.Drawing.Point(44, 158);
            this.questionByIdButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.questionByIdButton.Name = "questionByIdButton";
            this.questionByIdButton.Size = new System.Drawing.Size(156, 48);
            this.questionByIdButton.TabIndex = 18;
            this.questionByIdButton.Text = "הצג שאלה";
            this.questionByIdButton.UseVisualStyleBackColor = false;
            this.questionByIdButton.Click += new System.EventHandler(this.questionbyIdButton_Click);
            // 
            // menuPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1105, 840);
            this.Controls.Add(this.error_colById_label);
            this.Controls.Add(this.error_qById_label);
            this.Controls.Add(this.collectionbyIdButton);
            this.Controls.Add(this.collectionbyIdtextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chaptersQuestions);
            this.Controls.Add(this.questionByIdButton);
            this.Controls.Add(this.normalQuestions);
            this.Controls.Add(this.collectionsQuestions);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.questionByIDtextBox);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "menuPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "menuPage";
            this.Load += new System.EventHandler(this.menuPage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Button normalQuestions;
        private System.Windows.Forms.Button collectionsQuestions;

        private System.Windows.Forms.TextBox questionByIDtextBox;
        private System.Windows.Forms.Label label1;
        private RJButtons2 questionByIdButton;
        private System.Windows.Forms.Button chaptersQuestions;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox collectionbyIdtextBox;
        private RJButtons2 collectionbyIdButton;
        private System.Windows.Forms.Label error_qById_label;
        private System.Windows.Forms.Label error_colById_label;
    }
}