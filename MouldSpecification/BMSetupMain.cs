using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataService;
using GridSettings;
using IMSpecification;
//using DGVColumnSelector;

namespace MouldSpecification
{  
    public partial class BMSetupMain : Form
    {
        dgSettingsDictionary dgcols;
        DataSet MaterialAndGrade;
        DataSet BMSetupDS;
        //DataSet MBColour;
        //DataSet MBCode;
        TextBox tbx;
       
        ComboBox cbo;
        ComboBox cboMachine;
        ComboBox cboCustomer;
        ComboBox cboMaterial;
        ComboBox cboProductGrade;
        ComboBox cboPallet;
        ComboBox cboMBColour;
        ComboBox cboMBCode = new ComboBox();
        ComboBox cboMaterialGrade = new ComboBox();
        ComboBox cboAdditive;

        bool bIsLoading;
        bool bClosing = false;
        bool nonNumberEntered = false;
        string currentTab;

        public static string nextForm = "";        
        public BMSetupMain()
        {
            InitializeComponent();
            Program.NextForm = "";
        }        
        private void BMSetupMain_Load(object sender, EventArgs e)
        {
            try
            {
                bIsLoading = true;
                dgcols = new dgSettingsDictionary("BM");
                this.blowMouldToolStripMenuItem.Enabled = false;
                this.tbx = new TextBox();
                this.tbx.Multiline = true;
                this.tbx.Visible = false;
                this.dgView.Controls.Add(this.tbx);

                //this.ec = new TextBox();
                //ec.KeyPress += ec_KeyPress; //add this
                //this.dgView.Controls.Add(this.ec);

                cboMaterialGrade.Visible = false;


                MaterialAndGrade = new ProductDataService().GetMaterialAndGrade();
                cboSearchCode.ValueMember = "PmID";
                cboSearchCode.DisplayMember = "Code";
                cboSearchCode.DataSource = MaterialAndGrade.Tables[0];                
                cboSearchCode.ResetText();
                cboSearchCode.SelectedIndexChanged += new System.EventHandler(cboSearchCode_SelectedIndexChanged);

                /*
                 comboBoxsubject.ValueMember = dt.Columns[0].ToString();
                 comboBoxsubject.DisplayMember = dt.Columns[1].ToString();
                 comboBoxsubject.DataSource = dt; 
                 */

                DataSet ds = MaterialAndGrade.Copy();
                this.cbo = new ComboBox();
                this.cbo.ValueMember = "PmID";
                this.cbo.DisplayMember = "Code";                
                this.cbo.DataSource = ds.Tables[0];
                this.cbo.Visible = false;
                this.dgView.Controls.Add(this.cbo);
                //Associate the event-handling method with the 
                // SelectedIndexChanged event.
                this.cbo.SelectedIndexChanged += new System.EventHandler(cbo_SelectedIndexChanged);

                DataSet dsPg = new ProductDataService().GetProductGrade();
                this.cboProductGrade = new ComboBox();
                this.cboProductGrade.ValueMember = "GradeID";
                this.cboProductGrade.DisplayMember = "Description";
                this.cboProductGrade.DataSource = dsPg.Tables[0];
                this.cboProductGrade.Visible = false;
                this.dgView.Controls.Add(this.cboProductGrade);
                //Associate the event-handling method with the 
                // SelectedIndexChanged event.
                this.cboProductGrade.SelectedIndexChanged += new System.EventHandler(cboProductGrade_SelectedIndexChanged);

                DataSet dsM = new DataService.ProductDataService().GetMachine("BM");
                this.cboMachine = new ComboBox();
                this.cboMachine.ValueMember = "MachineID";
                this.cboMachine.DisplayMember = "Machine";
                this.cboMachine.DataSource = dsM.Tables[0];                                
                this.cboMachine.Visible = false;
                this.dgView.Controls.Add(this.cboMachine);
                //Associate the event-handling method with the 
                // SelectedIndexChanged event.
                this.cboMachine.SelectedIndexChanged += new System.EventHandler(cboMachine_SelectedIndexChanged);

                DataSet dsC = new DataService.ProductDataService().GetGPCustomerIndex();
                this.cboCustomer = new ComboBox();
                this.cboCustomer.Width = 200;
                this.cboCustomer.ValueMember = "CUSTNMBR";
                this.cboCustomer.DisplayMember = "ShipToName";
                this.cboCustomer.DataSource = dsC.Tables[0];                                
                this.cboCustomer.Visible = false;
                this.dgView.Controls.Add(this.cboCustomer);
                //Associate the event-handling method with the 
                // SelectedIndexChanged event.
                this.cboCustomer.SelectedIndexChanged += new System.EventHandler(cboCustomer_SelectedIndexChanged);

                DataSet dsMt = new DataService.ProductDataService().GetMaterial();
                this.cboMaterial = new ComboBox();
                //this.cboMaterial.Width = 200;
                this.cboMaterial.ValueMember = "MaterialID";
                this.cboMaterial.DisplayMember = "ShortDesc";
                this.cboMaterial.DataSource = dsMt.Tables[0];                                
                this.cboMaterial.Visible = false;
                this.dgView.Controls.Add(this.cboMaterial);
                //Associate the event-handling method with the 
                // SelectedIndexChanged event.
                //this.cboMaterial.SelectedIndexChanged += new System.EventHandler(cboMaterial_SelectedIndexChanged);
                
                DataSet dsMC = new DataService.ProductDataService().GetMBColour();
                this.cboMBColour = new ComboBox();
                //this.cboMaterial.Width = 200;
                this.cboMBColour.DataSource = dsMC.Tables[0];
                this.cboMBColour.DisplayMember = "MBColour";
                this.cboMBColour.ValueMember = "MBColour";
                this.cboMBColour.Visible = false;
                this.dgView.Controls.Add(this.cboMBColour);
                //Associate the event-handling method with the 
                // SelectedIndexChanged event.
                this.cboMBColour.SelectedIndexChanged += new System.EventHandler(cboMBColour_SelectedIndexChanged);

                DataSet dsP = new DataService.ProductDataService().GetPallet();
                this.cboPallet = new ComboBox();
                //this.cboMaterial.Width = 200;
                this.cboPallet.DataSource = dsP.Tables[0];
                this.cboPallet.DisplayMember = "Pallet";
                this.cboPallet.ValueMember = "Pallet";
                this.cboPallet.Visible = false;
                this.dgView.Controls.Add(this.cboPallet);
                //Associate the event-handling method with the 
                // SelectedIndexChanged event.
                this.cboPallet.SelectedIndexChanged += new System.EventHandler(cboPallet_SelectedIndexChanged);

                DataSet dsA = new DataService.ProductDataService().GetAdditive();
                this.cboAdditive = new ComboBox();
                //this.cboMaterial.Width = 200;
                this.cboAdditive.ValueMember = "ADDID";
                this.cboAdditive.DisplayMember = "AdditiveCode";
                this.cboAdditive.DataSource = dsA.Tables[0];                                
                this.cboAdditive.Visible = false;
                this.dgView.Controls.Add(this.cboAdditive);
                //Associate the event-handling method with the 
                // SelectedIndexChanged event.
                this.cboAdditive.SelectedIndexChanged += new System.EventHandler(cboAdditive_SelectedIndexChanged);
                cboMBCode.Visible = false;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            LoadGrid();
            bIsLoading = false;
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
        private void cbo_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cbo.SelectedIndex > -1)
            {
                int pmID = (int)cbo.SelectedValue;
                DataView dv = new DataView(MaterialAndGrade.Tables[0]);
                dv.RowFilter = "PmID=" + pmID.ToString();
                if (dv.Count > 0)
                {
                    dgView.Rows[dgView.CurrentRow.Index].Cells["PmID"].Value = (int)dv[0]["PmID"];
                    dgView.Rows[dgView.CurrentRow.Index].Cells["MaterialID"].Value = DBNull.Value;
                    if (dv[0]["MaterialID"] != null && dv[0]["MaterialID"] != DBNull.Value)
                    {
                        dgView.Rows[dgView.CurrentRow.Index].Cells["MaterialID"].Value = (int)dv[0]["MaterialID"];
                    }
                    dgView.Rows[dgView.CurrentRow.Index].Cells["Code"].Value = dv[0]["Code"].ToString().Trim();
                    dgView.Rows[dgView.CurrentRow.Index].Cells["Description"].Value = dv[0]["Description"].ToString().Trim();
                    dgView.Rows[dgView.CurrentRow.Index].Cells["MaterialType"].Value = dv[0]["Material"].ToString().Trim();
                    dgView.Rows[dgView.CurrentRow.Index].Cells["GradeID"].Value = (int)dv[0]["GradeID"];
                    dgView.Rows[dgView.CurrentRow.Index].Cells["ProductCategory"].Value = dv[0]["ProductCategory"].ToString().Trim();
                    dgView.Rows[dgView.CurrentRow.Index].Cells["CompanyCode"].Value = dv[0]["CompanyCode"].ToString().Trim();

                }
            }
        }
        private void cboMachine_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cboMachine.SelectedIndex > -1)
            {
                int mID = (int)cboMachine.SelectedValue;
                dgView.Rows[dgView.CurrentRow.Index].Cells["MachineID"].Value = mID;
                dgView.Rows[dgView.CurrentRow.Index].Cells["Machine"].Value = cboMachine.Text;
            }
        }
        private void cboProductGrade_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cboProductGrade.SelectedIndex > -1)
            {
                int pgID = (int)cboProductGrade.SelectedValue;
                dgView.Rows[dgView.CurrentRow.Index].Cells["GradeID"].Value = pgID;
                dgView.Rows[dgView.CurrentRow.Index].Cells["ProductCategory"].Value = cboProductGrade.Text;
            }
        }
        private void cboCustomer_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cboCustomer.SelectedIndex > -1)
            {
                string CustNmbr = cboCustomer.SelectedValue.ToString().Trim(); ;
                dgView.Rows[dgView.CurrentRow.Index].Cells["CUSTNMBR"].Value = CustNmbr;
                dgView.Rows[dgView.CurrentRow.Index].Cells["CUSTNAME"].Value = cboCustomer.Text;
            }
        }
        private void cboPallet_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cboPallet.SelectedIndex > -1)
            {
                string pallet = cboPallet.SelectedValue.ToString().Trim(); ;
                dgView.Rows[dgView.CurrentRow.Index].Cells["PalletType"].Value = pallet;
            }
        }
        //private void cboMaterial_SelectedIndexChanged(object sender, System.EventArgs e)
        //{
        //    if (cboMaterial.SelectedIndex > -1)
        //    {
        //        int materialID = (int)cboMaterial.SelectedValue;                
        //        DataSet dsMg = new DataService.ProductDataService().GetMaterialGrade(materialID);
        //        this.cboMaterialGrade = new ComboBox();
        //        //this.cboMaterial.Width = 200;
        //        /*
        //         comboBoxsubject.ValueMember = dt.Columns[0].ToString();
        //         comboBoxsubject.DisplayMember = dt.Columns[1].ToString();
        //         comboBoxsubject.DataSource = dt; 
        //         */
        //        this.cboMaterialGrade.ValueMember = "MaterialGradeID";
        //        this.cboMaterialGrade.DisplayMember = "MaterialGradeSTANDARD";
        //        this.cboMaterialGrade.DataSource = dsMg.Tables[0];
        //        this.cboMaterialGrade.Visible = false;
        //        this.dgView.Controls.Add(this.cboMaterialGrade);
        //        //Associate the event-handling method with the 
        //        // SelectedIndexChanged event.
        //        this.cboMaterialGrade.SelectedIndexChanged += new System.EventHandler(cboMaterialGrade_SelectedIndexChanged);

        //        if (dgView.Columns[dgView.CurrentCell.ColumnIndex].Name == "MaterialTypeA")
        //        {
        //            //dgView.Rows[dgView.CurrentRow.Index].Cells["MaterialIDA"].Value = MID;
        //            dgView.Rows[dgView.CurrentRow.Index].Cells["MaterialTypeA"].Value = cboMaterial.Text;
        //            dgView.Rows[dgView.CurrentRow.Index].Cells["MaterialType"].Value = cboMaterial.Text;
        //            dgView.Rows[dgView.CurrentRow.Index].Cells["MaterialID"].Value = materialID;
        //        }
        //        else if (dgView.Columns[dgView.CurrentCell.ColumnIndex].Name == "MaterialTypeB")
        //        {
        //            //dgView.Rows[dgView.CurrentRow.Index].Cells["MaterialIDB"].Value = MID;
        //            dgView.Rows[dgView.CurrentRow.Index].Cells["MaterialTypeB"].Value = cboMaterial.Text;
        //        }
        //        else if (dgView.Columns[dgView.CurrentCell.ColumnIndex].Name == "MaterialType")
        //        {
        //            //dgView.Rows[dgView.CurrentRow.Index].Cells["MaterialIDB"].Value = MID;
        //            dgView.Rows[dgView.CurrentRow.Index].Cells["MaterialTypeA"].Value = cboMaterial.Text;
        //            dgView.Rows[dgView.CurrentRow.Index].Cells["MaterialType"].Value = cboMaterial.Text;
        //            dgView.Rows[dgView.CurrentRow.Index].Cells["MaterialID"].Value = materialID;
        //        }

        //    }
        //}
        private void cboAdditive_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cboAdditive.SelectedIndex > -1)
            {
                int aID = (int)cboAdditive.SelectedValue;
                if (dgView.Columns[dgView.CurrentCell.ColumnIndex].Name == "AdditiveCode")
                {
                    dgView.Rows[dgView.CurrentRow.Index].Cells["AdditiveID"].Value = aID;
                    dgView.Rows[dgView.CurrentRow.Index].Cells["AdditiveCode"].Value = cboAdditive.Text;
                }
                else if (dgView.Columns[dgView.CurrentCell.ColumnIndex].Name == "AdditiveB")
                {
                    dgView.Rows[dgView.CurrentRow.Index].Cells["AdditiveBID"].Value = aID;
                    dgView.Rows[dgView.CurrentRow.Index].Cells["AdditiveB"].Value = cboAdditive.Text;
                }
            }
        }
        //cboMBColour_SelectedIndexChanged
        private void cboMBColour_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cboMBColour.SelectedIndex > -1)
            {
                string mbColour = (string)cboMBColour.SelectedValue;
                DataSet dsMcode = new ProductDataService().GetMBCode(mbColour);

                this.cboMBCode = new ComboBox();
                this.cboMBCode.ValueMember = "MBID";
                this.cboMBCode.DisplayMember = "MBCode";
                this.cboMBCode.DataSource = dsMcode.Tables[0];
                /*
                 comboBoxsubject.ValueMember = dt.Columns[0].ToString();
                 comboBoxsubject.DisplayMember = dt.Columns[1].ToString();
                 comboBoxsubject.DataSource = dt; 
                 */
                this.cboMBCode.Visible = false;
                this.cboMBCode.ResetText();
                this.dgView.Controls.Add(this.cboMBCode);
                //Associate the event-handling method with the 
                // SelectedIndexChanged event.
                this.cboMBCode.SelectedIndexChanged += new System.EventHandler(cboMBCode_SelectedIndexChanged);

                if (dgView.Columns[dgView.CurrentCell.ColumnIndex].Name == "MBColourA")
                {
                    dgView.Rows[dgView.CurrentRow.Index].Cells["MBColourA"].Value = mbColour;
                }
                else if (dgView.Columns[dgView.CurrentCell.ColumnIndex].Name == "MBColourB")
                {
                    dgView.Rows[dgView.CurrentRow.Index].Cells["MBColourB"].Value = mbColour;
                }
            }
        }
        private void cboMaterialGrade_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int MID = (int)cboMaterialGrade.SelectedValue;
            if (dgView.Columns[dgView.CurrentCell.ColumnIndex].Name == "MaterialGradeA")
            {
                dgView.Rows[dgView.CurrentRow.Index].Cells["MaterialIDA"].Value = MID;
                dgView.Rows[dgView.CurrentRow.Index].Cells["MaterialGradeA"].Value = cboMaterialGrade.Text;
                //dgView.Rows[dgView.CurrentRow.Index].Cells["MaterialA"].Value = cboMaterial.SelectedItem.ToString().Trim();
                //dgView.Rows[dgView.CurrentRow.Index].Cells["Material"].Value = cboMaterial.SelectedItem.ToString().Trim();
            }
            else if (dgView.Columns[dgView.CurrentCell.ColumnIndex].Name == "MaterialGradeB")
            {
                dgView.Rows[dgView.CurrentRow.Index].Cells["MaterialIDB"].Value = MID;
                dgView.Rows[dgView.CurrentRow.Index].Cells["MaterialGradeB"].Value = cboMaterialGrade.Text;
            }
        }
        private void cboMBCode_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int mbID = (int)cboMBCode.SelectedValue;
            string mbCode = cboMBCode.Text;
            if (dgView.Columns[dgView.CurrentCell.ColumnIndex].Name == "MBCodeA")
            {
                dgView.Rows[dgView.CurrentRow.Index].Cells["MBCodeA"].Value = mbCode;
                dgView.Rows[dgView.CurrentRow.Index].Cells["MasterBatchIDA"].Value = mbID;
            }
            else if (dgView.Columns[dgView.CurrentCell.ColumnIndex].Name == "MBCodeB")
            {
                dgView.Rows[dgView.CurrentRow.Index].Cells["MBCodeB"].Value = mbCode;
                dgView.Rows[dgView.CurrentRow.Index].Cells["MasterBatchIDB"].Value = mbID;
            }
        }
        private void LoadGrid()
        {
            ProductDataService pds = new ProductDataService();
            BMSetupDS = pds.SelectBMSetup();
            dgView.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgView.AutoGenerateColumns = true;
            dgView.DataSource = BMSetupDS.Tables[0];
        }
        private void SetupCols()
        {
            currentTab = tabControl1.SelectedTab.Name;
            GridColumns thisCol;
            //int istVisibleCol = 9;
            for (int i = 0; i < 12; i++)
            {
                dgView.Columns[i].Visible = false;
            }
            for (int i = 12; i < dgView.Columns.Count; i++)
            {
                //DataGridViewColumnHeaderCell headerCell = column.HeaderCell;
                //string headerCaptionText = column.HeaderText;
                //string columnName = column.Name; // Used as a key to myDataGridView.Columns['key_name'];                
                bIsLoading = true;
                if (dgcols.TryGetValue(dgView.Columns[i].Name, out thisCol))
                {
                    dgView.Columns[i].Width = thisCol.Width;
                    dgView.Columns[i].HeaderText = thisCol.Heading;
                    if (thisCol.Alignment == "Left") dgView.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    if (thisCol.Alignment == "Right") dgView.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    if (thisCol.Alignment == "Centre") dgView.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    if (thisCol.Format != null) dgView.Columns[i].DefaultCellStyle.Format = thisCol.Format;
                    dgView.Columns[i].ReadOnly = thisCol.ReadOnly;
                    dgView.Columns[i].Visible = (thisCol.Group == currentTab || thisCol.Seq > 12000 && thisCol.Seq < 16000);
                    //MessageBox.Show(dgView.Columns[i].Name + ", " + dgView.Columns[i].HeaderText + ", " + thisCol.Group + ", " + i.ToString() + ", " + dgView.Columns[i].Visible.ToString());
                    //dgView.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    
                }
                bIsLoading = false;
            }

        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetupCols();
        }
        private void dgView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (!bClosing)
            {
                for (int i = 0; i < dgView.Columns.Count; i++)
                {
                    if (dgView.Columns[i].Visible) this.dgView.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;
                }
            }

        }
        private void BMSetupMain_Shown(object sender, EventArgs e)
        {
            SetupCols();

        }
        private void cboSearchCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bIsLoading || cboSearchCode.SelectedIndex < 0)
                return;

            //int iFindNo = 14;
            // int iFindNo = (int)cboSearchCode.SelectedValue;
            //int selectedValue;
            //bool parseOK = Int32.TryParse(cboSearchCode.SelectedValue.ToString(), out selectedValue);
            DataRowView drv = (DataRowView)cboSearchCode.SelectedItem;
            int iFindNo = (int)drv.Row.ItemArray[0];
            int pmIDCol = 2;

            int j = (int)dgView.Rows.Count - 1;
            int iRowIndex = -1;
            for (int i = 0; i < Convert.ToInt32(dgView.Rows.Count / 2) + 1; i++)
            {
                if (dgView.Rows[i].Cells[pmIDCol].Value.ToString().Trim().Length > 0)
                {
                    if (Convert.ToInt32(dgView.Rows[i].Cells[pmIDCol].Value) == iFindNo)
                    {
                        iRowIndex = i;
                        break;
                    }
                }
                if ((dgView.Rows[i].Cells[pmIDCol].Value.ToString().Trim().Length > 0))
                {
                    if (Convert.ToInt32(dgView.Rows[j].Cells[pmIDCol].Value) == iFindNo)
                    {
                        iRowIndex = j;
                        break;
                    }
                    j--;
                }
            }
            if (iRowIndex > -1)
            {
                dgView.CurrentCell = dgView.Rows[iRowIndex].Cells["Code"];
                dgView.Rows[dgView.CurrentCell.RowIndex].Selected = true;
            }
            else
                MessageBox.Show("Index not found.");

        }
        private void dgView_Sorted(object sender, EventArgs e)
        {
            SetupCols();
        }
        private void dgView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
        private void dgView_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (bIsLoading)
                return;
            if (cbo.Visible)
            {
                dgView.CurrentCell.Value = cbo.Text;
                cbo.Visible = false;
                dgView.EndEdit();
            }
            if (cboMachine.Visible)
            {
                dgView.CurrentCell.Value = cboMachine.Text;
                cboMachine.Visible = false;
                dgView.EndEdit();
            }
            if (cboCustomer.Visible)
            {
                dgView.CurrentCell.Value = cboCustomer.Text;
                cboCustomer.Visible = false;
                dgView.EndEdit();
            }
            if (cboMaterial.Visible)
            {
                dgView.CurrentCell.Value = cboMaterial.Text;
                cboMaterial.Visible = false;
                dgView.EndEdit();
            }
            if (cboMaterialGrade.Visible)
            {
                dgView.CurrentCell.Value = cboMaterialGrade.Text;
                cboMaterialGrade.Visible = false;
                dgView.EndEdit();
            }
            if (cboPallet.Visible)
            {
                dgView.CurrentCell.Value = cboPallet.Text;
                cboPallet.Visible = false;
                dgView.EndEdit();
            }
            if (cboMBColour.Visible == true)
            {
                dgView.CurrentCell.Value = cboMBColour.Text;
                cboMBColour.Visible = false;
            }
            if (cboMBCode.Visible == true)
            {
                dgView.CurrentCell.Value = cboMBCode.Text;
                cboMBCode.Visible = false;
            }
        }
        private void dgView_Scroll(object sender, ScrollEventArgs e)
        {
            if (cbo.Visible == true)
            {
                dgView.CurrentCell.Value = cbo.Text;
                cbo.Visible = false;
                dgView.EndEdit();
            }
            if (cboMachine.Visible)
            {
                dgView.CurrentCell.Value = cboMachine.Text;
                cboMachine.Visible = false;
                dgView.EndEdit();
            }
            else if (cboProductGrade.Visible == true)
            {
                dgView.CurrentCell.Value = cboProductGrade.Text;
                cboProductGrade.Visible = false;
                dgView.EndEdit();
            }
            if (cboCustomer.Visible)
            {
                dgView.CurrentCell.Value = cboCustomer.Text;
                cboCustomer.Visible = false;
                dgView.EndEdit();
            }
            if (cboMaterial.Visible)
            {
                dgView.CurrentCell.Value = cboMaterial.Text;
                cboMaterial.Visible = false;
                dgView.EndEdit();
            }
            if (cboMaterialGrade.Visible == true)
            {
                dgView.CurrentCell.Value = cboMaterialGrade.Text;
                cboMaterialGrade.Visible = false;
                dgView.EndEdit();
            }
            if (cboPallet.Visible)
            {
                dgView.CurrentCell.Value = cboPallet.Text;
                cboPallet.Visible = false;
                dgView.EndEdit();
            }
            if (cboMBColour.Visible == true)
            {
                dgView.CurrentCell.Value = cboMBColour.Text;
                cboMBColour.Visible = false;
            }
            if (cboMBCode.Visible == true)
            {
                dgView.CurrentCell.Value = cboMBCode.Text;
                cboMBCode.Visible = false;
            }
        }
        private void dgView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            GridColumns thisCol;
            if (dgcols.TryGetValue(dgView.Columns[e.ColumnIndex].Name, out thisCol))
            {
                if (thisCol.ColumnName == "Code" && currentTab == "Product")
                {
                    cbo.Location = dgView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cbo.Width = thisCol.Width;
                    cbo.Visible = true;
                }
                else if (thisCol.ColumnName == "Machine" && currentTab == "Product")
                {
                    cboMachine.Location = dgView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cboMachine.Width = thisCol.Width;
                    cboMachine.Visible = true;
                }
                else if (thisCol.ColumnName == "CUSTNAME" && currentTab == "Product")
                {
                    cboCustomer.Location = dgView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cboCustomer.Width = thisCol.Width;
                    cboCustomer.Visible = true;
                }
                else if (thisCol.ColumnName == "MaterialType" && currentTab == "Product")
                {
                    cboMaterial.Location = dgView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cboMaterial.Width = thisCol.Width;
                    cboMaterial.Text = this.dgView.CurrentCell.Value.ToString();
                    cboMaterial.Visible = true;
                }                
                else if (thisCol.ColumnName == "ProductCategory" && currentTab == "Product")
                {
                    cboProductGrade.Location = dgView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cboProductGrade.Width = thisCol.Width;
                    cboProductGrade.Text = this.dgView.CurrentCell.Value.ToString();
                    cboProductGrade.Visible = true;
                }
                else if (thisCol.ColumnName == "MaterialTypeA" && currentTab == "Material")
                {
                    cboMaterial.Location = dgView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cboMaterial.Width = thisCol.Width;
                    cboMaterial.Text = this.dgView.CurrentCell.Value.ToString();
                    cboMaterial.Visible = true;
                }
                else if (thisCol.ColumnName == "MaterialTypeB" && currentTab == "Material")
                {
                    cboMaterial.Location = dgView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cboMaterial.Width = thisCol.Width;
                    cboMaterial.Text = this.dgView.CurrentCell.Value.ToString();
                    cboMaterial.Visible = true;
                }
                else if (thisCol.ColumnName == "MaterialGradeA" && currentTab == "Material")
                {
                    cboMaterialGrade.Location = dgView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cboMaterialGrade.Width = thisCol.Width;
                    cboMaterialGrade.Text = this.dgView.CurrentCell.Value.ToString();
                    cboMaterialGrade.Visible = true;
                }
                else if (thisCol.ColumnName == "MaterialGradeB" && currentTab == "Material")
                {
                    cboMaterialGrade.Location = dgView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cboMaterialGrade.Width = thisCol.Width;
                    cboMaterialGrade.Text = this.dgView.CurrentCell.Value.ToString();
                    cboMaterialGrade.Visible = true;
                }
                else if (thisCol.ColumnName == "AdditiveCode" && currentTab == "MasterBatch")
                {
                    cboAdditive.Location = dgView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cboAdditive.Width = thisCol.Width;
                    cboAdditive.Text = this.dgView.CurrentCell.Value.ToString();
                    cboAdditive.Visible = true;
                }
                else if (thisCol.ColumnName == "AdditiveB" && currentTab == "MasterBatch")
                {
                    cboAdditive.Location = dgView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cboAdditive.Width = thisCol.Width;
                    cboAdditive.Text = this.dgView.CurrentCell.Value.ToString();
                    cboAdditive.Visible = true;
                }
                else if (thisCol.ColumnName == "MBColourA" && currentTab == "MasterBatch")
                {
                    cboMBColour.Location = dgView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cboMBColour.Width = thisCol.Width;
                    cboMBColour.Text = this.dgView.CurrentCell.Value.ToString();
                    cboMBColour.Visible = true;
                }
                else if (thisCol.ColumnName == "MBColourB" && currentTab == "MasterBatch")
                {
                    cboMBColour.Location = dgView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cboMBColour.Width = thisCol.Width;
                    cboMBColour.Text = this.dgView.CurrentCell.Value.ToString();
                    cboMBColour.Visible = true;
                }
                else if (thisCol.ColumnName == "MBCodeA" && currentTab == "MasterBatch")
                {
                    cboMBCode.Location = dgView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cboMBCode.Width = thisCol.Width;
                    cboMBCode.Text = this.dgView.CurrentCell.Value.ToString();
                    cboMBCode.Visible = true;
                }
                else if (thisCol.ColumnName == "MBCodeB" && currentTab == "MasterBatch")
                {
                    cboMBCode.Location = dgView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cboMBCode.Width = thisCol.Width;
                    cboMBCode.Text = this.dgView.CurrentCell.Value.ToString();
                    cboMBCode.Visible = true;
                }
                else if (thisCol.ColumnName == "PalletType" && currentTab == "Packaging")
                {
                    cboPallet.Location = dgView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cboPallet.Width = thisCol.Width;
                    cboPallet.Text = this.dgView.CurrentCell.Value.ToString();
                    cboPallet.Visible = true;
                }
                else if (thisCol.DisplayLines > 1 && !thisCol.ReadOnly)
                {
                    this.tbx.Text = this.dgView.CurrentCell.Value.ToString();
                    int x = this.dgView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location.X - 3;
                    int y = this.dgView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location.Y - 3;
                    this.tbx.Location = new Point(x, y);
                    int width = this.dgView.CurrentCell.Size.Width + 6;
                    int height = this.dgView.CurrentCell.Size.Height * thisCol.DisplayLines + 6;
                    this.tbx.Size = new Size(width, height);
                    this.tbx.Visible = true;
                    this.dgView.BeginInvoke(new MethodInvoker(MyMethod));
                }
            }
        }
        private void tbx_KeyDown(object sender, KeyEventArgs e)
        {
            // Initialize the flag to false.
            nonNumberEntered = false;

            // Determine whether the keystroke is a number from the top of the keyboard.
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
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
        private void tbx_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

            if (e.KeyChar == '-' && (sender as TextBox).Text.Length > 0)
            {
                e.Handled = true;
            }
        }
        private void dgView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (tbx.Visible == true)
            {
                this.dgView.CurrentCell.Value = this.tbx.Text;
                this.tbx.Visible = false;
            }
            else if (cbo.Visible == true)
            {
                dgView.CurrentCell.Value = cbo.Text;
                cbo.Visible = false;
            }
            else if (cboMachine.Visible == true)
            {
                dgView.CurrentCell.Value = cboMachine.Text;
                cboMachine.Visible = false;
            }
            else if (cboCustomer.Visible == true)
            {
                dgView.CurrentCell.Value = cboCustomer.Text;
                cboCustomer.Visible = false;
            }
            else if (cboMaterial.Visible == true)
            {
                dgView.CurrentCell.Value = cboMaterial.Text;
                cboMaterial.Visible = false;
            }
            else if (cboProductGrade.Visible == true)
            {
                dgView.CurrentCell.Value = cboProductGrade.Text;
                cboProductGrade.Visible = false;
            }
            else if (cboMaterialGrade.Visible == true)
            {
                dgView.CurrentCell.Value = cboMaterialGrade.Text;
                cboMaterialGrade.Visible = false;
            }
            else if (cboAdditive.Visible == true)
            {
                dgView.CurrentCell.Value = cboAdditive.Text;
                cboAdditive.Visible = false;
            }
            else if (cboPallet.Visible == true)
            {
                dgView.CurrentCell.Value = cboPallet.Text;
                cboPallet.Visible = false;
            }
            else if (cboMBColour.Visible == true)
            {
                dgView.CurrentCell.Value = cboMBColour.Text;
                cboMBColour.Visible = false;
            }
            else if (cboMBCode.Visible == true)
            {
                dgView.CurrentCell.Value = cboMBCode.Text;
                cboMBCode.Visible = false;
            }
            // SetupCols();
        }
        private void MyMethod()
        {
            this.tbx.Focus();
            this.tbx.SelectAll();
        }
        private void BMSetupMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                bClosing = true;
                if (dgView.IsCurrentRowDirty)
                {
                    this.Validate();
                }
                dgView.EndEdit();
                dgView.DataSource = null;
                DataService.ProductDataService ds = new DataService.ProductDataService();
                ds.UpdateBMSetup(BMSetupDS);

            }
            catch
            {
                throw;
            }
        }
        private void dgView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (!e.Row.IsNewRow)
            {
                DialogResult response = MessageBox.Show("Are you sure?", "Delete row?",
                                  MessageBoxButtons.YesNo,
                                  MessageBoxIcon.Question,
                                  MessageBoxDefaultButton.Button2);

                if (response == DialogResult.No)
                    e.Cancel = true;
            }
        }
        private void dgView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            GridColumns thisCol;
            if (dgcols.TryGetValue(dgView.Columns[dgView.CurrentCell.ColumnIndex].Name, out thisCol))
            {
                if (thisCol.DataType=="real" || thisCol.DataType == "int")
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
        private void machineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Machine f = new Machine();
            f.ShowDialog();
        }
        private void materialTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MaterialTypeForm f = new MaterialTypeForm();
            f.ShowDialog();
        }
        private void materialGradeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MaterialGradeForm f = new MaterialGradeForm();
            f.ShowDialog();
        }
        private void masterbatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
        private void palletToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductGradeForm f = new ProductGradeForm();
            f.ShowDialog();
        }
        private void blowMouldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.NextForm = "BMSetupMain";
            this.Close();
        }
        private void injectionMouldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.NextForm = "IMSpecificationMain";
            this.Close();
        }
        private void masterbatchToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MasterBatchForm f = new MasterBatchForm();
            f.ShowDialog();
        }
        private void additiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdditiveForm f = new AdditiveForm();
            f.ShowDialog();
        }
        private void cartonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CartonPackaging f = new CartonPackaging();
            f.ShowDialog();
        }
        private void palletToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PalletForm f = new PalletForm();
            f.ShowDialog();
        }
        //private void injectionMouldToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    nextForm = "IMSpecificationMain";
        //    this.Close();
        //    //IMSpecificationMain f = new IMSpecificationMain();
        //    //f.Show();
        //    //if (BMSetupMain.ActiveForm != null) BMSetupMain.ActiveForm.Close();
        //    //Application.Run(new IMSpecificationMain());

        //}

        //private void blowMouldToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    this.Close();
        //    nextForm = "BMSetupMain";


        //}
    }
    
}

  
