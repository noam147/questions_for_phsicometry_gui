namespace clientForQuestions2._0
{
    partial class chaptersQuestionsMenu
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
            this.timePerQCheckbox = new System.Windows.Forms.CheckBox();
            this.timePerQPicker = new System.Windows.Forms.DateTimePicker();
            this.continueButton = new System.Windows.Forms.Button();
            this.backToMainMenu = new System.Windows.Forms.Button();
            this.chapterButton3 = new System.Windows.Forms.Button();
            this.chapterButton2 = new System.Windows.Forms.Button();
            this.chapterButton1 = new System.Windows.Forms.Button();
            this.titleOfPage = new System.Windows.Forms.Label();
            this.downloadExreciseButton = new System.Windows.Forms.Button();
            this.simulationDownloadButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timePerQCheckbox
            // 
            this.timePerQCheckbox.AutoSize = true;
            this.timePerQCheckbox.Location = new System.Drawing.Point(100, 245);
            this.timePerQCheckbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.timePerQCheckbox.Name = "timePerQCheckbox";
            this.timePerQCheckbox.Size = new System.Drawing.Size(190, 24);
            this.timePerQCheckbox.TabIndex = 29;
            this.timePerQCheckbox.Text = "זמן לפרק מותאם אישית";
            this.timePerQCheckbox.UseVisualStyleBackColor = true;
            this.timePerQCheckbox.CheckedChanged += new System.EventHandler(this.timePerQCheckbox_CheckedChanged);
            // 
            // timePerQPicker
            // 
            this.timePerQPicker.CustomFormat = "mm:ss";
            this.timePerQPicker.Enabled = false;
            this.timePerQPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timePerQPicker.Location = new System.Drawing.Point(166, 266);
            this.timePerQPicker.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.timePerQPicker.MaxDate = new System.DateTime(2000, 1, 1, 0, 59, 59, 0);
            this.timePerQPicker.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.timePerQPicker.Name = "timePerQPicker";
            this.timePerQPicker.ShowUpDown = true;
            this.timePerQPicker.Size = new System.Drawing.Size(70, 26);
            this.timePerQPicker.TabIndex = 28;
            this.timePerQPicker.Value = new System.DateTime(2000, 1, 1, 0, 20, 0, 0);
            // 
            // continueButton
            // 
            this.continueButton.Enabled = false;
            this.continueButton.Location = new System.Drawing.Point(138, 406);
            this.continueButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(111, 76);
            this.continueButton.TabIndex = 27;
            this.continueButton.Text = "התחלת תרגול";
            this.continueButton.UseVisualStyleBackColor = true;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // backToMainMenu
            // 
            this.backToMainMenu.Location = new System.Drawing.Point(14, 15);
            this.backToMainMenu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.backToMainMenu.Name = "backToMainMenu";
            this.backToMainMenu.Size = new System.Drawing.Size(141, 91);
            this.backToMainMenu.TabIndex = 26;
            this.backToMainMenu.Text = "חזרה לתפריט הראשי";
            this.backToMainMenu.UseVisualStyleBackColor = true;
            this.backToMainMenu.Click += new System.EventHandler(this.backToMainMenu_Click);
            // 
            // chapterButton3
            // 
            this.chapterButton3.Location = new System.Drawing.Point(393, 331);
            this.chapterButton3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chapterButton3.Name = "chapterButton3";
            this.chapterButton3.Size = new System.Drawing.Size(130, 81);
            this.chapterButton3.TabIndex = 25;
            this.chapterButton3.Text = "אנגלית";
            this.chapterButton3.UseVisualStyleBackColor = true;
            this.chapterButton3.Click += new System.EventHandler(this.chapterButton_Click);
            // 
            // chapterButton2
            // 
            this.chapterButton2.Location = new System.Drawing.Point(393, 229);
            this.chapterButton2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chapterButton2.Name = "chapterButton2";
            this.chapterButton2.Size = new System.Drawing.Size(130, 95);
            this.chapterButton2.TabIndex = 24;
            this.chapterButton2.Text = "חשיבה כמותית";
            this.chapterButton2.UseVisualStyleBackColor = true;
            this.chapterButton2.Click += new System.EventHandler(this.chapterButton_Click);
            // 
            // chapterButton1
            // 
            this.chapterButton1.Location = new System.Drawing.Point(393, 132);
            this.chapterButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chapterButton1.Name = "chapterButton1";
            this.chapterButton1.Size = new System.Drawing.Size(130, 89);
            this.chapterButton1.TabIndex = 23;
            this.chapterButton1.Text = "חשיבה מילולית";
            this.chapterButton1.UseVisualStyleBackColor = true;
            this.chapterButton1.Click += new System.EventHandler(this.chapterButton_Click);
            // 
            // titleOfPage
            // 
            this.titleOfPage.AutoSize = true;
            this.titleOfPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.titleOfPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.titleOfPage.Location = new System.Drawing.Point(354, 11);
            this.titleOfPage.Name = "titleOfPage";
            this.titleOfPage.Size = new System.Drawing.Size(218, 31);
            this.titleOfPage.TabIndex = 30;
            this.titleOfPage.Text = "תרגול פרקים מלאים";
            // 
            // downloadExreciseButton
            // 
            this.downloadExreciseButton.Enabled = false;
            this.downloadExreciseButton.Location = new System.Drawing.Point(659, 419);
            this.downloadExreciseButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.downloadExreciseButton.Name = "downloadExreciseButton";
            this.downloadExreciseButton.Size = new System.Drawing.Size(111, 76);
            this.downloadExreciseButton.TabIndex = 31;
            this.downloadExreciseButton.Text = "הורדת תרגול";
            this.downloadExreciseButton.UseVisualStyleBackColor = true;
            this.downloadExreciseButton.Click += new System.EventHandler(this.downloadExreciseButton_Click);
            // 
            // simulationDownloadButton
            // 
            this.simulationDownloadButton.Location = new System.Drawing.Point(659, 296);
            this.simulationDownloadButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simulationDownloadButton.Name = "simulationDownloadButton";
            this.simulationDownloadButton.Size = new System.Drawing.Size(111, 76);
            this.simulationDownloadButton.TabIndex = 32;
            this.simulationDownloadButton.Text = "סימולציה 6 פרקים להורדה";
            this.simulationDownloadButton.UseVisualStyleBackColor = true;
            this.simulationDownloadButton.Click += new System.EventHandler(this.simulationDownloadButton_Click);
            // 
            // chaptersQuestionsMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 562);
            this.Controls.Add(this.simulationDownloadButton);
            this.Controls.Add(this.downloadExreciseButton);
            this.Controls.Add(this.titleOfPage);
            this.Controls.Add(this.timePerQCheckbox);
            this.Controls.Add(this.timePerQPicker);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.backToMainMenu);
            this.Controls.Add(this.chapterButton3);
            this.Controls.Add(this.chapterButton2);
            this.Controls.Add(this.chapterButton1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "chaptersQuestionsMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "chaptersQuestionsMenu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox timePerQCheckbox;
        private System.Windows.Forms.DateTimePicker timePerQPicker;
        private System.Windows.Forms.Button continueButton;
        private System.Windows.Forms.Button backToMainMenu;
        private System.Windows.Forms.Button chapterButton3;
        private System.Windows.Forms.Button chapterButton2;
        private System.Windows.Forms.Button chapterButton1;
        private System.Windows.Forms.Label titleOfPage;
        private System.Windows.Forms.Button downloadExreciseButton;
        private System.Windows.Forms.Button simulationDownloadButton;
    }
}