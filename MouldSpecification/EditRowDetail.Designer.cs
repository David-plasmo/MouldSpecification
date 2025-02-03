namespace MouldSpecification
{
    partial class EditRowDetail
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.dgvEdit = new System.Windows.Forms.DataGridView();
            this.stripImage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.locateFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.copyLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.closeMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.opf = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEdit)).BeginInit();
            this.stripImage.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnCancel);
            this.splitContainer1.Panel1.Controls.Add(this.btnOK);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvEdit);
            this.splitContainer1.Size = new System.Drawing.Size(1614, 1235);
            this.splitContainer1.SplitterDistance = 119;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(1448, 23);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(128, 48);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(1230, 23);
            this.btnOK.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(128, 48);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // dgvEdit
            // 
            this.dgvEdit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEdit.Location = new System.Drawing.Point(0, 0);
            this.dgvEdit.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.dgvEdit.Name = "dgvEdit";
            this.dgvEdit.RowHeadersWidth = 25;
            this.dgvEdit.Size = new System.Drawing.Size(1614, 1108);
            this.dgvEdit.TabIndex = 1;
            this.dgvEdit.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvEdit_CellBeginEdit);
            this.dgvEdit.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.dgvEdit_CellContextMenuStripNeeded);
            this.dgvEdit.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEdit_CellEndEdit);
            this.dgvEdit.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvEdit_CellMouseDown);
            this.dgvEdit.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvEdit_ColumnWidthChanged);
            this.dgvEdit.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvEdit_DataBindingComplete);
            this.dgvEdit.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvEdit_DataError);
            this.dgvEdit.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvEdit_EditingControlShowing);
            this.dgvEdit.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgvEdit_Scroll);
            this.dgvEdit.Sorted += new System.EventHandler(this.dgvEdit_Sorted);
            this.dgvEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvEdit_KeyDown);
            // 
            // stripImage
            // 
            this.stripImage.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.stripImage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.locateFileToolStripMenuItem,
            this.saveLinkToolStripMenuItem,
            this.removeLinkToolStripMenuItem,
            this.toolStripMenuItem1,
            this.copyLinkToolStripMenuItem,
            this.pasteLinkToolStripMenuItem,
            this.toolStripMenuItem2,
            this.closeMenuToolStripMenuItem,
            this.toolStripSeparator1});
            this.stripImage.Name = "stripImage";
            this.stripImage.Size = new System.Drawing.Size(301, 332);
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(300, 38);
            this.openFileToolStripMenuItem.Text = "Open File";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // locateFileToolStripMenuItem
            // 
            this.locateFileToolStripMenuItem.Name = "locateFileToolStripMenuItem";
            this.locateFileToolStripMenuItem.Size = new System.Drawing.Size(300, 38);
            this.locateFileToolStripMenuItem.Text = "Locate File";
            this.locateFileToolStripMenuItem.Click += new System.EventHandler(this.locateFileToolStripMenuItem_Click);
            // 
            // saveLinkToolStripMenuItem
            // 
            this.saveLinkToolStripMenuItem.Name = "saveLinkToolStripMenuItem";
            this.saveLinkToolStripMenuItem.Size = new System.Drawing.Size(300, 38);
            this.saveLinkToolStripMenuItem.Text = "Save Link";
            this.saveLinkToolStripMenuItem.Click += new System.EventHandler(this.saveLinkToolStripMenuItem_Click);
            // 
            // removeLinkToolStripMenuItem
            // 
            this.removeLinkToolStripMenuItem.Name = "removeLinkToolStripMenuItem";
            this.removeLinkToolStripMenuItem.Size = new System.Drawing.Size(300, 38);
            this.removeLinkToolStripMenuItem.Text = "Remove Link";
            this.removeLinkToolStripMenuItem.Click += new System.EventHandler(this.removeLinkToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(297, 6);
            // 
            // copyLinkToolStripMenuItem
            // 
            this.copyLinkToolStripMenuItem.Name = "copyLinkToolStripMenuItem";
            this.copyLinkToolStripMenuItem.Size = new System.Drawing.Size(300, 38);
            this.copyLinkToolStripMenuItem.Text = "Copy Link";
            this.copyLinkToolStripMenuItem.Click += new System.EventHandler(this.copyLinkToolStripMenuItem_Click);
            // 
            // pasteLinkToolStripMenuItem
            // 
            this.pasteLinkToolStripMenuItem.Name = "pasteLinkToolStripMenuItem";
            this.pasteLinkToolStripMenuItem.Size = new System.Drawing.Size(300, 38);
            this.pasteLinkToolStripMenuItem.Text = "Paste Link";
            this.pasteLinkToolStripMenuItem.Click += new System.EventHandler(this.pasteLinkToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(297, 6);
            // 
            // closeMenuToolStripMenuItem
            // 
            this.closeMenuToolStripMenuItem.Name = "closeMenuToolStripMenuItem";
            this.closeMenuToolStripMenuItem.Size = new System.Drawing.Size(300, 38);
            this.closeMenuToolStripMenuItem.Text = "Close Menu";
            this.closeMenuToolStripMenuItem.Click += new System.EventHandler(this.closeMenuToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(297, 6);
            // 
            // opf
            // 
            this.opf.FileName = "openFileDialog1";
            // 
            // EditRowDetail
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1614, 1235);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "EditRowDetail";
            this.Text = "Edit Row Detail";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditRowDetail_FormClosing);
            this.Load += new System.EventHandler(this.EditRowDetail_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEdit)).EndInit();
            this.stripImage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DataGridView dgvEdit;
        private System.Windows.Forms.ContextMenuStrip stripImage;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem locateFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveLinkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeLinkToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyLinkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteLinkToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem closeMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.OpenFileDialog opf;
    }
}