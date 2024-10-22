namespace clientForQuestions2._0
{
    partial class HtmlConvertOptionsMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HtmlConvertOptionsMenu));
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.filePath_button = new System.Windows.Forms.Button();
            this.isNum_checkBox = new System.Windows.Forms.CheckBox();
            this.explanation_comboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // filePath_button
            // 
            this.filePath_button.Location = new System.Drawing.Point(103, 181);
            this.filePath_button.Name = "filePath_button";
            this.filePath_button.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.filePath_button.Size = new System.Drawing.Size(106, 47);
            this.filePath_button.TabIndex = 1;
            this.filePath_button.Text = "שמירת תרגול בשם...";
            this.filePath_button.UseVisualStyleBackColor = true;
            this.filePath_button.Click += new System.EventHandler(this.filePath_button_Click);
            // 
            // isNum_checkBox
            // 
            this.isNum_checkBox.AutoSize = true;
            this.isNum_checkBox.Checked = true;
            this.isNum_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isNum_checkBox.Location = new System.Drawing.Point(131, 116);
            this.isNum_checkBox.Name = "isNum_checkBox";
            this.isNum_checkBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.isNum_checkBox.Size = new System.Drawing.Size(114, 20);
            this.isNum_checkBox.TabIndex = 3;
            this.isNum_checkBox.Text = "מספור השאלות";
            this.isNum_checkBox.UseVisualStyleBackColor = true;
            // 
            // explanation_comboBox
            // 
            this.explanation_comboBox.FormattingEnabled = true;
            this.explanation_comboBox.Items.AddRange(new object[] {
            "בלי הסברים ותשובות",
            "הסברים ותשובות בסוף הקובץ",
            "הסבר ותשובה בסוף כל שאלה"});
            this.explanation_comboBox.Location = new System.Drawing.Point(30, 86);
            this.explanation_comboBox.Name = "explanation_comboBox";
            this.explanation_comboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.explanation_comboBox.Size = new System.Drawing.Size(262, 24);
            this.explanation_comboBox.TabIndex = 4;
            // 
            // HtmlConvertOptionsMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(323, 240);
            this.Controls.Add(this.explanation_comboBox);
            this.Controls.Add(this.isNum_checkBox);
            this.Controls.Add(this.filePath_button);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HtmlConvertOptionsMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HtmlConvertOptionsMenu";
            this.Load += new System.EventHandler(this.HtmlConvertOptionsMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button filePath_button;
        private System.Windows.Forms.CheckBox isNum_checkBox;
        private System.Windows.Forms.ComboBox explanation_comboBox;
    }
}