using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataService;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;

namespace MouldSpecification
{
    public partial class CartonPackaging : Form
    {
        bool bIsLoading = true;
        bool nonNumberEntered = false;
        //bool bIgnoreError = false;
        
        DataSet dsCartonPackaging;
        ComboBox cboGPCartonSize;
        ComboBox cboCartonPackaging;


        public CartonPackaging()
        {
            InitializeComponent();
        }

        private void CartonPackaging_Load(object sender, EventArgs e)
        {
            LoadGrid();
            bIsLoading = false;
            DataSet dsCS = new DataService.ProductDataService().GetCartonSize();
            this.cboGPCartonSize = new ComboBox();
            //this.cboMaterial.Width = 200;
            this.cboGPCartonSize.DataSource = dsCS.Tables[0];
            this.cboGPCartonSize.DisplayMember = "CartonType";
            this.cboGPCartonSize.ValueMember = "CtnID";
            this.cboGPCartonSize.Visible = false;
            this.dgvEdit.Controls.Add(this.cboGPCartonSize);
            //Associate the event-handling method with the 
            // SelectedIndexChanged event.
            this.cboGPCartonSize.SelectedIndexChanged += new System.EventHandler(cboGPCartonSize_SelectedIndexChanged);

            //DataSet dsCartonPackagingDropdown = new DataService.ProductDataService().GetCartonPackagingDropdown();
            //this.cboCartonPackaging = new ComboBox();
            ////this.cboMaterial.Width = 200;
            //this.cboCartonPackaging.ValueMember = "CtnID";
            //this.cboCartonPackaging.DisplayMember = "CartonType";
            //this.cboCartonPackaging.DataSource = dsCartonPackagingDropdown.Tables[0];
            //this.cboCartonPackaging.Visible = false;
            //this.dgvEdit.Controls.Add(this.cboCartonPackaging);
            ////Associate the event-handling method with the 
            //// SelectedIndexChanged event.
            //this.cboCartonPackaging.SelectedIndexChanged += new System.EventHandler(cboCartonPackaging_SelectedIndexChanged);
        }

        private void LoadGrid()
        {
            ProductDataService pds = new ProductDataService();
            dsCartonPackaging = pds.GetCartonPackaging();
            dgvEdit.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvEdit.AutoGenerateColumns = true;
            dgvEdit.DataSource = dsCartonPackaging.Tables[0];
            dgvEdit.Columns[0].Visible = false;
            dgvEdit.Columns[1].Visible = false;
            dgvEdit.Columns["WidthMM"].ReadOnly = true;
            dgvEdit.Columns["HeightMM"].ReadOnly = true;
            dgvEdit.Columns["DepthMM"].ReadOnly = true;
            dgvEdit.Columns["DepthMM"].ReadOnly = true;
            dgvEdit.Columns["PalletQty"].ReadOnly = true;
            dgvEdit.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvEdit.RowTemplate.Height = 22;
            //dgvEdit.AutoResizeColumns();
        }
        private void cboGPCartonSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            int gpCtnID = (int)cboGPCartonSize.SelectedValue;
            dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["GPCartonID"].Value = gpCtnID;
            dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["GPSize"].Value = cboGPCartonSize.Text;
        }
        //private void cboCartonPackaging_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}
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

        private void CartonPackaging_FormClosing(object sender, FormClosingEventArgs e)
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
                ds.UpdateCartonPackaging(dsCartonPackaging);

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
            if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "CartonCost" ||
                dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "InnerBagCost" ) //||
                //dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "WidthMM" ||
                //dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "HeightMM" ||
                //dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "DepthMM")
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
            //bIgnoreError = false;
            //if (e.KeyCode == Keys.Escape)
            //{
            //    bIgnoreError = true;
            //    return;
            //}
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
            if (dgvEdit.Columns[e.ColumnIndex].Name == "GPSize")
            {
                cboGPCartonSize.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                cboGPCartonSize.Width = dgvEdit.Columns[e.ColumnIndex].Width;
                cboGPCartonSize.Visible = true;
            }
            //if (dgvEdit.Columns[e.ColumnIndex].Name == "CartonType")
            //{
            //    cboCartonPackaging.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
            //    cboCartonPackaging.Width = dgvEdit.Columns[e.ColumnIndex].Width;
            //    cboCartonPackaging.Visible = true;
            //}
        }

        private void dgvEdit_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!string.IsNullOrEmpty(dgvEdit[e.ColumnIndex, e.RowIndex].ErrorText))
            {
                DataGridViewCell cell = dgvEdit.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.ErrorText = string.Empty;
                cell.Style.Padding = (Padding)cell.Tag;
                cell.Tag = null;
            }
            if (cboGPCartonSize.Visible == true)
            {
                dgvEdit.CurrentCell.Value = cboGPCartonSize.Text;
                cboGPCartonSize.Visible = false;
            }
            
        }

        private void dgvEdit_Scroll(object sender, ScrollEventArgs e)
        {
            if (cboGPCartonSize.Visible == true)
            {
                dgvEdit.CurrentCell.Value = cboGPCartonSize.Text;
                cboGPCartonSize.Visible = false;
                dgvEdit.EndEdit();
            }
            //if (cboCartonPackaging.Visible == true)
            //{
            //    dgvEdit.CurrentCell.Value = cboCartonPackaging.Text;
            //    cboCartonPackaging.Visible = false;
            //    dgvEdit.EndEdit();
            //}
        }

        private void dgvEdit_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string headerText =
            dgvEdit.Columns[e.ColumnIndex].HeaderText;

            // Abort validation if cell is not in the Carton Type column.
            if (!headerText.Equals("CartonType")) return;

            DataGridViewCell cell = dgvEdit.Rows[e.RowIndex].Cells[e.ColumnIndex];

            // Confirm that the cell is not empty.
            if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
            {
                //cell.ErrorText = "Carton Type must not be empty";
                //if (cell.Tag == null)
                //{
                //    cell.Tag = cell.Style.Padding;
                //    cell.Style.Padding = new Padding(0, 0, 18, 0);
                //}
                MessageBox.Show("Carton Type must not be empty");
                e.Cancel = true;
            }
            else
            {
                // check carton type is unique
                string ctnType = e.FormattedValue.ToString();
                DataSet ds = dsCartonPackaging.Copy();
                DataView dv = new DataView(ds.Tables[0]);
                dv.RowFilter = "CartonType='" + ctnType + "'";
                if (dv.Count > 0)  //&& !bIgnoreError)
                {
                    //cell.ErrorText = "Carton Type must be unique";
                    //if (cell.Tag == null)
                    //{
                    //    cell.Tag = cell.Style.Padding;
                    //    cell.Style.Padding = new Padding(0, 0, 18, 0);
                    //}
                    //dv.Sort = "CtnID";
                    //int i = dv.Find(ctnType);
                    dv.RowFilter = "";
                    for (int i = 0; i < dv.Count; i++)
                    {
                        string match = ds.Tables[0].Rows[i]["CartonType"].ToString();
                        if (match == ctnType && i != dgvEdit.CurrentRow.Index)
                        {
                            MessageBox.Show("Carton Type must be unique");
                            e.Cancel = true;
                            return;
                        }
                    }                    
                }
                else
                {
                    dgvEdit.Rows[e.RowIndex].ErrorText = string.Empty;
                }
            }
        }

        private void dgvEdit_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
    }
    
}
