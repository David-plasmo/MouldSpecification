using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace MouldSpecification
{
    public partial class MachinePref : Form
    {
        DataSet dsMachinePref, dsMachineIndex;
        Size screenRes = DrawingUtils.ScreenRes();

        int? curItemID = null;
        int? curCustID = null;

        public MachinePref(int? itemID, int? custID)
        {
            InitializeComponent();
            if (itemID != null) { curItemID = itemID; }
            if (custID != null) { curCustID = custID; }

            this.curCustID = curCustID;
        }
        public MachinePref()
        {
            
        }

        private void RefreshGrid()
        {
            try
            {
                dgvEdit.AllowUserToAddRows = false;
                MachinePrefDAL dal = new MachinePrefDAL();
                dsMachinePref = dal.SelectMachinePref(curItemID, curCustID);
                if (curItemID != null)
                {
                    DataTable dtMP = dsMachinePref.Tables[0];
                    dtMP.Columns["ItemID"].DefaultValue = curItemID;
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
                dgvEdit.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                dgvEdit.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
                dgvEdit.RowHeadersWidth = Convert.ToInt32(26 * screenRes.Width / 96);

                dgvEdit.DataSource = dsMachinePref.Tables[0];
                dgvEdit.Columns["MachPrefID"].Visible = false;
                dgvEdit.Columns["ItemID"].Visible = false;
                dgvEdit.Columns["last_updated_by"].Visible = false;
                dgvEdit.Columns["last_updated_on"].Visible = false;//
                dgvEdit.Columns["MachineABC"].HeaderText = "Preference";
                dgvEdit.Columns["ProgramNo"].HeaderText = "Program No.";
                dgvEdit.Columns["NoPartsPerHour"].HeaderText = "Parts/hr";
                dgvEdit.Columns["CycleTime"].HeaderText = "Cycle Time";

                dgvEdit.Columns["ProgramNo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvEdit.Columns["NoPartsPerHour"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvEdit.Columns["CycleTime"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvEdit.Columns["ProgramNo"].Width = 120;
                dgvEdit.Columns["NoPartsPerHour"].Width = 120;
                dgvEdit.Columns["CycleTime"].Width = 120;

                //setup dropdown columns for code and description
                DataGridViewComboBoxColumn cbcCode = new DataGridViewComboBoxColumn();
                //DataSet dsManItemIndex = new MainFormDAL().SelectMAN_ItemIndex(null);
                DataSet dsManItemIndex = new MainFormDAL().SelectMAN_ItemIndex();
                DataTable dt = dsManItemIndex.Tables[0];
                cbcCode.DataSource = dt;
                cbcCode.ValueMember = "ItemID";
                cbcCode.DisplayMember = "ITEMNMBR";
                cbcCode.Name = "Code";
                cbcCode.DataPropertyName = "ItemID";
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

                //setup dropdown column for Machine Preference 
                DataGridViewComboBoxColumn cbcMachineABC = new DataGridViewComboBoxColumn();
                DataSet dsMachineABC = dal.GetMachineABC();
                dt = dsMachineABC.Tables[0];
                cbcMachineABC.ValueMember = "Preference";
                cbcMachineABC.DisplayMember = "Preference";
                cbcMachineABC.DataSource = dt;
                cbcMachineABC.Name = "MachineABC";
                cbcMachineABC.DataPropertyName = "MachineABC";
                dgvEdit.Columns.Remove("MachineABC");
                dgvEdit.Columns.Insert(4, cbcMachineABC);
                dgvEdit.Columns["MachineABC"].HeaderText = "Machine Preference";
                dgvEdit.Columns["MachineABC"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                cbcMachineABC.DisplayStyleForCurrentCellOnly = true;
                cbcMachineABC.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;

                //setup dropdown column for Machine 
                DataGridViewComboBoxColumn cbcMachine = new DataGridViewComboBoxColumn();
                DataSet dsMachineIndex = dal.GetMachineIndex();
                dt = dsMachineIndex.Tables[0];
                cbcMachine.DataSource = dt;
                cbcMachine.ValueMember = "MachineID";
                cbcMachine.DisplayMember = "Machine";
                cbcMachine.Name = "Machine";
                cbcMachine.DataPropertyName = "MachineID";
                dgvEdit.Columns.Remove("MachineID");
                dgvEdit.Columns.Insert(5, cbcMachine);
                dgvEdit.Columns["Machine"].HeaderText = "Machine";
                cbcMachine.DisplayStyleForCurrentCellOnly = true;
                cbcMachine.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;

                dgvEdit.EditingControlShowing +=
                    new DataGridViewEditingControlShowingEventHandler(dgvEdit_EditingControlShowing);

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
                if (dsMachinePref != null)
                {
                    if (dgvEdit.IsCurrentRowDirty)
                    {
                        this.Validate();
                    }
                    dgvEdit.EndEdit();
                    //MainFormDAL dal = new MainFormDAL();
                    new MachinePrefDAL().UpdateMachinePref(dsMachinePref);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MachinePref_Shown(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void MachinePref_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveGrid();
        }

        private void dgvEdit_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            if (dgvEdit.CurrentCell.ColumnIndex == dgvEdit.Columns["ProgramNo"].Index) //Desired Column
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
