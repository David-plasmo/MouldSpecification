using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static Utils.DrawingUtils;

namespace MouldSpecification
{
    public partial class FixedCost : Form
    {
        DataSet dsFixedCost;
        Size screenRes = ScreenRes();

        public FixedCost()
        {
            InitializeComponent();
        }

        private void LoadGrid()
        {
            dsFixedCost = new FixedCostDAL().SelectFixedCost();

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
            dgvEdit.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dgvEdit.DataSource = dsFixedCost.Tables[0];
            dgvEdit.Columns[0].Visible = false;
            dgvEdit.Columns["last_updated_by"].Visible = false;
            dgvEdit.Columns["last_updated_on"].Visible = false;
            dgvEdit.Columns["FixedCost"].HeaderText = "Fixed Cost       $";
            dgvEdit.Columns["FixedCostDesc"].HeaderText = "Description";
            dgvEdit.Columns["FixedCostDesc"].Width = 400;
            dgvEdit.Columns["Comment"].Width = 400;
            dgvEdit.Columns["FixedCost"].Width = 120;
            dgvEdit.Columns["FixedCost"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvEdit.Columns["FixedCost"].DefaultCellStyle.Format = "N3";
        }

        private void FixedCost_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void FixedCost_Shown(object sender, EventArgs e)
        {
            this.splitContainer1.SplitterDistance = p96H(40);
        }
    }
}
