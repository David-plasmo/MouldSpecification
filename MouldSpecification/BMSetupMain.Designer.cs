namespace MouldSpecification
{
    partial class BMSetupMain
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboSearchCode = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.injectionMouldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blowMouldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.machineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.materialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.materialTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.materialGradeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.masterbatchToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.additiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.palletToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cartonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.palletToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.categoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgView = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Product = new System.Windows.Forms.TabPage();
            this.Material = new System.Windows.Forms.TabPage();
            this.MasterBatch = new System.Windows.Forms.TabPage();
            this.Packaging = new System.Windows.Forms.TabPage();
            this.DieSet = new System.Windows.Forms.TabPage();
            this.QualityControl = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).BeginInit();
            this.tabControl1.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.cboSearchCode);
            this.splitContainer1.Panel1.Controls.Add(this.menuStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1343, 620);
            this.splitContainer1.SplitterDistance = 40;
            this.splitContainer1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(276, 26);
            this.label1.TabIndex = 6;
            this.label1.Text = "Blow Mould Machine Setup";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(495, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Search Product Code";
            // 
            // cboSearchCode
            // 
            this.cboSearchCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboSearchCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboSearchCode.FormattingEnabled = true;
            this.cboSearchCode.Location = new System.Drawing.Point(610, 29);
            this.cboSearchCode.Name = "cboSearchCode";
            this.cboSearchCode.Size = new System.Drawing.Size(162, 21);
            this.cboSearchCode.TabIndex = 4;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.injectionMouldToolStripMenuItem,
            this.blowMouldToolStripMenuItem,
            this.machineToolStripMenuItem,
            this.materialToolStripMenuItem,
            this.palletToolStripMenuItem,
            this.categoryToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1343, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // injectionMouldToolStripMenuItem
            // 
            this.injectionMouldToolStripMenuItem.Name = "injectionMouldToolStripMenuItem";
            this.injectionMouldToolStripMenuItem.Size = new System.Drawing.Size(103, 20);
            this.injectionMouldToolStripMenuItem.Text = "Injection Mould";
            this.injectionMouldToolStripMenuItem.Click += new System.EventHandler(this.injectionMouldToolStripMenuItem_Click);
            // 
            // blowMouldToolStripMenuItem
            // 
            this.blowMouldToolStripMenuItem.Name = "blowMouldToolStripMenuItem";
            this.blowMouldToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.blowMouldToolStripMenuItem.Text = "Blow Mould";
            this.blowMouldToolStripMenuItem.Click += new System.EventHandler(this.blowMouldToolStripMenuItem_Click);
            // 
            // machineToolStripMenuItem
            // 
            this.machineToolStripMenuItem.Name = "machineToolStripMenuItem";
            this.machineToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.machineToolStripMenuItem.Text = "Machine";
            this.machineToolStripMenuItem.Click += new System.EventHandler(this.machineToolStripMenuItem_Click);
            // 
            // materialToolStripMenuItem
            // 
            this.materialToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.materialTypeToolStripMenuItem,
            this.materialGradeToolStripMenuItem,
            this.masterbatchToolStripMenuItem1,
            this.additiveToolStripMenuItem});
            this.materialToolStripMenuItem.Name = "materialToolStripMenuItem";
            this.materialToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.materialToolStripMenuItem.Text = "Material";
            // 
            // materialTypeToolStripMenuItem
            // 
            this.materialTypeToolStripMenuItem.Name = "materialTypeToolStripMenuItem";
            this.materialTypeToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.materialTypeToolStripMenuItem.Text = "Material Type";
            this.materialTypeToolStripMenuItem.Click += new System.EventHandler(this.materialTypeToolStripMenuItem_Click);
            // 
            // materialGradeToolStripMenuItem
            // 
            this.materialGradeToolStripMenuItem.Name = "materialGradeToolStripMenuItem";
            this.materialGradeToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.materialGradeToolStripMenuItem.Text = "Material Grade";
            this.materialGradeToolStripMenuItem.Click += new System.EventHandler(this.materialGradeToolStripMenuItem_Click);
            // 
            // masterbatchToolStripMenuItem1
            // 
            this.masterbatchToolStripMenuItem1.Name = "masterbatchToolStripMenuItem1";
            this.masterbatchToolStripMenuItem1.Size = new System.Drawing.Size(151, 22);
            this.masterbatchToolStripMenuItem1.Text = "Masterbatch";
            this.masterbatchToolStripMenuItem1.Click += new System.EventHandler(this.masterbatchToolStripMenuItem1_Click);
            // 
            // additiveToolStripMenuItem
            // 
            this.additiveToolStripMenuItem.Name = "additiveToolStripMenuItem";
            this.additiveToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.additiveToolStripMenuItem.Text = "Additive";
            this.additiveToolStripMenuItem.Click += new System.EventHandler(this.additiveToolStripMenuItem_Click);
            // 
            // palletToolStripMenuItem
            // 
            this.palletToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cartonToolStripMenuItem,
            this.palletToolStripMenuItem1});
            this.palletToolStripMenuItem.Name = "palletToolStripMenuItem";
            this.palletToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.palletToolStripMenuItem.Text = "Packaging";
            this.palletToolStripMenuItem.Click += new System.EventHandler(this.palletToolStripMenuItem_Click);
            // 
            // cartonToolStripMenuItem
            // 
            this.cartonToolStripMenuItem.Name = "cartonToolStripMenuItem";
            this.cartonToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.cartonToolStripMenuItem.Text = "Carton";
            this.cartonToolStripMenuItem.Click += new System.EventHandler(this.cartonToolStripMenuItem_Click);
            // 
            // palletToolStripMenuItem1
            // 
            this.palletToolStripMenuItem1.Name = "palletToolStripMenuItem1";
            this.palletToolStripMenuItem1.Size = new System.Drawing.Size(110, 22);
            this.palletToolStripMenuItem1.Text = "Pallet";
            this.palletToolStripMenuItem1.Click += new System.EventHandler(this.palletToolStripMenuItem1_Click);
            // 
            // categoryToolStripMenuItem
            // 
            this.categoryToolStripMenuItem.Name = "categoryToolStripMenuItem";
            this.categoryToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.categoryToolStripMenuItem.Text = "Category";
            this.categoryToolStripMenuItem.Click += new System.EventHandler(this.categoryToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1343, 553);
            this.panel1.TabIndex = 2;
            // 
            // dgView
            // 
            this.dgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgView.Location = new System.Drawing.Point(0, 0);
            this.dgView.Name = "dgView";
            this.dgView.RowHeadersWidth = 25;
            this.dgView.Size = new System.Drawing.Size(1343, 553);
            this.dgView.TabIndex = 0;
            this.dgView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgView_CellBeginEdit);
            this.dgView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgView_CellEndEdit);
            this.dgView.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgView_ColumnWidthChanged);
            this.dgView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgView_DataBindingComplete);
            this.dgView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgView_DataError);
            this.dgView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgView_EditingControlShowing);
            this.dgView.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgView_Scroll);
            this.dgView.Sorted += new System.EventHandler(this.dgView_Sorted);
            this.dgView.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgView_UserDeletingRow);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Product);
            this.tabControl1.Controls.Add(this.Material);
            this.tabControl1.Controls.Add(this.MasterBatch);
            this.tabControl1.Controls.Add(this.Packaging);
            this.tabControl1.Controls.Add(this.DieSet);
            this.tabControl1.Controls.Add(this.QualityControl);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1343, 23);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // Product
            // 
            this.Product.Location = new System.Drawing.Point(4, 22);
            this.Product.Name = "Product";
            this.Product.Padding = new System.Windows.Forms.Padding(3);
            this.Product.Size = new System.Drawing.Size(1335, 0);
            this.Product.TabIndex = 0;
            this.Product.Text = "Product";
            this.Product.UseVisualStyleBackColor = true;
            // 
            // Material
            // 
            this.Material.Location = new System.Drawing.Point(4, 22);
            this.Material.Name = "Material";
            this.Material.Padding = new System.Windows.Forms.Padding(3);
            this.Material.Size = new System.Drawing.Size(1335, 0);
            this.Material.TabIndex = 1;
            this.Material.Text = "Material";
            this.Material.UseVisualStyleBackColor = true;
            // 
            // MasterBatch
            // 
            this.MasterBatch.Location = new System.Drawing.Point(4, 22);
            this.MasterBatch.Name = "MasterBatch";
            this.MasterBatch.Size = new System.Drawing.Size(1335, 0);
            this.MasterBatch.TabIndex = 6;
            this.MasterBatch.Text = "MasterBatch";
            this.MasterBatch.UseVisualStyleBackColor = true;
            // 
            // Packaging
            // 
            this.Packaging.Location = new System.Drawing.Point(4, 22);
            this.Packaging.Name = "Packaging";
            this.Packaging.Size = new System.Drawing.Size(1335, 0);
            this.Packaging.TabIndex = 2;
            this.Packaging.Text = "Packaging";
            this.Packaging.UseVisualStyleBackColor = true;
            // 
            // DieSet
            // 
            this.DieSet.Location = new System.Drawing.Point(4, 22);
            this.DieSet.Name = "DieSet";
            this.DieSet.Size = new System.Drawing.Size(1335, 0);
            this.DieSet.TabIndex = 3;
            this.DieSet.Text = "DieSet";
            this.DieSet.UseVisualStyleBackColor = true;
            // 
            // QualityControl
            // 
            this.QualityControl.Location = new System.Drawing.Point(4, 22);
            this.QualityControl.Name = "QualityControl";
            this.QualityControl.Size = new System.Drawing.Size(1335, 0);
            this.QualityControl.TabIndex = 5;
            this.QualityControl.Text = "QualityControl";
            this.QualityControl.UseVisualStyleBackColor = true;
            // 
            // BMSetupMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1343, 620);
            this.Controls.Add(this.splitContainer1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "BMSetupMain";
            this.Text = "Blow Mould Setup";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BMSetupMain_FormClosing);
            this.Load += new System.EventHandler(this.BMSetupMain_Load);
            this.Shown += new System.EventHandler(this.BMSetupMain_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Product;
        private System.Windows.Forms.TabPage Material;
        private System.Windows.Forms.TabPage Packaging;
        private System.Windows.Forms.TabPage DieSet;
        private System.Windows.Forms.TabPage QualityControl;
        private System.Windows.Forms.TabPage MasterBatch;
        private System.Windows.Forms.DataGridView dgView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboSearchCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem machineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem materialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem palletToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem materialTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem materialGradeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem categoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem injectionMouldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blowMouldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem masterbatchToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem additiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cartonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem palletToolStripMenuItem1;
        //private System.Windows.Forms.ComboBox cbo;
    }
}

