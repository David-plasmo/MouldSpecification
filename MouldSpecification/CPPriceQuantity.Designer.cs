namespace MouldSpecification
{
    partial class CPPriceQuantity
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
            this.dgvEdit = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.strip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.duplicateOntoNewRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.strip.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvEdit
            // 
            this.dgvEdit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEdit.Location = new System.Drawing.Point(0, 0);
            this.dgvEdit.Margin = new System.Windows.Forms.Padding(6);
            this.dgvEdit.Name = "dgvEdit";
            this.dgvEdit.RowHeadersWidth = 25;
            this.dgvEdit.Size = new System.Drawing.Size(2481, 1293);
            this.dgvEdit.TabIndex = 0;
            this.dgvEdit.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvEdit_CellBeginEdit);
            this.dgvEdit.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEdit_CellClick);
            this.dgvEdit.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.dgvEdit_CellContextMenuStripNeeded);
            this.dgvEdit.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEdit_CellEndEdit);
            this.dgvEdit.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvEdit_CellMouseDown);
            this.dgvEdit.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvEdit_ColumnWidthChanged);
            this.dgvEdit.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvEdit_EditingControlShowing);
            this.dgvEdit.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgvEdit_Scroll);
            this.dgvEdit.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvEdit_UserDeletingRow);
            this.dgvEdit.VisibleChanged += new System.EventHandler(this.dgvEdit_VisibleChanged);
            this.dgvEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvEdit_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(364, 51);
            this.label1.TabIndex = 10;
            this.label1.Text = "CP Price Quantity";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(6);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvEdit);
            this.splitContainer1.Size = new System.Drawing.Size(2481, 1427);
            this.splitContainer1.SplitterDistance = 126;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 1;
            // 
            // strip
            // 
            this.strip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.duplicateOntoNewRowToolStripMenuItem});
            this.strip.Name = "contextMenuStrip1";
            this.strip.Size = new System.Drawing.Size(359, 42);
            // 
            // duplicateOntoNewRowToolStripMenuItem
            // 
            this.duplicateOntoNewRowToolStripMenuItem.Name = "duplicateOntoNewRowToolStripMenuItem";
            this.duplicateOntoNewRowToolStripMenuItem.Size = new System.Drawing.Size(358, 38);
            this.duplicateOntoNewRowToolStripMenuItem.Text = "Duplicate Onto New Row";
            this.duplicateOntoNewRowToolStripMenuItem.Click += new System.EventHandler(this.duplicateOntoNewRowToolStripMenuItem_Click);
            // 
            // CPPriceQuantity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2481, 1427);
            this.Controls.Add(this.splitContainer1);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "CPPriceQuantity";
            this.Text = "CP Price Quantity";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CPPriceQuantity_FormClosing);
            this.Load += new System.EventHandler(this.CPPriceQuantity_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEdit)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.strip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvEdit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ContextMenuStrip strip;
        private System.Windows.Forms.ToolStripMenuItem duplicateOntoNewRowToolStripMenuItem;
    }
}