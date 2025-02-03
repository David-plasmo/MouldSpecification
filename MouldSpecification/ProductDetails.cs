using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static Utils.DrawingUtils;

namespace MouldSpecification
{
    public partial class ProductDetails : Form
    {
        DataSet dsMAN_Item;
        BindingManagerBase bindingManager;
        DataGridViewImageCell currentBrowserCell;
        int? curItemID = null;
        int? curCustID = null;
        Size screenRes = ScreenRes();

        public ProductDetails(int? itemID, int? custID)
        {
            InitializeComponent();
            if (itemID != null) { curItemID = itemID; }
            if (custID != null) { curCustID = custID; }
        }

        public ProductDetails()
        {
            
        }

        private void RefreshGrid()
        {
            try
            {
                ProductDetailsDAL dal = new ProductDetailsDAL();
                dsMAN_Item = dal.SelectMAN_Item(curItemID, curCustID);
                dsMAN_Item.Tables[0].Columns["CompDB"].DefaultValue = "CP";

                DataGridViewCellStyle style = dgvEdit.ColumnHeadersDefaultCellStyle;
                style.BackColor = Color.Navy;
                style.ForeColor = Color.White;
                style.Font = new Font(dgvEdit.Font, FontStyle.Bold);
                dgvEdit.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                dgvEdit.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                dgvEdit.GridColor = SystemColors.ActiveBorder;
                dgvEdit.EnableHeadersVisualStyles = false;
                dgvEdit.AutoGenerateColumns = true;
                dgvEdit.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
                dgvEdit.DataSource = dsMAN_Item.Tables[0];
                dgvEdit.Columns["ItemID"].Visible = false;
                try
                {
                    dgvEdit.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                }
                catch { }

                //dgvEdit.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                //dgvEdit.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                //dgvEdit.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
                dgvEdit.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells);
                dgvEdit.RowHeadersWidth = Convert.ToInt32(26 * screenRes.Width / 96);
                dgvEdit.Columns["ITEMNMBR"].HeaderText = "Code";
                dgvEdit.Columns["ITEMDESC"].HeaderText = "Description";
                dgvEdit.Columns["ImageFile"].HeaderText = "Product Image";
                dgvEdit.Columns["SpecificationFile"].HeaderText = "Product Sheet";
                //dgvEdit.Columns["DangerousGood"].HeaderText = "Dangerous Good";

                DataGridViewComboBoxColumn cbcCompany = new DataGridViewComboBoxColumn();
                cbcCompany.Name = "CompDB";
                cbcCompany.DataPropertyName = "CompDB";
                dgvEdit.Columns.Remove("CompDB");
                dgvEdit.Columns.Insert(3, cbcCompany);
                dgvEdit.Columns["CompDB"].HeaderText = "Company";
                DataSet ds = dal.GetCompany();
                cbcCompany.ValueMember = "CompanyCode";
                cbcCompany.DisplayMember = "CompanyCode";
                cbcCompany.DisplayStyleForCurrentCellOnly = true;
                cbcCompany.DataSource = ds.Tables[0];

                DataGridViewComboBoxColumn cbcItemClass = new DataGridViewComboBoxColumn();
                cbcItemClass.Name = "ITMCLSCD";
                cbcItemClass.DataPropertyName = "ITMCLSCD";
                dgvEdit.Columns.Remove("ITMCLSCD");
                dgvEdit.Columns.Insert(4, cbcItemClass);
                dgvEdit.Columns["ITMCLSCD"].HeaderText = "ItemClass";
                ds = dal.GetItemClassByCompany(null);
                cbcItemClass.ValueMember = "ITMCLSCD";
                cbcItemClass.DisplayMember = "ITMCLSCD";
                cbcItemClass.DisplayStyleForCurrentCellOnly = true;
                cbcItemClass.DataSource = ds.Tables[0];

                /* specific to blowmould
                DataGridViewComboBoxColumn cbcCtnSize = new DataGridViewComboBoxColumn();
                cbcCtnSize.Name = "CartonID";
                cbcCtnSize.DataPropertyName = "CartonID";
                dgvEdit.Columns.Remove("CartonID");
                dgvEdit.Columns.Insert(4, cbcCtnSize);
                dgvEdit.Columns["CartonID"].HeaderText = "CtnSize";
                ds = dal.GetCartonSizes();
                cbcCtnSize.ValueMember = "CartonID";
                cbcCtnSize.DisplayMember = "CartonSize";
                cbcCtnSize.DisplayStyleForCurrentCellOnly = true;
                cbcCtnSize.DataSource = ds.Tables[0];
                */

                DataGridViewComboBoxColumn cbcProductGrade = new DataGridViewComboBoxColumn();
                cbcProductGrade.Name = "GradeID";
                cbcProductGrade.DataPropertyName = "GradeID";
                dgvEdit.Columns.Remove("GradeID");
                dgvEdit.Columns.Insert(10, cbcProductGrade);
                dgvEdit.Columns["GradeID"].HeaderText = "Product Grade";
                ds = dal.GetProductGrade();
                cbcProductGrade.ValueMember = "GradeID";
                cbcProductGrade.DisplayMember = "Description";
                cbcProductGrade.DisplayStyleForCurrentCellOnly = true;
                cbcProductGrade.DataSource = ds.Tables[0];

                /*specific to label printing
                DataGridViewComboBoxColumn cbcLabelType = new DataGridViewComboBoxColumn();
                cbcLabelType.Name = "LabelTypeID";
                cbcLabelType.DataPropertyName = "LabelTypeID";
                dgvEdit.Columns.Remove("LabelTypeID");
                dgvEdit.Columns.Insert(11, cbcLabelType);
                dgvEdit.Columns["LabelTypeID"].HeaderText = "Label Type";
                ds = dal.GetLabelTypes();
                cbcLabelType.ValueMember = "LabelTypeID";
                cbcLabelType.DisplayMember = "Description";
                cbcLabelType.DisplayStyleForCurrentCellOnly = true;
                cbcLabelType.DataSource = ds.Tables[0];
                */

                /*specific to blowmould
                DataGridViewComboBoxColumn cbcBottleSize = new DataGridViewComboBoxColumn();
                cbcBottleSize.Name = "BottleSize";
                cbcBottleSize.DataPropertyName = "BottleSize";
                dgvEdit.Columns.Remove("BottleSize");
                dgvEdit.Columns.Insert(11, cbcBottleSize);
                dgvEdit.Columns["BottleSize"].HeaderText = "BottleSize";
                ds = dal.LookupUSCATVLS(1);
                cbcBottleSize.ValueMember = "USCATVAL";
                cbcBottleSize.DisplayMember = "USCATVAL";
                cbcBottleSize.DisplayStyleForCurrentCellOnly = true;
                cbcBottleSize.DataSource = ds.Tables[0];


                DataGridViewComboBoxColumn cbcStyle = new DataGridViewComboBoxColumn();
                cbcStyle.Name = "Style";
                cbcStyle.DataPropertyName = "Style";
                dgvEdit.Columns.Remove("Style");
                dgvEdit.Columns.Insert(12, cbcStyle);
                dgvEdit.Columns["Style"].HeaderText = "Style";
                ds = dal.LookupUSCATVLS(2);
                cbcStyle.ValueMember = "USCATVAL";
                cbcStyle.DisplayMember = "USCATVAL";
                cbcStyle.DisplayStyleForCurrentCellOnly = true;
                cbcStyle.DataSource = ds.Tables[0];

                DataGridViewComboBoxColumn cbcNeckSize = new DataGridViewComboBoxColumn();
                cbcNeckSize.Name = "NeckSize";
                cbcNeckSize.DataPropertyName = "NeckSize";
                dgvEdit.Columns.Remove("NeckSize");
                dgvEdit.Columns.Insert(13, cbcNeckSize);
                dgvEdit.Columns["NeckSize"].HeaderText = "NeckSize";
                ds = dal.LookupUSCATVLS(3);
                cbcNeckSize.ValueMember = "USCATVAL";
                cbcNeckSize.DisplayMember = "USCATVAL";
                cbcNeckSize.DisplayStyleForCurrentCellOnly = true;
                cbcNeckSize.DataSource = ds.Tables[0];

                DataGridViewComboBoxColumn cbcColour = new DataGridViewComboBoxColumn();
                cbcColour.Name = "Colour";
                cbcColour.DataPropertyName = "Colour";
                dgvEdit.Columns.Remove("Colour");
                dgvEdit.Columns.Insert(14, cbcColour);
                dgvEdit.Columns["Colour"].HeaderText = "Colour";
                ds = dal.LookupUSCATVLS(4);
                cbcColour.ValueMember = "USCATVAL";
                cbcColour.DisplayMember = "USCATVAL";
                cbcColour.DisplayStyleForCurrentCellOnly = true;
                cbcColour.DataSource = ds.Tables[0];
                */

                //set button columns to show file browser prompt button
                DataGridViewImageColumn ic = new DataGridViewImageColumn();
                ic.Name = "FilePrompt1";
                ic.DataPropertyName = "FilePrompt1";
                ic.Width = Convert.ToInt32(26 * screenRes.Width / 96);
                ic.Image = new Bitmap(1, 1);
                dgvEdit.Columns.Insert(8, ic);
                dgvEdit.Columns["FilePrompt1"].HeaderText = "";
                dgvEdit.Columns["FilePrompt1"].DefaultCellStyle.NullValue = null;

                ic = new DataGridViewImageColumn();
                ic.Name = "FilePrompt2";
                ic.DataPropertyName = "FilePrompt2";
                ic.Width = Convert.ToInt32(26 * screenRes.Width / 96);
                ic.Image = new Bitmap(1, 1);
                dgvEdit.Columns.Insert(11, ic);
                dgvEdit.Columns["FilePrompt2"].HeaderText = "";
                dgvEdit.Columns["FilePrompt2"].DefaultCellStyle.NullValue = null;

                //set file browsing fields to show hand cursor hyperlink style
                dgvEdit.Columns["SpecificationFile"].DefaultCellStyle = GetHyperLinkStyleForGridCell();
                //dgvEdit.Columns["SpecificationFile"].Width = 100;
                ((DataGridViewTextBoxColumn)dgvEdit.Columns["SpecificationFile"]).MaxInputLength = 200;

                dgvEdit.Columns["ImageFile"].DefaultCellStyle = GetHyperLinkStyleForGridCell();
                //dgvEdit.Columns["ImageFile"].Width = 100;
                ((DataGridViewTextBoxColumn)dgvEdit.Columns["ImageFile"]).MaxInputLength = 200;

                dgvEdit.CellMouseEnter += new DataGridViewCellEventHandler(dgvEdit_CellMouseEnter);


                bindingManager = this.BindingContext[dgvEdit.DataSource];
                bindingManager.PositionChanged += new System.EventHandler(RowChanged);
                RowChanged(dgvEdit, EventArgs.Empty);

                UseWaitCursor = false;
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvEdit_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                return;
            }
            if ((dgvEdit.Columns[e.ColumnIndex].Name == "SpecificationFile"
                || dgvEdit.Columns[e.ColumnIndex].Name == "ImageFile")
                && dgvEdit.Columns[e.ColumnIndex].ToString().Length > 0)
            {
                dgvEdit.Cursor = Cursors.Hand;
            }
            else
            {
                dgvEdit.Cursor = Cursors.Default;
            }
        }

        private DataGridViewCellStyle GetHyperLinkStyleForGridCell()
        {
            DataGridViewCellStyle l_objDGVCS = new DataGridViewCellStyle();
            System.Drawing.Font l_objFont = new System.Drawing.Font(FontFamily.GenericSansSerif, 8, FontStyle.Underline);
            l_objDGVCS.Font = l_objFont;
            l_objDGVCS.ForeColor = Color.Blue;
            return l_objDGVCS;
        }

        private void RowChanged(object sender, System.EventArgs e)
        {
            try
            {
                //Console.WriteLine("RowChanged " + bindingManager.Position.ToString());                                   
                bool newRow = bindingManager.Count > ((DataTable)dgvEdit.DataSource).Rows.Count;
                bool rowOK = false;
                foreach (DataGridViewRow row in dgvEdit.Rows)
                {
                    rowOK = false;
                    DataGridViewImageCell ic = (DataGridViewImageCell)dgvEdit.Rows[row.Index].Cells["FilePrompt1"];
                    ic.Value = new Bitmap(1, 1);
                    ic = (DataGridViewImageCell)dgvEdit.Rows[row.Index].Cells["FilePrompt2"];
                    ic.Value = new Bitmap(1, 1);

                    if (!newRow && row.Index == bindingManager.Position)
                    {
                        rowOK = true;
                    }
                    else if (newRow && row.Index == bindingManager.Position)
                    {
                        //rowOK = false;
                        rowOK = true;
                    }
                    if (rowOK)
                    {
                        ic = (DataGridViewImageCell)dgvEdit.Rows[bindingManager.Position].Cells["FilePrompt1"];
                        ic.Tag = ButtonOp.Browse;
                        Size size = ic.Size;
                        ic.Value = GetImage((ButtonOp)ic.Tag, size.Width, size.Height - 2);
                        ic = (DataGridViewImageCell)dgvEdit.Rows[bindingManager.Position].Cells["FilePrompt2"];
                        ic.Tag = ButtonOp.Browse;
                        size = ic.Size;
                        ic.Value = GetImage((ButtonOp)ic.Tag, size.Width, size.Height - 2);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveGrid()
        {
            try
            {
                if (dsMAN_Item != null)
                {
                    if (dgvEdit.IsCurrentRowDirty)
                    {
                        this.Validate();
                    }
                    dgvEdit.EndEdit();
                    //MainFormDAL dal = new MainFormDAL();
                    new ProductDetailsDAL().UpdateMAN_Item(dsMAN_Item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ProductDetails_Shown(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void ProductDetails_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveGrid();
        }

        private void dgvEdit_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Confirm Delete?", "Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                int itemID = (int)dgvEdit.CurrentRow.Cells["ItemID"].Value;
                string dependencies = new ProductDetailsDAL().CheckDependencies(itemID);
                if (dependencies.Length != 0)
                {
                    if (MessageBox.Show(dependencies, "Are you Sure?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        new ProductDetailsDAL().DeleteProductDetails(itemID);
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                else
                    e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void dgvEdit_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dgvEdit_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                //MessageBox.Show(e.RowIndex.ToString() + ", " + dgvEdit.Rows.Count.ToString());
                dgvEdit.Rows[e.RowIndex].Cells["FilePrompt1"].Value = null;
                dgvEdit.Rows[e.RowIndex].Cells["FilePrompt2"].Value = null;
            }
            catch
            { }
        }

        private void ProductDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dgvEdit.IsCurrentRowDirty)
            {
                this.Validate();
            }
            dgvEdit.EndEdit();
            MainFormDAL dal = new MainFormDAL();
            //if (dal.HasCodeDuplicate(dsMAN_Item))
            //    e.Cancel = true;
        }

        private void dgvEdit_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["CompDB"].Value = "CP";
        }

        private void dgvEdit_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && dgvEdit.Columns[e.ColumnIndex].Name == "FilePrompt1") //DGV_ImageColumn
                {
                    //if (dgvDetail != null && dgvDetail.Visible)
                    //    CollapseDetail();
                    DataGridViewImageCell ic = (DataGridViewImageCell)dgvEdit.CurrentCell;
                    currentBrowserCell = ic;
                    string filePath = string.Empty;
                    OpenFileDialog ofd = new OpenFileDialog();

                    //set the opening directory
                    var appSettings = ConfigurationManager.AppSettings;
                    string initialDir = appSettings["QCImageFolderDir"];
                    ofd.InitialDirectory = initialDir;
                    if (!ofd.CheckPathExists)
                    {
                        MessageBox.Show("Initial folder not found");
                        ofd.InitialDirectory = "C:\\";
                    }
                    ofd.Filter = "jpg files (*.jpg)|*.jpg|pdf files (*.pdf)|*.pdf|txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    ofd.FilterIndex = 1;
                    ofd.RestoreDirectory = true;
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        //Get the path of specified file
                        filePath = ofd.FileName;
                        dgvEdit.CurrentRow.Cells["ImageFile"].Value = filePath;
                    }
                }
                if (e.ColumnIndex >= 0 && dgvEdit.Columns[e.ColumnIndex].Name == "FilePrompt2") //DGV_ImageColumn
                {
                    //if (dgvDetail != null && dgvDetail.Visible)
                    //    CollapseDetail();
                    DataGridViewImageCell ic = (DataGridViewImageCell)dgvEdit.CurrentCell;
                    currentBrowserCell = ic;
                    string filePath = string.Empty;
                    OpenFileDialog ofd = new OpenFileDialog();

                    //set the opening directory
                    var appSettings = ConfigurationManager.AppSettings;
                    string initialDir = appSettings["QCImageFolderDir"];
                    ofd.InitialDirectory = initialDir;
                    if (!ofd.CheckPathExists)
                    {
                        MessageBox.Show("Initial folder not found");
                        ofd.InitialDirectory = "C:\\";
                    }

                    ofd.InitialDirectory = "c:\\";
                    ofd.Filter = "pdf files (*.pdf)|*.pdf|txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    ofd.FilterIndex = 1;
                    ofd.RestoreDirectory = true;
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        //Get the path of specified file
                        filePath = ofd.FileName;
                        dgvEdit.CurrentRow.Cells["SpecificationFile"].Value = filePath;
                    }
                }
                if (e.ColumnIndex >= 0 && dgvEdit.Columns[e.ColumnIndex].Name == "SpecificationFile") //DGV_ImageColumn
                {
                    if (!String.IsNullOrWhiteSpace(dgvEdit.CurrentCell.Value.ToString()))
                    {
                        System.Diagnostics.Process.Start(dgvEdit.CurrentCell.Value.ToString());
                    }
                }
                if (e.ColumnIndex >= 0 && dgvEdit.Columns[e.ColumnIndex].Name == "ImageFile")
                {
                    if (!String.IsNullOrWhiteSpace(dgvEdit.CurrentCell.EditedFormattedValue.ToString()))
                    {
                        System.Diagnostics.Process.Start(dgvEdit.CurrentCell.EditedFormattedValue.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvEdit_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //*** Not valid for a ComboBoxColumn when datasource has been set
            //if (e.Control.GetType() == typeof(DataGridViewComboBoxEditingControl))
            //{
            //    DataGridViewComboBoxEditingControl cbo = e.Control as DataGridViewComboBoxEditingControl;
            //    cbo.DropDownStyle = ComboBoxStyle.DropDown;
            //    cbo.Validating += new CancelEventHandler(cbo_Validating);
            //}

            // set up handler for allowing only integer input
            if (dgvEdit.CurrentCell.ColumnIndex == dgvEdit.Columns["CtnQty"].Index) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    // Remove an existing event-handler, if present, to avoid 
                    // adding multiple handlers when the editing control is reused.
                    tb.KeyPress -= new KeyPressEventHandler(IntControl_KeyPress);

                    // Add the event handler
                    tb.KeyPress += new KeyPressEventHandler(IntControl_KeyPress);
                }
            }
        }

        private void cbo_Validating(object sender, CancelEventArgs e)
        {
            //*** Not valid for a ComboBoxColumn when datasource has been set.
            //DataGridViewComboBoxEditingControl cbo = sender as DataGridViewComboBoxEditingControl;
            //DataGridView grid = cbo.EditingControlDataGridView;
            //object value = cbo.Text;

            //// Add value to list if not there
            //if (cbo.Items.IndexOf(value) == -1)
            //{
            //    DataGridViewComboBoxColumn cboCol = grid.Columns[grid.CurrentCell.ColumnIndex] as DataGridViewComboBoxColumn;
            //    // Must add to both the current combobox as well as the template, to avoid duplicate entries...
            //    cbo.Items.Add(value);
            //    cboCol.Items.Add(value);
            //    grid.CurrentCell.Value = value;
            //}
        }


        private void IntControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dgvEdit_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "ITEMNMBR")
            {
                //check ITEMNMBR is unique
                if (dgvEdit.Columns[e.ColumnIndex].DataPropertyName == "ITEMNMBR"
                    && e.FormattedValue.ToString() != dgvEdit.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString())
                {
                    DataTable dt = (DataTable)dgvEdit.DataSource;
                    DataRow[] rows = dt.Select("ITEMNMBR = '" + e.FormattedValue + "'");

                    if (rows.Length > 0)
                    {
                        MessageBox.Show("This Product code is already used.");
                        e.Cancel = true;
                    }
                }
            }
        }
    }
}
