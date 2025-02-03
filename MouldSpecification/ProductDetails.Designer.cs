namespace MouldSpecification
{
    partial class ProductDetails
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvEdit
            // 
            this.dgvEdit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEdit.Location = new System.Drawing.Point(0, 0);
            this.dgvEdit.Margin = new System.Windows.Forms.Padding(2);
            this.dgvEdit.Name = "dgvEdit";
            this.dgvEdit.RowTemplate.Height = 33;
            this.dgvEdit.Size = new System.Drawing.Size(851, 446);
            this.dgvEdit.TabIndex = 0;
            this.dgvEdit.UseWaitCursor = true;
            this.dgvEdit.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEdit_CellClick);
            this.dgvEdit.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvEdit_CellValidating);
            this.dgvEdit.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvEdit_DataError);
            this.dgvEdit.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvEdit_DefaultValuesNeeded);
            this.dgvEdit.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvEdit_EditingControlShowing);
            this.dgvEdit.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvEdit_RowsAdded);
            this.dgvEdit.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvEdit_UserDeletingRow);
            // 
            // ProductDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 446);
            this.Controls.Add(this.dgvEdit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ProductDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Injection Mould Product Details";
            this.UseWaitCursor = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProductDetails_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ProductDetails_FormClosed);
            this.Shown += new System.EventHandler(this.ProductDetails_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEdit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvEdit;
    }
}