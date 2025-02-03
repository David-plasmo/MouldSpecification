using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace MouldSpecification
{
    public partial class MaterialComp : Form
    {
        //MainFormDAL dal;
        MaterialCompDAL dal;
        DataSet dsMaterialComp, dsPolymerGrade;
        int curPolymerNo = 0;
        int? curItemID = null;
        int? curCustID = null;
        ComboBox cboPolymer;
        Size screenRes = DrawingUtils.ScreenRes();

        public MaterialComp(int? itemID, int? custID)
        {
            InitializeComponent();
            if (itemID != null) { curItemID = itemID; }
            if (custID != null) { curCustID = custID; }
        }

        public MaterialComp()
        {
            
        }

        private void MaterialComp_Load(object sender, EventArgs e)
        {
            dal = new MaterialCompDAL();
            dsPolymerGrade = dal.SelectMaterialGradeByType();
            DataTable dt = dsPolymerGrade.Tables[0];
            cboPolymer = new ComboBox();
            cboPolymer.DisplayMember = "DisplayValue";
            cboPolymer.ValueMember = "MaterialGradeID";
            cboPolymer.DataSource = dt;
            cboPolymer.SelectedIndexChanged += new EventHandler(cboPolymer_SelectedIndexChanged);
        }

        private void RefreshGrid()
        {
            try
            {
                dgvEdit.AllowUserToAddRows = false;
                dsMaterialComp = dal.SelectMaterialComp(curItemID, curCustID);
                if (curItemID != null)
                {
                    DataTable dtMC = dsMaterialComp.Tables[0];
                    dtMC.Columns["ItemID"].DefaultValue = curItemID;
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
                dgvEdit.DataSource = dsMaterialComp.Tables[0];
                dgvEdit.Columns["MaterialCompID"].Visible = false;
                dgvEdit.Columns["ItemID"].Visible = false;
                dgvEdit.Columns["MaterialGradeID"].Visible = false;
                dgvEdit.Columns["last_updated_by"].Visible = false;
                dgvEdit.Columns["last_updated_on"].Visible = false;
                dgvEdit.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                dgvEdit.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
                dgvEdit.RowHeadersWidth = Convert.ToInt32(26 * screenRes.Width / 96);

                //setup dropdown columns for code and description
                DataGridViewComboBoxColumn cbcCode = new DataGridViewComboBoxColumn();
                DataSet dsManItemIndex = dal.SelectMAN_ItemIndex();
                DataTable dt = dsManItemIndex.Tables[0];
                cbcCode.DataSource = dt;
                cbcCode.ValueMember = "ItemID";
                cbcCode.DisplayMember = "ITEMNMBR";
                cbcCode.DataPropertyName = "ItemID";
                cbcCode.Name = "Code";
                dgvEdit.Columns.Insert(2, cbcCode);
                dgvEdit.Columns["Code"].HeaderText = "Code";
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

                dgvEdit.Columns["Code"].ReadOnly = true;
                dgvEdit.Columns["Description"].ReadOnly = true;
                dgvEdit.Columns["Code"].DefaultCellStyle.BackColor = Color.LightGray;
                dgvEdit.Columns["Description"].DefaultCellStyle.BackColor = Color.LightGray;

                //Set dropdown column for polymer #
                DataGridViewComboBoxColumn cbcPolymerNo = new DataGridViewComboBoxColumn();
                DataSet ds = dal.GetPolymer123();
                dt = ds.Tables[0];
                cbcPolymerNo.DataSource = dt;
                cbcPolymerNo.ValueMember = "PolymerNo";
                cbcPolymerNo.DisplayMember = "PolymerNo";
                cbcPolymerNo.Name = "Polymer123";
                cbcPolymerNo.DataPropertyName = "Polymer123";
                dgvEdit.Columns.Remove("Polymer123");
                dgvEdit.Columns.Insert(4, cbcPolymerNo);
                dgvEdit.Columns["Polymer123"].HeaderText = "Polymer No.";
                cbcPolymerNo.DisplayStyleForCurrentCellOnly = true;
                cbcPolymerNo.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;

                //Set dropdown column for Polymer
                DataGridViewComboBoxColumn cbcPolymer = new DataGridViewComboBoxColumn();
                dt = dsPolymerGrade.Tables[0];
                cbcPolymer.DataSource = dt;
                cbcPolymer.ValueMember = "MaterialGradeID";
                cbcPolymer.DisplayMember = "Description";
                cbcPolymer.Name = "Material";
                cbcPolymer.DataPropertyName = "MaterialGradeID";
                dgvEdit.Columns.Insert(6, cbcPolymer);
                dgvEdit.Columns["Material"].HeaderText = "Material";
                dgvEdit.Columns["Material"].ReadOnly = true;
                cbcPolymer.DisplayStyleForCurrentCellOnly = true;
                cbcPolymer.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

                //Set dropdown column for polymer Grade
                DataGridViewComboBoxColumn cbcPolymerGrade = new DataGridViewComboBoxColumn();
                cbcPolymerGrade.DataSource = dt;
                cbcPolymerGrade.ValueMember = "MaterialGradeID";
                cbcPolymerGrade.DisplayMember = "MaterialGrade";
                cbcPolymerGrade.Name = "Grade";
                cbcPolymerGrade.DataPropertyName = "MaterialGradeID";
                dgvEdit.Columns.Insert(7, cbcPolymerGrade);
                dgvEdit.Columns["Grade"].HeaderText = "Grade";
                dgvEdit.Columns["Grade"].ReadOnly = true;
                cbcPolymerGrade.DisplayStyleForCurrentCellOnly = true;
                cbcPolymerGrade.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

                dgvEdit.Columns["PolymerPercent"].HeaderText = "Polymer %";
                dgvEdit.Columns["PolymerPercent"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvEdit.Columns["PolymerPercent"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvEdit.Columns["RegrindMaxPC"].HeaderText = "Regrind Max %";
                dgvEdit.Columns["RegrindMaxPC"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvEdit.Columns["RegrindMaxPC"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvEdit.Columns["Polymer123"].HeaderText = "Polymer No.";
                dgvEdit.Columns["Polymer123"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEdit.Columns["Polymer123"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvEdit.EditingControlShowing +=
                   new DataGridViewEditingControlShowingEventHandler(dgvEdit_EditingControlShowing);

                if (dgvEdit.Rows.Count > 0)
                    dgvEdit.Rows[0].Cells["Polymer123"].Selected = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvEdit_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            if (dgvEdit.CurrentCell.ColumnIndex == dgvEdit.Columns["PolymerPercent"].Index) //Desired Column
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

        private void IntControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dgvEdit_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            curPolymerNo = curPolymerNo + 1;
            e.Row.Cells["Polymer123"].Value = curPolymerNo;
        }

        private void dgvEdit_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && dgvEdit.Columns[e.ColumnIndex].Name == "Material"
                    || e.ColumnIndex >= 0 && dgvEdit.Columns[e.ColumnIndex].Name == "Grade")
                {
                    //MessageBox.Show("todo:  handle Material dropdown");
                    ComboBox cbo = cboPolymer;
                    cbo.Font = new Font("Courier New", 10);
                    dgvEdit.Controls.Add(cbo);
                    cbo.DropDownClosed -= cbo_DropDownClosed;
                    int matCol = dgvEdit.Columns["Material"].Index;
                    Rectangle rect = dgvEdit.GetCellDisplayRectangle(matCol, e.RowIndex, true);
                    cbo.Location = new Point(rect.Left, rect.Bottom);
                    cbo.Width = Convert.ToInt32(500 * screenRes.Width / 96);  //500;
                    cbo.Text = "MATERIAL                      GRADE";
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

        private void cboPolymer_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("todo:  handle cbo_Click");
            //MessageBox.Show("cboSelectedValue" + cboPolymer.SelectedValue.ToString());
            int mgID = (int)cboPolymer.SelectedValue;
            dgvEdit.CurrentRow.Cells["MaterialGradeID"].Value = mgID;
            dgvEdit.CurrentRow.Cells["Material"].Value = mgID;
            dgvEdit.CurrentRow.Cells["Grade"].Value = mgID;
            this.Validate();
            dgvEdit.EndEdit();

            cboPolymer.Visible = false;
        }

        private void cbo_DropDownClosed(object sender, EventArgs e)
        {
            cboPolymer.Visible = false;
        }

        private void SaveGrid()
        {
            try
            {
                if (dsMaterialComp != null)
                {
                    if (dgvEdit.IsCurrentRowDirty)
                    {
                        this.Validate();
                    }
                    dgvEdit.EndEdit();
                    //MainFormDAL dal = new MainFormDAL();
                    dal.UpdateMaterialComp(dsMaterialComp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MaterialComp_Shown(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void MaterialComp_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveGrid();
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

        private void dgvEdit_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
    }
}
