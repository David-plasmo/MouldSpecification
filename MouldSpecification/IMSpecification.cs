using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static Utils.DrawingUtils;

namespace MouldSpecification
{
    public partial class IMSpecification : Form
    {
        DataSet dsIMSpecification;
        bool nonNumberEntered;
        int? curItemID = null;
        int? curCustID = null;
        Size screenRes = ScreenRes();

        public IMSpecification(int? itemID, int? custID)
        {
            InitializeComponent();
            if (itemID != null) { curItemID = itemID; }
            if (custID != null) { curCustID = custID; }
        }

        public IMSpecification()
        {            
        }

        private void RefreshGrid()
        {
            try
            {
                dgvEdit.AllowUserToAddRows = false;
                dgvEdit.EditingControlShowing -= dgvEdit_EditingControlShowing;
                dsIMSpecification = new IMSpecificationDAL().SelectIMSpecification(curItemID, curCustID);
                DataGridViewCellStyle style = dgvEdit.ColumnHeadersDefaultCellStyle;
                style.BackColor = Color.Navy;
                style.ForeColor = Color.White;
                style.Font = new Font(dgvEdit.Font, FontStyle.Bold);
                dgvEdit.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                dgvEdit.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                dgvEdit.GridColor = SystemColors.ActiveBorder;
                dgvEdit.EnableHeadersVisualStyles = false;
                dgvEdit.AutoGenerateColumns = true;
                dgvEdit.DataSource = dsIMSpecification.Tables[0];
                dgvEdit.Columns["MouldID"].Visible = false;
                dgvEdit.Columns["ItemID"].Visible = false;
                dgvEdit.Columns["last_updated_by"].Visible = false;
                dgvEdit.Columns["last_updated_on"].Visible = false;
                dgvEdit.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                dgvEdit.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
                dgvEdit.RowHeadersWidth = Convert.ToInt32(26 * screenRes.Width / 96);

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

                dgvEdit.Columns["MouldOwner"].HeaderText = "Mould Owner";
                dgvEdit.Columns["MouldLocation"].HeaderText = "Mould Location";
                dgvEdit.Columns["FamilyMould"].HeaderText = "Family Mould?";
                dgvEdit.Columns["NoOfCavities"].HeaderText = "No. of Cavities";
                dgvEdit.Columns["NoOfPart"].HeaderText = "No. of Part";
                dgvEdit.Columns["PartSummary"].HeaderText = "Part Summary";
                dgvEdit.Columns["Operation"].HeaderText = "Operation";
                dgvEdit.Columns["OtherFeatures"].HeaderText = "Other Features";
                dgvEdit.Columns["FixedHalf"].HeaderText = "Fixed Half";
                dgvEdit.Columns["FixedHalfTemp"].HeaderText = "Fixed Half Temp.";
                dgvEdit.Columns["MovingHalf"].HeaderText = "Moving Half";
                dgvEdit.Columns["MovingHalfTemp"].HeaderText = "Moving Half Temp.";
                //dgvEdit.Columns["ComponentWeight"].HeaderText = "Component Weight";
                //dgvEdit.Columns["SprueRunnerTotal"].HeaderText = "Sprue Runner Total";
                //dgvEdit.Columns["TotalShotWeight"].HeaderText = "Total Shot Weight";
                dgvEdit.Columns["PreMouldReq"].HeaderText = "Pre Mould Req";
                dgvEdit.Columns["PostMouldReq"].HeaderText = "Post Mould Req";

                dgvEdit.Columns["NoOfCavities"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvEdit.Columns["NoOfPart"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //dgvEdit.Columns["ComponentWeight"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //dgvEdit.Columns["SprueRunnerTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //dgvEdit.Columns["TotalShotWeight"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvEdit.Columns["NoOfCavities"].DefaultCellStyle.Format = "N0";
                dgvEdit.Columns["NoOfPart"].DefaultCellStyle.Format = "N0";
                //dgvEdit.Columns["SprueRunnerTotal"].DefaultCellStyle.Format = "N2";
                //dgvEdit.Columns["TotalShotWeight"].DefaultCellStyle.Format = "N2";

                dgvEdit.EditingControlShowing += dgvEdit_EditingControlShowing;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvEdit_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "NoOfCavities"
                || dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "NoOfPart"
                || dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "SprueRunnerTotal"
                || dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "TotalShotWeight")
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
        private void SaveGrid()
        {
            try
            {
                if (dsIMSpecification != null)
                {
                    if (dgvEdit.IsCurrentRowDirty)
                    {
                        this.Validate();
                    }
                    dgvEdit.EndEdit();
                    new IMSpecificationDAL().UpdateIMSpecification(dsIMSpecification);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void IMSpecification_Shown(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void IMSpecification_FormClosed(object sender, FormClosedEventArgs e)
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
