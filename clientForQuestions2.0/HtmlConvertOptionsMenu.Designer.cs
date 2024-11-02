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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HtmlConvertOptionsMenu));
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.filePath_button = new System.Windows.Forms.Button();
            this.isNum_checkBox = new System.Windows.Forms.CheckBox();
            this.explanation_comboBox = new System.Windows.Forms.ComboBox();
            this.i_downloadButton = new System.Windows.Forms.Label();
            this.i_toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.withTestId_checkBox = new System.Windows.Forms.CheckBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // filePath_button
            // 
            this.filePath_button.Location = new System.Drawing.Point(117, 181);
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
            this.isNum_checkBox.Location = new System.Drawing.Point(37, 116);
            this.isNum_checkBox.Name = "isNum_checkBox";
            this.isNum_checkBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.isNum_checkBox.Size = new System.Drawing.Size(114, 20);
            this.isNum_checkBox.TabIndex = 3;
            this.isNum_checkBox.Text = "מספור השאלות";
            this.isNum_checkBox.UseVisualStyleBackColor = true;
            // 
            // explanation_comboBox
            // 
            this.explanation_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.explanation_comboBox.FormattingEnabled = true;
            this.explanation_comboBox.Items.AddRange(new object[] {
            "בלי הסברים ותשובות",
            "הסברים ותשובות בסוף הקובץ",
            "הסבר ותשובה בסוף כל שאלה"});
            this.explanation_comboBox.Location = new System.Drawing.Point(33, 86);
            this.explanation_comboBox.Name = "explanation_comboBox";
            this.explanation_comboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.explanation_comboBox.Size = new System.Drawing.Size(262, 24);
            this.explanation_comboBox.TabIndex = 4;
            // 
            // i_downloadButton
            // 
            this.i_downloadButton.AutoSize = true;
            this.i_downloadButton.BackColor = System.Drawing.SystemColors.Highlight;
            this.i_downloadButton.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.i_downloadButton.Cursor = System.Windows.Forms.Cursors.Help;
            this.i_downloadButton.Font = new System.Drawing.Font("Elephant", 10.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.i_downloadButton.ForeColor = System.Drawing.Color.GhostWhite;
            this.i_downloadButton.Location = new System.Drawing.Point(92, 191);
            this.i_downloadButton.Name = "i_downloadButton";
            this.i_downloadButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.i_downloadButton.Size = new System.Drawing.Size(19, 26);
            this.i_downloadButton.TabIndex = 34;
            this.i_downloadButton.Text = "i";
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
            // withTestId_checkBox
            // 
            this.withTestId_checkBox.AutoSize = true;
            this.withTestId_checkBox.Checked = true;
            this.withTestId_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.withTestId_checkBox.Location = new System.Drawing.Point(37, 142);
            this.withTestId_checkBox.Name = "withTestId_checkBox";
            this.withTestId_checkBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.withTestId_checkBox.Size = new System.Drawing.Size(206, 20);
            this.withTestId_checkBox.TabIndex = 35;
            this.withTestId_checkBox.Text = "לציין את מספר התרגול בכותרת";
            this.withTestId_checkBox.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 109);
            this.progressBar1.MarqueeAnimationSpeed = 50;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(299, 39);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 0;
            this.progressBar1.Visible = false;
            // 
            // HtmlConvertOptionsMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(323, 240);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.withTestId_checkBox);
            this.Controls.Add(this.i_downloadButton);
            this.Controls.Add(this.explanation_comboBox);
            this.Controls.Add(this.isNum_checkBox);
            this.Controls.Add(this.filePath_button);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HtmlConvertOptionsMenu";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "הורדת תרגול";
            this.Load += new System.EventHandler(this.HtmlConvertOptionsMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button filePath_button;
        private System.Windows.Forms.CheckBox isNum_checkBox;
        private System.Windows.Forms.ComboBox explanation_comboBox;
        private System.Windows.Forms.Label i_downloadButton;
        public System.Windows.Forms.ToolTip i_toolTip;
        private System.Windows.Forms.CheckBox withTestId_checkBox;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}