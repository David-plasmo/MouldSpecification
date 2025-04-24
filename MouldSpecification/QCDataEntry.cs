using MouldSpecification.Properties;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static Utils.DrawingUtils;



namespace MouldSpecification
{
    public partial class QCDataEntry : Form
    {
        //identifiers for last edited product and customer
        public int? LastItemID { get; set; }
        public int? LastCustomerID { get; set; }
        public bool CustomerFilterOn { get; set; }

        //allows navigation to another form via menustrip
        public string NextForm { get; set; }

        ToolStripLabel tslCompany;
        ToolStripComboBox tscboCompany;
        ToolStripLabel tslProduct;
        ToolStripComboBox tscboProduct;
        ToolStripButton tsbtnReport;

        int maxRows = 2;
        bool ignoreZero = false;  //causes tscboCompany SelectedIndexChanged event to exit immediately, when true and index = 0;

        DataSet dsQCInstruction;
        BindingSource bsManItems, bsProductGradeItem, bsCustomerProducts, bsCustomer, bsQCInstruction;

        public QCDataEntry(int? lastItemID, int? lastCustomerID, bool customerFilterOn = false)
        {
            InitializeComponent();
            LastItemID = lastItemID;
            LastCustomerID = lastCustomerID;
            NextForm = this.Name;
            CustomerFilterOn = customerFilterOn;

            tsbtnAccept.Click += tsbtnAccept_Click;
            tsbtnCancel.Click += tsbtnCancel_Click;

            this.SuspendLayout();
            tslCompany = new ToolStripLabel() { Text = "Company" };
            tslProduct = new ToolStripLabel() { Text = "Product" };
            tscboCompany = new ToolStripComboBox();
            tscboProduct = new ToolStripComboBox();
            //tscboEntryForm = new ToolStripComboBox() { Text = "Product Specification" }; ;
            tsbtnReport = new ToolStripButton() { Text = "Report" };

            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                tslCompany,
                tscboCompany,
                tslProduct,
                tscboProduct,
                //tscboEntryForm,
                tsbtnReport
            });

            //tsbtnAccept.Click += tsbtnAccept_Click;
            //tsbtnCancel.Click += tsbtnCancel_Click;
            //tsbtnReload.Click += tsbtnReload_Click;
            //tsbtnReport.Click += tsbtnReport_Click;

            foreach (RowStyle style in this.tableLayoutPanel1.RowStyles)
            {
                if (style.SizeType == SizeType.Absolute)
                    style.Height = p96H(19);
            }

            this.ResumeLayout();
        }

        public QCDataEntry()
        {
            
        }

        private void TsbtnAccept_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void tsbtnAccept_Click(object sender, EventArgs e)
        {
            DoSave();
            this.DialogResult = DialogResult.Retry;
            this.Close();
        }

        private void tsbtnCancel_Click(object sender, EventArgs e)
        {

            bsQCInstruction.CancelEdit();            
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void QCDataEntry_Shown(object sender, EventArgs e)
        {
            Rectangle r = new Rectangle(5, 5, p96W(1000), p96H(1100));
            this.DesktopBounds = r;
            bindingNavigator1.Height = p96H(30);
            splitContainer1.SplitterDistance = p96H(55);
            splitContainer2.SplitterDistance = p96H(25);

            this.menuStrip1.Focus();
            this.qCInstructionsToolStripMenuItem.Select();
            //RefreshCurrent();
            SetFormState();
        }

        private void SetFormState()
        {
            try
            {
                if (CustomerFilterOn && LastCustomerID.HasValue && LastItemID.HasValue)
                {
                    //position to last selected customer and product
                    tscboCompany.ComboBox.SelectedIndexChanged -= tscboCompany_SelectedIndexChanged;
                    tscboCompany.ComboBox.SelectedValue = LastCustomerID;
                    SetProductFilter(LastCustomerID.Value, LastItemID.Value);
                    tscboCompany.ComboBox.SelectedIndexChanged += tscboCompany_SelectedIndexChanged;
                    RefreshCurrent();
                }
                else
                {
                    if (LastItemID.HasValue)
                    {
                        //position to last selected product, without customer filter
                        tscboCompany.ComboBox.SelectedIndexChanged -= tscboCompany_SelectedIndexChanged;
                        tscboCompany.ComboBox.SelectedIndex = -1;
                        tscboProduct.ComboBox.SelectedIndexChanged -= tscboProduct_SelectedIndexChanged;
                        tscboProduct.ComboBox.SelectedIndex = -1;
                        int lastItemID = LastItemID.Value;
                        tscboProduct.ComboBox.SelectedIndexChanged += tscboProduct_SelectedIndexChanged;
                        tscboProduct.ComboBox.SelectedValue = lastItemID;
                        tscboCompany.ComboBox.SelectedIndexChanged += tscboCompany_SelectedIndexChanged;
                        RefreshCurrent();
                        //SetComboBoxSelectionByValue(lastItemID);
                    }
                    else
                    {
                        //position to first product with no product filter
                        tscboCompany.ComboBox.SelectedIndexChanged -= tscboCompany_SelectedIndexChanged;
                        tscboProduct.ComboBox.SelectedIndex = -1;
                        tscboCompany.ComboBox.SelectedIndex = -1;
                        tscboCompany.ComboBox.SelectedIndexChanged += tscboCompany_SelectedIndexChanged;
                        tscboProduct.ComboBox.SelectedIndexChanged += tscboProduct_SelectedIndexChanged;
                        tscboProduct.ComboBox.SelectedIndex = 0;
                        RefreshCurrent();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void productSpecificationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoSave();
            NextForm = "SpecificationDataEntry";
            this.DialogResult = DialogResult.Retry;

        }

        //private void productAssemblyToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    DoSave();
        //    NextForm = "AssemblyDataEntry";
        //    this.DialogResult = DialogResult.Retry;
        //}

        private void qCInstructionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void productPackagingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoSave();
            NextForm = "PackagingDataEntry";
            this.DialogResult = DialogResult.Retry;
        }

        private void attachedDocumentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoSave();
            NextForm = "AttachedDocsDataEntry";
            this.DialogResult = DialogResult.Retry;
        }

        private void QCDataEntry_Load(object sender, EventArgs e)
        {
            //tsbtnDelete.Click += tsbtnDelete_Click;
            //tsbtnAddNew.Click += tsbtnAddNew_Click;
            tsbtnDelete.Enabled = false;
            tsbtnAddNew.Enabled = false;
            tsbtnReport.Enabled = false;
            tsbtnReport.Click += tsbtnReport_Click;

            lblItemID.Visible = false;

            tscboCompany.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            tscboCompany.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            tscboCompany.DropDownHeight = 400;
            tscboCompany.DropDownWidth = 300;
            tscboCompany.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            tscboCompany.IntegralHeight = false;
            tscboCompany.MaxDropDownItems = 9;
            tscboCompany.MergeAction = System.Windows.Forms.MergeAction.Insert;
            tscboCompany.Name = "tscboCompany";
            tscboCompany.Size = new System.Drawing.Size(p96W(250), p96H(25));
            tscboCompany.Sorted = true;

            tscboProduct.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            tscboProduct.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            tscboProduct.DropDownHeight = 400;
            tscboProduct.DropDownWidth = 200;
            tscboProduct.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            tscboProduct.IntegralHeight = false;
            tscboProduct.MaxDropDownItems = 9;
            tscboProduct.MergeAction = System.Windows.Forms.MergeAction.Insert;
            tscboProduct.Name = "tscboProduct";
            tscboProduct.Size = new System.Drawing.Size(p96W(200), p96H(25));
            tscboProduct.Sorted = true;

            btnQCInstructionNewRow.Size = new Size(p96W(24), p96H(20));
            System.Drawing.Image image = Properties.Resources.NewRow;
            btnQCInstructionNewRow.Image = RescaleImage((Bitmap)image, btnQCInstructionNewRow.Width, btnQCInstructionNewRow.Height);
            btnQCInstructionNewRow.Click += btnQCInstructionNewRow_Click;
            
            gpQCInstruction.Size = new Size(p96W(707), p96H(550));

            GetDataSets();
            BindControls();

            //this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
            //this.errorProvider1.DataMember = null;

            //if (LastItemID.HasValue)
            //{
            //    tscboProduct.SelectedIndexChanged += tscboProduct_SelectedIndexChanged;
            //    tscboProduct.ComboBox.SelectedValue = (int)LastItemID;
            //}
        }

        private void DoSave()
        {
            bsManItems.CurrentChanged -= bsManItems_CurrentChanged; //form is about to close;  don't want to refresh
            bsQCInstruction.EndEdit();
            dgvQCInstruction.EndEdit();
            QCInstructionDAL qCInstructionDAL = new QCInstructionDAL();
            qCInstructionDAL.UpdateFromPivotTable(dsQCInstruction);
        }


        private void btnQCInstructionNewRow_Click(object sender, EventArgs e)
        {
            // MessageBox.Show("todo:  add new QCInstruction row");                        
            bsQCInstruction.AddNew();
            bsQCInstruction.EndEdit();
            bsQCInstruction.Position = bsQCInstruction.Count - 1;
            DataTable ct = dsQCInstruction.Relations["ItemQCInstruction"].ChildTable;
            DataRow[] foundRows = ct.Select("ItemID1 = " + LastItemID.ToString());
            int count = foundRows.Length;
            btnQCInstructionNewRow.Enabled = (count < maxRows);
            lblQCInstructionNewRow.Enabled = (count < maxRows);
        }

        private void FormatQCInstruction()
        {
            //format dgvQCInstruction
            //https://stackoverflow.com/questions/30200529/merge-datagridview-image-cell-with-text-cell
            dgvQCInstruction.CellFormatting += dgvQCInstruction_CellFormatting;
            dgvQCInstruction.CellClick += dgvQCInstruction_CellClick;

            dgvQCInstruction.AllowUserToAddRows = false;

            dgvQCInstruction.SelectionMode = DataGridViewSelectionMode.CellSelect;
            //dgvQCInstruction.Width = p96H(600);
            dgvQCInstruction.Height = p96H(200);
            dgvQCInstruction.RowHeadersWidth = p96H(26);
            dgvQCInstruction.ClearSelection();

            dgvQCInstruction.ColumnHeadersHeight = p96H(19);
            dgvQCInstruction.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvQCInstruction.AllowUserToResizeRows = false;
            dgvQCInstruction.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            DataGridViewCellStyle style = dgvQCInstruction.ColumnHeadersDefaultCellStyle;
            style.BackColor = Color.RosyBrown;
            style.ForeColor = Color.MidnightBlue;
            style.Font = new Font(dgvQCInstruction.Font, FontStyle.Regular);
            dgvQCInstruction.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgvQCInstruction.ColumnHeadersDefaultCellStyle.BackColor;
            dgvQCInstruction.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            dgvQCInstruction.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgvQCInstruction.GridColor = SystemColors.ActiveBorder;
            dgvQCInstruction.EnableHeadersVisualStyles = false;
            dgvQCInstruction.AutoGenerateColumns = true;

            //insert Image column; assigned dynamically in CellFormat event
            DataGridViewImageColumn bc = new DataGridViewImageColumn();
            bc.Name = "QCImage1";
            bc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            dgvQCInstruction.Columns.Add(bc);

            bc = new DataGridViewImageColumn();
            bc.Name = "QCImage2";
            bc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            dgvQCInstruction.Columns.Add(bc);

            dgvQCInstruction.RowTemplate.Height = p96H(170);
            dgvQCInstruction.Columns["QCImage1"].Width = p96W(200);
            dgvQCInstruction.Columns["QCImage2"].Width = p96W(200);
            dgvQCInstruction.Columns["QCInstruction1"].Width = p96W(150);
            dgvQCInstruction.Columns["QCInstruction2"].Width = p96W(150);
            dgvQCInstruction.Columns["QCInstruction1"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvQCInstruction.Columns["QCInstruction1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            dgvQCInstruction.Columns["QCInstruction2"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvQCInstruction.Columns["QCInstruction2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;

            dgvQCInstruction.Columns["QCInstructionID1"].Visible = false;
            dgvQCInstruction.Columns["QCInstructionID2"].Visible = false;
            dgvQCInstruction.Columns["InstructionNo1"].Visible = false;
            dgvQCInstruction.Columns["InstructionNo2"].Visible = false;
            dgvQCInstruction.Columns["QCInstruction1"].Visible = true;
            dgvQCInstruction.Columns["QCInstruction2"].Visible = true;
            dgvQCInstruction.Columns["ItemID1"].Visible = false;
            dgvQCInstruction.Columns["ItemID2"].Visible = false;
            dgvQCInstruction.Columns["QCImageFilepath1"].Visible = false;
            dgvQCInstruction.Columns["QCImageFilepath2"].Visible = false;
            dgvQCInstruction.Columns["QCImage1"].Visible = true;
            dgvQCInstruction.Columns["QCImage2"].Visible = true;

            dgvQCInstruction.Columns["QCInstruction1"].DisplayIndex = 0;
            dgvQCInstruction.Columns["QCImage1"].DisplayIndex = 1;
            dgvQCInstruction.Columns["QCInstruction1"].DisplayIndex = 2;
            dgvQCInstruction.Columns["QCImage2"].DisplayIndex = 3;

            dgvQCInstruction.Columns["QCInstructionID1"].DisplayIndex = 4;
            dgvQCInstruction.Columns["QCInstructionID2"].DisplayIndex = 5;
            dgvQCInstruction.Columns["InstructionNo1"].DisplayIndex = 6;
            dgvQCInstruction.Columns["InstructionNo2"].DisplayIndex = 7;
            //dgvQCInstruction.Columns["QCInstruction1"].DisplayIndex = 0;
            //dgvQCInstruction.Columns["QCInstruction2"].DisplayIndex = 0;
            dgvQCInstruction.Columns["ItemID1"].DisplayIndex = 8;
            dgvQCInstruction.Columns["ItemID2"].DisplayIndex = 9;
            dgvQCInstruction.Columns["QCImageFilepath1"].DisplayIndex = 10;
            dgvQCInstruction.Columns["QCImageFilepath2"].DisplayIndex = 11;
            //dgvQCInstruction.Columns["QCImage1"].DisplayIndex = 0;
            //dgvQCInstruction.Columns["QCImage2"].DisplayIndex = 0;


            dgvQCInstruction.CellPainting += dgvQCInstruction_CellPainting;
            dgvQCInstruction.DataBindingComplete += dgvQCInstruction_DataBindingComplete;
            dgvQCInstruction.DataError += dgvQCInstruction_DataError;
            //dgvQCInstruction.DefaultValuesNeeded += dgvQCInstruction_DefaultValuesNeeded;

            //create context menu
            dgvQCInstruction.CellMouseDown += dgvQCInstruction_CellMouseDown;


        }

        private void dgvQCInstruction_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Ignore if a column or row header is clicked
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if (e.Button == MouseButtons.Right)
                {
                    DataGridViewCell clickedCell = (sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex];

                    // Here you can do whatever you want with the cell
                    this.dgvQCInstruction.CurrentCell = clickedCell;  // Select the clicked cell, for instance

                    // Get mouse position relative to the vehicles grid
                    var relativeMousePosition = dgvQCInstruction.PointToClient(Cursor.Position);

                    ContextMenuStrip cms = new ContextMenuStrip();
                    dgvQCInstruction.ContextMenuStrip = cms;
                    if (clickedCell.OwningColumn.Name == "QCImage1" |
                        clickedCell.OwningColumn.Name == "QCImage2")
                    {
                        int fp1Index = dgvQCInstruction.Columns["QCImageFilepath1"].Index;
                        int fp2Index = dgvQCInstruction.Columns["QCImageFilepath2"].Index;
                        string fp1 = dgvQCInstruction.Rows[e.RowIndex].Cells[fp1Index].Value.ToString();
                        string fp2 = dgvQCInstruction.Rows[e.RowIndex].Cells[fp2Index].Value.ToString();
                        if ((clickedCell.OwningColumn.Name == "QCImage1" && fp1.Length > 0) ||
                            (clickedCell.OwningColumn.Name == "QCImage2" && fp2.Length > 0))
                        {
                            cms.Items.Add("&Zoom", Resources.zoom, new System.EventHandler(this.Zoom_Click));
                            cms.Items.Add("&Remove", Resources.remove, new System.EventHandler(this.Remove_Click));
                        }

                        cms.Items.Add("&Browse", Resources.browse, new System.EventHandler(this.Browse_Click));
                        cms.Items.Add(new ToolStripSeparator());
                    }
                    cms.Items.Add("&Delete Row", Resources.delete, new System.EventHandler(this.Delete_Click));

                    // Show the context menu
                    cms.Show(dgvQCInstruction, relativeMousePosition);
                }
            }
        }

        private void Zoom_Click(Object sender, EventArgs e)
        {
            string scName = ((ContextMenuStrip)(((ToolStripMenuItem)sender).Owner)).SourceControl.Name;
            //MessageBox.Show("todo:  Show image;  sender: " + scName);
            int fp1Index = dgvQCInstruction.Columns["QCImageFilepath1"].Index;
            int fp2Index = dgvQCInstruction.Columns["QCImageFilepath2"].Index;
            string fp1 = dgvQCInstruction.CurrentRow.Cells[fp1Index].Value.ToString();
            string fp2 = dgvQCInstruction.CurrentRow.Cells[fp2Index].Value.ToString();
            if (dgvQCInstruction.CurrentCell.OwningColumn.Name == "QCImage1" && fp1.Length > 0)
            {
                //MessageBox.Show("todo:  Show image1;  sender: " + scName);
                if (File.Exists(fp1))
                {
                    Image image = GetImage(fp1, p96W(1200), p96W(800));
                    PicturePopup pp = new PicturePopup(image);
                    pp.Show(this);
                }
                else
                    MessageBox.Show("file not found: " + fp1);
            }
            else if (dgvQCInstruction.CurrentCell.OwningColumn.Name == "QCImage2" && fp2.Length > 0)
            {
                //MessageBox.Show("todo:  Show image2;  sender: " + scName);
                if (File.Exists(fp2))
                {
                    Image image = GetImage(fp1, p96W(1200), p96W(800));
                    PicturePopup pp = new PicturePopup(image);
                    pp.Show(this);
                }
                else
                    MessageBox.Show("file not found: " + fp2);
            }
        }

        private void pb_MouseLeave(object sender, EventArgs e)
        {
            this.Controls["qcPicture"].Visible = false;
            this.Controls["qcPicture"].Dispose();
        }

        private void Browse_Click(Object sender, EventArgs e)
        {
            string scName = ((ContextMenuStrip)(((ToolStripMenuItem)sender).Owner)).SourceControl.Name;
            //MessageBox.Show("todo:  Browse;  sender: " + scName);
            int fp1Index = dgvQCInstruction.Columns["QCImageFilepath1"].Index;
            int fp2Index = dgvQCInstruction.Columns["QCImageFilepath2"].Index;
            string fp1 = dgvQCInstruction.CurrentRow.Cells[fp1Index].Value.ToString();
            string fp2 = dgvQCInstruction.CurrentRow.Cells[fp2Index].Value.ToString();
            string fieldName = null;
            if (dgvQCInstruction.CurrentCell.OwningColumn.Name == "QCImage1")
                fieldName = "QCImageFilepath1";
            else if (dgvQCInstruction.CurrentCell.OwningColumn.Name == "QCImage2")
                fieldName = "QCImageFilepath2";

            MessageBox.Show("todo:  browse " + fieldName + ";  sender: " + scName);
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "QC Instruction Image Files";
            fdlg.InitialDirectory = @"S:CONSOLIDATED PLASTICS\INJECTION MOULDING\Database\Images\";
            fdlg.Filter = "JPG (*.jpg)|*.jpg|PNG (*.png)|*.png|tif (*.tif)|*.tif|Bitmap (*.bmp)|*.bmp|All files (*.*)|*.*";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                string fileSelected = fdlg.FileName;

                BindingManagerBase bm = this.dgvQCInstruction.BindingContext[this.dgvQCInstruction.DataSource, this.dgvQCInstruction.DataMember];
                DataRow dr = ((DataRowView)bm.Current).Row;
                dr[fieldName] = fileSelected;
                dr.EndEdit();
            }            
        }

        private void Remove_Click(Object sender, EventArgs e)
        {
            try
            {
                string scName = ((ContextMenuStrip)(((ToolStripMenuItem)sender).Owner)).SourceControl.Name;
                //MessageBox.Show("todo:  Remove;  sender: " + scName);
                int fp1Index = dgvQCInstruction.Columns["QCImageFilepath1"].Index;
                int fp2Index = dgvQCInstruction.Columns["QCImageFilepath2"].Index;
                string fp1 = dgvQCInstruction.CurrentRow.Cells[fp1Index].Value.ToString();
                string fp2 = dgvQCInstruction.CurrentRow.Cells[fp2Index].Value.ToString();
                if (dgvQCInstruction.CurrentCell.OwningColumn.Name == "QCImage1" && fp1.Length > 0)
                {
                    //MessageBox.Show("todo:  remove image1;  sender: " + scName);
                    BindingManagerBase bm = this.dgvQCInstruction.BindingContext[this.dgvQCInstruction.DataSource, this.dgvQCInstruction.DataMember];
                    DataRow dr = ((DataRowView)bm.Current).Row;
                    dr["QCImageFilePath1"] = DBNull.Value;
                    dr.EndEdit();
                }
                else if (dgvQCInstruction.CurrentCell.OwningColumn.Name == "QCImage2" && fp2.Length > 0)
                {
                    //MessageBox.Show("todo:  remove image2;  sender: " + scName);
                    BindingManagerBase bm = this.dgvQCInstruction.BindingContext[this.dgvQCInstruction.DataSource, this.dgvQCInstruction.DataMember];
                    DataRow dr = ((DataRowView)bm.Current).Row;
                    dr["QCImageFilePath2"] = DBNull.Value;
                    dr.EndEdit();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            
        }

        private void Delete_Click(Object sender, EventArgs e)
        {
            string scName = ((ContextMenuStrip)(((ToolStripMenuItem)sender).Owner)).SourceControl.Name;
            if (MessageBox.Show("Are you Sure? (sender: " + scName + ")", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                BindingManagerBase bm = this.dgvQCInstruction.BindingContext[this.dgvQCInstruction.DataSource, this.dgvQCInstruction.DataMember];
                DataRow dr = ((DataRowView)bm.Current).Row;
                dr.Delete();
                dr.EndEdit();
                btnQCInstructionNewRow.Enabled = (bm.Count < 2);                    
            }
        }

        //private void dgvQCInstruction_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        private void dgvQCInstruction_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                //add tooltip texts
                if (e.RowIndex < 0)
                    return;

                // add space for two lines:
                //dgvQCInstruction.Rows[0].Height = ((Image)dgvQCInstruction[0, 0].Value).Height + 35;
                // if the previous line throws an error..
                // .. because you didn't put a 'real image' into the cell try this:
                // dgvQCInstruction.Rows[0].Height = 
                //((Image)dgvQCInstruction[0, 0].FormattedValue).Height + 35;

                //assign tooltips
                if (dgvQCInstruction.Columns[e.ColumnIndex].Name == "QCImage1" ||
                    dgvQCInstruction.Columns[e.ColumnIndex].Name == "QCImage2") //||
                                                                                //dgvQCInstruction.Columns[e.ColumnIndex].Name == "QCImage3")
                {
                    DataGridViewCell cell =
                    dgvQCInstruction.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.ToolTipText = "Click to browse for image";
                }

                //assign graphic images
                int colIndex1 = dgvQCInstruction.Columns["QCImage1"].Index;
                int colIndex2 = dgvQCInstruction.Columns["QCImage2"].Index;
                //int colIndex3 = dgvQCInstruction.Columns["QCImage3"].Index;

                string filepath = null;
                Image image = null;

                int rowHeight = dgvQCInstruction.RowTemplate.Height;
                int colWidth1 = dgvQCInstruction.Columns["QCImage1"].Width;
                int colWidth2 = dgvQCInstruction.Columns["QCImage2"].Width;
                //int colWidth3 = dgvQCInstruction.Columns["QCImage3"].Width;

                if (e.ColumnIndex == colIndex1)
                {
                    image = EmptyImage();
                    if (dgvQCInstruction.Rows[e.RowIndex].Cells["QCImageFilepath1"].Value != null)
                    {
                        filepath = dgvQCInstruction.Rows[e.RowIndex].Cells["QCImageFilepath1"].Value.ToString();
                        if (File.Exists(filepath))
                            image = GetImage(filepath, colWidth1, rowHeight);
                    }
                    e.Value = image;

                }
                else if (e.ColumnIndex == colIndex2)
                {
                    image = EmptyImage();
                    if (dgvQCInstruction.Rows[e.RowIndex].Cells["QCImageFilepath2"].Value != null)
                    {
                        filepath = dgvQCInstruction.Rows[e.RowIndex].Cells["QCImageFilepath2"].Value.ToString();
                        if (File.Exists(filepath))
                            image = GetImage(filepath, colWidth1, rowHeight);
                    }
                    e.Value = image;
                }
                //else if (e.ColumnIndex == colIndex3)
                //{
                //    image = EmptyImage();
                //    if (dgvQCInstruction.Rows[e.RowIndex].Cells["QCImageFilepath3"].Value != null)
                //    {
                //        filepath = dgvQCInstruction.Rows[e.RowIndex].Cells["QCImageFilepath3"].Value.ToString();
                //        if (File.Exists(filepath))
                //            image = GetImage(filepath, colWidth1, rowHeight);
                //    }
                //    e.Value = image;
                //}
            }
            catch (Exception ex) { }
        }

        private void dgvQCInstruction_CellPainting(object sender,
                                        DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;                  // no image in the header            
            int imageColumn1 = dgvQCInstruction.Columns["QCImage1"].Index;
            int imageColumn2 = dgvQCInstruction.Columns["QCImage2"].Index;
            int instructionColumn1 = dgvQCInstruction.Columns["QCInstruction1"].Index;
            int instructionColumn2 = dgvQCInstruction.Columns["QCInstruction2"].Index;
            int instructionNo1 = dgvQCInstruction.Columns["InstructionNo1"].Index;
            int instructionNo2 = dgvQCInstruction.Columns["InstructionNo2"].Index;

            if (e.ColumnIndex == imageColumn1 || e.ColumnIndex == imageColumn2)
            {
                e.PaintBackground(e.ClipBounds, false);  // no highlighting
                e.PaintContent(e.ClipBounds);

                // calculate the location of your text..:
                int y = e.CellBounds.Bottom - 32;

                string yourText = (e.ColumnIndex == imageColumn1)
                    ? dgvQCInstruction[instructionNo1, e.RowIndex].Value.ToString()
                    //+ ". " + dgvQCInstruction[instructionColumn1, e.RowIndex].Value.ToString()
                    : dgvQCInstruction[instructionNo2, e.RowIndex].Value.ToString();
                //+ ". " + dgvQCInstruction[instructionColumn2, e.RowIndex].Value.ToString();

                Font yourFont = new Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular, GraphicsUnit.Point);
                System.Drawing.Brush yourColor = Brushes.Black;
                e.Graphics.DrawString(yourText, yourFont, yourColor, e.CellBounds.Left, y);

                // maybe draw more text with other fonts etc..
                e.Handled = true;                        // done with the image column 
            }
        }

        private void dgvQCInstruction_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void dgvQCInstruction_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                //MessageBox.Show("todo:  implement CellClick");
                string filepath;
                string fieldName;
                if (dgvQCInstruction.Columns[e.ColumnIndex].Name == "QCImage1")
                {
                    filepath = dgvQCInstruction.CurrentRow.Cells["QCImageFilepath1"].Value.ToString();
                    fieldName = "QCImageFilepath1";
                }
                else if (dgvQCInstruction.Columns[e.ColumnIndex].Name == "QCImage2")
                {
                    filepath = dgvQCInstruction.CurrentRow.Cells["QCImageFilepath2"].Value.ToString();
                    fieldName = "QCImageFilepath2";
                }
                //else if (dgvQCInstruction.Columns[e.ColumnIndex].Name == "QCImage3")
                //{
                //    filepath = dgvQCInstruction.CurrentRow.Cells["QCImageFilepath3"].Value.ToString();
                //    fieldName = "QCImageFilepath3";
                //}
                else
                    return;

                OpenFileDialog fdlg = new OpenFileDialog();
                fdlg.Title = "QC Instruction Image Files";
                fdlg.InitialDirectory = @"S:CONSOLIDATED PLASTICS\INJECTION MOULDING\Database\Images\";
                fdlg.Filter = "JPG (*.jpg)|*.jpg|PNG (*.png)|*.png|tif (*.tif)|*.tif|Bitmap (*.bmp)|*.bmp|All files (*.*)|*.*";
                fdlg.FilterIndex = 1;
                fdlg.RestoreDirectory = true;
                if (fdlg.ShowDialog() == DialogResult.OK)
                {
                    string fileSelected = fdlg.FileName;

                    //DataRowView drv = (DataRowView)bsQCInstruction.Current;
                    //DataRow row = drv.Row;
                    BindingManagerBase bm = this.dgvQCInstruction.BindingContext[this.dgvQCInstruction.DataSource, this.dgvQCInstruction.DataMember];
                    DataRow dr = ((DataRowView)bm.Current).Row;
                    dr[fieldName] = fileSelected;
                    dr.EndEdit();

                }
            }
            catch (Exception ex) { }
        }

        private void dgvQCInstruction_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvQCInstruction.ClearSelection();
        }

        private void tsbtnReport_Click(object sender, EventArgs e)
        {
            try
            {
                InjectionMouldReports.Reports.ViewReport(LastItemID.Value, "QCInstruction.rdlc");

                /*
                //** won't load as a dll
                //IMSpecificationReport f = new IMSpecificationReport();
                //WindowsFormsFramework.ReportPrompt f = new WindowsFormsFramework.ReportPrompt();
                //f.ShowDialog();

                // load as exe
                var appSettings = ConfigurationManager.AppSettings;
                string reportPath = appSettings["IMReportsPath"];
                string appStartPath = System.Windows.Forms.Application.StartupPath;
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = appStartPath + reportPath + @"\InjectionMouldReports.exe";
                //int custID = (int)tscboCompany.ComboBox.SelectedValue;
                //int itemID = (int)tscboProduct.ComboBox.SelectedValue;
                int custID = (int)LastCustomerID;
                int itemID = (int)LastItemID;
                info.Arguments = custID.ToString() + " " + itemID.ToString() + " " + "QCInstruction.rdlc";
                info.UseShellExecute = true;
                info.CreateNoWindow = false;
                info.WorkingDirectory = reportPath;
                Process proc = Process.Start(info);
                proc.WaitForExit();
                proc.Dispose();
                */
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetDataSets()
        {
            try
            {
                SpecificationDataEntryDAL dal = new SpecificationDataEntryDAL();
                dsQCInstruction = dal.BuildQCDataset();
            }
            catch (Exception ex) { }
        }

        private void BindControls()
        {
            try
            {
                //create binding sources
                bsManItems = new BindingSource
                {
                    DataSource = dsQCInstruction,
                    DataMember = "MAN_Items",
                    Sort = "ITEMDESC ASC"
                };

                bsProductGradeItem = new BindingSource();
                bsProductGradeItem.DataSource = dsQCInstruction.Relations["ProductGradeItem"].ParentTable;

                bsCustomerProducts = new BindingSource();
                bsCustomerProducts.DataSource = dsQCInstruction.Tables["CustomerProduct"];

                bsCustomer = new BindingSource();
                bsCustomer.DataSource = dsQCInstruction.Tables["Customer"];

                //bsManItems.AddingNew += bsManItems_AddingNew;
                //DataTable manItems = dsIMSpecificationForm.Tables["MAN_Items"];

                //Navigation toolbar
                bindingNavigator1.BindingSource = new BindingSource();
                bindingNavigator1.BindingSource = bsManItems;
                bsManItems.CurrentChanged += bsManItems_CurrentChanged;

                //Toolbar Customer dropdown
                DataTable dt = dsQCInstruction.Tables["Customer"];
                tscboCompany.ComboBox.DataSource = dt;
                tscboCompany.ComboBox.DisplayMember = "CUSTNAME";
                tscboCompany.ComboBox.ValueMember = "CustomerID";
                tscboCompany.ComboBox.SelectedIndexChanged += tscboCompany_SelectedIndexChanged;

                dt = dsQCInstruction.Tables["Product"];                
                tscboProduct.ComboBox.DataSource = dt;
                tscboProduct.ComboBox.DisplayMember = "ITEMDESC";
                tscboProduct.ComboBox.ValueMember = "ItemID";
                tscboProduct.ComboBox.SelectedIndexChanged += tscboProduct_SelectedIndexChanged;
                //dt.RowChanging += Dt_RowChanging;
                //dt.TableNewRow += Dt_TableNewRow;

                txtITEMNMBR.DataBindings.Add(new Binding("Text", bsManItems, "ITEMNMBR"));
                txtITEMDESC.DataBindings.Add(new Binding("Text", bsManItems, "ITEMDESC"));
                //picImageFile.DataBindings.Add("ImageLocation", bsManItems, "ImageFile");
                //cboGradeID.DataBindings.Add(new Binding("SelectedValue", bsManItems, "GradeID"));
                //coloured label icon

                //picLabelIcon.DataBindings.Add("ImageLocation", bsProductGradeItem, "LabelIcon");
                //txtProductCategory.DataBindings.Add("Text", bsProductGradeItem, "Description");
                lblItemID.DataBindings.Add(new Binding("Text", bsManItems, "ItemID"));
                //lblProductCategory.DataBindings.Add(new Binding("Text", bsManItems, "GradeID"));
                txtCustomer.DataBindings.Add(new Binding("Text", bsCustomer, "CUSTNAME"));

                //QVInstruction image
                bsQCInstruction = new BindingSource();
                bsQCInstruction.AddingNew += bsQCInstruction_AddingNew;
                bsQCInstruction.DataSource = dsQCInstruction.Tables["QCInstruction"];
                dgvQCInstruction.DataSource = bsManItems;
                dgvQCInstruction.DataMember = "ItemQCInstruction";

                //dgvQCInstruction.CellPainting += dgvQCInstruction_CellPainting;
                dgvQCInstruction.DataBindingComplete += dgvQCInstruction_DataBindingComplete;
                dgvQCInstruction.DataError += dgvQCInstruction_DataError;
                FormatQCInstruction();


            }
            catch (Exception ex)
            {

            }
        }

        private void bsQCInstruction_AddingNew(object sender, AddingNewEventArgs e)
        {
            try
            {
                DataRowView rowView = (DataRowView)this.bsManItems.Current;
                DataRow row = rowView.Row;
                //MessageBox.Show(row["ItemID"].ToString());
                int itemID = (int)row["ItemID"];
                DataTable dt = (DataTable)bsQCInstruction.DataSource;
                dt.Columns["ItemID1"].DefaultValue = itemID;
                dt.Columns["ItemID2"].DefaultValue = itemID;
                dt.Columns["QCInstructionID1"].DefaultValue = -1;
                dt.Columns["QCInstructionID2"].DefaultValue = -1;

                DataTable ct = dsQCInstruction.Relations["ItemQCInstruction"].ChildTable;
                DataRow[] foundRows = ct.Select("ItemID1 = " + itemID.ToString());
                int count = foundRows.Length;
                int instructionNo = (count + 1) * 2 - 1;  //creates sequence of 1,3,5
                dt.Columns["InstructionNo1"].DefaultValue = instructionNo;
                dt.Columns["InstructionNo2"].DefaultValue = instructionNo + 1;

                

                btnQCInstructionNewRow.Enabled = (count < maxRows);
                lblQCInstructionNewRow.Enabled = (count < maxRows);

            }
            catch (Exception ex) { }
        }

        private void tscboCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tscboCompany.ComboBox.SelectedIndex == 0 && ignoreZero || tscboCompany.ComboBox.SelectedValue == null )
                return; 
            else
            {
                int custID = (int)tscboCompany.ComboBox.SelectedValue;
                SetProductFilter(custID);
            }
        }

        private void tscboProduct_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (tscboProduct.SelectedIndex == 0 && ignoreZero || tscboProduct.ComboBox.SelectedValue == null)
                return;
            else
            {
                int itemID = (int)tscboProduct.ComboBox.SelectedValue;
                int itemIndex = bsManItems.Find("ItemID", itemID);
                if (itemID != -1)
                {
                    tsbtnReport.Enabled = true;
                    bsManItems.Position = itemIndex;
                    //RefreshCurrent();
                }
            }
        }

        private void SetProductFilter(int custID, int itemID = 0)
        {
            try
            {
                CustomerFilterOn = true;
                //DataTable dt = dsIMSpecificationForm.Tables["CustomerProduct"].Copy();
                DataTable dt = (DataTable)bsCustomerProducts.DataSource;
                DataView dv = new DataView(dt, "CustomerID = " + custID.ToString(), "CustomerID", DataViewRowState.CurrentRows);
                DataTable dt1 = dv.ToTable();
                DataTable dt2 = dsQCInstruction.Tables["Product"];
                dt2.DefaultView.RowFilter = "";
                if (dt1.Rows.Count == 0)
                {
                    MessageBox.Show("This customer has no products. You may wish to add this customer for an existing product",
                                            "Add customer for product", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    EnableGroups(false);
                }
                else
                {
                    var ids = dt1.AsEnumerable().Select(r => r.Field<int>("ItemID"));
                    dt2.DefaultView.RowFilter = string.Format("ItemID in ({0})", string.Join(",", ids));
                }
                tscboProduct.SelectedIndexChanged -= tscboProduct_SelectedIndexChanged;
                tscboProduct.ComboBox.DataBindings.Clear();
                tscboProduct.ComboBox.SelectedIndex = -1;
                ignoreZero = true;  //exits SelectedIndexChanged 
                tscboProduct.ComboBox.DataSource = dt2;
                tscboProduct.ComboBox.ValueMember = "ItemID";
                tscboProduct.ComboBox.DisplayMember = "ITEMDESC";

                tscboProduct.ComboBox.SelectedIndexChanged += tscboProduct_SelectedIndexChanged;
                if (dt1.Rows.Count > 0)
                {
                    ignoreZero = false;
                    if (bsManItems == null)
                    {
                        BindControls();
                        FormatQCInstruction();
                    }
                    EnableGroups(true);
                    bsManItems.Filter = dt2.DefaultView.RowFilter;
                    if (itemID != 0)
                        if (dt2.DefaultView.RowFilter.Contains(itemID.ToString()))
                        {
                            tscboProduct.ComboBox.SelectedValue = itemID;
                        }
                        else
                            tscboProduct.ComboBox.SelectedIndex = 0;
                    else
                        //position to first item within selected company
                        tscboProduct.ComboBox.SelectedIndex = 0;
                }
                //bsManItems.Filter = dv2.RowFilter;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SetProductFilter", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EnableGroups(bool flag)
        {
            gpGeneral.Enabled = flag;
            gpQCInstruction.Enabled = flag;
            dgvQCInstruction.Visible = flag;            
        }


        private void bsManItems_CurrentChanged(object sender, EventArgs e)
        {
            RefreshCurrent();

            /*
            try
            {                
                DataRowView drv = (DataRowView)this.bsManItems.Current;
                DataRow dr = drv.Row;

                if (!drv.IsNew)
                {
                    //bindingNavigator sometimes loses primary key value;
                    //happens when clicking between MoveNext, MovePrevious or MoveFirst, MoveLast
                    //Handle by resetting key to original value
                    if (dr.HasVersion(DataRowVersion.Original))
                    {
                        if (dr["ItemID", DataRowVersion.Current].ToString().Length == 0 &&
                        dr["ItemID", DataRowVersion.Original].ToString().Length != 0)
                        {
                            int itemID = (int)dr["ItemID", DataRowVersion.Original];
                            dr["ItemID"] = itemID;
                            dr.EndEdit();
                        }
                    }

                    //reset product filter dropdown index
                    if (bsManItems.Filter != null)
                    {
                        int itemID = (int)dr["ItemID"];
                        tscboProduct.SelectedIndexChanged -= tscboProduct_SelectedIndexChanged;
                        tscboProduct.ComboBox.SelectedValue = itemID;
                        tscboProduct.SelectedIndexChanged += tscboProduct_SelectedIndexChanged;
                    }
                }

                //Handle new record
                if (drv.IsNew)
                {
                    int curItemID = (int)drv.Row["ItemID"];

                    //bindingNavigator does not allow disabling of built-in navigation buttons.
                    //Set invisible instead;   
                    bindingNavigatorMoveFirstItem.Visible = false;
                    bindingNavigatorMovePreviousItem.Visible = false;
                    tsbtnDelete.Enabled = false;
                    tsbtnAddNew.Enabled = false;

                    tscboCompany.Enabled = false;
                    tscboProduct.Enabled = false;
                    tsbtnReport.Enabled = false;
                }

                RefreshCurrent();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //if (OnSave)
            //    return;
            */
        }

        private void RefreshCurrent()
        {
            try
            {
                if (this.bsManItems.Current != null)
                {
                    DataRowView rowView = (DataRowView)this.bsManItems.Current;
                    DataRow row = rowView.Row;


                    int itemID = int.TryParse(row["ItemID"].ToString(), out itemID) ? itemID : -999;

                    if (itemID == -999)
                        return;


                    //form will reopen here after Save
                    if (itemID > 0)
                    {
                        LastItemID = itemID;
                        tscboProduct.SelectedIndexChanged -= tscboProduct_SelectedIndexChanged;
                        tscboProduct.ComboBox.SelectedValue = itemID;
                        tscboProduct.SelectedIndexChanged += tscboProduct_SelectedIndexChanged;
                        tsbtnReport.Enabled = true;
                    }

                    //MessageBox.Show(bsManItems.Position.ToString());

                    //locate customer for this product                
                    int cpIndex = bsCustomerProducts.Find("ItemID", itemID);
                    if (cpIndex != -1)
                    {
                        bsCustomer.SuspendBinding();
                        bsCustomerProducts.Position = cpIndex;
                        DataRowView drv = (DataRowView)bsCustomerProducts.Current;
                        DataRow dr = drv.Row;
                        int customerID = (int)dr["CustomerID"];
                        int custIndex = bsCustomer.Find("CustomerID", customerID);
                        if (custIndex != -1)
                        {
                            bsCustomer.ResumeBinding();
                            bsCustomer.Position = custIndex;
                            LastCustomerID = customerID;
                            tsbtnReport.Enabled = true;
                        }
                    }

                    //locate ProductGrade aka ProductCategory
                    //MessageBox.Show("GradeID=" + row["GradeID"].ToString());
                    bsProductGradeItem.SuspendBinding();
                    int gradeID = int.TryParse(row["GradeID"].ToString(), out gradeID) ? gradeID : -1;
                    if (gradeID != -1)
                    {
                        int pgIndex = bsProductGradeItem.Find("GradeID", gradeID);
                        if (pgIndex != -1)
                        {
                            bsProductGradeItem.ResumeBinding();
                            bsProductGradeItem.Position = pgIndex;
                        }
                    }

                    //locate item in QCInstruction
                    bsQCInstruction.SuspendBinding();
                    int qcID = bsQCInstruction.Find("ItemID1", itemID);
                    if (qcID != -1)
                    {
                        bsQCInstruction.ResumeBinding();
                        bsQCInstruction.Position = qcID;
                    }
                    else
                    {
                        //create new packing image record
                        bsQCInstruction.AddNew();
                        bsQCInstruction.EndEdit();
                        bsQCInstruction.ResumeBinding();
                        bsQCInstruction.Position = bsQCInstruction.Count - 1;
                    }

                    DataTable ct = dsQCInstruction.Relations["ItemQCInstruction"].ChildTable;
                    DataRow[] foundRows = ct.Select("ItemID1 = " + itemID.ToString());
                    int count = foundRows.Length;
                    btnQCInstructionNewRow.Enabled = (count < maxRows);
                    lblQCInstructionNewRow.Enabled = (count < maxRows);

                    /*
                    //locate item in MasterBatchComp                
                    bsMasterBatch.SuspendBinding();
                    bsMBComp.SuspendBinding();
                    bsMBMBComp.SuspendBinding();
                    bsAddMBComp.SuspendBinding();

                    //locate MBCode, Colour
                    //bsMBComp.Filter = "IsPreferred = 1";  -- not in use
                    int mcIndex = bsMBComp.Find("ItemID", itemID);
                    if (mcIndex != -1)
                    {
                        bsMBComp.ResumeBinding();
                        bsMBComp.Position = mcIndex;
                        rowView = (DataRowView)this.bsMBComp.Current;
                        row = rowView.Row;
                        int mbID = int.TryParse(row["MBID"].ToString(), out mbID) ? mbID : -1;
                        if (mbID != -1)
                        {
                            //locate Masterbatch colour
                            bsMasterBatch.ResumeBinding();
                            bsMBMBComp.ResumeBinding();
                            int mbIndex = bsMasterBatch.Find("MBID", mbID);
                            bsMasterBatch.Position = mbIndex;
                            int mbColourPos = bsMBMBComp.Find("MBID", mbID);
                            if (mbColourPos != -1)
                            {
                                bsMBMBComp.Position = mbColourPos;
                            }

                            //locate AdditiveCode, Additive
                            int additiveID = int.TryParse(row["AdditiveID"].ToString(), out additiveID) ? additiveID : -1;
                            if (additiveID != -1)
                            {
                                bsAdditive.ResumeBinding();
                                bsAddMBComp.ResumeBinding();
                                int adIndex = bsAdditive.Find("AdditiveID", additiveID);
                                bsAdditive.Position = adIndex;
                                int additivePos = bsAddMBComp.Find("AdditiveID", additiveID);
                                if (additivePos != -1)
                                {
                                    bsAddMBComp.Position = additivePos;
                                }
                            }
                        }
                    }

                    //locate item in MaterialComp
                    bsMaterialComp.SuspendBinding();
                    bsMaterialGrade.SuspendBinding();
                    bsMaterialComp.ResetBindings(false);
                    bsMaterialGrade.ResetBindings(false);
                    bsMaterialComp.Filter = "IsActive = 1";
                    int mgID = bsMaterialComp.Find("ItemID", itemID);
                    if (mgID != -1)
                    {
                        bsMaterialComp.ResetBindings(false);
                        bsMaterialComp.ResumeBinding();
                        bsMaterialComp.Position = mgID;
                        rowView = (DataRowView)this.bsMaterialComp.Current;
                        row = rowView.Row;

                        //locate MaterialGrade
                        int materialGradeID = int.TryParse(row["MaterialGradeID"].ToString(), out materialGradeID) ? materialGradeID : -1;
                        if (materialGradeID != -1)
                        {
                            bsMaterialGrade.ResetBindings(false);
                            bsMaterialGrade.ResumeBinding();
                            int mgIndex = bsMaterialGrade.Find("MaterialGradeID", materialGradeID);
                            bsMaterialGrade.Position = mgIndex;
                        }
                    }

                    //locate mould specification
                    bsMouldSpec.EndEdit();
                    bsMouldSpec.SuspendBinding();
                    bsMouldSpec.Sort = "ItemID";
                    //DataTable dt = dsIMSpecificationForm.Tables["InjectionMouldSpecification"];
                    //DataView view = new DataView(dt, "", "ItemID", DataViewRowState.CurrentRows);
                    //int msID = view.Find(itemID);
                    int msID = bsMouldSpec.Find("ItemID", itemID);
                    if (msID != -1)
                    {
                        bsMouldSpec.ResetBindings(false);
                        bsMouldSpec.ResumeBinding();
                        //bsMouldSpec.Position = (int)view[msID].Row.RowId();
                        //MessageBox.Show(view[msID].Row["ItemID"].ToString() + ", " + view[msID].Row["MouldNumber"].ToString());
                        bsMouldSpec.Position = msID;
                        //rowView = (DataRowView)this.bsMouldSpec.Current;
                        //row = rowView.Row;
                        //MessageBox.Show(row["ItemID"].ToString() + ", " + row["MouldNumber"].ToString());
                        //bsMouldSpec.Position = msID;
                        //rowView = (DataRowView)this.bsMouldSpec.Current;
                        //row = rowView.Row;

                    }

                    //locate machine preference
                    bsMachPref.EndEdit();
                    bsMachPref.SuspendBinding();
                    bsMachPref.Sort = "ItemID";
                    int mpID = bsMachPref.Find("ItemID", itemID);
                    if (mpID != -1)
                    {
                        bsMachPref.ResetBindings(false);
                        bsMachPref.ResumeBinding();
                    }

                    //locate Quality Control
                    bsQC.EndEdit();
                    bsQC.SuspendBinding();
                    //dt = dsIMSpecificationForm.Tables["QualityControl"];
                    //view = new DataView(dt, "", "ItemID", DataViewRowState.CurrentRows);
                    //int qcID = view.Find(itemID);
                    bsQC.Sort = "ItemID";
                    int qcID = bsQC.Find("ItemID", itemID);
                    if (qcID != -1)
                    {
                        bsQC.ResetBindings(false);
                        bsQC.ResumeBinding();
                        //bsQC.Position = (int)view[msID].Row.RowId();
                        //MessageBox.Show(view[qcID].Row["ItemID"].ToString() + ", " + view[qcID].Row["FinishedPTQC"].ToString());
                        bsQC.Position = qcID;
                        //rowView = (DataRowView)this.bsQC.Current;
                        //row = rowView.Row;
                        //MessageBox.Show(row["ItemID"].ToString() + ", " + row["FinishedPTQC"].ToString());
                    }
                }
                //MessageBox.Show(bsManItems.Position.ToString());
                */
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
    }
}
