using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace MouldSpecification
{
    public partial class MasterBatchComp : Form
    {
        DataSet dsMasterBatchComp, dsMasterBatchDropDown;
        MainFormDAL dal;
        private DataGridView dgvEdit = new DataGridView();
        private ComboBox cboMasterBatch = new ComboBox();
        bool nonNumberEntered = false;
        int? curItemID = null;
        int? curCustID = null;
        Size screenRes = DrawingUtils.ScreenRes();

        public MasterBatchComp(int? itemID, int? custID)
        {
            this.ClientSize = new System.Drawing.Size(1702, 858);
            this.dgvEdit.Dock = DockStyle.Fill;
            this.Controls.Add(this.dgvEdit);

            this.dgvEdit.DataError += dgvEdit_DataError;
            this.dgvEdit.CellClick += DgvEdit_CellClick;
            dgvEdit.RowHeadersWidth = Convert.ToInt32(26 * screenRes.Width / 96);
            this.Load += MasterBatchComp_Load;
            this.Shown += MasterBatchComp_Shown;
            this.FormClosed += MasterBatchComp_FormClosed;

            if (itemID != null) { curItemID = itemID; }
            if (custID != null) { curCustID = custID; }
        }

        public MasterBatchComp()
        {
            
        }

        private void MasterBatchComp_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveGrid();
        }

        private void MasterBatchComp_FormClosed1(object sender, FormClosedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MasterBatchComp_Load(object sender, EventArgs e)
        {
            this.Text = "MasterBatch Composition";
            dal = new MainFormDAL();
            dsMasterBatchDropDown = new MasterBatchCompDAL().SelectMasterBatchByColour();
            DataTable dt = dsMasterBatchDropDown.Tables[0];
            cboMasterBatch = new ComboBox();
            cboMasterBatch.DisplayMember = "DisplayValue";
            cboMasterBatch.ValueMember = "MBID";
            cboMasterBatch.DataSource = dt;
            cboMasterBatch.SelectedIndexChanged += new EventHandler(cboMasterBatch_SelectedIndexChanged);
        }

        private void DgvEdit_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && dgvEdit.Columns[e.ColumnIndex].Name == "Colour"
                    || e.ColumnIndex >= 0 && dgvEdit.Columns[e.ColumnIndex].Name == "MBCode") //DGV_ImageColumn
                {
                    //MessageBox.Show("todo:  handle Material dropdown");
                    ComboBox cbo = cboMasterBatch;
                    cbo.DropDownClosed -= cbo_DropDownClosed;
                    cbo.Font = new Font("Courier New", 10);
                    dgvEdit.Controls.Add(cbo);
                    int mbColourCol = dgvEdit.Columns["Colour"].Index;
                    Rectangle rect = dgvEdit.GetCellDisplayRectangle(mbColourCol, e.RowIndex, true);
                    cbo.Location = new Point(rect.Left, rect.Bottom);
                    cbo.Width = Convert.ToInt32(400 * screenRes.Width / 96);  //500;
                    cbo.Text = "COLOUR                    CODE";
                    cbo.Visible = true;
                    cbo.DroppedDown = true;
                    cbo.DropDownClosed += cbo_DropDownClosed;
                    //cboPolymer.BringToFront();
                    //cbo.SelectedValueChanged += new EventHandler(cbo_SelectedValueChanged);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbo_DropDownClosed(object sender, EventArgs e)
        {
            //int mbID = (int)dgvEdit.CurrentRow.Cells["MBID"].Value;
            //dgvEdit.CurrentRow.Cells["MBID"].Value = mbID;
            //dgvEdit.CurrentRow.Cells["MBID"].Value = mbID;
            //dgvEdit.CurrentRow.Cells["Colour"].Value = mbID;
            //dgvEdit.CurrentRow.Cells["Code"].Value = mbID;

            cboMasterBatch.Visible = false;
        }

        private void DgvEdit_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void MasterBatchComp_Shown(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void cboMasterBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("todo:  handle cbo_Click");
            //MessageBox.Show("cboSelectedValue" + cboPolymer.SelectedValue.ToString());
            int mbID = (int)cboMasterBatch.SelectedValue;
            dgvEdit.CurrentRow.Cells["MBID"].Value = mbID;
            dgvEdit.CurrentRow.Cells["Colour"].Value = mbID;
            this.Validate();
            dgvEdit.EndEdit();
            dgvEdit.CurrentRow.Cells["MBCode"].Value = mbID;

            cboMasterBatch.Visible = false;
        }

        private void RefreshGrid()
        {
            try
            {
                //MainFormDAL dal = new MainFormDAL();
                dgvEdit.AllowUserToAddRows = false;
                dsMasterBatchComp = new MasterBatchCompDAL().SelectMasterBatchComp(curItemID, curCustID);
                if (curItemID != null)
                {
                    DataTable dtMBC = dsMasterBatchComp.Tables[0];
                    dtMBC.Columns["ItemID"].DefaultValue = curItemID;
                    dgvEdit.AllowUserToAddRows = true;
                }
                DataGridViewCellStyle style = dgvEdit.ColumnHeadersDefaultCellStyle;
                style.BackColor = Color.Navy;
                style.ForeColor = Color.White;
                style.Font = new Font(dgvEdit.Font, FontStyle.Bold);
                dgvEdit.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                dgvEdit.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                dgvEdit.GridColor = SystemColors.ActiveBorder;
                dgvEdit.EnableHeadersVisualStyles = false;
                dgvEdit.AutoGenerateColumns = true;
                dgvEdit.DataSource = dsMasterBatchComp.Tables[0];
                dgvEdit.Columns["MBID"].Visible = false;
                dgvEdit.Columns["MBCompID"].Visible = false;
                dgvEdit.Columns["ItemID"].Visible = false;
                dgvEdit.Columns["last_updated_by"].Visible = false;
                dgvEdit.Columns["last_updated_on"].Visible = false;
                dgvEdit.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                dgvEdit.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
                dgvEdit.RowHeadersWidth = Convert.ToInt32(26 * screenRes.Width / 96);

                //setup dropdown columns for code and description
                DataGridViewComboBoxColumn cbcCode = new DataGridViewComboBoxColumn();
                //DataSet dsManItemIndex = dal.SelectMAN_ItemIndex(null);
                DataSet dsManItemIndex = dal.SelectMAN_ItemIndex();
                DataTable dt = dsManItemIndex.Tables[0];
                cbcCode.DataSource = dt;
                cbcCode.ValueMember = "ItemID";
                cbcCode.DisplayMember = "ITEMNMBR";
                cbcCode.Name = "ITEMNMBR";
                cbcCode.DataPropertyName = "ItemID";
                dgvEdit.Columns.Insert(2, cbcCode);
                dgvEdit.Columns["ITEMNMBR"].HeaderText = "Code";
                cbcCode.DisplayStyleForCurrentCellOnly = true;
                cbcCode.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

                DataGridViewComboBoxColumn cbcDescription = new DataGridViewComboBoxColumn();
                cbcDescription.DataSource = dt;
                cbcDescription.ValueMember = "ItemID";
                cbcDescription.DisplayMember = "ITEMDESC";
                cbcDescription.Name = "Description";
                cbcDescription.DataPropertyName = "ItemID";
                dgvEdit.Columns.Insert(3, cbcDescription);
                dgvEdit.Columns["Description"].HeaderText = "Description";
                cbcDescription.DisplayStyleForCurrentCellOnly = true;
                cbcDescription.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

                dgvEdit.Columns["ITEMNMBR"].ReadOnly = true;
                dgvEdit.Columns["Description"].ReadOnly = true;
                dgvEdit.Columns["ITEMNMBR"].DefaultCellStyle.BackColor = Color.LightGray;
                dgvEdit.Columns["Description"].DefaultCellStyle.BackColor = Color.LightGray;

                //Set dropdown column for MasterBatch 1 to 3
                DataGridViewComboBoxColumn cbcMB123 = new DataGridViewComboBoxColumn();
                DataSet ds = dal.GetMB123();
                dt = ds.Tables[0];
                cbcMB123.DataSource = dt;
                cbcMB123.ValueMember = "MasterBatch";
                cbcMB123.DisplayMember = "MasterBatch";
                cbcMB123.Name = "MB123";
                cbcMB123.DataPropertyName = "MB123";
                dgvEdit.Columns.Remove("MB123");
                dgvEdit.Columns.Insert(5, cbcMB123);
                dgvEdit.Columns["MB123"].HeaderText = "Mb No.";
                cbcMB123.DisplayStyleForCurrentCellOnly = true;
                cbcMB123.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;

                //Set dropdown column for Masterbatch Colour
                DataGridViewComboBoxColumn cbcMBColour = new DataGridViewComboBoxColumn();
                ds = new MasterBatchCompDAL().SelectMasterBatchByColour();
                dt = ds.Tables[0];
                cbcMBColour.DataSource = dt;
                cbcMBColour.ValueMember = "MBID";
                cbcMBColour.DisplayMember = "MBColour";
                cbcMBColour.Name = "Colour";
                cbcMBColour.DataPropertyName = "MBID";
                //dgvEdit.Columns.Remove("AdditiveID");
                dgvEdit.Columns.Insert(6, cbcMBColour);
                dgvEdit.Columns["Colour"].HeaderText = "Colour";
                dgvEdit.Columns["Colour"].ReadOnly = true;

                //load MasterBatch dropdown dataset for combo to show on datagrid click event for colour column
                dsMasterBatchDropDown = new MasterBatchCompDAL().SelectMasterBatchByColour();

                cbcMBColour.DisplayStyleForCurrentCellOnly = true;
                cbcMBColour.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

                //Set dropdown column for Masterbatch Code
                DataGridViewComboBoxColumn cbcMBCode = new DataGridViewComboBoxColumn();
                //ds = dal.SelectMasterBatchByColour();
                //dt = ds.Tables[0];
                cbcMBCode.DataSource = dt;
                cbcMBCode.ValueMember = "MBID";
                cbcMBCode.DisplayMember = "MBCode";
                cbcMBCode.Name = "MBCode";
                cbcMBCode.DataPropertyName = "MBID";
                //dgvEdit.Columns.Remove("AdditiveID");
                dgvEdit.Columns.Insert(7, cbcMBCode);
                dgvEdit.Columns["MBCode"].HeaderText = "MBCode";
                dgvEdit.Columns["ITEMNMBR"].ReadOnly = true;
                cbcMBCode.DisplayStyleForCurrentCellOnly = true;
                cbcMBCode.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;


                //Set dropdown column for Additive 
                DataGridViewComboBoxColumn cbcAdditive = new DataGridViewComboBoxColumn();
                ds = new MasterBatchCompDAL().GetAdditiveCode();
                dt = ds.Tables[0];
                cbcAdditive.DataSource = dt;
                cbcAdditive.ValueMember = "AdditiveID";
                cbcAdditive.DisplayMember = "AdditiveCode";
                cbcAdditive.Name = "AdditiveID";
                cbcAdditive.DataPropertyName = "AdditiveID";
                dgvEdit.Columns.Remove("AdditiveID");
                dgvEdit.Columns.Insert(10, cbcAdditive);
                dgvEdit.Columns["AdditiveID"].HeaderText = "Additive Code";
                cbcAdditive.DisplayStyleForCurrentCellOnly = true;
                cbcAdditive.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;

                dgvEdit.Columns["AdditivePC"].HeaderText = "Additive %";
                dgvEdit.Columns["MBPercent"].HeaderText = "MB %";
                dgvEdit.Columns["MBPercent"].DefaultCellStyle.Format = "N2";
                dgvEdit.Columns["AdditivePC"].DefaultCellStyle.Format = "N2";
                dgvEdit.Columns["AdditivePC"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvEdit.Columns["MBPercent"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //dgvEdit.Columns["Additive123"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEdit.Columns["MB123"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;



                dgvEdit.EditingControlShowing +=
                   new DataGridViewEditingControlShowingEventHandler(dgvEdit_EditingControlShowing);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvEdit_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            if (dgvEdit.CurrentCell.ColumnIndex == dgvEdit.Columns["AdditivePC"].Index
                || dgvEdit.CurrentCell.ColumnIndex == dgvEdit.Columns["MBPercent"].Index) //Desired Column
            {
                // setup editing for numerical input
                DataGridViewTextBoxEditingControl ec = (DataGridViewTextBoxEditingControl)e.Control;
                ec.KeyPress -= ec_KeyPress;
                ec.KeyPress += new KeyPressEventHandler(ec_KeyPress);
                ec.KeyDown -= ec_KeyDown;
                ec.KeyDown += new KeyEventHandler(ec_KeyDown);
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

        private void CmbBx_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var cmbBx = (DataGridViewComboBoxEditingControl)sender;
            //if (cmbBx.SelectedIndex == -1)
            //{
            //    dgvEdit.CurrentRow.Cells[dgvEdit.Columns["AdditiveID"].Index].Value = DBNull.Value;
            //}
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


        private void dgvEdit_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && dgvEdit.Columns[e.ColumnIndex].Name == "Material"
                    || e.ColumnIndex >= 0 && dgvEdit.Columns[e.ColumnIndex].Name == "Code") //DGV_ImageColumn
                {
                    //MessageBox.Show("todo:  handle Material dropdown");
                    ComboBox cbo = cboMasterBatch;
                    cbo.Font = new Font("Courier New", 10);
                    dgvEdit.Controls.Add(cbo);
                    int mcIndex = dgvEdit.Columns["Material"].Index;
                    Rectangle rect = dgvEdit.GetCellDisplayRectangle(mcIndex, e.RowIndex, true);
                    cbo.Location = new Point(rect.Left, rect.Bottom);
                    cbo.Width = 500;
                    cbo.Visible = true;
                    cbo.DroppedDown = true;
                    //cboPolymer.BringToFront();
                    //cbo.SelectedValueChanged += new EventHandler(cbo_SelectedValueChanged);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void IntControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void SaveGrid()
        {
            try
            {
                if (dsMasterBatchComp != null)
                {
                    if (dgvEdit.IsCurrentRowDirty)
                    {
                        this.Validate();
                    }
                    dgvEdit.EndEdit();
                    //MainFormDAL dal = new MainFormDAL();
                    new MasterBatchCompDAL().UpdateMasterBatchComp(dsMasterBatchComp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvEdit_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Confirm Delete?", "Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MasterBatchComp
            // 
            this.ClientSize = new System.Drawing.Size(274, 229);
            this.Name = "MasterBatchComp";
            this.ResumeLayout(false);

        }

        private void dgvEdit_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
    }
}
