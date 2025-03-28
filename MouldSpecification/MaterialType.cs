using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static Utils.DrawingUtils;

namespace MouldSpecification
{
    public partial class MaterialType : Form
    {
        bool bIsLoading = true;
        DataSet dsMaterialType;
        Size screenRes = ScreenRes();

        public MaterialType()
        {
            InitializeComponent();
        }

        private void MaterialTypeForm_Load(object sender, EventArgs e)
        {
            //bIsLoading = true;
            //LoadGrid();
            //bIsLoading = false;
            //this.Size = new Size(500, 1000);
        }
        private void LoadGrid()
        {
            dsMaterialType = new MaterialTypeDAL().GetMaterial();

            DataGridViewCellStyle style = dgvEdit.ColumnHeadersDefaultCellStyle;
            style.BackColor = Color.Navy;
            style.ForeColor = Color.White;
            style.Font = new Font(dgvEdit.Font, FontStyle.Bold);
            dgvEdit.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            dgvEdit.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgvEdit.GridColor = SystemColors.ActiveBorder;
            dgvEdit.EnableHeadersVisualStyles = false;
            dgvEdit.AutoGenerateColumns = true;
            dgvEdit.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvEdit.RowHeadersWidth = Convert.ToInt32(26 * screenRes.Width / 96);

            dgvEdit.DataSource = dsMaterialType.Tables[0];
            dgvEdit.Columns[0].Visible = false;
            dgvEdit.Columns["Description"].Width = 300;
            dgvEdit.Columns["Comment"].Width = 400;
            dgvEdit.Columns["last_updated_by"].Visible = false;
            dgvEdit.Columns["last_updated_on"].Visible = false;
        }

        private void dgvEdit_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (!e.Row.IsNewRow)
            {
                int materialID = (int)e.Row.Cells["MaterialID"].Value;
                if (new MaterialTypeDAL().CheckDependencies(materialID))
                {
                    DialogResult response = MessageBox.Show("This material has associated Material Grades. You should change or delete them in here first.", "Unable to Delete",
                                                      MessageBoxButtons.OK,
                                                      MessageBoxIcon.Information,
                                                      MessageBoxDefaultButton.Button2);
                    e.Cancel = true;
                }
                else
                {
                    DialogResult response = MessageBox.Show("Are you sure?", "Confirm Delete?");
                    if (response == DialogResult.Cancel)
                        e.Cancel = true;
                }
            }
        }

        private void dgvEdit_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

            //Check columnn for unique cell values..
            if (dgvEdit.Columns[e.ColumnIndex].DataPropertyName == "ShortDesc"
                && e.FormattedValue.ToString() != dgvEdit.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString())
            {
                DataTable dt = (DataTable)dgvEdit.DataSource;
                DataRow[] rows = dt.Select("ShortDesc = '" + e.FormattedValue + "'");

                if (rows.Length > 0)
                {
                    MessageBox.Show("This ShortDesc is already used.");
                    e.Cancel = true;
                }
            }
        }

        private void MaterialType_FormClosed(object sender, FormClosedEventArgs e)
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
                new MaterialTypeDAL().UpdateMaterial(dsMaterialType);
            }
            catch
            {
                throw;
            }
        }

        private void MaterialType_Shown(object sender, EventArgs e)
        {
            this.splitContainer1.SplitterDistance = p96H(40);
            LoadGrid();
        }
    }
}
