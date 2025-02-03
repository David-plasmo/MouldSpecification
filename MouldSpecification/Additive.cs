using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouldSpecification
{
    public partial class Additive : Form
    {
        DataSet dsAdditive;
        
        public Additive()
        {
            InitializeComponent();
        }

        private void RefreshGrid()
        {
            try
            {
                MainFormDAL dal = new MainFormDAL();
                dsAdditive = dal.SelectAdditiveComp();
                DataGridViewCellStyle style = dgvEdit.ColumnHeadersDefaultCellStyle;
                style.BackColor = Color.Navy;
                style.ForeColor = Color.White;
                style.Font = new Font(dgvEdit.Font, FontStyle.Bold);
                dgvEdit.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                dgvEdit.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                dgvEdit.GridColor = SystemColors.ActiveBorder;
                dgvEdit.EnableHeadersVisualStyles = false;
                dgvEdit.AutoGenerateColumns = true;
                dgvEdit.DataSource = dsAdditive.Tables[0];
                dgvEdit.Columns["AddCompID"].Visible = false;
                dgvEdit.Columns["ItemID"].Visible = false;
                dgvEdit.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                dgvEdit.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
                dgvEdit.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;

                //setup dropdown columns for code and description
                DataGridViewComboBoxColumn cbcCode = new DataGridViewComboBoxColumn();
                DataSet dsManItemIndex = dal.SelectMAN_ItemIndex();
                DataTable dt = dsManItemIndex.Tables[0];
                cbcCode.DataSource = dt;
                cbcCode.ValueMember = "ItemID";
                cbcCode.DisplayMember = "ITEMNMBR";
                cbcCode.Name = "Code";
                cbcCode.DataPropertyName = "ItemID";
                dgvEdit.Columns.Insert(2, cbcCode);
                dgvEdit.Columns["Code"].HeaderText = "Code";
                cbcCode.DisplayStyleForCurrentCellOnly = true;
                cbcCode.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

                DataGridViewComboBoxColumn cbcDescription = new DataGridViewComboBoxColumn();
                cbcDescription.DataSource = dt;
                cbcDescription.ValueMember = "ItemID";
                cbcDescription.DisplayMember = "ITEMDESC";
                cbcDescription.Name = "Description";
                cbcDescription.DataPropertyName = "ItemID";
                dgvEdit.Columns.Insert(3, cbcDescription);
                dgvEdit.Columns["Description"].HeaderText = "Description";
                cbcDescription.DisplayStyleForCurrentCellOnly = true;
                cbcDescription.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

                dgvEdit.Columns["Code"].ReadOnly = true;
                dgvEdit.Columns["Description"].ReadOnly = true;
                dgvEdit.Columns["Code"].DefaultCellStyle.BackColor = Color.LightGray;
                dgvEdit.Columns["Description"].DefaultCellStyle.BackColor = Color.LightGray;

                //Set dropdown column for Additive 1 to 3
                DataGridViewComboBoxColumn cbcAdditive123 = new DataGridViewComboBoxColumn();
                DataSet ds = dal.GetAdditive123();
                dt = ds.Tables[0];
                cbcAdditive123.DataSource = dt;
                cbcAdditive123.ValueMember = "Additive";
                cbcAdditive123.DisplayMember = "Additive";
                cbcAdditive123.Name = "Additive123";
                cbcAdditive123.DataPropertyName = "Additive123";
                dgvEdit.Columns.Remove("Additive123");
                dgvEdit.Columns.Insert(4, cbcAdditive123);
                dgvEdit.Columns["Additive123"].HeaderText = "Additive 1 to 3";
                cbcAdditive123.DisplayStyleForCurrentCellOnly = true;
                cbcAdditive123.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;

                //Set dropdown column for Additive 
                DataGridViewComboBoxColumn cbcAdditive = new DataGridViewComboBoxColumn();
                ds = dal.GetAdditiveCode();
                dt = ds.Tables[0];
                cbcAdditive.DataSource = dt;
                cbcAdditive.ValueMember = "AdditiveID";
                cbcAdditive.DisplayMember = "AdditiveCode";
                cbcAdditive.Name = "AdditiveID";
                cbcAdditive.DataPropertyName = "AdditiveID";
                dgvEdit.Columns.Remove("AdditiveID");
                dgvEdit.Columns.Insert(5, cbcAdditive);
                dgvEdit.Columns["AdditiveID"].HeaderText = "Additive Code";
                cbcAdditive.DisplayStyleForCurrentCellOnly = true;
                cbcAdditive.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;

                dgvEdit.Columns["AdditivePC"].HeaderText = "Additive %";
                dgvEdit.Columns["AdditivePC"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvEdit.Columns["Additive123"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvEdit.EditingControlShowing +=
                   new DataGridViewEditingControlShowingEventHandler(dgvEdit_EditingControlShowing);
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvEdit_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            if (dgvEdit.CurrentCell.ColumnIndex == dgvEdit.Columns["AdditivePC"].Index) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    // Remove an existing event-handler, if present, to avoid 
                    // adding multiple handlers when the editing control is reused.
                    tb.KeyPress -= new KeyPressEventHandler(IntControl_KeyPress);

                    // Add the event handler
                    tb.KeyPress += new KeyPressEventHandler(IntControl_KeyPress);
                }
            }
        }

        private void IntControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void SaveGrid()
        {
            try
            {
                if (dsAdditive != null)
                {
                    if (dgvEdit.IsCurrentRowDirty)
                    {
                        this.Validate();
                    }
                    dgvEdit.EndEdit();
                    MainFormDAL dal = new MainFormDAL();
                    dal.UpdateAdditiveComp(dsAdditive);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Additive_Shown(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void Additive_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveGrid();
        }

        private void dgvEdit_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Confirm Delete?", "Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void dgvEdit_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
           
        }
    }
}
