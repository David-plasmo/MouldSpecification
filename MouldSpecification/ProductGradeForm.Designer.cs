namespace MouldSpecification
{
    partial class ProductGradeForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvEdit = new System.Windows.Forms.DataGridView();
            this.opf = new System.Windows.Forms.OpenFileDialog();
            this.strip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.locateFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.copyLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.closeMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEdit)).BeginInit();
            this.strip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
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
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 43;
            this.splitContainer1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(287, 26);
            this.label1.TabIndex = 11;
            this.label1.Text = "Product Category Reference";
            // 
            // dgvEdit
            // 
            this.dgvEdit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEdit.Location = new System.Drawing.Point(0, 0);
            this.dgvEdit.Name = "dgvEdit";
            this.dgvEdit.Size = new System.Drawing.Size(800, 403);
            this.dgvEdit.TabIndex = 0;
            this.dgvEdit.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.dgvEdit_CellContextMenuStripNeeded);
            this.dgvEdit.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvEdit_CellMouseDown);
            this.dgvEdit.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvEdit_UserDeletingRow);
            this.dgvEdit.DoubleClick += new System.EventHandler(this.dgvEdit_DoubleClick);
            this.dgvEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvEdit_KeyDown);
            // 
            // opf
            // 
            this.opf.FileName = "openFileDialog1";
            // 
            // strip
            // 
            this.strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.locateFileToolStripMenuItem,
            this.saveLinkToolStripMenuItem,
            this.removeLinkToolStripMenuItem,
            this.toolStripMenuItem1,
            this.copyLinkToolStripMenuItem,
            this.pasteLinkToolStripMenuItem,
            this.toolStripMenuItem2,
            this.closeMenuToolStripMenuItem});
            this.strip.Name = "strip";
            this.strip.Size = new System.Drawing.Size(143, 170);
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openFileToolStripMenuItem.Text = "Open File";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // locateFileToolStripMenuItem
            // 
            this.locateFileToolStripMenuItem.Name = "locateFileToolStripMenuItem";
            this.locateFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.locateFileToolStripMenuItem.Text = "Locate File";
            this.locateFileToolStripMenuItem.Click += new System.EventHandler(this.locateFileToolStripMenuItem_Click);
            // 
            // saveLinkToolStripMenuItem
            // 
            this.saveLinkToolStripMenuItem.Name = "saveLinkToolStripMenuItem";
            this.saveLinkToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveLinkToolStripMenuItem.Text = "Save Link";
            this.saveLinkToolStripMenuItem.Click += new System.EventHandler(this.saveLinkToolStripMenuItem_Click);
            // 
            // removeLinkToolStripMenuItem
            // 
            this.removeLinkToolStripMenuItem.Name = "removeLinkToolStripMenuItem";
            this.removeLinkToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.removeLinkToolStripMenuItem.Text = "Remove Link";
            this.removeLinkToolStripMenuItem.Click += new System.EventHandler(this.removeLinkToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // copyLinkToolStripMenuItem
            // 
            this.copyLinkToolStripMenuItem.Name = "copyLinkToolStripMenuItem";
            this.copyLinkToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.copyLinkToolStripMenuItem.Text = "Copy Link";
            this.copyLinkToolStripMenuItem.Click += new System.EventHandler(this.copyLinkToolStripMenuItem_Click);
            // 
            // pasteLinkToolStripMenuItem
            // 
            this.pasteLinkToolStripMenuItem.Name = "pasteLinkToolStripMenuItem";
            this.pasteLinkToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.pasteLinkToolStripMenuItem.Text = "Paste Link";
            this.pasteLinkToolStripMenuItem.Click += new System.EventHandler(this.pasteLinkToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(177, 6);
            // 
            // closeMenuToolStripMenuItem
            // 
            this.closeMenuToolStripMenuItem.Name = "closeMenuToolStripMenuItem";
            this.closeMenuToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.closeMenuToolStripMenuItem.Text = "Close Menu";
            this.closeMenuToolStripMenuItem.Click += new System.EventHandler(this.closeMenuToolStripMenuItem_Click);
            // 
            // ProductGradeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ProductGradeForm";
            this.Text = "Product Category";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProductGradeForm_FormClosing);
            this.Load += new System.EventHandler(this.ProductGradeForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEdit)).EndInit();
            this.strip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvEdit;
        private System.Windows.Forms.OpenFileDialog opf;
        private System.Windows.Forms.ContextMenuStrip strip;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem locateFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveLinkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeLinkToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyLinkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteLinkToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem closeMenuToolStripMenuItem;
    }
}