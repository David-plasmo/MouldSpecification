using MouldSpecification.Properties;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static Utils.DrawingUtils;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using Point = System.Drawing.Point;
using Font = System.Drawing.Font;
using Rectangle = System.Drawing.Rectangle;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using Microsoft.SqlServer.Management.Smo.Agent;


namespace MouldSpecification
{
    public partial class PackagingDataEntry : Form
    {
        //identifiers for last edited product and customer
        public int? LastItemID { get; set; }
        public int? LastCustomerID { get; set; }
        public bool CustomerFilterOn { get; set; }

        //allows navigation to another form via menustrip
        public string NextForm { get; set; }

        bool ChangedByCode = false;  //forces exit to SelectedIndexChanged events for ToolStripBar combobox controls

        int maxRows = 3;  //maximum rows for assembly instruction datagrid 

        ToolStripLabel tslCompany;
        ToolStripComboBox tscboCompany;
        ToolStripLabel tslCode;
        ToolStripComboBox tscboCode;
        ToolStripLabel tslProduct;
        ToolStripComboBox tscboProduct;

        DataSet dsPackaging;
        BindingSource bsManItems, bsProductGradeItem, bsCustomerProducts, bsCustomer,
            bsPackaging, bsPackingImage, bsPackingInstruction, bsReworkInstruction,
            //bsItemPackaging,
            bsCartonPackaging, bsPalletPackaging,
            bsAssemblyInstructionPivot;

        bool ReworkExpanded = true;
        bool AssemblyExpanded = true;
        Point assemblyLoc;

        public PackagingDataEntry(int? lastItemID, int? lastCustomerID, bool customerFilterOn = false)
        {
            InitializeComponent();
            LastItemID = lastItemID;
            LastCustomerID = lastCustomerID;
            CustomerFilterOn = customerFilterOn;
            NextForm = this.Name;

            this.SuspendLayout();
            tslCompany = new ToolStripLabel() { Text = "Company" };
            tslCode = new ToolStripLabel() { Text = "Code" };
            tslProduct = new ToolStripLabel() { Text = "Product" };
            tscboCompany = new ToolStripComboBox();
            tscboCode = new ToolStripComboBox();
            tscboProduct = new ToolStripComboBox();
            //tscboEntryForm = new ToolStripComboBox() { Text = "Product Specification" }; 
            //tsbtnReport = new ToolStripButton() { Text = "Report" };

            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                tslCompany,
                tscboCompany,
                tslCode,
                tscboCode,
                tslProduct,
                tscboProduct
                //tscboEntryForm,
                //tsbtnReport
            });

            tsbtnAccept.Click += tsbtnAccept_Click;
            tsbtnCancel.Click += tsbtnCancel_Click;
            //tsbtnReload.Click += tsbtnReload_Click;
            //btnReport.Click += tsbtnReport_Click;

            foreach (RowStyle style in this.tableLayoutPanel1.RowStyles)
            {
                if (style.SizeType == SizeType.Absolute)
                    style.Height = p96H(19);
            }
            foreach (RowStyle style in this.tableLayoutPanel2.RowStyles)
            {
                if (style.SizeType == SizeType.Absolute)
                    style.Height = p96H(19);
            }
            foreach (RowStyle style in this.tableLayoutPanel3.RowStyles)
            {
                if (style.SizeType == SizeType.Absolute)
                    style.Height = p96H(19);
            }
            foreach (RowStyle style in this.tableLayoutPanel4.RowStyles)
            {
                if (style.SizeType == SizeType.Absolute)
                    style.Height = p96H(19);
            }
            this.ResumeLayout();
        }


        public PackagingDataEntry()
        {

        }

        private void tsbtnAccept_Click(object sender, EventArgs e)
        {
            DoSave();
            this.DialogResult = DialogResult.Retry;
            this.Close();
        }

        private void tsbtnCancel_Click(object sender, EventArgs e)
        {

            //bsPackaging.CancelEdit(); 
            bsPackingImage.CancelEdit();
            bsPackingInstruction.CancelEdit();
            bsReworkInstruction.CancelEdit();
            bsPackaging.CancelEdit();
            bsCartonPackaging.CancelEdit();
            bsPalletPackaging.CancelEdit();
            bsAssemblyInstructionPivot.CancelEdit();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void PackagingDataEntry_Load(object sender, EventArgs e)
        {
            //tsbtnDelete.Click += tsbtnDelete_Click;
            //tsbtnAddNew.Click += tsbtnAddNew_Click;
            this.Shown += PackagingDataEntry_Shown;
            tsbtnDelete.Enabled = false;
            tsbtnAddNew.Enabled = false;
            btnReport.Enabled = false;
            //lblItemID.Visible = false;

            tscboCompany.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            tscboCompany.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            tscboCompany.DropDownHeight = 400;
            tscboCompany.DropDownWidth = p96W(250);
            tscboCompany.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            tscboCompany.IntegralHeight = false;
            tscboCompany.MaxDropDownItems = 9;
            tscboCompany.MergeAction = System.Windows.Forms.MergeAction.Insert;
            tscboCompany.Name = "tscboCompany";
            tscboCompany.Size = new System.Drawing.Size(p96W(250), p96H(25));
            tscboCompany.Sorted = true;

            tscboCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            tscboCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            tscboCode.DropDownHeight = 400;
            tscboCode.DropDownWidth = p96W(150);
            tscboCode.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            tscboCode.IntegralHeight = false;
            tscboCode.MaxDropDownItems = 9;
            tscboCode.MergeAction = System.Windows.Forms.MergeAction.Insert;
            tscboCode.Name = "tscboCode";
            tscboCode.Size = new System.Drawing.Size(p96W(150), p96H(25));
            tscboCode.Sorted = true;

            tscboProduct.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            tscboProduct.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            tscboProduct.DropDownHeight = 400;
            tscboProduct.DropDownWidth = p96W(300);
            tscboProduct.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            tscboProduct.IntegralHeight = false;
            tscboProduct.MaxDropDownItems = 9;
            tscboProduct.MergeAction = System.Windows.Forms.MergeAction.Insert;
            tscboProduct.Name = "tscboProduct";
            tscboProduct.Size = new System.Drawing.Size(p96W(300), p96H(25));
            tscboProduct.Sorted = true;

            btnReport.Click += btnReport_Click;

            System.Drawing.Image image = Properties.Resources.minus;
            btnShowAssembly.Size = new Size(p96W(20), p96H(20));
            btnShowRework.Image = RescaleImage((Bitmap)image, p96W(btnShowRework.Width), p96H(btnShowRework.Height));

            btnShowRework.Size = new Size(p96H(20), p96H(20));
            btnShowRework.Click += btnShowRework_Click;
            btnShowAssembly.Image = RescaleImage((Bitmap)image, p96W(btnShowRework.Width), p96H(btnShowRework.Height));
            btnShowAssembly.Click += btnShowAssembly_Click;
            assemblyLoc = gpAssemblyInstructions.Location;

            image = Properties.Resources.NewRow;
            btnAssemblyImageNewRow.Size = new Size(p96W(24), p96H(20));
            btnAssemblyImageNewRow.Image = RescaleImage((Bitmap)image, btnAssemblyImageNewRow.Width, btnAssemblyImageNewRow.Height);
            btnAssemblyImageNewRow.Click += btnAssemblyImageNewRow_Click;

            gpAssemblyInstructions.Size = new Size(p96W(707), p96H(550));

            GetDataSets();
            BindControls();
            //SetFormState();
        }

        private void btnShowRework_Click(object sender, EventArgs e)
        {
            SetReworkInstruction();
        }

        private void SetReworkInstruction()
        {
            try
            {
                int leftg = assemblyLoc.X;
                int topg = assemblyLoc.Y;

                ReworkExpanded = !ReworkExpanded;
                System.Drawing.Image image = (ReworkExpanded)
                    ? Properties.Resources.minus
                    : Properties.Resources.plus;
                btnShowRework.Image = RescaleImage((Bitmap)image, p96W(btnShowRework.Width), p96H(btnShowRework.Height));

                //expand/collapse Rework control group
                if (!ReworkExpanded)
                {
                    //MessageBox.Show("todo: collapse Rework");
                    dgvReworkInstruction.Visible = false;
                    gpReworkInstructions.Height = p96H(100);
                    splitContainer5.SplitterDistance = p96H(25);
                    topg = assemblyLoc.Y - p96H(70);
                }
                else
                {
                    //MessageBox.Show("todo: expand Rework");
                    dgvReworkInstruction.Visible = true;
                    gpReworkInstructions.Height = p96H(250);
                    splitContainer5.SplitterDistance = p96H(25);
                    topg = assemblyLoc.Y;
                }
                gpAssemblyInstructions.Location = new Point(leftg, topg);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnShowAssembly_Click(object sender, EventArgs e)
        {
            SetAssemblyInstruction();
        }

        private void SetAssemblyInstruction()
        {
            try
            {
                AssemblyExpanded = !AssemblyExpanded;
                Image image = (AssemblyExpanded)
                    ? Properties.Resources.minus
                    : Properties.Resources.plus;
                btnShowAssembly.Image = RescaleImage((Bitmap)image, btnShowRework.Width, btnShowRework.Height);

                //expand/collapse Assembly control group
                if (!AssemblyExpanded)
                {
                    //MessageBox.Show("todo: collapse Assembly");
                    dgvAssemblyInstruction.Visible = false;
                    btnAssemblyImageNewRow.Visible = false;
                    lblAddImageRow.Visible = false;
                    //gpAssemblyInstructions.Height = p96H(100);
                }
                else
                {
                    //MessageBox.Show("todo: expand Assembly");
                    dgvAssemblyInstruction.Visible = true;
                    btnAssemblyImageNewRow.Visible = true;
                    lblAddImageRow.Visible = true;
                    gpAssemblyInstructions.Height = p96H(300);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }


        private void BindControls()
        {
            try
            {
                //????
                //dsPackaging.AcceptChanges();
                //?????
                //create binding sources
                bsManItems = new BindingSource
                {
                    DataSource = dsPackaging,
                    DataMember = "MAN_Items",
                    Sort = "ITEMDESC ASC"
                };

                bsProductGradeItem = new BindingSource();
                bsProductGradeItem.DataSource = dsPackaging.Relations["ProductGradeItem"].ParentTable;

                bsCustomerProducts = new BindingSource();
                bsCustomerProducts.DataSource = dsPackaging.Tables["CustomerProduct"];

                bsCustomer = new BindingSource();
                bsCustomer.DataSource = dsPackaging.Tables["Customer"];

                bsPackaging = new BindingSource();
                bsPackaging.DataSource = dsPackaging.Tables["Packaging"];
                bsPackaging.AddingNew += bsPackaging_AddingNew;

                //bsCustomerCustProducts = new BindingSource();
                //bsCustomerCustProducts.DataSource = dsPackaging.Relations["CustomerCustProducts"].ParentTable;

                //bsItemPackaging = new BindingSource();
                //bsItemPackaging.DataSource = dsPackaging.Relations["ItemPackaging"].ChildTable;
                //bsItemPackaging.AddingNew += bsItemPackaging_AddingNew;

                //Navigation toolbar
                bindingNavigator1.BindingSource = new BindingSource();
                bindingNavigator1.BindingSource = bsManItems;
                bsManItems.CurrentChanged += bsManItems_CurrentChanged;

                ////Toolbar Customer dropdown
                //DataTable dt = dsPackaging.Tables["Customer"];
                //tscboCompany.ComboBox.DataSource = dt;
                //tscboCompany.ComboBox.DisplayMember = "CUSTNAME";
                //tscboCompany.ComboBox.ValueMember = "CustomerID";                

                //dt = dsPackaging.Tables["Product"];               
                //tscboProduct.ComboBox.DataSource = dt;
                //tscboProduct.ComboBox.DisplayMember = "ITEMDESC";
                //tscboProduct.ComboBox.ValueMember = "ItemID";
                //tscboProduct.ComboBox.SelectedIndexChanged += tscboProduct_SelectedIndexChanged;


                //dt.RowChanging += Dt_RowChanging;
                //dt.TableNewRow += Dt_TableNewRow;

                txtITEMNMBR.DataBindings.Add(new Binding("Text", bsManItems, "ITEMNMBR"));
                txtITEMDESC.DataBindings.Add(new Binding("Text", bsManItems, "ITEMDESC"));
                picImageFile.DataBindings.Add(new Binding("ImageLocation", bsManItems, "ImageFile"));
                picLabelIcon.DataBindings.Add(new Binding("ImageLocation", bsProductGradeItem, "LabelIcon"));
                txtProductCategory.DataBindings.Add(new Binding("Text", bsProductGradeItem, "Description"));
                //lblItemID.DataBindings.Add(new Binding("Text", bsManItems, "ItemID"));
                lblProductCategory.DataBindings.Add(new Binding("Text", bsManItems, "GradeID"));
                txtCustomer.DataBindings.Add(new Binding("Text", bsCustomer, "CUSTNAME"));

                chkPackedInCtn.DataBindings.Add(new Binding("Checked", bsPackaging, "PackedInCtn"));
                chkLiner.DataBindings.Add(new Binding("Checked", bsPackaging, "Liner"));
                chkInnerBag.DataBindings.Add(new Binding("Checked", bsPackaging, "InnerBag"));
                chkPackedOnPallet.DataBindings.Add(new Binding("Checked", bsPackaging, "PackedOnPallet"));
                chkPalletCover.DataBindings.Add(new Binding("Checked", bsPackaging, "PalletCover"));
                txtCtnQty.DataBindings.Add(new Binding("Text", bsPackaging, "CtnQty"));
                txtBagQty.DataBindings.Add(new Binding("Text", bsPackaging, "BagQty"));
                txtPalQty.DataBindings.Add(new Binding("Text", bsPackaging, "PalQty"));
                txtCtnsPerPallet.DataBindings.Add(new Binding("Text", bsPackaging, "CtnsPerPallet"));

                //Carton packaging combo dropdown list
                cboCtnType.DataSource = dsPackaging.Tables["CartonPackaging"];
                cboCtnType.ValueMember = "CtnID";
                cboCtnType.DisplayMember = "CartonType";

                //databinding for CartonType 
                bsCartonPackaging = new BindingSource();
                bsCartonPackaging.DataSource = dsPackaging.Relations["ctnPackagingPackaging"].ParentTable;
                cboCtnType.DataBindings.Add(new Binding("SelectedValue", bsPackaging, "CtnID"));

                //Pallet type dropdown list               
                cboPalletType.DataSource = dsPackaging.Tables["Pallet"];
                cboPalletType.ValueMember = "PalletID";
                cboPalletType.DisplayMember = "Pallet";

                //databinding for Pallet Type 
                bsPalletPackaging = new BindingSource();
                bsPalletPackaging.DataSource = dsPackaging.Relations["PalletPackaging"].ParentTable;
                cboPalletType.DataBindings.Add(new Binding("SelectedValue", bsPackaging, "PalletID"));

                //Packing style
                cboPackingStyle.DataSource = dsPackaging.Tables["PackingStyle"];
                cboPackingStyle.ValueMember = "PackingStyle";
                cboPackingStyle.DisplayMember = "PackingStyle";
                cboPackingStyle.DataBindings.Add(new Binding("SelectedValue", bsPackaging, "PackingStyle"));

                //Wrapping type
                cboWrapping.DataSource = dsPackaging.Tables["Wrapping"];
                cboWrapping.ValueMember = "Wrapping";
                cboWrapping.DisplayMember = "Wrapping";
                cboWrapping.DataBindings.Add(new Binding("SelectedValue", bsPackaging, "Wrapping"));

                //Barcode labels
                cboBarcodeLabel.DataSource = dsPackaging.Tables["BarcodeLabel"];
                cboBarcodeLabel.ValueMember = "BarcodeLabel";
                cboBarcodeLabel.DisplayMember = "BarcodeLabel";
                cboBarcodeLabel.DataBindings.Add(new Binding("SelectedValue", bsPackaging, "BarcodeLabel"));

                //label inner bag
                cboLabelInnerBag.DataSource = dsPackaging.Tables["LabelInnerBag"];
                cboLabelInnerBag.ValueMember = "LabelInnerBag";
                cboLabelInnerBag.DisplayMember = "LabelInnerBag";
                cboLabelInnerBag.DataBindings.Add(new Binding("SelectedValue", bsPackaging, "LabelInnerBag"));

                //Packing image
                bsPackingImage = new BindingSource();
                bsPackingImage.AddingNew += bsPackingImage_AddingNew;
                bsPackingImage.DataSource = dsPackaging.Tables["PackingImage"];
                dgvPackingImage.DataSource = bsManItems;
                dgvPackingImage.DataMember = "ItemPackingImage";
                //dgvPackingImage.CellPainting += dgvPackingImage_CellPainting;
                dgvPackingImage.DataBindingComplete += dgvPackingImage_DataBindingComplete;
                dgvPackingImage.CellClick += dgvPackingImage_CellClick;
                dgvPackingImage.DataError += dgvPackingImage_DataError;
                FormatPackingImage();

                //Packing instruction
                bsPackingInstruction = new BindingSource();
                bsPackingInstruction.AddingNew += bsPackingInstruction_AddingNew;
                bsPackingInstruction.DataSource = dsPackaging.Tables["PackingInstruction"];
                dgvPackingInstruction.DataSource = bsManItems;
                dgvPackingInstruction.DataMember = "ItemPackingInstruction";
                //dgvPackingInstruction.CellFormatting +=dgvPackingInstruction_CellFormatting;
                //dgvPackingInstruction.CellPainting +=dgvPackingInstruction_CellPainting;
                dgvPackingInstruction.DataBindingComplete += dgvPackingInstruction_DataBindingComplete;
                //dgvPackingInstruction.CellEndEdit += dgvPackingInstruction_CellEndEdit;
                //dgvPackingInstruction.CellClick +=dgvPackingInstruction_CellClick;
                //dgvPackingInstruction.RowStateChanged += dgvPackingInstruction_RowStateChanged;
                dgvPackingInstruction.DataError += dgvPackingInstruction_DataError;
                FormatPackingInstruction();

                //Rework instruction
                bsReworkInstruction = new BindingSource();
                bsReworkInstruction.AddingNew += bsReworkInstruction_AddingNew;
                bsReworkInstruction.DataSource = dsPackaging.Tables["ReworkInstruction"];
                dgvReworkInstruction.DataSource = bsManItems;
                dgvReworkInstruction.DataMember = "ItemReworkInstruction";
                dgvReworkInstruction.DataBindingComplete += dgvReworkInstruction_DataBindingComplete;
                dgvReworkInstruction.DataError += dgvReworkInstruction_DataError;
                FormatReworkInstruction();

                //Assembly instruction
                bsAssemblyInstructionPivot = new BindingSource();
                bsAssemblyInstructionPivot.AddingNew += bsAssemblyInstructionPivot_AddingNew;
                bsAssemblyInstructionPivot.DataSource = dsPackaging.Tables["AssemblyInstructionPivot"];
                dgvAssemblyInstruction.DataSource = bsManItems;
                dgvAssemblyInstruction.DataMember = "ItemAssemblyInstruction";
                dgvAssemblyInstruction.DataBindingComplete += dgvAssemblyInstruction_DataBindingComplete;
                dgvPackingInstruction.DataError += dgvAssemblyInstruction_DataError;
                FormatAssemblyInstruction();

            }
            catch (Exception ex)
            {

            }
        }

        //private void bsItemPackaging_AddingNew(object sender, AddingNewEventArgs e)
        //{            
        //    try
        //    {
        //        DataRowView rowView = (DataRowView)this.bsManItems.Current;
        //        DataRow row = rowView.Row;
        //        //MessageBox.Show(row["ItemID"].ToString());
        //        int itemID = (int)row["ItemID"];
        //        DataTable dt = (DataTable)bsItemPackaging.DataSource;
        //        dt.Columns["ItemID"].DefaultValue = itemID;
        //        //create a dummy primary key
        //        dt.Columns["PackingID"].DefaultValue = bsItemPackaging.Count + 1000;
        //    }
        //    catch (Exception ex) { MessageBox.Show(ex.Message); }
        //}

        private void bsPackaging_AddingNew(object sender, AddingNewEventArgs e)
        {
            try
            {
                DataRowView rowView = (DataRowView)this.bsManItems.Current;
                DataRow row = rowView.Row;
                //MessageBox.Show(row["ItemID"].ToString());
                int itemID = (int)row["ItemID"];
                DataTable dt = (DataTable)bsPackaging.DataSource;
                dt.Columns["ItemID"].DefaultValue = itemID;
                dt.Columns["PackingID"].DefaultValue = dt.Rows.Count + 1000;  //dummy primary key
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void bsPackingImage_AddingNew(object sender, AddingNewEventArgs e)
        {
            try
            {
                DataRowView rowView = (DataRowView)this.bsManItems.Current;
                DataRow row = rowView.Row;
                //MessageBox.Show(row["ItemID"].ToString());
                int itemID = (int)row["ItemID"];
                DataTable dt = (DataTable)bsPackingImage.DataSource;
                dt.Columns["ItemID"].DefaultValue = itemID;
            }
            catch (Exception ex) { }
        }

        private void bsPackingInstruction_AddingNew(object sender, AddingNewEventArgs e)
        {
            try
            {
                DataRowView rowView = (DataRowView)this.bsManItems.Current;
                DataRow row = rowView.Row;
                //MessageBox.Show(row["ItemID"].ToString());
                int itemID = (int)row["ItemID"];
                DataTable dt = (DataTable)bsPackingInstruction.DataSource;
                dt.Columns["ItemID"].DefaultValue = itemID;
            }
            catch (Exception ex) { }
        }

        private void bsAssemblyInstructionPivot_AddingNew(object sender, AddingNewEventArgs e)
        {
            try
            {
                DataRowView rowView = (DataRowView)this.bsManItems.Current;
                DataRow row = rowView.Row;
                //MessageBox.Show(row["ItemID"].ToString());
                int itemID = (int)row["ItemID"];
                DataTable dt = (DataTable)bsAssemblyInstructionPivot.DataSource;
                dt.Columns["ItemID1"].DefaultValue = itemID;
                dt.Columns["ItemID2"].DefaultValue = itemID;
                dt.Columns["AssemblyInstructionID1"].DefaultValue = -1;
                dt.Columns["AssemblyInstructionID2"].DefaultValue = -1;

                DataTable ct = dsPackaging.Relations["ItemAssemblyInstruction"].ChildTable;
                DataRow[] foundRows = ct.Select("ItemID1 = " + LastItemID.ToString());
                int count = foundRows.Length;
                int instructionNo = (count + 1) * 2 - 1;  //creates sequence of 1,3,5
                dt.Columns["InstructionNo1"].DefaultValue = instructionNo;
                dt.Columns["InstructionNo2"].DefaultValue = instructionNo + 1;

                btnAssemblyImageNewRow.Enabled = count < maxRows;

            }
            catch (Exception ex) { }
        }

        private void bsReworkInstruction_AddingNew(object sender, AddingNewEventArgs e)
        {
            try
            {
                DataRowView rowView = (DataRowView)this.bsManItems.Current;
                DataRow row = rowView.Row;
                //MessageBox.Show(row["ItemID"].ToString());
                int itemID = (int)row["ItemID"];
                DataTable dt = (DataTable)bsReworkInstruction.DataSource;
                dt.Columns["ItemID"].DefaultValue = itemID;
                dt.Columns["InstructionNo"].DefaultValue = dt.Rows.Count;
            }
            catch (Exception ex) { }
        }

        private void FormatPackingImage()
        {
            try
            {
                dgvPackingImage.CellFormatting -= dgvPackingImage_CellFormatting;
                dgvPackingImage.AllowUserToAddRows = false;
                dgvPackingImage.SelectionMode = DataGridViewSelectionMode.CellSelect;
                //dgvPackingImage.Width = p96H(600);
                dgvPackingImage.Height = p96H(100);
                dgvPackingImage.RowHeadersWidth = p96H(26);
                dgvPackingImage.ClearSelection();

                dgvPackingImage.ColumnHeadersHeight = p96H(19);
                dgvPackingImage.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dgvPackingImage.AllowUserToResizeRows = false;
                dgvPackingImage.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

                DataGridViewCellStyle style = dgvPackingImage.ColumnHeadersDefaultCellStyle;
                style.BackColor = Color.RosyBrown;
                style.ForeColor = Color.MidnightBlue;
                style.Font = new Font(dgvPackingImage.Font, FontStyle.Regular);
                dgvPackingImage.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgvPackingImage.ColumnHeadersDefaultCellStyle.BackColor;
                dgvPackingImage.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                dgvPackingImage.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                dgvPackingImage.GridColor = SystemColors.ActiveBorder;
                dgvPackingImage.EnableHeadersVisualStyles = false;
                dgvPackingImage.AutoGenerateColumns = true;

                dgvPackingImage.Columns["PackingImageID"].Visible = false;
                dgvPackingImage.Columns["ItemID"].Visible = false;
                dgvPackingImage.Columns["PackingImageFilepath1"].Visible = false;
                dgvPackingImage.Columns["PackingImageFilepath2"].Visible = false;
                dgvPackingImage.Columns["PackingImageFilepath3"].Visible = false;

                //add image button columns                
                //DataGridViewButtonColumn bc = new DataGridViewButtonColumn();
                DataGridViewImageColumn bc = new DataGridViewImageColumn();
                bc.Name = "btnImage1";
                dgvPackingImage.Columns.Add(bc);

                bc = new DataGridViewImageColumn();
                bc.Name = "btnImage2";
                dgvPackingImage.Columns.Add(bc);

                bc = new DataGridViewImageColumn();
                bc.Name = "btnImage3";
                dgvPackingImage.Columns.Add(bc);

                dgvPackingImage.RowTemplate.Height = p96H(100);
                dgvPackingImage.Columns["btnImage1"].Width = p96W(100);
                dgvPackingImage.Columns["btnImage2"].Width = p96W(100);
                dgvPackingImage.Columns["btnImage3"].Width = p96W(100);

                //create context menu
                dgvPackingImage.CellMouseDown += dgvPackingImage_CellMouseDown;
                dgvPackingImage.CellFormatting += dgvPackingImage_CellFormatting;
                dgvPackingImage.DefaultValuesNeeded += dgvPackingImage_DefaultValuesNeeded;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void dgvPackingImage_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void dgvPackingImage_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Ignore if a column or row header is clicked
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if (e.Button == MouseButtons.Right)
                {
                    DataGridViewCell clickedCell = (sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex];

                    // Here you can do whatever you want with the cell
                    this.dgvPackingImage.CurrentCell = clickedCell;  // Select the clicked cell, for instance

                    // Get mouse position relative to the vehicles grid
                    var relativeMousePosition = dgvPackingImage.PointToClient(Cursor.Position);
                    ContextMenuStrip cms = new ContextMenuStrip();

                    int fp1Index = dgvPackingImage.Columns["PackingImageFilepath1"].Index;
                    int fp2Index = dgvPackingImage.Columns["PackingImageFilepath2"].Index;
                    int fp3Index = dgvPackingImage.Columns["PackingImageFilepath3"].Index;
                    string fp1 = dgvPackingImage.Rows[e.RowIndex].Cells[fp1Index].Value.ToString();
                    string fp2 = dgvPackingImage.Rows[e.RowIndex].Cells[fp2Index].Value.ToString();
                    string fp3 = dgvPackingImage.Rows[e.RowIndex].Cells[fp3Index].Value.ToString();
                    if ((clickedCell.OwningColumn.Name == "btnImage1" && fp1.Length > 0) ||
                        (clickedCell.OwningColumn.Name == "btnImage2" && fp2.Length > 0) ||
                        (clickedCell.OwningColumn.Name == "btnImage3" && fp3.Length > 0))
                    {
                        cms.Items.Add("&Zoom", Resources.zoom, new System.EventHandler(this.Zoom_Click));
                        cms.Items.Add("&Remove", Resources.remove, new System.EventHandler(this.Remove_Click));
                    }
                    cms.Items.Add("&Browse", Resources.browse, new System.EventHandler(this.Browse_Click));
                    cms.Items.Add(new ToolStripSeparator());
                    cms.Items.Add("&Delete Row", Resources.delete, new System.EventHandler(this.Delete_Click));

                    // Show the context menu
                    cms.Show(dgvPackingImage, relativeMousePosition);
                }
            }
        }

        private void Zoom_Click(Object sender, EventArgs e)
        {
            string scName = ((ContextMenuStrip)(((ToolStripMenuItem)sender).Owner)).SourceControl.Name;
            //MessageBox.Show("todo:  Zoom image;  sender: " + scName);

            if (scName == "dgvAssemblyInstruction")
            {
                int fp1Index = dgvAssemblyInstruction.Columns["AssemblyImageFilePath1"].Index;
                int fp2Index = dgvAssemblyInstruction.Columns["AssemblyImageFilePath2"].Index;
                //int fp3Index = dgvAssemblyInstruction.Columns["AssemblyImageFilePath3"].Index;
                string fp1 = dgvAssemblyInstruction.CurrentRow.Cells[fp1Index].Value.ToString();
                string fp2 = dgvAssemblyInstruction.CurrentRow.Cells[fp2Index].Value.ToString();
                //string fp3 = dgvAssemblyInstruction.CurrentRow.Cells[fp3Index].Value.ToString();
                string fp = null;
                if (dgvAssemblyInstruction.CurrentCell.OwningColumn.Name == "AssemblyImage1" && fp1.Length > 0)
                    fp = fp1;
                if (dgvAssemblyInstruction.CurrentCell.OwningColumn.Name == "AssemblyImage2" && fp2.Length > 0)
                    fp = fp2;

                //MessageBox.Show("todo:  Zoom " + fp + ";  sender: " + scName);

                if (System.IO.File.Exists(fp))
                {
                    Image image = GetImage(fp, p96W(1200), p96W(800));
                    PicturePopup pp = new PicturePopup(image);
                    pp.Show(this);
                }
                else
                    MessageBox.Show("file not found: " + fp);
            }
            else if (scName == "dgvPackingImage")
            {
                int fp1Index = dgvPackingImage.Columns["PackingImageFilepath1"].Index;
                int fp2Index = dgvPackingImage.Columns["PackingImageFilepath2"].Index;
                int fp3Index = dgvPackingImage.Columns["PackingImageFilepath3"].Index;
                string fp1 = dgvPackingImage.CurrentRow.Cells[fp1Index].Value.ToString();
                string fp2 = dgvPackingImage.CurrentRow.Cells[fp2Index].Value.ToString();
                string fp3 = dgvPackingImage.CurrentRow.Cells[fp3Index].Value.ToString();
                string fp = null;
                if (dgvPackingImage.CurrentCell.OwningColumn.Name == "btnImage1" && fp1.Length > 0)
                    fp = fp1;
                if (dgvPackingImage.CurrentCell.OwningColumn.Name == "btnImage2" && fp2.Length > 0)
                    fp = fp2;
                if (dgvPackingImage.CurrentCell.OwningColumn.Name == "btnImage3" && fp2.Length > 0)
                    fp = fp3;

                //MessageBox.Show("todo:  Zoom " + fp + "; sender: " + scName);

                if (System.IO.File.Exists(fp))
                {
                    Image image = GetImage(fp, p96W(1200), p96W(800));
                    PicturePopup pp = new PicturePopup(image);
                    pp.Show(this);
                }
                else
                    MessageBox.Show("file not found: " + fp);
            }

        }

        private void Browse_Click(Object sender, EventArgs e)
        {
            string scName = ((ContextMenuStrip)(((ToolStripMenuItem)sender).Owner)).SourceControl.Name;
            //MessageBox.Show("todo:  Browse;  sender: " + scName);
            if (scName == "dgvAssemblyInstruction")
            {
                int fp1Index = dgvAssemblyInstruction.Columns["AssemblyImageFilePath1"].Index;
                int fp2Index = dgvAssemblyInstruction.Columns["AssemblyImageFilePath2"].Index;
                string fp1 = dgvAssemblyInstruction.CurrentRow.Cells[fp1Index].Value.ToString();
                string fp2 = dgvAssemblyInstruction.CurrentRow.Cells[fp2Index].Value.ToString();
                string fieldName = null;
                if (dgvAssemblyInstruction.CurrentCell.OwningColumn.Name == "AssemblyImage1")
                    fieldName = "AssemblyImageFilePath1";
                else if (dgvAssemblyInstruction.CurrentCell.OwningColumn.Name == "AssemblyImage2")
                    fieldName = "AssemblyImageFilePath2";

                //MessageBox.Show("todo:  browse " + fieldName + ";  sender: " + scName);

                OpenFileDialog fdlg = new OpenFileDialog();
                fdlg.Title = "QC Instruction Image Files";
                fdlg.InitialDirectory = @"S:CONSOLIDATED PLASTICS\INJECTION MOULDING\Database\Images\";
                fdlg.Filter = "JPG (*.jpg)|*.jpg|PNG (*.png)|*.png|tif (*.tif)|*.tif|Bitmap (*.bmp)|*.bmp|All files (*.*)|*.*";
                fdlg.FilterIndex = 1;
                fdlg.RestoreDirectory = true;
                if (fdlg.ShowDialog() == DialogResult.OK)
                {
                    string fileSelected = fdlg.FileName;

                    BindingManagerBase bm = this.dgvAssemblyInstruction.BindingContext[this.dgvAssemblyInstruction.DataSource, this.dgvAssemblyInstruction.DataMember];
                    DataRow dr = ((DataRowView)bm.Current).Row;
                    dr[fieldName] = fileSelected;
                    dr.EndEdit();
                    dgvAssemblyInstruction.CurrentCell.Selected = true; //forces repaint 
                }
            }
            else if (scName == "dgvPackingImage")
            {
                int fp1Index = dgvPackingImage.Columns["PackingImageFilepath1"].Index;
                int fp2Index = dgvPackingImage.Columns["PackingImageFilepath2"].Index;
                int fp3Index = dgvPackingImage.Columns["PackingImageFilepath3"].Index;
                string fp1 = dgvPackingImage.CurrentRow.Cells[fp1Index].Value.ToString();
                string fp2 = dgvPackingImage.CurrentRow.Cells[fp2Index].Value.ToString();
                string fp3 = dgvPackingImage.CurrentRow.Cells[fp3Index].Value.ToString();
                string fieldName = null;
                if (dgvPackingImage.CurrentCell.OwningColumn.Name == "btnImage1")
                    fieldName = "PackingImageFilepath1";
                else if (dgvPackingImage.CurrentCell.OwningColumn.Name == "btnImage2")
                    fieldName = "PackingImageFilepath2";
                else if (dgvPackingImage.CurrentCell.OwningColumn.Name == "btnImage3")
                    fieldName = "PackingImageFilepath3";

                //MessageBox.Show("todo:  browse " + fieldName + ";  sender: " + scName);

                OpenFileDialog fdlg = new OpenFileDialog();
                fdlg.Title = "Packaging Image Files";
                fdlg.InitialDirectory = @"S:CONSOLIDATED PLASTICS\INJECTION MOULDING\Database\Images\";
                fdlg.Filter = "JPG (*.jpg)|*.jpg|PNG (*.png)|*.png|tif (*.tif)|*.tif|Bitmap (*.bmp)|*.bmp|All files (*.*)|*.*";
                fdlg.FilterIndex = 1;
                fdlg.RestoreDirectory = true;
                if (fdlg.ShowDialog() == DialogResult.OK)
                {
                    string fileSelected = fdlg.FileName;

                    BindingManagerBase bm = this.dgvPackingImage.BindingContext[this.dgvPackingImage.DataSource, this.dgvPackingImage.DataMember];
                    DataRow dr = ((DataRowView)bm.Current).Row;
                    dr[fieldName] = fileSelected;
                    dr.EndEdit();
                    dgvPackingImage.CurrentCell.Selected = true;  //forces image to repaint
                }
            }
        }
        private void Remove_Click(Object sender, EventArgs e)
        {
            //string scName = ((ContextMenuStrip)(((ToolStripMenuItem)sender).Owner)).SourceControl.Name;
            //MessageBox.Show("todo:  Remove;  sender: " + scName);
            try
            {
                string scName = ((ContextMenuStrip)(((ToolStripMenuItem)sender).Owner)).SourceControl.Name;
                if (scName == "dgvAssemblyInstruction")
                {
                    //MessageBox.Show("todo:  Remove;  sender: " + scName);
                    int fp1Index = dgvAssemblyInstruction.Columns["AssemblyImageFilePath1"].Index;
                    int fp2Index = dgvAssemblyInstruction.Columns["AssemblyImageFilePath2"].Index;
                    //int fp3Index = dgvAssemblyInstruction.Columns["PackingImageFilepath3"].Index;
                    string fp1 = dgvAssemblyInstruction.CurrentRow.Cells[fp1Index].Value.ToString();
                    string fp2 = dgvAssemblyInstruction.CurrentRow.Cells[fp2Index].Value.ToString();
                    //string fp3 = dgvAssemblyInstruction.CurrentRow.Cells[fp3Index].Value.ToString();
                    string fp = null;
                    if (dgvAssemblyInstruction.CurrentCell.OwningColumn.Name == "AssemblyImage1" && fp1.Length > 0)
                    {
                        BindingManagerBase bm = this.dgvAssemblyInstruction.BindingContext[this.dgvAssemblyInstruction.DataSource, this.dgvAssemblyInstruction.DataMember];
                        DataRow dr = ((DataRowView)bm.Current).Row;
                        dr["AssemblyImageFilepath1"] = DBNull.Value;
                        dr.EndEdit();
                        dgvAssemblyInstruction.CurrentCell.Selected = true;  //forces cell repaint
                    }
                    else if (dgvAssemblyInstruction.CurrentCell.OwningColumn.Name == "AssemblyImage2" && fp2.Length > 0)
                    {
                        BindingManagerBase bm = this.dgvAssemblyInstruction.BindingContext[this.dgvAssemblyInstruction.DataSource, this.dgvAssemblyInstruction.DataMember];
                        DataRow dr = ((DataRowView)bm.Current).Row;
                        dr["AssemblyImageFilepath2"] = DBNull.Value;
                        dr.EndEdit();
                        dgvAssemblyInstruction.CurrentCell.Selected = true;  //forces cell repaint
                    }
                }
                else if (scName == "dgvPackingImage")
                {
                    //MessageBox.Show("todo:  Remove;  sender: " + scName);
                    int fp1Index = dgvPackingImage.Columns["PackingImageFilepath1"].Index;
                    int fp2Index = dgvPackingImage.Columns["PackingImageFilepath2"].Index;
                    int fp3Index = dgvPackingImage.Columns["PackingImageFilepath3"].Index;
                    string fp1 = dgvPackingImage.CurrentRow.Cells[fp1Index].Value.ToString();
                    string fp2 = dgvPackingImage.CurrentRow.Cells[fp2Index].Value.ToString();
                    string fp3 = dgvPackingImage.CurrentRow.Cells[fp3Index].Value.ToString();
                    string fp = null;
                    if (dgvPackingImage.CurrentCell.OwningColumn.Name == "btnImage1" && fp1.Length > 0)
                    {
                        BindingManagerBase bm = this.dgvPackingImage.BindingContext[this.dgvPackingImage.DataSource, this.dgvPackingImage.DataMember];
                        DataRow dr = ((DataRowView)bm.Current).Row;
                        dr["PackingImageFilepath1"] = DBNull.Value;
                        dr.EndEdit();
                        dgvPackingImage.CurrentCell.Selected = true;
                    }
                    else if (dgvPackingImage.CurrentCell.OwningColumn.Name == "btnImage2" && fp2.Length > 0)
                    {
                        BindingManagerBase bm = this.dgvPackingImage.BindingContext[this.dgvPackingImage.DataSource, this.dgvPackingImage.DataMember];
                        DataRow dr = ((DataRowView)bm.Current).Row;
                        dr["PackingImageFilepath2"] = DBNull.Value;
                        dr.EndEdit();
                        dgvPackingImage.CurrentCell.Selected = true;
                    }
                    else if (dgvPackingImage.CurrentCell.OwningColumn.Name == "btnImage3" && fp3.Length > 0)
                    {
                        BindingManagerBase bm = this.dgvPackingImage.BindingContext[this.dgvPackingImage.DataSource, this.dgvPackingImage.DataMember];
                        DataRow dr = ((DataRowView)bm.Current).Row;
                        dr["PackingImageFilepath3"] = DBNull.Value;
                        dr.EndEdit();
                        dgvPackingImage.CurrentCell.Selected = true;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Delete_Click(Object sender, EventArgs e)
        {
            //string scName = ((ContextMenuStrip)(((ToolStripMenuItem)sender).Owner)).SourceControl.Name;
            //MessageBox.Show("Are you Sure? (sender: " + scName + ")", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            string scName = ((ContextMenuStrip)(((ToolStripMenuItem)sender).Owner)).SourceControl.Name;
            if (MessageBox.Show("Are you Sure?", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (scName == "dgvAssemblyInstruction")
                {
                    BindingManagerBase bm = this.dgvAssemblyInstruction.BindingContext[this.dgvAssemblyInstruction.DataSource, this.dgvAssemblyInstruction.DataMember];
                    DataRow dr = ((DataRowView)bm.Current).Row;
                    dr.Delete();
                    dr.EndEdit();
                }
                if (scName == "dgvPackingImage")
                {
                    BindingManagerBase bm = this.dgvPackingImage.BindingContext[this.dgvPackingImage.DataSource, this.dgvPackingImage.DataMember];
                    DataRow dr = ((DataRowView)bm.Current).Row;
                    dr.Delete();
                    dr.EndEdit();
                    btnAssemblyImageNewRow.Enabled = (bm.Count < maxRows);
                }
            }
        }

        private void dgvPackingImage_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //dgvPackingImage.ClearSelection();
        }

        private void dgvPackingImage_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                string filepath;
                string fieldName;
                if (dgvPackingImage.Columns[e.ColumnIndex].Name == "btnImage1")
                {
                    filepath = dgvPackingImage.CurrentRow.Cells["PackingImageFilepath1"].Value.ToString();
                    fieldName = "PackingImageFilepath1";
                }
                else if (dgvPackingImage.Columns[e.ColumnIndex].Name == "btnImage2")
                {
                    filepath = dgvPackingImage.CurrentRow.Cells["PackingImageFilepath2"].Value.ToString();
                    fieldName = "PackingImageFilepath2";
                }
                else if (dgvPackingImage.Columns[e.ColumnIndex].Name == "btnImage3")
                {
                    filepath = dgvPackingImage.CurrentRow.Cells["PackingImageFilepath3"].Value.ToString();
                    fieldName = "PackingImageFilepath3";
                }
                else
                    return;

                OpenFileDialog fdlg = new OpenFileDialog();
                fdlg.Title = "Packing Image Files";
                fdlg.InitialDirectory = @"S:CONSOLIDATED PLASTICS\INJECTION MOULDING\Database\Images\";
                fdlg.Filter = "JPG (*.jpg)|*.jpg|PNG (*.png)|*.png|tif (*.tif)|*.tif|Bitmap (*.bmp)|*.bmp|All files (*.*)|*.*";
                fdlg.FilterIndex = 1;
                fdlg.RestoreDirectory = true;
                if (fdlg.ShowDialog() == DialogResult.OK)
                {
                    string fileSelected = fdlg.FileName;
                    DataRowView drv = (DataRowView)bsPackingImage.Current;
                    DataRow row = drv.Row;
                    row[fieldName] = fileSelected;
                    row.EndEdit();
                }
            }
            catch (Exception ex) { }
        }

        private void dgvPackingImage_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //add tooltip texts
            if (e.RowIndex < 0)
                return;

            if (dgvPackingImage.Columns[e.ColumnIndex].Name == "btnImage1" ||
                dgvPackingImage.Columns[e.ColumnIndex].Name == "btnImage2" ||
                dgvPackingImage.Columns[e.ColumnIndex].Name == "btnImage3")
            {
                DataGridViewCell cell =
                dgvPackingImage.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.ToolTipText = "Click to browse for image file.";
            }

            //assign graphic images
            int colIndex1 = dgvPackingImage.Columns["btnImage1"].Index;
            int colIndex2 = dgvPackingImage.Columns["btnImage2"].Index;
            int colIndex3 = dgvPackingImage.Columns["btnImage3"].Index;

            string filepath = null;
            Image image = null;

            int rowHeight = dgvPackingImage.RowTemplate.Height;
            int colWidth1 = dgvPackingImage.Columns["btnImage1"].Width;
            int colWidth2 = dgvPackingImage.Columns["btnImage2"].Width;
            int colWidth3 = dgvPackingImage.Columns["btnImage3"].Width;

            if (e.ColumnIndex == colIndex1)
            {
                image = EmptyImage();
                if (dgvPackingImage.Rows[e.RowIndex].Cells["PackingImageFilepath1"].Value != DBNull.Value)
                {
                    filepath = dgvPackingImage.Rows[e.RowIndex].Cells["PackingImageFilepath1"].Value.ToString();
                    if (System.IO.File.Exists(filepath))
                        image = GetImage(filepath, colWidth1, rowHeight);
                }
                e.Value = image;
                dgvPackingImage.Rows[e.RowIndex].Cells["PackingImageFilepath1"].Selected = true;
            }
            else if (e.ColumnIndex == colIndex2)
            {
                image = EmptyImage();
                if (dgvPackingImage.Rows[e.RowIndex].Cells["PackingImageFilepath2"].Value != DBNull.Value)
                {
                    filepath = dgvPackingImage.Rows[e.RowIndex].Cells["PackingImageFilepath2"].Value.ToString();
                    if (System.IO.File.Exists(filepath))
                        image = GetImage(filepath, colWidth1, rowHeight);
                }
                e.Value = image;
                dgvPackingImage.Rows[e.RowIndex].Cells["PackingImageFilepath2"].Selected = true;
            }
            else if (e.ColumnIndex == colIndex3)
            {
                image = EmptyImage();
                if (dgvPackingImage.Rows[e.RowIndex].Cells["PackingImageFilepath3"].Value != DBNull.Value)
                {
                    filepath = dgvPackingImage.Rows[e.RowIndex].Cells["PackingImageFilepath3"].Value.ToString();
                    if (System.IO.File.Exists(filepath))
                        image = GetImage(filepath, colWidth1, rowHeight);
                }
                e.Value = image;
                dgvPackingImage.Rows[e.RowIndex].Cells["PackingImageFilepath3"].Selected = true;
            }

        }

        private void dgvPackingImage_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                int colIndex1 = dgvPackingImage.Columns["btnImage1"].Index;
                int colIndex2 = dgvPackingImage.Columns["btnImage2"].Index;
                int colIndex3 = dgvPackingImage.Columns["btnImage3"].Index;

                string filepath;

                Image image = null;

                int rowHeight = dgvPackingImage.RowTemplate.Height;
                int colWidth1 = dgvPackingImage.Columns["btnImage1"].Width;
                int colWidth2 = dgvPackingImage.Columns["btnImage2"].Width;
                int colWidth3 = dgvPackingImage.Columns["btnImage3"].Width;
                if (e.ColumnIndex == colIndex1)
                {
                    filepath = dgvPackingImage.CurrentRow.Cells["PackingImageFilepath1"].Value.ToString();
                    if (System.IO.File.Exists(filepath))
                        image = GetImage(filepath, colWidth1, rowHeight);
                    else
                        image = EmptyImage();

                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                    var w = colWidth1;
                    var h = rowHeight;
                    var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                    e.Graphics.DrawImage(image, new Rectangle(x, y, w, h));
                    e.Handled = true;
                }
                else if (e.ColumnIndex == colIndex2)
                {
                    filepath = dgvPackingImage.CurrentRow.Cells["PackingImageFilepath2"].Value.ToString();
                    if (System.IO.File.Exists(filepath))
                        image = GetImage(filepath, colWidth1, rowHeight);
                    else
                        image = EmptyImage();

                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                    var w = colWidth2;
                    var h = rowHeight;
                    var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                    e.Graphics.DrawImage(image, new Rectangle(x, y, w, h));
                    e.Handled = true;
                }
                else if (e.ColumnIndex == colIndex3)
                {
                    filepath = dgvPackingImage.CurrentRow.Cells["PackingImageFilepath3"].Value.ToString();
                    if (System.IO.File.Exists(filepath))
                        image = GetImage(filepath, colWidth1, rowHeight);
                    else
                        image = EmptyImage();

                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                    var w = colWidth3;
                    var h = rowHeight;
                    var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                    e.Graphics.DrawImage(image, new Rectangle(x, y, w, h));
                    e.Handled = true;
                }

            }
            catch (Exception ex) { }
        }


        private void dgvPackingImage_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void FormatPackingInstruction()
        {
            try
            {
                dgvPackingInstruction.AllowUserToAddRows = true;
                dgvPackingInstruction.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                //dgvPackingInstruction.Width = p96H(600);
                dgvPackingInstruction.Height = p96H(100);
                dgvPackingInstruction.RowHeadersWidth = p96H(26);
                dgvPackingInstruction.ClearSelection();

                dgvPackingInstruction.ColumnHeadersHeight = p96H(19);
                dgvPackingInstruction.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dgvPackingInstruction.AllowUserToResizeRows = false;
                dgvPackingInstruction.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

                DataGridViewCellStyle style = dgvPackingInstruction.ColumnHeadersDefaultCellStyle;
                style.BackColor = Color.RosyBrown;
                style.ForeColor = Color.MidnightBlue;
                style.Font = new Font(dgvPackingInstruction.Font, FontStyle.Regular);
                dgvPackingInstruction.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgvPackingInstruction.ColumnHeadersDefaultCellStyle.BackColor;
                dgvPackingInstruction.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                dgvPackingInstruction.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                dgvPackingInstruction.GridColor = SystemColors.ActiveBorder;
                dgvPackingInstruction.EnableHeadersVisualStyles = false;
                dgvPackingInstruction.AutoGenerateColumns = true;
                dgvPackingInstruction.AutoResizeColumns();
                dgvPackingInstruction.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
                dgvPackingInstruction.Columns["PackingInstruction"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvPackingInstruction.Columns["PackingInstruction"].Width = p96W(615);
                dgvPackingInstruction.Columns["PackingInstructionID"].Visible = false;
                dgvPackingInstruction.Columns["ItemID"].Visible = false;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void dgvPackingInstruction_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvPackingInstruction.ClearSelection();
        }

        private void dgvPackingInstruction_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
        private void dgvPackingInstruction_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //DataTable.Rows[index].EndEdit() 
                DataGridViewRow row = this.dgvPackingInstruction.Rows[e.RowIndex];
                DataRowView drv = (DataRowView)row.DataBoundItem;
                DataRow dr = drv.Row;
                dr.EndEdit();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void dgvPackingInstruction_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            //System.Text.StringBuilder messageBoxCS = new System.Text.StringBuilder();
            //messageBoxCS.AppendFormat("{0} = {1}", "Row", e.Row);
            //messageBoxCS.AppendLine();
            //messageBoxCS.AppendFormat("{0} = {1}", "StateChanged", e.StateChanged);
            //messageBoxCS.AppendLine();
            //MessageBox.Show(messageBoxCS.ToString(), "RowStateChanged Event");
        }

        private void FormatReworkInstruction()
        {
            try
            {
                dgvReworkInstruction.AllowUserToAddRows = true;
                dgvReworkInstruction.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvReworkInstruction.Width = p96H(600);
                dgvReworkInstruction.Height = p96H(100);
                dgvReworkInstruction.RowHeadersWidth = p96H(26);
                dgvReworkInstruction.ClearSelection();

                dgvReworkInstruction.ColumnHeadersHeight = p96H(19);
                dgvReworkInstruction.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dgvReworkInstruction.AllowUserToResizeRows = false;
                dgvReworkInstruction.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

                DataGridViewCellStyle style = dgvReworkInstruction.ColumnHeadersDefaultCellStyle;
                style.BackColor = Color.RosyBrown;
                style.ForeColor = Color.MidnightBlue;
                style.Font = new Font(dgvReworkInstruction.Font, FontStyle.Regular);
                dgvReworkInstruction.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgvReworkInstruction.ColumnHeadersDefaultCellStyle.BackColor;
                dgvReworkInstruction.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                dgvReworkInstruction.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                dgvReworkInstruction.GridColor = SystemColors.ActiveBorder;
                dgvReworkInstruction.EnableHeadersVisualStyles = false;
                dgvReworkInstruction.AutoGenerateColumns = true;
                dgvReworkInstruction.AutoResizeColumns();
                dgvReworkInstruction.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
                dgvReworkInstruction.Columns["ReworkInstruction"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvReworkInstruction.Columns["ReworkInstruction"].Width = p96W(615);
                dgvReworkInstruction.Columns["ReworkInstructionID"].Visible = false;
                dgvReworkInstruction.Columns["ItemID"].Visible = false;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void dgvReworkInstruction_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvReworkInstruction.ClearSelection();
        }

        private void dgvReworkInstruction_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void FormatAssemblyInstruction()
        {
            //format dgvAssemblyInstruction
            //https://stackoverflow.com/questions/30200529/merge-datagridview-image-cell-with-text-cell
            dgvAssemblyInstruction.CellFormatting += dgvAssemblyInstruction_CellFormatting;
            dgvAssemblyInstruction.CellClick += dgvAssemblyInstruction_CellClick;

            dgvAssemblyInstruction.AllowUserToAddRows = false;

            dgvAssemblyInstruction.SelectionMode = DataGridViewSelectionMode.CellSelect;
            //dgvAssemblyInstruction.Width = p96H(600);
            //dgvAssemblyInstruction.Height = p96H(400);
            dgvAssemblyInstruction.RowHeadersWidth = p96H(26);
            dgvAssemblyInstruction.ClearSelection();

            dgvAssemblyInstruction.ColumnHeadersHeight = p96H(19);
            dgvAssemblyInstruction.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvAssemblyInstruction.AllowUserToResizeRows = false;
            dgvAssemblyInstruction.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            DataGridViewCellStyle style = dgvAssemblyInstruction.ColumnHeadersDefaultCellStyle;
            style.BackColor = Color.RosyBrown;
            style.ForeColor = Color.MidnightBlue;
            style.Font = new Font(dgvAssemblyInstruction.Font, FontStyle.Regular);
            dgvAssemblyInstruction.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgvAssemblyInstruction.ColumnHeadersDefaultCellStyle.BackColor;
            dgvAssemblyInstruction.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            dgvAssemblyInstruction.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgvAssemblyInstruction.GridColor = SystemColors.ActiveBorder;
            dgvAssemblyInstruction.EnableHeadersVisualStyles = false;
            dgvAssemblyInstruction.AutoGenerateColumns = true;

            //insert Image column; assigned dynamically in CellFormat event
            DataGridViewImageColumn bc = new DataGridViewImageColumn();
            bc.Name = "AssemblyImage1";
            bc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            dgvAssemblyInstruction.Columns.Add(bc);

            bc = new DataGridViewImageColumn();
            bc.Name = "AssemblyImage2";
            bc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            dgvAssemblyInstruction.Columns.Add(bc);

            dgvAssemblyInstruction.RowTemplate.Height = p96H(170);
            dgvAssemblyInstruction.Columns["AssemblyImage1"].Width = p96W(200);
            dgvAssemblyInstruction.Columns["AssemblyImage2"].Width = p96W(200);
            dgvAssemblyInstruction.Columns["AssemblyInstruction1"].Width = p96W(150);
            dgvAssemblyInstruction.Columns["AssemblyInstruction2"].Width = p96W(150);
            dgvAssemblyInstruction.Columns["AssemblyInstruction1"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvAssemblyInstruction.Columns["AssemblyInstruction1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            dgvAssemblyInstruction.Columns["AssemblyInstruction2"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvAssemblyInstruction.Columns["AssemblyInstruction2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;

            dgvAssemblyInstruction.Columns["AssemblyInstructionID1"].Visible = false;
            dgvAssemblyInstruction.Columns["AssemblyInstructionID2"].Visible = false;
            dgvAssemblyInstruction.Columns["InstructionNo1"].Visible = false;
            dgvAssemblyInstruction.Columns["InstructionNo2"].Visible = false;
            dgvAssemblyInstruction.Columns["AssemblyInstruction1"].Visible = true;
            dgvAssemblyInstruction.Columns["AssemblyInstruction2"].Visible = true;
            dgvAssemblyInstruction.Columns["ItemID1"].Visible = false;
            dgvAssemblyInstruction.Columns["ItemID2"].Visible = false;
            dgvAssemblyInstruction.Columns["AssemblyImageFilepath1"].Visible = false;
            dgvAssemblyInstruction.Columns["AssemblyImageFilepath2"].Visible = false;
            dgvAssemblyInstruction.Columns["AssemblyImage1"].Visible = true;
            dgvAssemblyInstruction.Columns["AssemblyImage2"].Visible = true;

            dgvAssemblyInstruction.Columns["AssemblyInstruction1"].DisplayIndex = 0;
            dgvAssemblyInstruction.Columns["AssemblyImage1"].DisplayIndex = 1;
            dgvAssemblyInstruction.Columns["AssemblyInstruction1"].DisplayIndex = 2;
            dgvAssemblyInstruction.Columns["AssemblyImage2"].DisplayIndex = 3;

            dgvAssemblyInstruction.Columns["AssemblyInstructionID1"].DisplayIndex = 4;
            dgvAssemblyInstruction.Columns["AssemblyInstructionID2"].DisplayIndex = 5;
            dgvAssemblyInstruction.Columns["InstructionNo1"].DisplayIndex = 6;
            dgvAssemblyInstruction.Columns["InstructionNo2"].DisplayIndex = 7;
            //dgvAssemblyInstruction.Columns["AssemblyInstruction1"].DisplayIndex = 0;
            //dgvAssemblyInstruction.Columns["AssemblyInstruction2"].DisplayIndex = 0;
            dgvAssemblyInstruction.Columns["ItemID1"].DisplayIndex = 8;
            dgvAssemblyInstruction.Columns["ItemID2"].DisplayIndex = 9;
            dgvAssemblyInstruction.Columns["AssemblyImageFilepath1"].DisplayIndex = 10;
            dgvAssemblyInstruction.Columns["AssemblyImageFilepath2"].DisplayIndex = 11;

            dgvAssemblyInstruction.CellPainting += dgvAssemblyInstruction_CellPainting;
            dgvAssemblyInstruction.DataBindingComplete += dgvAssemblyInstruction_DataBindingComplete;
            dgvAssemblyInstruction.DataError += dgvAssemblyInstruction_DataError;
            //dgvAssemblyInstruction.DefaultValuesNeeded += dgvAssemblyInstruction_DefaultValuesNeeded;

            //create context menu
            dgvAssemblyInstruction.CellMouseDown += dgvAssemblyInstruction_CellMouseDown;
        }

        private void dgvAssemblyInstruction_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                //add tooltip texts
                if (e.RowIndex < 0)
                    return;

                // add space for two lines:
                //dgvAssemblyInstruction.Rows[0].Height = ((Image)dgvAssemblyInstruction[0, 0].Value).Height + 35;
                // if the previous line throws an error..
                // .. because you didn't put a 'real image' into the cell try this:
                // dgvAssemblyInstruction.Rows[0].Height = 
                //((Image)dgvAssemblyInstruction[0, 0].FormattedValue).Height + 35;

                //assign tooltips
                if (dgvAssemblyInstruction.Columns[e.ColumnIndex].Name == "AssemblyImage1" ||
                    dgvAssemblyInstruction.Columns[e.ColumnIndex].Name == "AssemblyImage2") //||
                                                                                            //dgvAssemblyInstruction.Columns[e.ColumnIndex].Name == "AssemblyImage3")
                {
                    DataGridViewCell cell =
                    dgvAssemblyInstruction.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.ToolTipText = "Click to browse for image";
                }

                //assign graphic images
                int colIndex1 = dgvAssemblyInstruction.Columns["AssemblyImage1"].Index;
                int colIndex2 = dgvAssemblyInstruction.Columns["AssemblyImage2"].Index;
                //int colIndex3 = dgvAssemblyInstruction.Columns["AssemblyImage3"].Index;

                string filepath = null;
                Image image = null;

                int rowHeight = dgvAssemblyInstruction.RowTemplate.Height;
                int colWidth1 = dgvAssemblyInstruction.Columns["AssemblyImage1"].Width;
                int colWidth2 = dgvAssemblyInstruction.Columns["AssemblyImage2"].Width;
                //int colWidth3 = dgvAssemblyInstruction.Columns["AssemblyImage3"].Width;

                if (e.ColumnIndex == colIndex1)
                {
                    image = EmptyImage();
                    if (dgvAssemblyInstruction.Rows[e.RowIndex].Cells["AssemblyImageFilepath1"].Value != null)
                    {
                        filepath = dgvAssemblyInstruction.Rows[e.RowIndex].Cells["AssemblyImageFilePath1"].Value.ToString();
                        if (System.IO.File.Exists(filepath))
                            image = GetImage(filepath, colWidth1, rowHeight);
                    }
                    e.Value = image;

                }
                else if (e.ColumnIndex == colIndex2)
                {
                    image = EmptyImage();
                    if (dgvAssemblyInstruction.Rows[e.RowIndex].Cells["AssemblyImageFilePath2"].Value != null)
                    {
                        filepath = dgvAssemblyInstruction.Rows[e.RowIndex].Cells["AssemblyImageFilepath2"].Value.ToString();
                        if (System.IO.File.Exists(filepath))
                            image = GetImage(filepath, colWidth1, rowHeight);
                    }
                    e.Value = image;
                }
            }
            catch (Exception ex) { }
        }

        private void dgvAssemblyInstruction_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                //MessageBox.Show("todo:  implement CellClick");
                string filepath;
                string fieldName;
                if (dgvAssemblyInstruction.Columns[e.ColumnIndex].Name == "AssemblyImage1")
                {
                    filepath = dgvAssemblyInstruction.CurrentRow.Cells["AssemblyImageFilePath1"].Value.ToString();
                    fieldName = "AssemblyImageFilePath1";
                }
                else if (dgvAssemblyInstruction.Columns[e.ColumnIndex].Name == "AssemblyImage2")
                {
                    filepath = dgvAssemblyInstruction.CurrentRow.Cells["AssemblyImageFilePath2"].Value.ToString();
                    fieldName = "AssemblyImageFilePath2";
                }
                else
                    return;

                OpenFileDialog fdlg = new OpenFileDialog();
                fdlg.Title = "Assembly Instruction Image Files";
                fdlg.InitialDirectory = @"S:CONSOLIDATED PLASTICS\INJECTION MOULDING\Database\Images\";
                fdlg.Filter = "JPG (*.jpg)|*.jpg|PNG (*.png)|*.png|tif (*.tif)|*.tif|Bitmap (*.bmp)|*.bmp|All files (*.*)|*.*";
                fdlg.FilterIndex = 1;
                fdlg.RestoreDirectory = true;
                if (fdlg.ShowDialog() == DialogResult.OK)
                {
                    string fileSelected = fdlg.FileName;

                    //DataRowView drv = (DataRowView)bsAssemblyInstruction.Current;
                    //DataRow row = drv.Row;
                    BindingManagerBase bm = this.dgvAssemblyInstruction.BindingContext[this.dgvAssemblyInstruction.DataSource, this.dgvAssemblyInstruction.DataMember];
                    DataRow dr = ((DataRowView)bm.Current).Row;
                    dr[fieldName] = fileSelected;
                    dr.EndEdit();
                }
            }
            catch (Exception ex) { }
        }

        private void dgvAssemblyInstruction_CellPainting(object sender,
                                        DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;                  // no image in the header            
            int imageColumn1 = dgvAssemblyInstruction.Columns["AssemblyImage1"].Index;
            int imageColumn2 = dgvAssemblyInstruction.Columns["AssemblyImage2"].Index;
            int instructionColumn1 = dgvAssemblyInstruction.Columns["AssemblyInstruction1"].Index;
            int instructionColumn2 = dgvAssemblyInstruction.Columns["AssemblyInstruction2"].Index;
            int instructionNo1 = dgvAssemblyInstruction.Columns["InstructionNo1"].Index;
            int instructionNo2 = dgvAssemblyInstruction.Columns["InstructionNo2"].Index;

            if (e.ColumnIndex == imageColumn1 || e.ColumnIndex == imageColumn2)
            {
                e.PaintBackground(e.ClipBounds, false);  // no highlighting
                e.PaintContent(e.ClipBounds);

                // calculate the location of your text..:
                int y = e.CellBounds.Bottom - 32;

                string yourText = (e.ColumnIndex == imageColumn1)
                    ? dgvAssemblyInstruction[instructionNo1, e.RowIndex].Value.ToString()
                    //+ ". " + dgvAssemblyInstruction[instructionColumn1, e.RowIndex].Value.ToString()
                    : dgvAssemblyInstruction[instructionNo2, e.RowIndex].Value.ToString();
                //+ ". " + dgvAssemblyInstruction[instructionColumn2, e.RowIndex].Value.ToString();

                Font yourFont = new Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular, GraphicsUnit.Point);
                System.Drawing.Brush yourColor = Brushes.Black;
                e.Graphics.DrawString(yourText, yourFont, yourColor, e.CellBounds.Left, y);

                // maybe draw more text with other fonts etc..
                e.Handled = true;                        // done with the image column 
                //Debug.WriteLine(yourText);
            }
        }

        private void dgvAssemblyInstruction_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Ignore if a column or row header is clicked
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if (e.Button == MouseButtons.Right)
                {
                    DataGridViewCell clickedCell = (sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex];

                    // Here you can do whatever you want with the cell
                    this.dgvAssemblyInstruction.CurrentCell = clickedCell;  // Select the clicked cell, for instance

                    // Get mouse position relative to the vehicles grid
                    var relativeMousePosition = dgvAssemblyInstruction.PointToClient(Cursor.Position);

                    ContextMenuStrip cms = new ContextMenuStrip();
                    dgvAssemblyInstruction.ContextMenuStrip = cms;
                    if (clickedCell.OwningColumn.Name == "AssemblyImage1" |
                        clickedCell.OwningColumn.Name == "AssemblyImage2")
                    {
                        int fp1Index = dgvAssemblyInstruction.Columns["AssemblyImageFilepath1"].Index;
                        int fp2Index = dgvAssemblyInstruction.Columns["AssemblyImageFilepath2"].Index;
                        string fp1 = dgvAssemblyInstruction.Rows[e.RowIndex].Cells[fp1Index].Value.ToString();
                        string fp2 = dgvAssemblyInstruction.Rows[e.RowIndex].Cells[fp2Index].Value.ToString();
                        if ((clickedCell.OwningColumn.Name == "AssemblyImage1" && fp1.Length > 0) ||
                            (clickedCell.OwningColumn.Name == "AssemblyImage2" && fp2.Length > 0))
                        {
                            cms.Items.Add("&Zoom", Resources.zoom, new System.EventHandler(this.Zoom_Click));
                            cms.Items.Add("&Remove", Resources.remove, new System.EventHandler(this.Remove_Click));
                        }

                        cms.Items.Add("&Browse", Resources.browse, new System.EventHandler(this.Browse_Click));
                        cms.Items.Add(new ToolStripSeparator());
                    }
                    cms.Items.Add("&Delete Row", Resources.delete, new System.EventHandler(this.Delete_Click));

                    // Show the context menu
                    cms.Show(dgvAssemblyInstruction, relativeMousePosition);
                }
            }
        }

        private void dgvAssemblyInstruction_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvAssemblyInstruction.ClearSelection();
        }

        private void dgvAssemblyInstruction_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void btnAssemblyImageNewRow_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("todo:  add new image row");                             
            bsAssemblyInstructionPivot.AddNew();
            bsAssemblyInstructionPivot.EndEdit();
            bsAssemblyInstructionPivot.Position = bsAssemblyInstructionPivot.Count - 1;
            DataTable ct = dsPackaging.Relations["ItemAssemblyInstruction"].ChildTable;
            DataRow[] foundRows = ct.Select("ItemID1 = " + LastItemID.ToString());
            int count = foundRows.Length;
            btnAssemblyImageNewRow.Enabled = (count < maxRows);
            lblAddImageRow.Enabled = (count < maxRows);
        }

        private void SetFormState()
        {
            try
            {
                //disable NavigationBar controls
                foreach (Object o in bindingNavigator1.Items)
                {
                    if (o.GetType() == typeof(System.Windows.Forms.ToolStripComboBox))
                    {
                        ToolStripComboBox tscb = (ToolStripComboBox)o;
                        if (tscb.Name != "tscboCompany")
                            tscb.Enabled = false;
                    }
                    if (o.GetType() == typeof(ToolStripLabel))
                    {
                        ToolStripLabel lb = (ToolStripLabel)o;
                        if (lb.Text != "Company")
                            lb.ForeColor = SystemColors.GrayText;
                    }
                }

                //Populate Toolbar Customer dropdown

                //DataTable dt = dsCompany.Tables[0].Copy();
                DataTable dt = (DataTable)dsPackaging.Tables["Customer"].Copy();
                dt.TableName = "customer";
                tscboCompany.ComboBox.SelectedIndexChanged -= tscboCompany_SelectedIndexChanged;
                ChangedByCode = true;
                tscboCompany.ComboBox.DataSource = dt;
                tscboCompany.ComboBox.DisplayMember = "CUSTNAME";
                tscboCompany.ComboBox.ValueMember = "CustomerID";
                tscboCompany.ComboBox.SelectedIndex = -1;
                tscboCompany.ComboBox.SelectedIndexChanged += tscboCompany_SelectedIndexChanged;
                ChangedByCode = false;

                if (CustomerFilterOn && LastCustomerID.HasValue && LastItemID.HasValue)
                {
                    tscboCompany.ComboBox.SelectedIndexChanged -= tscboCompany_SelectedIndexChanged;
                    ChangedByCode = true;
                    tscboCompany.ComboBox.SelectedValue = LastCustomerID.Value;
                    SetProductFilter(LastCustomerID.Value, LastItemID.Value);
                    tscboCompany.ComboBox.SelectedIndexChanged += tscboCompany_SelectedIndexChanged;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetComboBoxSelectionByValue(int? value)
        {
            try
            {
                DataTable table = (DataTable)this.tscboProduct.ComboBox.DataSource;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    string displayItem = table.Rows[i][tscboProduct.ComboBox.DisplayMember].ToString();
                    int valueItem = (int)table.Rows[i][tscboProduct.ComboBox.ValueMember];

                    if (valueItem == value)
                    {
                        this.tscboProduct.ComboBox.SelectedIndex = i;
                        return;
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void PackagingDataEntry_Shown(object sender, EventArgs e)
        {
            //Rectangle r = new Rectangle(5, 5, p96W(2176), p96H(1950));
            //Rectangle r = new Rectangle(5, 5, p96W(1000), p96H(2000));
            //this.DesktopBounds = r;
            //this.DesktopBounds = r;
            this.Size = new Size(p96W(1175), p96H(935));
            bindingNavigator1.Height = p96H(30);
            splitContainer1.SplitterDistance = p96H(55);
            splitContainer2.SplitterDistance = p96H(25);//Assembly Instruction
            splitContainer4.SplitterDistance = p96H(90);
            splitContainer5.SplitterDistance = p96H(25);//Rework Instruction
            this.menuStrip1.Focus();
            this.productPackagingToolStripMenuItem.Select();
            SetFormState();
        }

        private void productSpecificationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoSave();
            NextForm = "SpecificationDataEntry";
            this.DialogResult = DialogResult.Retry;
        }

        private void productAssemblyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoSave();
            NextForm = "AssemblyDataEntry";
            this.DialogResult = DialogResult.Retry;
        }


        private void qCInstructionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoSave();
            NextForm = "QCDataEntry";
            this.DialogResult = DialogResult.Retry;
        }

        private void attachedDocumentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoSave();
            NextForm = "AttachedDocsDataEntry";
            this.DialogResult = DialogResult.Retry;
        }

        private void DoSave()
        {
            try
            {
                bsManItems.CurrentChanged -= bsManItems_CurrentChanged; //form is about to close;  don't want to refresh
                DataViewRowState dvrs;
                //DataRow[] rows;
                DataSet ds = dsPackaging;
                DataRowView drv = (DataRowView)this.bsManItems.Current;
                DataRow row = drv.Row;
                int currentID = (int)row["ItemID"];
                LastItemID = currentID;

                bsPackaging.EndEdit();
                bsCartonPackaging.EndEdit();
                bsPalletPackaging.EndEdit();


                PackagingDAL packagingDAL = new PackagingDAL();
                packagingDAL.UpdatePackaging(dsPackaging, "Packaging");

                bsPackingInstruction.EndEdit();
                dgvPackingInstruction.EndEdit();
                PackingInstructionDAL packingInstructionDAL = new PackingInstructionDAL();
                packingInstructionDAL.UpdatePackingInstruction(dsPackaging, "PackingInstruction");

                bsPackingImage.EndEdit();
                dgvPackingImage.EndEdit();
                PackingImageDAL packingImageDAL = new PackingImageDAL();
                packingImageDAL.UpdatePackingImage(dsPackaging, "PackingImage");

                bsReworkInstruction.EndEdit();
                dgvReworkInstruction.EndEdit();
                ReworkInstructionDAL reworkInstructionDAL = new ReworkInstructionDAL();
                reworkInstructionDAL.UpdateReworkInstruction(dsPackaging, "ReworkInstruction");

                bsAssemblyInstructionPivot.EndEdit();
                dgvAssemblyInstruction.EndEdit();
                AssemblyInstructionDAL assemblyInstructionDAL = new AssemblyInstructionDAL();
                //assemblyInstructionDAL.UpdateAssemblyInstruction(dsPackaging, "AssemblyInstruction");
                assemblyInstructionDAL.UpdateFromPivotTable(dsPackaging, "AssemblyInstructionPivot");

            }
            catch (Exception ex) { }
        }


        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                InjectionMouldReports.Reports.ViewReport(LastItemID.Value, "ProductPackaging.rdlc");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsbtnAddNew_Click(object sender, EventArgs e)
        {

        }

        private void GetDataSets()
        {
            try
            {
                SpecificationDataEntryDAL dal = new SpecificationDataEntryDAL();
                dsPackaging = dal.BuildPackagingDataSet();
            }
            catch (Exception ex) { }
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void tscboCompany_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            // ## gotcha !!! ##
            // toolstrip combo automatically changes selected index to 0 after detecting value of -1
            // workaround:  set ChangedByCode to true !!!
            // ###
            if (tscboCompany.SelectedIndex != -1 && !ChangedByCode)
            {
                int custID = (int)tscboCompany.ComboBox.SelectedValue;
                if (custID != LastCustomerID)
                {
                    SetProductFilter(custID);
                    LastCustomerID = custID;
                }
            }
        }

        private void SetProductFilter(int custID, int itemID = 0)
        {
            try
            {
                LastCustomerID = custID;
                CustomerFilterOn = true;
                DataTable dt = dsPackaging.Tables["CustomerProduct"].Copy();                
                DataView dv = new DataView(dt, "CustomerID = " + custID.ToString(), "CustomerID", DataViewRowState.CurrentRows);
                DataTable dt1 = dv.ToTable();
                DataTable dt2 = dsPackaging.Tables["Product"];
                //dt2.DefaultView.RowFilter = "";
                string rowFilter = "";
                if (dt1.Rows.Count == 0)
                {
                    if (MessageBox.Show("This customer has no products.  Do you wish to add this customer for an existing product?",
                                            "Add customer for product", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        MessageBox.Show("handle add customer");
                    }

                    EnableGroups(false);
                }
                else
                {
                    var ids = dt1.AsEnumerable().Select(r => r.Field<int>("ItemID"));
                    //dt2.DefaultView.RowFilter = string.Format("ItemID in ({0})", string.Join(",", ids));
                    rowFilter = string.Format("ItemID in ({0})", string.Join(",", ids));
                    int defaultItemID = ids.FirstOrDefault();  //selects first item
                    LastItemID = (rowFilter.Contains(itemID.ToString())) ? itemID : defaultItemID;
                }
                //populate navigation bar Product dropdown
                tscboProduct.SelectedIndexChanged -= tscboProduct_SelectedIndexChanged;
                ChangedByCode = true;  //exits SelectedIndexChanged
                tscboProduct.ComboBox.DataBindings.Clear();                                 
                DataView vp = new DataView(dt2);
                vp.Sort = "ITEMDESC ASC";
                vp.RowFilter = rowFilter;
                tscboProduct.ComboBox.DataSource = vp;
                tscboProduct.ComboBox.ValueMember = "ItemID";
                tscboProduct.ComboBox.DisplayMember = "ITEMDESC";
                tscboProduct.SelectedIndexChanged += tscboProduct_SelectedIndexChanged;                

                // populate navigation bar code dropdown
                DataView vc = new DataView(dt2);
                vc.Sort = "ITEMNMBR ASC";
                vc.RowFilter = rowFilter;
                tscboCode.ComboBox.SelectedIndexChanged -= tscboCode_SelectedIndexChanged;
                tscboCode.ComboBox.DataBindings.Clear();
                tscboCode.ComboBox.DataSource = vc;
                tscboCode.ComboBox.ValueMember = "ItemID";
                tscboCode.ComboBox.DisplayMember = "ITEMNMBR";
                tscboCode.ComboBox.SelectedIndexChanged += tscboCode_SelectedIndexChanged;
                ChangedByCode = false;

                if (dt1.Rows.Count > 0)
                {                    
                    if (bsManItems == null)
                    {
                        BindControls();
                        FormatAssemblyInstruction();
                        FormatPackingImage();
                        FormatPackingInstruction();
                        FormatReworkInstruction();
                    }
                    bsManItems.Filter = rowFilter;
                    int itemIndex = bsManItems.Find("ItemID", itemID);
                    if (itemID != -1)
                    {
                        EnableGroups(true);
                        bsManItems.Position = itemIndex;                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SetProductFilter", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
           
        private void tscboCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tscboCode.SelectedIndex != -1 && !ChangedByCode)
            {
                int itemID = (int)tscboCode.ComboBox.SelectedValue;

                if (itemID != LastItemID)
                {
                    LastItemID = itemID;
                    if (bsManItems == null)
                    {
                        BindControls();
                        FormatAssemblyInstruction();
                        FormatPackingImage();
                        FormatPackingInstruction();
                        FormatReworkInstruction();
                    }
                    int itemIndex = bsManItems.Find("ItemID", itemID);
                    if (itemID != -1)
                    {
                        btnReport.Enabled = true;
                        bsManItems.Position = itemIndex;
                    }
                }
            }
        }

        private void EnableGroups(bool flag)
        {
            gpGeneral.Enabled = flag;
            gpPallets.Enabled = flag;
            gpPackaging.Enabled = flag;
            gpLabel.Enabled = flag;
            gpPackerQC.Enabled = flag;
            dgvPackingInstruction.Visible = flag;
            dgvPackingImage.Visible = flag;
            gpReworkInstructions.Enabled = flag;
            dgvReworkInstruction.Visible = flag;
            gpAssemblyInstructions.Enabled = flag;  
            dgvAssemblyInstruction.Visible = flag;
            btnReport.Enabled = flag;

            //enable NavigationBar controls
            foreach (Object o in bindingNavigator1.Items)
            {
                if (o.GetType() == typeof(System.Windows.Forms.ToolStripComboBox))
                {
                    ToolStripComboBox tscb = (ToolStripComboBox)o;
                    if (tscb.Name != "tscboCompany")
                        tscb.Enabled = flag;
                }
                if (o.GetType() == typeof(ToolStripLabel))
                {
                    ToolStripLabel lb = (ToolStripLabel)o;
                    if (lb.Text != "Company")
                        lb.ForeColor = flag ? Color.Black : SystemColors.GrayText;
                }
            }
        }

        private void productPackagingToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void tscboProduct_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (tscboProduct.SelectedIndex != -1 && !ChangedByCode)
            {
                int itemID = (int)tscboProduct.ComboBox.SelectedValue;
                if (itemID != LastItemID)
                {
                    LastItemID = itemID;
                    if (bsManItems == null)
                    {
                        BindControls();
                        FormatAssemblyInstruction();
                        FormatPackingImage();
                        FormatPackingInstruction();
                        FormatReworkInstruction();
                    }                                        
                    int itemIndex = bsManItems.Find("ItemID", itemID);
                    if (itemID != -1)
                    {
                        btnReport.Enabled = true;
                        bsManItems.Position = itemIndex;
                    }
                }
            }
        }

        private void bsManItems_CurrentChanged(object sender, EventArgs e)
        {
            try
            {                
                DataRowView drv = (DataRowView)this.bsManItems.Current;
                DataRow dr = drv.Row;

                if (!drv.IsNew)
                {
                    //bindingNavigator sometimes loses primary key value;
                    //happens when clicking between MoveNext, MovePrevious or MoveFirst, MoveLast
                    //Handle by resetting key to original value
                    if (dr.HasVersion(DataRowVersion.Original))
                    {
                        if (dr["ItemID", DataRowVersion.Current].ToString().Length == 0 &&
                        dr["ItemID", DataRowVersion.Original].ToString().Length != 0)
                        {
                            int itemID = (int)dr["ItemID", DataRowVersion.Original];
                            dr["ItemID"] = itemID;
                            dr.EndEdit();
                        }
                    }                    
                }

                //Handle new record
                if (drv.IsNew)
                {
                    int curItemID = (int)drv.Row["ItemID"];

                    //bindingNavigator does not allow disabling of built-in navigation buttons.
                    //Set invisible instead;   
                    bindingNavigatorMoveFirstItem.Visible = false;
                    bindingNavigatorMovePreviousItem.Visible = false;
                    tsbtnDelete.Enabled = false;
                    tsbtnAddNew.Enabled = false;

                    tscboCompany.Enabled = false;
                    tscboProduct.Enabled = false;
                    btnReport.Enabled = false;

                    //reset product filter dropdown index
                    if (bsManItems.Filter != null)
                    {
                        int itemID = (int)dr["ItemID"];
                        tscboProduct.SelectedIndexChanged -= tscboProduct_SelectedIndexChanged;
                        tscboCode.SelectedIndexChanged -= tscboCode_SelectedIndexChanged;
                        ChangedByCode = true; //unsubscribing doesn't work for navigation bar combobox !!!
                        SetDropDownIndex(tscboProduct, itemID);
                        SetDropDownIndex(tscboCode, itemID);
                        tscboProduct.SelectedIndexChanged += tscboProduct_SelectedIndexChanged;
                        tscboCode.SelectedIndexChanged += tscboCode_SelectedIndexChanged;
                        ChangedByCode = false;
                    }
                }

                RefreshCurrent();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         }

        private void SetDropDownIndex(ToolStripComboBox tscb, int itemID)
        {
            try
            {
                int selectedIndex = -1;
                for (int i = 0; i < tscb.ComboBox.Items.Count; i++)
                {
                    DataRowView drv = tscb.ComboBox.Items[i] as DataRowView;
                    DataRow row = drv.Row;

                    if (row != null)
                    {
                        //string displayValue = row["ITEMNMBR"].ToString();
                        //Debug.Print(displayValue);
                        int testItemID = Convert.ToInt32(row["ItemID"].ToString());
                        if (testItemID == itemID)
                        {
                            selectedIndex = i;
                            tscb.ComboBox.SelectedIndex = selectedIndex;
                            break;
                        }
                    }
                }
            }
            catch { }
        }

        private void bsPackaging_PositionChanged(object sender, EventArgs e)
        {
            MessageBox.Show(bsPackaging.Position.ToString());
        }

        private void RefreshCurrent()
        {
            try
            {                
                if (this.bsManItems.Current != null)
                {
                    DataRowView rowView = (DataRowView)this.bsManItems.Current;
                    DataRow row = rowView.Row;


                    int itemID = int.TryParse(row["ItemID"].ToString(), out itemID) ? itemID : -999;

                    if (itemID == -999)
                        return;
                    
                    //reset product filter dropdown index
                    if (bsManItems.Filter != null)
                    {                        
                        tscboProduct.SelectedIndexChanged -= tscboProduct_SelectedIndexChanged;
                        ChangedByCode = true; //unsubscribing doesn't work for navigation bar combobox !!!
                        SetDropDownIndex(tscboProduct, itemID);
                        SetDropDownIndex(tscboCode, itemID);
                        ChangedByCode = false;
                    }

                    //MessageBox.Show(bsManItems.Position.ToString());

                    //locate customer for this product                
                    int cpIndex = bsCustomerProducts.Find("ItemID", itemID);
                    if (cpIndex != -1)
                    {
                        bsCustomer.SuspendBinding();
                        bsCustomerProducts.Position = cpIndex;
                        DataRowView drv = (DataRowView)bsCustomerProducts.Current;
                        DataRow dr = drv.Row;
                        int customerID = (int)dr["CustomerID"];
                        int custIndex = bsCustomer.Find("CustomerID", customerID);
                        if (custIndex != -1)
                        {
                            bsCustomer.ResumeBinding();
                            bsCustomer.Position = custIndex;
                            LastCustomerID = customerID;
                        }
                    }

                    //locate ProductGrade aka ProductCategory
                    //MessageBox.Show("GradeID=" + row["GradeID"].ToString());
                    bsProductGradeItem.SuspendBinding();
                    int gradeID = int.TryParse(row["GradeID"].ToString(), out gradeID) ? gradeID : -1;
                    if (gradeID != -1)
                    {
                        int pgIndex = bsProductGradeItem.Find("GradeID", gradeID);
                        if (pgIndex != -1)
                        {
                            bsProductGradeItem.ResumeBinding();
                            bsProductGradeItem.Position = pgIndex;
                        }
                    }

                    //locate Item packaging
                   

                    /*
                    //locate carton type               
                    bsCartonPackaging.SuspendBinding();
                    DataRowView dv = (DataRowView)bsItemPackaging.Current;
                    DataRow r = dv.Row;
                    int ctnID = int.TryParse(r["CtnID"].ToString(), out ctnID) ? ctnID : -1;
                    if (ctnID != -1)
                    {
                        int pIndex = bsCartonPackaging.Find("CtnID", ctnID);
                        if (pIndex != -1)
                        {
                            bsCartonPackaging.ResumeBinding();
                            bsCartonPackaging.Position = pIndex;
                            //DataRowView dv = (DataRowView)bsItemPackaging.Current;
                            //DataRow r = dv.Row;
                            //MessageBox.Show(r["CtnQty"].ToString());
                        }
                    }

                    //locate pallet type               
                    bsPalletPackaging.SuspendBinding();
                    dv = (DataRowView)bsItemPackaging.Current;
                    r = dv.Row;
                    int palletID = int.TryParse(r["PalletID"].ToString(), out palletID) ? palletID : -1;
                    if (palletID != -1)
                    {
                        int pIndex = bsPalletPackaging.Find("PalletID", palletID);
                        if (pIndex != -1)
                        {
                            bsPalletPackaging.ResumeBinding();
                            bsPalletPackaging.Position = pIndex;
                            //DataRowView dv = (DataRowView)bsPackagingItem.Current;
                            //DataRow r = dv.Row;
                            //MessageBox.Show(r["CtnQty"].ToString());
                        }
                    }
                    */

                    //locate Packaging item
                    bsPackaging.EndEdit();
                    bsPackaging.Sort = "ItemID";
                    DataTable dt = dsPackaging.Tables["Packaging"];
                    DataView view = new DataView(dt, "", "ItemID", DataViewRowState.CurrentRows);
                    int piID = view.Find(itemID);
                    //int piID = bsPackaging.Find("ItemID", itemID);
                    if (piID != -1)
                    {
                        bsPackaging.ResetBindings(false);
                        bsPackaging.ResumeBinding();
                        //bsPackaging.Position = (int)view[msID].Row.RowId();
                        //MessageBox.Show(view[msID].Row["ItemID"].ToString() + ", " + view[msID].Row["MouldNumber"].ToString());
                        bsPackaging.Position = piID;
                        //rowView = (DataRowView)this.bsMouldSpec.Current;
                        //row = rowView.Row;
                        //MessageBox.Show(row["ItemID"].ToString() + ", " + row["MouldNumber"].ToString());
                        //bsMouldSpec.Position = msID;
                        //rowView = (DataRowView)this.bsMouldSpec.Current;
                        //row = rowView.Row;

                    }


                    //locate PackagingImage
                    bsPackingImage.SuspendBinding();
                    int pgID = bsPackingImage.Find("ItemID", itemID);
                    if (pgID != -1)
                    {
                        bsPackingImage.ResumeBinding();
                        bsPackingImage.Position = pgID;
                    }
                    else
                    {
                        //create new packing image record
                        bsPackingImage.AddNew();
                        bsPackingImage.EndEdit();
                        bsPackingImage.ResumeBinding();
                        bsPackingImage.Position = bsPackingImage.Count - 1;
                    }

                    //locate item in PackingInstruction
                    bsPackingInstruction.SuspendBinding();
                    int pkID = bsPackingInstruction.Find("ItemID", itemID);
                    if (pkID != -1)
                    {
                        bsPackingInstruction.ResumeBinding();
                        bsPackingInstruction.Position = pkID;
                    }

                    //locate item in ReworkInstruction
                    ReworkExpanded = true; //collapses control if no data
                    bsReworkInstruction.SuspendBinding();
                    int rwID = bsReworkInstruction.Find("ItemID", itemID);
                    if (rwID != -1)
                    {
                        bsReworkInstruction.ResumeBinding();
                        bsReworkInstruction.Position = rwID;
                        ReworkExpanded = false;  //expands control to show data
                    }
                    //set rework instruction control to expand or collapse 
                    SetReworkInstruction();

                    //locate item in AssemblyInstruction
                    AssemblyExpanded = true; //collapses control if no data 
                    bsAssemblyInstructionPivot.SuspendBinding();
                    int asID = bsAssemblyInstructionPivot.Find("ItemID1", itemID);
                    if (asID != -1)
                    {
                        AssemblyExpanded = false; //expands control to show data
                        bsAssemblyInstructionPivot.ResumeBinding();
                        bsAssemblyInstructionPivot.Position = asID;
                    }
                    else
                    {
                        //create new assembly instruction record
                        bsAssemblyInstructionPivot.AddNew();
                        bsAssemblyInstructionPivot.EndEdit();
                        bsAssemblyInstructionPivot.ResumeBinding();
                        bsAssemblyInstructionPivot.Position = bsAssemblyInstructionPivot.Count - 1;
                    }

                    DataTable ct = dsPackaging.Relations["ItemAssemblyInstruction"].ChildTable;
                    DataRow[] foundRows = ct.Select("ItemID1 = " + itemID.ToString());
                    int count = foundRows.Length;
                    btnAssemblyImageNewRow.Enabled = (count < maxRows);
                    lblAddImageRow.Enabled = (count < maxRows);

                    //set assembly instruction control height to expand or collapse
                    SetAssemblyInstruction();                        
                }                    
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        //private void bsItemPackaging_PositionChanged(object sender, EventArgs e)
        //{
        //    MessageBox.Show(bsItemPackaging.Position.ToString()); 
        //}
    }
}
