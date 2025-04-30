using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Linq;
using Utils;
using static Utils.DrawingUtils;
//using Microsoft.Office.Interop.Excel;





namespace MouldSpecification
{
    public partial class SpecificationDataEntry : Form
    {
        public int? LastItemID { get; set; }
        public int? LastCustomerID { get; set; }
        public string NextForm { get; set; }
        public bool CustomerFilterOn { get; set; }

        //When a toolstrip combo datasource changes, its SelectionIndexChanged event always fires
        //with index=0, even if the event is unsubscribed;  set this boolean to make this event exit
        //straight away.  Applies to tscboCompany
        bool ignoreZero = false;

        //private BindingSource bindingIMSpecification;
        DataSet dsCompany, dsProduct, dsGradeID, dsIMSpecificationForm,
                dsPolymerGrade, dsPolymerType, dsMBColour;

        BindingSource bsManItems, bsMouldSpec, bsMaterialComp, bsMBComp, bsQC,
            bsMachPref, bsCustomer, bsCustomerProducts,
            bsMaterialGrade, bsMaterialGradeComp, bsMaterial,
            bsMasterBatch, bsMBMBComp, bsAdditive, bsAddMBComp,
            bsProductGrade, bsProductGradeItem; //, bsNavigator;

        DataViewManager viewManager;
        DataView custView, prodView;

        DataGridView dgvMBCode, dgvAdditive;

        ToolStripLabel tslCompany;
        ToolStripComboBox tscboCompany;
        ToolStripLabel tslProduct;
        ToolStripComboBox tscboProduct;
        ToolStripButton tsbtnReport;
        //ToolStripComboBox tscboEntryForm;


        private ErrorProvider errorProvider1;
        DataGridViewComboBoxEditingControl cbec = null;
        private DataGridViewCell currentCell;

        private bool OnNewRow = false;
        private int curPolymerNo = 0, curMaterialGradeID;
        private int maxRowsMaterial = 3;
        private int maxRowsMachine = 3;
        private int maxRowsMB = 2;

        //primary key identifier for table MAN_Items
        //negatively incremented for new records being added
        private int newItemID = 0;



        public SpecificationDataEntry(int? lastItemID, int? lastCustomerID, bool customerFilterOn = true)
        {
            InitializeComponent();

            //identifiers for last edited product and customer
            LastItemID = lastItemID;
            LastCustomerID = lastCustomerID;
            CustomerFilterOn = customerFilterOn;

            //allows navigation to another form via toolbar
            NextForm = this.Name;

            this.SuspendLayout();
            tslCompany = new ToolStripLabel() { Text = "Company" };
            tslProduct = new ToolStripLabel() { Text = "Product" };
            tscboCompany = new ToolStripComboBox();
            tscboProduct = new ToolStripComboBox();
            //tscboEntryForm = new ToolStripComboBox() { Text = "Product Specification" }; ;
            tsbtnReport = new ToolStripButton() { Text = "Report" };

            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                tslCompany,
                tscboCompany,
                tslProduct,
                tscboProduct,
                //tscboEntryForm,
                tsbtnReport
            });

            //tsbtnAccept.Click += tsbtnAccept_Click;
            tsbtnCancel.Click += tsbtnCancel_Click;
            //tsbtnReload.Click += tsbtnReload_Click;
            tsbtnReport.Click += tsbtnReport_Click;

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
            //foreach (RowStyle style in this.tableLayoutPanel5.RowStyles)
            //{
            //    if (style.SizeType == SizeType.Absolute)
            //        style.Height = p96H(19);
            //}
            foreach (RowStyle style in this.tableLayoutPanel6.RowStyles)
            {
                if (style.SizeType == SizeType.Absolute)
                    style.Height = p96H(19);
            }
            foreach (RowStyle style in this.tableLayoutPanel7.RowStyles)
            {
                if (style.SizeType == SizeType.Absolute)
                    style.Height = p96H(19);
            }

            this.ResumeLayout();
        }

        public SpecificationDataEntry()
        {

        }

        private void tsbtnReport_Click(object sender, EventArgs e)
        {
            try
            {

                InjectionMouldReports.Reports.ViewReport(LastItemID.Value, "ProductSpecification.rdlc");

                /*
                // load as exe
                var appSettings = ConfigurationManager.AppSettings;
                string reportPath = appSettings["IMReportsPath"];
                string appStartPath = System.Windows.Forms.Application.StartupPath;
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = appStartPath + reportPath + @"\InjectionMouldReports.exe";
                //int custID = (int)tscboCompany.ComboBox.SelectedValue;
                //int itemID = (int)tscboProduct.ComboBox.SelectedValue;
                int custID = (int)LastCustomerID;
                int itemID = (int)LastItemID;
                info.Arguments = custID.ToString() + " " + itemID.ToString() + " " + "ProductSpecification.rdlc";
                info.UseShellExecute = true;
                info.CreateNoWindow = false;
                info.WorkingDirectory = reportPath;
                Process proc = Process.Start(info);
                proc.WaitForExit();
                proc.Dispose();
                */

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsbtnReload_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void tsbtnAccept_Click(object sender, EventArgs e)
        {
            SaveEdits();
            this.DialogResult = DialogResult.Retry;
            this.Close();
        }

        private void SpecificationDataEntry_Load(object sender, EventArgs e)
        {
            try
            {
                tsbtnDelete.Click += tsbtnDelete_Click;
                tsbtnAddNew.Click += tsbtnAddNew_Click;
                tsbtnAddNew.Enabled = false;
                tsbtnDelete.Enabled = false;
                tsbtnCancel.Enabled = false;
                tsbtnAccept.Enabled = false;
                tsbtnReport.Enabled = false;
                lblItemID.Visible = false;

                tscboCompany.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
                tscboCompany.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
                tscboCompany.DropDownHeight = 400;
                tscboCompany.DropDownWidth = 300;
                tscboCompany.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                tscboCompany.IntegralHeight = false;
                tscboCompany.MaxDropDownItems = 9;
                tscboCompany.MergeAction = System.Windows.Forms.MergeAction.Insert;
                tscboCompany.Name = "tscboCompany";
                tscboCompany.Size = new System.Drawing.Size(p96W(250), p96H(25));
                tscboCompany.Sorted = true;

                tscboProduct.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
                tscboProduct.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
                tscboProduct.DropDownHeight = 400;
                tscboProduct.DropDownWidth = 200;
                tscboProduct.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                tscboProduct.IntegralHeight = false;
                tscboProduct.MaxDropDownItems = 9;
                tscboProduct.MergeAction = System.Windows.Forms.MergeAction.Insert;
                tscboProduct.Name = "tscboProduct";
                tscboProduct.Size = new System.Drawing.Size(p96W(200), p96H(25));
                tscboProduct.Sorted = true;

                this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
                this.errorProvider1.DataMember = null;


                btnCopyToNew.Click += btnCopyToNew_Click;
                btnBrowseImage.Image = GetImage(ButtonOp.Browse, p96H(20), p96H(20));
                btnBrowseImage.Click += btnBrowseImage_Click;
                //lblMBCode.Image = DrawingUtils.GetImage(ButtonOp.Expand, p96W(15), p96H(15));
                //lblAdditiveCode.Image = DrawingUtils.GetImage(ButtonOp.Expand, p96W(15), p96H(15));

                //set up buttons for deleting current Additive and Masterbatch properties
                System.Drawing.Image image = Properties.Resources.delete;
                //btnDeleteAdditive.Image = RescaleImage((Bitmap)image, btnDeleteAdditive.Width, btnDeleteAdditive.Height);
                //btnDeleteMB.Image = RescaleImage((Bitmap)image, btnDeleteMB.Width, btnDeleteMB.Height);

                //set up buttons for adding new rows to polymer, masterbatch and machine datagrids
                image = Properties.Resources.NewRow;
                btnAddNewMaterial.Image = RescaleImage((Bitmap)image, btnAddNewMaterial.Width, btnAddNewMaterial.Height);
                btnAddNewMachine.Image = RescaleImage((Bitmap)image, btnAddNewMachine.Width, btnAddNewMachine.Height);
                btnAddNewMB.Image = RescaleImage((Bitmap)image, btnAddNewMB.Width, btnAddNewMB.Height);
                btnAddNewMaterial.Click += btnAddNewMaterial_Click;
                btnAddNewMB.Click += btnAddNewMB_Click;
                btnAddNewMachine.Click += btnAddNewMachine_Click;

                // Create ToolTips for AddNewMachine and AddNewMaterial buttons
                ToolTip toolTip1 = new ToolTip();
                toolTip1.SetToolTip(this.btnAddNewMaterial, "Add new polymer");
                toolTip1.SetToolTip(this.btnAddNewMB, "Add new MasterBatch");
                toolTip1.SetToolTip(this.btnAddNewMachine, "Add new machine");

                dgvMBCode = new DataGridView();
                this.Controls.Add(dgvMBCode);
                dgvMBCode.Visible = false;
                dgvMBCode.Leave += dgvMBCode_Leave;
                dgvAdditive = new DataGridView();
                this.Controls.Add(dgvAdditive);
                dgvAdditive.Visible = false;
                dgvAdditive.Leave += dgvAdditive_Leave;
                GetDataSets();
            }
            catch (Exception ex) { }

        }

        private void btnAddNewMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                bsMaterialComp.AddNew();
                bsMaterialComp.EndEdit();
                bsMaterialComp.Position = bsMaterialComp.Count - 1;
                btnAddNewMaterial.Enabled = (dgvPolymer.Rows.Count < maxRowsMaterial);


                //btnAddNewMaterial.Enabled = dgvPolymer.Rows.Count < maxRowsMaterial;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnAddNewMB_Click(object sender, EventArgs e)
        {
            try
            {
                bsMBComp.AddNew();
                bsMBComp.EndEdit();
                bsMBComp.Position = bsMBComp.Count - 1;
                btnAddNewMB.Enabled = (dgvMasterBatchComp.Rows.Count < maxRowsMB);                
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }


        private void btnAddNewMachine_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("todo:  AddNewMachine");
            bsMachPref.AddNew();
            bsMachPref.EndEdit();
            bsMachPref.Position = bsMachPref.Count - 1;
            //DataTable ct = dsIMSpecificationForm.Relations["MachinePref"].ChildTable;
            //DataRow[] foundRows = ct.Select("ItemID = " + LastItemID.ToString());
            //int count = foundRows.Length;
            //btnAddNewMachine.Enabled = (count < maxRowsMachine);
            btnAddNewMachine.Enabled = dgvMachine.Rows.Count < maxRowsMachine;
        }

        private void SetFormState()
        {
            try
            {
                //Toolbar Customer dropdown
                DataTable dt = dsCompany.Tables[0].Copy();
                dt.TableName = "company";
                tscboCompany.ComboBox.DataSource = dt;
                tscboCompany.ComboBox.DisplayMember = "CUSTNAME";
                tscboCompany.ComboBox.ValueMember = "CustomerID";
                tscboCompany.ComboBox.SelectedIndexChanged += tscboCompany_SelectedIndexChanged;

                dt = dsProduct.Tables[0];
                dt.TableName = "product";
                tscboProduct.ComboBox.SelectedIndexChanged -= tscboProduct_SelectedIndexChanged;
                tscboProduct.ComboBox.DataSource = dt;
                tscboProduct.ComboBox.DisplayMember = "ITEMDESC";
                //tscboProduct.ComboBox.DisplayMember = "DisplayValue";            
                tscboProduct.ComboBox.ValueMember = "ItemID";
                dt.RowChanging += Dt_RowChanging;
                dt.TableNewRow += Dt_TableNewRow;
                if (CustomerFilterOn && LastCustomerID.HasValue && LastItemID.HasValue)
                {
                    //position to last selected customer and product
                    tscboCompany.ComboBox.SelectedIndexChanged -= tscboCompany_SelectedIndexChanged;
                    tscboCompany.ComboBox.SelectedValue = LastCustomerID;
                    SetProductFilter(LastCustomerID.Value, LastItemID.Value);
                    tscboCompany.ComboBox.SelectedIndexChanged += tscboCompany_SelectedIndexChanged;
                }
                else
                {
                    if (LastItemID.HasValue)
                    {
                        //position to last selected product, without customer filter
                        tscboCompany.ComboBox.SelectedIndexChanged -= tscboCompany_SelectedIndexChanged;
                        tscboCompany.ComboBox.SelectedIndex = -1;
                        tscboProduct.ComboBox.SelectedValue = LastItemID;
                        tscboCompany.ComboBox.SelectedIndexChanged += tscboCompany_SelectedIndexChanged;
                        RefreshCurrent();
                    }
                    else
                    {
                        //position to first product with no company filter
                        tscboCompany.ComboBox.SelectedIndexChanged -= tscboCompany_SelectedIndexChanged;
                        tscboCompany.ComboBox.SelectedIndex = -1;
                        tscboProduct.ComboBox.SelectedIndexChanged -= tscboProduct_SelectedIndexChanged;
                        tscboProduct.ComboBox.SelectedIndex = -1;
                        tscboProduct.ComboBox.SelectedIndexChanged += tscboProduct_SelectedIndexChanged;
                        tscboCompany.ComboBox.SelectedIndexChanged += tscboCompany_SelectedIndexChanged;
                        //tscboProduct.ComboBox.SelectedIndex = 0;
                        //RefreshCurrent();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void tsbtnAddNew_Click(object sender, EventArgs e)
        {
            if (NoEmptyItems())
            {
                btnCopyToNew.Enabled = false;
                bsManItems.AddNew();
                //MessageBox.Show(bsMouldSpec.Position.ToString());
            }
        }

        private void tsbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.bsManItems.Current != null)
                {
                    DataRowView rowView = (DataRowView)this.bsManItems.Current;
                    DataRow row = rowView.Row;

                    //MessageBox.Show(bsManItems.Position.ToString());

                    int itemID = int.TryParse(row["ItemID"].ToString(), out itemID) ? itemID : -1;
                    if (itemID == -1)
                        return;  //happens when moving to first record!!!

                    DialogResult dr = MessageBox.Show("Are you sure?", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.OK)
                    {
                        new ProductDetailsDAL().DeleteProductDetails(itemID);
                        this.LastCustomerID = null;
                        this.LastItemID = null;
                        this.DialogResult = DialogResult.Retry;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        //private void lblMBCode_Click(object sender, EventArgs e)
        //{
        //    if (!dgvMBCode.Visible)
        //    {
        //        //MessageBox.Show("todo:  Show MBGrid");
        //        //lblMBCode.Image = DrawingUtils.GetImage(ButtonOp.Collapse, p96W(15), p96H(15));

        //        //position grid below label cell row
        //        //int leftg = gpMaterial.Location.X + tableLayoutPanel5.Location.X + lblMBCode.Location.X;
        //        //Point cScreen = lblMBCode.PointToScreen(lblMBCode.Location);
        //        //Point fScreen = this.Location;
        //        //Point cFormRel = new Point(cScreen.X - fScreen.X, cScreen.Y - fScreen.Y - p96H(30));

        //        //int topg = cFormRel.Y + p96H(19);

        //        int leftg = 0, topg = 0;
        //        dgvMBCode.Location = new Point(leftg, topg);
        //        dgvMBCode.Visible = true;
        //        dgvMBCode.BringToFront();
        //        dgvMBCode.Focus();

        //        dgvMBCode.Location = new Point(leftg, topg);
        //        dgvMBCode.Visible = true;
        //        dgvMBCode.BringToFront();
        //        dgvMBCode.Focus();
        //    }
        //    else
        //    {
        //        MessageBox.Show("todo:  Collapse MBGrid");
        //        //lblMBCode.Image = DrawingUtils.GetImage(ButtonOp.Expand, p96W(15), p96H(15));
        //        dgvMBCode.Visible = false;
        //    }
        //}

        private void lblAdditiveCode_Click(object sender, EventArgs e)
        {
            if (!dgvAdditive.Visible)
            {
                //MessageBox.Show("todo:  Show AdditiveGrid");
                //lblAdditiveCode.Image = DrawingUtils.GetImage(ButtonOp.Collapse, p96W(15), p96H(15));

                //position grid below label cell row
                //int leftg = gpMaterial.Location.X + tableLayoutPanel5.Location.X + lblAdditiveCode.Location.X;

                // Point cScreen = lblAdditiveCode.PointToScreen(lblAdditiveCode.Location);
                //Point fScreen = this.Location;
                // Point cFormRel = new Point(cScreen.X - fScreen.X, cScreen.Y - fScreen.Y - p96H(30));

                //int topg = cFormRel.Y;// + p96H(19);


                int leftg = 0, topg = 0;
                dgvAdditive.Location = new Point(leftg, topg);
                dgvAdditive.Visible = true;
                dgvAdditive.BringToFront();
                dgvAdditive.Focus();
            }
            else
            {
                MessageBox.Show("todo:  Collapse AdditiveGrid");
                //lblAdditiveCode.Image = DrawingUtils.GetImage(ButtonOp.Expand, p96W(15), p96H(15));
                dgvAdditive.Visible = false;
            }
        }

        private void dgvAdditive_Leave(object sender, EventArgs e)
        {
            //lblAdditiveCode.Image = DrawingUtils.GetImage(ButtonOp.Expand, p96W(15), p96H(15));
            dgvAdditive.Visible = false;
            //MessageBox.Show("todo:  save Additive grid edits");
        }

        private void dgvMBCode_Leave(object sender, EventArgs e)
        {
            //lblMBCode.Image = DrawingUtils.GetImage(ButtonOp.Expand, p96W(15), p96H(15));
            dgvMBCode.Visible = false;
            //MessageBox.Show("todo:  save MBCode grid edits");
        }

        private void GetDataSets()
        {
            try
            {
                SpecificationDataEntryDAL dal = new SpecificationDataEntryDAL();
                dsCompany = dal.GetCustomerIndex("IM");
                dsProduct = dal.GetProductIndex("IM");
                dsIMSpecificationForm = dal.BuildFormDataSet();
                dsPolymerGrade = new MaterialCompDAL().SelectMaterialGrade();
                dsPolymerType = new MaterialCompDAL().SelectMaterial();
                dsMBColour = new MasterBatchCompDAL().SelectMasterBatchByColour();
            }
            catch (Exception ex) { }
        }

        private void BindControls()
        {
            try
            {
                viewManager = new DataViewManager(dsIMSpecificationForm);

                foreach (DataViewSetting viewSetting in viewManager.DataViewSettings)
                    viewSetting.ApplyDefaultSort = true;

                //Customer dropdown - form
                DataTable dt = dsCompany.Tables[0];
                cboCUSTNAME.DataSource = dt;
                cboCUSTNAME.DisplayMember = "CUSTNAME";
                cboCUSTNAME.ValueMember = "CustomerID";

                //create binding sources
                bsManItems = new BindingSource();  //injection mould products
                bsManItems.DataSource = dsIMSpecificationForm;
                bsManItems.DataMember = "MAN_Items";
                bsManItems.Sort = "ITEMDESC ASC";
                bsManItems.AddingNew += bsManItems_AddingNew;
                DataTable manItems = dsIMSpecificationForm.Tables["MAN_Items"];
                manItems.ColumnChanged -= new DataColumnChangeEventHandler(manItemsColumn_Changed);
                //manItems.ColumnChanging += new DataColumnChangeEventHandler(manItemsColumn_Changing);

                bsProductGrade = new BindingSource(); //for datagrid combobox value
                bsProductGrade.DataSource = dsIMSpecificationForm;
                bsProductGrade.DataMember = "ProductGrade";
                cboGradeID.DataSource = dsIMSpecificationForm.Tables["ProductGrade"];
                cboGradeID.DisplayMember = "Description";
                cboGradeID.ValueMember = "GradeID";
                cboGradeID.DataBindings.Add("SelectedValue", bsManItems, "GradeID");
                //cboGradeID.MouseUp += cboGradeID_MouseUp;
                cboGradeID.DropDownClosed += cboGradeID_DropDownClosed;

                //lookup for ProductGrade aka ProductCategory
                //dt = dsIMSpecificationForm.Relations["ProductGradeItem"].ParentTable;
                bsProductGradeItem = new BindingSource();
                bsProductGradeItem.DataSource = dsIMSpecificationForm.Relations["ProductGradeItem"].ParentTable;

                //black and white print label icon
                //picLabelIcon.DataBindings.Add("ImageLocation", bsProductGradeItem, "ImagePath");

                //coloured label icon
                picLabelIcon.DataBindings.Add("ImageLocation", bsProductGradeItem, "LabelIcon");

                bsMouldSpec = new BindingSource(); //injection mould specification
                bsMouldSpec.DataSource = dsIMSpecificationForm.Tables["InjectionMouldSpecification"];
                //bsMouldSpec.AddingNew += bsmouldSpec_AddingNew;

                //Material Composition
                bsMaterialComp = new BindingSource();  //material composition
                bsMaterialComp.DataSource = dsIMSpecificationForm.Tables["MaterialComp"];
                bsMaterialComp.AddingNew += bsMaterialComp_AddingNew;

                bsMaterialGrade = new BindingSource(); //material grade
                bsMaterialGrade.DataSource = dsIMSpecificationForm.Tables["MaterialGrade"];
                //txtAdditionalNotes.DataBindings.Add(new Binding("Text", bsMaterialGrade, "AdditionalNotes"));

                //bsMaterialGradeComp = new BindingSource(); //lookup for material grade
                //bsMaterialGradeComp.DataSource = dsIMSpecificationForm.Tables["MaterialComp"].ParentRelations["MaterialGradeComp"].ParentTable;

                bsMaterial = new BindingSource(); //material
                bsMaterial.DataSource = dsIMSpecificationForm.Tables["Material"];

                //MasterBatch
                bsMasterBatch = new BindingSource(); //populates datagrid dgvMasterbatch
                bsMasterBatch.DataSource = dsIMSpecificationForm.Tables["MasterBatch"];

                //masterbatch composition
                bsMBComp = new BindingSource();
                bsMBComp.DataSource = dsIMSpecificationForm.Tables["MasterBatchComp"];
                bsMBComp.AddingNew += bsMBComp_AddingNew;

                //lookup for Masterbatch code and Colour
                bsMBMBComp = new BindingSource();
                bsMBMBComp.DataSource = dsIMSpecificationForm.Tables["MasterBatchComp"].ParentRelations["MBMBComp"].ParentTable;
                //lblMBCode.DataBindings.Add(new Binding("Text", bsMBMBComp, "MBCode"));
                //txtMBColour.DataBindings.Add(new Binding("Text", bsMBMBComp, "MBColour"));

                //lookup for Additive and Additive Code
                bsAdditive = new BindingSource();
                bsAdditive.DataSource = dsIMSpecificationForm.Tables["Additive"];
                bsAddMBComp = new BindingSource();
                bsAddMBComp.DataSource = dsIMSpecificationForm.Tables["MasterBatchComp"].ParentRelations["AddMBComp"].ParentTable;
                //lblAdditiveCode.DataBindings.Add(new Binding("Text", bsAddMBComp, "AdditiveCode"));
                //txtAdditive.DataBindings.Add(new Binding("Text", bsAddMBComp, "Additive", false, DataSourceUpdateMode.OnPropertyChanged, null));

                //Machine preference
                bsMachPref = new BindingSource();
                bsMachPref.DataSource = dsIMSpecificationForm.Tables["MachinePref"];
                bsMachPref.AddingNew += bsMachPref_AddingNew;

                //Quality control
                bsQC = new BindingSource();
                bsQC.DataSource = dsIMSpecificationForm.Tables["QualityControl"];
                txtFinishedPTQC.DataBindings.Add(new Binding("Text", bsQC, "FinishedPTQC"));
                chkProductSample.DataBindings.Add(new Binding("Checked", bsQC, "ProductSample"));
                chkCertificateOfConformance.DataBindings.Add(new Binding("Checked", bsQC, "CertificateOfConformance"));

                //Customer
                bsCustomer = new BindingSource();
                bsCustomer.DataSource = dsIMSpecificationForm.Tables["Customer"];
                bsCustomerProducts = new BindingSource();
                bsCustomerProducts.DataSource = dsIMSpecificationForm.Tables["CustomerProduct"];
                bsCustomerProducts.Sort = "CustomerID ASC, ItemID ASC";
                cboCUSTNAME.DataBindings.Add(new Binding("SelectedValue", bsCustomerProducts, "CustomerID"));
                cboCUSTNAME.SelectedIndexChanged += cboCUSTNAME_SelectedIndexChanged;
                bsCustomerProducts.CurrentChanged += bsCustomerProducts_CurrentChanged;
                bsCustomerProducts.ListChanged += bsCustomerProducts_ListChanged;
                bsCustomerProducts.PositionChanged += bsCustomerProducts_PositionChanged; ;

                //Navigation toolbar
                bindingNavigator1.BindingSource = new BindingSource();
                bindingNavigator1.BindingSource = bsManItems;

                //bsNavigator = new BindingSource();                                           
                //bsNavigator.DataMember = dsIMSpecificationForm.Relations["CustProductItem"].RelationName;
                //bsNavigator.DataSource = bsCustomerProducts;
                //bindingNavigator1.BindingSource = bsNavigator;

                bsMouldSpec.AddingNew += bsMouldSpec_AddingNew; ;
                bsQC.AddingNew += bsQC_AddingNew;
                bsCustomerProducts.AddingNew += bsCustomerProducts_AddingNew;

                bsManItems.CurrentChanged += bsManItems_CurrentChanged;

                txtITEMNMBR.DataBindings.Add(new Binding("Text", bsManItems, "ITEMNMBR", false,
                    DataSourceUpdateMode.OnValidation));
                txtITEMDESC.DataBindings.Add(new Binding("Text", bsManItems, "ITEMDESC", false,
                    DataSourceUpdateMode.OnValidation));
                txtImageFile.DataBindings.Add(new Binding("Text", bsManItems, "ImageFile"));
                txtComponentWeight.DataBindings.Add(new Binding("Text", bsManItems, "ComponentWeight"));
                txtSprueRunnerTotal.DataBindings.Add(new Binding("Text", bsManItems, "SprueRunnerTotal"));
                txtTotalShotWeight.DataBindings.Add(new Binding("Text", bsManItems, "TotalShotWeight"));
                lblItemID.DataBindings.Add(new Binding("Text", bsManItems, "ItemID"));
                txtAltCode.DataBindings.Add(new Binding("Text", bsManItems, "AltCode"));
                txtAdditionalNotes.DataBindings.Add(new Binding("Text", bsManItems, "AdditionalNotes"));

                txtMouldNumber.DataBindings.Add(new Binding("Text", bsMouldSpec, "MouldNumber"));
                txtMouldOwner.DataBindings.Add(new Binding("Text", bsMouldSpec, "MouldOwner"));
                txtMouldLocation.DataBindings.Add(new Binding("Text", bsMouldSpec, "MouldLocation"));
                chkFamilyMould.DataBindings.Add(new Binding("Checked", bsMouldSpec, "FamilyMould"));
                chkAdditionalLabourReqd.DataBindings.Add(new Binding("Checked", bsMouldSpec, "AdditionalLabourReqd"));
                txtNoOfCavities.DataBindings.Add(new Binding("Text", bsMouldSpec, "NoOfCavities"));
                txtNoOfPart.DataBindings.Add(new Binding("Text", bsMouldSpec, "NoOfPart"));
                txtPartSummary.DataBindings.Add(new Binding("Text", bsMouldSpec, "PartSummary"));
                txtOperation.DataBindings.Add(new Binding("Text", bsMouldSpec, "Operation"));
                txtOtherFeatures.DataBindings.Add(new Binding("Text", bsMouldSpec, "OtherFeatures"));
                txtFixedHalf.DataBindings.Add(new Binding("Text", bsMouldSpec, "FixedHalf"));
                txtMovingHalf.DataBindings.Add(new Binding("Text", bsMouldSpec, "MovingHalf"));
                txtFixedHalfTemp.DataBindings.Add(new Binding("Text", bsMouldSpec, "FixedHalfTemp"));
                txtMovingHalfTemp.DataBindings.Add(new Binding("Text", bsMouldSpec, "MovingHalfTemp"));
                txtPremouldReq.DataBindings.Add(new Binding("Text", bsMouldSpec, "PremouldReq"));
                txtPostMouldReq.DataBindings.Add(new Binding("Text", bsMouldSpec, "PostMouldReq"));

                picImageFile.DataBindings.Add("ImageLocation", bsManItems, "ImageFile");
                tscboProduct.ComboBox.DataBindings.Add("SelectedItem", bsManItems, "ItemID");
                cboGradeID.DataBindings.Add(new Binding("SelectedItem", bsManItems, "GradeID"));

                dgvPolymer.DataSource = bsManItems;
                dgvPolymer.DataMember = "ItemMaterialComp";
                //this.dgvPolymer.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(this.dgvPolymer_DataBindingComplete);
                this.dgvPolymer.DataError += new DataGridViewDataErrorEventHandler(this.dgvPolymer_DataError);
                this.dgvPolymer.DefaultValuesNeeded += new DataGridViewRowEventHandler(this.dgvPolymer_DefaultValuesNeeded);

                dgvMasterBatchComp.DataSource = bsManItems;
                dgvMasterBatchComp.DataMember = "ItemMasterBatchComp";
                //this.dgvMachine.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvMachine_DataBindingComplete);
                this.dgvMasterBatchComp.DataError += dgvMasterBatchComp_DataError;
                this.dgvMasterBatchComp.DefaultValuesNeeded += dgvMasterBatchComp_DefaultValuesNeeded; ;

                dgvMachine.DataSource = bsManItems;
                dgvMachine.DataMember = "MachinePref";
                //this.dgvMachine.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvMachine_DataBindingComplete);
                this.dgvMachine.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvMachine_DataError);
                this.dgvMachine.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvMachine_DefaultValuesNeeded);


                dgvAdditive.DataSource = dsIMSpecificationForm.Tables["Additive"];//bsMBComp;
                                                                                  //dgvAdditive.DataMember = "AdditiveMBComp";
                dgvMBCode.DataSource = dsIMSpecificationForm.Tables["MasterBatch"];

                tscboCompany.ComboBox.SelectedIndexChanged += tscboCompany_SelectedIndexChanged;
                tscboProduct.ComboBox.SelectedIndexChanged += tscboProduct_SelectedIndexChanged;


                //**** testing navigation
                //bsManItems.Position = 0;
                //DataRowView rowView = (DataRowView)this.bsManItems.Current;
                //DataRow row = rowView.Row;
                //int itemID = (int)row["ItemID"];
                bsManItems.CurrentItemChanged += bsManItems_CurrentItemChanged;

                //set tooltip texts for additive delete and MB delete buttons
                //btnDeleteAdditive.MouseHover += btnDeleteAdditive_MouseHover;
                //btnDeleteAdditive.MouseLeave += btnDeleteAdditive_MouseLeave;
                //btnDeleteMB.MouseHover += btnDeleteMB_MouseHover;
                //btnDeleteMB.MouseLeave += btnDeleteMB_MouseLeave;
                //btnDeleteMB.Click += btnDeleteMB_Click;
                //btnDeleteAdditive.Click += btnDeleteAdditive_Click;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "BindControls", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bsCustomerProducts_AddingNew(object sender, AddingNewEventArgs e)
        {
            DataRowView rowView = (DataRowView)this.bsManItems.Current;
            DataRow row = rowView.Row;
            int itemID = (int)row["ItemID"];
            DataTable dt = (DataTable)bsCustomerProducts.DataSource;
            dt.Columns["ItemID"].DefaultValue = itemID;
            dt.Columns["CustomerProductID"].DefaultValue = -1;
        }

        private void bsQC_AddingNew(object sender, AddingNewEventArgs e)
        {
            DataRowView rowView = (DataRowView)this.bsManItems.Current;
            DataRow row = rowView.Row;
            int itemID = (int)row["ItemID"];
            DataTable dt = (DataTable)bsQC.DataSource;
            dt.Columns["ItemID"].DefaultValue = itemID;
            dt.Columns["last_updated_on"].DefaultValue = DateTime.MinValue;
            dt.Columns["last_updated_by"].DefaultValue = System.Environment.UserName;
        }

        private void bsMouldSpec_AddingNew(object sender, AddingNewEventArgs e)
        {
            DataRowView rowView = (DataRowView)this.bsManItems.Current;
            DataRow row = rowView.Row;            
            int itemID = (int)row["ItemID"];
            DataTable dt = (DataTable)bsMouldSpec.DataSource;
            dt.Columns["ItemID"].DefaultValue = itemID;
            dt.Columns["last_updated_on"].DefaultValue = DateTime.MinValue;
            dt.Columns["last_updated_by"].DefaultValue = System.Environment.UserName;
        }

        private void cboCUSTNAME_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCUSTNAME != null && cboCUSTNAME.SelectedIndex != -1)
                LastCustomerID = (int)cboCUSTNAME.SelectedValue;
        }

        private void bsCustomerProducts_ListChanged(object sender, ListChangedEventArgs e)
        {
            DataRowView rowView = (DataRowView)this.bsCustomerProducts.Current;
            if (this.bsCustomerProducts.Current != null) 
            {
                rowView = (DataRowView)this.bsCustomerProducts.Current;
                if (rowView != null) { DataRow row = rowView.Row;  }
            }                       
        }

        private void bsCustomerProducts_CurrentChanged(object sender, EventArgs e)
        {
            if (this.bsCustomerProducts.Current != null)
            {
                DataRowView rowView = (DataRowView)this.bsCustomerProducts.Current;
                if (rowView != null) { DataRow row = rowView.Row; }
            }                        
        }

        private void bsCustomerProducts_PositionChanged(object sender, EventArgs e)
        {
            try
            {                
                if (this.bsCustomerProducts.Current != null)
                {
                    DataRowView rowView = (DataRowView)this.bsCustomerProducts.Current;
                    if (rowView.Row != null) { DataRow row = rowView.Row; }
                }                            
            }
            catch { }            
        }

        private void dgvMasterBatchComp_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            int rowIndex = e.Row.Index;
            //dgvMasterBatchComp[rowIndex].Cells["MB123"].Value = rowIndex + 1;
            //dgvMasterBatchComp.Rows[rowIndex].Cells["MB123"].Value = rowIndex + 1;
        }
        
        private void dgvMasterBatchComp_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
           // throw new NotImplementedException();
        }

        private void dgvPolymer_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // Automatically maintains a Row Header Index Number 
            //   like the Excel row number, independent of sort order

            DataGridView grid = (DataGridView)sender;
            string rowIdx = "Polymer " + (e.RowIndex + 1).ToString();

            System.Drawing.Font rowFont = new System.Drawing.Font("Tahoma", 8.0f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);

            StringFormat centerFormat = new StringFormat();
            centerFormat.Alignment = StringAlignment.Center;
            centerFormat.LineAlignment = StringAlignment.Center;

            Rectangle headerBounds = new Rectangle(
                e.RowBounds.Left, e.RowBounds.Top,
                grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, rowFont, SystemBrushes.ControlText,
                headerBounds, centerFormat);
        }

        private void bsMaterialComp_AddingNew(object sender, AddingNewEventArgs e)
        {
            DataRowView rowView = (DataRowView)this.bsManItems.Current;
            DataRow row = rowView.Row;
            //MessageBox.Show(row["ItemID"].ToString());
            int itemID = (int)row["ItemID"];
            DataTable dt = (DataTable)bsMaterialComp.DataSource;
            dt.Columns["ItemID"].DefaultValue = itemID;
            dt.Columns["MaterialGradeID"].DefaultValue = -1;
            dt.Columns["MaterialCompID"].DefaultValue = -1;
            dt.Columns["last_updated_on"].DefaultValue = DateTime.MinValue;
            dt.Columns["last_updated_by"].DefaultValue = System.Environment.UserName;
        }

        private void bsMBComp_AddingNew(object sender, AddingNewEventArgs e)
        {
            DataRowView rowView = (DataRowView)this.bsManItems.Current;
            DataRow row = rowView.Row;
            //MessageBox.Show(row["ItemID"].ToString());
            int itemID = (int)row["ItemID"];
            DataTable dt = (DataTable)bsMBComp.DataSource;
            dt.Columns["ItemID"].DefaultValue = itemID;
            dt.Columns["MB123"].DefaultValue = dgvMasterBatchComp.Rows.Count + 1;          
            dt.Columns["MBID"].DefaultValue = -1;
            dt.Columns["MBCompID"].DefaultValue = -1;          
            dt.Columns["last_updated_on"].DefaultValue = DateTime.MinValue;
            dt.Columns["last_updated_by"].DefaultValue = System.Environment.UserName;
        }

        private void bsMachPref_AddingNew(object sender, AddingNewEventArgs e)
        {
            DataRowView rowView = (DataRowView)this.bsManItems.Current;
            DataRow row = rowView.Row;
            //MessageBox.Show(row["ItemID"].ToString());
            int itemID = (int)row["ItemID"];
            DataTable dt = (DataTable)bsMachPref.DataSource;
            dt.Columns["ItemID"].DefaultValue = itemID;
            dt.Columns["last_updated_on"].DefaultValue = DateTime.MinValue;
            dt.Columns["last_updated_by"].DefaultValue = System.Environment.UserName;

            DataTable ct = dsIMSpecificationForm.Relations["MachinePref"].ChildTable;
            DataRow[] foundRows = ct.Select("ItemID = " + itemID.ToString());
            int count = foundRows.Length;
            string machineABC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string curABC = machineABC.Substring(count, 1);
            dt.Columns["MachineABC"].DefaultValue = curABC;

            //btnAddNewMachine.Enabled = (count < maxRowsMachine);
        }

        private void btnDeleteAdditive_Click(object sender, EventArgs e)
        {
            try
            {
                //soft delete.  MasterBatch and Additive keys are on same row in MasterBatchComp
                DataRowView rowView = (DataRowView)this.bsMBComp.Current;
                if (rowView != null)
                {
                    DataRow row = rowView.Row;
                    row["AdditiveID"] = DBNull.Value;
                    row.EndEdit();
                    RefreshCurrent();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteMB_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView rowView = (DataRowView)this.bsMBComp.Current;
                if (rowView != null)
                {
                    DataRow row = rowView.Row;
                    row.Delete();
                    RefreshCurrent();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        //private void btnDeleteAdditive_MouseHover(object sender, EventArgs e)
        //{
        //    lblDeleteAdditiveTooltip.Visible = true;
        //}

        //private void btnDeleteAdditive_MouseLeave(object sender, EventArgs e)
        //{
        //    lblDeleteAdditiveTooltip.Visible = false;
        //}

        //private void btnDeleteMB_MouseHover(object sender, EventArgs e)
        //{
        //    lblDeleteMBtooltip.Visible = true;
        //}

        //private void btnDeleteMB_MouseLeave(object sender, EventArgs e)
        //{
        //    lblDeleteMBtooltip.Visible = false;
        //}

        private void cboGradeID_DropDownClosed(object sender, EventArgs e)
        {
            if (cboGradeID.SelectedIndex != -1)
            {
                int gradeID = (int)cboGradeID.SelectedValue;
                DataRow[] row = dsIMSpecificationForm.Tables["ProductGrade"].Select("GradeID=" + gradeID.ToString());
                if (row.Length != 0)
                {
                    string labelIcon = row[0]["LabelIcon"].ToString();
                    string imagePath = row[0]["ImagePath"].ToString();
                    picLabelIcon.ImageLocation = labelIcon.Length > 0 ? labelIcon : imagePath;
                }
            }
        }





        private void bsManItems_CurrentItemChanged(object sender, EventArgs e)
        {

        }

        //private void cboCUSTNAME_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cboCUSTNAME.SelectedIndex != -1)
        //    {
        //        EditCustomerProduct();
        //    }
        //}

        private void EditCustomerProduct()
        {
            //locate selected customer in Customer products
            int custID = (int)cboCUSTNAME.SelectedValue;
            DataTable dt = dsIMSpecificationForm.Tables["CustomerProduct"];

            //locate current product
            DataRowView rowView = (DataRowView)this.bsManItems.Current;
            DataRow row = rowView.Row;
            int itemID = (int)row["ItemID"];

            DataView dv = new DataView(dt, "CustomerID = " + custID.ToString() + " AND ItemID = " + itemID.ToString(), "CustomerID", DataViewRowState.CurrentRows);

            if (dv.Count == 0)
            {
                //add this product for selected customer
                var newRow = dt.NewRow();
                newRow["CustomerID"] = custID;
                newRow["ItemID"] = itemID;
                dt.Rows.Add(newRow);
            }
            cboCUSTNAME.SelectedValue = custID;
            //cboCUSTNAME.SelectedIndexChanged += cboCUSTNAME_SelectedIndexChanged;

            //enable alternative code input for Angel or CP
            string testMatching = "Angel Products | Consolidated Plastics";
            string testCompany = cboCUSTNAME.Text.Trim();//dr["CUSTNAME"].ToString().Trim();
            if (testMatching.Contains(testCompany))
            {
                txtAltCode.Enabled = true;
                lblAltCode.Enabled = true;
            }
            else
            {
                txtAltCode.Enabled = false;
                lblAltCode.Enabled = false;
            }
        }

        private void manItemsColumn_Changing(object sender, DataColumnChangeEventArgs e)
        {
            string msg = e.Column.ColumnName + " = " + e.Row[e.Column.ColumnName].ToString() + "; Proposed value="
                + e.ProposedValue.ToString();
            //Console.WriteLine(msg);
            if (e.Column.ColumnName == "ImageFile"
                && e.Row[e.Column.ColumnName].ToString().Length == 0
                && e.ProposedValue.ToString().Length == 0)
                bsManItems.CancelEdit();

        }

        private void manItemsColumn_Changed(object sender, DataColumnChangeEventArgs e)
        {
            //string msg = e.Column.ColumnName + "=" + e.Row[e.Column.ColumnName].ToString() + "; Original value="
            //    + e.Row[e.Column.ColumnName.ToString(), DataRowVersion.Original];
            //Console.WriteLine(msg);
            if (e.Column.ColumnName == "ImageFile" && e.Row[e.Column.ColumnName].ToString().Length == 0)
            {
                try
                {
                    if (e.Row[e.Column.ColumnName.ToString(), DataRowVersion.Original].ToString().Length == 0)
                    {
                        bsManItems.CancelEdit();
                    }
                }
                catch
                {
                    bsManItems.CancelEdit();
                }
            }
        }

        private void bsManItems_AddingNew(object sender, AddingNewEventArgs e)
        {
            try
            {
                //cboCUSTNAME.SelectedIndexChanged
                //bsManItems.CurrentChanged -= bsManItems_CurrentChanged;
                //bsManItems.RemoveFilter();
                //bsManItems.CurrentChanged += bsManItems_CurrentChanged;
                //tscboCompany.ComboBox.SelectedIndexChanged -= tscboCompany_SelectedIndexChanged;
                //ignoreZero = true;                //
                //tscboCompany.SelectedIndex = -1;  //SelectedIndexChanged event resets -1 to zero
                //ignoreZero = false;               //  
                tscboProduct.SelectedIndexChanged -= tscboProduct_SelectedIndexChanged;
                tscboProduct.SelectedIndex = -1;
                cboCUSTNAME.SelectedIndex = -1;
                //cboCUSTNAME.SelectedIndexChanged += cboCUSTNAME_SelectedIndexChanged;

                //create new key
                newItemID--; // -ve increment flags new records

                //Assign default keys
                //Items
                DataTable dt = dsIMSpecificationForm.Tables["MAN_Items"];
                dt.Columns["ItemID"].DefaultValue = newItemID;
                dt.Columns["ProductType"].DefaultValue = "IM";                
                dt.Columns["ITEMNMBR"].DefaultValue = "[NEW]";
                dt.Columns["last_updated_on"].DefaultValue = DateTime.MinValue;
                dt.Columns["last_updated_by"].DefaultValue = System.Environment.UserName;
                //Injection Mould
                dt = dsIMSpecificationForm.Tables["InjectionMouldSpecification"];
                dt.Columns["ItemID"].DefaultValue = newItemID;
                dt.Columns["MouldID"].DefaultValue = newItemID;
                dt.Columns["FamilyMould"].DefaultValue = false;                
                dt.Columns["AdditionalLabourReqd"].DefaultValue = false;
                dt.Columns["last_updated_on"].DefaultValue = DateTime.MinValue;
                dt.Columns["last_updated_by"].DefaultValue = System.Environment.UserName;
                //Quality Control
                dt = dsIMSpecificationForm.Tables["QualityControl"];
                dt.Columns["ItemID"].DefaultValue = newItemID;
                dt.Columns["QualityControlID"].DefaultValue = newItemID;
                dt.Columns["ProductSample"].DefaultValue = false;
                dt.Columns["CertificateOfConformance"].DefaultValue = false;
                dt.Columns["last_updated_on"].DefaultValue = DateTime.MinValue;
                dt.Columns["last_updated_by"].DefaultValue = System.Environment.UserName;

                //Material Composition
                dt = dsIMSpecificationForm.Tables["MaterialComp"];
                dt.Columns["ItemID"].DefaultValue = newItemID;
                dt.Columns["Polymer123"].DefaultValue = 1;
                dt.Columns["MaterialGradeID"].DefaultValue = newItemID;
                dt.Columns["MaterialCompID"].DefaultValue = newItemID;
                dt.Columns["last_updated_on"].DefaultValue = DateTime.MinValue;
                dt.Columns["last_updated_by"].DefaultValue = System.Environment.UserName;

                //Material Grade
                dt = dsIMSpecificationForm.Tables["LookupMaterialGrade"];
                dt.Columns["MaterialGradeID"].DefaultValue = newItemID;
                //dt.Columns["last_updated_on"].DefaultValue = DateTime.MinValue;
                //dt.Columns["last_updated_by"].DefaultValue = System.Environment.UserName;

                //Customer Products
                dt = dsIMSpecificationForm.Tables["CustomerProduct"];
                dt.Columns["ItemID"].DefaultValue = newItemID;
                dt.Columns["CustomerID"].DefaultValue = LastCustomerID.Value;
                dt.Columns["CustomerProductID"].DefaultValue = -1; 
                
                //Machine preference
                dt = dsIMSpecificationForm.Tables["MachinePref"];
                dt.Columns["ItemID"].DefaultValue = newItemID;
                dt.Columns["MachineABC"].DefaultValue = "A";;
                dt.Columns["last_updated_on"].DefaultValue = DateTime.MinValue;
                dt.Columns["last_updated_by"].DefaultValue = System.Environment.UserName;
                dt.Columns["last_updated_on"].DefaultValue = DateTime.MinValue;
                dt.Columns["last_updated_by"].DefaultValue = System.Environment.UserName;

                //Masterbatch composition
                dt = dsIMSpecificationForm.Tables["MasterBatchComp"];
                dt.Columns["ItemID"].DefaultValue = newItemID;
                dt.Columns["MBID"].DefaultValue = newItemID;
                dt.Columns["MBCompID"].DefaultValue = newItemID;
                dt.Columns["MB123"].DefaultValue = 1;
                dt.Columns["last_updated_on"].DefaultValue = DateTime.MinValue;
                dt.Columns["last_updated_by"].DefaultValue = System.Environment.UserName;

                curPolymerNo = 0;
                tscboCompany.ComboBox.SelectedIndexChanged += tscboCompany_SelectedIndexChanged;
                tscboProduct.SelectedIndexChanged += tscboProduct_SelectedIndexChanged;
                                                             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private void bsmouldSpec_AddingNew(object sender, AddingNewEventArgs e)
        //{
        //    DataRowView rowView = (DataRowView)this.bsManItems.Current;
        //    DataRow row = rowView.Row;
        //    //MessageBox.Show(row["ItemID"].ToString());
        //    int itemID = (int)row["ItemID"];
        //    DataTable dt = (DataTable) bsMouldSpec.DataSource;
        //    dt.Columns["ItemID"].DefaultValue = itemID;
        //}

        //private void bsQC_AddingNew(object sender, AddingNewEventArgs e)
        //{
        //    DataRowView rowView = (DataRowView)this.bsManItems.Current;
        //    DataRow row = rowView.Row;
        //    //MessageBox.Show(row["ItemID"].ToString());
        //    int itemID = (int)row["ItemID"];
        //    DataTable dt = (DataTable)bsQC.DataSource;
        //    dt.Columns["ItemID"].DefaultValue = itemID;
        //}

        //private void MBComp_AddingNew(object sender, AddingNewEventArgs e)
        //{
        //    //DataRowView rowView = (DataRowView)this.bsManItems.Current;
        //    //DataRow row = rowView.Row;
        //    ////MessageBox.Show(row["ItemID"].ToString());
        //    //int itemID = (int)row["ItemID"];
        //    //DataTable dt = (DataTable)bsMBComp.DataSource;
        //    //dt.Columns["ItemID"].DefaultValue = itemID;
        //}

        //private void bsMachPref_AddingNew(object sender, AddingNewEventArgs e)
        //{
        //    //DataRowView rowView = (DataRowView)this.bsManItems.Current;
        //    //DataRow row = rowView.Row;
        //    ////MessageBox.Show(row["ItemID"].ToString());
        //    //int itemID = (int)row["ItemID"];
        //    //DataTable dt = (DataTable) bsMachPref.DataSource; //dsIMSpecificationForm.Tables["MachinePref"];
        //    //dt.Columns["ItemID"].DefaultValue = itemID;
        //}

        //private void bsMaterialComp_AddingNew(object sender, AddingNewEventArgs e)
        //{
        //    //DataRowView rowView = (DataRowView)this.bsManItems.Current;
        //    //DataRow row = rowView.Row;
        //    ////MessageBox.Show(row["ItemID"].ToString());
        //    //int itemID = (int)row["ItemID"];
        //    //DataTable dt = (DataTable)bsMaterialComp.DataSource;
        //    //dt.Columns["ItemID"].DefaultValue = itemID;
        //    //dt.Columns["MaterialGradeID"].DefaultValue = itemID;
        //}

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

                    //reset product filter dropdown index
                    if (bsManItems.Filter != null)
                    {
                        int itemID = (int)dr["ItemID"];
                        tscboProduct.SelectedIndexChanged -= tscboProduct_SelectedIndexChanged;
                        tscboProduct.ComboBox.SelectedValue = itemID;
                        tscboProduct.SelectedIndexChanged += tscboProduct_SelectedIndexChanged;
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
                    tsbtnReport.Enabled = false;

                    //add new CustomerProduct
                    DataTable dt = dsIMSpecificationForm.Tables["CustomerProduct"];
                    DataView view = new DataView(dt, "", "ItemID", DataViewRowState.CurrentRows);
                    int indexNotFound = view.Find(curItemID);
                    if (indexNotFound == -1)
                    {
                        bsCustomerProducts.SuspendBinding();
                        var newRow = dt.NewRow();
                        dt.Rows.Add(newRow);
                        //MessageBox.Show(newRow["ItemID"].ToString() + ", " + newRow["MouldNumber"].ToString());
                        //bsMouldSpec.ResetBindings(false);
                        bsCustomerProducts.ResumeBinding();
                    }


                    //add new Injection Mould
                    dt = dsIMSpecificationForm.Tables["InjectionMouldSpecification"];
                    view = new DataView(dt, "", "ItemID", DataViewRowState.CurrentRows);
                    indexNotFound = view.Find(curItemID);
                    if (indexNotFound == -1)
                    {
                        bsMouldSpec.SuspendBinding();
                        var newRow = dt.NewRow();
                        dt.Rows.Add(newRow);
                        //MessageBox.Show(newRow["ItemID"].ToString() + ", " + newRow["MouldNumber"].ToString());
                        //bsMouldSpec.ResetBindings(false);
                        bsMouldSpec.ResumeBinding();
                    }

                    //add new Quality Control
                    dt = dsIMSpecificationForm.Tables["QualityControl"];
                    view = new DataView(dt, "", "ItemID", DataViewRowState.CurrentRows);
                    indexNotFound = view.Find(curItemID);
                    if (indexNotFound == -1)
                    {
                        bsQC.SuspendBinding();
                        var newRow = dt.NewRow();
                        dt.Rows.Add(newRow);
                        int indexFound = view.Find(curItemID);
                        if (indexFound != -1)
                        {
                            bsQC.ResumeBinding();
                        }
                    }

                    //add new Material Composition
                    dt = dsIMSpecificationForm.Tables["MaterialComp"];
                    view = new DataView(dt, "", "ItemID", DataViewRowState.CurrentRows);
                    indexNotFound = view.Find(curItemID);
                    if (indexNotFound == -1)
                    {
                        bsMaterialComp.SuspendBinding();
                        var newRow = dt.NewRow();
                        dt.Rows.Add(newRow);
                        int indexFound = view.Find(curItemID);
                        if (indexFound != -1)
                        {
                            bsMaterialComp.ResumeBinding();
                        }
                    }

                    //add new Masterbatch composition
                    dt = dsIMSpecificationForm.Tables["MasterBatchComp"];
                    view = new DataView(dt, "", "ItemID", DataViewRowState.CurrentRows);
                    indexNotFound = view.Find(curItemID);
                    if (indexNotFound == -1)
                    {
                        bsMBComp.SuspendBinding();
                        var newRow = dt.NewRow();
                        dt.Rows.Add(newRow);
                        //bsQC.ResetBindings(false);
                        bsMBComp.ResumeBinding();
                    }

                    //add new Machine preference
                    dt = dsIMSpecificationForm.Tables["MachinePref"];
                    view = new DataView(dt, "", "ItemID", DataViewRowState.CurrentRows);
                    indexNotFound = view.Find(curItemID);
                    if (indexNotFound == -1)
                    {
                        bsMachPref.SuspendBinding();
                        var newRow = dt.NewRow();
                        dt.Rows.Add(newRow);
                        int indexFound = view.Find(curItemID);
                        if (indexFound != -1)
                        {
                            bsMachPref.ResumeBinding();
                        }
                    }
                }


                //set polymer datagridview row limit
                DataView dv = drv.CreateChildView("ItemMaterialComp", true);
                //int maxRows = 3;
                //dgvPolymer.AllowUserToAddRows = (dv.ToTable().Rows.Count < maxRows)
                btnAddNewMaterial.Enabled = (dv.ToTable().Rows.Count < maxRowsMaterial)
                ? true
                : false;

                //set MasterBatchComp row limit
                dv = drv.CreateChildView("ItemMasterBatchComp", true);
                btnAddNewMB.Enabled = (dv.ToTable().Rows.Count < maxRowsMB)
               ? true
               : false;

                //Set machine datagridview row limit
                //maxRows = 3;
                //dgvMachine.AllowUserToAddRows = (dv.ToTable().Rows.Count < maxRows)
                dv = drv.CreateChildView("MachinePref", true);
                btnAddNewMachine.Enabled = (dv.ToTable().Rows.Count < maxRowsMachine)
                ? true
                : false;

                RefreshCurrent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bsMouldSpec_CurrentChanged(object sender, EventArgs e)
        {
            //DataRowView drv = (DataRowView)bsMouldSpec.Current;

            //DataRow row = drv.Row;
            //MessageBox.Show(row["ItemID"].ToString() + ", " + row["MouldNumber"].ToString());
        }

        private void Dt_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            OnNewRow = true;
        }

        private void Dt_RowChanging(object sender, DataRowChangeEventArgs e)
        {
            //MessageBox.Show(e.Row["FreightCompany"].ToString(), e.Action.ToString());
            if (e.Action == DataRowAction.Change)
                OnNewRow = false;
        }

        private bool NoEmptyItems()
        {
            try
            {
                DataRowView drv = (DataRowView)bsManItems.Current;
                if (drv.IsNew)
                    return false;
                if ((int)drv.Row["ItemID"] < 0)
                    return false;

                DataView dv = new DataView(dsIMSpecificationForm.Tables["MAN_Items"], "", "ItemID",
                                        DataViewRowState.CurrentRows);
                dv.RowFilter = "Isnull(ITEMNMBR,'') = ''";
                if (dv.Count > 0)
                    return false;
                else
                    return true;
            }
            catch { return false; }
        }

        private void tscboProduct_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (tscboProduct.SelectedIndex != -1)
            {
                int itemID = (int)tscboProduct.ComboBox.SelectedValue;
                if (bsManItems == null)
                {
                    {
                        BindControls();
                        FormatPolymerGrid();
                        FormatAdditiveGrid();
                        FormatMBGrid();
                        FormatMasterBatchCompGrid();
                        FormatMachineGrid();
                        EnableGroups(true);
                    }
                }
                int itemIndex = bsManItems.Find("ItemID", itemID);
                if (itemID != -1)
                {
                    tsbtnReport.Enabled = true;
                    bsManItems.Position = itemIndex;
                }
            }
        }

        private void tscboCompany_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            // ## gotcha !!! ##
            // toolstrip combo automatically changes selected index to 0 after detecting value of -1
            // workaround:  set ignoreZero to true !!!
            // ###
            if (tscboCompany.SelectedIndex == -1 || 
               (tscboCompany.SelectedIndex == 0 && ignoreZero))
                return;
            else if (CustomerFilterOn)
            {                
                int custID = (int)tscboCompany.ComboBox.SelectedValue;
                SetProductFilter(custID); 
                //ignoreZero = false;
            }
            else
                CustomerFilterOn = false;
        }

        private void SetProductFilter(int custID, int itemID = 0)
        {
            try
            {
                LastCustomerID = custID;
                CustomerFilterOn = true;
                DataTable dt = dsIMSpecificationForm.Tables["CustomerProduct"].Copy();
                DataView dv = new DataView(dt, "CustomerID = " + custID.ToString(), "CustomerID", DataViewRowState.CurrentRows);
                DataTable dt1 = dv.ToTable();
                DataTable dt2 = dsProduct.Tables["product"];
                dt2.DefaultView.RowFilter = "";
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
                    dt2.DefaultView.RowFilter = string.Format("ItemID in ({0})", string.Join(",", ids));
                }                
                tscboProduct.SelectedIndexChanged -= tscboProduct_SelectedIndexChanged;                
                tscboProduct.ComboBox.DataBindings.Clear();
                tscboProduct.ComboBox.SelectedIndex = -1;
                ignoreZero = true;  //exits SelectedIndexChanged 
                tscboProduct.ComboBox.DataSource = dt2;
                tscboProduct.ComboBox.ValueMember = "ItemID";
                tscboProduct.ComboBox.DisplayMember = "ITEMDESC";
                
                tscboProduct.ComboBox.SelectedIndexChanged += tscboProduct_SelectedIndexChanged;
                if (dt1.Rows.Count > 0)
                {
                    ignoreZero = false;
                    if (bsManItems == null)
                    {
                        BindControls();
                        FormatPolymerGrid();
                        FormatAdditiveGrid();
                        FormatMBGrid();
                        FormatMasterBatchCompGrid();
                        FormatMachineGrid();                        
                    }
                    EnableGroups(true);
                    bsManItems.Filter = dt2.DefaultView.RowFilter;
                    if (itemID != 0)
                        if (dt2.DefaultView.RowFilter.Contains(itemID.ToString()))
                        {
                            tscboProduct.ComboBox.SelectedValue = itemID;
                        }
                        else
                            tscboProduct.ComboBox.SelectedIndex = 0;
                    else
                        //position to first item within selected company
                        tscboProduct.ComboBox.SelectedIndex = 0;
                }
                //bsManItems.Filter = dv2.RowFilter;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SetProductFilter", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void EnableGroups(bool flag)
        {
            gpGeneral.Enabled = flag;
            gpImage.Enabled = flag;
            gpMaterial.Enabled = flag;
            gpMasterBatch.Enabled = flag;   
            dgvPolymer.Visible = flag;
            dgvMachine.Visible = flag;
            gpMould.Enabled = flag;
            gpWeight.Enabled = flag;
            tsbtnAddNew.Enabled = flag;
            tsbtnDelete.Enabled = flag;
            tsbtnCancel.Enabled = flag;
            tsbtnAccept.Enabled = flag;
        }

        private void FormatPolymerGrid()
        {
            try
            {
                //dgvPolymer.UserAddedRow += dgvPolymer_RowCountChanged;
                dgvPolymer.UserDeletedRow += dgvPolymer_UserDeletedRow; ;
                dgvPolymer.EditingControlShowing += dgvPolymer_EditingControlShowing;
                dgvPolymer.CellEndEdit += dgvPolymer_CellEndEdit;
                dgvPolymer.CellStateChanged += new DataGridViewCellStateChangedEventHandler(dgvPolymer_CellStateChanged);
                dgvPolymer.AllowUserToAddRows = false; //handled by btnAddNewMaterial instead;  prevents dgv adding multiple rows
                this.dgvPolymer.RowPostPaint += dgvPolymer_RowPostPaint;
                dgvPolymer.AllowUserToDeleteRows = true;
                dgvPolymer.UserDeletingRow += dgvPolymer_UserDeletingRow;


                DataGridViewCellStyle style = dgvPolymer.ColumnHeadersDefaultCellStyle;
                style.BackColor = Color.LightSteelBlue;
                style.ForeColor = Color.Black;
                style.Font = new Font(dgvPolymer.Font, FontStyle.Regular);
                dgvPolymer.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                dgvPolymer.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                dgvPolymer.GridColor = SystemColors.ActiveBorder;
                dgvPolymer.EnableHeadersVisualStyles = false;
                dgvPolymer.AutoGenerateColumns = true;
                dgvPolymer.ColumnHeadersHeight = p96H(19);
                dgvPolymer.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dgvPolymer.AllowUserToResizeRows = false;
                dgvPolymer.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                dgvPolymer.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                dgvPolymer.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                dgvPolymer.Height = p96H(70);
                dgvPolymer.RowHeadersWidth = p96W(77);
                //dgvPolymer.RowTemplate.Height = p96H(19);
                //Padding newPadding = new Padding(0, 0, 5, 0);
                //dgvPolymer.RowHeadersDefaultCellStyle.Padding = newPadding;
                //dgvPolymer.Rows[dgvPolymer.NewRowIndex].MinimumHeight = p96H(19);


                //insert a dynamic combobox column for Material
                DataGridViewComboBoxColumn cbcMaterial = new DataGridViewComboBoxColumn();
                DataTable dt = dsIMSpecificationForm.Tables["LookupMaterialGrade"];
                cbcMaterial.DataSource = dt;
                cbcMaterial.ValueMember = "MaterialGradeID";
                cbcMaterial.DisplayMember = "Material";
                cbcMaterial.Name = "Material";
                cbcMaterial.DataPropertyName = "MaterialGradeID";
                cbcMaterial.DefaultCellStyle.BackColor = SystemColors.Control;
                cbcMaterial.ReadOnly = true;
                dgvPolymer.Columns.Insert(3, cbcMaterial);
                dgvPolymer.Columns["Material"].HeaderText = "Material";
                cbcMaterial.DisplayStyleForCurrentCellOnly = true;
                cbcMaterial.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

                /*
                 * change request Trish 29/04/25:  
                 * -- remove Additional notes from MaterialGrade;    
                 * -- move into MAN_Items
                 * -- display in MATERIAL group under MasterBatch grid
                 * 
                //Create a dynamic combobox column for Additional Notes
                DataGridViewComboBoxColumn cbcAdditionalNotes = new DataGridViewComboBoxColumn();
                //dt = dsIMSpecificationForm.Tables["LookupMaterialComp"];
                cbcAdditionalNotes.DataSource = dt;
                cbcAdditionalNotes.ValueMember = "MaterialGradeID";
                cbcAdditionalNotes.DisplayMember = "AdditionalNotes";
                cbcAdditionalNotes.Name = "AdditionalNotes";
                cbcAdditionalNotes.DataPropertyName = "MaterialGradeID";
                cbcAdditionalNotes.DefaultCellStyle.BackColor = SystemColors.Control;
                cbcAdditionalNotes.ReadOnly = true;
                dgvPolymer.Columns.Insert(9, cbcAdditionalNotes);
                dgvPolymer.Columns["AdditionalNotes"].HeaderText = "Additional Notes";
                cbcAdditionalNotes.DisplayStyleForCurrentCellOnly = true;
                cbcAdditionalNotes.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                */

                dgvPolymer.Columns["MaterialCompID"].Visible = false;
                dgvPolymer.Columns["ItemID"].Visible = false;
                dgvPolymer.Columns["MaterialGradeID"].Visible = false;
                //dgvPolymer.Columns["MaterialID"].Visible = false;                
                dgvPolymer.Columns["last_updated_by"].Visible = false;
                dgvPolymer.Columns["last_updated_on"].Visible = false;
                dgvPolymer.ReadOnly = false;
                //dgvPolymer.AllowUserToAddRows = true;

                //Set dropdown column for polymer #
                DataGridViewComboBoxColumn cbcPolymerNo = new DataGridViewComboBoxColumn();
                MaterialCompDAL dal = new MaterialCompDAL();
                DataSet ds = dal.GetPolymer123();
                DataTable dtPNo = ds.Tables[0];
                cbcPolymerNo.DataSource = dtPNo;
                cbcPolymerNo.ValueMember = "PolymerNo";
                cbcPolymerNo.DisplayMember = "PolymerNo";
                cbcPolymerNo.Name = "Polymer123";
                cbcPolymerNo.DataPropertyName = "Polymer123";
                dgvPolymer.Columns.Remove("Polymer123");
                dgvPolymer.Columns.Insert(4, cbcPolymerNo);
                dgvPolymer.Columns["Polymer123"].HeaderText = "#";
                cbcPolymerNo.DisplayStyleForCurrentCellOnly = true;
                cbcPolymerNo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

                //above dropdown column to be removed in future version.  Its data is now shown as header row text
                dgvPolymer.Columns["Polymer123"].Visible = false;

                //Set dropdown column for Polymer type
                //DataGridViewComboBoxColumn cbcPolymer = new DataGridViewComboBoxColumn();
                //dt = dsPolymerType.Tables[0];
                //cbcPolymer.DataSource = dt;
                //cbcPolymer.ValueMember = "MaterialID";
                //cbcPolymer.DisplayMember = "Description";
                //cbcPolymer.Name = "Material";
                //cbcPolymer.DataPropertyName = "MaterialID";
                //dgvPolymer.Columns.Insert(3, cbcPolymer);
                //dgvPolymer.Columns["Material"].HeaderText = "Material";
                //dgvPolymer.Columns["Material"].ReadOnly = true;
                //dgvPolymer.Columns["Material"].DefaultCellStyle.BackColor = SystemColors.Control;
                //cbcPolymer.DisplayStyleForCurrentCellOnly = true;
                //cbcPolymer.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

                //Set dropdown column for polymer Grade
                DataGridViewComboBoxColumn cbcPolymerGrade = new DataGridViewComboBoxColumn();
                //dt = dsPolymerGrade.Tables[0];
                cbcPolymerGrade.DataSource = dt;
                cbcPolymerGrade.ValueMember = "MaterialGradeID";
                cbcPolymerGrade.DisplayMember = "MaterialGrade";
                cbcPolymerGrade.Name = "Grade";
                cbcPolymerGrade.DataPropertyName = "MaterialGradeID";
                dgvPolymer.Columns.Insert(5, cbcPolymerGrade);
                dgvPolymer.Columns["Grade"].HeaderText = "Grade";
                dgvPolymer.Columns["Grade"].ReadOnly = false;
                cbcPolymerGrade.DisplayStyleForCurrentCellOnly = true;
                cbcPolymerGrade.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

                dgvPolymer.Columns["PolymerPercent"].HeaderText = "Polymer %";
                dgvPolymer.Columns["PolymerPercent"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPolymer.Columns["PolymerPercent"].DefaultCellStyle.Format = "N3";
                dgvPolymer.Columns["PolymerPercent"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvPolymer.Columns["RegrindMaxPC"].HeaderText = "Regrind %";
                dgvPolymer.Columns["RegrindMaxPC"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPolymer.Columns["RegrindMaxPC"].DefaultCellStyle.Format = "N3";
                dgvPolymer.Columns["RegrindMaxPC"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvPolymer.Columns["Polymer123"].HeaderText = "#";
                dgvPolymer.Columns["Polymer123"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvPolymer.Columns["Polymer123"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvPolymer.Columns["Polymer123"].Width = p96W(50);
                dgvPolymer.Columns["RegrindMaxPC"].Width = p96W(70);
                dgvPolymer.Columns["PolymerPercent"].Width = p96W(70);
                dgvPolymer.Columns["Grade"].Width = p96W(250);
                dgvPolymer.Columns["Material"].Width = p96W(100);
                //dgvPolymer.Columns["AdditionalNotes"].Width = p96W(230);

                dgvPolymer.EditingControlShowing +=
                   new DataGridViewEditingControlShowingEventHandler(dgvPolymer_EditingControlShowing);

                //dgvPolymer.CellFormatting += dgvPolymer_CellFormatting;
                //if (dgvPolymer.Rows.Count > 0)
                //    dgvPolymer.Rows[0].Cells["Polymer123"].Selected = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FormatMasterBatchCompGrid()
        {
            try
            {
                dgvMasterBatchComp.UserAddedRow += dgvMasterBatchComp_UserAddedRow;
                dgvMasterBatchComp.UserDeletedRow += dgvMasterBatchComp_UserDeletedRow;
                dgvMasterBatchComp.EditingControlShowing += dgvMasterBatchComp_EditingControlShowing;
                dgvMasterBatchComp.CellEndEdit += dgvMasterBatchComp_CellEndEdit;
                dgvMasterBatchComp.CellStateChanged += dgvMasterBatchComp_CellStateChanged;
                dgvMasterBatchComp.AllowUserToAddRows = false; //current maximum 2                
                //this.dgvMasterBatchComp.RowPostPaint += dgvMasterBatchComp_RowPostPaint;
                dgvMasterBatchComp.AllowUserToDeleteRows = true;
                dgvMasterBatchComp.UserDeletingRow += dgvMasterBatchComp_UserDeletingRow;

                DataGridViewCellStyle style = dgvMasterBatchComp.ColumnHeadersDefaultCellStyle;
                style.BackColor = Color.LightSteelBlue;
                style.ForeColor = Color.Black;
                style.Font = new Font(dgvMasterBatchComp.Font, FontStyle.Regular);
                dgvMasterBatchComp.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                dgvMasterBatchComp.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                dgvMasterBatchComp.GridColor = SystemColors.ActiveBorder;
                dgvMasterBatchComp.EnableHeadersVisualStyles = false;
                dgvMasterBatchComp.AutoGenerateColumns = true;
                dgvMasterBatchComp.ColumnHeadersHeight = p96H(19);
                dgvMasterBatchComp.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dgvMasterBatchComp.AllowUserToResizeRows = false;
                dgvMasterBatchComp.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                dgvMasterBatchComp.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                dgvMasterBatchComp.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                dgvMasterBatchComp.Height = p96H(70);
                dgvMasterBatchComp.RowHeadersWidth = p96W(26);
                dgvMasterBatchComp.RowTemplate.Height = p96H(19);
                //dgvMasterBatchComp.Rows[dgvMasterBatchComp.NewRowIndex].MinimumHeight = p96H(19);


                //insert a dynamic combobox column for MBCode
                DataGridViewComboBoxColumn cbcMBCode = new DataGridViewComboBoxColumn();
                DataTable dt = dsIMSpecificationForm.Tables["MasterBatch"];
                cbcMBCode.DataSource = dt;
                cbcMBCode.ValueMember = "MBID";
                cbcMBCode.DisplayMember = "MBCode";
                cbcMBCode.Name = "MBCode";
                cbcMBCode.DataPropertyName = "MBID";
                //cbcMBCode.DefaultCellStyle.BackColor = SystemColors.Control;
                cbcMBCode.ReadOnly = false;
                int pos = dgvMasterBatchComp.Columns.Count;
                dgvMasterBatchComp.Columns.Insert(5, cbcMBCode);                
                dgvMasterBatchComp.Columns["MBCode"].HeaderText = "MBCode";
                cbcMBCode.DisplayStyleForCurrentCellOnly = true;
                cbcMBCode.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
               

                //Create a dynamic combobox column for MBColour
                DataGridViewComboBoxColumn cbcMBColour = new DataGridViewComboBoxColumn();
                //dt = dsIMSpecificationForm.Tables["LookupMaterialComp"];
                cbcMBColour.DataSource = dt;
                cbcMBColour.ValueMember = "MBID";
                cbcMBColour.DisplayMember = "MBColour";
                cbcMBColour.Name = "MBColour";
                cbcMBColour.DataPropertyName = "MBID";
                cbcMBColour.DefaultCellStyle.BackColor = SystemColors.Control;
                cbcMBColour.ReadOnly = true;
                pos = dgvMasterBatchComp.Columns.Count;
                dgvMasterBatchComp.Columns.Insert(6, cbcMBColour);
                dgvMasterBatchComp.Columns["MBColour"].HeaderText = "MBColour";
                cbcMBColour.DisplayStyleForCurrentCellOnly = true;
                cbcMBColour.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

                //Create a dynamic combobox column for Additive
                DataGridViewComboBoxColumn cbcAdditive = new DataGridViewComboBoxColumn();
                dt = dsIMSpecificationForm.Tables["Additive"];
                cbcAdditive.DataSource = dt;
                cbcAdditive.ValueMember = "AdditiveID";
                cbcAdditive.DisplayMember = "Additive";
                cbcAdditive.Name = "Additive";
                cbcAdditive.DataPropertyName = "AdditiveID";
                //cbcAdditive.DefaultCellStyle.BackColor = SystemColors.Control;
                cbcAdditive.ReadOnly = false;
                pos = dgvMasterBatchComp.Columns.Count;
                dgvMasterBatchComp.Columns.Insert(9, cbcAdditive);
                dgvMasterBatchComp.Columns["Additive"].HeaderText = "Additive";
                cbcAdditive.DisplayStyleForCurrentCellOnly = true;
                cbcAdditive.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

                //Create a dynamic combobox column for AdditiveCode
                DataGridViewComboBoxColumn cbcAdditiveCode = new DataGridViewComboBoxColumn();
                dt = dsIMSpecificationForm.Tables["Additive"];
                cbcAdditiveCode.DataSource = dt;
                cbcAdditiveCode.ValueMember = "AdditiveID";
                cbcAdditiveCode.DisplayMember = "AdditiveCode";
                cbcAdditiveCode.Name = "AdditiveCode";
                cbcAdditiveCode.DataPropertyName = "AdditiveID";
                cbcAdditiveCode.DefaultCellStyle.BackColor = SystemColors.Control;
                cbcAdditiveCode.ReadOnly = true;
                pos = dgvMasterBatchComp.Columns.Count;
                dgvMasterBatchComp.Columns.Insert(10, cbcAdditiveCode);
                dgvMasterBatchComp.Columns["AdditiveCode"].HeaderText = "Additive Code";
                cbcAdditiveCode.DisplayStyleForCurrentCellOnly = true;
                cbcAdditiveCode.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

                dgvMasterBatchComp.Columns["MB123"].HeaderText = "#";
                dgvMasterBatchComp.Columns["MB123"].Width = p96W(20);
                dgvMasterBatchComp.Columns["MB123"].DefaultCellStyle.BackColor = SystemColors.Control;
                dgvMasterBatchComp.Columns["MB123"].ReadOnly = false;

                dgvMasterBatchComp.Columns["MBPercent"].HeaderText = "MB %";
                dgvMasterBatchComp.Columns["MBPercent"].Width = p96W(50);
                dgvMasterBatchComp.Columns["MBPercent"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvMasterBatchComp.Columns["MBPercent"].DefaultCellStyle.Format = "N2";
                dgvMasterBatchComp.Columns["AdditivePC"].HeaderText = "Add %";
                dgvMasterBatchComp.Columns["AdditivePC"].Width = p96W(50);
                dgvMasterBatchComp.Columns["AdditivePC"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvMasterBatchComp.Columns["AdditivePC"].DefaultCellStyle.Format = "N2";

                dgvMasterBatchComp.Columns["MBCode"].Width = p96W(250);
                dgvMasterBatchComp.Columns["Additive"].Width = p96W(220);

                dgvMasterBatchComp.Columns["MBCompID"].Visible = false;
                dgvMasterBatchComp.Columns["MBID"].Visible = false;
                dgvMasterBatchComp.Columns["ItemID"].Visible = false;
                dgvMasterBatchComp.Columns["AdditiveID"].Visible = false;
                dgvMasterBatchComp.Columns["last_updated_by"].Visible = false;
                dgvMasterBatchComp.Columns["last_updated_on"].Visible = false;
                //dgvMasterBatchComp.Columns["AdditiveID"].Visible = false;                
                //dgvMasterBatchComp.Columns["last_updated_by"].Visible = false;
                //dgvMasterBatchComp.Columns["last_updated_on"].Visible = false;
                dgvMasterBatchComp.ReadOnly = false;
                
                


                //dgvMasterBatchComp.CellFormatting += dgvMasterBatchComp_CellFormatting;
                //if (dgvMasterBatchComp.Rows.Count > 0)
                //    dgvMasterBatchComp.Rows[0].Cells["Polymer123"].Selected = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvMasterBatchComp_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void dgvMasterBatchComp_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure?", "Confirm Delete Polymer", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void dgvMasterBatchComp_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void dgvMasterBatchComp_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void dgvMasterBatchComp_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (cbec != null)
            {
                // Here we will remove the subscription for selected index changed
                cbec.SelectedIndexChanged -= new EventHandler(Cbec_SelectedIndexChanged);

                //allow an empty cell selection
                if (cbec.Text.Length == 0)
                {
                    if (dgvMasterBatchComp.CurrentCell.ColumnIndex == dgvMasterBatchComp.Columns["MBCode"].Index)
                    {
                        dgvMasterBatchComp.CurrentRow.Cells["MBColour"].Value = DBNull.Value;
                        dgvMasterBatchComp.CurrentRow.Cells["MBID"].Value = DBNull.Value;
                    }
                    else if (dgvMasterBatchComp.CurrentCell.ColumnIndex == dgvMasterBatchComp.Columns["Additive"].Index)
                    {
                        dgvMasterBatchComp.CurrentRow.Cells["AdditiveID"].Value = DBNull.Value;
                        dgvMasterBatchComp.CurrentRow.Cells["AdditiveCode"].Value = DBNull.Value;
                    }
                }
                

                //show selected value right away, without needing to change grid row
                //Doesnt work for new row!
                //dgvPolymer.BindingContext[dgvPolymer.DataSource].EndCurrentEdit();
            }
        }

        private void dgvMasterBatchComp_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            btnAddNewMB.Enabled = dgvMasterBatchComp.Rows.Count < maxRowsMB;
        }

        private void dgvMasterBatchComp_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (dgvMasterBatchComp.CurrentCell.ColumnIndex == dgvMasterBatchComp.Columns["MBPercent"].Index
                || dgvMasterBatchComp.CurrentCell.ColumnIndex == dgvMasterBatchComp.Columns["AdditivePC"].Index) //Desired Column
                {
                    System.Windows.Forms.TextBox tb = e.Control as System.Windows.Forms.TextBox;
                    if (tb != null)
                    {
                        // Remove an existing event-handler, if present, to avoid 
                        // adding multiple handlers when the editing control is reused.
                        tb.KeyPress -= new KeyPressEventHandler(FloatControl_KeyPress);

                        // Add the event handler
                        tb.KeyPress += new KeyPressEventHandler(FloatControl_KeyPress);
                    }
                }
                else if (dgvMasterBatchComp.CurrentCell.ColumnIndex == dgvMasterBatchComp.Columns["MBCode"].Index
                    || dgvMasterBatchComp.CurrentCell.ColumnIndex == dgvMasterBatchComp.Columns["Additive"].Index)
                {
                    if (e.Control is DataGridViewComboBoxEditingControl)
                    {
                        cbec = new DataGridViewComboBoxEditingControl();
                        cbec = e.Control as DataGridViewComboBoxEditingControl;



                        cbec.DropDownStyle = ComboBoxStyle.DropDown;
                        cbec.SelectedIndexChanged -= Cbec_SelectedIndexChanged;
                        cbec.SelectedIndexChanged += Cbec_SelectedIndexChanged;

                        // Fix the black background issue on the drop down menu
                        e.CellStyle.BackColor = this.dgvMasterBatchComp.DefaultCellStyle.BackColor;

                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    
        void NumControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                //The char is not a number or a control key
                //true means key press is not accepted
                e.Handled = true;               
            }            
        }

        private void ValidateFloat(object sender, KeyPressEventArgs e)
        {
            int b;

            if (e.KeyChar == (char)Keys.Back ||
                e.KeyChar == (char)Keys.Delete ||
                e.KeyChar == (char)Keys.Left ||
                e.KeyChar == (char)Keys.Right ||
                int.TryParse(e.KeyChar.ToString(), out b))
            {
                TextBox obj = sender as TextBox;
                //if (e.KeyChar == '.' && obj.Text.IndexOf('.') > 0)
                if (e.KeyChar == '.' && (obj.Text.IndexOf('.') > 0 || obj.Text.Length == 0))
                    e.Handled = true;
                else
                    e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void FloatControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.ValidateFloat(sender, e);
        }

        private void dDgvMasterBatchComp_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void dgvPolymer_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            btnAddNewMaterial.Enabled = dgvPolymer.Rows.Count < maxRowsMaterial;
        }

        //private void dgvPolymer_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        //{
        //    //MessageBox.Show("todo:  assign material type");
        //    if (e.RowIndex < 0)
        //        return;

        //    //assign material
        //    if (dgvPolymer.Columns[e.ColumnIndex].Name == "Material")
        //    {
        //        if (dgvPolymer.Rows[e.RowIndex].Cells["MaterialGradeID"].Value != null)
        //        {
        //            int materialGradeID = (int)dgvPolymer.Rows[e.RowIndex].Cells["MaterialGradeID"].Value;
        //            dgvPolymer.Rows[e.RowIndex].Cells["Material"].Value = GetMaterial(materialGradeID);
        //        }
        //    }
        //}

        private void dgvPolymer_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure?", "Confirm Delete MasterBatch", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        // Disable cell selection
        private void dgvPolymer_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected && e.Cell.OwningColumn.Name == "Material")
                e.Cell.Selected = false;
        }

        private void dgvPolymer_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (cbec != null)
            {
                // Here we will remove the subscription for selected index changed
                cbec.SelectedIndexChanged -= new EventHandler(Cbec_SelectedIndexChanged);

                //show selected value right away, without needing to change grid row
                //Doesnt work for new row!
                //dgvPolymer.BindingContext[dgvPolymer.DataSource].EndCurrentEdit();
            }
        }



        private void dgvPolymer_RowCountChanged(object sender, DataGridViewRowEventArgs e)
        {
            //int maxRowCount = 3;
            //dgvPolymer.AllowUserToAddRows = (dgvPolymer.Rows.Count <= maxRowsMaterial) ? true : false;

        }

        private void dgvPolymer_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                int rowIndex = e.Row.Index;
                dgvPolymer.Rows[rowIndex].Cells["Polymer123"].Value = rowIndex + 1;
                dgvPolymer.Rows[rowIndex].HeaderCell.Value = "Polymer " + (rowIndex + 1).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvPolymer_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // first remove event handler to keep from attaching multiple:
            //cboPolymer.SelectedIndexChanged -= new
            //EventHandler(cboPolymer_SelectedIndexChanged);


            if (dgvPolymer.CurrentCell.ColumnIndex == dgvPolymer.Columns["PolymerPercent"].Index) //Desired Column
            {
                System.Windows.Forms.TextBox tb = e.Control as System.Windows.Forms.TextBox;
                if (tb != null)
                {
                    // Remove an existing event-handler, if present, to avoid 
                    // adding multiple handlers when the editing control is reused.
                    tb.KeyPress -= new KeyPressEventHandler(IntControl_KeyPress);

                    // Add the event handler
                    tb.KeyPress += new KeyPressEventHandler(IntControl_KeyPress);
                }
            }
            else if (dgvPolymer.CurrentCell.ColumnIndex == dgvPolymer.Columns["Grade"].Index)
            {
                if (e.Control is DataGridViewComboBoxEditingControl)
                {
                    cbec = e.Control as DataGridViewComboBoxEditingControl;
                    cbec.SelectedIndexChanged -= Cbec_SelectedIndexChanged;
                    cbec.SelectedIndexChanged += Cbec_SelectedIndexChanged;
                }

                //if (e.Control is ComboBox)
                //{
                //    cboPolymer = (ComboBox)e.Control;
                //    if (cboPolymer != null)
                //    {
                //        cboPolymer.SelectedIndexChanged += new EventHandler(cboPolymer_SelectedIndexChanged);
                //    }
                //    currentCell = this.dgvPolymer.CurrentCell;
                //}               
            }
        }

        private void Cbec_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbec != null)
                {
                    BeginInvoke(new MethodInvoker(EndEdit));
                    //DataRowView drv = cbec.SelectedItem as DataRowView;
                    //if (drv != null)
                    //{
                    //    //this.dataGridView1[currentCell.ColumnIndex + 1, currentCell.RowIndex].Value = drv[2].ToString();
                    //    //this.dataGridView1.EndEdit();
                    //    int materialGradeID = (int)cbec.SelectedValue;
                    //    dgvPolymer.CurrentRow.Cells["MaterialGradeID"].Value = (int)drv["MaterialGradeID"];
                    //    dgvPolymer.CurrentRow.Cells["MaterialID"].Value = (int)drv["MaterialID"];
                    //    dgvPolymer.CurrentRow.Cells["Polymer123"].Value = dgvPolymer.CurrentRow.Index + 1;
                    //    BeginInvoke(new MethodInvoker(EndEdit));
                    //}
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }            
        }

        private void EndEdit()
        {
            try
            {
                // Change the content of appropriate cell when selected index changes
                // Unsubscribe from  SelectedIndexChanged event
                cbec.SelectedIndexChanged -= Cbec_SelectedIndexChanged;
                if (cbec != null && cbec.SelectedIndex != -1 && cbec.SelectedValue != null)
                {
                    DataRowView drv = cbec.SelectedItem as DataRowView;
                    if (drv != null && drv.DataView.Table.TableName == "LookupMaterialGrade")
                    {
                        //DataRowView rowView = (DataRowView)this.bsMaterialComp.Current;
                        //DataRow row = rowView.Row;
                        //int materialGradeID = (int)cbec.SelectedValue;
                        //row["MaterialGradeID"] = materialGradeID;
                        //row["Polymer123"] = dgvPolymer.CurrentRow.Index + 1;
                        //row.EndEdit();

                        dgvPolymer.CurrentRow.Cells["MaterialGradeID"].Value = (int)drv["MaterialGradeID"];
                        dgvPolymer.CurrentRow.Cells["Polymer123"].Value = dgvPolymer.CurrentRow.Index + 1;
                        dgvPolymer.CurrentRow.Cells["Material"].Selected = true;
                        dgvPolymer.CurrentRow.Cells["AdditionalNotes"].Selected = true;
                        dgvPolymer.CurrentRow.Cells["Material"].Selected = false;
                        dgvPolymer.CurrentRow.Cells["AdditionalNotes"].Selected = false;
                        dgvPolymer.CurrentRow.Cells["Grade"].Selected = true;
                        dgvPolymer.EndEdit();
                    }
                    else if (drv != null && drv.DataView.Table.TableName == "MasterBatch")
                    {
                        if (dgvMasterBatchComp.CurrentCell.ColumnIndex == dgvMasterBatchComp.Columns["MBCode"].Index)
                        {
                            dgvMasterBatchComp.CurrentRow.Cells["MBColour"].Value = (int)drv["MBID"];
                        }
                    }
                    else if (drv != null && drv.DataView.Table.TableName == "Additive")
                    {
                        if (dgvMasterBatchComp.CurrentCell.ColumnIndex == dgvMasterBatchComp.Columns["Additive"].Index)
                        {
                            dgvMasterBatchComp.CurrentRow.Cells["AdditiveCode"].Value = (int)drv["AdditiveID"];
                        }
                    }
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        string GetMaterial(int materialGradeID)
        {
            try
            {
                DataRowView drv = (DataRowView)bsMaterialGrade.Current;
                int mgIndex = bsMaterialGrade.Find("MaterialGradeID", materialGradeID);
                string material = null;
                if (mgIndex != -1)
                {
                    bsMaterialGrade.Position = mgIndex;
                    drv = (DataRowView)bsMaterialGrade.Current;
                    string additionalNotes = drv.Row["AdditionalNotes"].ToString();
                    int materialID = (int)drv.Row["MaterialID"];
                    int materialIndex = bsMaterial.Find("MaterialID", materialID);
                    if (materialIndex != -1)
                    {
                        bsMaterial.Position = materialIndex;
                        drv = (DataRowView)bsMaterial.Current;
                        material = drv.Row["ShortDesc"].ToString() + "#" + additionalNotes;
                    }
                    return material;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private void dgvPolymer_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
        private void dgvMachine_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void FormatAdditiveGrid()
        {
            //dsAdditive = new AdditiveCostDAL().SelectAdditiveCost();
            dgvAdditive.Width = p96W(700);
            dgvAdditive.Height = p96H(400);
            dgvAdditive.ReadOnly = true;
            dgvAdditive.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAdditive.ClearSelection();
            DataGridViewCellStyle style = dgvAdditive.ColumnHeadersDefaultCellStyle;
            style.BackColor = Color.RosyBrown;
            style.ForeColor = Color.MidnightBlue;
            style.Font = new Font(dgvAdditive.Font, FontStyle.Regular);
            dgvAdditive.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            dgvAdditive.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgvAdditive.ColumnHeadersDefaultCellStyle.BackColor;
            dgvAdditive.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgvAdditive.GridColor = SystemColors.ActiveBorder;
            dgvAdditive.EnableHeadersVisualStyles = false;
            dgvAdditive.AutoGenerateColumns = true;
            dgvAdditive.AllowUserToAddRows = false;

            dgvAdditive.ColumnHeadersHeight = p96H(19);
            dgvAdditive.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvAdditive.AllowUserToResizeRows = false;
            dgvAdditive.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            //dgvAdditive.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            //dgvAdditive.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            //dgvAdditive.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            dgvAdditive.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvAdditive.RowHeadersWidth = p96W(26); //Convert.ToInt32(26 * screenRes.Width / 96);
            //dgvAdditive.DataSource = dsAdditive.Tables[0];

            dgvAdditive.Columns["CostPerKg"].HeaderText = "Cost/kg     $";
            dgvAdditive.Columns["CostPerKg"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvAdditive.Columns["CostPerKg"].DefaultCellStyle.Format = "N3";
            dgvAdditive.Columns["CostPerKg"].Width = p96W(100);
            dgvAdditive.Columns["Additive"].Width = p96W(100);
            dgvAdditive.Columns["Additive"].DisplayIndex = 2;
            dgvAdditive.Columns["Comment"].Width = p96W(100);
            dgvAdditive.Columns["Description"].Width = p96W(100);
            dgvAdditive.Columns["AdditiveCOde"].HeaderText = "Additive Code";
            //dgvAdditive.Columns["MBCompID"].Visible = false;
            //dgvAdditive.Columns["MBID"].Visible = false;
            dgvAdditive.Columns["AdditiveID"].Visible = false;
            dgvAdditive.Columns["last_updated_by"].Visible = false;
            dgvAdditive.Columns["last_updated_on"].Visible = false;

            //add click event to catch selected row
            dgvAdditive.Click += dgvAdditive_Click;

            //add keydown event to catch esc key
            dgvAdditive.KeyDown += dgvAdditive_KeyDown;
        }

        private void dgvAdditive_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                //MessageBox.Show("todo:  collapse grid");
                //lblAdditiveCode.Image = DrawingUtils.GetImage(ButtonOp.Expand, p96W(15), p96H(15));
                dgvAdditive.Visible = false;
            }
        }

        private void dgvMachine_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //if(dgvMachine.CurrentCell.OwningColumn.Name == "CycleTime")
            //{
            //    dgvMachine.BindingContext[dgvMachine.DataSource].EndCurrentEdit();
            //}            
        }

        private void btnCopyToNew_Click(object sender, EventArgs e)
        {
            SaveEdits();
            //CopyToNew();

            btnCopyToNew.Enabled = false;
            //bindingNavigator does not allow disabling of built-in navigation buttons.
            //Set invisible instead;   available option buttons are now Save and Cancel only.
            bindingNavigatorMoveFirstItem.Visible = false;
            bindingNavigatorMovePreviousItem.Visible = false;
            tsbtnDelete.Visible = false;
            tsbtnAddNew.Visible = false;
            tscboCompany.SelectedIndexChanged -= tscboCompany_SelectedIndexChanged;

            tscboCompany.Enabled = false;
            tscboProduct.Enabled = false;
            tsbtnReport.Enabled = false;

            DataRowView drv = (DataRowView)bsManItems.Current;
            int curItemID = (int)drv.Row["ItemID"];
            int curCustomerID = LastCustomerID.Value;
            MAN_ItemDAL dal = new MAN_ItemDAL();
            dal.CopyToNew(curItemID, curCustomerID);
            this.LastItemID = dal.NewItemID;
            this.DialogResult = DialogResult.Retry;
            this.Close();
        }

        private void CopyToNew()
        {
            try
            {
                btnCopyToNew.Enabled = false;
                //bindingNavigator does not allow disabling of built-in navigation buttons.
                //Set invisible instead;   available option buttons are now Save and Cancel only.
                bindingNavigatorMoveFirstItem.Visible = false;
                bindingNavigatorMovePreviousItem.Visible = false;
                tsbtnDelete.Visible = false;
                tsbtnAddNew.Visible = false;
                tscboCompany.SelectedIndexChanged -= tscboCompany_SelectedIndexChanged;

                tscboCompany.Enabled = false;
                tscboProduct.Enabled = false;
                tsbtnReport.Enabled = false;

                DataRowView drv = (DataRowView)bsManItems.Current;
                int curItemID = (int)drv.Row["ItemID"];


                bsManItems.CurrentChanged -= bsManItems_CurrentChanged;
                bsManItems.AddingNew -= bsManItems_AddingNew;
                bsManItems.RemoveFilter();
                bsManItems.SuspendBinding();

                tscboCompany.ComboBox.SelectedIndexChanged -= tscboCompany_SelectedIndexChanged;

                tscboProduct.SelectedIndexChanged -= tscboProduct_SelectedIndexChanged;
                tscboProduct.SelectedIndex = -1;
                cboCUSTNAME.SelectedIndex = -1;
                
                //copy Product details                
                DataTable dt = dsIMSpecificationForm.Tables["MAN_Items"];
                DataView view = new DataView(dt, "", "ItemID", DataViewRowState.CurrentRows);
                int rowIndexToCopy = view.Find(curItemID);
                DataRow existingRow = view[rowIndexToCopy].Row;
                string copiedDesc = existingRow["ITEMDESC"].ToString();

                //copy to a new row
                DataRow newRow = dt.NewRow();
                newRow.ItemArray = existingRow.ItemArray.Clone() as object[];
                newItemID--;  //generates a -ve new row index
                newRow["ItemID"] = newItemID;
                string newCode = new ProductDetailsDAL().NewFormattedCode();
                newRow["ITEMNMBR"] = newCode;
                newRow["ITEMDESC"] = "Copied from:  " + copiedDesc;
                newRow["ItemID"] = newItemID;
                dt.Rows.Add(newRow);
                dt.AcceptChanges();
                dt.Rows[dt.Rows.Count - 1].SetAdded(); //resets row status back to Added
                newRow.EndEdit();
                bsManItems.EndEdit();

                int itemIndex = bsManItems.Find("ItemID", newItemID);
                if (itemIndex != -1)
                {
                    bsManItems.ResumeBinding();
                    bsManItems.Position = itemIndex;
                }

                //add customer for new product
                //  ## bug ## statement following changes current CustomerID to null 
                //  bsCustomerProducts.SuspendBinding();
                dt = dsIMSpecificationForm.Tables["CustomerProduct"];
                view = new DataView(dt, "", "ItemID", DataViewRowState.CurrentRows);
                //rowIndexToCopy = view.Find(curItemID);
                rowIndexToCopy = view.Find(LastCustomerID.Value);
                if(rowIndexToCopy != -1)
                {
                    existingRow = view[rowIndexToCopy].Row;
                    //copy to a new row
                    newRow = dt.NewRow();
                    newRow.ItemArray = existingRow.ItemArray.Clone() as object[];
                    newRow["ItemID"] = newItemID;
                    newRow["CustomerID"] = LastCustomerID.Value;
                    newRow["CustomerProductID"] = -1;
                    dt.Rows.Add(newRow);
                }
               
                //bsCustomerProducts.ResumeBinding();

                cboCUSTNAME.SelectedValue = LastCustomerID;

                //cboCUSTNAME.SelectedValue = LastCustomerID.Value;
                //dt.Rows.Add(-1,LastCustomerID.Value, newItemID);
                //bsCustomerProducts.EndEdit();
                //bsCustomerProducts.ResumeBinding();
                //cboCUSTNAME.SelectedValue = LastCustomerID.Value;

                //cboCUSTNAME.SelectedIndexChanged += cboCUSTNAME_SelectedIndexChanged;

                //copy Mould Specification
                bsMouldSpec.SuspendBinding();
                dt = dsIMSpecificationForm.Tables["InjectionMouldSpecification"];
                view = new DataView(dt, "", "ItemID", DataViewRowState.CurrentRows);
                rowIndexToCopy = view.Find(curItemID);
                if(rowIndexToCopy != -1)
                {
                    existingRow = view[rowIndexToCopy].Row;
                    //copy to a new row
                    newRow = dt.NewRow();
                    newRow.ItemArray = existingRow.ItemArray.Clone() as object[];
                    newRow["ItemID"] = newItemID;
                    dt.Rows.Add(newRow);
                }                
                bsMouldSpec.ResumeBinding();
                //bsMouldSpec.CurrentChanged += bsMouldSpec_CurrentChanged;

                //copy Quality Control
                bsQC.SuspendBinding();
                dt = dsIMSpecificationForm.Tables["QualityControl"];
                view = new DataView(dt, "", "ItemID", DataViewRowState.CurrentRows);
                rowIndexToCopy = view.Find(curItemID);
                if(rowIndexToCopy != -1)
                {
                    existingRow = view[rowIndexToCopy].Row;
                    //copy to a new row
                    newRow = dt.NewRow();
                    newRow.ItemArray = existingRow.ItemArray.Clone() as object[];
                    newRow["ItemID"] = newItemID;
                    dt.Rows.Add(newRow);
                }                
                bsQC.ResumeBinding();

                //copy Material Composition
                bsMaterialComp.SuspendBinding();
                //bsMaterialComp.AddingNew -= bsMaterialComp_AddingNew;
                dt = dsIMSpecificationForm.Tables["MaterialComp"];
                DataView dv = (DataView)dsIMSpecificationForm.Tables["MaterialComp"].DefaultView;
                dv.RowFilter = "ItemID = " + curItemID.ToString();
                foreach (DataRowView drvm in dv)
                {
                    //rowIndexToCopy = drvm.Row.RowId();
                    existingRow = drvm.Row;
                    newRow = dt.NewRow();
                    newRow.ItemArray = existingRow.ItemArray.Clone() as object[];
                    newRow["ItemID"] = newItemID;
                    dt.Rows.Add(newRow);
                }
                bsMaterialComp.EndEdit();
                bsMaterialComp.ResumeBinding();

                //copy Masterbatch composition
                bsMBComp.SuspendBinding();
                bsMBMBComp.SuspendBinding();  //Masterbatch colour
                bsAddMBComp.SuspendBinding(); //Masterbatch additive
                dt = dsIMSpecificationForm.Tables["MasterBatchComp"];
                dv = (DataView)dsIMSpecificationForm.Tables["MasterBatchComp"].DefaultView;
                dv.RowFilter = "ItemID = " + curItemID.ToString();
                int nextMB = 0;
                foreach (DataRowView drvm in dv)
                {
                    //rowIndexToCopy = drvm.Row.RowId();
                    existingRow = drvm.Row;
                    newRow = dt.NewRow();
                    newRow.ItemArray = existingRow.ItemArray.Clone() as object[];
                    newRow["ItemID"] = newItemID;
                    nextMB ++;  
                    newRow["MB123"] = nextMB;
                    newRow["IsPreferred"] = true;
                    dt.Rows.Add(newRow);
                }
                bsMBComp.EndEdit();

                //***** test if working *****
                bsMBComp.Filter = "IsPreferred = true";
                int mcIndex = bsMBComp.Find("ItemID", newItemID);
                if (mcIndex != -1)
                {
                    bsMBComp.ResumeBinding();    //Masterbatch composition
                    bsMBMBComp.ResumeBinding();  //Masterbatch colour
                    bsAddMBComp.ResumeBinding(); //Masterbatch additive
                }

                //copy Machine preference              
                bsMachPref.SuspendBinding();
                dt = dsIMSpecificationForm.Tables["MachinePref"];
                dv = (DataView)dsIMSpecificationForm.Tables["MachinePref"].DefaultView;
                dv.RowFilter = "ItemID = " + curItemID.ToString();
                foreach (DataRowView drvm in dv)
                {
                    //rowIndexToCopy = drvm.Row.RowId();
                    existingRow = drvm.Row;
                    newRow = dt.NewRow();
                    newRow.ItemArray = existingRow.ItemArray.Clone() as object[];
                    newRow["ItemID"] = newItemID;
                    dt.Rows.Add(newRow);
                }
                bsMachPref.ResumeBinding();

                bsManItems.CurrentChanged += bsManItems_CurrentChanged;
                bsManItems.AddingNew += bsManItems_AddingNew;
                //cboCUSTNAME.SelectedIndexChanged += cboCUSTNAME_SelectedIndexChanged;

                SaveEdits();



                RefreshCurrent();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "CopyToNew",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void SpecificationDataEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.Retry)
                this.DialogResult = DialogResult.OK;
        }

        private void productAssemblyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NextForm = "AssemblyDataEntry";
            SaveEdits();
            this.DialogResult = DialogResult.Retry;
            this.Close();
        }

        private void qCIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NextForm = "QCDataEntry";
            SaveEdits();
            this.DialogResult = DialogResult.Retry;
            this.Close();
        }

        private void documentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NextForm = "AttachedDocsDataEntry";
            SaveEdits();
            this.DialogResult = DialogResult.Retry;
            this.Close();
        }

        private void productPackagingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NextForm = "PackagingDataEntry";
            SaveEdits();
            this.DialogResult = DialogResult.Retry;
            this.Close();
        }

        private void dgvAdditive_Click(object sender, EventArgs e)
        {
            try
            {
                int additiveID = (int)dgvAdditive.CurrentRow.Cells["AdditiveID"].Value;
                DataRowView rowView = (DataRowView)this.bsManItems.Current;
                DataRow row = rowView.Row;
                int itemID = (int)row["ItemID"];

                //locate additive in MasterBatch Composition 
                bsMBComp.SuspendBinding();
                int mcIndex = bsMBComp.Find("ItemID", itemID);
                if (mcIndex != -1)
                {
                    bsMBComp.ResumeBinding();
                    bsMBComp.Position = mcIndex;
                    rowView = (DataRowView)this.bsMBComp.Current;
                    row = rowView.Row;
                    row["AdditiveID"] = additiveID;
                    bsMBComp.EndEdit();
                }
                // ## not valid.  MasterBatch must exist befor additive can be added
                //Add additive to MasterBatch composition
                //else
                //{
                //    bsMBComp.ResumeBinding();
                //    bsMBComp.AddNew();
                //    bsMBComp.MoveLast();
                //    rowView = (DataRowView)bsMBComp.Current;
                //    row = rowView.Row;
                //    row["AdditiveID"] = additiveID;
                //    bsMBComp.EndEdit();
                //}
                dgvAdditive.Visible = false;
                RefreshCurrent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void addCustomersForProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("handle add customers for product");
            ProductCustomerDataEntry f = new ProductCustomerDataEntry();
            f.ShowDialog();
        }

        private void addProductsForCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("handle add products for customer");
            CustomerProductDataEntry f = new CustomerProductDataEntry();
            f.ShowDialog();

        }

        private void addEditCustomersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Add/Edit Customers");            
            Customer f = new Customer(LastCustomerID.Value);
            f.ShowDialog();
        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {

        }

        

        private void FormatMBGrid()
        {
            try
            {
                //MainFormDAL dal = new MainFormDAL();
                dgvMBCode.AllowUserToAddRows = false;
                dgvMBCode.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvMBCode.Width = p96H(600);
                dgvMBCode.Height = p96H(400);
                dgvMBCode.RowHeadersWidth = p96H(26);
                dgvMBCode.ClearSelection();

                dgvMBCode.ColumnHeadersHeight = p96H(19);
                dgvMBCode.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dgvMBCode.AllowUserToResizeRows = false;
                dgvMBCode.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

                DataGridViewCellStyle style = dgvMBCode.ColumnHeadersDefaultCellStyle;
                style.BackColor = Color.RosyBrown;
                style.ForeColor = Color.MidnightBlue;
                style.Font = new Font(dgvMBCode.Font, FontStyle.Regular);
                dgvMBCode.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgvMBCode.ColumnHeadersDefaultCellStyle.BackColor;
                dgvMBCode.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                dgvMBCode.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                dgvMBCode.GridColor = SystemColors.ActiveBorder;
                dgvMBCode.EnableHeadersVisualStyles = false;
                dgvMBCode.AutoGenerateColumns = true;
                dgvMBCode.Columns[0].Visible = false;
                dgvMBCode.Columns["last_updated_by"].Visible = false;
                dgvMBCode.Columns["last_updated_on"].Visible = false;
                dgvMBCode.Columns["CostPerKg"].HeaderText = "Cost/kg        $";
                dgvMBCode.Columns["CostPerKg"].Width = p96W(120);
                dgvMBCode.Columns["Comment"].Width = p96W(400);
                dgvMBCode.Columns["CostPerKg"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvMBCode.Columns["CostPerKg"].DefaultCellStyle.Format = "N3";

                //add combobox column for colour
                //DataSet dsMBColour = new MasterBatchDAL().SelectMBColour();
                DataGridViewComboBoxColumn cbcColour = new DataGridViewComboBoxColumn();
                cbcColour.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                DataTable dt = dsMBColour.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    cbcColour.Items.Add(row["MBColour"].ToString().TrimEnd());
                }
                cbcColour.DataPropertyName = "MBColour";
                cbcColour.Name = "MBColour";
                dgvMBCode.Columns.Remove("MBColour");
                dgvMBCode.Columns.Insert(1, cbcColour);
                dgvMBCode.Columns["MBColour"].HeaderText = "Colour";
                dgvMBCode.Columns["MBColour"].DisplayIndex = 2;
                //cbcColour.DisplayStyleForCurrentCellOnly = true;

                //add click event to catch selected row
                dgvMBCode.Click += dgvMBCode_Click;

                //add key down event to catch esc key
                dgvMBCode.KeyDown += dgvMBCode_KeyDown;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvMBCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                //lblMBCode.Image = DrawingUtils.GetImage(ButtonOp.Expand, p96W(15), p96H(15));
                dgvMBCode.Visible = false;
            }
        }

        private void dgvMBCode_Click(object sender, EventArgs e)
        {
            try
            {
                int mbID = (int)dgvMBCode.CurrentRow.Cells["MBID"].Value;
                DataRowView rowView = (DataRowView)this.bsManItems.Current;
                DataRow row = rowView.Row;
                int itemID = (int)row["ItemID"];

                //locate selected MasterBatch in MasterBatch Composition
                bsMBComp.SuspendBinding();
                int mcIndex = bsMBComp.Find("ItemID", itemID);
                if (mcIndex != -1)
                {
                    bsMBComp.ResumeBinding();
                    bsMBComp.Position = mcIndex;
                    rowView = (DataRowView)this.bsMBComp.Current;
                    row = rowView.Row;
                    row["MBID"] = mbID;
                    bsMBComp.EndEdit();
                }
                //add MasterBatch to MasterBatch Composition
                else
                {
                    bsMBComp.ResumeBinding();
                    bsMBComp.AddNew();
                    bsMBComp.MoveLast();
                    rowView = (DataRowView)this.bsMBComp.Current;
                    row = rowView.Row;
                    row["MBID"] = mbID;
                    row["ItemID"] = itemID;
                    bsMBComp.EndEdit();
                }
                RefreshCurrent();
                dgvMBCode.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

                    menuStrip1.Enabled = true;

                    //form will reopen here after Save
                    if (itemID > 0)
                    {
                        tsbtnReport.Enabled = true;
                        LastItemID = itemID;
                        tscboProduct.SelectedIndexChanged -= tscboProduct_SelectedIndexChanged;
                        tscboProduct.ComboBox.SelectedValue = itemID;
                        tscboProduct.SelectedIndexChanged += tscboProduct_SelectedIndexChanged;
                    }
                    
                    //locate customer for this product
                    bsCustomerProducts.Filter = "CustomerID = " + LastCustomerID.Value.ToString() + " AND ItemID = " + itemID.ToString();
                    if (bsCustomerProducts.Current != null)
                    {
                        DataRowView dv = (DataRowView)bsCustomerProducts.Current;
                        DataRow dr = dv.Row;
                        int custProdID = (int)dr["CustomerProductID"];
                        int cpIndex = bsCustomerProducts.Find("CustomerProductID", custProdID);
                        if (cpIndex != -1)
                        {

                            bsCustomerProducts.Position = cpIndex;
                            //cboCUSTNAME.SelectedIndexChanged += cboCUSTNAME_SelectedIndexChanged;
                            DataRowView drv = (DataRowView)this.bsCustomerProducts.Current;
                            dr = drv.Row;
                            LastCustomerID = (int)dr["CustomerID"];
                            
                            //Enable Alternative code input for Angel or Consolidated Plastics
                            int custIndex = bsCustomer.Find("CustomerID", LastCustomerID.Value);
                            if (custIndex != -1)
                            {
                                bsCustomer.Position = custIndex;
                                drv = (DataRowView)this.bsCustomer.Current;
                                dr = drv.Row;
                                string testMatching = "Angel Products | Consolidated Plastics";
                                string testCompany = dr["CUSTNAME"].ToString().Trim();
                                if (testMatching.Contains(testCompany))
                                {
                                    txtAltCode.Enabled = true;
                                    lblAltCode.Enabled = true;
                                }
                                else
                                {
                                    txtAltCode.Enabled = false;
                                    lblAltCode.Enabled = false;
                                }
                            }
                        }
                    }
                    
                    //locate ProductGrade aka ProductCategory                   
                    bsProductGradeItem.SuspendBinding();
                    int gradeID = int.TryParse(row["GradeID"].ToString(), out gradeID) ? gradeID : -1;
                    if (gradeID != -1)
                    {
                        int pgIndex = bsProductGradeItem.Find("GradeID", gradeID);
                        if (pgIndex != -1)
                        {
                            bsProductGradeItem.ResumeBinding();
                            bsProductGradeItem.Position = pgIndex;

                            //DataRowView drv = (DataRowView)this.bsProductGradeItem.Current;
                            //DataRow dr = drv.Row;
                            //MessageBox.Show(itemID.ToString() + ", " + dr["GradeID"].ToString()
                            //    + ", " + dr["Description"].ToString()
                            //    + ", " + dr["LabelIcon"].ToString());
                        }
                    }

                    //locate item in MasterBatchComp                
                    bsMasterBatch.SuspendBinding();
                    bsMBComp.SuspendBinding();
                    bsMBMBComp.SuspendBinding();
                    bsAddMBComp.SuspendBinding();
                    //btnDeleteMB.Enabled = false;
                    //btnDeleteAdditive.Enabled = false;

                    //locate MBCode, Colour
                    //bsMBComp.Filter = "IsPreferred = 1";  -- not in use
                    int mcIndex = bsMBComp.Find("ItemID", itemID);
                    if (mcIndex != -1)
                    {
                        bsMBComp.ResumeBinding();
                        bsMBComp.Position = mcIndex;
                        rowView = (DataRowView)this.bsMBComp.Current;
                        row = rowView.Row;
                        int mbID = int.TryParse(row["MBID"].ToString(), out mbID) ? mbID : -1;
                        if (mbID != -1)
                        {
                            //locate Masterbatch colour
                            bsMasterBatch.ResumeBinding();
                            bsMBMBComp.ResumeBinding();
                            int mbIndex = bsMasterBatch.Find("MBID", mbID);
                            bsMasterBatch.Position = mbIndex;
                            int mbColourPos = bsMBMBComp.Find("MBID", mbID);
                            if (mbColourPos != -1)
                            {
                                bsMBMBComp.Position = mbColourPos;
                            }
                            //btnDeleteMB.Enabled = true;

                            //locate AdditiveCode, Additive
                            int additiveID = int.TryParse(row["AdditiveID"].ToString(), out additiveID) ? additiveID : -1;
                            if (additiveID != -1)
                            {
                                bsAdditive.ResumeBinding();
                                bsAddMBComp.ResumeBinding();
                                int adIndex = bsAdditive.Find("AdditiveID", additiveID);
                                bsAdditive.Position = adIndex;
                                int additivePos = bsAddMBComp.Find("AdditiveID", additiveID);
                                if (additivePos != -1)
                                {
                                    bsAddMBComp.Position = additivePos;
                                }
                                //btnDeleteAdditive.Enabled = true;
                            }
                        }
                    }

                    //locate item in MaterialComp
                    bsMaterialComp.SuspendBinding();
                    bsMaterialGrade.SuspendBinding();
                    //bsMaterialComp.ResetBindings(false);
                    //bsMaterialGrade.ResetBindings(false);
                    bsMaterialComp.Filter = "IsActive = 1";
                    int mgID = bsMaterialComp.Find("ItemID", itemID);
                    if (mgID != -1)
                    {
                        //bsMaterialComp.ResetBindings(false);
                        bsMaterialComp.ResumeBinding();
                        bsMaterialComp.Position = mgID;
                        rowView = (DataRowView)this.bsMaterialComp.Current;
                        row = rowView.Row;
                        DataTable ct = dsIMSpecificationForm.Relations["ItemMaterialComp"].ChildTable;
                        DataRow[] foundRows = ct.Select("ItemID = " + LastItemID.ToString());
                        int count = foundRows.Length;
                        btnAddNewMaterial.Enabled = (count < maxRowsMaterial);

                        //locate MaterialGrade
                        int materialGradeID = int.TryParse(row["MaterialGradeID"].ToString(), out materialGradeID) ? materialGradeID : -1;
                        if (materialGradeID != -1)
                        {
                            bsMaterialGrade.ResetBindings(false);
                            bsMaterialGrade.ResumeBinding();
                            int mgIndex = bsMaterialGrade.Find("MaterialGradeID", materialGradeID);
                            bsMaterialGrade.Position = mgIndex;
                        }

                        //update grid material type and additional notes
                        //UpdateMaterialType();
                    }

                    //locate mould specification
                    bsMouldSpec.EndEdit();
                    bsMouldSpec.SuspendBinding();
                    bsMouldSpec.Sort = "ItemID";
                    //DataTable dt = dsIMSpecificationForm.Tables["InjectionMouldSpecification"];
                    //DataView view = new DataView(dt, "", "ItemID", DataViewRowState.CurrentRows);
                    //int msID = view.Find(itemID);
                    int msID = bsMouldSpec.Find("ItemID", itemID);
                    if (msID != -1)
                    {
                        bsMouldSpec.ResetBindings(false);
                        bsMouldSpec.ResumeBinding();
                        //bsMouldSpec.Position = (int)view[msID].Row.RowId();
                        //MessageBox.Show(view[msID].Row["ItemID"].ToString() + ", " + view[msID].Row["MouldNumber"].ToString());
                        bsMouldSpec.Position = msID;
                        //rowView = (DataRowView)this.bsMouldSpec.Current;
                        //row = rowView.Row;
                        //MessageBox.Show(row["ItemID"].ToString() + ", " + row["MouldNumber"].ToString());
                        //bsMouldSpec.Position = msID;
                        //rowView = (DataRowView)this.bsMouldSpec.Current;
                        //row = rowView.Row;
                    }
                    else
                    {
                        bsMouldSpec.AddNew();
                        bsMouldSpec.ResumeBinding();
                    }

                    //locate machine preference
                    bsMachPref.EndEdit();
                    bsMachPref.SuspendBinding();
                    bsMachPref.Sort = "ItemID";
                    int mpID = bsMachPref.Find("ItemID", itemID);
                    if (mpID != -1)
                    {
                        bsMachPref.ResetBindings(false);
                        bsMachPref.ResumeBinding();
                    }

                    //locate Quality Control
                    bsQC.EndEdit();
                    bsQC.SuspendBinding();
                    //dt = dsIMSpecificationForm.Tables["QualityControl"];
                    //view = new DataView(dt, "", "ItemID", DataViewRowState.CurrentRows);
                    //int qcID = view.Find(itemID);
                    bsQC.Sort = "ItemID";
                    int qcID = bsQC.Find("ItemID", itemID);
                    if (qcID != -1)
                    {
                        bsQC.ResetBindings(false);
                        bsQC.ResumeBinding();
                        //bsQC.Position = (int)view[msID].Row.RowId();
                        //MessageBox.Show(view[qcID].Row["ItemID"].ToString() + ", " + view[qcID].Row["FinishedPTQC"].ToString());
                        bsQC.Position = qcID;
                        //rowView = (DataRowView)this.bsQC.Current;
                        //row = rowView.Row;
                        //MessageBox.Show(row["ItemID"].ToString() + ", " + row["FinishedPTQC"].ToString());
                    }
                    else
                    {
                        bsQC.AddNew();
                        bsQC.ResumeBinding();
                    }
                }
                //MessageBox.Show(bsManItems.Position.ToString());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void txtAdditive_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormatMachineGrid()
        {
            try
            {
                //dgvMachine.UserAddedRow += dgvMachine_RowCountChanged;
                dgvMachine.UserDeletedRow += dgvMachine_UserDeletedRow;
                //dgvMachine.DataBindingComplete += dgvMachine_DataBindingComplete;
                //dgvMachine.RowValidating += dgvMachine_RowValidating;
                DataGridViewCellStyle style = dgvMachine.ColumnHeadersDefaultCellStyle;
                style.BackColor = Color.LightSteelBlue;
                style.ForeColor = Color.Black;
                style.Font = new Font(dgvMachine.Font, FontStyle.Regular);
                dgvMachine.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                dgvMachine.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                dgvMachine.GridColor = SystemColors.ActiveBorder;
                dgvMachine.EnableHeadersVisualStyles = false;
                dgvMachine.AutoGenerateColumns = true;
                dgvMachine.ColumnHeadersHeight = p96H(19);
                dgvMachine.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dgvMachine.AllowUserToResizeRows = false;
                dgvMachine.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

                dgvMachine.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                dgvMachine.Height = p96H(100);
                dgvMachine.RowHeadersWidth = p96W(75);
                dgvMachine.RowTemplate.MinimumHeight = p96H(19);
                dgvMachine.RowTemplate.Height = p96H(19);
                dgvMachine.Rows[dgvMachine.NewRowIndex].MinimumHeight = p96H(19);
                dgvMachine.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dgvMachine.AllowUserToAddRows = false;//dgv adds multiple spurious new rows;  function is now handled by btnAddNewMachine
                dgvMachine.AllowUserToDeleteRows = true;
                dgvMachine.UserDeletingRow += dgvMachine_UserDeletingRow;
                dgvMachine.RowPostPaint += dgvMachine_RowPostPaint;

                //dgvMachine.DataSource = dsMachinePref.Tables[0];
                dgvMachine.Columns["MachPrefID"].Visible = false;
                dgvMachine.Columns["ItemID"].Visible = false;
                dgvMachine.Columns["last_updated_by"].Visible = false;
                dgvMachine.Columns["last_updated_on"].Visible = false;//
                dgvMachine.Columns["MachineABC"].HeaderText = "";
                dgvMachine.Columns["ProgramNo"].HeaderText = "Program No.";
                dgvMachine.Columns["NoPartsPerHour"].HeaderText = "Parts/hr";
                dgvMachine.Columns["CycleTime"].HeaderText = "Cycle Time";

                dgvMachine.Columns["ProgramNo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvMachine.Columns["NoPartsPerHour"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvMachine.Columns["CycleTime"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


                //setup dropdown column for Machine Preference 
                DataGridViewComboBoxColumn cbcMachineABC = new DataGridViewComboBoxColumn();
                MachinePrefDAL dal = new MachinePrefDAL();
                DataSet dsMachineABC = dal.GetMachineABC();
                DataTable dt = dsMachineABC.Tables[0];
                cbcMachineABC.ValueMember = "Preference";
                cbcMachineABC.DisplayMember = "Preference";
                cbcMachineABC.DataSource = dt;
                cbcMachineABC.Name = "MachineABC";
                cbcMachineABC.DataPropertyName = "MachineABC";
                dgvMachine.Columns.Remove("MachineABC");
                dgvMachine.Columns.Insert(2, cbcMachineABC);
                dgvMachine.Columns["MachineABC"].HeaderText = "";
                dgvMachine.Columns["MachineABC"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                cbcMachineABC.DisplayStyleForCurrentCellOnly = true;
                cbcMachineABC.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;

                //this column's data is now shown by header row;  above dropdown column to be removed in future version
                dgvMachine.Columns["MachineABC"].Visible = false;


                //setup dropdown column for Machine 
                DataGridViewComboBoxColumn cbcMachine = new DataGridViewComboBoxColumn();
                DataSet dsMachineIndex = dal.GetMachineIndex();
                dt = dsMachineIndex.Tables[0];
                cbcMachine.DataSource = dt;
                cbcMachine.ValueMember = "MachineID";
                cbcMachine.DisplayMember = "Machine";
                cbcMachine.Name = "Machine";
                cbcMachine.DataPropertyName = "MachineID";
                dgvMachine.Columns.Remove("MachineID");
                dgvMachine.Columns.Insert(3, cbcMachine);
                dgvMachine.Columns["Machine"].HeaderText = "Machine";
                cbcMachine.DisplayStyleForCurrentCellOnly = true;
                cbcMachine.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;

                dgvMachine.Columns["ProgramNo"].Width = p96W(120);
                dgvMachine.Columns["NoPartsPerHour"].Width = p96W(120);
                dgvMachine.Columns["CycleTime"].Width = p96W(120);
                dgvMachine.Columns["MachineABC"].Width = p96W(50);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SaveEdits", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void dgvMachine_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        //{
        //    DataGridViewRow row = dgvMachine.Rows[e.RowIndex];
        //    DataGridViewCell cellABC = row.Cells[dgvMachine.Columns["MachineABC"].Index];
        //    if(cellABC.Value.ToString().Length == 0)
        //    {
        //        int rowIndex = row.Index;
        //        string machineABC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //        string curABC = machineABC.Substring(rowIndex, 1);
        //        cellABC.Value = curABC;
        //    }
        //    e.Cancel = false;
        //}

        //private void dgvMachine_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        //{
        //    foreach (DataGridViewRow row in dgvMachine.Rows)
        //    {
        //        if (row.Cells["MachineABC"].Value == DBNull.Value)
        //        {
        //            int rowIndex = row.Index;
        //            string machineABC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //            string curABC = machineABC.Substring(rowIndex, 1);
        //            //dgvMachine.Rows[rowIndex].Cells["MachineABC"].Value = curABC;
        //        }
        //    }
        //}

        private void dgvMachine_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            btnAddNewMachine.Enabled = dgvMachine.Rows.Count < maxRowsMachine;
        }

        private void dgvMachine_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure?", "Confirm Delete Machine", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void dgvMachine_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // Automatically maintains a Row Header Index Number 
            //   like the Excel row number, independent of sort order

            DataGridView grid = (DataGridView)sender;
            int i = (e.RowIndex);
            string machineABC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string rowIdx = "Machine " + machineABC.Substring(i, 1);

            System.Drawing.Font rowFont = new System.Drawing.Font("Tahoma", 8.0f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);

            StringFormat centerFormat = new StringFormat();
            centerFormat.Alignment = StringAlignment.Center;
            centerFormat.LineAlignment = StringAlignment.Center;

            Rectangle headerBounds = new Rectangle(
                e.RowBounds.Left, e.RowBounds.Top,
                grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, rowFont, SystemBrushes.ControlText,
                headerBounds, centerFormat);
        }

        private void tsbtnCancel_Click(object sender, EventArgs e)
        {
            //? bsAdditive.CancelEdit();
            //? bsCustomer.CancelEdit();
            //? bsGrade.CancelEdit();
            bsMachPref.CancelEdit();
            bsManItems.CancelEdit();
            //? bsMasterBatch.CancelEdit();
            bsMaterialComp.CancelEdit();
            //? bsMaterialGrade.CancelEdit();
            bsMBComp.CancelEdit();
            bsMouldSpec.CancelEdit();
            bsQC.CancelEdit();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void dgvMachine_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            //int rowIndex = e.Row.Index;
            //string machineABC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            //string curABC = machineABC.Substring(rowIndex, 1);
            //dgvMachine.Rows[rowIndex].Cells["MachineABC"].Value = curABC;
            //dgvMachine.Rows[rowIndex].HeaderCell.Value = "Machine " + curABC;
            bsMachPref.AddNew();
        }

        //private void tsbtnExit_Click(object sender, EventArgs e)
        //{
        //    SaveEdits();
        //    this.DialogResult = DialogResult.Retry;
        //    this.Close();
        //}

        private void SaveEdits()
        {
            try
            {
                bsManItems.CurrentChanged -= bsManItems_CurrentChanged; //form is about to close;  will stop RefreshCurrent executing
                DataViewRowState dvrs;
                DataRow[] rows;
                DataSet ds = dsIMSpecificationForm;
                DataRowView drv = (DataRowView)this.bsManItems.Current;
                DataRow row = drv.Row;
                int currentID = (int)row["ItemID"];
                LastItemID = currentID;

                bsManItems.EndEdit();
                bsCustomerProducts.EndEdit();
                bsMaterialComp.EndEdit();
                dgvPolymer.EndEdit();                
                bsMBComp.EndEdit();
                dgvMasterBatchComp.EndEdit();
                bsMouldSpec.EndEdit();
                bsMachPref.EndEdit();
                dgvMachine.EndEdit();
                bsQC.EndEdit();
                
                //Process new items in parent table
                //new ProductDetailsDAL().UpdateMAN_Item(ds, "MAN_Items", "Added", false);
                new MAN_ItemDAL().UpdateMAN_Item(ds, "MAN_Items", "Added");

                DataView dv = new DataView();

                //Update child tables with new parent primary keys
                dvrs = DataViewRowState.Added;
                rows = ds.Tables["MAN_Items"].Select("", "", dvrs);

                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    if (dr.HasVersion(DataRowVersion.Proposed))
                    {
                        if (!object.ReferenceEquals(dr["ItemID", DataRowVersion.Current],
                            dr["ItemID", DataRowVersion.Proposed]))
                        {
                            //int currentID = int.TryParse(dr["ItemID", DataRowVersion.Current].ToString(), out currentID) ? currentID : -999;
                            //int currentID = (int)dr["ItemID", DataRowVersion.Current];
                            int proposedID = (int)dr["ItemID", DataRowVersion.Proposed];
                            drv = (DataRowView)bsManItems.Current;

                            //Process Customer Products
                            dv = new DataView(dsIMSpecificationForm.Tables["CustomerProduct"], "", "ItemID",
                                        DataViewRowState.Added);
                            dv.Sort = "ItemID";
                            DataRowView[] foundRows = dv.FindRows(currentID);
                            for (int j = 0; j < foundRows.Length; j++)
                            {
                                foundRows[j]["ItemID"] = proposedID;
                                foundRows[j].EndEdit();
                            }
                            bsCustomerProducts.EndEdit();

                            //Process Material composition                            
                            dv = new DataView(dsIMSpecificationForm.Tables["MaterialComp"], "", "ItemID",
                                        DataViewRowState.Added);
                            dv.Sort = "ItemID";
                            foundRows = dv.FindRows(currentID);
                            for (int j = 0; j < dv.Count; j++)
                            {
                                foundRows[j]["ItemID"] = proposedID;
                                foundRows[j].EndEdit();
                            }
                            bsMaterialComp.EndEdit();

                            //Process Masterbatch composition                            
                            dv = new DataView(dsIMSpecificationForm.Tables["MasterBatchComp"], "", "ItemID",
                                        DataViewRowState.Added);
                            dv.Sort = "ItemID";
                            foundRows = dv.FindRows(currentID);
                            for (int j = 0; j < dv.Count; j++)
                            {
                                foundRows[j]["ItemID"] = proposedID;
                                foundRows[j].EndEdit();
                            }
                            bsMBComp.EndEdit();

                            //Process Mould Specification                            
                            dv = new DataView(dsIMSpecificationForm.Tables["InjectionMouldSpecification"], "", "ItemID",
                                        DataViewRowState.Added);
                            dv.Sort = "ItemID";
                            foundRows = dv.FindRows(currentID);
                            for (int j = 0; j < dv.Count; j++)
                            {
                                foundRows[j]["ItemID"] = proposedID;
                                foundRows[j].EndEdit();
                            }

                            //Process Machine                            
                            dv = new DataView(dsIMSpecificationForm.Tables["MachinePref"], "", "ItemID",
                                        DataViewRowState.Added);
                            dv.Sort = "ItemID";
                            foundRows = dv.FindRows(currentID);
                            for (int j = 0; j < dv.Count; j++)
                            {
                                foundRows[j]["ItemID"] = proposedID;
                                foundRows[j].EndEdit();
                            }
                            bsMachPref.EndEdit();

                            //Process Quality Control                           
                            dv = new DataView(dsIMSpecificationForm.Tables["QualityControl"], "", "ItemID",
                                        DataViewRowState.Added);
                            dv.Sort = "ItemID";
                            foundRows = dv.FindRows(currentID);
                            for (int j = 0; j < dv.Count; j++)
                            {
                                foundRows[j]["ItemID"] = proposedID;
                                foundRows[j].EndEdit();
                            }
                            bsQC.EndEdit();

                            currentID = proposedID;
                            LastItemID = currentID; 
                        }
                    }
                }




                new CustomerProductDAL().UpdateCustomerProduct(dsIMSpecificationForm, "CustomerProduct");
                new MaterialCompDAL().UpdateMaterialComp(dsIMSpecificationForm, "MaterialComp");
                new MasterBatchCompDAL().UpdateMasterBatchComp(dsIMSpecificationForm, "MasterBatchComp");
                new IMSpecificationDAL().UpdateIMSpecification(dsIMSpecificationForm, "InjectionMouldSpecification");
                new MachinePrefDAL().UpdateMachinePref(dsIMSpecificationForm, "MachinePref");
                new QualityControlDAL().UpdateQualityControl(dsIMSpecificationForm, "QualityControl");
                
                //add a new row for packaging table if one not existing
                DataTable table = dsIMSpecificationForm.Tables["Packaging"];
                DataRow[] prows = table.Select("ItemID = " + LastItemID.ToString());
                if (prows.Length == 0)
                {
                    row = table.NewRow();
                    row["ItemID"] = LastItemID;
                    row["CtnID"] = DBNull.Value;
                    row["PalletID"] = DBNull.Value;
                    row["PackingID"] = table.Rows.Count + 1000;  //create a dummy PK
                    table.Rows.Add(row);
                    
                    new PackagingDAL().UpdatePackaging(dsIMSpecificationForm, "Packaging");
                }

                if (rows.Length > 0)
                {
                    //done with additions - all submitted
                    dsIMSpecificationForm.Tables["MAN_Items"].AcceptChanges();
                }


                //process parent modified/deleted
                //new ProductDetailsDAL().UpdateMAN_Item(dsIMSpecificationForm, "MAN_Items", "Modify/Delete", true);
                new MAN_ItemDAL().UpdateMAN_Item(ds, "MAN_Items", "Modified/Deleted");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SaveEdits", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void IntControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtITEMNMBR_Validating(object sender, CancelEventArgs e)
        {
            //check not empty
            //check unique
            string errorMsg = "";
            if (!ValidProductCode(txtITEMNMBR.Text, out errorMsg))
            {
                // Cancel the event and select the text to be corrected by the user.                
                txtITEMNMBR.Select(0, txtITEMNMBR.Text.Length);

                // Set the ErrorProvider error with the text to display. 
                this.errorProvider1.SetError(txtITEMNMBR, errorMsg);
                e.Cancel = true;

                //Dont do MessageBox because focus shifts to there and retriggers validation.  Better to use a label
                //MessageBox.Show(errorMsg, "Duplicate Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }

            //display (or clear) error message in a label instead.
            lblErrorMsg.Text = errorMsg;

        }

        private bool ValidProductCode(string ProductCode, out string errorMessage)
        {
            // Confirm that the email address string is not empty.
            if (ProductCode.Length == 0)
            {
                errorMessage = "Product code is required.";
                return false;
            }

            // Confirm that freight company name is not duplicated
            if (!ProductCodeDuplicated(ProductCode))
            {
                errorMessage = "";
                return true;
            }


            errorMessage = "This product code already exists.";
            return false;
        }

        private bool ProductCodeDuplicated(string ProductCode)
        {
            //code to find duplicates, return true if there any
            DataView view = new DataView();
            view.Table = dsIMSpecificationForm.Tables["MAN_Items"];
            view.RowFilter = "ITEMNMBR = '" + ProductCode + "'";
            view.RowStateFilter = DataViewRowState.CurrentRows;

            //must be unique if on new row
            if (OnNewRow)
            {
                if (view.Count > 0)
                    return true;
                else
                    return false;
            }
            //otherwise, allow 0 or 1
            else
            {
                if (view.Count > 1)
                    return true;
                else
                    return false;
            }
        }

        private void txtITEMNMBR_Validated(object sender, EventArgs e)
        {
            string errorMsg = "";
            this.errorProvider1.SetError(txtITEMNMBR, errorMsg);
        }

        private void txtITEMDESC_Validating(object sender, CancelEventArgs e)
        {
            //check not empty
            //check unique
            string errorMsg = "";
            if (!ValidProductDesc(txtITEMDESC.Text, out errorMsg))
            {
                // Cancel the event and select the text to be corrected by the user.                
                txtITEMDESC.Select(0, txtITEMDESC.Text.Length);

                // Set the ErrorProvider error with the text to display. 
                this.errorProvider1.SetError(txtITEMDESC, errorMsg);
                e.Cancel = true;

                //Dont do MessageBox because focus shifts to there and retriggers validation.  Better to use a label
                //MessageBox.Show(errorMsg, "Duplicate Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }

            //display (or clear) error message in a label instead.
            lblErrorMsg.Text = errorMsg;
        }

        private bool ValidProductDesc(string ProductDesc, out string errorMessage)
        {
            // Confirm that the email address string is not empty.
            if (ProductDesc.Length == 0)
            {
                errorMessage = "Product description is required.";
                return false;
            }

            // Confirm that freight company name is not duplicated
            if (!ProductDescDuplicated(ProductDesc))
            {
                errorMessage = "";
                return true;
            }


            errorMessage = "This product description already exists.";
            return false;
        }

        private bool ProductDescDuplicated(string ProductDesc)
        {
            //code to find duplicates, return true if there any
            DataView view = new DataView();
            view.Table = dsIMSpecificationForm.Tables["MAN_Items"];
            view.RowFilter = "ITEMDESC = '" + ProductDesc + "'";
            view.RowStateFilter = DataViewRowState.CurrentRows;

            //must be unique if on new row
            if (OnNewRow)
            {
                if (view.Count > 0)
                    return true;
                else
                    return false;
            }
            //otherwise, allow 0 or 1
            else
            {
                if (view.Count > 1)
                    return true;
                else
                    return false;
            }
        }

        private void txtITEMDESC_Validated(object sender, EventArgs e)
        {
            string errorMsg = "";
            this.errorProvider1.SetError(txtITEMDESC, errorMsg);
        }

        private void ResizeControls()
        {
            //splitContainer1.SplitterDistance = p96H(30);
            bindingNavigator1.Height = p96H(25);
            gpGeneral.Size = new Size(p96W(gpGeneral.Width), p96H(gpGeneral.Height));
            gpWeight.Size = new Size(p96W(gpWeight.Width), p96H(gpWeight.Height));
            gpMaterial.Size = new Size(p96W(gpMaterial.Width), p96H(gpMaterial.Height));
            gpMould.Size = new Size(p96W(gpMould.Width), p96H(gpMould.Height));
            gpCooling.Size = new Size(p96W(gpCooling.Width), p96H(gpCooling.Height));
            gpMachine.Size = new Size(p96W(gpMachine.Width), p96H(gpMachine.Height));
            gpMouldRequirements.Size = new Size(p96W(gpMouldRequirements.Width), p96H(gpMouldRequirements.Height));

            dgvPolymer.Size = new Size(p96W(dgvPolymer.Width), p96H(dgvPolymer.Height));

            tableLayoutPanel1.RowStyles[0].Height = p96H((int)tableLayoutPanel1.RowStyles[0].Height);
            tableLayoutPanel1.RowStyles[1].Height = p96H((int)tableLayoutPanel1.RowStyles[1].Height);
            tableLayoutPanel1.RowStyles[2].Height = p96H((int)tableLayoutPanel1.RowStyles[2].Height);
            tableLayoutPanel1.RowStyles[3].Height = p96H((int)tableLayoutPanel1.RowStyles[3].Height);

            tableLayoutPanel2.RowStyles[0].Height = p96H((int)tableLayoutPanel2.RowStyles[0].Height);
            tableLayoutPanel2.RowStyles[1].Height = p96H((int)tableLayoutPanel2.RowStyles[1].Height);
            tableLayoutPanel2.RowStyles[2].Height = p96H((int)tableLayoutPanel2.RowStyles[2].Height);

            tableLayoutPanel3.RowStyles[0].Height = p96H((int)tableLayoutPanel3.RowStyles[0].Height);
            tableLayoutPanel3.RowStyles[1].Height = p96H((int)tableLayoutPanel3.RowStyles[1].Height);

            tableLayoutPanel4.RowStyles[0].Height = p96H((int)tableLayoutPanel4.RowStyles[0].Height);
            tableLayoutPanel4.RowStyles[1].Height = p96H((int)tableLayoutPanel4.RowStyles[1].Height);

            lblITEMNMBR.Size = new System.Drawing.Size(p96W(lblITEMNMBR.Width), p96H(lblITEMNMBR.Height));
            txtITEMNMBR.Size = new System.Drawing.Size(p96W(txtITEMNMBR.Width), p96H(txtITEMNMBR.Height));
            lblITEMDESC.Size = new System.Drawing.Size(p96W(lblITEMDESC.Width), p96H(lblITEMDESC.Height));
            txtITEMDESC.Size = new System.Drawing.Size(p96W(txtITEMDESC.Width), p96H(txtITEMDESC.Height));
            lblCUSTNAME.Size = new System.Drawing.Size(p96W(lblCUSTNAME.Width), p96H(lblCUSTNAME.Height));
            cboCUSTNAME.Size = new System.Drawing.Size(p96W(cboCUSTNAME.Width), p96H(cboCUSTNAME.Height));
            lblGradeID.Size = new System.Drawing.Size(p96W(lblGradeID.Width), p96H(lblGradeID.Height));
            cboGradeID.Size = new System.Drawing.Size(p96W(cboGradeID.Width), p96H(cboGradeID.Height));

        }

        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = string.Empty;
                OpenFileDialog ofd = new OpenFileDialog();

                //set the opening directory
                var appSettings = ConfigurationManager.AppSettings;
                string initialDir = appSettings["QCImageFolderDir"];
                ofd.InitialDirectory = initialDir;
                if (!ofd.CheckPathExists)
                {
                    MessageBox.Show("Images directory not found");
                    ofd.InitialDirectory = "C:\\";
                }
                ofd.Filter = "jpg files (*.jpg)|*.jpg|pdf files (*.pdf)|*.pdf|txt files (*.txt)|*.txt|All files (*.*)|*.*";
                ofd.FilterIndex = 1;
                ofd.RestoreDirectory = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = ofd.FileName;
                    //txtImageFile.Text = filePath;
                    //DataRowView rowView = (DataRowView)this.bsManItems.Current;
                    //DataRow row = rowView.Row;                                        
                    picImageFile.ImageLocation = filePath;
                    txtImageFile.Text = filePath;
                    //row.EndEdit();                    
                    //bsManItems.EndEdit();
                    //bsManItems.Position = rowPos;
                    //bsManItems.ResumeBinding();
                    //RefreshCurrent();
                    //txtImageFile.Text = filePath;
                    //picImageFile.ImageLocation = filePath;
                    //bsMBComp.EndEdit();

                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); };

        }

        private void SpecificationDataEntry_Shown(object sender, EventArgs e)
        {
            //ResizeControls();
            //Rectangle r = new Rectangle(5, 5, p96W(1000), p96H(1100));
            this.Size = new Size(p96W(1024), p96H(935));
            bindingNavigator1.Height = p96H(30);
            splitContainer1.SplitterDistance = p96H(55);
            //btnDeleteAdditive.Size = new System.Drawing.Size(p96W(24), p96W(24));
           // btnDeleteMB.Size = new System.Drawing.Size(p96W(24), p96W(24));
            this.menuStrip1.Focus();
            this.productSpecificationToolStripMenuItem.Select();
            dgvPolymer.ClearSelection();
            dgvMachine.ClearSelection();
            SetFormState();
        }

        private void gpMaterial_Enter(object sender, EventArgs e)
        {

        }
    }
}
