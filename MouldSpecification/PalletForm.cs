using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataService;

namespace MouldSpecification
{
    public partial class PalletForm : Form
    {
        bool bIsLoading = true;
        DataSet dsPalletRef;
        public PalletForm()
        {
            InitializeComponent();
        }

        private void PalletForm_Load(object sender, EventArgs e)
        {
            bIsLoading = true;
            LoadGrid();
            bIsLoading = false;
        }
        private void LoadGrid()
        {
            ProductDataService pds = new ProductDataService();
            dsPalletRef = pds.GetPallet();
            dgvEdit.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvEdit.AutoGenerateColumns = true;
            dgvEdit.DataSource = dsPalletRef.Tables[0];
            dgvEdit.Columns[0].Visible = false;


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

        private void PalletForm_FormClosing(object sender, FormClosingEventArgs e)
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
                DataService.ProductDataService ds = new DataService.ProductDataService();
                ds.UpdatePallet(dsPalletRef);

            }
            catch
            {
                throw;
            }
        }
    }
}
