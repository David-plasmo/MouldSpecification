using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Utils;


namespace MouldSpecification
{
    public partial class MasterBatch : Form
    {
        bool nonNumberEntered;
        DataSet dsMasterBatch;
        DataGridViewComboBoxColumn cbcColour;
        Size screenRes = DrawingUtils.ScreenRes();

        public MasterBatch()
        {
            InitializeComponent();
        }

        private void MasterBatchForm_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            dsMasterBatch = new MasterBatchDAL().SelectMasterBatch();

            DataGridViewCellStyle style = dgvEdit.ColumnHeadersDefaultCellStyle;
            style.BackColor = Color.Navy;
            style.ForeColor = Color.White;
            style.Font = new Font(dgvEdit.Font, FontStyle.Bold);
            dgvEdit.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            dgvEdit.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgvEdit.GridColor = SystemColors.ActiveBorder;
            dgvEdit.EnableHeadersVisualStyles = false;
            dgvEdit.AutoGenerateColumns = true;
            //dgvEdit.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            //dgvEdit.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            //dgvEdit.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            dgvEdit.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvEdit.RowHeadersWidth = Convert.ToInt32(26 * screenRes.Width / 96);
            dgvEdit.AutoGenerateColumns = true;

            dgvEdit.DataSource = dsMasterBatch.Tables[0];
            dgvEdit.Columns[0].Visible = false;
            dgvEdit.Columns["last_updated_by"].Visible = false;
            dgvEdit.Columns["last_updated_on"].Visible = false;
            dgvEdit.Columns["CostPerKg"].HeaderText = "Cost/kg        $";
            dgvEdit.Columns["CostPerKg"].Width = 120;
            dgvEdit.Columns["Comment"].Width = 400;
            dgvEdit.Columns["CostPerKg"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvEdit.Columns["CostPerKg"].DefaultCellStyle.Format = "N3";

            //add combobox column for colour
            DataSet dsMBColour = new MasterBatchDAL().SelectMBColour();
            cbcColour = new DataGridViewComboBoxColumn();
            DataTable dt = dsMBColour.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                cbcColour.Items.Add(row["MBColour"].ToString().TrimEnd());
            }
            cbcColour.DataPropertyName = "MBColour";
            cbcColour.Name = "MBColour";
            dgvEdit.Columns.Remove("MBColour");
            dgvEdit.Columns.Insert(1, cbcColour);
            dgvEdit.Columns["MBColour"].HeaderText = "Colour";
            cbcColour.DisplayStyleForCurrentCellOnly = true;
            cbcColour.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;

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

        private void dgvEdit_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            //GridColumns thisCol;

            if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "CostPerKg")
            {
                // setup editing for numerical input  
                DataGridViewTextBoxEditingControl ec = (DataGridViewTextBoxEditingControl)e.Control;
                ec.KeyPress -= new KeyPressEventHandler(ec_KeyPress);
                ec.KeyPress += new KeyPressEventHandler(ec_KeyPress);
                ec.KeyDown -= ec_KeyDown;
                ec.KeyDown += ec_KeyDown;
                //}
            }
            if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "MBColour")
            {
                //your code....
                var cmbBx = e.Control as DataGridViewComboBoxEditingControl; // or your combobox control
                if (cmbBx != null)
                {
                    cmbBx.DropDown -= CmbBx_DropDown;
                    cmbBx.TextChanged -= CmbBx_TextChanged;
                    cmbBx.Click -= CmbBx_Click;

                    // Fix the black background on the drop down menu
                    e.CellStyle.BackColor = SystemColors.Control;

                    //setup an editable combobox
                    System.Windows.Forms.ComboBox c = e.Control as System.Windows.Forms.ComboBox;
                    if (c != null)
                    {
                        c.DropDownStyle = ComboBoxStyle.DropDown;
                    }

                    cmbBx.DropDown += CmbBx_DropDown;
                    cmbBx.TextChanged += CmbBx_TextChanged;
                    cmbBx.Click += CmbBx_Click;
                    cmbBx.Enter += CmbBx_Enter;

                }
            }
        }

        private void CmbBx_Enter(object sender, EventArgs e)
        {
            // Fix the black background on the drop down cell
            var cmbBx = (DataGridViewComboBoxEditingControl)sender; // or your combobox control
            cmbBx.BackColor = SystemColors.Control;
        }

        private void CmbBx_Click(object sender, EventArgs e)
        {
            // Fix the black background on the drop down cell
            var cmbBx = (DataGridViewComboBoxEditingControl)sender; // or your combobox control
            cmbBx.BackColor = SystemColors.Control;
        }

        private void CmbBx_TextChanged(object sender, EventArgs e)
        {
            // Fix the black background on the drop down cell
            var cmbBx = (DataGridViewComboBoxEditingControl)sender; // or your combobox control
            cmbBx.BackColor = SystemColors.Control;
        }

        private void CmbBx_DropDown(object sender, EventArgs e)
        {
            // Fix the black background on the drop down menu
            var cmbBx = (DataGridViewComboBoxEditingControl)sender; // or your combobox control
            cmbBx.BackColor = SystemColors.Control;
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

        private void MasterBatch_FormClosed(object sender, FormClosedEventArgs e)
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

                new MasterBatchDAL().UpdateMasterBatch(dsMasterBatch);

            }
            catch
            {
                throw;
            }
        }

        private void dgvEdit_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "MBColour")
            {
                object eFV = e.FormattedValue;
                DataGridViewComboBoxColumn cbc = cbcColour;
                if (!cbcColour.Items.Contains(eFV))
                {
                    cbc.Items.Add(eFV);
                    dgvEdit.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = eFV;
                }
            }
            if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "MBCode")
            {
                //check code is unique
                if (dgvEdit.Columns[e.ColumnIndex].DataPropertyName == "MBCode"
                    && e.FormattedValue.ToString() != dgvEdit.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString())
                {
                    DataTable dt = (DataTable)dgvEdit.DataSource;
                    DataRow[] rows = dt.Select("MBCode = '" + e.FormattedValue + "'");

                    if (rows.Length > 0)
                    {
                        MessageBox.Show("This MBCode is already used.");
                        e.Cancel = true;
                    }
                }
            }
        }

        private void dgvEdit_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
    }
}
