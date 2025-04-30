using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static Utils.DrawingUtils;

namespace MouldSpecification
{
    public partial class MaterialGradeCost : Form
    {
        bool bIsLoading = true;
        bool nonNumberEntered;
        DataSet dsMaterialGrade;
        Size screenRes = ScreenRes();
        //ComboBox cboMaterial;

        public MaterialGradeCost()
        {
            InitializeComponent();
        }

        private void MaterialGradeForm_Load(object sender, EventArgs e)
        {
            //bIsLoading = true;
            //cboMaterial = new ComboBox();           
            //DataSet dsMt = new MaterialGradeDAL().GetMaterial();
            //this.cboMaterial = new ComboBox();
            //this.cboMaterial.Width = 200;
            //this.cboMaterial.ValueMember = "MaterialID";
            //this.cboMaterial.DisplayMember = "ShortDesc";
            //this.cboMaterial.DataSource = dsMt.Tables[0];
            //this.cboMaterial.Visible = false;
            //this.dgvEdit.Controls.Add(this.cboMaterial);
            //Associate the event-handling method with the SelectedIndexChanged event.
            //this.cboMaterial.SelectedIndexChanged += new System.EventHandler(cboMaterial_SelectedIndexChanged);

            //LoadGrid();
            //bIsLoading = false;
            //this.Size = new Size(800, 1000);
        }
        private void LoadGrid()
        {
            //ProductDataService pds = new ProductDataService();
            //dsMaterialGrade = pds.GetMaterialGradeRef();
            dsMaterialGrade = new MaterialGradeDAL().SelectMaterialGrade();
            DataGridViewCellStyle style = dgvEdit.ColumnHeadersDefaultCellStyle;
            style.BackColor = Color.Navy;
            style.ForeColor = Color.White;
            style.Font = new Font(dgvEdit.Font, FontStyle.Bold);
            dgvEdit.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            dgvEdit.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgvEdit.GridColor = SystemColors.ActiveBorder;
            dgvEdit.EnableHeadersVisualStyles = false;
            dgvEdit.AutoGenerateColumns = true;
            dgvEdit.RowHeadersWidth = Convert.ToInt32(26 * screenRes.Width / 96);

            dgvEdit.DataSource = dsMaterialGrade.Tables[0];
            dgvEdit.Columns[0].Visible = false;
            dgvEdit.Columns[1].Visible = false;
            dgvEdit.Columns["MaterialGrade"].Width = 200;
            dgvEdit.Columns["MaterialGrade"].HeaderText = "Material Grade";
            //dgvEdit.Columns["AdditionalNotes"].HeaderText = "Additional Notes";
            //dgvEdit.Columns["AdditionalNotes"].Width = 200;
            dgvEdit.Columns["CostPerKg"].Width = 60;
            dgvEdit.Columns["Comment"].Width = 200;
            dgvEdit.Columns["CostPerKg"].DefaultCellStyle.Format = "N3";
            dgvEdit.Columns["CostPerKg"].HeaderText = "Cost/kg      $";
            dgvEdit.Columns["CostPerKg"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvEdit.Columns["last_updated_by"].Visible = false;
            dgvEdit.Columns["last_updated_on"].Visible = false;

            //setup dropdown columns for material
            DataGridViewComboBoxColumn cbcMaterial = new DataGridViewComboBoxColumn();
            DataSet dsMaterial = new MaterialGradeDAL().GetMaterial();
            DataTable dt = dsMaterial.Tables[0];
            cbcMaterial.DataSource = dt;
            cbcMaterial.ValueMember = "MaterialID";
            cbcMaterial.DisplayMember = "ShortDesc";
            cbcMaterial.Name = "Material";
            cbcMaterial.DataPropertyName = "MaterialID";
            dgvEdit.Columns.Insert(1, cbcMaterial);
            dgvEdit.Columns["Material"].HeaderText = "Material";
            cbcMaterial.DisplayStyleForCurrentCellOnly = true;
            cbcMaterial.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

            dgvEdit.CellValidating += DgvEdit_CellValidating;

        }

        private void DgvEdit_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvEdit.Columns[e.ColumnIndex].DataPropertyName == "MaterialGrade"
                && e.FormattedValue.ToString() != dgvEdit.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString())
            {
                DataTable dt = (DataTable)dgvEdit.DataSource;
                DataRow[] rows = dt.Select("MaterialGrade = '" + e.FormattedValue + "'");

                if (rows.Length > 0)
                {
                    MessageBox.Show("This MaterialGrade is already used.");
                    e.Cancel = true;
                }
            }
        }

        private void cboMaterial_SelectedIndexChanged(object sender, System.EventArgs e)
        {

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

        private void MaterialGradeCost_FormClosed(object sender, FormClosedEventArgs e)
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
                new MaterialGradeDAL().UpdateMaterialGrade(dsMaterialGrade);

            }
            catch
            {
                throw;
            }
        }

        private void MaterialGradeCost_Shown(object sender, EventArgs e)
        {
            this.splitContainer1.SplitterDistance = p96H(40);
            LoadGrid();
        }
    }
}
