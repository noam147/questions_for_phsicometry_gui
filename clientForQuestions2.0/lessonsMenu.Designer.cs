namespace clientForQuestions2._0
{
    partial class lessonsMenu
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.backToMainMenu_button = new System.Windows.Forms.Button();
            this.lessons_dataGridView = new System.Windows.Forms.DataGridView();
            this.emptyLessons_label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.lessons_dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // backToMainMenu_button
            // 
            this.backToMainMenu_button.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.backToMainMenu_button.ForeColor = System.Drawing.Color.Black;
            this.backToMainMenu_button.Location = new System.Drawing.Point(12, 11);
            this.backToMainMenu_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.backToMainMenu_button.Name = "backToMainMenu_button";
            this.backToMainMenu_button.Size = new System.Drawing.Size(113, 60);
            this.backToMainMenu_button.TabIndex = 3;
            this.backToMainMenu_button.Text = "חזרה לתפריט הראשי";
            this.backToMainMenu_button.UseVisualStyleBackColor = false;
            this.backToMainMenu_button.Click += new System.EventHandler(this.backToMainMenu_button_Click);
            // 
            // lessons_dataGridView
            // 
            this.lessons_dataGridView.AllowUserToAddRows = false;
            this.lessons_dataGridView.AllowUserToDeleteRows = false;
            this.lessons_dataGridView.AllowUserToOrderColumns = true;
            this.lessons_dataGridView.AllowUserToResizeColumns = false;
            this.lessons_dataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.lessons_dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.lessons_dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.lessons_dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.lessons_dataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.lessons_dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lessons_dataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenHorizontal;
            this.lessons_dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.lessons_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.lessons_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.lessons_dataGridView.DefaultCellStyle = dataGridViewCellStyle7;
            this.lessons_dataGridView.EnableHeadersVisualStyles = false;
            this.lessons_dataGridView.Location = new System.Drawing.Point(12, 96);
            this.lessons_dataGridView.MultiSelect = false;
            this.lessons_dataGridView.Name = "lessons_dataGridView";
            this.lessons_dataGridView.ReadOnly = true;
            this.lessons_dataGridView.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lessons_dataGridView.RowHeadersVisible = false;
            this.lessons_dataGridView.RowHeadersWidth = 51;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.lessons_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.lessons_dataGridView.RowTemplate.Height = 24;
            this.lessons_dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.lessons_dataGridView.Size = new System.Drawing.Size(240, 150);
            this.lessons_dataGridView.TabIndex = 4;
            this.lessons_dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // emptyLessons_label
            // 
            this.emptyLessons_label.AutoSize = true;
            this.emptyLessons_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.emptyLessons_label.Location = new System.Drawing.Point(178, 192);
            this.emptyLessons_label.Name = "emptyLessons_label";
            this.emptyLessons_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.emptyLessons_label.Size = new System.Drawing.Size(441, 54);
            this.emptyLessons_label.TabIndex = 16;
            this.emptyLessons_label.Text = "עדיין לא שמרת לקחים";
            this.emptyLessons_label.Visible = false;
            // 
            // lessonsMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.emptyLessons_label);
            this.Controls.Add(this.lessons_dataGridView);
            this.Controls.Add(this.backToMainMenu_button);
            this.Name = "lessonsMenu";
            this.Text = "lessonsMenu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.lessonsMenu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lessons_dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button backToMainMenu_button;
        private System.Windows.Forms.DataGridView lessons_dataGridView;
        private System.Windows.Forms.Label emptyLessons_label;
    }
}