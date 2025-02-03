namespace MouldSpecification
{
    partial class CartonPackaging
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
            this.dgvEdit = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvEdit
            // 
            this.dgvEdit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEdit.Location = new System.Drawing.Point(0, 0);
            this.dgvEdit.Name = "dgvEdit";
            this.dgvEdit.RowHeadersWidth = 25;
            this.dgvEdit.Size = new System.Drawing.Size(1250, 399);
            this.dgvEdit.TabIndex = 0;
            this.dgvEdit.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvEdit_CellBeginEdit);
            this.dgvEdit.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEdit_CellEndEdit);
            this.dgvEdit.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvEdit_CellValidating);
            this.dgvEdit.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvEdit_DataError);
            this.dgvEdit.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvEdit_EditingControlShowing);
            this.dgvEdit.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgvEdit_Scroll);
            this.dgvEdit.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvEdit_UserDeletingRow);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(291, 26);
            this.label1.TabIndex = 7;
            this.label1.Text = "Carton Packaging Reference";
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
            this.splitContainer1.Size = new System.Drawing.Size(1250, 450);
            this.splitContainer1.SplitterDistance = 47;
            this.splitContainer1.TabIndex = 1;
            // 
            // CartonPackaging
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1250, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "CartonPackaging";
            this.Text = "Carton Packaging";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CartonPackaging_FormClosing);
            this.Load += new System.EventHandler(this.CartonPackaging_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEdit)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvEdit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}