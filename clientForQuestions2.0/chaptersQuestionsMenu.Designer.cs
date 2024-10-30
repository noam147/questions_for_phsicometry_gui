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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(chaptersQuestionsMenu));
            this.timePerQCheckbox = new System.Windows.Forms.CheckBox();
            this.timePerQPicker = new System.Windows.Forms.DateTimePicker();
            this.continueButton = new System.Windows.Forms.Button();
            this.backToMainMenu = new System.Windows.Forms.Button();
            this.chapterButton_english = new System.Windows.Forms.Button();
            this.chapterButton_math = new System.Windows.Forms.Button();
            this.chapterButton_hebrew = new System.Windows.Forms.Button();
            this.titleOfPage = new System.Windows.Forms.Label();
            this.downloadChapterButton = new System.Windows.Forms.Button();
            this.simulationDownloadButton = new System.Windows.Forms.Button();
            this.i_toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.i_simulationDownload = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.i_downloadChapter = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timePerQCheckbox
            // 
            this.timePerQCheckbox.AutoSize = true;
            this.timePerQCheckbox.Location = new System.Drawing.Point(89, 196);
            this.timePerQCheckbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.timePerQCheckbox.Name = "timePerQCheckbox";
            this.timePerQCheckbox.Size = new System.Drawing.Size(159, 20);
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
            this.timePerQPicker.Location = new System.Drawing.Point(148, 213);
            this.timePerQPicker.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.timePerQPicker.MaxDate = new System.DateTime(2000, 1, 1, 0, 59, 59, 0);
            this.timePerQPicker.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.timePerQPicker.Name = "timePerQPicker";
            this.timePerQPicker.ShowUpDown = true;
            this.timePerQPicker.Size = new System.Drawing.Size(63, 22);
            this.timePerQPicker.TabIndex = 28;
            this.timePerQPicker.Value = new System.DateTime(2000, 1, 1, 0, 20, 0, 0);
            // 
            // continueButton
            // 
            this.continueButton.Enabled = false;
            this.continueButton.Location = new System.Drawing.Point(123, 325);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(99, 61);
            this.continueButton.TabIndex = 27;
            this.continueButton.Text = "התחלת תרגול";
            this.continueButton.UseVisualStyleBackColor = true;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // backToMainMenu
            // 
            this.backToMainMenu.Location = new System.Drawing.Point(12, 12);
            this.backToMainMenu.Name = "backToMainMenu";
            this.backToMainMenu.Size = new System.Drawing.Size(125, 73);
            this.backToMainMenu.TabIndex = 26;
            this.backToMainMenu.Text = "חזרה לתפריט הראשי";
            this.backToMainMenu.UseVisualStyleBackColor = true;
            this.backToMainMenu.Click += new System.EventHandler(this.backToMainMenu_Click);
            // 
            // chapterButton_english
            // 
            this.chapterButton_english.Location = new System.Drawing.Point(349, 265);
            this.chapterButton_english.Name = "chapterButton_english";
            this.chapterButton_english.Size = new System.Drawing.Size(116, 65);
            this.chapterButton_english.TabIndex = 25;
            this.chapterButton_english.Text = "אנגלית";
            this.chapterButton_english.UseVisualStyleBackColor = true;
            this.chapterButton_english.Click += new System.EventHandler(this.chapterButton_Click);
            // 
            // chapterButton_math
            // 
            this.chapterButton_math.Location = new System.Drawing.Point(349, 183);
            this.chapterButton_math.Name = "chapterButton_math";
            this.chapterButton_math.Size = new System.Drawing.Size(116, 76);
            this.chapterButton_math.TabIndex = 24;
            this.chapterButton_math.Text = "חשיבה כמותית";
            this.chapterButton_math.UseVisualStyleBackColor = true;
            this.chapterButton_math.Click += new System.EventHandler(this.chapterButton_Click);
            // 
            // chapterButton_hebrew
            // 
            this.chapterButton_hebrew.Location = new System.Drawing.Point(349, 106);
            this.chapterButton_hebrew.Name = "chapterButton_hebrew";
            this.chapterButton_hebrew.Size = new System.Drawing.Size(116, 71);
            this.chapterButton_hebrew.TabIndex = 23;
            this.chapterButton_hebrew.Text = "חשיבה מילולית";
            this.chapterButton_hebrew.UseVisualStyleBackColor = true;
            this.chapterButton_hebrew.Click += new System.EventHandler(this.chapterButton_Click);
            // 
            // titleOfPage
            // 
            this.titleOfPage.AutoSize = true;
            this.titleOfPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.titleOfPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.titleOfPage.Location = new System.Drawing.Point(315, 9);
            this.titleOfPage.Name = "titleOfPage";
            this.titleOfPage.Size = new System.Drawing.Size(182, 27);
            this.titleOfPage.TabIndex = 30;
            this.titleOfPage.Text = "תרגול פרקים מלאים";
            // 
            // downloadChapterButton
            // 
            this.downloadChapterButton.Enabled = false;
            this.downloadChapterButton.Location = new System.Drawing.Point(586, 319);
            this.downloadChapterButton.Name = "downloadChapterButton";
            this.downloadChapterButton.Size = new System.Drawing.Size(99, 61);
            this.downloadChapterButton.TabIndex = 31;
            this.downloadChapterButton.Text = "הורדת הפרק";
            this.downloadChapterButton.UseVisualStyleBackColor = true;
            this.downloadChapterButton.Click += new System.EventHandler(this.downloadExreciseButton_Click);
            // 
            // simulationDownloadButton
            // 
            this.simulationDownloadButton.Location = new System.Drawing.Point(586, 238);
            this.simulationDownloadButton.Name = "simulationDownloadButton";
            this.simulationDownloadButton.Size = new System.Drawing.Size(99, 61);
            this.simulationDownloadButton.TabIndex = 32;
            this.simulationDownloadButton.Text = "סימולציה מותאמת אישית להורדה";
            this.simulationDownloadButton.UseVisualStyleBackColor = true;
            this.simulationDownloadButton.Click += new System.EventHandler(this.simulationDownloadButton_Click);
            // 
            // i_toolTip
            // 
            this.i_toolTip.AutomaticDelay = 100;
            this.i_toolTip.AutoPopDelay = 10000000;
            this.i_toolTip.InitialDelay = 100;
            this.i_toolTip.IsBalloon = true;
            this.i_toolTip.OwnerDraw = true;
            this.i_toolTip.ReshowDelay = 0;
            this.i_toolTip.ShowAlways = true;
            this.i_toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.i_toolTip.UseAnimation = false;
            this.i_toolTip.UseFading = false;
            // 
            // i_simulationDownload
            // 
            this.i_simulationDownload.AutoSize = true;
            this.i_simulationDownload.BackColor = System.Drawing.SystemColors.Highlight;
            this.i_simulationDownload.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.i_simulationDownload.Cursor = System.Windows.Forms.Cursors.Help;
            this.i_simulationDownload.Font = new System.Drawing.Font("Elephant", 16.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.i_simulationDownload.ForeColor = System.Drawing.Color.GhostWhite;
            this.i_simulationDownload.Location = new System.Drawing.Point(701, 246);
            this.i_simulationDownload.Name = "i_simulationDownload";
            this.i_simulationDownload.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.i_simulationDownload.Size = new System.Drawing.Size(29, 39);
            this.i_simulationDownload.TabIndex = 33;
            this.i_simulationDownload.Text = "i";
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 100;
            this.toolTip1.AutoPopDelay = 10000000;
            this.toolTip1.InitialDelay = 100;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.OwnerDraw = true;
            this.toolTip1.ReshowDelay = 0;
            this.toolTip1.ShowAlways = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.UseAnimation = false;
            this.toolTip1.UseFading = false;
            this.toolTip1.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip1_Popup);
            // 
            // i_downloadChapter
            // 
            this.i_downloadChapter.AutoSize = true;
            this.i_downloadChapter.BackColor = System.Drawing.SystemColors.Highlight;
            this.i_downloadChapter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.i_downloadChapter.Cursor = System.Windows.Forms.Cursors.Help;
            this.i_downloadChapter.Font = new System.Drawing.Font("Elephant", 16.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.i_downloadChapter.ForeColor = System.Drawing.Color.GhostWhite;
            this.i_downloadChapter.Location = new System.Drawing.Point(701, 327);
            this.i_downloadChapter.Name = "i_downloadChapter";
            this.i_downloadChapter.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.i_downloadChapter.Size = new System.Drawing.Size(29, 39);
            this.i_downloadChapter.TabIndex = 34;
            this.i_downloadChapter.Text = "i";
            // 
            // chaptersQuestionsMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.i_downloadChapter);
            this.Controls.Add(this.i_simulationDownload);
            this.Controls.Add(this.simulationDownloadButton);
            this.Controls.Add(this.downloadChapterButton);
            this.Controls.Add(this.titleOfPage);
            this.Controls.Add(this.timePerQCheckbox);
            this.Controls.Add(this.timePerQPicker);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.backToMainMenu);
            this.Controls.Add(this.chapterButton_english);
            this.Controls.Add(this.chapterButton_math);
            this.Controls.Add(this.chapterButton_hebrew);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
        private System.Windows.Forms.Button chapterButton_english;
        private System.Windows.Forms.Button chapterButton_math;
        private System.Windows.Forms.Button chapterButton_hebrew;
        private System.Windows.Forms.Label titleOfPage;
        private System.Windows.Forms.Button downloadChapterButton;
        private System.Windows.Forms.Button simulationDownloadButton;
        public System.Windows.Forms.ToolTip i_toolTip;
        private System.Windows.Forms.Label i_simulationDownload;
        public System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label i_downloadChapter;
    }
}