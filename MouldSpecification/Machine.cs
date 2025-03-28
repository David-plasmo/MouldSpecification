using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static Utils.DrawingUtils;

namespace MouldSpecification
{
    public partial class Machine : Form
    {
        //bool bIsLoading = true;
        bool nonNumberEntered = false;
        DataSet dsMachine;
        Size screenRes = ScreenRes();
        //ComboBox cboType;
        //ComboBox cboCapacity;

        public Machine()
        {
            InitializeComponent();
        }

        private void Machine_Load(object sender, EventArgs e)
        {
            //cboType = new ComboBox();
            //cboType.Visible = false;

            LoadGrid();

        }

        private void LoadGrid()
        {
            dsMachine = new MachineDAL().SelectMachine("IM");

            dgvEdit.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
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

            dgvEdit.DataSource = dsMachine.Tables[0];
            dgvEdit.Columns[0].Visible = false;
            dgvEdit.Columns["last_updated_by"].Visible = false;
            dgvEdit.Columns["last_updated_on"].Visible = false;
            dgvEdit.Columns["CostPerHour"].HeaderText = "Cost/hr        $";
            dgvEdit.Columns["CostPerHour"].Width = 120;
            dgvEdit.Columns["Comment"].Width = 400;
            dgvEdit.Columns["CostPerHour"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvEdit.Columns["CostPerHour"].DefaultCellStyle.Format = "N3";


            DataGridViewComboBoxColumn cbcCapacity = new DataGridViewComboBoxColumn();
            cbcCapacity.Items.Add("");
            cbcCapacity.Items.Add("Single");
            cbcCapacity.Items.Add("Twin");
            cbcCapacity.Name = "Capacity";
            cbcCapacity.DataPropertyName = "Capacity";
            dgvEdit.Columns.Remove("Capacity");
            dgvEdit.Columns.Insert(2, cbcCapacity);
            dgvEdit.Columns["Capacity"].HeaderText = "Capacity";
            cbcCapacity.DisplayStyleForCurrentCellOnly = true;
            cbcCapacity.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;

            DataGridViewComboBoxColumn cbcType = new DataGridViewComboBoxColumn();
            cbcType.Items.Add("BM");
            cbcType.Items.Add("IM");
            cbcType.Name = "Type";
            cbcType.DataPropertyName = "Type";
            dgvEdit.Columns.Remove("Type");
            dgvEdit.Columns.Insert(3, cbcType);
            dgvEdit.Columns["Type"].HeaderText = "Type";
            cbcType.DisplayStyleForCurrentCellOnly = true;
            cbcType.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
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

        private void Machine_FormClosing(object sender, FormClosingEventArgs e)
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
                new MachineDAL().UpdateMachine(dsMachine);

            }
            catch
            {
                throw;
            }
        }

        private void dgvEdit_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //GridColumns thisCol;


            if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "CostPerHour")
            {
                //if (thisCol.DataType == "real" || thisCol.DataType == "int")
                //{
                // setup editing for numerical input
                // 
                DataGridViewTextBoxEditingControl ec = (DataGridViewTextBoxEditingControl)e.Control;
                ec.KeyPress -= new KeyPressEventHandler(ec_KeyPress);
                ec.KeyPress += new KeyPressEventHandler(ec_KeyPress);
                ec.KeyDown -= ec_KeyDown;
                ec.KeyDown += ec_KeyDown;
                //}
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

        private void dgvEdit_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dgvEdit_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "Machine")
            {
                //check machine is unique
                if (dgvEdit.Columns[e.ColumnIndex].DataPropertyName == "Machine"
                    && e.FormattedValue.ToString() != dgvEdit.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString())
                {
                    DataTable dt = (DataTable)dgvEdit.DataSource;
                    DataRow[] rows = dt.Select("Machine = '" + e.FormattedValue + "'");

                    if (rows.Length > 0)
                    {
                        MessageBox.Show("This Machine name is already used.");
                        e.Cancel = true;
                    }
                }
            }
        }

        private void Machine_Shown(object sender, EventArgs e)
        {
            this.splitContainer1.SplitterDistance = p96H(40);
        }
    }
}
