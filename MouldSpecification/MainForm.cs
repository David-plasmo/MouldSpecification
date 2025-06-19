//using IMSpecification;
using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using static Utils.DrawingUtils;

//Useful links for references on holding images in WinForms
//https://www.codeproject.com/Tips/5359158/Adding-Images-to-a-Winforms-Project-so-They-are-Ac
//https://stackoverflow.com/questions/2041000/loop-through-all-the-resources-in-a-resx-file

namespace MouldSpecification
{
    
    public partial class MainForm : Form
    {
        Form curForm = null;

        int? curItemID = null;
        int? curCustID = null;

        private static string namespacePrefix = "MouldSpecification.";

        bool ComboBoxBusy;
        
        public MainForm()
        {
            InitializeComponent();
        }

        
        private void tvMain_MouseDown(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            TreeNode selectedNode = tvMain.GetNodeAt(e.X, e.Y);
            SplitContainer sc = this.splitContainer1;

            if (selectedNode != null)
            {
                if (selectedNode.Text == "Data Table Maintenance"
                    || (selectedNode.Parent != null && selectedNode.Parent.Text == "Data Table Maintenance"))
                {
                    //Show Table Maintenance filter controls
                    sc.SplitterDistance = p96H(70);
                }
                else if (selectedNode.Text == "Product Data Entry"
                    || (selectedNode.Parent != null && selectedNode.Parent.Text == "Product Data Entry"))
                {
                    //Hide Table Maintenance filter controls
                    sc.SplitterDistance = p96H(25);
                }
                tvMain.SelectedNode = selectedNode;
                ShowInputForm(selectedNode.Name);
            }
        }

        private void ShowInputForm(string formName)
        {
            try
            {
                SplitterPanel sp = this.splitContainer2.Panel2;


                Form f;

                if (curForm != null)
                {
                    curForm.Close();
                    if (curForm.Visible)
                        return;

                    curForm = null;
                }
                if (cboCustomer.Text.Length == 0) { curCustID = null; }
                if (cboProduct.Text.Length == 0) { curItemID = null; }

                switch (formName)
                {
                    case "IMSpecificationDataEntry":

                        int? lastItemID = null;
                        int? lastCustomerID = null;
                        string nextForm = "SpecificationDataEntry";
                        bool customerFilterOn = true;

                    next_form: //enables navigating between specification, packaging and assembly data entry forms
                        if (nextForm == "SpecificationDataEntry")
                        {
                            SpecificationDataEntry sdeForm = new SpecificationDataEntry(lastItemID, lastCustomerID, customerFilterOn);
                            curForm = sdeForm;

                            while (sdeForm.ShowDialog() == DialogResult.Retry)
                            {
                                lastItemID = sdeForm.LastItemID;
                                lastCustomerID = sdeForm.LastCustomerID;
                                nextForm = sdeForm.NextForm; //enables opening other dataentry form   
                                customerFilterOn = sdeForm.CustomerFilterOn;

                                sdeForm.Dispose();
                                sdeForm = null;
                                if (nextForm != "SpecificationDataEntry")
                                    goto next_form;
                                sdeForm = new SpecificationDataEntry(lastItemID, lastCustomerID, customerFilterOn);
                            }
                            break;
                        }
                        else if (nextForm == "PackagingDataEntry")
                        {
                            PackagingDataEntry pdeForm = new PackagingDataEntry(lastItemID, lastCustomerID, customerFilterOn);
                            curForm = pdeForm;

                            while (pdeForm.ShowDialog() == DialogResult.Retry)
                            {
                                lastItemID = pdeForm.LastItemID;
                                lastCustomerID = pdeForm.LastCustomerID;
                                nextForm = pdeForm.NextForm; //enables opening other dataentry form   
                                customerFilterOn = pdeForm.CustomerFilterOn;
                                pdeForm.Dispose();
                                pdeForm = null;
                                if (nextForm != "PackagingDataEntry")
                                    goto next_form;
                                pdeForm = new PackagingDataEntry(lastItemID, lastCustomerID, customerFilterOn);
                            }
                            break;
                        }
                        //else if (nextForm == "AssemblyDataEntry")
                        //{
                        //    AssemblyDataEntry adeForm = new AssemblyDataEntry(lastItemID, lastCustomerID);
                        //    curForm = adeForm;

                        //    while (adeForm.ShowDialog() == DialogResult.Retry)
                        //    {
                        //        lastItemID = adeForm.LastItemID;
                        //        nextForm = adeForm.NextForm; //enables opening other dataentry form   
                        //        adeForm.Dispose();
                        //        adeForm = null;
                        //        if (nextForm != "AssemblyDataEntry")
                        //            goto next_form;
                        //        adeForm = new AssemblyDataEntry(lastItemID, lastCustomerID);
                        //    }
                        //    break;
                        //}
                        else if (nextForm == "QCDataEntry")
                        {
                            QCDataEntry qdeForm = new QCDataEntry(lastItemID, lastCustomerID, customerFilterOn);
                            curForm = qdeForm;

                            while (qdeForm.ShowDialog() == DialogResult.Retry)
                            {
                                lastItemID = qdeForm.LastItemID;
                                nextForm = qdeForm.NextForm; //enables opening other dataentry form
                                customerFilterOn = qdeForm.CustomerFilterOn;
                                qdeForm.Dispose();
                                qdeForm = null;
                                if (nextForm != "QCDataEntry")
                                    goto next_form;
                                qdeForm = new QCDataEntry(lastItemID, lastCustomerID, customerFilterOn);
                            }
                            break;
                        }
                        else if (nextForm == "AttachedDocsDataEntry")
                        {
                            AttachedDocsDataEntry addeForm = new AttachedDocsDataEntry(lastItemID, lastCustomerID, customerFilterOn);
                            curForm = addeForm;

                            while (addeForm.ShowDialog() == DialogResult.Retry)
                            {
                                lastItemID = addeForm.LastItemID;
                                nextForm = addeForm.NextForm; //enables opening other dataentry form   
                                addeForm.Dispose();
                                addeForm = null;
                                if (nextForm != "AttachedDocsDataEntry")
                                    goto next_form;
                                addeForm = new AttachedDocsDataEntry(lastItemID, lastCustomerID);
                            }
                            break;
                        }

                        break;


                    case "ProductDetails":
                        f = new ProductDetails(curItemID, curCustID);
                        curForm = f;
                        f.TopLevel = false;
                        f.Size = sp.ClientSize;
                        sp.Controls.Add(f);
                        f.Dock = DockStyle.Fill;
                        f.Show();
                        Cursor.Current = Cursors.Default;
                        break;
                    case "MaterialComp":
                        f = new MaterialComp(curItemID, curCustID);
                        curForm = f;
                        f.TopLevel = false;
                        sp = this.splitContainer2.Panel2;
                        f.Size = sp.ClientSize;
                        sp.Controls.Add(f);
                        f.Dock = DockStyle.Fill;
                        f.Show();
                        break;
                    case "MasterBatchComp":
                        f = new MasterBatchComp(curItemID, curCustID);
                        curForm = f;
                        f.TopLevel = false;
                        sp = this.splitContainer2.Panel2;
                        f.Size = sp.ClientSize;
                        sp.Controls.Add(f);
                        f.Dock = DockStyle.Fill;
                        f.Show();
                        break;
                    case "MachinePref":
                        f = new MachinePref(curItemID, curCustID);
                        curForm = f;
                        f.TopLevel = false;
                        sp = this.splitContainer2.Panel2;
                        f.Size = sp.ClientSize;
                        sp.Controls.Add(f);
                        f.Dock = DockStyle.Fill;
                        f.Show();
                        break;
                    case "CustomerPriceQty":
                        f = new CustomerPriceQty(curItemID, curCustID);
                        curForm = f;
                        f.TopLevel = false;
                        sp = this.splitContainer2.Panel2;
                        f.Size = sp.ClientSize;
                        sp.Controls.Add(f);
                        f.Dock = DockStyle.Fill;
                        f.Show();
                        f.UseWaitCursor = false;
                        break;
                    case "MouldSpecification":
                        f = new IMSpecification(curItemID, curCustID);
                        curForm = f;
                        f.TopLevel = false;
                        sp = this.splitContainer2.Panel2;
                        f.Size = sp.ClientSize;
                        sp.Controls.Add(f);
                        f.Dock = DockStyle.Fill;
                        f.Show();
                        break;
                    case "QualityControl":
                        f = new QualityControl(curItemID, curCustID);
                        curForm = f;
                        f.TopLevel = false;
                        sp = this.splitContainer2.Panel2;
                        f.Size = sp.ClientSize;
                        sp.Controls.Add(f);
                        f.Dock = DockStyle.Fill;
                        f.Show();
                        break;
                    case "Packaging":
                        f = new Packaging(curItemID, curCustID);
                        curForm = f;
                        f.TopLevel = false;
                        sp = this.splitContainer2.Panel2;
                        f.Size = sp.ClientSize;
                        sp.Controls.Add(f);
                        f.Dock = DockStyle.Fill;
                        f.Show();
                        break;
                }
            }
            catch (Exception ex) 
            {  
                MessageBox.Show(ex.Message, "ShowInputForm", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            tvMain.HideSelection = false;
            cboCustomer.ValueMember = "CustomerID";
            cboCustomer.DisplayMember = "CUSTNAME";
            cboProduct.ValueMember = "ItemID";
            cboProduct.DisplayMember = "DisplayValue"; // "ITEMNMBR";
            cboProduct.Font = new System.Drawing.Font("Courier New", 8);
            cboProduct.AutoCompleteMode = AutoCompleteMode.Suggest;
            LoadCustomerCbo();

            Cursor.Current = Cursors.Default;
        }

        private void LoadCustomerCbo()
        {
            DataSet dsCustomer = new MainFormDAL().SelectCustomerIndex("IM"); //filters on customers for Injection Mould products
            cboCustomer.DataSource = dsCustomer.Tables[0];
            cboCustomer.Text = "";
            curCustID = null;
            curItemID = null;

            DataSet dsProduct = new MainFormDAL().SelectItemByCustomer(curCustID);
            cboProduct.DataSource = dsProduct.Tables[0];
            cboProduct.Text = "";

            curItemID = null;
            cboCustomer.TextChanged += CboCustomer_TextChanged;
            cboCustomer.SelectedIndexChanged += CboCustomer_SelectedIndexChanged;
        }

        private void CboCustomer_TextChanged(object sender, EventArgs e)
        {
            if (cboCustomer.Text.Length == 0)
            {
                cboProduct.SelectedIndexChanged -= CboProduct_SelectedIndexChanged;
                curCustID = null;
                DataSet dsProduct = new MainFormDAL().SelectItemByCustomer(curCustID);
                cboProduct.DataSource = dsProduct.Tables[0];

                cboProduct.Text = "";
                curItemID = null;
                cboProduct.SelectedIndexChanged += CboProduct_SelectedIndexChanged;

            }
        }

        private void CboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCustomer.SelectedIndex != -1 || cboCustomer.Text.Length == 0)
            {
                curCustID = (int)cboCustomer.SelectedValue;
                DataSet dsProduct = new MainFormDAL().SelectItemByCustomer(curCustID);
                cboProduct.SelectedIndexChanged -= CboProduct_SelectedIndexChanged;
                cboProduct.DataSource = dsProduct.Tables[0];

                cboProduct.Text = "";
                curItemID = null;
                cboProduct.SelectedIndexChanged += CboProduct_SelectedIndexChanged;
            }
        }

        private void CboProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboProduct != null && cboProduct.SelectedIndex != -1)
            {
                curItemID = (int)cboProduct.SelectedValue;
                if (curForm != null) { ShowInputForm(curForm.Name); }
            }
        }

        /*
private void LoadProductCbo()
{
   MainFormDAL dal = new MainFormDAL();
   DataSet ds = dal.SelectMAN_ItemIndex();
   System.Data.DataTable dt = ds.Tables[0];

   ComboBoxBusy = false;
   CBFullList = new Dictionary<string, Int32>();
   CBFilteredList = new Dictionary<string, Int32>();

   this.cboProduct.Items.Clear();
   var dict = new Dictionary<string, string>();
   for (int i = 0; i<dt.Rows.Count; i++)
   {
       CBFullList.Add(dt.Rows[i]["DisplayValue"].ToString(), (int)dt.Rows[i]["ItemID"]);
       FilterList(false);
   }

   cboProduct.DropDownWidth = 400;
   cboProduct.AutoCompleteMode = AutoCompleteMode.Suggest;
   //AutoCompleteBehaviour acb = new AutoCompleteBehaviour(cboProduct);
   cboProduct.SelectedIndexChanged += cboProduct_SelectedIndexChanged;
   cboProduct.TextUpdate += cboProduct_TextUpdate;
}

private void cboProduct_TextUpdate(object sender, EventArgs e)
{
   FilterList(true);
}

private void FilterList(bool show)
{
   if (ComboBoxBusy == false)
   {
       String orgText;

       ComboBoxBusy = true;
       orgText = cboProduct.Text;

       cboProduct.DroppedDown = false;

       CBFilteredList.Clear();

       foreach (KeyValuePair<string, Int32> item in CBFullList)
       {
           if (item.Key.ToUpper().Contains(orgText.ToUpper()))
               CBFilteredList.Add(item.Key, item.Value);
       }

       if (CBFilteredList.Count < 1)
           CBFilteredList.Add("None", 0);

       cboProduct.BeginUpdate();
       cboProduct.DataSource = new BindingSource(CBFilteredList, null);
       cboProduct.DisplayMember = "Key";
       cboProduct.ValueMember = "Value";

       cboProduct.DroppedDown = show;
       cboProduct.SelectedIndex = -1;
       cboProduct.Text = orgText;
       cboProduct.Select(cboProduct.Text.Length, 0);
       cboProduct.EndUpdate();

       ComboBoxBusy = false;
   }
}


private void cboProduct_SelectedIndexChanged(object sender, EventArgs e)
{
   if (ComboBoxBusy == false)
   {
       FilterList(false);
   }

   if (cboProduct != null && cboProduct.SelectedIndex != -1 && !ComboBoxBusy)
   {
       Cursor.Current = Cursors.WaitCursor;
       curItemID = (int)cboProduct.SelectedValue;
       TreeNode selectedNode = tvMain.Nodes["ProductDetails"];
       tvMain.Focus();
       tvMain.SelectedNode = selectedNode;
       ShowInputForm(selectedNode.Name);
       Cursor.Current = Cursors.Default;
   }
}
*/

        private void MainForm_Shown(object sender, EventArgs e)
        {
            SplitContainer sc = this.splitContainer2;
            sc.SplitterDistance = (int)((int)(double)this.Width * .15);
            sc = this.splitContainer1;
            sc.SplitterDistance = p96H(25);
            curForm = null;
            TreeNode selectedNode = tvMain.Nodes["IMSpecificationDataEntryRoot"];
            selectedNode.ExpandAll();
            //TreeNode selectedNode = tvMain.Nodes["ProductDetails"];
            //tvMain.Focus();
            //tvMain.SelectedNode = selectedNode;
            //ShowInputForm(selectedNode.Name);
        }

        //private void packagingToolStripMenuItem_Click(object sender, EventArgs e)
        //{

        //}

        private void btnImportAccess_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            MainFormDAL dal = new MainFormDAL();
            dal.UpdateAccessWorkTables();
            Cursor.Current = Cursors.Default;
            MessageBox.Show("Injection Moulding Specification Import Succeeded.", "Import Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnGoToAccess_Click(object sender, EventArgs e)
        {
        }

        private void chkClearProduct_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void blowMouldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdditiveCost f = new AdditiveCost();
            f.ShowDialog();
        }

        private void polymerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void materialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MaterialGradeCost f = new MaterialGradeCost();
            f.ShowDialog();
        }

        private void materialTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MaterialType f = new MaterialType();
            f.ShowDialog();
        }

        private void machineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Machine f = new Machine();
            f.ShowDialog();
        }

        private void masterBatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MasterBatch f = new MasterBatch();
            f.ShowDialog();
        }

        private void injectionMouldToolStripMenuItem2_Click(object sender, EventArgs e)
        {

            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = @"msaccess";
            processStartInfo.Arguments = "" + '\u0022' + "S:\\CONSOLIDATED PLASTICS\\INJECTION MOULDING\\Database\\Injection Mould Specifications.accdb" + '\u0022' + "";

            Process.Start(processStartInfo);
        }

        private void blowMouldToolStripMenuItem3_Click(object sender, EventArgs e)
        {

            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = @"msaccess";
            processStartInfo.Arguments = "" + '\u0022' + "S:\\PRODUCTION\\PLASMO\\DATABASE\\Blow Mould Specifications.accdb" + '\u0022' + "";

            Process.Start(processStartInfo);
        }

        private void fixedCostsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FixedCost f = new FixedCost();
            f.ShowDialog();
        }

        private void injectionMouldToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                ImportAccessIM f = new ImportAccessIM();
                f.ShowDialog();
                f.Dispose();

                //Cursor.Current = Cursors.WaitCursor;
                //new MainFormDAL().ImportAccessIM();
                //Cursor.Current = Cursors.Default;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ////Parking this because of issue seeing provider for Access 16 in sql 2022 dev edition
            ///and issue with blanks in connection strings in dtsx file.
            ////
            //// Variables
            //string targetServerName = "SPECTRE";
            //string folderName = "Project1Folder";
            //string projectName = "Integration Services Project1";
            //string packageName = "Package.dtsx";

            //// Create a connection to the server
            //string sqlConnectionString = "Data Source=" + targetServerName +
            //    ";Initial Catalog=master;Integrated Security=true;";
            //SqlConnection sqlConnection = new SqlConnection(sqlConnectionString);
            //sqlConnection.Open();

            //// Create the Integration Services object
            //IntegrationServices integrationServices = new IntegrationServices(sqlConnection);

            //// Get the Integration Services catalog
            //Catalog catalog = integrationServices.Catalogs["SSISDB"];

            //// Get the folder
            //CatalogFolder folder = catalog.Folders[folderName];

            //// Get the project
            //ProjectInfo project = folder.Projects[projectName];

            //// Get the package
            //PackageInfo package = project.Packages[packageName];

            //// Run the package
            //package.Execute(false, null);
        }

        private void extrusionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void blowMouldToolStripMenuItem4_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (curForm != null)
                curForm.Close();
        }

        private void updateImageAndAttachmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            new MainFormDAL().UpdateAttachmentFilepaths();
            MessageBox.Show("Image and attachment Filepaths updated.", "Update Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Cursor = Cursors.Default;
        }

        private void blowMouldToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void cboProduct_DropDownClosed(object sender, EventArgs e)
        {
            if (cboProduct.Text.Length == 0)
            {
                curItemID = null;
            }
        }

        private void cboProduct_TextChanged(object sender, EventArgs e)
        {
            if (cboProduct.Text.Length == 0)
            {
                curItemID = null;
            }
        }

        private void injectionMouldToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            try
            {
                //** won't load as a dll
                //IMSpecificationReport f = new IMSpecificationReport();
                //WindowsFormsFramework.ReportPrompt f = new WindowsFormsFramework.ReportPrompt();
                //f.ShowDialog();

                // load as exe
                var appSettings = ConfigurationManager.AppSettings;
                string reportPath = appSettings["IMReportsPath"];
                string appStartPath = Application.StartupPath;
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = appStartPath + reportPath + @"\InjectionMouldReports.exe";
                info.UseShellExecute = true;
                info.CreateNoWindow = false;
                info.WorkingDirectory = reportPath;
                Process proc = Process.Start(info);
                proc.WaitForExit();
                proc.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void suppliersToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        //private void injectionMouldToolStripMenuItem4_Click(object sender, EventArgs e)
        //{

        //}

        //private void testReportToolStripMenuItem_Click(object sender, EventArgs e)
        //{

        //}

        //private void button1_Click(object sender, EventArgs e)
        //{

        //}

        //private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        //{

        //}

        //private void injectionMouldToolStripMenuItem4_Click_1(object sender, EventArgs e)
        //{

        //}
    }
}
