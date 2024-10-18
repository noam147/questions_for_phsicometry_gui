namespace clientForQuestions2._0
{
    partial class testHistoryMenu
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
            this.backToMainMenu_button = new System.Windows.Forms.Button();
            this.dataGridView1_ZMANI = new System.Windows.Forms.DataGridView();
            this.emptyHistory_label = new System.Windows.Forms.Label();
            this.nextExreciseButton = new clientForQuestions2._0.RJButtons2();
            this.previousExreciseButton = new clientForQuestions2._0.RJButtons2();
            this.resetHistory_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1_ZMANI)).BeginInit();
            this.SuspendLayout();
            // 
            // backToMainMenu_button
            // 
            this.backToMainMenu_button.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.backToMainMenu_button.ForeColor = System.Drawing.Color.Black;
            this.backToMainMenu_button.Location = new System.Drawing.Point(3, 1);
            this.backToMainMenu_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.backToMainMenu_button.Name = "backToMainMenu_button";
            this.backToMainMenu_button.Size = new System.Drawing.Size(113, 60);
            this.backToMainMenu_button.TabIndex = 2;
            this.backToMainMenu_button.Text = "חזרה לתפריט הראשי";
            this.backToMainMenu_button.UseVisualStyleBackColor = false;
            this.backToMainMenu_button.Click += new System.EventHandler(this.backToMainMenu_button_Click);
            // 
            // dataGridView1_ZMANI
            // 
            this.dataGridView1_ZMANI.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1_ZMANI.Location = new System.Drawing.Point(339, 186);
            this.dataGridView1_ZMANI.Name = "dataGridView1_ZMANI";
            this.dataGridView1_ZMANI.RowHeadersWidth = 51;
            this.dataGridView1_ZMANI.RowTemplate.Height = 24;
            this.dataGridView1_ZMANI.Size = new System.Drawing.Size(240, 150);
            this.dataGridView1_ZMANI.TabIndex = 14;
            this.dataGridView1_ZMANI.Visible = false;
            // 
            // emptyHistory_label
            // 
            this.emptyHistory_label.AutoSize = true;
            this.emptyHistory_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.emptyHistory_label.Location = new System.Drawing.Point(127, 165);
            this.emptyHistory_label.Name = "emptyHistory_label";
            this.emptyHistory_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.emptyHistory_label.Size = new System.Drawing.Size(503, 54);
            this.emptyHistory_label.TabIndex = 15;
            this.emptyHistory_label.Text = "היסטורית התרגולים ריקה";
            this.emptyHistory_label.Visible = false;
            this.emptyHistory_label.Click += new System.EventHandler(this.emptyHistory_label_Click);
            // 
            // nextExreciseButton
            // 
            this.nextExreciseButton.BackColor = System.Drawing.Color.Chocolate;
            this.nextExreciseButton.BorderRadiuos = 32;
            this.nextExreciseButton.BorderSize = 1;
            this.nextExreciseButton.FlatAppearance.BorderSize = 0;
            this.nextExreciseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nextExreciseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.nextExreciseButton.ForeColor = System.Drawing.Color.White;
            this.nextExreciseButton.Location = new System.Drawing.Point(645, 83);
            this.nextExreciseButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nextExreciseButton.Name = "nextExreciseButton";
            this.nextExreciseButton.Size = new System.Drawing.Size(32, 32);
            this.nextExreciseButton.TabIndex = 13;
            this.nextExreciseButton.Text = "🢂";
            this.nextExreciseButton.UseVisualStyleBackColor = false;
            this.nextExreciseButton.Click += new System.EventHandler(this.nextExreciseButton_Click);
            // 
            // previousExreciseButton
            // 
            this.previousExreciseButton.BackColor = System.Drawing.Color.Chocolate;
            this.previousExreciseButton.BorderRadiuos = 32;
            this.previousExreciseButton.BorderSize = 1;
            this.previousExreciseButton.FlatAppearance.BorderSize = 0;
            this.previousExreciseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.previousExreciseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.previousExreciseButton.ForeColor = System.Drawing.Color.White;
            this.previousExreciseButton.Location = new System.Drawing.Point(84, 83);
            this.previousExreciseButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.previousExreciseButton.Name = "previousExreciseButton";
            this.previousExreciseButton.Size = new System.Drawing.Size(32, 32);
            this.previousExreciseButton.TabIndex = 12;
            this.previousExreciseButton.Text = "🢀";
            this.previousExreciseButton.UseVisualStyleBackColor = false;
            this.previousExreciseButton.Click += new System.EventHandler(this.previousExreciseButton_Click);
            // 
            // resetHistory_button
            // 
            this.resetHistory_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.resetHistory_button.Location = new System.Drawing.Point(709, 1);
            this.resetHistory_button.Name = "resetHistory_button";
            this.resetHistory_button.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.resetHistory_button.Size = new System.Drawing.Size(88, 44);
            this.resetHistory_button.TabIndex = 3;
            this.resetHistory_button.Text = "איפוס היסטוריה";
            this.resetHistory_button.UseVisualStyleBackColor = false;
            this.resetHistory_button.Click += new System.EventHandler(this.resetHistory_button_Click);
            // 
            // testHistoryMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 450);
            this.Controls.Add(this.emptyHistory_label);
            this.Controls.Add(this.dataGridView1_ZMANI);
            this.Controls.Add(this.nextExreciseButton);
            this.Controls.Add(this.previousExreciseButton);
            this.Controls.Add(this.resetHistory_button);
            this.Controls.Add(this.backToMainMenu_button);
            this.Name = "testHistoryMenu";
            this.Text = "testHistoryMenu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.testHistoryMenu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1_ZMANI)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button backToMainMenu_button;
        private RJButtons2 previousExreciseButton;
        private RJButtons2 nextExreciseButton;
        private System.Windows.Forms.DataGridView dataGridView1_ZMANI;
        private System.Windows.Forms.Label emptyHistory_label;
        private System.Windows.Forms.Button resetHistory_button;
    }
}