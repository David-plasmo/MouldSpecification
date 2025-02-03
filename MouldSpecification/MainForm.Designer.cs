namespace MouldSpecification
{
    partial class MainForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Product Specification");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Product Data Entry", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Product Details");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Material Composition");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("MasterBatch Composition");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Machine Preference");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Customer Price Qty");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Mould Specification");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Quality Control");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Packaging");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Data Table Maintenance", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10});
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.cboCustomer = new System.Windows.Forms.ComboBox();
            this.lblProudct = new System.Windows.Forms.Label();
            this.cboProduct = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mouldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blowMouldToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.injectionMouldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extrusionMouldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mouldToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.blowMouldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.polymerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.materialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.materialTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.machineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.masterBatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fixedCostsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.suppliersToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.suppliersToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.accessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.injectionMouldToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.injectionMouldToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.blowMouldToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.blowMouldToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.injectionMouldToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.extrusionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blowMouldingSpecificationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.injectionMouldToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tvMain = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.cboCustomer);
            this.splitContainer1.Panel1.Controls.Add(this.lblProudct);
            this.splitContainer1.Panel1.Controls.Add(this.cboProduct);
            this.splitContainer1.Panel1.Controls.Add(this.menuStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1311, 606);
            this.splitContainer1.SplitterDistance = 30;
            this.splitContainer1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Customer";
            // 
            // cboCustomer
            // 
            this.cboCustomer.FormattingEnabled = true;
            this.cboCustomer.Location = new System.Drawing.Point(68, 36);
            this.cboCustomer.Name = "cboCustomer";
            this.cboCustomer.Size = new System.Drawing.Size(202, 21);
            this.cboCustomer.TabIndex = 1;
            // 
            // lblProudct
            // 
            this.lblProudct.AutoSize = true;
            this.lblProudct.Location = new System.Drawing.Point(282, 39);
            this.lblProudct.Name = "lblProudct";
            this.lblProudct.Size = new System.Drawing.Size(44, 13);
            this.lblProudct.TabIndex = 2;
            this.lblProudct.Text = "Product";
            // 
            // cboProduct
            // 
            this.cboProduct.FormattingEnabled = true;
            this.cboProduct.Location = new System.Drawing.Point(332, 36);
            this.cboProduct.Name = "cboProduct";
            this.cboProduct.Size = new System.Drawing.Size(421, 21);
            this.cboProduct.TabIndex = 1;
            this.cboProduct.DropDownClosed += new System.EventHandler(this.cboProduct_DropDownClosed);
            this.cboProduct.TextChanged += new System.EventHandler(this.cboProduct_TextChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mouldToolStripMenuItem,
            this.mouldToolStripMenuItem1,
            this.accessToolStripMenuItem,
            this.reportsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(1311, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mouldToolStripMenuItem
            // 
            this.mouldToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.blowMouldToolStripMenuItem1,
            this.injectionMouldToolStripMenuItem,
            this.extrusionMouldToolStripMenuItem});
            this.mouldToolStripMenuItem.Name = "mouldToolStripMenuItem";
            this.mouldToolStripMenuItem.Size = new System.Drawing.Size(54, 22);
            this.mouldToolStripMenuItem.Text = "Mould";
            // 
            // blowMouldToolStripMenuItem1
            // 
            this.blowMouldToolStripMenuItem1.Enabled = false;
            this.blowMouldToolStripMenuItem1.Name = "blowMouldToolStripMenuItem1";
            this.blowMouldToolStripMenuItem1.Size = new System.Drawing.Size(158, 22);
            this.blowMouldToolStripMenuItem1.Text = "Injection Mould";
            this.blowMouldToolStripMenuItem1.Click += new System.EventHandler(this.blowMouldToolStripMenuItem1_Click);
            // 
            // injectionMouldToolStripMenuItem
            // 
            this.injectionMouldToolStripMenuItem.Enabled = false;
            this.injectionMouldToolStripMenuItem.Name = "injectionMouldToolStripMenuItem";
            this.injectionMouldToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.injectionMouldToolStripMenuItem.Text = "Blow Mould";
            // 
            // extrusionMouldToolStripMenuItem
            // 
            this.extrusionMouldToolStripMenuItem.Enabled = false;
            this.extrusionMouldToolStripMenuItem.Name = "extrusionMouldToolStripMenuItem";
            this.extrusionMouldToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.extrusionMouldToolStripMenuItem.Text = "Extrusion";
            // 
            // mouldToolStripMenuItem1
            // 
            this.mouldToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.blowMouldToolStripMenuItem,
            this.polymerToolStripMenuItem,
            this.machineToolStripMenuItem,
            this.masterBatchToolStripMenuItem,
            this.fixedCostsToolStripMenuItem,
            this.suppliersToolStripMenuItem,
            this.suppliersToolStripMenuItem1});
            this.mouldToolStripMenuItem1.Name = "mouldToolStripMenuItem1";
            this.mouldToolStripMenuItem1.Size = new System.Drawing.Size(48, 22);
            this.mouldToolStripMenuItem1.Text = "Costs";
            // 
            // blowMouldToolStripMenuItem
            // 
            this.blowMouldToolStripMenuItem.Name = "blowMouldToolStripMenuItem";
            this.blowMouldToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.blowMouldToolStripMenuItem.Text = "Additive";
            this.blowMouldToolStripMenuItem.Click += new System.EventHandler(this.blowMouldToolStripMenuItem_Click);
            // 
            // polymerToolStripMenuItem
            // 
            this.polymerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.materialToolStripMenuItem,
            this.materialTypeToolStripMenuItem});
            this.polymerToolStripMenuItem.Name = "polymerToolStripMenuItem";
            this.polymerToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.polymerToolStripMenuItem.Text = "Material";
            this.polymerToolStripMenuItem.Click += new System.EventHandler(this.polymerToolStripMenuItem_Click);
            // 
            // materialToolStripMenuItem
            // 
            this.materialToolStripMenuItem.Name = "materialToolStripMenuItem";
            this.materialToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.materialToolStripMenuItem.Text = "Material Grade";
            this.materialToolStripMenuItem.Click += new System.EventHandler(this.materialToolStripMenuItem_Click);
            // 
            // materialTypeToolStripMenuItem
            // 
            this.materialTypeToolStripMenuItem.Name = "materialTypeToolStripMenuItem";
            this.materialTypeToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.materialTypeToolStripMenuItem.Text = "Material Type";
            this.materialTypeToolStripMenuItem.Click += new System.EventHandler(this.materialTypeToolStripMenuItem_Click);
            // 
            // machineToolStripMenuItem
            // 
            this.machineToolStripMenuItem.Name = "machineToolStripMenuItem";
            this.machineToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.machineToolStripMenuItem.Text = "Machine";
            this.machineToolStripMenuItem.Click += new System.EventHandler(this.machineToolStripMenuItem_Click);
            // 
            // masterBatchToolStripMenuItem
            // 
            this.masterBatchToolStripMenuItem.Name = "masterBatchToolStripMenuItem";
            this.masterBatchToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.masterBatchToolStripMenuItem.Text = "MasterBatch";
            this.masterBatchToolStripMenuItem.Click += new System.EventHandler(this.masterBatchToolStripMenuItem_Click);
            // 
            // fixedCostsToolStripMenuItem
            // 
            this.fixedCostsToolStripMenuItem.Name = "fixedCostsToolStripMenuItem";
            this.fixedCostsToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.fixedCostsToolStripMenuItem.Text = "Fixed Costs";
            this.fixedCostsToolStripMenuItem.Click += new System.EventHandler(this.fixedCostsToolStripMenuItem_Click);
            // 
            // suppliersToolStripMenuItem
            // 
            this.suppliersToolStripMenuItem.Name = "suppliersToolStripMenuItem";
            this.suppliersToolStripMenuItem.Size = new System.Drawing.Size(137, 6);
            // 
            // suppliersToolStripMenuItem1
            // 
            this.suppliersToolStripMenuItem1.Enabled = false;
            this.suppliersToolStripMenuItem1.Name = "suppliersToolStripMenuItem1";
            this.suppliersToolStripMenuItem1.Size = new System.Drawing.Size(140, 22);
            this.suppliersToolStripMenuItem1.Text = "Suppliers";
            // 
            // accessToolStripMenuItem
            // 
            this.accessToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.injectionMouldToolStripMenuItem1,
            this.blowMouldToolStripMenuItem2});
            this.accessToolStripMenuItem.Enabled = false;
            this.accessToolStripMenuItem.Name = "accessToolStripMenuItem";
            this.accessToolStripMenuItem.Size = new System.Drawing.Size(55, 22);
            this.accessToolStripMenuItem.Text = "Access";
            // 
            // injectionMouldToolStripMenuItem1
            // 
            this.injectionMouldToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.injectionMouldToolStripMenuItem2,
            this.blowMouldToolStripMenuItem3});
            this.injectionMouldToolStripMenuItem1.Name = "injectionMouldToolStripMenuItem1";
            this.injectionMouldToolStripMenuItem1.Size = new System.Drawing.Size(137, 22);
            this.injectionMouldToolStripMenuItem1.Text = "Data Entry";
            // 
            // injectionMouldToolStripMenuItem2
            // 
            this.injectionMouldToolStripMenuItem2.Name = "injectionMouldToolStripMenuItem2";
            this.injectionMouldToolStripMenuItem2.Size = new System.Drawing.Size(212, 22);
            this.injectionMouldToolStripMenuItem2.Text = "Injection Mould/Extrusion";
            this.injectionMouldToolStripMenuItem2.Click += new System.EventHandler(this.injectionMouldToolStripMenuItem2_Click);
            // 
            // blowMouldToolStripMenuItem3
            // 
            this.blowMouldToolStripMenuItem3.Name = "blowMouldToolStripMenuItem3";
            this.blowMouldToolStripMenuItem3.Size = new System.Drawing.Size(212, 22);
            this.blowMouldToolStripMenuItem3.Text = "Blow Mould";
            this.blowMouldToolStripMenuItem3.Click += new System.EventHandler(this.blowMouldToolStripMenuItem3_Click);
            // 
            // blowMouldToolStripMenuItem2
            // 
            this.blowMouldToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.injectionMouldToolStripMenuItem3,
            this.extrusionToolStripMenuItem,
            this.blowMouldingSpecificationsToolStripMenuItem});
            this.blowMouldToolStripMenuItem2.Name = "blowMouldToolStripMenuItem2";
            this.blowMouldToolStripMenuItem2.Size = new System.Drawing.Size(137, 22);
            this.blowMouldToolStripMenuItem2.Text = "Data Import";
            // 
            // injectionMouldToolStripMenuItem3
            // 
            this.injectionMouldToolStripMenuItem3.Name = "injectionMouldToolStripMenuItem3";
            this.injectionMouldToolStripMenuItem3.Size = new System.Drawing.Size(251, 22);
            this.injectionMouldToolStripMenuItem3.Text = "Injection Moulding Specifications";
            this.injectionMouldToolStripMenuItem3.Click += new System.EventHandler(this.injectionMouldToolStripMenuItem3_Click);
            // 
            // extrusionToolStripMenuItem
            // 
            this.extrusionToolStripMenuItem.Enabled = false;
            this.extrusionToolStripMenuItem.Name = "extrusionToolStripMenuItem";
            this.extrusionToolStripMenuItem.Size = new System.Drawing.Size(251, 22);
            this.extrusionToolStripMenuItem.Text = "Extrusion Specifications";
            // 
            // blowMouldingSpecificationsToolStripMenuItem
            // 
            this.blowMouldingSpecificationsToolStripMenuItem.Enabled = false;
            this.blowMouldingSpecificationsToolStripMenuItem.Name = "blowMouldingSpecificationsToolStripMenuItem";
            this.blowMouldingSpecificationsToolStripMenuItem.Size = new System.Drawing.Size(251, 22);
            this.blowMouldingSpecificationsToolStripMenuItem.Text = "Blow Moulding Specifications";
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.injectionMouldToolStripMenuItem4});
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(59, 22);
            this.reportsToolStripMenuItem.Text = "Reports";
            // 
            // injectionMouldToolStripMenuItem4
            // 
            this.injectionMouldToolStripMenuItem4.Name = "injectionMouldToolStripMenuItem4";
            this.injectionMouldToolStripMenuItem4.Size = new System.Drawing.Size(158, 22);
            this.injectionMouldToolStripMenuItem4.Text = "Injection Mould";
            this.injectionMouldToolStripMenuItem4.Click += new System.EventHandler(this.injectionMouldToolStripMenuItem4_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tvMain);
            this.splitContainer2.Size = new System.Drawing.Size(1311, 572);
            this.splitContainer2.SplitterDistance = 118;
            this.splitContainer2.TabIndex = 0;
            // 
            // tvMain
            // 
            this.tvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvMain.Location = new System.Drawing.Point(0, 0);
            this.tvMain.Name = "tvMain";
            treeNode1.Name = "IMSpecificationDataEntry";
            treeNode1.Text = "Product Specification";
            treeNode2.Name = "IMSpecificationDataEntryRoot";
            treeNode2.Text = "Product Data Entry";
            treeNode3.Name = "ProductDetails";
            treeNode3.Text = "Product Details";
            treeNode4.Name = "MaterialComp";
            treeNode4.Text = "Material Composition";
            treeNode5.Name = "MasterBatchComp";
            treeNode5.Text = "MasterBatch Composition";
            treeNode6.Name = "MachinePref";
            treeNode6.Text = "Machine Preference";
            treeNode7.Name = "CustomerPriceQty";
            treeNode7.Text = "Customer Price Qty";
            treeNode8.Name = "MouldSpecification";
            treeNode8.Text = "Mould Specification";
            treeNode9.Name = "QualityControl";
            treeNode9.Text = "Quality Control";
            treeNode10.Name = "Packaging";
            treeNode10.Text = "Packaging";
            treeNode11.Name = "TableMaintenance";
            treeNode11.Text = "Data Table Maintenance";
            this.tvMain.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode11});
            this.tvMain.Size = new System.Drawing.Size(118, 572);
            this.tvMain.TabIndex = 0;
            this.tvMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvMain_MouseDown);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1311, 606);
            this.Controls.Add(this.splitContainer1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Mould Specification";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblProudct;
        private System.Windows.Forms.ComboBox cboProduct;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mouldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blowMouldToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem injectionMouldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mouldToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem blowMouldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem polymerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem machineToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView tvMain;
        private System.Windows.Forms.ToolStripMenuItem extrusionMouldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem masterBatchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fixedCostsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem materialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem materialTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem accessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem injectionMouldToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem injectionMouldToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem blowMouldToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem blowMouldToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem injectionMouldToolStripMenuItem3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboCustomer;
        private System.Windows.Forms.ToolStripSeparator suppliersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem suppliersToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem extrusionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blowMouldingSpecificationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem injectionMouldToolStripMenuItem4;
    }
}