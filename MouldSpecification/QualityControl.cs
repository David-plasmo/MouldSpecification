using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static Utils.DrawingUtils;

namespace MouldSpecification
{
    public partial class QualityControl : Form
    {
        DataSet dsQualityControl;
        int? curItemID = null;
        int? curCustID = null;
        Size screenRes = ScreenRes();
        BindingManagerBase bindingManager;
        DataGridViewImageCell currentBrowserCell;

        public QualityControl(int? itemID, int? custID)
        {
            InitializeComponent();
            if (itemID != null) { curItemID = itemID; }
            if (custID != null) { curCustID = custID; }

            this.curCustID = curCustID;
        }

        public QualityControl()
        {
           
        }

        private void RefreshGrid()
        {
            try
            {
                dgvEdit.AllowUserToAddRows = false;
                MainFormDAL dal = new MainFormDAL();
                dsQualityControl = new QualityControlDAL().SelectQualityControl(curItemID, curCustID);
                DataGridViewCellStyle style = dgvEdit.ColumnHeadersDefaultCellStyle;
                style.BackColor = Color.Navy;
                style.ForeColor = Color.White;
                style.Font = new Font(dgvEdit.Font, FontStyle.Bold);
                dgvEdit.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                dgvEdit.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                dgvEdit.GridColor = SystemColors.ActiveBorder;
                dgvEdit.EnableHeadersVisualStyles = false;
                dgvEdit.AutoGenerateColumns = true;
                dgvEdit.DataSource = dsQualityControl.Tables[0];
                dgvEdit.Columns["QualityControlID"].Visible = false;
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

                //set button columns to show file browser prompt button
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

                dgvEdit.Columns["FinishedPTQC"].HeaderText = "Finished PT & QC";
                dgvEdit.Columns["ProductSample"].HeaderText = "Product Sample";
                dgvEdit.Columns["CertificateOfConformance"].HeaderText = "Certificate of Conformance";
                dgvEdit.Columns["QCInstruction1"].HeaderText = "QC Instruction 1";
                dgvEdit.Columns["QCInstruction2"].HeaderText = "QC Instruction 2";
                dgvEdit.Columns["QCInstruction3"].HeaderText = "QC Instruction 3";
                dgvEdit.Columns["QCInstruction4"].HeaderText = "QC Instruction 4";
                dgvEdit.Columns["QCImage1"].HeaderText = "QC Image 1";
                dgvEdit.Columns["QCImage2"].HeaderText = "QC Image 2";
                dgvEdit.Columns["QCImage3"].HeaderText = "QC Image 3";
                dgvEdit.Columns["QCImage4"].HeaderText = "QC Image 4";
                dgvEdit.Columns["LabelIcon"].HeaderText = "Label Icon";
                dgvEdit.Columns["SpecialInstructionDoc"].HeaderText = "Special Instruction Doc";



                dgvEdit.CellClick += dgvEdit_CellClick;

                bindingManager = this.BindingContext[dgvEdit.DataSource];
                bindingManager.PositionChanged += new System.EventHandler(RowChanged);
                RowChanged(dgvEdit, EventArgs.Empty);

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
                //    QualityControlDC dc = new QualityControlDC();
                //    if (dgvEdit.CurrentRow != null)
                //    {
                //        dc.QCImage1 = dgvEdit.CurrentRow.Cells["QCImage1"].Value.ToString();
                //        dc.QCImage2 = dgvEdit.CurrentRow.Cells["QCImage2"].Value.ToString();
                //        dc.QCImage3 = dgvEdit.CurrentRow.Cells["QCImage3"].Value.ToString();
                //        dc.QCImage4 = dgvEdit.CurrentRow.Cells["QCImage4"].Value.ToString();
                //        dc.LabelIcon = dgvEdit.CurrentRow.Cells["LabelIcon"].Value.ToString();
                //        dc.SpecialInstructionDoc = dgvEdit.CurrentRow.Cells["SpecialInstructionDoc"].Value.ToString();
                //    }
                //    QCAttachments f = new QCAttachments(dc);
                //    //if (f.ShowDialog(this).DialogResult == DialogResult.OK
                //    if (f.ShowDialog(this) == DialogResult.OK)
                //    {
                //        dgvEdit.CurrentRow.Cells["QCImage1"].Value = dc.QCImage1;
                //        dgvEdit.CurrentRow.Cells["QCImage2"].Value = dc.QCImage2;
                //        dgvEdit.CurrentRow.Cells["QCImage3"].Value = dc.QCImage3;
                //        dgvEdit.CurrentRow.Cells["QCImage4"].Value = dc.QCImage4;
                //        dgvEdit.CurrentRow.Cells["LabelIcon"].Value = dc.LabelIcon;
                //        dgvEdit.CurrentRow.Cells["SpecialInstructionDoc"].Value = dc.SpecialInstructionDoc;
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

        private void SaveGrid()
        {
            try
            {
                if (dsQualityControl != null)
                {
                    if (dgvEdit.IsCurrentRowDirty)
                    {
                        this.Validate();
                    }
                    dgvEdit.EndEdit();
                    new QualityControlDAL().UpdateQualityControl(dsQualityControl);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void QualityControl_Shown(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void QualityControl_FormClosed(object sender, FormClosedEventArgs e)
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
