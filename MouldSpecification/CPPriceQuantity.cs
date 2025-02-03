using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataService;

namespace MouldSpecification
{
    public partial class CPPriceQuantity : Form
    {
        bool bIsLoading;
        bool nonNumberEntered;
        string CurrentCompanyCode;
        string CustNumbr;
        DataSet dsCPPriceQty;
        DataSet dsCPCustomer;
        DataSet dsCompany;
        DataSet dsProduct;
        ComboBox cboCPCustomer;
        ComboBox cboCompanyCode;
        ComboBox cboProduct;
        DateTimePicker dtp = new DateTimePicker();
        Rectangle rectangle;

        public CPPriceQuantity()
        {
            InitializeComponent();
            dgvEdit.Controls.Add(dtp);
            dtp.Visible = false;
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = "dd-MM-yyyy";
            dtp.TextChanged += new EventHandler(dtp_TextChange);
            //dtp.KeyPress += new KeyPressEventHandler(dtp_KeyPress);
            //dtp.KeyDown += new KeyEventHandler(dtp_KeyDown);

            //dtp.ShowCheckBox = true;
            //dtp.Checked = false;
        }
        private void LoadGrid()
        {
            ProductDataService pds = new ProductDataService();
            dsCPPriceQty = pds.GetCPPriceQty();
            dgvEdit.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvEdit.AutoGenerateColumns = true;
            dgvEdit.DataSource = dsCPPriceQty.Tables[0];
            dgvEdit.Columns[0].Visible = false;
            dgvEdit.Columns[1].Visible = false;
            dgvEdit.Columns[2].Visible = false;
            dgvEdit.Columns[3].Visible = false;
            dgvEdit.Columns[4].Visible = false;
            //dgvEdit.Columns[5].Visible = false;
            dgvEdit.Columns["DateChanged"].DefaultCellStyle.Format = "dd-MM-yyyy";
            dgvEdit.Columns["PricingQuantity"].HeaderText = "Quantity";
            dgvEdit.Columns["last_updated_by"].ReadOnly = true;
            dgvEdit.Columns["last_updated_on"].ReadOnly = true;
            dgvEdit.Columns["last_updated_on"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";


        }
        //private void dtp_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == (char)Keys.Delete)
        //    {
        //        dgvEdit.CurrentCell.Value = DBNull.Value;
        //    }
        //}
        //private void dtp_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Delete)
        //    {
        //        MessageBox.Show("delete pressed");
        //        e.Handled = true;
        //    }
        //}
    
        private void dtp_TextChange(object sender, EventArgs e)
        {

            DateTime date;
            if (((DateTimePicker)sender).ShowCheckBox == true)
            {
                if (((DateTimePicker)sender).Checked == false)
                {
                    ((DateTimePicker)sender).CustomFormat = @" ";
                    ((DateTimePicker)sender).Format = DateTimePickerFormat.Custom;
                    dgvEdit.CurrentCell.Value = DBNull.Value; //date.ToString();
                }
                else
                {
                    ((DateTimePicker)sender).CustomFormat = "dd-MM-yyyy"; //DateTimePickerFormat.Short;
                    ((DateTimePicker)sender).Format = DateTimePickerFormat.Custom;
                   
                    if (DateTime.TryParseExact(dtp.Text.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                    {
                        // Success
                        dgvEdit.CurrentCell.Value = date.ToString();
                        dtp.Visible = false;
                    }
                    else
                    {
                        // Parse failed
                    }
                }
            }
            else
            {
                //((DateTimePicker)sender).Format = DateTimePickerFormat.Short;
                ((DateTimePicker)sender).CustomFormat = "dd-MM-yyyy"; //DateTimePickerFormat.Short;
                ((DateTimePicker)sender).Format = DateTimePickerFormat.Custom;
                if (DateTime.TryParseExact(dtp.Text.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    // Success
                    dgvEdit.CurrentCell.Value = date.ToString();
                    //dtp.Visible = false;
                }
                else
                {
                    // Parse failed
                }
            }


            //DateTime date;
            //if (DateTime.TryParseExact(dtp.Text.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture,DateTimeStyles.None, out date))
            //{
            //    // Success
            //    dgvEdit.CurrentCell.Value = date.ToString();
            //    dtp.Visible = false;
            //}
            //else
            //{
            //    // Parse failed
            //}

        }

        private void duplicateOntoNewRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("to do:  duplicate selected row onto a new row");
            DataTable dt = dgvEdit.DataSource as DataTable;
            //Create the new row
            DataRow nrow = dt.NewRow();

            //Populate the row with selected row data, excepting primary key columnn
            DataRow srow = dt.Rows[dgvEdit.CurrentRow.Index];
            for (int i = 1; i < dt.Columns.Count; i++)
            {
                nrow[i] = srow[i];
            }

            //Add the row to data table
            dt.Rows.Add(nrow);

            //position to the new bottom row
            dgvEdit.FirstDisplayedScrollingRowIndex = dgvEdit.RowCount - 1;

        }

        private void dgvEdit_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            //var current = dgvEdit.CurrentRow;
            //if (current != null)
            int rowSelected = e.RowIndex;
            if (e.RowIndex != -1)
                e.ContextMenuStrip = strip;
        }

        private void dgvEdit_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridViewCell currentCell = (sender as DataGridView).CurrentCell;
            if ((e.KeyCode == Keys.F10 && e.Shift) || e.KeyCode == Keys.Apps)
            {
                e.SuppressKeyPress = true;
                
                if (currentCell != null)
                {
                    ContextMenuStrip cms = currentCell.ContextMenuStrip;
                    if (cms != null)
                    {
                        Rectangle r = currentCell.DataGridView.GetCellDisplayRectangle(currentCell.ColumnIndex, currentCell.RowIndex, false);
                        Point p = new Point(r.X + r.Width, r.Y + r.Height);
                        cms.Show(currentCell.DataGridView, p);
                    }
                }
            }
            if (e.KeyCode == Keys.Delete)
            {
                if (currentCell != null && currentCell.OwningColumn.Name == "DateChanged")
                {
                    currentCell.Value = DBNull.Value;
                }
            }
        }

        private void dgvEdit_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1 && e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                DataGridViewCell c = (sender as DataGridView)[e.ColumnIndex, e.RowIndex];
                if (!c.Selected)
                {
                    c.DataGridView.ClearSelection();
                    c.DataGridView.CurrentCell = c;
                    c.Selected = true;
                }
            }
        }

        private void CPPriceQuantity_Load(object sender, EventArgs e)
        {
            bIsLoading = true;
            LoadGrid();
            dsCPCustomer = new ProductDataService().SelectCPCustomerDropdown();
            cboCPCustomer = new ComboBox();
            cboCPCustomer.ValueMember = "CustID";
            cboCPCustomer.DisplayMember = "Customer";
            cboCPCustomer.DataSource = dsCPCustomer.Tables[0];
            cboCPCustomer.ResetText();
            dgvEdit.Controls.Add(cboCPCustomer);
            cboCPCustomer.SelectedIndexChanged += new System.EventHandler(cboCPCustomer_SelectedIndexChanged);
            cboProduct = new ComboBox();
            dgvEdit.Controls.Add(cboProduct);

            dsCompany = new DataService.ProductDataService().GetCompany();
            DataView dv = new DataView(dsCompany.Tables[0]);
            //dv.RowFilter = "CompanyCode <> 'PL'";
            this.cboCompanyCode = new ComboBox();
            this.cboCompanyCode.ValueMember = "CMPANYID";
            this.cboCompanyCode.DisplayMember = "CompanyCode";
            this.cboCompanyCode.DataSource = dv;
            this.cboCompanyCode.Visible = false;
            this.dgvEdit.Controls.Add(this.cboCompanyCode);
            //Associate the event-handling method with the 
            // SelectedIndexChanged event.
            this.cboCompanyCode.SelectedIndexChanged += new System.EventHandler(cboCompanyCode_SelectedIndexChanged);
            bIsLoading = false;
        }
        private void cboCPCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bIsLoading)
            {
                int custid = (int)cboCPCustomer.SelectedValue;
                dgvEdit.CurrentRow.Cells["CustID"].Value = custid;
            }
        }
        private void cboCompanyCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bIsLoading)
            {
                int pmID = (int)cboCompanyCode.SelectedValue;
                DataTable table = dsCompany.Copy().Tables[0];
                DataRow[] row = table.Select("CMPANYID = " + pmID.ToString());
                string compCode = row[0]["CompanyCode"].ToString();
                CurrentCompanyCode = compCode;
                ProductDataService pds = new ProductDataService();
                //DataSet ds = pds.GetCustomerIndexByCompany(compCode);
                dsProduct = pds.SelectCPProductDropdown(compCode);
;               if (dsProduct != null)
                {
                    //cboCPCustomer.ValueMember = "CUSTNMBR";
                    cboProduct.ValueMember = "PmID";
                    cboProduct.DisplayMember = "Code";
                    cboProduct.DataSource = dsProduct.Tables[0];
                    cboProduct.ResetText();
                    cboProduct.SelectedIndexChanged += new System.EventHandler(cboProduct_SelectedIndexChanged);
                    //reset product, description, customer,
                    DataTable dt = dgvEdit.DataSource as DataTable;
                    if (!dgvEdit.CurrentRow.IsNewRow)
                    {
                        DataRow srow = dt.Rows[dgvEdit.CurrentRow.Index];
                        srow["Code"] = null;
                        srow["Description"] = null;
                        srow["Customer"] = null;
                    }
                    else
                    {
                        dgvEdit.CurrentRow.Cells["Code"].Value = DBNull.Value;
                        dgvEdit.CurrentRow.Cells["Description"].Value = DBNull.Value;
                        dgvEdit.CurrentRow.Cells["Customer"].Value = DBNull.Value;
                    }
                }
            }
        }
        private void cboProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bIsLoading)
            {
                int pmID = (int)cboProduct.SelectedValue;
                DataSet copyDataSet;
                copyDataSet = dsProduct.Copy();
                DataView dv = new DataView(copyDataSet.Tables[0]);
                dv.RowFilter = "PmID = " + pmID.ToString() + " AND CompanyCode = '" + CurrentCompanyCode + "'";
                //reset description
                //DataTable dt = dgvEdit.DataSource as DataTable;
                //DataRow srow = dt.Rows[dgvEdit.CurrentRow.Index];
                if (dv.Count > 0)
                {
                    //srow["Description"] = dv[0]["Description"].ToString();
                    dgvEdit.CurrentRow.Cells["Description"].Value = dv[0]["Description"].ToString();
                    dgvEdit.CurrentRow.Cells["PmID"].Value = Convert.ToInt32(dv[0]["PmID"].ToString());
                }
                    
            }
        }

        private void dgvEdit_VisibleChanged(object sender, EventArgs e)
        {
            if (dgvEdit.Visible)
            {
                dgvEdit.AutoResizeColumns();
            }
            
        }

        private void dgvEdit_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            dtp.Visible = false;
            cboCPCustomer.Visible = false;
            cboCompanyCode.Visible = false;
            cboProduct.Visible = false;
        }

        private void dgvEdit_Scroll(object sender, ScrollEventArgs e)
        {
            dtp.Visible = false;
            if (cboCPCustomer.Visible == true)
            {
                dgvEdit.CurrentCell.Value = cboCPCustomer.Text;
                cboCPCustomer.Visible = false;
                dgvEdit.EndEdit();
            }
            if (cboCompanyCode.Visible == true)
            {
                dgvEdit.CurrentCell.Value = cboCompanyCode.Text;
                cboCompanyCode.Visible = false;
                dgvEdit.EndEdit();
            }
            if (cboProduct.Visible == true)
            {
                dgvEdit.CurrentCell.Value = cboProduct.Text;
                cboProduct.Visible = false;
                dgvEdit.EndEdit();
            }
        }

        private void dgvEdit_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //switch (e.ColumnIndex)
            //{
            //    case 12: // Column index of needed dateTimePicker cell

            //        rectangle = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true); //  
            //        dtp.Size = new Size(rectangle.Width, rectangle.Height); //  
            //        dtp.Location = new Point(rectangle.X, rectangle.Y); //  
            //        dtp.Visible = true;

            //        break;
            //}
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

        private void CPPriceQuantity_FormClosing(object sender, FormClosingEventArgs e)
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
                DataService.ProductDataService pds = new DataService.ProductDataService();
                pds.UpdateCPPriceQty(dsCPPriceQty);

            }
            catch
            {
                throw;
            }
        }

        private void dgvEdit_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //GridColumns thisCol;
            DataGridViewTextBoxEditingControl ec = (DataGridViewTextBoxEditingControl)e.Control;
            ec.KeyPress -= new KeyPressEventHandler(ec_KeyPress);
            if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "Price"
                || dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "Number")
            {
                //if (thisCol.DataType == "real" || thisCol.DataType == "int")
                //{
                // setup editing for numerical input                                     
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

        private void dgvEdit_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dgvEdit.Columns[e.ColumnIndex].Name == "Customer")
            {
                cboCPCustomer.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                cboCPCustomer.Width = dgvEdit.Columns[e.ColumnIndex].Width;
                cboCPCustomer.Visible = true;
            }
            if (dgvEdit.Columns[e.ColumnIndex].Name == "Company")
            {
                cboCompanyCode.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                cboCompanyCode.Width = dgvEdit.Columns[e.ColumnIndex].Width;
                cboCompanyCode.Visible = true;
            }
            if (dgvEdit.Columns[e.ColumnIndex].Name == "Code")
            {
                cboProduct.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                cboProduct.Width = dgvEdit.Columns[e.ColumnIndex].Width;
                cboProduct.Visible = true;
            }
            if (dgvEdit.Columns[e.ColumnIndex].Name == "DateChanged")
            {
                //cboProduct.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                //cboProduct.Width = dgvEdit.Columns[e.ColumnIndex].Width;
                //cboProduct.Visible = true;

                rectangle = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true); //  
                dtp.Size = new Size(rectangle.Width, rectangle.Height); //  
                dtp.Location = new Point(rectangle.X, rectangle.Y); //  
                dtp.Visible = true;
            }
        }

        private void dgvEdit_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (cboCPCustomer.Visible == true)
            {
                dgvEdit.CurrentCell.Value = cboCPCustomer.Text;
                cboCPCustomer.Visible = false;
            }
            if (cboCompanyCode.Visible == true)
            {
                dgvEdit.CurrentCell.Value = cboCompanyCode.Text;
                cboCompanyCode.Visible = false;
            }
            if (cboProduct.Visible == true)
            {
                dgvEdit.CurrentCell.Value = cboProduct.Text;
                cboProduct.Visible = false;
            }
            if (dtp.Visible == true)
            {
                //dgvEdit.CurrentCell.Value = cboProduct.Text;
                dtp.Visible = false;
            }

        }
    }
}
