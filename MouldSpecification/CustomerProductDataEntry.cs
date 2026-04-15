using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Utils.DrawingUtils;

namespace MouldSpecification
{
    public partial class CustomerProductDataEntry : Form
    {
        DataSet dsCustomerProduct, dsCompany, dsProduct;
        DataView dvSelectedCustomer, dvSelectedProduct;
        DataGridViewComboBoxEditingControl cbec = null;

        int customerID, itemID;
        bool productExists = false;

        public CustomerProductDataEntry()
        {
            InitializeComponent();
        }

        private void CustomerProductDataEntry_Load(object sender, EventArgs e)
        {
            LoadData();
            FormatGrid();                        
        }

        private void CustomerProductDataEntry_FormClosing1(object sender, FormClosingEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void LoadData()
        {
            SpecificationDataEntryDAL dal = new SpecificationDataEntryDAL();
            dsCompany = dal.GetCustomerIndex("IM"); //filters on Injection Mould
            dsProduct = dal.GetProductIndex("IM");  //filters on Injection Mould
            dvSelectedProduct = dsProduct.Tables[0].DefaultView;
            dsCustomerProduct = new CustomerProductDAL().CustomerProductDS();
            dsCustomerProduct.Tables[0].TableName = "CustomerProduct";
            dvSelectedCustomer = dsCustomerProduct.Tables[0].DefaultView;
            dgvCustomerProduct.DataSource = dvSelectedCustomer;  //dsCustomerProduct.Tables[0];

            //Customer dropdown 
            DataTable dt = dsCompany.Tables[0];
            cboCustomer.DataSource = dt;
            cboCustomer.DisplayMember = "CUSTNAME";
            cboCustomer.ValueMember = "CustomerID";
            cboCustomer.SelectedIndexChanged += cboCustomer_SelectedIndexChanged;
            cboCustomer.SelectedIndex = -1; //force cbo to change selection
            cboCustomer.SelectedIndex = 0;  //to first index value
        }

        private void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(cboCustomer.SelectedValue.ToString());
            if (cboCustomer.SelectedValue != null)
            {
                customerID = (int)cboCustomer.SelectedValue;
                dvSelectedCustomer.RowFilter = "CustomerID = " + customerID.ToString();
                dvSelectedCustomer.Table.Columns["CustomerID"].DefaultValue = customerID;
            }
               
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            this.DialogResult= DialogResult.OK;
            SaveGrid();
            this.Close();
        }

        private void FormatGrid()
        {            
            DataGridViewCellStyle style = dgvCustomerProduct.ColumnHeadersDefaultCellStyle;
            style.BackColor = Color.LightSteelBlue;
            style.ForeColor = Color.Black;
            style.Font = new Font(dgvCustomerProduct.Font, FontStyle.Regular);
            dgvCustomerProduct.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            dgvCustomerProduct.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgvCustomerProduct.GridColor = SystemColors.ActiveBorder;
            dgvCustomerProduct.EnableHeadersVisualStyles = false;
            dgvCustomerProduct.AutoGenerateColumns = true;
            dgvCustomerProduct.ColumnHeadersHeight = p96H(19);
            dgvCustomerProduct.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvCustomerProduct.AllowUserToResizeRows = false;
            dgvCustomerProduct.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            dgvCustomerProduct.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            dgvCustomerProduct.Height = p96H(100);
            dgvCustomerProduct.RowHeadersWidth = p96W(26);
            dgvCustomerProduct.RowTemplate.MinimumHeight = p96H(19);
            dgvCustomerProduct.RowTemplate.Height = p96H(19);
            dgvCustomerProduct.Rows[dgvCustomerProduct.NewRowIndex].MinimumHeight = p96H(19);
            dgvCustomerProduct.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvCustomerProduct.AllowUserToAddRows = true;
            dgvCustomerProduct.AllowUserToDeleteRows = true;
            dgvCustomerProduct.UserDeletingRow += dgvCustomerProduct_UserDeletingRow;

            //add product ComboBoxColumn
            DataGridViewComboBoxColumn cbcProduct;
            cbcProduct = new DataGridViewComboBoxColumn();            
            cbcProduct.HeaderText = "Product";
            dgvCustomerProduct.Columns.Insert(3, cbcProduct);
            dgvCustomerProduct.Columns[3].Name = "Product";
            cbcProduct.DataSource = dvSelectedProduct;
            cbcProduct.DataPropertyName = "ItemID";
            cbcProduct.ValueMember = "ItemID";
            cbcProduct.DisplayMember = "ITEMDESC";
            cbcProduct.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;            
            cbcProduct.DisplayStyleForCurrentCellOnly = true;
            dgvCustomerProduct.Columns[0].Visible = false;
            dgvCustomerProduct.Columns[1].Visible = false;
            dgvCustomerProduct.Columns[2].Visible = false;


            dgvCustomerProduct.EditingControlShowing += dgvCustomerProduct_EditingControlShowing;
            dgvCustomerProduct.CurrentCellDirtyStateChanged += dgvCustomerProduct_CurrentCellDirtyStateChanged;

            dgvCustomerProduct.Columns["Product"].Width = p96W(300);

        }

        private void dgvCustomerProduct_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dgvCustomerProduct_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                cbec = e.Control as DataGridViewComboBoxEditingControl;
                cbec.SelectedIndexChanged -= Cbec_SelectedIndexChanged;
                cbec.SelectedIndexChanged += Cbec_SelectedIndexChanged;
            }
        }

        private void dgvCustomerProduct_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvCustomerProduct.IsCurrentCellDirty)
            {
                dgvCustomerProduct.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void Cbec_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbec != null && cbec.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)cbec.SelectedItem;
                int valueOfItem = (int)drv["ItemID"];
                itemID = valueOfItem;
            }
        }

        private void dgvCustomerProduct_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure?", "Confirm Delete Machine", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void dgvCustomerProduct_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (dgvCustomerProduct.Columns[e.ColumnIndex].HeaderText == "Product" && e.FormattedValue.ToString() != dgvCustomerProduct[e.ColumnIndex, e.RowIndex].Value.ToString())
                {
                    int count = 0;
                    productExists = false;
                    foreach (DataGridViewRow row in dgvCustomerProduct.Rows)
                    {
                        if (row.Cells[2].Value != null && (int)row.Cells[2].Value == itemID)
                        {
                            count += 1;
                            if (count > 1)
                            {
                                MessageBox.Show("This product already exists.");
                                e.Cancel = true;
                                productExists = true;
                                break;
                            }
                        }
                    }

                    if (!productExists)
                    {
                        dgvCustomerProduct[2, e.RowIndex].Value = itemID; //0=CustomerProductID, 1=CustomerID, 2= itemID
                    }
                }
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
                if (dsCustomerProduct != null)
                {
                    if (dgvCustomerProduct.IsCurrentRowDirty)
                    {
                        this.Validate();
                    }
                    dgvCustomerProduct.EndEdit();
                    new CustomerProductDAL().UpdateCustomerProduct(dsCustomerProduct, "CustomerProduct");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
