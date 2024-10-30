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
            this.englishAmount = new System.Windows.Forms.NumericUpDown();
            this.mathAmount = new System.Windows.Forms.NumericUpDown();
            this.hebrewAmount = new System.Windows.Forms.NumericUpDown();
            this.simulationDownloadButton = new System.Windows.Forms.Button();
            this.titleOfPage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.backToMainMenu = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.englishAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mathAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hebrewAmount)).BeginInit();
            this.SuspendLayout();
            // 
            // englishAmount
            // 
            this.englishAmount.Location = new System.Drawing.Point(357, 212);
            this.englishAmount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.englishAmount.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.englishAmount.Name = "englishAmount";
            this.englishAmount.Size = new System.Drawing.Size(86, 26);
            this.englishAmount.TabIndex = 17;
            // 
            // mathAmount
            // 
            this.mathAmount.Location = new System.Drawing.Point(244, 212);
            this.mathAmount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mathAmount.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.mathAmount.Name = "mathAmount";
            this.mathAmount.Size = new System.Drawing.Size(86, 26);
            this.mathAmount.TabIndex = 17;
            // 
            // hebrewAmount
            // 
            this.hebrewAmount.Location = new System.Drawing.Point(127, 212);
            this.hebrewAmount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.hebrewAmount.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.hebrewAmount.Name = "hebrewAmount";
            this.hebrewAmount.Size = new System.Drawing.Size(86, 26);
            this.hebrewAmount.TabIndex = 18;
            this.hebrewAmount.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // simulationDownloadButton
            // 
            this.simulationDownloadButton.Location = new System.Drawing.Point(244, 280);
            this.simulationDownloadButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simulationDownloadButton.Name = "simulationDownloadButton";
            this.simulationDownloadButton.Size = new System.Drawing.Size(111, 76);
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
            this.titleOfPage.Location = new System.Drawing.Point(357, 157);
            this.titleOfPage.Name = "titleOfPage";
            this.titleOfPage.Size = new System.Drawing.Size(86, 31);
            this.titleOfPage.TabIndex = 34;
            this.titleOfPage.Text = "אנגלית";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(244, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 31);
            this.label1.TabIndex = 35;
            this.label1.Text = "כמותי";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(127, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 31);
            this.label2.TabIndex = 36;
            this.label2.Text = "מילולי";
            // 
            // backToMainMenu
            // 
            this.backToMainMenu.Location = new System.Drawing.Point(29, 23);
            this.backToMainMenu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.backToMainMenu.Name = "backToMainMenu";
            this.backToMainMenu.Size = new System.Drawing.Size(141, 91);
            this.backToMainMenu.TabIndex = 37;
            this.backToMainMenu.Text = "חזרה לתפריט הראשי";
            this.backToMainMenu.UseVisualStyleBackColor = true;
            this.backToMainMenu.Click += new System.EventHandler(this.backToMainMenu_Click);
            // 
            // ChaptersToPrintPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.backToMainMenu);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.titleOfPage);
            this.Controls.Add(this.simulationDownloadButton);
            this.Controls.Add(this.hebrewAmount);
            this.Controls.Add(this.mathAmount);
            this.Controls.Add(this.englishAmount);
            this.Name = "ChaptersToPrintPage";
            this.Text = "ChaptersToPrintPage";
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button backToMainMenu;
    }
}