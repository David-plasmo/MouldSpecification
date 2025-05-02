namespace MouldSpecification
{
    partial class QCDataEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QCDataEntry));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnAddNew = new System.Windows.Forms.ToolStripButton();
            this.tsbtnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnCancel = new System.Windows.Forms.ToolStripButton();
            this.tsbtnAccept = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.productSpecificationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productPackagingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.qCInstructionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.attachedDocumentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gpQCInstruction = new System.Windows.Forms.GroupBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btnQCInstructionNewRow = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.lblQCInstructionNewRow = new System.Windows.Forms.Label();
            this.dgvQCInstruction = new System.Windows.Forms.DataGridView();
            this.gpGeneral = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblITEMDESC = new System.Windows.Forms.Label();
            this.lblITEMNMBR = new System.Windows.Forms.Label();
            this.txtITEMNMBR = new System.Windows.Forms.TextBox();
            this.lblCUSTNAME = new System.Windows.Forms.Label();
            this.txtITEMDESC = new System.Windows.Forms.TextBox();
            this.txtCustomer = new System.Windows.Forms.TextBox();
            this.btnReport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.gpQCInstruction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQCInstruction)).BeginInit();
            this.gpGeneral.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.LightBlue;
            this.splitContainer1.Panel1.Controls.Add(this.bindingNavigator1);
            this.splitContainer1.Panel1.Controls.Add(this.menuStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.AntiqueWhite;
            this.splitContainer1.Panel2.Controls.Add(this.gpQCInstruction);
            this.splitContainer1.Panel2.Controls.Add(this.gpGeneral);
            this.splitContainer1.Panel2.Margin = new System.Windows.Forms.Padding(11, 0, 0, 0);
            this.splitContainer1.Size = new System.Drawing.Size(1599, 1368);
            this.splitContainer1.SplitterDistance = 156;
            this.splitContainer1.SplitterWidth = 7;
            this.splitContainer1.TabIndex = 1;
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = null;
            this.bindingNavigator1.AutoSize = false;
            this.bindingNavigator1.BackColor = System.Drawing.Color.LightBlue;
            this.bindingNavigator1.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator1.DeleteItem = null;
            this.bindingNavigator1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bindingNavigator1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.tsbtnAddNew,
            this.tsbtnDelete,
            this.toolStripSeparator1,
            this.tsbtnCancel,
            this.tsbtnAccept});
            this.bindingNavigator1.Location = new System.Drawing.Point(0, 102);
            this.bindingNavigator1.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator1.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator1.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator1.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.bindingNavigator1.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator1.Size = new System.Drawing.Size(1599, 54);
            this.bindingNavigator1.TabIndex = 0;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(54, 49);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.AutoSize = false;
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(24, 32);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.AutoSize = false;
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(24, 32);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 54);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(41, 31);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 54);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.AutoSize = false;
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(24, 32);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.AutoSize = false;
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(24, 32);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 54);
            // 
            // tsbtnAddNew
            // 
            this.tsbtnAddNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAddNew.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAddNew.Image")));
            this.tsbtnAddNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAddNew.Name = "tsbtnAddNew";
            this.tsbtnAddNew.Size = new System.Drawing.Size(34, 49);
            this.tsbtnAddNew.Text = "Add";
            this.tsbtnAddNew.ToolTipText = "Add New";
            // 
            // tsbtnDelete
            // 
            this.tsbtnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnDelete.Image")));
            this.tsbtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDelete.Name = "tsbtnDelete";
            this.tsbtnDelete.Size = new System.Drawing.Size(34, 49);
            this.tsbtnDelete.Text = "Delete";
            this.tsbtnDelete.ToolTipText = "Delete";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 54);
            // 
            // tsbtnCancel
            // 
            this.tsbtnCancel.AutoSize = false;
            this.tsbtnCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnCancel.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnCancel.Image")));
            this.tsbtnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnCancel.Name = "tsbtnCancel";
            this.tsbtnCancel.Size = new System.Drawing.Size(32, 32);
            this.tsbtnCancel.Text = "Cancel";
            // 
            // tsbtnAccept
            // 
            this.tsbtnAccept.AutoSize = false;
            this.tsbtnAccept.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAccept.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAccept.Image")));
            this.tsbtnAccept.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAccept.Name = "tsbtnAccept";
            this.tsbtnAccept.Size = new System.Drawing.Size(32, 32);
            this.tsbtnAccept.Text = "Accept";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.LightBlue;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.productSpecificationToolStripMenuItem,
            this.productPackagingToolStripMenuItem,
            this.qCInstructionsToolStripMenuItem,
            this.attachedDocumentsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(6, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1599, 33);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // productSpecificationToolStripMenuItem
            // 
            this.productSpecificationToolStripMenuItem.Checked = true;
            this.productSpecificationToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.productSpecificationToolStripMenuItem.Name = "productSpecificationToolStripMenuItem";
            this.productSpecificationToolStripMenuItem.Size = new System.Drawing.Size(195, 29);
            this.productSpecificationToolStripMenuItem.Text = "Product Specification";
            this.productSpecificationToolStripMenuItem.Click += new System.EventHandler(this.productSpecificationToolStripMenuItem_Click);
            // 
            // productPackagingToolStripMenuItem
            // 
            this.productPackagingToolStripMenuItem.Name = "productPackagingToolStripMenuItem";
            this.productPackagingToolStripMenuItem.Size = new System.Drawing.Size(175, 29);
            this.productPackagingToolStripMenuItem.Text = "Product Packaging";
            this.productPackagingToolStripMenuItem.Click += new System.EventHandler(this.productPackagingToolStripMenuItem_Click);
            // 
            // qCInstructionsToolStripMenuItem
            // 
            this.qCInstructionsToolStripMenuItem.Name = "qCInstructionsToolStripMenuItem";
            this.qCInstructionsToolStripMenuItem.Size = new System.Drawing.Size(150, 29);
            this.qCInstructionsToolStripMenuItem.Text = "QC Instructions";
            this.qCInstructionsToolStripMenuItem.Click += new System.EventHandler(this.qCInstructionsToolStripMenuItem_Click);
            // 
            // attachedDocumentsToolStripMenuItem
            // 
            this.attachedDocumentsToolStripMenuItem.Name = "attachedDocumentsToolStripMenuItem";
            this.attachedDocumentsToolStripMenuItem.Size = new System.Drawing.Size(195, 29);
            this.attachedDocumentsToolStripMenuItem.Text = "Attached Documents";
            this.attachedDocumentsToolStripMenuItem.Click += new System.EventHandler(this.attachedDocumentsToolStripMenuItem_Click);
            // 
            // gpQCInstruction
            // 
            this.gpQCInstruction.Controls.Add(this.splitContainer2);
            this.gpQCInstruction.Location = new System.Drawing.Point(0, 166);
            this.gpQCInstruction.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.gpQCInstruction.Name = "gpQCInstruction";
            this.gpQCInstruction.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.gpQCInstruction.Size = new System.Drawing.Size(1296, 1015);
            this.gpQCInstruction.TabIndex = 22;
            this.gpQCInstruction.TabStop = false;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(6, 28);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.btnQCInstructionNewRow);
            this.splitContainer2.Panel1.Controls.Add(this.label12);
            this.splitContainer2.Panel1.Controls.Add(this.lblQCInstructionNewRow);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dgvQCInstruction);
            this.splitContainer2.Size = new System.Drawing.Size(1284, 981);
            this.splitContainer2.SplitterDistance = 46;
            this.splitContainer2.SplitterWidth = 2;
            this.splitContainer2.TabIndex = 0;
            // 
            // btnQCInstructionNewRow
            // 
            this.btnQCInstructionNewRow.Enabled = false;
            this.btnQCInstructionNewRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQCInstructionNewRow.Location = new System.Drawing.Point(810, 2);
            this.btnQCInstructionNewRow.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnQCInstructionNewRow.Name = "btnQCInstructionNewRow";
            this.btnQCInstructionNewRow.Size = new System.Drawing.Size(24, 26);
            this.btnQCInstructionNewRow.TabIndex = 52;
            this.btnQCInstructionNewRow.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(4, 4);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(169, 24);
            this.label12.TabIndex = 54;
            this.label12.Text = "QC INSTRUCTION";
            // 
            // lblQCInstructionNewRow
            // 
            this.lblQCInstructionNewRow.AutoSize = true;
            this.lblQCInstructionNewRow.Enabled = false;
            this.lblQCInstructionNewRow.Location = new System.Drawing.Point(874, 4);
            this.lblQCInstructionNewRow.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblQCInstructionNewRow.Name = "lblQCInstructionNewRow";
            this.lblQCInstructionNewRow.Size = new System.Drawing.Size(132, 24);
            this.lblQCInstructionNewRow.TabIndex = 53;
            this.lblQCInstructionNewRow.Text = "Add New Row";
            // 
            // dgvQCInstruction
            // 
            this.dgvQCInstruction.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvQCInstruction.ColumnHeadersHeight = 4;
            this.dgvQCInstruction.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvQCInstruction.ColumnHeadersVisible = false;
            this.dgvQCInstruction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvQCInstruction.Location = new System.Drawing.Point(0, 0);
            this.dgvQCInstruction.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvQCInstruction.Name = "dgvQCInstruction";
            this.dgvQCInstruction.RowHeadersVisible = false;
            this.dgvQCInstruction.RowHeadersWidth = 4;
            this.dgvQCInstruction.RowTemplate.Height = 37;
            this.dgvQCInstruction.Size = new System.Drawing.Size(1284, 933);
            this.dgvQCInstruction.TabIndex = 31;
            // 
            // gpGeneral
            // 
            this.gpGeneral.Controls.Add(this.tableLayoutPanel1);
            this.gpGeneral.Location = new System.Drawing.Point(6, 0);
            this.gpGeneral.Margin = new System.Windows.Forms.Padding(0);
            this.gpGeneral.Name = "gpGeneral";
            this.gpGeneral.Padding = new System.Windows.Forms.Padding(0);
            this.gpGeneral.Size = new System.Drawing.Size(862, 174);
            this.gpGeneral.TabIndex = 0;
            this.gpGeneral.TabStop = false;
            this.gpGeneral.Text = "GENERAL";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.95014F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.04986F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 258F));
            this.tableLayoutPanel1.Controls.Add(this.lblITEMDESC, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblITEMNMBR, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtITEMNMBR, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblCUSTNAME, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtITEMDESC, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtCustomer, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnReport, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 42);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(808, 113);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblITEMDESC
            // 
            this.lblITEMDESC.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblITEMDESC.AutoSize = true;
            this.lblITEMDESC.Location = new System.Drawing.Point(0, 43);
            this.lblITEMDESC.Margin = new System.Windows.Forms.Padding(0);
            this.lblITEMDESC.Name = "lblITEMDESC";
            this.lblITEMDESC.Padding = new System.Windows.Forms.Padding(11, 0, 0, 0);
            this.lblITEMDESC.Size = new System.Drawing.Size(185, 24);
            this.lblITEMDESC.TabIndex = 8;
            this.lblITEMDESC.Text = "Product Description";
            // 
            // lblITEMNMBR
            // 
            this.lblITEMNMBR.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblITEMNMBR.AutoSize = true;
            this.lblITEMNMBR.Location = new System.Drawing.Point(0, 6);
            this.lblITEMNMBR.Margin = new System.Windows.Forms.Padding(0);
            this.lblITEMNMBR.Name = "lblITEMNMBR";
            this.lblITEMNMBR.Padding = new System.Windows.Forms.Padding(11, 0, 0, 0);
            this.lblITEMNMBR.Size = new System.Drawing.Size(137, 24);
            this.lblITEMNMBR.TabIndex = 6;
            this.lblITEMNMBR.Text = "Product Code";
            // 
            // txtITEMNMBR
            // 
            this.txtITEMNMBR.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtITEMNMBR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtITEMNMBR.Location = new System.Drawing.Point(208, 4);
            this.txtITEMNMBR.Margin = new System.Windows.Forms.Padding(0);
            this.txtITEMNMBR.MaxLength = 31;
            this.txtITEMNMBR.Name = "txtITEMNMBR";
            this.txtITEMNMBR.ReadOnly = true;
            this.txtITEMNMBR.Size = new System.Drawing.Size(339, 29);
            this.txtITEMNMBR.TabIndex = 0;
            // 
            // lblCUSTNAME
            // 
            this.lblCUSTNAME.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCUSTNAME.AutoSize = true;
            this.lblCUSTNAME.Location = new System.Drawing.Point(0, 81);
            this.lblCUSTNAME.Margin = new System.Windows.Forms.Padding(0);
            this.lblCUSTNAME.Name = "lblCUSTNAME";
            this.lblCUSTNAME.Padding = new System.Windows.Forms.Padding(11, 0, 0, 0);
            this.lblCUSTNAME.Size = new System.Drawing.Size(102, 24);
            this.lblCUSTNAME.TabIndex = 10;
            this.lblCUSTNAME.Text = "Customer";
            // 
            // txtITEMDESC
            // 
            this.txtITEMDESC.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtITEMDESC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtITEMDESC.Location = new System.Drawing.Point(208, 41);
            this.txtITEMDESC.Margin = new System.Windows.Forms.Padding(0);
            this.txtITEMDESC.MaxLength = 101;
            this.txtITEMDESC.Name = "txtITEMDESC";
            this.txtITEMDESC.ReadOnly = true;
            this.txtITEMDESC.Size = new System.Drawing.Size(339, 29);
            this.txtITEMDESC.TabIndex = 9;
            // 
            // txtCustomer
            // 
            this.txtCustomer.Location = new System.Drawing.Point(208, 74);
            this.txtCustomer.Margin = new System.Windows.Forms.Padding(0);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.ReadOnly = true;
            this.txtCustomer.Size = new System.Drawing.Size(338, 29);
            this.txtCustomer.TabIndex = 13;
            // 
            // btnReport
            // 
            this.btnReport.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnReport.Location = new System.Drawing.Point(602, 0);
            this.btnReport.Margin = new System.Windows.Forms.Padding(0);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(152, 37);
            this.btnReport.TabIndex = 29;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = true;
            // 
            // QCDataEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1599, 1368);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "QCDataEntry";
            this.Text = "QC Instructions";
            this.Load += new System.EventHandler(this.QCDataEntry_Load);
            this.Shown += new System.EventHandler(this.QCDataEntry_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gpQCInstruction.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvQCInstruction)).EndInit();
            this.gpGeneral.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton tsbtnAddNew;
        private System.Windows.Forms.ToolStripButton tsbtnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbtnCancel;
        private System.Windows.Forms.ToolStripButton tsbtnAccept;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem productSpecificationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productPackagingToolStripMenuItem;
        private System.Windows.Forms.GroupBox gpGeneral;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblITEMDESC;
        private System.Windows.Forms.Label lblITEMNMBR;
        private System.Windows.Forms.TextBox txtITEMNMBR;
        private System.Windows.Forms.Label lblCUSTNAME;
        private System.Windows.Forms.TextBox txtITEMDESC;
        private System.Windows.Forms.TextBox txtCustomer;
        private System.Windows.Forms.ToolStripMenuItem qCInstructionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem attachedDocumentsToolStripMenuItem;
        private System.Windows.Forms.GroupBox gpQCInstruction;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dgvQCInstruction;
        private System.Windows.Forms.Button btnQCInstructionNewRow;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblQCInstructionNewRow;
        private System.Windows.Forms.Button btnReport;
    }
}