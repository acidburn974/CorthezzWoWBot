namespace BotTemplate.Forms
{
    partial class teleportForm
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
            this.telePointGrid = new System.Windows.Forms.DataGridView();
            this.teleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.bSaveCurrent = new System.Windows.Forms.Button();
            this.cbHotKey = new System.Windows.Forms.CheckBox();
            this.nudTeleSpeed = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.telePointGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTeleSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // telePointGrid
            // 
            this.telePointGrid.AllowUserToAddRows = false;
            this.telePointGrid.AllowUserToDeleteRows = false;
            this.telePointGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.telePointGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.teleName});
            this.telePointGrid.Location = new System.Drawing.Point(12, 12);
            this.telePointGrid.MultiSelect = false;
            this.telePointGrid.Name = "telePointGrid";
            this.telePointGrid.ReadOnly = true;
            this.telePointGrid.Size = new System.Drawing.Size(280, 217);
            this.telePointGrid.TabIndex = 0;
            this.telePointGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.telePointGrid_CellDoubleClick);
            this.telePointGrid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.telePointGrid_KeyUp);
            // 
            // teleName
            // 
            this.teleName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.teleName.HeaderText = "Name";
            this.teleName.Name = "teleName";
            this.teleName.ReadOnly = true;
            // 
            // tbSearch
            // 
            this.tbSearch.Location = new System.Drawing.Point(12, 238);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(144, 20);
            this.tbSearch.TabIndex = 1;
            this.tbSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbSearch_KeyUp);
            // 
            // bSaveCurrent
            // 
            this.bSaveCurrent.Location = new System.Drawing.Point(200, 234);
            this.bSaveCurrent.Name = "bSaveCurrent";
            this.bSaveCurrent.Size = new System.Drawing.Size(92, 26);
            this.bSaveCurrent.TabIndex = 2;
            this.bSaveCurrent.Text = "Save Current";
            this.bSaveCurrent.UseVisualStyleBackColor = true;
            this.bSaveCurrent.Click += new System.EventHandler(this.bSaveCurrent_Click);
            // 
            // cbHotKey
            // 
            this.cbHotKey.AutoSize = true;
            this.cbHotKey.Location = new System.Drawing.Point(12, 268);
            this.cbHotKey.Name = "cbHotKey";
            this.cbHotKey.Size = new System.Drawing.Size(132, 17);
            this.cbHotKey.TabIndex = 3;
            this.cbHotKey.Text = "Enable hotkey teleport";
            this.cbHotKey.UseVisualStyleBackColor = true;
            this.cbHotKey.CheckedChanged += new System.EventHandler(this.cbHotKey_CheckedChanged);
            // 
            // nudTeleSpeed
            // 
            this.nudTeleSpeed.DecimalPlaces = 1;
            this.nudTeleSpeed.Location = new System.Drawing.Point(150, 268);
            this.nudTeleSpeed.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.nudTeleSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudTeleSpeed.Name = "nudTeleSpeed";
            this.nudTeleSpeed.Size = new System.Drawing.Size(112, 20);
            this.nudTeleSpeed.TabIndex = 4;
            this.nudTeleSpeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // teleportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 297);
            this.Controls.Add(this.nudTeleSpeed);
            this.Controls.Add(this.cbHotKey);
            this.Controls.Add(this.bSaveCurrent);
            this.Controls.Add(this.tbSearch);
            this.Controls.Add(this.telePointGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "teleportForm";
            this.Text = "teleportForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.teleportForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.telePointGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTeleSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView telePointGrid;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.Button bSaveCurrent;
        private System.Windows.Forms.DataGridViewTextBoxColumn teleName;
        private System.Windows.Forms.CheckBox cbHotKey;
        private System.Windows.Forms.NumericUpDown nudTeleSpeed;


    }
}