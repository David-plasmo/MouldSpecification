using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static Utils.DrawingUtils;

namespace MouldSpecification
{
    public partial class Packaging : Form
    {
        DataSet dsPackaging;
        int? curItemID = null;
        int? curCustID = null;
        Size screenRes = ScreenRes();
        BindingManagerBase bindingManager;
        DataGridViewImageCell currentBrowserCell;

        public Packaging(int? itemID, int? custID)
        {
            InitializeComponent();
            if (itemID != null) { curItemID = itemID; }
            if (custID != null) { curCustID = custID; }
        }

        public Packaging()
        {
           
        }


        private void RefreshGrid()
        {
            try
            {
                dgvEdit.AllowUserToAddRows = false;
                dgvEdit.EditingControlShowing += dgvEdit_EditingControlShowing;
                MainFormDAL dal = new MainFormDAL();
                dsPackaging = new PackagingDAL().SelectPackaging(curItemID, curCustID);
                DataGridViewCellStyle style = dgvEdit.ColumnHeadersDefaultCellStyle;
                style.BackColor = Color.Navy;
                style.ForeColor = Color.White;
                style.Font = new Font(dgvEdit.Font, FontStyle.Bold);
                dgvEdit.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                dgvEdit.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                dgvEdit.GridColor = SystemColors.ActiveBorder;
                dgvEdit.EnableHeadersVisualStyles = false;
                dgvEdit.AutoGenerateColumns = true;
                dgvEdit.DataSource = dsPackaging.Tables[0];
                dgvEdit.Columns["PackingID"].Visible = false;
                dgvEdit.Columns["ItemID"].Visible = false;
                dgvEdit.Columns["last_updated_by"].Visible = false;
                dgvEdit.Columns["last_updated_on"].Visible = false;
                dgvEdit.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                dgvEdit.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
                dgvEdit.RowHeadersWidth = Convert.ToInt32(26 * screenRes.Width / 96);

                //setup dropdown columns for code and description
                DataGridViewComboBoxColumn cbcCode = new DataGridViewComboBoxColumn();
                //DataSet dsManItemIndex = dal.SelectMAN_ItemIndex(null);
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

                //set button column to show file browser prompt button
                DataGridViewImageColumn ic = new DataGridViewImageColumn();
                ic.Name = "FilePrompt1";
                ic.DataPropertyName = "FilePrompt1";
                ic.Width = Convert.ToInt32(26 * screenRes.Width / 96);
                ic.Image = new Bitmap(1, 1);
                dgvEdit.Columns.Insert(4, ic);
                dgvEdit.Columns["FilePrompt1"].HeaderText = "";
                dgvEdit.Columns["FilePrompt1"].DefaultCellStyle.NullValue = null;

                dgvEdit.Columns["Code"].ReadOnly = true;
                dgvEdit.Columns["Description"].ReadOnly = true;
                dgvEdit.Columns["Code"].DefaultCellStyle.BackColor = Color.LightGray;
                dgvEdit.Columns["Description"].DefaultCellStyle.BackColor = Color.LightGray;

                dgvEdit.Columns["PackedInCtn"].HeaderText = "Packed in Ctn";
                dgvEdit.Columns["CtnType"].HeaderText = "Ctn Type";
                dgvEdit.Columns["CtnQty"].HeaderText = "Ctn Qty";
                dgvEdit.Columns["InnerBag"].HeaderText = "Inner Bag";
                dgvEdit.Columns["BagQty"].HeaderText = "Bag Qty";
                dgvEdit.Columns["PackingStyle"].HeaderText = "Packing Style";
                dgvEdit.Columns["PackedOnPallet"].HeaderText = "Packed On Pallet";
                dgvEdit.Columns["PalletType"].HeaderText = "Pallet Type";
                dgvEdit.Columns["PalQty"].HeaderText = "Pal Qty";
                dgvEdit.Columns["CtnsPerPallet"].HeaderText = "Ctns/Pallet";
                dgvEdit.Columns["PalletCover"].HeaderText = "Pallet Cover";
                dgvEdit.Columns["LabelInnerBag"].HeaderText = "Label Inner Bag";
                dgvEdit.Columns["BarcodeLabel"].HeaderText = "Barcode Label";
                dgvEdit.Columns["PackerQCInstructions"].HeaderText = "Packer QC Instructions 1";
                dgvEdit.Columns["PackerQCInstruction2"].HeaderText = "Packer QC Instructions 2";
                dgvEdit.Columns["PackerQCInstruction3"].HeaderText = "Packer QC Instructions 3";
                dgvEdit.Columns["PackerQCInstruction4"].HeaderText = "Packer QC Instructions 4";
                dgvEdit.Columns["PackerQCInstruction5"].HeaderText = "Packer QC Instructions 5";
                dgvEdit.Columns["PackingImage1"].HeaderText = "Packing Image 1";
                dgvEdit.Columns["PackingImage2"].HeaderText = "Packing Image 2";
                dgvEdit.Columns["PackingImage3"].HeaderText = "Packing Image 3";
                dgvEdit.Columns["ReworkInstructions1"].HeaderText = "Rework Instructions 1";
                dgvEdit.Columns["ReworkInstructions2"].HeaderText = "Rework Instructions 2";
                dgvEdit.Columns["AssemblyInstructions"].HeaderText = "Assembly Instructions";
                dgvEdit.Columns["AssemblyImage1"].HeaderText = "Assembly Image 1";
                dgvEdit.Columns["AssemblyImage2"].HeaderText = "Assembly Image 2";
                dgvEdit.Columns["AssemblyImage3"].HeaderText = "Assembly Image 3";
                dgvEdit.Columns["AssemblyImage4"].HeaderText = "Assembly Image 4";
                dgvEdit.Columns["AssemblyImage5"].HeaderText = "Assembly Image 5";
                dgvEdit.Columns["AssemblyImage6"].HeaderText = "Assembly Image 6";

                dgvEdit.Columns["CtnQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvEdit.Columns["BagQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvEdit.Columns["PalQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvEdit.Columns["CtnsPerPallet"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                bindingManager = this.BindingContext[dgvEdit.DataSource];
                bindingManager.PositionChanged += new System.EventHandler(RowChanged);
                RowChanged(dgvEdit, EventArgs.Empty);

                dgvEdit.EditingControlShowing += dgvEdit_EditingControlShowing;
                dgvEdit.CellClick += dgvEdit_CellClick;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvEdit_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (e.ColumnIndex >= 0 && dgvEdit.Columns[e.ColumnIndex].Name == "FilePrompt1") //DGV_ImageColumn
                //{
                //    //if (dgvDetail != null && dgvDetail.Visible)
                //    //    CollapseDetail();
                //    DataGridViewImageCell ic = (DataGridViewImageCell)dgvEdit.CurrentCell;
                //    currentBrowserCell = ic;

                //    //Pass current grid row to Attachments form for editing
                //    PackagingDC dc = new PackagingDC();
                //    if (dgvEdit.CurrentRow != null)
                //    {
                //        dc.PackingImage1 = dgvEdit.CurrentRow.Cells["PackingImage1"].Value.ToString();
                //        dc.PackingImage2 = dgvEdit.CurrentRow.Cells["PackingImage2"].Value.ToString();
                //        dc.PackingImage3 = dgvEdit.CurrentRow.Cells["PackingImage3"].Value.ToString();
                //        dc.AssemblyImage1 = dgvEdit.CurrentRow.Cells["AssemblyImage1"].Value.ToString();
                //        dc.AssemblyImage2 = dgvEdit.CurrentRow.Cells["AssemblyImage2"].Value.ToString();
                //        dc.AssemblyImage3 = dgvEdit.CurrentRow.Cells["AssemblyImage3"].Value.ToString();
                //        dc.AssemblyImage4 = dgvEdit.CurrentRow.Cells["AssemblyImage4"].Value.ToString();
                //        dc.AssemblyImage5 = dgvEdit.CurrentRow.Cells["AssemblyImage5"].Value.ToString();
                //        dc.AssemblyImage6 = dgvEdit.CurrentRow.Cells["AssemblyImage6"].Value.ToString();

                //    }
                //    PackagingAttachment f = new PackagingAttachment(dc);
                //    //if (f.DialogResult == DialogResult.OK)
                //    if (f.ShowDialog(this) == DialogResult.OK)
                //    {
                //        //Pass Attachment form edits back to current grid row
                //        dgvEdit.CurrentRow.Cells["PackingImage1"].Value = dc.PackingImage1;
                //        dgvEdit.CurrentRow.Cells["PackingImage2"].Value = dc.PackingImage2;
                //        dgvEdit.CurrentRow.Cells["PackingImage3"].Value = dc.PackingImage3;
                //        dgvEdit.CurrentRow.Cells["AssemblyImage1"].Value = dc.AssemblyImage1;
                //        dgvEdit.CurrentRow.Cells["AssemblyImage2"].Value = dc.AssemblyImage2;
                //        dgvEdit.CurrentRow.Cells["AssemblyImage3"].Value = dc.AssemblyImage3;
                //        dgvEdit.CurrentRow.Cells["AssemblyImage4"].Value = dc.AssemblyImage4;
                //        dgvEdit.CurrentRow.Cells["AssemblyImage5"].Value = dc.AssemblyImage5;
                //        dgvEdit.CurrentRow.Cells["AssemblyImage6"].Value = dc.AssemblyImage6;
                //    }

                //    f.Dispose();

                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RowChanged(object sender, System.EventArgs e)
        {
            try
            {
                //Console.WriteLine("RowChanged " + bindingManager.Position.ToString());                                   
                bool newRow = bindingManager.Count > ((DataTable)dgvEdit.DataSource).Rows.Count;
                bool rowOK = false;
                foreach (DataGridViewRow row in dgvEdit.Rows)
                {
                    rowOK = false;
                    DataGridViewImageCell ic = (DataGridViewImageCell)dgvEdit.Rows[row.Index].Cells["FilePrompt1"];
                    ic.Value = new Bitmap(1, 1);
                    //ic = (DataGridViewImageCell)dgvEdit.Rows[row.Index].Cells["FilePrompt2"];
                    //ic.Value = new Bitmap(1, 1);

                    if (!newRow && row.Index == bindingManager.Position)
                    {
                        rowOK = true;
                    }
                    else if (newRow && row.Index == bindingManager.Position)
                    {
                        //rowOK = false;
                        rowOK = true;
                    }
                    if (rowOK)
                    {
                        ic = (DataGridViewImageCell)dgvEdit.Rows[bindingManager.Position].Cells["FilePrompt1"];
                        ic.Tag = ButtonOp.Browse;
                        Size size = ic.Size;
                        ic.Value = GetImage((ButtonOp)ic.Tag, size.Width, size.Height - 2);
                        //ic = (DataGridViewImageCell)dgvEdit.Rows[bindingManager.Position].Cells["FilePrompt2"];
                        //ic.Tag = ButtonOp.Browse;
                        //size = ic.Size;
                        //ic.Value = GetImage((ButtonOp)ic.Tag, size.Width, size.Height - 2);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvEdit_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SaveGrid()
        {
            try
            {
                if (dsPackaging != null)
                {
                    if (dgvEdit.IsCurrentRowDirty)
                    {
                        this.Validate();
                    }
                    dgvEdit.EndEdit();
                    new PackagingDAL().UpdatePackaging(dsPackaging);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Packaging_Shown(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void Packaging_FormClosed(object sender, FormClosedEventArgs e)
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
