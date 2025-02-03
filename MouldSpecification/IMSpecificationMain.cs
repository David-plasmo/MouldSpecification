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
using MouldSpecification;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;

namespace IMSpecification
{
    public partial class IMSpecificationMain : Form
    {
        dgSettingsDictionary dgcols;
        DataSet dsMG;
        DataSet dsMAG;
        DataSet dsMC; 
        DataSet IMSpecificationDS;
        DataSet dsAdditive;
        DataSet dsCartonPackagingDropdown;
        
        //DataSet MBColour;
        //DataSet MBCode;
        TextBox tbx;

        ComboBox cbo;
        ComboBox cboMachine;
        ComboBox cboCustomer;
        ComboBox cboCompanyCode;
        //ComboBox cboMaterial;
        ComboBox cboProductGrade;
        ComboBox cboPallet;
        ComboBox cboMBColour;
        //ComboBox cboMBCode = new ComboBox();
        ComboBox cboMaterialGrade;
        //ComboBox cboAdditive;
        ComboBox cboAdditiveCode;
        ComboBox cboCartonPackaging;
        ComboBox cboOperation;

        bool bIsLoading;
        bool bClosing = false;
        bool nonNumberEntered = false;
        string currentTab;
        string curImagePath;
        string cname;
        
        public IMSpecificationMain()
        {
            InitializeComponent();
            Program.NextForm = "";
        }
        private void IMSpecificationMain_Load(object sender, EventArgs e)
        {
            DoLoad();
            dgvEdit.CurrentCell = dgvEdit.Rows[0].Cells["Code"];
        }        
        private void DoLoad()
        {
            try
            {
                bIsLoading = true;
                dgcols = new dgSettingsDictionary("IM");
                this.injectionMouldingToolStripMenuItem.Enabled = false;
                this.tbx = new TextBox();
                this.tbx.Multiline = true;
                this.tbx.Visible = false;
                this.dgvEdit.Controls.Add(this.tbx);

                //this.ec = new TextBox();
                //ec.KeyPress += ec_KeyPress; //add this
                //this.dgvEdit.Controls.Add(this.ec);
                cboMaterialGrade = new ComboBox();
                cboMaterialGrade.Visible = false;


                dsMG = new ProductDataService().GetMaterialAndGrade();
                cboSearchCode.ValueMember = "PmID";
                cboSearchCode.DisplayMember = "Code";
                cboSearchCode.DataSource = dsMG.Tables[0];
                cboSearchCode.ResetText();
                cboSearchCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                cboSearchCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cboSearchCode.AutoCompleteSource = AutoCompleteSource.ListItems;
                cboSearchCode.SelectedIndexChanged += new System.EventHandler(cboSearchCode_SelectedIndexChanged);

                /*
                 comboBoxsubject.ValueMember = dt.Columns[0].ToString();
                 comboBoxsubject.DisplayMember = dt.Columns[1].ToString();
                 comboBoxsubject.DataSource = dt; 
                 */

                DataSet dsC = new DataService.ProductDataService().SelectCPCustomerDropdown();
                this.cboCustomer = new ComboBox();
                this.cboCustomer.Width = 200;
                this.cboCustomer.ValueMember = "CustID";
                this.cboCustomer.DisplayMember = "Customer";
                this.cboCustomer.DataSource = dsC.Tables[0];
                this.cboCustomer.Visible = false;
                this.dgvEdit.Controls.Add(this.cboCustomer);
                //Associate the event-handling method with the 
                // SelectedIndexChanged event.
                this.cboCustomer.SelectedIndexChanged += new System.EventHandler(cboCustomer_SelectedIndexChanged);

                DataSet dsPg = new ProductDataService().GetProductGrade();
                this.cboProductGrade = new ComboBox();
                this.cboProductGrade.ValueMember = "GradeID";
                this.cboProductGrade.DisplayMember = "Description";
                this.cboProductGrade.DataSource = dsPg.Tables[0];
                this.cboProductGrade.Visible = false;
                this.dgvEdit.Controls.Add(this.cboProductGrade);
                //Associate the event-handling method with the 
                // SelectedIndexChanged event.
                this.cboProductGrade.SelectedIndexChanged += new System.EventHandler(cboProductGrade_SelectedIndexChanged);

                DataSet dsM = new DataService.ProductDataService().GetMachine("IM");
                this.cboMachine = new ComboBox();
                this.cboMachine.ValueMember = "MachineID";
                this.cboMachine.DisplayMember = "Machine";
                this.cboMachine.DataSource = dsM.Tables[0];
                this.cboMachine.Visible = false;
                this.dgvEdit.Controls.Add(this.cboMachine);
                //Associate the event-handling method with the 
                // SelectedIndexChanged event.
                this.cboMachine.SelectedIndexChanged += new System.EventHandler(cboMachine_SelectedIndexChanged);

                DataSet dsCmp = new DataService.ProductDataService().GetCompany();
                this.cboCompanyCode = new ComboBox();
                this.cboCompanyCode.ValueMember = "CompanyCode";
                this.cboCompanyCode.DisplayMember = "CompanyCode";
                this.cboCompanyCode.DataSource = dsCmp.Tables[0];
                this.cboCompanyCode.Visible = false;
                this.dgvEdit.Controls.Add(this.cboCompanyCode);
                //Associate the event-handling method with the 
                // SelectedIndexChanged event.
                this.cboCompanyCode.SelectedIndexChanged += new System.EventHandler(cboCompanyCode_SelectedIndexChanged);



                //int? material                
                //DataSet dsMt = new DataService.ProductDataService().GetMaterial();
                //this.cboMaterial = new ComboBox();
                ////this.cboMaterial.Width = 200;
                //this.cboMaterial.ValueMember = "MaterialID";
                //this.cboMaterial.DisplayMember = "ShortDesc";
                //this.cboMaterial.DataSource = dsMt.Tables[0];
                //this.cboMaterial.Visible = false;
                //this.dgvEdit.Controls.Add(this.cboMaterial);
                ////Associate the event-handling method with the 
                //// SelectedIndexChanged event.
                //this.cboMaterial.SelectedIndexChanged += new System.EventHandler(cboMaterial_SelectedIndexChanged);
                //this.cboMaterial.TextChanged += new System.EventHandler(cboMaterial_SelectedIndexChanged);


                //this.cboMaterial.Width = 200;
                /*
                 comboBoxsubject.ValueMember = dt.Columns[0].ToString();
                 comboBoxsubject.DisplayMember = dt.Columns[1].ToString();
                 comboBoxsubject.DataSource = dt; 
                 */
                dsMAG = new DataService.ProductDataService().MaterialAndGradeDropdown();
                this.cboMaterialGrade = new ComboBox();
                this.cboMaterialGrade.ValueMember = "MaterialGradeID";
                this.cboMaterialGrade.DisplayMember = "ComboList";
                this.cboMaterialGrade.DataSource = dsMAG.Tables[0];
                this.cboMaterialGrade.Visible = false;
                //this.cboMaterialGrade.Font = new Font("Courier New", 10);
                this.cboMaterialGrade.Font = new Font("Lucida Sans Typewriter", 10);
                this.cboMaterialGrade.DropDownWidth = 400;
                this.dgvEdit.Controls.Add(this.cboMaterialGrade);
                //Associate the event-handling method with the 
                // SelectedIndexChanged event.
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
                //Associate the event-handling method with the 
                // SelectedIndexChanged event.
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
                //Associate the event-handling method with the 
                // SelectedIndexChanged event.
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
                //Associate the event-handling method with the 
                // SelectedIndexChanged event.
                this.cboAdditiveCode.SelectedIndexChanged += new System.EventHandler(cboAdditiveCode_SelectedIndexChanged);
                this.cboAdditiveCode.TextChanged += new System.EventHandler(cboAdditiveCode_TextChanged);

                //this.cboAdditive = new ComboBox();
                ////this.cboMaterial.Width = 200;
                //this.cboAdditive.ValueMember = "ADDID";
                //this.cboAdditive.DisplayMember = "Additive";
                //this.cboAdditive.DataSource = dsAdditive.Tables[0];
                //this.cboAdditive.Visible = false;
                //this.dgvEdit.Controls.Add(this.cboAdditive);
                ////Associate the event-handling method with the 
                //// SelectedIndexChanged event.
                //this.cboAdditive.SelectedIndexChanged += new System.EventHandler(cboAdditive_SelectedIndexChanged);

                dsCartonPackagingDropdown = new DataService.ProductDataService().GetCartonPackagingDropdown();
                this.cboCartonPackaging = new ComboBox();
                //this.cboMaterial.Width = 200;
                this.cboCartonPackaging.ValueMember = "CtnID";
                this.cboCartonPackaging.DisplayMember = "CartonType";
                this.cboCartonPackaging.DataSource = dsCartonPackagingDropdown.Tables[0];
                this.cboCartonPackaging.Visible = false;
                this.dgvEdit.Controls.Add(this.cboCartonPackaging);
                //Associate the event-handling method with the 
                // SelectedIndexChanged event.
                this.cboCartonPackaging.SelectedIndexChanged += new System.EventHandler(cboCartonPackaging_SelectedIndexChanged);

                this.cboOperation = new ComboBox();
                cboOperation.Items.Add("Automatic");
                cboOperation.Items.Add("Manual");
                cboOperation.Visible = false;
                this.dgvEdit.Controls.Add(this.cboOperation);
                this.cboOperation.SelectedIndexChanged += new System.EventHandler(cboOperation_SelectedIndexChanged);

                //cboMBCode.Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            LoadGrid();
            bIsLoading = false;
        }
        private void LoadGrid()
        {
            ProductDataService pds = new ProductDataService();
            IMSpecificationDS = pds.SelectIMSpecification();
            dgvEdit.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvEdit.AutoGenerateColumns = true;
            dgvEdit.DataSource = IMSpecificationDS.Tables[0];
        }
        private void SetupCols()
        {
            currentTab = tabControl1.SelectedTab.Name;           
            GridColumns thisCol;
            //int istVisibleCol = 9;
            for (int i = 0; i < 16; i++)
            {
                //MessageBox.Show(i.ToString() + "=" + dgvEdit.Columns[i].Name);
                dgvEdit.Columns[i].Visible = false;
            }
            //for (int i = 104; i < dgvEdit.Columns.Count; i++)
            //{
            //    dgvEdit.Columns[i].Visible = false;
            //}
            for (int i = 16; i < dgvEdit.Columns.Count; i++)
            {
                //DataGridViewColumnHeaderCell headerCell = column.HeaderCell;
                //string headerCaptionText = column.HeaderText;
                //string columnName = column.Name; // Used as a key to myDataGridView.Columns['key_name'];                
                bIsLoading = true;
                //if (i == 120)
                //MessageBox.Show(i.ToString() + "=" + dgvEdit.Columns[i].Name);

                if (dgcols.TryGetValue(dgvEdit.Columns[i].Name, out thisCol))
                {
                    dgvEdit.Columns[i].Width = thisCol.Width;
                    dgvEdit.Columns[i].HeaderText = thisCol.Heading;
                    if (thisCol.Alignment == "Left") dgvEdit.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    if (thisCol.Alignment == "Right") dgvEdit.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    if (thisCol.Alignment == "Centre") dgvEdit.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    if (thisCol.Format != null && thisCol.Format.ToString().Length > 0)
                    {
                        dgvEdit.Columns[i].DefaultCellStyle.Format = thisCol.Format;
                    }
                    dgvEdit.Columns[i].ReadOnly = thisCol.ReadOnly;
                    
                    dgvEdit.Columns[i].Visible = (thisCol.Group == currentTab || (thisCol.Seq >= 17 && thisCol.Seq <= 19 ));
                    if (dgvEdit.Columns[i].Visible && currentTab != "Product" && thisCol.ColumnName == "CompanyCode") dgvEdit.Columns[i].ReadOnly = true;
                    //MessageBox.Show(dgvEdit.Columns[i].Name + ", " + dgvEdit.Columns[i].HeaderText + ", " + thisCol.Group + ", " + i.ToString() + ", " + dgvEdit.Columns[i].Visible.ToString());
                    //dgvEdit.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                }
                bIsLoading = false;
            }
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

            int j = (int)dgvEdit.Rows.Count - 1;
            int iRowIndex = -1;
            for (int i = 0; i < Convert.ToInt32(dgvEdit.Rows.Count / 2) + 1; i++)
            {
                if (dgvEdit.Rows[i].Cells[pmIDCol].Value.ToString().Trim().Length > 0)
                {
                    if (Convert.ToInt32(dgvEdit.Rows[i].Cells[pmIDCol].Value) == iFindNo)
                    {
                        iRowIndex = i;
                        break;
                    }
                }
                if ((dgvEdit.Rows[i].Cells[pmIDCol].Value.ToString().Trim().Length > 0))
                {
                    if (Convert.ToInt32(dgvEdit.Rows[j].Cells[pmIDCol].Value) == iFindNo)
                    {
                        iRowIndex = j;
                        break;
                    }
                    j--;
                }
            }
            if (iRowIndex > -1)
            {
                dgvEdit.CurrentCell = dgvEdit.Rows[iRowIndex].Cells["Code"];
                dgvEdit.Rows[dgvEdit.CurrentCell.RowIndex].Selected = true;
                dgvEdit.FirstDisplayedScrollingRowIndex = iRowIndex;
            }
            else
            MessageBox.Show("Index not found.");
        }
        private void cbo_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cbo.SelectedIndex > -1)
            {
                int pmID = (int)cbo.SelectedValue;
                DataView dv = new DataView(dsMG.Tables[0]);
                dv.RowFilter = "PmID=" + pmID.ToString();
                if (dv.Count > 0)
                {
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["PmID"].Value = (int)dv[0]["PmID"];
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["MaterialID"].Value = DBNull.Value;
                    if (dv[0]["MaterialID"] != null && dv[0]["MaterialID"] != DBNull.Value)
                    {
                        dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["MaterialID"].Value = (int)dv[0]["MaterialID"];
                        dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Polymer1TypeID"].Value = (int)dv[0]["MaterialID"];
                    }
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Code"].Value = dv[0]["Code"].ToString().Trim();
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Description"].Value = dv[0]["Description"].ToString().Trim();
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Polymer1Type"].Value = dv[0]["Material"].ToString().Trim();
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["GradeID"].Value = (int)dv[0]["GradeID"];
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["ProductCategory"].Value = dv[0]["ProductCategory"].ToString().Trim();
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["CompanyCode"].Value = dv[0]["CompanyCode"].ToString().Trim();

                }
            }
        }
        private void cboMachine_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cboMachine.SelectedIndex > -1)
            {
                
                int mID = (int)cboMachine.SelectedValue;
                if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "MachineA")
                {
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["MachineAID"].Value = mID;
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["MachineA"].Value = cboMachine.Text;
                }
                else if(dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "MachineB")
                {
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["MachineBID"].Value = mID;
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["MachineB"].Value = cboMachine.Text;
                }
                else if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "MachineC")
                {
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["MachineCID"].Value = mID;
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["MachineC"].Value = cboMachine.Text;
                }

            }
        }
        private void cboProductGrade_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cboProductGrade.SelectedIndex > -1)
            {
                int pgID = (int)cboProductGrade.SelectedValue;
                dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["GradeID"].Value = pgID;
                dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["ProductCategory"].Value = cboProductGrade.Text;
            }
        }
        private void cboCustomer_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (!bIsLoading)
            {
                int custid = (int)cboCustomer.SelectedValue;
                dgvEdit.CurrentRow.Cells["CustID"].Value = custid;
            }
        }
        private void cboCompanyCode_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cboCompanyCode.SelectedIndex > -1)
            {
                string compCode = cboCompanyCode.SelectedValue.ToString().Trim(); 
                dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["CompanyCode"].Value = cboCompanyCode.Text;
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
            }
        }
        private void cboPallet_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cboPallet.SelectedIndex > -1)
            {
                string pallet = cboPallet.SelectedValue.ToString().Trim(); ;
                dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["PalletType"].Value = pallet;
            }
        }
        //private void dboMaterial_SelectedTextChanged(object sender, System.EventArgs e)
        //{
        //    //if (cboMaterial.SelectedIndex < 0)
        //    //{
        //    //    //comboBox1.Text = "Please, select any value";
        //    //}
        //    //else
        //    //{
        //    //    cboMaterial.Text = cboMaterial.SelectedText;
        //    //}
        //}

        //private void cboMaterial_SelectedIndexChanged(object sender, System.EventArgs e)
        //{
        //    if (cboMaterial.SelectedIndex > -1)
        //    {
        //        int materialID = (int)cboMaterial.SelectedValue;
        //        //DataSet dsMGCopy = dsMG.Copy();
        //        //DataView dv = new DataView(dsMGCopy.Tables[0]);
        //        //dv.RowFilter = "MaterialID=" + materialID.ToString();
        //        ////this.cboMaterialGrade = new ComboBox();
        //        ////this.cboMaterial.Width = 200;
        //        ///*
        //        // comboBoxsubject.ValueMember = dt.Columns[0].ToString();
        //        // comboBoxsubject.DisplayMember = dt.Columns[1].ToString();
        //        // comboBoxsubject.DataSource = dt; 
        //        // */
        //        //this.cboMaterialGrade.ValueMember = "MaterialGradeID";
        //        //this.cboMaterialGrade.DisplayMember = "MaterialGrade";
        //        //this.cboMaterialGrade.DataSource = dv; //dsMg.Tables[0];
        //        //this.cboMaterialGrade.Visible = false;
        //        ////this.dgvEdit.Controls.Add(this.cboMaterialGrade);
        //        ////Associate the event-handling method with the 
        //        //// SelectedIndexChanged event.
        //        ////this.cboMaterialGrade.SelectedIndexChanged += new System.EventHandler(cboMaterialGrade_SelectedIndexChanged);

        //        if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "Polymer1Type")
        //        {
        //            //dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["MaterialIDA"].Value = MID;
        //            dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Polymer1Type"].Value = cboMaterial.Text;
        //            dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Polymer1TypeID"].Value = materialID;
        //            dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["MaterialID"].Value = materialID;
        //        }
        //        else if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "Polymer2Type")
        //        {
        //            //dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["MaterialIDB"].Value = MID;
        //            dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Polymer2Type"].Value = cboMaterial.Text;
        //            dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Polymer1TypeID"].Value = materialID;
        //        }
        //    }
        //    else if (cboMaterial.Text.Length == 0)
        //    {
        //        //allow blanked out selected item
        //        if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "Polymer1Type")
        //        {
        //            //dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["MaterialIDA"].Value = MID;
        //            dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Polymer1Type"].Value = DBNull.Value;
        //            dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Polymer1TypeID"].Value = DBNull.Value;
        //            dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["MaterialID"].Value = DBNull.Value;
        //        }
        //        else if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "Polymer2Type")
        //        {
        //            //dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["MaterialIDB"].Value = MID;
        //            dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Polymer2Type"].Value = DBNull.Value;
        //            dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Polymer1TypeID"].Value = DBNull.Value;
        //        }
        //    }
        //}

        //private void cboAdditive_SelectedIndexChanged(object sender, System.EventArgs e)
        //{
        //    if (cboAdditive.SelectedIndex > -1)
        //    {
        //        int aID = (int)cboAdditive.SelectedValue;
        //        // Get the DataTable of a DataSet.
        //        DataTable table = dsAdditive.Copy().Tables[0];
        //        DataRow[] row = table.Select("ADDID = " + aID.ToString());
        //        if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "Additive")
        //        {
        //            dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["AdditiveACodeID"].Value = aID;
        //            dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Additive"].Value = cboAdditive.Text;
        //            dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["AdditiveCode"].Value = row[0]["AdditiveCode"];
        //        }
        //        else if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "AdditiveB")
        //        {
        //            dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["AdditiveBCodeID"].Value = aID;
        //            dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["AdditiveB"].Value = cboAdditive.Text;
        //            dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["AdditiveBCode"].Value = row[0]["AdditiveCode"];
        //        }
        //    }
        //}
        private void cboAdditiveCode_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cboAdditiveCode.SelectedIndex > -1)
            {
                int aID = (int)cboAdditiveCode.SelectedValue;
                DataTable table = dsAdditive.Copy().Tables[0];
                DataRow[] row = table.Select("ADDID = " + aID.ToString());
                if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "AdditiveCode" ||
                    dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "Additive")
                {
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["AdditiveACodeID"].Value = aID;
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["AdditiveCode"].Value = row[0]["AdditiveCode"];//cboAdditiveCode.Text;
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Additive"].Value =  row[0]["Additive"];
                }
                else if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "AdditiveBCode" ||
                         dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "AdditiveB")
                {
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["AdditiveBCodeID"].Value = aID;
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["AdditiveBCode"].Value = row[0]["AdditiveCode"];//cboAdditiveCode.Text;
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["AdditiveB"].Value = row[0]["Additive"];
                }
            }
        }
        private void cboMBColour_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cboMBColour.SelectedIndex > -1)
            {
                int mbid = (int)cboMBColour.SelectedValue;
                DataSet dsMCCopy = dsMC.Copy();
                DataView dv = new DataView(dsMCCopy.Tables[0]);
                dv.RowFilter = "MBID = " + mbid.ToString();
                DataTable tb = dv.ToTable();

                string mbColour = tb.Rows[0]["Colour"].ToString().Trim();//(string)cboMBColour.SelectedValue;
                string mbCode = tb.Rows[0]["Code"].ToString().Trim();//(string)cboMBColour.SelectedValue;
                //DataSet dsMcode = new ProductDataService().GetMBCode(mbColour);

                //this.cboMBCode = new ComboBox();
                //this.cboMBCode.ValueMember = "MBID";
                //this.cboMBCode.DisplayMember = "MBCode";
                //this.cboMBCode.DataSource = dsMcode.Tables[0];
                /////*
                // comboBoxsubject.ValueMember = dt.Columns[0].ToString();
                // comboBoxsubject.DisplayMember = dt.Columns[1].ToString();
                // comboBoxsubject.DataSource = dt; 
                // */
                //this.cboMBCode.Visible = false;
                //this.cboMBCode.ResetText();
                //this.dgvEdit.Controls.Add(this.cboMBCode);
                //Associate the event-handling method with the 
                // SelectedIndexChanged event.
                //this.cboMBCode.SelectedIndexChanged += new System.EventHandler(cboMBCode_SelectedIndexChanged);

                if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "MBColourA" ||
                    dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "MBCodeA")
                {
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["MBColourA"].Value = mbColour;
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["MBCodeA"].Value = mbCode;
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["MBID"].Value = mbid;
                }
                else if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "MBColourB" ||
                    dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "MBCodeB")
                {
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["MBColourB"].Value = mbColour;
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["MBCodeB"].Value = mbCode;
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["MBBID"].Value = mbid;
                }
            }
            else if (cboMBColour.Text.Length == 0)
            {
                //Allow blank value entry
                if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "MBColourA" ||
                    dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "MBCodeA")
                {
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["MBColourA"].Value = DBNull.Value;
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["MBCodeA"].Value = DBNull.Value;
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["MBID"].Value = DBNull.Value;
                }
                else if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "MBColourB" ||
                    dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "MBCodeB")
                {
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["MBColourB"].Value = DBNull.Value;
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["MBCodeB"].Value = DBNull.Value;
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["MBBID"].Value = DBNull.Value;
                }
            }
        }
        private void cboMaterialGrade_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cboMaterialGrade.SelectedIndex > -1)
            {
                int MID = (int)cboMaterialGrade.SelectedValue;
                DataSet dsMAGCopy = dsMAG.Copy();
                DataView dv = new DataView(dsMAGCopy.Tables[0]);
                dv.RowFilter = "MaterialGradeID = " + MID.ToString();
                DataTable tb = dv.ToTable();
                int materialID = (int)tb.Rows[0]["MaterialID"];

                if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "Polymer1Grade" ||
                    dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "Polymer1Type")
                {
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Polymer1GradeID"].Value = MID;
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Polymer1Grade"].Value = tb.Rows[0]["MaterialGrade"].ToString().Trim(); //cboMaterialGrade.Text;                   
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Polymer1Type"].Value = tb.Rows[0]["Material"].ToString().Trim();
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Polymer1TypeID"].Value = materialID;
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["MaterialID"].Value = materialID;
                }
                else if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "Polymer2Grade" ||
                         dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "Polymer2Type")
                {
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Polymer2GradeID"].Value = MID;
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Polymer2Grade"].Value = tb.Rows[0]["MaterialGrade"].ToString().Trim();  //cboMaterialGrade.Text;
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Polymer2Type"].Value = tb.Rows[0]["Material"].ToString().Trim();
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Polymer2TypeID"].Value = materialID;
                }
            }
            else if (cboMaterialGrade.Text.Length == 0)
            {
                //Allow blank value entry
                if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "Polymer1Grade" ||
                    dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "Polymer1Type")
                {
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Polymer1GradeID"].Value = DBNull.Value;
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Polymer1Grade"].Value = cboMaterialGrade.Text;
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Polymer1Type"].Value = DBNull.Value;
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Polymer1TypeID"].Value = DBNull.Value;
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["MaterialID"].Value = DBNull.Value;
                }
                else if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "Polymer2Grade" ||
                         dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "Polymer2Type")
                {
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Polymer2GradeID"].Value = DBNull.Value;
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Polymer2Grade"].Value = DBNull.Value;
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Polymer2Type"].Value = DBNull.Value;
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Polymer2TypeID"].Value = DBNull.Value;
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
                if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "CartonType")
                {
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["CtnID"].Value = ctnID;
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["CartonType"].Value = cboCartonPackaging.Text;                    
                }                
            }
        }
        private void cboOperation_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cboOperation.SelectedIndex > -1)
            {
                dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Operation"].Value = cboOperation.Text;
            }
        }
        private void IMSpecificationMain_Shown(object sender, EventArgs e)
        {
            SetupCols();
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetupCols();
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
        private void dgvEdit_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            GridColumns thisCol;
            if (dgcols.TryGetValue(dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name, out thisCol))
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
        private void dgvEdit_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
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
        private void dgvEdit_Sorted(object sender, EventArgs e)
        {
            SetupCols();
        }
        private void dgvEdit_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
        private void dgvEdit_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            TransferComboText();          
        }
        private void dgvEdit_Scroll(object sender, ScrollEventArgs e)
        {
            TransferComboText();
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
        private void dgvEdit_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            GridColumns thisCol;
            if (dgcols.TryGetValue(dgvEdit.Columns[e.ColumnIndex].Name, out thisCol))
            {
                if (thisCol.ColumnName == "Code" && currentTab == "Product" && cbo != null)
                {
                    cbo.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cbo.Width = thisCol.Width;
                    cbo.Visible = true;
                }
                else if (thisCol.ColumnName == "MachineA" && currentTab == "Product")
                {
                    cboMachine.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cboMachine.Width = thisCol.Width;
                    cboMachine.Visible = true;
                }
                else if (thisCol.ColumnName == "MachineB" && currentTab == "Product")
                {
                    cboMachine.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cboMachine.Width = thisCol.Width;
                    cboMachine.Visible = true;
                }
                else if (thisCol.ColumnName == "MachineC" && currentTab == "Product")
                {
                    cboMachine.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cboMachine.Width = thisCol.Width;
                    cboMachine.Visible = true;
                }
                else if (thisCol.ColumnName == "Customer" && currentTab == "Product")
                {
                    cboCustomer.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cboCustomer.Width = thisCol.Width;
                    cboCustomer.Visible = true;
                }
                //else if (thisCol.ColumnName == "MaterialType" && currentTab == "Product")
                //{
                //    cboMaterial.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                //    cboMaterial.Width = thisCol.Width;
                //    cboMaterial.Text = this.dgvEdit.CurrentCell.Value.ToString();
                //    cboMaterial.Visible = true;
                //}
                else if (thisCol.ColumnName == "ProductCategory" && currentTab == "Product")
                {
                    cboProductGrade.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cboProductGrade.Width = thisCol.Width;
                    cboProductGrade.Text = this.dgvEdit.CurrentCell.Value.ToString();
                    cboProductGrade.Visible = true;
                }
                else if (thisCol.ColumnName == "CompanyCode" && currentTab == "Product")
                {
                    cboCompanyCode.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cboCompanyCode.Width = thisCol.Width;
                    cboCompanyCode.Text = this.dgvEdit.CurrentCell.Value.ToString();
                    cboCompanyCode.Visible = true;
                }
                else if (thisCol.ColumnName == "Polymer1Type" && currentTab == "Material")
                {
                    cboMaterialGrade.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cboMaterialGrade.Width = thisCol.Width;
                    cboMaterialGrade.Text = this.dgvEdit.CurrentCell.Value.ToString();
                    cboMaterialGrade.Visible = true;
                }
                else if (thisCol.ColumnName == "Polymer2Type" && currentTab == "Material")
                {
                    cboMaterialGrade.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cboMaterialGrade.Width = thisCol.Width;
                    cboMaterialGrade.Text = this.dgvEdit.CurrentCell.Value.ToString();
                    cboMaterialGrade.Visible = true;
                }
                else if (thisCol.ColumnName == "Polymer1Grade" && currentTab == "Material")
                {
                    cboMaterialGrade.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cboMaterialGrade.Width = thisCol.Width;
                    cboMaterialGrade.Text = this.dgvEdit.CurrentCell.Value.ToString();
                    cboMaterialGrade.Visible = true;
                }
                else if (thisCol.ColumnName == "Polymer2Grade" && currentTab == "Material")
                {
                    cboMaterialGrade.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cboMaterialGrade.Width = thisCol.Width;
                    cboMaterialGrade.Text = this.dgvEdit.CurrentCell.Value.ToString();
                    cboMaterialGrade.Visible = true;
                }
                else if (thisCol.ColumnName == "AdditiveCode" && currentTab == "Masterbatch" ||
                         thisCol.ColumnName == "Additive" && currentTab == "Masterbatch" ||
                         thisCol.ColumnName == "AdditiveBCode" && currentTab == "Masterbatch" ||
                         thisCol.ColumnName == "AdditiveB" && currentTab == "Masterbatch")
                {
                    cboAdditiveCode.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cboAdditiveCode.Width = thisCol.Width;
                    cboAdditiveCode.Text = this.dgvEdit.CurrentCell.Value.ToString();
                    cboAdditiveCode.Visible = true;
                }
                //else if (thisCol.ColumnName == "Additive" && currentTab == "Masterbatch")
                //{
                //    cboAdditive.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                //    cboAdditive.Width = thisCol.Width;
                //    cboAdditive.Text = this.dgvEdit.CurrentCell.Value.ToString();
                //    cboAdditive.Visible = true;
                //}
                //else if (thisCol.ColumnName == "AdditiveBCode" && currentTab == "Masterbatch")
                //{
                //    cboAdditiveCode.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                //    cboAdditiveCode.Width = thisCol.Width;
                //    cboAdditiveCode.Text = this.dgvEdit.CurrentCell.Value.ToString();
                //    cboAdditiveCode.Visible = true;
                //}
                //else if (thisCol.ColumnName == "AdditiveB" && currentTab == "Masterbatch")
                //{
                //    cboAdditive.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                //    cboAdditive.Width = thisCol.Width;
                //    cboAdditive.Text = this.dgvEdit.CurrentCell.Value.ToString();
                //    cboAdditive.Visible = true;
                //}
                else if (thisCol.ColumnName == "MBColourA" && currentTab == "Masterbatch"  ||
                         thisCol.ColumnName == "MBCodeA" && currentTab == "Masterbatch")
                {
                    cboMBColour.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cboMBColour.Width = thisCol.Width;
                    cboMBColour.Text = this.dgvEdit.CurrentCell.Value.ToString();
                    cboMBColour.Visible = true;
                }
                else if (thisCol.ColumnName == "MBColourB" && currentTab == "Masterbatch" ||
                         thisCol.ColumnName == "MBCodeB" && currentTab == "Masterbatch")
                {
                    cboMBColour.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cboMBColour.Width = thisCol.Width;
                    cboMBColour.Text = this.dgvEdit.CurrentCell.Value.ToString();
                    cboMBColour.Visible = true;
                }
                //else if (thisCol.ColumnName == "MBCodeA" && currentTab == "Masterbatch")
                //{
                //    cboMBCode.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                //    cboMBCode.Width = thisCol.Width;
                //    cboMBCode.Text = this.dgvEdit.CurrentCell.Value.ToString();
                //    cboMBCode.Visible = true;
                //}
                //else if (thisCol.ColumnName == "MBCodeB" && currentTab == "Masterbatch")
                //{
                //    cboMBCode.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                //    cboMBCode.Width = thisCol.Width;
                //    cboMBCode.Text = this.dgvEdit.CurrentCell.Value.ToString();
                //    cboMBCode.Visible = true;
                //}
                else if (thisCol.ColumnName == "PalletType" && currentTab == "Pallets")
                {
                    cboPallet.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cboPallet.Width = thisCol.Width;
                    cboPallet.Text = this.dgvEdit.CurrentCell.Value.ToString();
                    cboPallet.Visible = true;
                }
                else if (thisCol.ColumnName == "CartonType" && currentTab == "Packaging")
                {
                    cboCartonPackaging.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cboCartonPackaging.Width = thisCol.Width;
                    cboCartonPackaging.Text = this.dgvEdit.CurrentCell.Value.ToString();
                    cboCartonPackaging.Visible = true;
                }
                else if (thisCol.ColumnName == "Operation" && currentTab == "Mould")
                {
                    cboOperation.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cboOperation.Width = thisCol.Width;
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
            //else if (cboMaterial.Visible == true)
            //{
            //    dgvEdit.CurrentCell.Value = cboMaterial.Text;
            //    cboMaterial.Visible = false;
            //}
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
            //else if (cboAdditive.Visible == true)
            //{
            //    dgvEdit.CurrentCell.Value = cboAdditive.Text;
            //    cboAdditive.Visible = false;
            //}
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
            //else if (cboMBCode.Visible == true)
            //{
            //    dgvEdit.CurrentCell.Value = cboMBCode.Text;
            //    cboMBCode.Visible = false;
            //}
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
        private void blowMouldingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.NextForm = "BMSetupMain";
            this.Close();
        }
        private void injectionMouldingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.NextForm = "IMSpecificationMain";
            this.Close();
        }
        private void machineToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            int imID = -1;
            int rowIndex = -1;
            if (dgvEdit.CurrentRow.Cells["ImID"].Value != null)
            {
                imID = (int)dgvEdit.CurrentRow.Cells["ImID"].Value;
                rowIndex = dgvEdit.CurrentRow.Index;
            }
            UpdateGrid();
            Machine f = new Machine();
            f.ShowDialog();
            DoLoad();
            SetupCols();
            if (imID > -1) LocateGridRow("ImID", imID);
        }
        private void injectionMouldToolStripMenuItem_Click(object sender, EventArgs e)
        {                                  
        }
        private void injectionMouldPriceDifferenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
        private void materialTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            int imID = -1;
            int rowIndex = -1;
            if (dgvEdit.CurrentRow.Cells["ImID"].Value != null)
            {
                imID = (int)dgvEdit.CurrentRow.Cells["ImID"].Value;
                rowIndex = dgvEdit.CurrentRow.Index;
            }
            UpdateGrid();
            MaterialTypeForm f = new MaterialTypeForm();
            f.ShowDialog();
            DoLoad();
            SetupCols();
            if (imID > -1) LocateGridRow("ImID", imID);            
        }
        private void materialGradeToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            int imID = -1;
            int rowIndex = -1;
            if (dgvEdit.CurrentRow.Cells["ImID"].Value != null)
            {
                imID = (int)dgvEdit.CurrentRow.Cells["ImID"].Value;
                rowIndex = dgvEdit.CurrentRow.Index;
            }
            UpdateGrid();
            MaterialGradeForm f = new MaterialGradeForm();
            f.ShowDialog();
            DoLoad();
            SetupCols();
            if (imID > -1) LocateGridRow("ImID", imID);
        }
        private void masterbatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }
        private void cartonsToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            int imID = -1;
            int rowIndex = -1;
            if (dgvEdit.CurrentRow.Cells["ImID"].Value != null)
            {
                imID = (int)dgvEdit.CurrentRow.Cells["ImID"].Value;
                rowIndex = dgvEdit.CurrentRow.Index;
            }
            UpdateGrid();
            CartonPackaging f = new CartonPackaging();
            f.ShowDialog();
            DoLoad();
            SetupCols();
            if (imID > -1) LocateGridRow("ImID", imID);
        }
        private void palletsToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            int imID = -1;
            int rowIndex = -1;
            if (dgvEdit.CurrentRow.Cells["ImID"].Value != null)
            {
                imID = (int)dgvEdit.CurrentRow.Cells["ImID"].Value;
                rowIndex = dgvEdit.CurrentRow.Index;
            }
            UpdateGrid();
            PalletForm f = new PalletForm();
            f.ShowDialog();
            DoLoad();
            SetupCols();
            if (imID > -1) LocateGridRow("ImID", imID);
        }
        private void masterbatchToolStripMenuItem1_Click(object sender, EventArgs e)
        {            
            int imID = -1;
            int rowIndex = -1;
            if (dgvEdit.CurrentRow.Cells["ImID"].Value != null)
            {
                imID = (int)dgvEdit.CurrentRow.Cells["ImID"].Value;
                rowIndex = dgvEdit.CurrentRow.Index;
            }
            UpdateGrid();
            MasterBatchForm f = new MasterBatchForm();
            f.ShowDialog();
            DoLoad();
            SetupCols();
            if (imID > -1) LocateGridRow("ImID", imID);            
        }
        private void additiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //AdditiveForm f = new AdditiveForm();
            //f.ShowDialog();
            int imID = -1;
            int rowIndex = -1;
            if (dgvEdit.CurrentRow.Cells["ImID"].Value != null)
            {
                imID = (int)dgvEdit.CurrentRow.Cells["ImID"].Value;
                rowIndex = dgvEdit.CurrentRow.Index;
            }
            UpdateGrid();
            AdditiveForm f = new AdditiveForm();
            f.ShowDialog();
            DoLoad();
            SetupCols();
            if (rowIndex > -1)
            {
                dgvEdit.CurrentCell = dgvEdit.Rows[rowIndex].Cells["Code"];
                dgvEdit.Rows[dgvEdit.CurrentCell.RowIndex].Selected = true;
                dgvEdit.FirstDisplayedScrollingRowIndex = rowIndex;
            }
        }       
        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int imID = -1;
            int rowIndex = -1;
            if (dgvEdit.CurrentRow.Cells["ImID"].Value != null)
            {
                imID = (int)dgvEdit.CurrentRow.Cells["ImID"].Value;
                rowIndex = dgvEdit.CurrentRow.Index;
            }
            UpdateGrid();
            ReportPrompt f = new ReportPrompt(imID);
            f.ShowDialog();
            DoLoad();
            SetupCols();
            if (imID > -1) LocateGridRow("ImID", imID);
        }
        private void productSpecificationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void packagingInjectionMouldingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void productCostCalculationInjectionMouldingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void cPPriceQuantityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
        private void dgvEdit_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;
            cname = dgvEdit.Columns[e.ColumnIndex].Name;
            if (cname == "Image" || cname == "PackingImage1" || cname == "PackingImage2" || cname == "PackingImage3" ||
                cname == "AssemblyImage1" || cname == "AssemblyImage2" || cname == "AssemblyImage3" || cname == "AssemblyImage4" ||
                cname == "QCImage1" || cname == "QCImage2" || cname == "QCImage3" || cname == "QCImage4" ||
                cname == "AssemblyImage5" || cname == "AssemblyImage6" || cname == "Field1" || cname == "Field2")
            {
                e.ContextMenuStrip = stripImage;
                stripImage.Items[0].Enabled = (dgvEdit.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Length > 0);
                stripImage.Items[2].Enabled = (dgvEdit.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Length > 0);
                stripImage.Items[3].Enabled = (dgvEdit.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Length > 0);
                stripImage.Items[5].Enabled = (stripImage.Items[2].Enabled || curImagePath != null);
                stripImage.Items[6].Enabled = (stripImage.Items[2].Enabled || curImagePath != null);
                stripImage.AutoClose = false;
            }
            else
            {
                if (stripImage.Visible)
                {
                    stripImage.Close();
                }
                e.ContextMenuStrip = contextMenuStrip1;
                contextMenuStrip1.Items[2].Enabled = (e.RowIndex != dgvEdit.Rows.Count - 1);
            }            
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
            catch(Exception ex)
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
            // MessageBox.Show("todo:  save/replace link");
            dgvEdit.CurrentCell.Value = curImagePath;
            stripImage.Items[0].Enabled = true;
            stripImage.Items[2].Enabled = true;
            stripImage.Items[3].Enabled = true;
            stripImage.Items[5].Enabled = true;
            stripImage.Items[6].Enabled = true;
            //dataGridView1.CurrentCell.ContextMenuStrip = strip;            
            //strip.Show();
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
        private void closeMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stripImage.Close();
        }
        private void dgvEdit_DoubleClick(object sender, EventArgs e)
        {
            DoDoubleClick();
        }
        private void DoDoubleClick()
        {
            if (dgvEdit.CurrentRow.Index == -1 || dgvEdit.CurrentCell.ColumnIndex == -1)
                return;
            //string cname = dgvEdit.CurrentCell.OwningColumn.Name;
            if (cname == "Image" || cname == "PackingImage1" || cname == "PackingImage2" || cname == "PackingImage3" ||
                cname == "QCImage1" || cname == "QCImage2" || cname == "QCImage3" || cname == "QCImage4" ||
                cname == "AssemblyImage1" || cname == "AssemblyImage2" || cname == "AssemblyImage3" || cname == "AssemblyImage4" ||
                cname == "AssemblyImage5" || cname == "AssemblyImage6" || cname == "Field1" || cname == "Field2")
            {
                //MessageBox.Show("todo:  open file");
                string fileName = dgvEdit.CurrentCell.Value.ToString();
                if (fileName.Length > 0)
                {
                    try
                    {
                        ProcessStartInfo pi = new ProcessStartInfo(fileName);
                        pi.Arguments = Path.GetFileName(fileName);
                        pi.UseShellExecute = true;
                        pi.WorkingDirectory = Path.GetDirectoryName(fileName);
                        pi.FileName = fileName;
                        pi.Verb = "OPEN";
                        Process.Start(pi);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            else
            {
                //MessageBox.Show("todo: edit row detail");
                int imID = -1;
                int rowIndex = -1;
                if (dgvEdit.CurrentRow.Cells["ImID"].Value != null &&
                    dgvEdit.CurrentRow.Cells["ImID"].Value != DBNull.Value &&
                    String.IsNullOrWhiteSpace(dgvEdit.CurrentRow.Cells["ImID"].Value.ToString()) != true
                   )
                {
                    imID = (int)dgvEdit.CurrentRow.Cells["ImID"].Value;
                }
                rowIndex = dgvEdit.CurrentRow.Index;
                DataTable dtCopy = transposeDataRow(imID);
                if (EditRow(ref dtCopy, imID))
                {
                    if (imID == -1)
                    {
                        int newImID = UpdateNewGridRow();
                        DoLoad();
                        SetupCols();
                        if (newImID > -1) LocateGridRow("ImID", newImID);
                    }
                }
            }
        }
        private void dgvEdit_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {            
            if (e.ColumnIndex != -1 && e.RowIndex != -1 && e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                DataGridViewCell c = (sender as DataGridView)[e.ColumnIndex, e.RowIndex];
                string cname = c.OwningColumn.Name;
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
        private void IMSpecificationMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            UpdateGrid();
        }
        private void UpdateGrid()
        {
            try
            {
                //bClosing = true;                
                if (dgvEdit.IsCurrentRowDirty)
                {
                    this.Validate();
                }
                dgvEdit.EndEdit();
                dgvEdit.DataSource = null;
                DataService.ProductDataService pds = new DataService.ProductDataService();
                pds.UpdateIMSpecification(IMSpecificationDS);
            }
            catch
            {
                throw;
            }
        }
        private int UpdateNewGridRow()
        {
            try
            {
                int newImID = -1;
                if (dgvEdit.IsCurrentRowDirty)
                {
                    this.Validate();
                }
                dgvEdit.EndEdit();
                dgvEdit.DataSource = null;
                DataService.ProductDataService pds = new DataService.ProductDataService();
                pds.UpdateIMSpecification(IMSpecificationDS);
                newImID = pds.LastAdded.ImID;
                return newImID;
            }
            catch
            {
                throw;
            }
        }
        private void productCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            int imID = -1;
            int rowIndex = -1;
            if (dgvEdit.CurrentRow.Cells["ImID"].Value != null)
            {
                imID = (int)dgvEdit.CurrentRow.Cells["ImID"].Value;
                rowIndex = dgvEdit.CurrentRow.Index;
            }
            UpdateGrid();
            ProductGradeForm f = new ProductGradeForm();
            f.ShowDialog();
            DoLoad();
            SetupCols();
            if (imID > -1) LocateGridRow("ImID", imID);
        }
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {            
            int imID = -1;
            int rowIndex = -1;
            if (dgvEdit.CurrentRow.Cells["ImID"].Value != null)
            {
                imID = (int)dgvEdit.CurrentRow.Cells["ImID"].Value;
                rowIndex = dgvEdit.CurrentRow.Index;
            }
            UpdateGrid();
            EditPastelMaster f = new EditPastelMaster();
            f.ShowDialog();
            DoLoad();
            SetupCols();
            if (imID > -1) LocateGridRow("ImID", imID);
        }
        private void priceQuantityToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            int imID = -1;
            int rowIndex = -1;
            if (dgvEdit.CurrentRow.Cells["ImID"].Value != null)
            {
                imID = (int)dgvEdit.CurrentRow.Cells["ImID"].Value;
                rowIndex = dgvEdit.CurrentRow.Index;
            }
            UpdateGrid();
            CPPriceQuantity f = new CPPriceQuantity();
            f.ShowDialog();
            DoLoad();
            SetupCols();
            if (imID > -1) LocateGridRow("ImID", imID);
        }
        private DataTable transposeDataRow(int imID)
        {
            try
            {
                DataTable dtCopy = new DataTable();

                //create first column containing column names
                DataColumn column;
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "Column";
                dtCopy.Columns.Add(column);

                // Create second column for editing values.
                column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.ColumnName = "Value";
                dtCopy.Columns.Add(column);

                //Create third (hidden) column containing hidden column names
                column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.ColumnName = "HiddenName";
                dtCopy.Columns.Add(column);

                //Create 4th (hidden) column containing hidden values, to be populated by comboboxes
                column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.ColumnName = "HiddenValue";
                dtCopy.Columns.Add(column);

                //position to current row bound to datagrid
                DataView dv = new DataView(IMSpecificationDS.Tables[0]);
                if (imID > -1)
                    dv.RowFilter = ("ImID = " + imID.ToString());
                else
                {
                    DataRow row = IMSpecificationDS.Tables[0].NewRow();
                    row["ImID"] = -1;
                    IMSpecificationDS.Tables[0].Rows.Add(row);
                }
                //populate columns, excluding invisible ones
                GridColumns thisCol;
                for (int i = 0; i < dv.Table.Columns.Count; i++)
                {
                    if (dgcols.TryGetValue(dv.Table.Columns[i].ColumnName, out thisCol))
                    {
                        if (thisCol.Group != "Hidden")
                        {
                            DataRow dr = dtCopy.NewRow();
                            dr[0] = dv.Table.Columns[i].ColumnName;
                            if (imID > -1)
                            {
                                dr[1] = dv[0][i];

                                //populate hidden vaLues
                                if (dv.Table.Columns[i].ColumnName == "ProductCode")
                                {
                                    //dgvEdit.Rows[gridRow].Cells["HiddenValue"].Value = pmID.ToString();
                                    //dgvEdit.Rows[gridRow].Cells["HiddenName"].Value = "PmID";
                                    dr[2] = "PmID";
                                    dr[3] = dv[0]["PmID"].ToString();
                                }
                                else if (dv.Table.Columns[i].ColumnName == "Description")
                                {
                                    //dgvEdit.Rows[gridRow].Cells["HiddenValue"].Value = dv[0]["MaterialID"].ToString(); //(int)dv[0]["MaterialID"]; // this is same as Polymer1TypeID - required 
                                    //dgvEdit.Rows[gridRow].Cells["HiddenName"].Value = "MaterialID";            // for compatibility with labelling system  
                                    dr[2] = "MaterialID";
                                    dr[3] = dv[0]["MaterialID"].ToString();
                                }
                                else if (dv.Table.Columns[i].ColumnName == "Material" )
                                {
                                    //dgvEdit.Rows[gridRow].Cells["HiddenValue"].Value = dv[0]["MaterialID"].ToString(); // (int)dv[0]["Polymer1TypeID"];
                                    //dgvEdit.Rows[gridRow].Cells["HiddenName"].Value = "Polymer1TypeID";
                                    dr[2] = "Polymer1TypeID";
                                    dr[3] = dv[0]["PolymerTypeID"].ToString();
                                }
                                else if (dv.Table.Columns[i].ColumnName == "ProductCategory")
                                {
                                    //dgvEdit.Rows[gridRow].Cells["Value"].Value = dv[0]["ProductCategory"].ToString().Trim();
                                    //dgvEdit.Rows[gridRow].Cells["HiddenValue"].Value = dv[0]["GradeID"].ToString(); // (int)dv[0]["GradeID"];
                                    //dgvEdit.Rows[gridRow].Cells["HiddenName"].Value = "GradeID";
                                    dr[2] = "GradeID";
                                    dr[3] = dv[0]["GradeID"].ToString();
                                }
                                else if (dv.Table.Columns[i].ColumnName == "MachineA")
                                {
                                    //dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["HiddenValue"].Value = mID.ToString();
                                    //dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["HiddenName"].Value = "MachineAID";
                                    //dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Value"].Value = cboMachine.Text;
                                    dr[2] = "MachineAID";
                                    dr[3] = dv[0]["MachineAID"].ToString();
                                }
                                else if (dv.Table.Columns[i].ColumnName == "MachineB")
                                {
                                    //dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["HiddenValue"].Value = mID.ToString();
                                    //dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["HiddenName"].Value = "MachineAID";
                                    //dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Value"].Value = cboMachine.Text;
                                    dr[2] = "MachineBID";
                                    dr[3] = dv[0]["MachineBID"].ToString();
                                }
                                else if (dv.Table.Columns[i].ColumnName == "MachineC")
                                {
                                    //dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["HiddenValue"].Value = mID.ToString();
                                    //dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["HiddenName"].Value = "MachineAID";
                                    //dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Value"].Value = cboMachine.Text;
                                    dr[2] = "MachineCID";
                                    dr[3] = dv[0]["MachineCID"].ToString();
                                }
                                else if (dv.Table.Columns[i].ColumnName == "ProductCategory")
                                {
                                    //dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["HiddenValue"].Value = pgID.ToString(); //["GradeID"].Value = pgID;
                                    //dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["HiddenName"].Value = "GradeID";
                                    //dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Value"].Value = cboProductGrade.Text; //["ProductCategory"].Value = cboProductGrade.Text;
                                    dr[2] = "GradeID";
                                    dr[3] = dv[0]["GradeID"].ToString();
                                }
                                else if (dv.Table.Columns[i].ColumnName == "Customer")
                                {
                                    //dgvEdit.CurrentRow.Cells["Value"].Value = cboCustomer.Text;
                                    //dgvEdit.CurrentRow.Cells["HiddenName"].Value = "CustID";
                                    //dgvEdit.CurrentRow.Cells["HiddenValue"].Value = custid.ToString();
                                    dr[2] = "CustID";
                                    dr[3] = dv[0]["CustID"].ToString();
                                }
                                else if (dv.Table.Columns[i].ColumnName == "AdditiveCode")
                                {
                                    //dgvEdit.Rows[irow].Cells["HiddenValue"].Value = aID.ToString(); //"AdditiveACodeID"
                                    //dgvEdit.Rows[irow].Cells["HiddenName"].Value = "AdditiveACodeID";
                                    //dgvEdit.Rows[irow].Cells["Value"].Value = row[0]["AdditiveCode"].ToString();   //cboAdditiveCode.Text;
                                    dr[2] = "AdditiveACodeID";
                                    dr[3] = dv[0]["AdditiveACodeID"].ToString();
                                }
                                else if (dv.Table.Columns[i].ColumnName == "AdditiveBCode")
                                {
                                    //dgvEdit.Rows[irow].Cells["HiddenName"].Value = "AdditiveBCodeID";
                                    //dgvEdit.Rows[irow].Cells["Value"].Value = row[0]["AdditiveCode"].ToString();   //cboAdditiveCode.Text;
                                    dr[2] = "AdditiveBCodeID";
                                    dr[3] = dv[0]["AdditiveBCodeID"].ToString();
                                }
                                else if (dv.Table.Columns[i].ColumnName == "MBColourA")
                                {
                                    //dgvEdit.Rows[irow].Cells["HiddenName"].Value = "MBID";
                                    //dgvEdit.Rows[irow].Cells["HiddenValue"].Value = mbid.ToString();
                                    dr[2] = "MBID";
                                    dr[3] = dv[0]["MBID"].ToString();
                                }
                                else if (dv.Table.Columns[i].ColumnName == "MBColourB")
                                {
                                    //dgvEdit.Rows[irow].Cells["HiddenName"].Value = "MBID";
                                    //dgvEdit.Rows[irow].Cells["HiddenValue"].Value = mbid.ToString();
                                    dr[2] = "MBBID";
                                    dr[3] = dv[0]["MBBID"].ToString();
                                }
                                else if (dv.Table.Columns[i].ColumnName == "Polymer1Grade")
                                {
                                    //dgvEdit.Rows[irow].Cells["HiddenName"].Value = "Polymer1GradeID";
                                    //dgvEdit.Rows[irow].Cells["HiddenValue"].Value = MID;
                                    dr[2] = "Polymer1GradeID";
                                    dr[3] = dv[0]["Polymer1GradeID"].ToString();
                                }
                                else if (dv.Table.Columns[i].ColumnName == "Polymer1Type")
                                {
                                    //dgvEdit.Rows[irow].Cells["HiddenName"].Value = "Polymer1GradeID";
                                    //dgvEdit.Rows[irow].Cells["HiddenValue"].Value = MID;
                                    dr[2] = "Polymer1TypeID";
                                    dr[3] = dv[0]["Polymer1TypeID"].ToString();
                                }
                                else if (dv.Table.Columns[i].ColumnName == "Polymer2Grade")
                                {
                                    //dgvEdit.Rows[irow].Cells["HiddenName"].Value = "Polymer1GradeID";
                                    //dgvEdit.Rows[irow].Cells["HiddenValue"].Value = MID;
                                    dr[2] = "Polymer2GradeID";
                                    dr[3] = dv[0]["Polymer2GradeID"].ToString();
                                }
                                else if (dv.Table.Columns[i].ColumnName == "Polymer2Type")
                                {
                                    //dgvEdit.Rows[irow].Cells["HiddenName"].Value = "Polymer1GradeID";
                                    //dgvEdit.Rows[irow].Cells["HiddenValue"].Value = MID;
                                    dr[2] = "Polymer2TypeID";
                                    dr[3] = dv[0]["Polymer2TypeID"].ToString();
                                }
                                else if (dv.Table.Columns[i].ColumnName == "CartonType")
                                {
                                    //dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["HiddenValue"].Value = ctnID.ToString();
                                    //dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["HiddenName"].Value = "CtnID";
                                    dr[2] = "CtnID";
                                    dr[3] = dv[0]["CtnID"].ToString();
                                }
                                
                            }
                            
                            else
                            {
                                dr[1] = DBNull.Value;
                                dr.SetField(2, DBNull.Value);
                                dr.SetField(3, DBNull.Value);
                            }
                            dtCopy.Rows.Add(dr);
                        }
                    }
                }

                return dtCopy;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        private void TransferEdits(DataTable dtEdit, int imID)
        {
            try
            {
                //variable.GetType().Name
                GridColumns thisCol;
                GridColumns hiddenCol;
                DataRow dr = null;
                DateTime dateValue;
                //bool bResult;
                Single snglResult;
                int intResult;
                DataView dv = new DataView(IMSpecificationDS.Tables[0]);
                dv.RowFilter = ("ImID = " + imID.ToString());
                if (dv.ToTable().Rows.Count > 0)
                {                    
                    for (int i = 0; i < dtEdit.Rows.Count; i++)
                    {
                        dr = dtEdit.Rows[i];
                        if (dgcols.TryGetValue(dr[0].ToString(), out thisCol))
                        {
                            if (!thisCol.ReadOnly)
                            {
                                //Transfer column values:-
                                if (thisCol.DataType == "int")
                                {
                                    if (int.TryParse(dr[1].ToString(), out intResult))
                                        dv[0][thisCol.ColumnName] = intResult; // Convert.ToInt32(dr[1].ToString());}
                                }
                                else if (thisCol.DataType == "real")
                                {
                                    if (Single.TryParse(dr[1].ToString(), out snglResult))
                                        dv[0][thisCol.ColumnName] = snglResult; // Convert.ToSingle(dr[1].ToString());
                                }
                                else if (thisCol.DataType == "bit")
                                {
                                    if (int.TryParse(dr[1].ToString(), out intResult))
                                        dv[0][thisCol.ColumnName] = Convert.ToBoolean(intResult);
                                }
                                else if (thisCol.DataType.ToString().IndexOf("char") == 0)
                                {
                                    dv[0][thisCol.ColumnName] = dr[1].ToString();
                                }
                                else if (thisCol.DataType.ToString().IndexOf("varchar") == 0)
                                {
                                    dv[0][thisCol.ColumnName] = dr[1].ToString();
                                }
                                else if (thisCol.DataType.ToString().IndexOf("datetime2") == 0)
                                {
                                    if (DateTime.TryParse(dr[1].ToString(), out dateValue))
                                        dv[0][thisCol.ColumnName] = dateValue; //Convert.ToDateTime(dr[1].ToString());  
                                }
                                //Transfer associated hidden column values, if they exist:-
                                if (dr[2] != DBNull.Value)
                                {
                                    if (dgcols.TryGetValue(dr[2].ToString(), out hiddenCol))
                                    {
                                        if (hiddenCol.DataType == "int")
                                        {
                                            if (int.TryParse(dr[3].ToString(), out intResult))
                                                dv[0][hiddenCol.ColumnName] = intResult; // Convert.ToInt32(dr[1].ToString());}
                                        }
                                        else if (hiddenCol.DataType == "real")
                                        {
                                            if (Single.TryParse(dr[3].ToString(), out snglResult))
                                                dv[0][hiddenCol.ColumnName] = snglResult; // Convert.ToSingle(dr[1].ToString());
                                        }
                                        else if (hiddenCol.DataType == "bit")
                                        {
                                            if (int.TryParse(dr[3].ToString(), out intResult))
                                                dv[0][hiddenCol.ColumnName] = Convert.ToBoolean(intResult);
                                        }
                                        else if (hiddenCol.DataType.ToString().IndexOf("char") == 0)
                                        {
                                            dv[0][hiddenCol.ColumnName] = dr[3].ToString();
                                        }
                                        else if (hiddenCol.DataType.ToString().IndexOf("varchar") == 0)
                                        {
                                            dv[0][hiddenCol.ColumnName] = dr[3].ToString();
                                        }
                                        else if (hiddenCol.DataType.ToString().IndexOf("datetime2") == 0)
                                        {
                                            if (DateTime.TryParse(dr[3].ToString(), out dateValue))
                                                dv[0][hiddenCol.ColumnName] = dateValue; //Convert.ToDateTime(dr[1].ToString());  
                                        }
                                    }

                                }
                            }                            
                        }
                    }                    
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private bool EditRow(ref DataTable dtCopy, int imID)
        {
            try
            {
                bool bresult = false;

                //Show the edit form
                EditRowDetail f = new EditRowDetail();
                f.EditDT = dtCopy;
                f.dgcols = dgcols;
                f.ShowDialog();
                if (f.DialogResult != DialogResult.No && f.DialogResult != DialogResult.None && f.DialogResult != DialogResult.Cancel)
                {
                    TransferEdits(dtCopy, imID);
                    bresult = true;
                }
                //if (f.DialogResult == DialogResult.Cancel) f.Dispose();
                f.Dispose();
                return bresult;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }   
        private void LocateGridRow(string findCol, int iFindNo)
        {
            try
            {
                int iRowIndex = -1;
                for (int i = 0; i < Convert.ToInt32(dgvEdit.Rows.Count); i++)
                {
                    if (dgvEdit.Rows[i].Cells[findCol].Value.ToString().Trim().Length > 0)
                    {
                        if (Convert.ToInt32(dgvEdit.Rows[i].Cells[findCol].Value) == iFindNo)
                        {
                            iRowIndex = i;
                            break;
                        }
                    }
                }
                if (iRowIndex > -1)
                {
                    dgvEdit.CurrentCell = dgvEdit.Rows[iRowIndex].Cells["Code"];
                    dgvEdit.Rows[dgvEdit.CurrentCell.RowIndex].Selected = true;
                    dgvEdit.FirstDisplayedScrollingRowIndex = iRowIndex;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ToDO:  CPCustomer");
            return;
            //int imID = -1;
            //int rowIndex = -1;
            //if (dgvEdit.CurrentRow.Cells["ImID"].Value != null)
            //{
            //    imID = (int)dgvEdit.CurrentRow.Cells["ImID"].Value;
            //    rowIndex = dgvEdit.CurrentRow.Index;
            //}
            //UpdateGrid();
            //CPCustomer f = new CPCustomer();
            //f.ShowDialog();
            //DoLoad();
            //SetupCols();
            //if (imID > -1) LocateGridRow("ImID", imID);
        }
        private void productNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int imID = -1;
            int rowIndex = -1;
            if (dgvEdit.CurrentRow.Cells["ImID"].Value != null)
            {
                imID = (int)dgvEdit.CurrentRow.Cells["ImID"].Value;
                rowIndex = dgvEdit.CurrentRow.Index;
            }
            UpdateGrid();
            EditPastelMaster f = new EditPastelMaster();
            f.DoEdit();
            //f.ShowDialog();
            f.Dispose();
            DoLoad();
            SetupCols();
            if (imID > -1) LocateGridRow("ImID", imID);
        }
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            //add new row and edit in 2-column mode
            int imID = -1;
            DataTable dtCopy = transposeDataRow(imID);
            if(EditRow(ref dtCopy, imID))
            {
                int newImID = UpdateNewGridRow();
                DoLoad();
                SetupCols();
                if (newImID > -1) LocateGridRow("ImID", newImID);
            }                     
        }
        private void editExistingRowToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            DoDoubleClick();
        }
        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            //delete row
            if (!dgvEdit.CurrentRow.IsNewRow)
            {
                DialogResult response = MessageBox.Show("Are you sure?", "Delete row?",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question,
                                 MessageBoxDefaultButton.Button2);

                if (response == DialogResult.Yes)
                    dgvEdit.Rows.Remove(dgvEdit.Rows[dgvEdit.CurrentRow.Index]);
            }
        }
    }
}
