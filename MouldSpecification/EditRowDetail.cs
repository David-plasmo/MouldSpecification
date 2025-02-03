using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using GridSettings;
using DataService;

namespace MouldSpecification
{
    public partial class EditRowDetail : Form
    {
        public DataTable EditDT;
        public dgSettingsDictionary dgcols;
        DataSet dsMG;
        DataSet dsMAG;
        DataSet dsMC;
        DataSet IMSpecificationDS;
        DataSet dsAdditive;
        DataSet dsCartonPackagingDropdown;
        
        TextBox tbx;
        ComboBox cbo;
        ComboBox cboMachine;
        ComboBox cboCustomer;
        ComboBox cboCompanyCode;        
        ComboBox cboProductGrade;
        ComboBox cboPallet;
        ComboBox cboMBColour;       
        ComboBox cboMaterialGrade;        
        ComboBox cboAdditiveCode;
        ComboBox cboCartonPackaging;
        ComboBox cboOperation;

        bool bIsLoading;
        bool bClosing = false;
        bool nonNumberEntered = false;        
        string curImagePath;

        public EditRowDetail()
        {
            InitializeComponent();
        }
        private void EditRowDetail_Load(object sender, EventArgs e)
        {
            bIsLoading = true;
            DoLoad();
            SetupCols();
            bIsLoading = false;
        }
        private void DoLoad()
        {
            try
            {                
                dgcols = new dgSettingsDictionary("IM");
                
                this.tbx = new TextBox();
                this.tbx.Multiline = true;
                this.tbx.Visible = false;
                this.dgvEdit.Controls.Add(this.tbx);
                
                cboMaterialGrade = new ComboBox();
                cboMaterialGrade.Visible = false;
                dsMG = new ProductDataService().GetMaterialAndGrade();
                
                DataSet dsC = new DataService.ProductDataService().SelectCPCustomerDropdown();
                this.cboCustomer = new ComboBox();
                this.cboCustomer.Width = 200;
                this.cboCustomer.ValueMember = "CustID";
                this.cboCustomer.DisplayMember = "Customer";
                this.cboCustomer.DataSource = dsC.Tables[0];
                this.cboCustomer.Visible = false;
                this.dgvEdit.Controls.Add(this.cboCustomer);                
                this.cboCustomer.SelectedIndexChanged += new System.EventHandler(cboCustomer_SelectedIndexChanged);

                DataSet dsPg = new ProductDataService().GetProductGrade();
                this.cboProductGrade = new ComboBox();
                this.cboProductGrade.ValueMember = "GradeID";
                this.cboProductGrade.DisplayMember = "Description";
                this.cboProductGrade.DataSource = dsPg.Tables[0];
                this.cboProductGrade.Visible = false;
                this.dgvEdit.Controls.Add(this.cboProductGrade);                
                this.cboProductGrade.SelectedIndexChanged += new System.EventHandler(cboProductGrade_SelectedIndexChanged);

                DataSet dsM = new DataService.ProductDataService().GetMachine("IM");
                this.cboMachine = new ComboBox();
                this.cboMachine.ValueMember = "MachineID";
                this.cboMachine.DisplayMember = "Machine";
                this.cboMachine.DataSource = dsM.Tables[0];
                this.cboMachine.Visible = false;
                this.dgvEdit.Controls.Add(this.cboMachine); 
                this.cboMachine.SelectedIndexChanged += new System.EventHandler(cboMachine_SelectedIndexChanged);

                DataSet dsCmp = new DataService.ProductDataService().GetCompany();
                this.cboCompanyCode = new ComboBox();
                this.cboCompanyCode.ValueMember = "CompanyCode";
                this.cboCompanyCode.DisplayMember = "CompanyCode";
                this.cboCompanyCode.DataSource = dsCmp.Tables[0];
                this.cboCompanyCode.Visible = false;
                this.dgvEdit.Controls.Add(this.cboCompanyCode);                
                this.cboCompanyCode.SelectedIndexChanged += new System.EventHandler(cboCompanyCode_SelectedIndexChanged);
                               
                dsMAG = new DataService.ProductDataService().MaterialAndGradeDropdown();
                this.cboMaterialGrade = new ComboBox();
                this.cboMaterialGrade.ValueMember = "MaterialGradeID";
                this.cboMaterialGrade.DisplayMember = "ComboList";
                this.cboMaterialGrade.DataSource = dsMAG.Tables[0];
                this.cboMaterialGrade.Visible = false;                
                this.cboMaterialGrade.Font = new Font("Lucida Sans Typewriter", 10);
                this.cboMaterialGrade.DropDownWidth = 400;
                this.dgvEdit.Controls.Add(this.cboMaterialGrade);                
                this.cboMaterialGrade.SelectedIndexChanged += new System.EventHandler(cboMaterialGrade_SelectedIndexChanged);
                this.cboMaterialGrade.TextChanged += new System.EventHandler(cboMaterialGrade_TextChanged);

                dsMC = new ProductDataService().MasterBatchDropdown();
                this.cboMBColour = new ComboBox();
                //this.cboMaterial.Width = 200;
                this.cboMBColour.DataSource = dsMC.Tables[0];
                this.cboMBColour.DisplayMember = "ComboList";
                this.cboMBColour.ValueMember = "MBID";
                this.cboMBColour.Font = new Font("Lucida Sans Typewriter", 10);
                this.cboMBColour.DropDownWidth = 410;
                this.cboMBColour.Visible = false;
                this.dgvEdit.Controls.Add(this.cboMBColour);                
                this.cboMBColour.SelectedIndexChanged += new System.EventHandler(cboMBColour_SelectedIndexChanged);
                this.cboMBColour.TextChanged += new System.EventHandler(cboMBColour_TextChanged);

                DataSet dsP = new DataService.ProductDataService().GetPallet();
                this.cboPallet = new ComboBox();
                //this.cboMaterial.Width = 200;
                this.cboPallet.DataSource = dsP.Tables[0];
                this.cboPallet.DisplayMember = "Pallet";
                this.cboPallet.ValueMember = "Pallet";
                this.cboPallet.Visible = false;
                this.dgvEdit.Controls.Add(this.cboPallet);                
                this.cboPallet.SelectedIndexChanged += new System.EventHandler(cboPallet_SelectedIndexChanged);

                //dsAdditive = new DataService.ProductDataService().GetAdditive();
                dsAdditive = new ProductDataService().AdditiveDropDown();
                this.cboAdditiveCode = new ComboBox();
                //this.cboMaterial.Width = 200;
                this.cboAdditiveCode.ValueMember = "ADDID";
                this.cboAdditiveCode.DisplayMember = "Combolist";
                this.cboAdditiveCode.DataSource = dsAdditive.Tables[0];
                this.cboAdditiveCode.Visible = false;
                this.cboAdditiveCode.Font = new Font("Lucida Sans Typewriter", 8);
                this.cboAdditiveCode.DropDownWidth = 500;
                this.dgvEdit.Controls.Add(this.cboAdditiveCode);                
                this.cboAdditiveCode.SelectedIndexChanged += new System.EventHandler(cboAdditiveCode_SelectedIndexChanged);
                this.cboAdditiveCode.TextChanged += new System.EventHandler(cboAdditiveCode_TextChanged);

                dsCartonPackagingDropdown = new DataService.ProductDataService().GetCartonPackagingDropdown();
                this.cboCartonPackaging = new ComboBox();
                //this.cboMaterial.Width = 200;
                this.cboCartonPackaging.ValueMember = "CtnID";
                this.cboCartonPackaging.DisplayMember = "CartonType";
                this.cboCartonPackaging.DataSource = dsCartonPackagingDropdown.Tables[0];
                this.cboCartonPackaging.Visible = false;
                this.dgvEdit.Controls.Add(this.cboCartonPackaging);                
                this.cboCartonPackaging.SelectedIndexChanged += new System.EventHandler(cboCartonPackaging_SelectedIndexChanged);

                this.cboOperation = new ComboBox();
                cboOperation.Items.Add("Automatic");
                cboOperation.Items.Add("Manual");
                cboOperation.Visible = false;
                this.dgvEdit.Controls.Add(this.cboOperation);
                this.cboOperation.SelectedIndexChanged += new System.EventHandler(cboOperation_SelectedIndexChanged);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            LoadGrid();
            
        }
        private void LoadGrid()
        {            
            dgvEdit.DataSource = EditDT;
            dgvEdit.Columns[0].ReadOnly = true;
            dgvEdit.Columns[2].Visible = false;  //hidden column name   referenced by combos
            dgvEdit.Columns[3].Visible = false;  //hidden column value  populated by combos
            dgvEdit.AutoResizeColumns();
            int irow = LocateGridRow("Description");
            if (irow > -1)
            {
                dgvEdit.Rows[irow].Cells["Value"].ReadOnly = true;
                if (dgvEdit.Rows[irow].Cells["Value"].Value.ToString().Length == 0)
                {
                    dgvEdit.Columns[1].Width = 200;  // sets default column width for a new record                    
                }                   
            }
            irow = LocateGridRow("Code");
            if (irow > -1)
            {
                if (dgvEdit.Rows[irow].Cells["Value"].Value.ToString().Length == 0)
                {                    
                    dgvEdit.Rows[irow].Cells["Value"].ReadOnly = true;
                }
            }
        }
        private void SetupCols()
        {
            GridColumns thisCol;
            for (int i = 1; i < Convert.ToInt32(dgvEdit.Rows.Count) - 1; i++)
            {
                //dgvEdit.Rows[i].Cells[0].ReadOnly = true;                
                if (dgcols.TryGetValue(dgvEdit.Rows[i].Cells[0].Value.ToString(), out thisCol))
                {
                    if (thisCol.DataType == "bit") //dgvEdit.Rows[i].Cells[1].
                    {
                        var cell = new DataGridViewCheckBoxCell()
                        {
                            TrueValue = "1",
                            FalseValue = "0",
                        };
                        cell.Style.NullValue = false;
                        this.dgvEdit.Rows[i].Cells[1] = cell;
                    }
                    if (thisCol.ReadOnly)
                    {
                        this.dgvEdit.Rows[i].Cells[1].ReadOnly = true;
                    }
                }                
            }
            int rowIndex = LocateGridRow("CompanyCode");
            if (rowIndex > -1)
            {
                this.cboCompanyCode.SelectedValue = dgvEdit.Rows[rowIndex].Cells["Value"].Value.ToString();
                //UpdateProductCodeDropdown();
                
            }
            
        }        
        private void cbo_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            UpdateProductCode();            
        }
        private void UpdateProductCode()
        {
            try
            {
                if (cbo.SelectedIndex > -1)
                {
                    int pmID = (int)cbo.SelectedValue;
                    DataView dv = new DataView(dsMG.Tables[0]);
                    dv.RowFilter = "PmID=" + pmID.ToString();
                    int gridRow;
                    if (dv.Count > 0)
                    {

                        //dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["PmID"].Value = (int)dv[0]["PmID"]; 
                        gridRow = LocateGridRow("Code");
                        if (gridRow > -1)
                        {
                            dgvEdit.Rows[gridRow].Cells["Value"].Value = dv[0]["Code"].ToString().Trim(); //["Code"].Value = dv[0]["Code"].ToString().Trim();
                            dgvEdit.Rows[gridRow].Cells["HiddenValue"].Value = pmID.ToString();
                            dgvEdit.Rows[gridRow].Cells["HiddenName"].Value = "PmID";
                        }

                        gridRow = LocateGridRow("Description");
                        if (gridRow > -1)
                        {
                            dgvEdit.Rows[gridRow].Cells["Value"].Value = dv[0]["Description"].ToString().Trim();
                            dgvEdit.Rows[gridRow].Cells["HiddenValue"].Value = dv[0]["MaterialID"].ToString(); //(int)dv[0]["MaterialID"]; // this is same as Polymer1TypeID - required 
                            dgvEdit.Rows[gridRow].Cells["HiddenName"].Value = "MaterialID";            // for compatibility with labelling system  
                        }

                        gridRow = LocateGridRow("Polymer1Type");
                        if (gridRow > -1)
                        {
                            dgvEdit.Rows[gridRow].Cells["Value"].Value = dv[0]["Material"].ToString(); //(int)dv[0]["MaterialID"];
                            dgvEdit.Rows[gridRow].Cells["HiddenValue"].Value = dv[0]["MaterialID"].ToString(); // (int)dv[0]["Polymer1TypeID"];
                            dgvEdit.Rows[gridRow].Cells["HiddenName"].Value = "Polymer1TypeID";
                        }

                        gridRow = LocateGridRow("ProductCategory");
                        if (gridRow > -1)
                        {
                            dgvEdit.Rows[gridRow].Cells["Value"].Value = dv[0]["ProductCategory"].ToString().Trim();
                            dgvEdit.Rows[gridRow].Cells["HiddenValue"].Value = dv[0]["GradeID"].ToString(); // (int)dv[0]["GradeID"];
                            dgvEdit.Rows[gridRow].Cells["HiddenName"].Value = "GradeID";
                        }
                        gridRow = LocateGridRow("CompanyCode");
                        if (gridRow > -1) dgvEdit.Rows[gridRow].Cells["Value"].Value = dv[0]["CompanyCode"].ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private Int32 LocateGridRow(string findName)
        {
            int iresult = -1;                
            for (int i = 0; i < Convert.ToInt32(dgvEdit.Rows.Count); i++)
            {
                if (dgvEdit.Rows[i].Cells[0].Value.ToString().Trim().Length > 0)
                {
                    if (dgvEdit.Rows[i].Cells[0].Value.ToString() == findName)
                    {
                        iresult = i;
                        break;
                    }
                }
            }
            return iresult;                      
        }
        private void cboMachine_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (cboMachine.SelectedIndex > -1)
                {
                    int mID = (int)cboMachine.SelectedValue;
                    //if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "MachineA")
                    if (dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Column"].Value.ToString() == "MachineA")
                    {
                        dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["HiddenValue"].Value = mID.ToString();
                        dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["HiddenName"].Value = "MachineAID";
                        dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Value"].Value = cboMachine.Text;
                    }
                    //else if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "MachineB")
                    else if (dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Column"].Value.ToString() == "MachineB")
                    {
                        dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["HiddenValue"].Value = mID.ToString();
                        dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["HiddenName"].Value = "MachineBID";
                        dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Value"].Value = cboMachine.Text;
                    }
                    //else if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "MachineC")
                    else if (dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Column"].Value.ToString() == "MachineC")
                    {
                        dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["HiddenValue"].Value = mID.ToString();
                        dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["HiddenName"].Value = "MachineCID";
                        dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Value"].Value = cboMachine.Text;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }                           
        }
        private void cboProductGrade_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cboProductGrade.SelectedIndex > -1)
            {
                int pgID = (int)cboProductGrade.SelectedValue;
                dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["HiddenValue"].Value = pgID.ToString(); //["GradeID"].Value = pgID;
                dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["HiddenName"].Value = "GradeID";
                dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Value"].Value = cboProductGrade.Text; //["ProductCategory"].Value = cboProductGrade.Text;
            }
        }
        private void cboCustomer_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (!bIsLoading)
            {
                int custid = (int)cboCustomer.SelectedValue;
                dgvEdit.CurrentRow.Cells["Value"].Value = cboCustomer.Text;
                dgvEdit.CurrentRow.Cells["HiddenName"].Value = "CustID";
                dgvEdit.CurrentRow.Cells["HiddenValue"].Value = custid.ToString();
            }
        }
        private void cboCompanyCode_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (cboCompanyCode.SelectedIndex > -1 && !bIsLoading) 
                {
                    UpdateProductCodeDropdown();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void UpdateProductCodeDropdown()
        {
            if (cboCompanyCode.SelectedValue != null && !bIsLoading)
            {
                string compCode = cboCompanyCode.SelectedValue.ToString().Trim();
                dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Value"].Value = cboCompanyCode.Text;
                DataSet ds = dsMG.Copy();
                DataView dv = new DataView(ds.Tables[0]);
                dv.RowFilter = "CompanyCode='" + compCode + "'";
                this.cbo = new ComboBox();
                this.cbo.ValueMember = "PmID";
                this.cbo.DisplayMember = "Code";
                this.cbo.DataSource = dv;
                this.cbo.Visible = false;
                this.dgvEdit.Controls.Add(this.cbo);
                //Associate the event-handling method with the 
                // SelectedIndexChanged event.
                this.cbo.SelectedIndexChanged += new System.EventHandler(cbo_SelectedIndexChanged);
                int irow = -1;
                irow = LocateGridRow("Description");
                if (irow > -1)
                {
                    dgvEdit.Rows[irow].Cells["Value"].Value = DBNull.Value;
                }
                irow = LocateGridRow("Code");
                if (irow > -1)
                {
                    dgvEdit.Rows[irow].Cells["Value"].Value = DBNull.Value;
                    dgvEdit.Rows[irow].Cells["Value"].ReadOnly = false;
                }
            }
            
        }
        private void cboPallet_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cboPallet.SelectedIndex > -1)
            {
                string pallet = cboPallet.SelectedValue.ToString().Trim(); ;
                //dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["PalletType"].Value = pallet;
                dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Value"].Value = pallet;
            }
        }
        private void cboAdditiveCode_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cboAdditiveCode.SelectedIndex > -1)
            {
                int aID = (int)cboAdditiveCode.SelectedValue;
                int irow;
                DataTable table = dsAdditive.Copy().Tables[0];
                DataRow[] row = table.Select("ADDID = " + aID.ToString());
                //if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "AdditiveCode" ||
                //    dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "Additive")
                if (dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Column"].Value.ToString() == "AdditiveCode" ||
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Column"].Value.ToString() == "Additive")
                {
                    irow = LocateGridRow("AdditiveCode");
                    if (irow > -1)
                    {
                        dgvEdit.Rows[irow].Cells["HiddenValue"].Value = aID.ToString(); //"AdditiveACodeID"
                        dgvEdit.Rows[irow].Cells["HiddenName"].Value = "AdditiveACodeID";
                        dgvEdit.Rows[irow].Cells["Value"].Value = row[0]["AdditiveCode"].ToString();   //cboAdditiveCode.Text;

                        irow = LocateGridRow("Additive");
                        if (irow > -1)
                        {
                            dgvEdit.Rows[irow].Cells["Value"].Value = row[0]["Additive"].ToString();
                        }
                    }
                }
                //else if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "AdditiveBCode" ||
                //         dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "AdditiveB")
                else if (dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Column"].Value.ToString() == "AdditiveBCode" ||
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Column"].Value.ToString() == "AdditiveB")
                {
                    irow = LocateGridRow("AdditiveBCode");
                    if (irow > -1)
                    {
                        dgvEdit.Rows[irow].Cells["HiddenValue"].Value = aID.ToString(); //"AdditiveACodeID"
                        dgvEdit.Rows[irow].Cells["HiddenName"].Value = "AdditiveBCodeID";
                        dgvEdit.Rows[irow].Cells["Value"].Value = row[0]["AdditiveCode"].ToString();   //cboAdditiveCode.Text;

                        irow = LocateGridRow("AdditiveB");
                        if (irow > -1)
                        {
                            dgvEdit.Rows[irow].Cells["Value"].Value = row[0]["Additive"].ToString();
                        }
                    }
                }
            }
        }
        private void cboMBColour_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int irow = -1;
            string mbColour;
            string mbCode;
            if (cboMBColour.SelectedIndex > -1)
            {
                int mbid = (int)cboMBColour.SelectedValue;
                DataSet dsMCCopy = dsMC.Copy();
                DataView dv = new DataView(dsMCCopy.Tables[0]);
                dv.RowFilter = "MBID = " + mbid.ToString();
                DataTable tb = dv.ToTable();

                mbColour = tb.Rows[0]["Colour"].ToString().Trim();//(string)cboMBColour.SelectedValue;
                mbCode = tb.Rows[0]["Code"].ToString().Trim();//(string)cboMBColour.SelectedValue;


                //if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "MBColourA" ||
                //    dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "MBCodeA")
                if (dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Column"].Value.ToString() == "MBColourA" ||
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Column"].Value.ToString() == "MBCodeA")
                {
                    irow = LocateGridRow("MBColourA");
                    if (irow > -1)
                    {
                        dgvEdit.Rows[irow].Cells["Value"].Value = mbColour;                        
                        dgvEdit.Rows[irow].Cells["HiddenName"].Value = "MBID";
                        dgvEdit.Rows[irow].Cells["HiddenValue"].Value = mbid.ToString();
                    }
                    
                    irow = LocateGridRow("MBCodeA");
                    if (irow > -1)
                    {
                        dgvEdit.Rows[irow].Cells["Value"].Value = mbCode;
                    }
                }                
                else if (dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Column"].Value.ToString() == "MBColourB" ||
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Column"].Value.ToString() == "MBCodeB")
                {
                    irow = LocateGridRow("MBColourB");
                    if (irow > -1 )
                    {
                        dgvEdit.Rows[irow].Cells["Value"].Value = mbColour;
                        dgvEdit.Rows[irow].Cells["HiddenName"].Value = "MBBID";
                        dgvEdit.Rows[irow].Cells["HiddenValue"].Value = mbid.ToString();
                    }
                    
                    irow = LocateGridRow("MBCodeB");
                    if (irow > -1)
                    {
                        dgvEdit.Rows[irow].Cells["Value"].Value = mbCode;
                    }
                }                         
            }
            else if (cboMBColour.Text.Length == 0)
            {
                //Allow blank value entry
                if (dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Column"].Value.ToString() == "MBColourA" ||
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Column"].Value.ToString() == "MBCodeA")
                {
                    irow = LocateGridRow("MBColourA");
                    if (irow > -1)
                    {
                        dgvEdit.Rows[irow].Cells["Value"].Value = DBNull.Value;
                        dgvEdit.Rows[irow].Cells["HiddenName"].Value = "MBID";
                        dgvEdit.Rows[irow].Cells["HiddenValue"].Value = DBNull.Value;
                    }

                    irow = LocateGridRow("MBCodeA");
                    if (irow > -1)
                    {
                        dgvEdit.Rows[irow].Cells["Value"].Value = DBNull.Value;
                    }
                }
                else if (dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Column"].Value.ToString() == "MBColourB" ||
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Column"].Value.ToString() == "MBCodeB")
                {
                    irow = LocateGridRow("MBColourB");
                    if (irow > -1)
                    {
                        dgvEdit.Rows[irow].Cells["Value"].Value = DBNull.Value;
                        dgvEdit.Rows[irow].Cells["HiddenName"].Value = "MBBID";
                        dgvEdit.Rows[irow].Cells["HiddenValue"].Value = DBNull.Value;
                    }

                    irow = LocateGridRow("MBCodeB");
                    if (irow > -1)
                    {
                        dgvEdit.Rows[irow].Cells["Value"].Value = DBNull.Value;
                    }
                }
            }
        }
        private void cboMaterialGrade_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int irow = -1;
            if (cboMaterialGrade.SelectedIndex > -1)
            {
                int MID = (int)cboMaterialGrade.SelectedValue;
                DataSet dsMAGCopy = dsMAG.Copy();
                DataView dv = new DataView(dsMAGCopy.Tables[0]);
                dv.RowFilter = "MaterialGradeID = " + MID.ToString();
                DataTable tb = dv.ToTable();
                int materialID = (int)tb.Rows[0]["MaterialID"];

                if (dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Column"].Value.ToString() == "Polymer1Grade" ||
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Column"].Value.ToString() == "Polymer1Type")
                {
                    irow = LocateGridRow("Polymer1Grade");
                    {
                        if (irow > -1)
                        {
                            dgvEdit.Rows[irow].Cells["Value"].Value = tb.Rows[0]["MaterialGrade"].ToString().Trim(); //cboMaterialGrade.Text; 
                            dgvEdit.Rows[irow].Cells["HiddenName"].Value = "Polymer1GradeID";
                            dgvEdit.Rows[irow].Cells["HiddenValue"].Value = MID;
                        }
                        irow = LocateGridRow("Polymer1Type");
                        {
                            if (irow > -1)
                            {
                                dgvEdit.Rows[irow].Cells["Value"].Value = tb.Rows[0]["Material"].ToString().Trim();
                                dgvEdit.Rows[irow].Cells["HiddenName"].Value = "Polymer1TypeID";
                                dgvEdit.Rows[irow].Cells["HiddenValue"].Value = materialID;
                            }
                        }                        
                    }
                }
                else if (dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Column"].Value.ToString() == "Polymer2Grade" ||
                         dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Column"].Value.ToString() == "Polymer2Type")
                {
                    irow = LocateGridRow("Polymer2Grade");
                    if (irow > -1)
                    {
                        dgvEdit.Rows[irow].Cells["HiddenValue"].Value = MID.ToString();
                        dgvEdit.Rows[irow].Cells["HiddenName"].Value = "Polymer2GradeID";
                        dgvEdit.Rows[irow].Cells["Value"].Value = tb.Rows[0]["MaterialGrade"].ToString().Trim();  //cboMaterialGrade.Text;
                    }
                    irow = LocateGridRow("Polymer2Type");
                    if (irow > -1)
                    {
                        dgvEdit.Rows[irow].Cells["Value"].Value = tb.Rows[0]["Material"].ToString().Trim();
                        dgvEdit.Rows[irow].Cells["HiddenValue"].Value = materialID.ToString();
                        dgvEdit.Rows[irow].Cells["HiddenName"].Value = "Polymer2TypeID";
                    }
                }
            }
            else if (cboMaterialGrade.Text.Length == 0)
            {
                //Allow blank value entry
                if (dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Column"].Value.ToString() == "Polymer1Grade" ||
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Column"].Value.ToString() == "Polymer1Type")
                {
                    irow = LocateGridRow("Polymer1Grade");
                    {
                        if (irow > -1)
                        {
                            dgvEdit.Rows[irow].Cells["Value"].Value = DBNull.Value;
                            dgvEdit.Rows[irow].Cells["HiddenName"].Value = "Polymer1GradeID";
                            dgvEdit.Rows[irow].Cells["HiddenValue"].Value = DBNull.Value;
                        }
                        irow = LocateGridRow("Polymer1Type");
                        {
                            if (irow > -1)
                            {
                                dgvEdit.Rows[irow].Cells["Value"].Value = DBNull.Value;
                                dgvEdit.Rows[irow].Cells["HiddenName"].Value = "Polymer1TypeID";
                                dgvEdit.Rows[irow].Cells["HiddenValue"].Value = DBNull.Value;
                            }
                        }
                    }
                }
                else if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "Polymer2Grade" ||
                         dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "Polymer2Type")
                {
                    irow = LocateGridRow("Polymer2Grade");
                    if (irow > -1)
                    {
                        dgvEdit.Rows[irow].Cells["HiddenValue"].Value = DBNull.Value;
                        dgvEdit.Rows[irow].Cells["HiddenName"].Value = "Polymer2GradeID";
                        dgvEdit.Rows[irow].Cells["Value"].Value = DBNull.Value;
                    }
                    irow = LocateGridRow("Polymer2Type");
                    if (irow > -1)
                    {
                        dgvEdit.Rows[irow].Cells["Value"].Value = DBNull.Value;
                        dgvEdit.Rows[irow].Cells["HiddenValue"].Value = DBNull.Value;
                        dgvEdit.Rows[irow].Cells["HiddenName"].Value = "Polymer2TypeID";
                    }
                }
                
            }
        }
        private void cboAdditiveCode_TextChanged(object sender, System.EventArgs e)
        {
            //MessageBox.Show(cboMaterialGrade.Text);
            if (cboAdditiveCode.Text.Length == 0)
            {
                cboAdditiveCode.SelectedIndex = -1;
                cboAdditiveCode_SelectedIndexChanged(cboAdditiveCode, EventArgs.Empty);
            }
        }
        private void cboMaterialGrade_TextChanged(object sender, System.EventArgs e)
        {
            //MessageBox.Show(cboMaterialGrade.Text);
            if (cboMaterialGrade.Text.Length == 0)
            {
                cboMaterialGrade.SelectedIndex = -1;
                cboMaterialGrade_SelectedIndexChanged(cboMaterialGrade, EventArgs.Empty);
            }
        }
        private void cboMBColour_TextChanged(object sender, System.EventArgs e)
        {
            //MessageBox.Show(cboMaterialGrade.Text);
            if (cboMBColour.Text.Length == 0)
            {
                cboMBColour.SelectedIndex = -1;
                cboMBColour_SelectedIndexChanged(cboMBColour, EventArgs.Empty);
            }
        }
        private void cboCartonPackaging_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cboCartonPackaging.SelectedIndex > -1)
            {
                int ctnID = (int)cboCartonPackaging.SelectedValue;
                DataTable table = dsCartonPackagingDropdown.Copy().Tables[0];
                DataRow[] row = table.Select("CtnID = " + ctnID.ToString());
                if (dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Column"].Value.ToString() == "CartonType")
                {
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["HiddenValue"].Value = ctnID.ToString();
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["HiddenName"].Value = "CtnID";
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Value"].Value = cboCartonPackaging.Text;
                }
            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            Close();
        }
        private void EditRowDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            int irow;
            if (dgvEdit.IsCurrentRowDirty)
            {
                this.Validate();
            }
            
            if (this.DialogResult != DialogResult.Cancel && this.DialogResult != DialogResult.No)
            {
                //validate edits
                irow = LocateGridRow("CompanyCode");
                if (irow > -1)
                {
                    if (dgvEdit.Rows[irow].Cells["Value"].Value == DBNull.Value)
                    {
                        MessageBox.Show("Company must not be blank.");
                        e.Cancel = true; //(keeps form open)
                        return;
                    }
                }
                irow = LocateGridRow("Code");
                if (irow >-1)
                {
                    if (dgvEdit.Rows[irow].Cells["Value"].Value.ToString().Length == 0)
                    {
                        MessageBox.Show("Product Code must not be blank.");
                        e.Cancel = true; //(keeps form open)
                        return;
                    }
                }
                //e.Cancel = true;  (keeps form open)
            }
            dgvEdit.EndEdit();
            dgvEdit.DataSource = null;
            e.Cancel = false;
            
        }
        private void dgvEdit_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1 && e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                DataGridViewCell c = (sender as DataGridView)[e.ColumnIndex-1, e.RowIndex];
                string cname = c.Value.ToString(); //c.OwningColumn.Name;
                if (cname == "Image" || cname == "PackingImage1" || cname == "PackingImage2" || cname == "PackingImage3" ||
                    cname == "AssemblyImage1" || cname == "AssemblyImage2" || cname == "AssemblyImage3" || cname == "AssemblyImage4" ||
                    cname == "QCImage1" || cname == "QCImage2" || cname == "QCImage3" || cname == "QCImage4" ||
                    cname == "AssemblyImage1" || cname == "AssemblyImage2" || cname == "AssemblyImage3" || cname == "AssemblyImage4" ||
                    cname == "AssemblyImage5" || cname == "AssemblyImage6" || cname == "Field1" || cname == "Field2")
                {
                    if (!c.Selected)
                    {
                        c.DataGridView.ClearSelection();
                        c.DataGridView.CurrentCell = c;
                        c.Selected = true;
                    }
                }
            }
        }
        private void dgvEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.F10 && e.Shift) || e.KeyCode == Keys.Apps)
            {
                e.SuppressKeyPress = true;
                DataGridViewCell currentCell = (sender as DataGridView).CurrentCell;
                if (currentCell != null)
                {
                    ContextMenuStrip cms = currentCell.ContextMenuStrip;
                    if (cms != null)
                    {
                        Rectangle r = currentCell.DataGridView.GetCellDisplayRectangle(currentCell.ColumnIndex, currentCell.RowIndex, false);
                        Point p = new Point(r.X + r.Width, r.Y + r.Height);
                        cms.Show(currentCell.DataGridView, p);
                    }
                }
            }
        }
        private void TransferComboText()
        {
            if (bIsLoading)
                return;
            if (cbo != null)
            {
                if (cbo.Visible)
                {
                    dgvEdit.CurrentCell.Value = cbo.Text;
                    cbo.Visible = false;
                    dgvEdit.EndEdit();
                }
            }
            if (cboMachine.Visible)
            {
                dgvEdit.CurrentCell.Value = cboMachine.Text;
                cboMachine.Visible = false;
                dgvEdit.EndEdit();
            }
            if (cboCustomer.Visible)
            {
                dgvEdit.CurrentCell.Value = cboCustomer.Text;
                cboCustomer.Visible = false;
                dgvEdit.EndEdit();
            }
            //if (cboMaterial.Visible)
            //{
            //    dgvEdit.CurrentCell.Value = cboMaterial.Text;
            //    cboMaterial.Visible = false;
            //    dgvEdit.EndEdit();
            //}
            if (cboMaterialGrade.Visible)
            {
                //dgvEdit.CurrentCell.Value = cboMaterialGrade.Text;
                cboMaterialGrade.Visible = false;
                dgvEdit.EndEdit();
            }
            if (cboPallet.Visible)
            {
                dgvEdit.CurrentCell.Value = cboPallet.Text;
                cboPallet.Visible = false;
                dgvEdit.EndEdit();
            }
            if (cboMBColour.Visible == true)
            {
                //dgvEdit.CurrentCell.Value = cboMBColour.Text;
                cboMBColour.Visible = false;
            }
            //if (cboMBCode.Visible == true)
            //{
            //    dgvEdit.CurrentCell.Value = cboMBCode.Text;
            //    cboMBCode.Visible = false;
            //}
            if (cboAdditiveCode.Visible == true)
            {
                dgvEdit.CurrentCell.Value = cboAdditiveCode.Text;
                cboAdditiveCode.Visible = false;
            }
            //if (cboAdditive.Visible == true)
            //{
            //    dgvEdit.CurrentCell.Value = cboAdditive.Text;
            //    cboAdditive.Visible = false;
            //}
            if (cboCartonPackaging.Visible == true)
            {
                dgvEdit.CurrentCell.Value = cboCartonPackaging.Text;
                cboCartonPackaging.Visible = false;
            }
            else if (cboCompanyCode.Visible == true)
            {
                dgvEdit.CurrentCell.Value = cboCompanyCode.Text;
                cboCompanyCode.Visible = false;
            }
            else if (cboCustomer.Visible == true)
            {
                dgvEdit.CurrentCell.Value = cboCompanyCode.Text;
                cboCustomer.Visible = false;
            }
            else if (cboOperation.Visible == true)
            {
                dgvEdit.CurrentCell.Value = cboOperation.Text;
                cboOperation.Visible = false;
            }
        }
        private void locateFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show("todo:  open file");  
                stripImage.SendToBack();
                string fileName = dgvEdit.CurrentCell.Value.ToString();
                ProcessStartInfo pi = new ProcessStartInfo(fileName);
                pi.Arguments = Path.GetFileName(fileName);
                pi.UseShellExecute = true;
                pi.WorkingDirectory = Path.GetDirectoryName(fileName);
                pi.FileName = fileName;
                pi.Verb = "OPEN";
                Process.Start(pi);
                stripImage.SendToBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void locateFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("todo:  browse for file");
            opf.InitialDirectory = "\\\\plasmo-fp-01\\Data\\CONSOLIDATED PLASTICS\\INJECTION MOULDING\\Database\\Images";
            //\\plasmo-fp-01\Data\CONSOLIDATED PLASTICS\INJECTION MOULDING\Database\Images
            stripImage.SendToBack();
            if (opf.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    curImagePath = opf.FileName;
                    stripImage.Items[2].Enabled = true;
                    ProcessStartInfo pi = new ProcessStartInfo(curImagePath);
                    pi.Arguments = Path.GetFileName(curImagePath);
                    pi.UseShellExecute = true;
                    pi.WorkingDirectory = Path.GetDirectoryName(curImagePath);
                    pi.FileName = curImagePath;
                    pi.Verb = "OPEN";
                    //Process.Start(pi).WaitForExit();
                    Process.Start(pi);
                    stripImage.SendToBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void saveLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // MessageBox.Show("todo:  save/replace link");
                //dgvEdit.CurrentCell.Value = curImagePath;
                dgvEdit.CurrentRow.Cells["Value"].Value = curImagePath;
                stripImage.Items[0].Enabled = true;
                stripImage.Items[2].Enabled = true;
                stripImage.Items[3].Enabled = true;
                stripImage.Items[5].Enabled = true;
                stripImage.Items[6].Enabled = true;
                //dataGridView1.CurrentCell.ContextMenuStrip = strip;            
                //strip.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }           
        }
        private void removeLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("todo:  remove link");
            curImagePath = null;
            dgvEdit.CurrentCell.Value = curImagePath;
            stripImage.Items[0].Enabled = false;
            stripImage.Items[2].Enabled = false;
            stripImage.Items[3].Enabled = false;
            stripImage.Items[5].Enabled = false;
            stripImage.Items[6].Enabled = false;
        }
        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show("todo:  open file");  
                stripImage.SendToBack();
                string fileName = dgvEdit.CurrentCell.Value.ToString();
                ProcessStartInfo pi = new ProcessStartInfo(fileName);
                pi.Arguments = Path.GetFileName(fileName);
                pi.UseShellExecute = true;
                pi.WorkingDirectory = Path.GetDirectoryName(fileName);
                pi.FileName = fileName;
                pi.Verb = "OPEN";
                Process.Start(pi);
                stripImage.SendToBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void copyLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            curImagePath = dgvEdit.CurrentCell.Value.ToString();
            stripImage.Items[5].Enabled = (curImagePath != null);
            stripImage.Items[6].Enabled = (curImagePath != null);
            stripImage.Close();
        }
        private void pasteLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //curImagePath = dataGridView1.CurrentCell.Value.ToString();
            dgvEdit.CurrentCell.Value = curImagePath;
            stripImage.Items[0].Enabled = true;
            stripImage.Items[2].Enabled = true;
            stripImage.Items[3].Enabled = true;
            stripImage.Items[5].Enabled = true;
            stripImage.Items[6].Enabled = true;
            stripImage.Close();
        }
        private void openFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            stripImage.Close();
        }
        private void dgvEdit_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                GridColumns thisCol;
                //if (dgcols.TryGetValue(dgvEdit.Columns[e.ColumnIndex].Name, out thisCol))
                if (e.ColumnIndex != -1 && e.RowIndex != -1)
                {
                    DataGridViewCell c = (sender as DataGridView)[e.ColumnIndex - 1, e.RowIndex];
                    string cname = c.Value.ToString();
                    if (dgcols.TryGetValue(cname, out thisCol))
                    {
                        if (thisCol.ColumnName == "Code" && cbo != null) //currentTab == "Product" && cbo != null)
                        {
                            cbo.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                            cbo.Width = dgvEdit.Columns[1].Width;
                            cbo.Visible = true;
                        }
                        else if (thisCol.ColumnName == "MachineA") // currentTab == "Product")
                        {
                            cboMachine.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                            cboMachine.Width = dgvEdit.Columns[1].Width;
                            cboMachine.Visible = true;
                        }
                        else if (thisCol.ColumnName == "MachineB") // currentTab == "Product")
                        {
                            cboMachine.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                            cboMachine.Width = dgvEdit.Columns[1].Width;
                            cboMachine.Visible = true;
                        }
                        else if (thisCol.ColumnName == "MachineC") // currentTab == "Product")
                        {
                            cboMachine.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                            cboMachine.Width = dgvEdit.Columns[1].Width;
                            cboMachine.Visible = true;
                        }
                        else if (thisCol.ColumnName == "Customer") // currentTab == "Product")
                        {
                            cboCustomer.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                            cboCustomer.Width = dgvEdit.Columns[1].Width;
                            cboCustomer.Visible = true;
                        }
                        else if (thisCol.ColumnName == "ProductCategory") // currentTab == "Product")
                        {
                            cboProductGrade.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                            cboProductGrade.Width = dgvEdit.Columns[1].Width;
                            cboProductGrade.Text = this.dgvEdit.CurrentCell.Value.ToString();
                            cboProductGrade.Visible = true;
                        }
                        else if (thisCol.ColumnName == "CompanyCode") // currentTab == "Product")
                        {
                            cboCompanyCode.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                            cboCompanyCode.Width = dgvEdit.Columns[1].Width;
                            cboCompanyCode.Text = this.dgvEdit.CurrentCell.Value.ToString();
                            cboCompanyCode.Visible = true;
                        }
                        else if (thisCol.ColumnName == "Polymer1Type") // currentTab == "Material")
                        {
                            cboMaterialGrade.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                            cboMaterialGrade.Width = dgvEdit.Columns[1].Width;
                            cboMaterialGrade.Text = this.dgvEdit.CurrentCell.Value.ToString();
                            cboMaterialGrade.Visible = true;
                        }
                        else if (thisCol.ColumnName == "Polymer2Type") // currentTab == "Material")
                        {
                            cboMaterialGrade.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                            cboMaterialGrade.Width = dgvEdit.Columns[1].Width;
                            cboMaterialGrade.Text = this.dgvEdit.CurrentCell.Value.ToString();
                            cboMaterialGrade.Visible = true;
                        }
                        else if (thisCol.ColumnName == "Polymer1Grade") // currentTab == "Material")
                        {
                            cboMaterialGrade.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                            cboMaterialGrade.Width = dgvEdit.Columns[1].Width;
                            cboMaterialGrade.Text = this.dgvEdit.CurrentCell.Value.ToString();
                            cboMaterialGrade.Visible = true;
                        }
                        else if (thisCol.ColumnName == "Polymer2Grade") // currentTab == "Material")
                        {
                            cboMaterialGrade.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                            cboMaterialGrade.Width = dgvEdit.Columns[1].Width;
                            cboMaterialGrade.Text = this.dgvEdit.CurrentCell.Value.ToString();
                            cboMaterialGrade.Visible = true;
                        }
                        else if (thisCol.ColumnName == "AdditiveCode" ||    // currentTab == "Masterbatch" ||
                                 thisCol.ColumnName == "Additive" ||        // currentTab == "Masterbatch" ||
                                 thisCol.ColumnName == "AdditiveBCode" ||   // currentTab == "Masterbatch" ||
                                 thisCol.ColumnName == "AdditiveB")        // currentTab == "Masterbatch")
                        {
                            cboAdditiveCode.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                            cboAdditiveCode.Width = dgvEdit.Columns[1].Width;
                            cboAdditiveCode.Text = this.dgvEdit.CurrentCell.Value.ToString();
                            cboAdditiveCode.Visible = true;
                        }
                        else if (thisCol.ColumnName == "MBColourA" || // currentTab == "Masterbatch" ||
                                 thisCol.ColumnName == "MBCodeA") // currentTab == "Masterbatch")
                        {
                            cboMBColour.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                            cboMBColour.Width = dgvEdit.Columns[1].Width;
                            cboMBColour.Text = this.dgvEdit.CurrentCell.Value.ToString();
                            cboMBColour.Visible = true;
                        }
                        else if (thisCol.ColumnName == "MBColourB" || // currentTab == "Masterbatch" ||
                                 thisCol.ColumnName == "MBCodeB") // currentTab == "Masterbatch")
                        {
                            cboMBColour.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                            cboMBColour.Width = dgvEdit.Columns[1].Width;
                            cboMBColour.Text = this.dgvEdit.CurrentCell.Value.ToString();
                            cboMBColour.Visible = true;
                        }
                        else if (thisCol.ColumnName == "PalletType") // currentTab == "Pallets")
                        {
                            cboPallet.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                            cboPallet.Width = dgvEdit.Columns[1].Width;
                            cboPallet.Text = this.dgvEdit.CurrentCell.Value.ToString();
                            cboPallet.Visible = true;
                        }
                        else if (thisCol.ColumnName == "CartonType") // currentTab == "Packaging")
                        {
                            cboCartonPackaging.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                            cboCartonPackaging.Width = dgvEdit.Columns[1].Width;
                            cboCartonPackaging.Text = this.dgvEdit.CurrentCell.Value.ToString();
                            cboCartonPackaging.Visible = true;
                        }
                        else if (thisCol.ColumnName == "Operation") // && currentTab == "Mould")
                        {
                            cboOperation.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                            cboOperation.Width = dgvEdit.Columns[1].Width;
                            cboOperation.Text = this.dgvEdit.CurrentCell.Value.ToString();
                            cboOperation.Visible = true;
                        }
                        else if (thisCol.DisplayLines > 1 && !thisCol.ReadOnly)
                        {
                            this.tbx.Text = this.dgvEdit.CurrentCell.Value.ToString();
                            int x = this.dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location.X - 3;
                            int y = this.dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location.Y - 3;
                            this.tbx.Location = new Point(x, y);
                            int width = this.dgvEdit.CurrentCell.Size.Width + 6;
                            int height = this.dgvEdit.CurrentCell.Size.Height * thisCol.DisplayLines + 6;
                            this.tbx.Size = new Size(width, height);
                            this.tbx.Visible = true;
                            this.dgvEdit.BeginInvoke(new MethodInvoker(MyMethod));
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }                                
        }
        private void MyMethod()
        {
            this.tbx.Focus();
            this.tbx.SelectAll();
        }
        private void dgvEdit_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (tbx.Visible == true)
            {
                this.dgvEdit.CurrentCell.Value = this.tbx.Text;
                this.tbx.Visible = false;
            }
            else if (cbo != null && cbo.Visible == true)
            {
                dgvEdit.CurrentCell.Value = cbo.Text;
                cbo.Visible = false;
            }
            else if (cboMachine.Visible == true)
            {
                dgvEdit.CurrentCell.Value = cboMachine.Text;
                cboMachine.Visible = false;
            }
            else if (cboCustomer.Visible == true)
            {
                dgvEdit.CurrentCell.Value = cboCustomer.Text;
                cboCustomer.Visible = false;
            }           
            else if (cboProductGrade.Visible == true)
            {
                dgvEdit.CurrentCell.Value = cboProductGrade.Text;
                cboProductGrade.Visible = false;
            }
            else if (cboMaterialGrade.Visible == true)
            {
                //dgvEdit.CurrentCell.Value = cboMaterialGrade.Text;
                cboMaterialGrade.Visible = false;
            }
            else if (cboAdditiveCode.Visible == true)
            {
                dgvEdit.CurrentCell.Value = cboAdditiveCode.Text;
                cboAdditiveCode.Visible = false;
            }
            else if (cboPallet.Visible == true)
            {
                dgvEdit.CurrentCell.Value = cboPallet.Text;
                cboPallet.Visible = false;
            }
            else if (cboMBColour.Visible == true)
            {
                //dgvEdit.CurrentCell.Value = cboMBColour.Text;
                cboMBColour.Visible = false;
            }            
            else if (cboCompanyCode.Visible == true)
            {
                dgvEdit.CurrentCell.Value = cboCompanyCode.Text;
                cboCompanyCode.Visible = false;
            }
            else if (cboOperation.Visible == true)
            {
                dgvEdit.CurrentCell.Value = cboOperation.Text;
                cboOperation.Visible = false;
            }
        }
        private void dgvEdit_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;
            //string cname = dgvEdit.Columns[e.ColumnIndex].Name;
            DataGridViewCell c = (sender as DataGridView)[e.ColumnIndex - 1, e.RowIndex];
            string cname = c.Value.ToString();
            if (cname == "Image" || cname == "PackingImage1" || cname == "PackingImage2" || cname == "PackingImage3" ||
                cname == "AssemblyImage1" || cname == "AssemblyImage2" || cname == "AssemblyImage3" || cname == "AssemblyImage4" ||
                cname == "QCImage1" || cname == "QCImage2" || cname == "QCImage3" || cname == "QCImage4" ||
                cname == "AssemblyImage5" || cname == "AssemblyImage6" || cname == "Field1" || cname == "Field2")
            {
                e.ContextMenuStrip = stripImage;
                stripImage.Items[0].Enabled = (dgvEdit.Rows[e.RowIndex].Cells["Value"].Value.ToString().Length > 0);
                stripImage.Items[2].Enabled = (dgvEdit.Rows[e.RowIndex].Cells["Value"].Value.ToString().Length > 0);
                stripImage.Items[3].Enabled = (dgvEdit.Rows[e.RowIndex].Cells["Value"].Value.ToString().Length > 0);
                stripImage.Items[5].Enabled = (stripImage.Items[2].Enabled || curImagePath != null);
                stripImage.Items[6].Enabled = (stripImage.Items[2].Enabled || curImagePath != null);
                stripImage.AutoClose = false;
            }
        }
        private void dgvEdit_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (!bClosing)
            {
                for (int i = 0; i < dgvEdit.Columns.Count; i++)
                {
                    if (dgvEdit.Columns[i].Visible) this.dgvEdit.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;
                }
            }
        }
        private void dgvEdit_Sorted(object sender, EventArgs e)
        {
            SetupCols();
        }
        private void dgvEdit_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            TransferComboText();
        }
        private void dgvEdit_Scroll(object sender, ScrollEventArgs e)
        {
            TransferComboText();
        }
        private void dgvEdit_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
        private void ec_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
            if (nonNumberEntered == true)
            {
                //MessageBox.Show("Please enter number only..."); 
                e.Handled = true;
            }
        }
        private void ec_KeyDown(object sender, KeyEventArgs e)
        {
            // Initialize the flag to false.
            nonNumberEntered = false;

            // Determine whether the keystroke is a number from the top of the keyboard, minus or a decimal.
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9 && e.KeyCode != Keys.OemMinus && e.KeyCode != Keys.OemPeriod)
            {
                // Determine whether the keystroke is a number from the keypad.
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    // Determine whether the keystroke is a backspace.
                    if (e.KeyCode != Keys.Back)
                    {
                        // A non-numerical keystroke was pressed.
                        // Set the flag to true and evaluate in KeyPress event.
                        nonNumberEntered = true;
                    }
                }
            }
        }
        private void dgvEdit_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            GridColumns thisCol;
            //if (dgcols.TryGetValue(dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name, out thisCol))
            DataGridViewCell c = dgvEdit.CurrentRow.Cells[0];
            string cname = c.Value.ToString();
            if (dgcols.TryGetValue(cname, out thisCol))
            {
                if (thisCol.DataType == "real" || thisCol.DataType == "int")
                {
                    // setup editing for numerical input
                    DataGridViewTextBoxEditingControl ec = (DataGridViewTextBoxEditingControl)e.Control;
                    ec.KeyPress -= new KeyPressEventHandler(ec_KeyPress);
                    ec.KeyPress += new KeyPressEventHandler(ec_KeyPress);
                    ec.KeyDown -= ec_KeyDown;
                    ec.KeyDown += ec_KeyDown;
                }
            }
        }
        private void cboOperation_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cboOperation.SelectedIndex > -1)
            {
                dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Value"].Value = cboOperation.Text;
            }
        }

        private void closeMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stripImage.Close();
        }
    }    
}
