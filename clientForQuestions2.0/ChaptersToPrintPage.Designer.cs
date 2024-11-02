namespace clientForQuestions2._0
{
    partial class ChaptersToPrintPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChaptersToPrintPage));
            this.englishAmount = new System.Windows.Forms.NumericUpDown();
            this.mathAmount = new System.Windows.Forms.NumericUpDown();
            this.hebrewAmount = new System.Windows.Forms.NumericUpDown();
            this.simulationDownloadButton = new System.Windows.Forms.Button();
            this.titleOfPage = new System.Windows.Forms.Label();
            this.mathLabel = new System.Windows.Forms.Label();
            this.hebrewTitle = new System.Windows.Forms.Label();
            this.backToMainMenu = new System.Windows.Forms.Button();
            this.englishTextsCheckBox = new System.Windows.Forms.CheckBox();
            this.hebrewTextsCheckBox = new System.Windows.Forms.CheckBox();
            this.mathGraphCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.englishAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mathAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hebrewAmount)).BeginInit();
            this.SuspendLayout();
            // 
            // englishAmount
            // 
            this.englishAmount.Location = new System.Drawing.Point(419, 176);
            this.englishAmount.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.englishAmount.Name = "englishAmount";
            this.englishAmount.Size = new System.Drawing.Size(76, 22);
            this.englishAmount.TabIndex = 17;
            // 
            // mathAmount
            // 
            this.mathAmount.Location = new System.Drawing.Point(319, 176);
            this.mathAmount.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.mathAmount.Name = "mathAmount";
            this.mathAmount.Size = new System.Drawing.Size(76, 22);
            this.mathAmount.TabIndex = 17;
            // 
            // hebrewAmount
            // 
            this.hebrewAmount.Location = new System.Drawing.Point(215, 176);
            this.hebrewAmount.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.hebrewAmount.Name = "hebrewAmount";
            this.hebrewAmount.Size = new System.Drawing.Size(76, 22);
            this.hebrewAmount.TabIndex = 18;
            this.hebrewAmount.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // simulationDownloadButton
            // 
            this.simulationDownloadButton.Location = new System.Drawing.Point(319, 230);
            this.simulationDownloadButton.Name = "simulationDownloadButton";
            this.simulationDownloadButton.Size = new System.Drawing.Size(99, 61);
            this.simulationDownloadButton.TabIndex = 33;
            this.simulationDownloadButton.Text = "הורדת פרקים";
            this.simulationDownloadButton.UseVisualStyleBackColor = true;
            this.simulationDownloadButton.Click += new System.EventHandler(this.simulationDownloadButton_Click);
            // 
            // titleOfPage
            // 
            this.titleOfPage.AutoSize = true;
            this.titleOfPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.titleOfPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.titleOfPage.Location = new System.Drawing.Point(419, 132);
            this.titleOfPage.Name = "titleOfPage";
            this.titleOfPage.Size = new System.Drawing.Size(73, 27);
            this.titleOfPage.TabIndex = 34;
            this.titleOfPage.Text = "אנגלית";
            // 
            // mathLabel
            // 
            this.mathLabel.AutoSize = true;
            this.mathLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mathLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.mathLabel.Location = new System.Drawing.Point(319, 132);
            this.mathLabel.Name = "mathLabel";
            this.mathLabel.Size = new System.Drawing.Size(64, 27);
            this.mathLabel.TabIndex = 35;
            this.mathLabel.Text = "כמותי";
            // 
            // hebrewTitle
            // 
            this.hebrewTitle.AutoSize = true;
            this.hebrewTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hebrewTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.hebrewTitle.Location = new System.Drawing.Point(215, 132);
            this.hebrewTitle.Name = "hebrewTitle";
            this.hebrewTitle.Size = new System.Drawing.Size(70, 27);
            this.hebrewTitle.TabIndex = 36;
            this.hebrewTitle.Text = "מילולי";
            // 
            // backToMainMenu
            // 
            this.backToMainMenu.Location = new System.Drawing.Point(26, 18);
            this.backToMainMenu.Name = "backToMainMenu";
            this.backToMainMenu.Size = new System.Drawing.Size(125, 73);
            this.backToMainMenu.TabIndex = 37;
            this.backToMainMenu.Text = "חזרה לתפריט הראשי";
            this.backToMainMenu.UseVisualStyleBackColor = true;
            this.backToMainMenu.Click += new System.EventHandler(this.backToMainMenu_Click);
            // 
            // englishTextsCheckBox
            // 
            this.englishTextsCheckBox.AutoSize = true;
            this.englishTextsCheckBox.Checked = true;
            this.englishTextsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.englishTextsCheckBox.Location = new System.Drawing.Point(419, 88);
            this.englishTextsCheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.englishTextsCheckBox.Name = "englishTextsCheckBox";
            this.englishTextsCheckBox.Size = new System.Drawing.Size(79, 20);
            this.englishTextsCheckBox.TabIndex = 38;
            this.englishTextsCheckBox.Text = "עם טקסט";
            this.englishTextsCheckBox.UseVisualStyleBackColor = true;
            // 
            // hebrewTextsCheckBox
            // 
            this.hebrewTextsCheckBox.AutoSize = true;
            this.hebrewTextsCheckBox.Checked = true;
            this.hebrewTextsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.hebrewTextsCheckBox.Location = new System.Drawing.Point(215, 88);
            this.hebrewTextsCheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.hebrewTextsCheckBox.Name = "hebrewTextsCheckBox";
            this.hebrewTextsCheckBox.Size = new System.Drawing.Size(79, 20);
            this.hebrewTextsCheckBox.TabIndex = 39;
            this.hebrewTextsCheckBox.Text = "עם טקסט";
            this.hebrewTextsCheckBox.UseVisualStyleBackColor = true;
            // 
            // mathGraphCheckBox
            // 
            this.mathGraphCheckBox.AutoSize = true;
            this.mathGraphCheckBox.Checked = true;
            this.mathGraphCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mathGraphCheckBox.Location = new System.Drawing.Point(312, 88);
            this.mathGraphCheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mathGraphCheckBox.Name = "mathGraphCheckBox";
            this.mathGraphCheckBox.Size = new System.Drawing.Size(69, 20);
            this.mathGraphCheckBox.TabIndex = 40;
            this.mathGraphCheckBox.Text = "עם גרף";
            this.mathGraphCheckBox.UseVisualStyleBackColor = true;
            // 
            // ChaptersToPrintPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 360);
            this.Controls.Add(this.mathGraphCheckBox);
            this.Controls.Add(this.hebrewTextsCheckBox);
            this.Controls.Add(this.englishTextsCheckBox);
            this.Controls.Add(this.backToMainMenu);
            this.Controls.Add(this.hebrewTitle);
            this.Controls.Add(this.mathLabel);
            this.Controls.Add(this.titleOfPage);
            this.Controls.Add(this.simulationDownloadButton);
            this.Controls.Add(this.hebrewAmount);
            this.Controls.Add(this.mathAmount);
            this.Controls.Add(this.englishAmount);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ChaptersToPrintPage";
            this.Text = "תפריט פרקים להורדה";
            ((System.ComponentModel.ISupportInitialize)(this.englishAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mathAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hebrewAmount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown englishAmount;
        private System.Windows.Forms.NumericUpDown mathAmount;
        private System.Windows.Forms.NumericUpDown hebrewAmount;
        private System.Windows.Forms.Button simulationDownloadButton;
        private System.Windows.Forms.Label titleOfPage;
        private System.Windows.Forms.Label mathLabel;
        private System.Windows.Forms.Label hebrewTitle;
        private System.Windows.Forms.Button backToMainMenu;
        private System.Windows.Forms.CheckBox englishTextsCheckBox;
        private System.Windows.Forms.CheckBox hebrewTextsCheckBox;
        private System.Windows.Forms.CheckBox mathGraphCheckBox;
    }
}