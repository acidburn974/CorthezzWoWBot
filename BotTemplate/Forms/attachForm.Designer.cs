namespace BotTemplate.Forms
{
    partial class attachForm
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
            this.processList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // processList
            // 
            this.processList.FormattingEnabled = true;
            this.processList.Location = new System.Drawing.Point(12, 11);
            this.processList.Name = "processList";
            this.processList.Size = new System.Drawing.Size(122, 147);
            this.processList.TabIndex = 1;
            this.processList.Click += new System.EventHandler(this.processList_Click);
            // 
            // attachForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(147, 176);
            this.Controls.Add(this.processList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "attachForm";
            this.Text = "attachForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox processList;

    }
}