namespace clientForQuestions2._0
{
    partial class summrizePage
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
            this.button1 = new System.Windows.Forms.Button();
            this.timeTookForQLabel = new System.Windows.Forms.Label();
            this.category_of_q = new System.Windows.Forms.Label();
            this.total_time = new System.Windows.Forms.Label();
            this.avrage_time = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 18);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 57);
            this.button1.TabIndex = 1;
            this.button1.Text = "back to menu";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timeTookForQLabel
            // 
            this.timeTookForQLabel.AutoSize = true;
            this.timeTookForQLabel.Location = new System.Drawing.Point(261, 89);
            this.timeTookForQLabel.Name = "timeTookForQLabel";
            this.timeTookForQLabel.Size = new System.Drawing.Size(145, 16);
            this.timeTookForQLabel.TabIndex = 2;
            this.timeTookForQLabel.Text = "Time took for question: ";
            this.timeTookForQLabel.Click += new System.EventHandler(this.timeTookForQLabel_Click);
            // 
            // category_of_q
            // 
            this.category_of_q.AutoSize = true;
            this.category_of_q.Location = new System.Drawing.Point(574, 89);
            this.category_of_q.Name = "category_of_q";
            this.category_of_q.Size = new System.Drawing.Size(60, 16);
            this.category_of_q.TabIndex = 3;
            this.category_of_q.Text = "category";
            this.category_of_q.Click += new System.EventHandler(this.label1_Click);
            // 
            // total_time
            // 
            this.total_time.AutoSize = true;
            this.total_time.Location = new System.Drawing.Point(12, 89);
            this.total_time.Name = "total_time";
            this.total_time.Size = new System.Drawing.Size(63, 16);
            this.total_time.TabIndex = 4;
            this.total_time.Text = "total time:";
            this.total_time.Click += new System.EventHandler(this.label2_Click);
            // 
            // avrage_time
            // 
            this.avrage_time.AutoSize = true;
            this.avrage_time.Location = new System.Drawing.Point(122, 89);
            this.avrage_time.Name = "avrage_time";
            this.avrage_time.Size = new System.Drawing.Size(81, 16);
            this.avrage_time.TabIndex = 5;
            this.avrage_time.Text = "avrage time:";
            // 
            // summrizePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 739);
            this.Controls.Add(this.avrage_time);
            this.Controls.Add(this.total_time);
            this.Controls.Add(this.category_of_q);
            this.Controls.Add(this.timeTookForQLabel);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "summrizePage";
            this.Text = "summrizePage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label timeTookForQLabel;
        private System.Windows.Forms.Label category_of_q;
        private System.Windows.Forms.Label total_time;
        private System.Windows.Forms.Label avrage_time;
    }
}