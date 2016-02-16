namespace BotTemplate.Forms
{
    partial class whisperForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cTime,
            this.cType,
            this.cFrom,
            this.cText});
            this.dataGridView1.Location = new System.Drawing.Point(8, 10);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(746, 194);
            this.dataGridView1.TabIndex = 0;
            // 
            // cTime
            // 
            this.cTime.HeaderText = "Time";
            this.cTime.Name = "cTime";
            // 
            // cType
            // 
            this.cType.HeaderText = "Type";
            this.cType.Name = "cType";
            // 
            // cFrom
            // 
            this.cFrom.HeaderText = "From";
            this.cFrom.Name = "cFrom";
            // 
            // cText
            // 
            this.cText.HeaderText = "Text";
            this.cText.Name = "cText";
            this.cText.Width = 402;
            // 
            // bClear
            // 
            this.bClear.Location = new System.Drawing.Point(8, 210);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(130, 36);
            this.bClear.TabIndex = 1;
            this.bClear.Text = "Clear";
            this.bClear.UseVisualStyleBackColor = true;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // whisperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 258);
            this.Controls.Add(this.bClear);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "whisperForm";
            this.Text = "whisperForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button bClear;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn cType;
        private System.Windows.Forms.DataGridViewTextBoxColumn cFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn cText;
    }
}