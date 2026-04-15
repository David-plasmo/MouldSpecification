using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static Utils.DrawingUtils;

namespace MouldSpecification
{
    public partial class ProductCustomerDataEntry : Form
    {
        DataSet dsCustomerProduct, dsCompany, dsProduct;
        DataView dvSelectedCustomer, dvSelectedProduct;
        DataGridViewComboBoxEditingControl cbec = null;

        int customerID, itemID;
        bool customerExists;

        public ProductCustomerDataEntry()
        {
            InitializeComponent();
        }

        private void ProductCustomerDataEntry_Load(object sender, EventArgs e)
        {
            LoadData();
            FormatGrid();
        }

        private void LoadData()
        {
            SpecificationDataEntryDAL dal = new SpecificationDataEntryDAL();
            dsCompany = dal.GetCustomerIndex("IM");
            dsProduct = dal.GetProductIndex("IM");            
            dsCustomerProduct = new CustomerProductDAL().CustomerProductDS();
            dsCustomerProduct.Tables[0].TableName = "CustomerProduct";
            dvSelectedProduct = dsCustomerProduct.Tables[0].DefaultView;
            dvSelectedCustomer = dsCompany.Tables[0].DefaultView;
            dgvCustomerProduct.DataSource = dvSelectedProduct;  //dsCustomerProduct.Tables[0];

            //Product dropdown 
            System.Data.DataTable dt = dsProduct.Tables[0];
            cboProduct.DataSource = dt;
            cboProduct.DisplayMember = "ITEMDESC";
            cboProduct.ValueMember = "ItemID";
            cboProduct.SelectedIndexChanged += cboProduct_SelectedIndexChanged;
            cboProduct.SelectedIndex = -1; //force cbo to change selection
            cboProduct.SelectedIndex = 0;  //to first index value
        }

        private void cboProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(cboCustomer.SelectedValue.ToString());
            if (cboProduct.SelectedValue != null)
            {
                itemID = (int)cboProduct.SelectedValue;
                dvSelectedProduct.RowFilter = "itemID = " + itemID.ToString();
                dvSelectedProduct.Table.Columns["itemID"].DefaultValue = itemID;
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
            style.Font = new System.Drawing.Font(dgvCustomerProduct.Font, FontStyle.Regular);
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

            //add customer ComboBoxColumn
            DataGridViewComboBoxColumn cbcCustomer;
            cbcCustomer = new DataGridViewComboBoxColumn();            
            cbcCustomer.HeaderText = "Customer";
            dgvCustomerProduct.Columns.Insert(3, cbcCustomer);
            dgvCustomerProduct.Columns[3].Name = "Customer";
            cbcCustomer.DataSource = dvSelectedCustomer;
            cbcCustomer.DataPropertyName = "CustomerID";
            cbcCustomer.ValueMember = "CustomerID";
            cbcCustomer.DisplayMember = "CUSTNAME";
            cbcCustomer.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;            
            cbcCustomer.DisplayStyleForCurrentCellOnly = true;
            dgvCustomerProduct.Columns[0].Visible = false;
            dgvCustomerProduct.Columns[1].Visible = false;
            dgvCustomerProduct.Columns[2].Visible = false;

            dgvCustomerProduct.Columns["Customer"].Width = p96W(300);

            dgvCustomerProduct.EditingControlShowing += dgvCustomerProduct_EditingControlShowing;            

            

        }

        private void dgvCustomerProduct_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dgvCustomerProduct_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvCustomerProduct.Columns["Customer"].Index && e.RowIndex >= 0) //check if combobox column
            {
                object selectedValue = dgvCustomerProduct.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            }
        }

        private void dgvCustomerProduct_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvCustomerProduct.IsCurrentCellDirty)
            {
                dgvCustomerProduct.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
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

        private void Cbec_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbec != null && cbec.SelectedIndex != -1)
            {                
                DataRowView dv = (DataRowView)cbec.SelectedItem;
                customerID = (int)dv.Row[0];                               
            }
        }

        private void dgvCustomerProduct_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (dgvCustomerProduct.Columns[e.ColumnIndex].HeaderText == "Customer" && e.FormattedValue.ToString() != dgvCustomerProduct[e.ColumnIndex, e.RowIndex].Value.ToString())
                {
                    int count = 0;
                    customerExists = false;
                    foreach (DataGridViewRow row in dgvCustomerProduct.Rows)
                    {
                        if (row.Cells[1].Value != null && (int)row.Cells[1].Value == customerID)
                        {
                            count += 1;
                            if (count > 1)
                            {
                                MessageBox.Show("This customer already exists.");
                                e.Cancel = true;
                                customerExists = true;
                                break;
                            }                                
                        }
                    }

                    if (!customerExists)
                    {
                        dgvCustomerProduct[1, e.RowIndex].Value = customerID; //Column Index:  2=ItemID, 1=CustomerID, 0=CustomerProductID
                    }
                   
                        
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
