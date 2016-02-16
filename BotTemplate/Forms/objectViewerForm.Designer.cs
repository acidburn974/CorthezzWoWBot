namespace BotTemplate.Forms
{
    partial class objectViewerForm
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
            this.objectGrid = new System.Windows.Forms.DataGridView();
            this.bGetGameobjects = new System.Windows.Forms.Button();
            this.bGetUnits = new System.Windows.Forms.Button();
            this.mtbFilter = new System.Windows.Forms.MaskedTextBox();
            this.bInteract = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.objectGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // objectGrid
            // 
            this.objectGrid.AllowUserToAddRows = false;
            this.objectGrid.AllowUserToDeleteRows = false;
            this.objectGrid.AllowUserToOrderColumns = true;
            this.objectGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.objectGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.objectGrid.Location = new System.Drawing.Point(14, 12);
            this.objectGrid.MultiSelect = false;
            this.objectGrid.Name = "objectGrid";
            this.objectGrid.ReadOnly = true;
            this.objectGrid.Size = new System.Drawing.Size(706, 193);
            this.objectGrid.TabIndex = 0;
            this.objectGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.objectGrid_CellDoubleClick);
            this.objectGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.objectGrid_KeyPress);
            // 
            // bGetGameobjects
            // 
            this.bGetGameobjects.Location = new System.Drawing.Point(124, 211);
            this.bGetGameobjects.Name = "bGetGameobjects";
            this.bGetGameobjects.Size = new System.Drawing.Size(104, 24);
            this.bGetGameobjects.TabIndex = 2;
            this.bGetGameobjects.Text = "Get Game objects";
            this.bGetGameobjects.UseVisualStyleBackColor = true;
            this.bGetGameobjects.Click += new System.EventHandler(this.bGetGameobjects_Click);
            // 
            // bGetUnits
            // 
            this.bGetUnits.Location = new System.Drawing.Point(14, 211);
            this.bGetUnits.Name = "bGetUnits";
            this.bGetUnits.Size = new System.Drawing.Size(104, 24);
            this.bGetUnits.TabIndex = 3;
            this.bGetUnits.Text = "Get Units";
            this.bGetUnits.UseVisualStyleBackColor = true;
            this.bGetUnits.Click += new System.EventHandler(this.bGetUnits_Click);
            // 
            // mtbFilter
            // 
            this.mtbFilter.Location = new System.Drawing.Point(264, 214);
            this.mtbFilter.Name = "mtbFilter";
            this.mtbFilter.Size = new System.Drawing.Size(139, 20);
            this.mtbFilter.TabIndex = 4;
            this.mtbFilter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.mtbFilter_KeyUp);
            // 
            // bInteract
            // 
            this.bInteract.Location = new System.Drawing.Point(426, 211);
            this.bInteract.Name = "bInteract";
            this.bInteract.Size = new System.Drawing.Size(140, 24);
            this.bInteract.TabIndex = 5;
            this.bInteract.Text = "Interact with Object";
            this.bInteract.UseVisualStyleBackColor = true;
            this.bInteract.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // objectViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 251);
            this.Controls.Add(this.bInteract);
            this.Controls.Add(this.mtbFilter);
            this.Controls.Add(this.bGetUnits);
            this.Controls.Add(this.bGetGameobjects);
            this.Controls.Add(this.objectGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "objectViewerForm";
            this.Text = "objectViewerForm";
            ((System.ComponentModel.ISupportInitialize)(this.objectGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView objectGrid;
        private System.Windows.Forms.Button bGetGameobjects;
        private System.Windows.Forms.Button bGetUnits;
        private System.Windows.Forms.MaskedTextBox mtbFilter;
        private System.Windows.Forms.Button bInteract;
    }
}