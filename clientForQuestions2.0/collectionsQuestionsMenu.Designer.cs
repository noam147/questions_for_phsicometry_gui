namespace clientForQuestions2._0
{
    partial class collectionsQuestionsMenu
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
            this.colButton_math = new System.Windows.Forms.Button();
            this.colButton_hebrew = new System.Windows.Forms.Button();
            this.colButton_english = new System.Windows.Forms.Button();
            this.backToMainMenu = new System.Windows.Forms.Button();
            this.continueButton = new System.Windows.Forms.Button();
            this.timePerQCheckbox = new System.Windows.Forms.CheckBox();
            this.timePerQPicker = new System.Windows.Forms.DateTimePicker();
            this.titleOfPage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // colButton_math
            // 
            this.colButton_math.Location = new System.Drawing.Point(350, 107);
            this.colButton_math.Name = "colButton1";
            this.colButton_math.Size = new System.Drawing.Size(131, 71);
            this.colButton_math.TabIndex = 0;
            this.colButton_math.Text = "הסקה מתרשים";
            this.colButton_math.UseVisualStyleBackColor = true;
            this.colButton_math.Click += new System.EventHandler(this.colButton_Click);
            // 
            // colButton_hebrew
            // 
            this.colButton_hebrew.Location = new System.Drawing.Point(350, 184);
            this.colButton_hebrew.Name = "colButton2";
            this.colButton_hebrew.Size = new System.Drawing.Size(131, 96);
            this.colButton_hebrew.TabIndex = 1;
            this.colButton_hebrew.Text = "קטע קריאה";
            this.colButton_hebrew.UseVisualStyleBackColor = true;
            this.colButton_hebrew.Click += new System.EventHandler(this.colButton_Click);
            // 
            // colButton_english
            // 
            this.colButton_english.Location = new System.Drawing.Point(350, 286);
            this.colButton_english.Name = "colButton3";
            this.colButton_english.Size = new System.Drawing.Size(131, 83);
            this.colButton_english.TabIndex = 2;
            this.colButton_english.Text = "Reading Comprehension";
            this.colButton_english.UseVisualStyleBackColor = true;
            this.colButton_english.Click += new System.EventHandler(this.colButton_Click);
            // 
            // backToMainMenu
            // 
            this.backToMainMenu.Location = new System.Drawing.Point(13, 13);
            this.backToMainMenu.Name = "backToMainMenu";
            this.backToMainMenu.Size = new System.Drawing.Size(114, 68);
            this.backToMainMenu.TabIndex = 3;
            this.backToMainMenu.Text = "חזרה לתפריט הראשי";
            this.backToMainMenu.UseVisualStyleBackColor = true;
            this.backToMainMenu.Click += new System.EventHandler(this.backToMainMenu_Click);
            // 
            // continueButton
            // 
            this.continueButton.Enabled = false;
            this.continueButton.Location = new System.Drawing.Point(124, 326);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(99, 61);
            this.continueButton.TabIndex = 4;
            this.continueButton.Text = "התחלת תרגול";
            this.continueButton.UseVisualStyleBackColor = true;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // timePerQCheckbox
            // 
            this.timePerQCheckbox.AutoSize = true;
            this.timePerQCheckbox.Checked = true;
            this.timePerQCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.timePerQCheckbox.Location = new System.Drawing.Point(90, 197);
            this.timePerQCheckbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.timePerQCheckbox.Name = "timePerQCheckbox";
            this.timePerQCheckbox.Size = new System.Drawing.Size(133, 20);
            this.timePerQCheckbox.TabIndex = 22;
            this.timePerQCheckbox.Text = "הגבלת זמן לשאלה";
            this.timePerQCheckbox.UseVisualStyleBackColor = true;
            this.timePerQCheckbox.CheckedChanged += new System.EventHandler(this.timePerQCheckbox_CheckedChanged);
            // 
            // timePerQPicker
            // 
            this.timePerQPicker.CustomFormat = "mm:ss";
            this.timePerQPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timePerQPicker.Location = new System.Drawing.Point(149, 214);
            this.timePerQPicker.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.timePerQPicker.MaxDate = new System.DateTime(2000, 1, 1, 0, 59, 59, 0);
            this.timePerQPicker.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.timePerQPicker.Name = "timePerQPicker";
            this.timePerQPicker.ShowUpDown = true;
            this.timePerQPicker.Size = new System.Drawing.Size(63, 22);
            this.timePerQPicker.TabIndex = 21;
            this.timePerQPicker.Value = new System.DateTime(2000, 1, 1, 0, 1, 0, 0);
            // 
            // titleOfPage
            // 
            this.titleOfPage.AutoSize = true;
            this.titleOfPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.titleOfPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.titleOfPage.Location = new System.Drawing.Point(289, 9);
            this.titleOfPage.Name = "titleOfPage";
            this.titleOfPage.Size = new System.Drawing.Size(261, 27);
            this.titleOfPage.TabIndex = 25;
            this.titleOfPage.Text = "תרגול קטעי קריאה ותרשימים";
            // 
            // collectionsQuestionsMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.titleOfPage);
            this.Controls.Add(this.timePerQCheckbox);
            this.Controls.Add(this.timePerQPicker);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.backToMainMenu);
            this.Controls.Add(this.colButton_english);
            this.Controls.Add(this.colButton_hebrew);
            this.Controls.Add(this.colButton_math);
            this.Name = "collectionsQuestionsMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "collectionsQuestionsMenu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button colButton_math;
        private System.Windows.Forms.Button colButton_hebrew;
        private System.Windows.Forms.Button colButton_english;
        private System.Windows.Forms.Button backToMainMenu;
        private System.Windows.Forms.Button continueButton;
        private System.Windows.Forms.CheckBox timePerQCheckbox;
        private System.Windows.Forms.DateTimePicker timePerQPicker;
        private System.Windows.Forms.Label titleOfPage;
    }
}