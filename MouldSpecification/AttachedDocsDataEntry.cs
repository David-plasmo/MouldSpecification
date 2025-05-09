using MouldSpecification.Properties;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static Utils.DrawingUtils;

namespace MouldSpecification
{
    /// <summary>
    /// Represents the data entry form for managing attached documents related to items and customers.
    /// </summary>
    public partial class AttachedDocsDataEntry : Form
    {

        public int? LastItemID { get; set; }
        public int? LastCustomerID { get; set; }
        public bool CustomerFilterOn { get; set; }
        public string NextForm { get; set; }

        bool ChangedByCode = false; //forces exit to SelectedIndexChanged event for toostrip combobox control
        
        // ToolStrip controls to be added to Navigation bar
        ToolStripLabel tslCompany;
        ToolStripComboBox tscboCompany;
        ToolStripLabel tslCode;
        ToolStripComboBox tscboCode;
        ToolStripLabel tslProduct;
        ToolStripComboBox tscboProduct;

        // DataSet and BindingSources for data binding
        DataSet dsAttachedDoc;
        BindingSource bsManItems, bsProductGradeItem, bsCustomerProducts, bsCustomer, bsAttachedDoc;

        public AttachedDocsDataEntry(int? lastItemID, int? lastCustomerID, bool customerFilterOn = false)
        {
            InitializeComponent();

            // Initialize properties 
            LastItemID = lastItemID;
            LastCustomerID = lastCustomerID;
            CustomerFilterOn = customerFilterOn;

            // Set the default next form name.
            NextForm = this.Name;

            // Event handlers for cancel and accept buttons
            tsbtnCancel.Click += tsbtnCancel_Click;
            tsbtnAccept.Click += tsbtnAccept_Click;

            tslCompany = new ToolStripLabel() { Text = "Company" };
            tslCode = new ToolStripLabel() { Text = "Code" };
            tslProduct = new ToolStripLabel() { Text = "Product" };
            tscboCompany = new ToolStripComboBox();
            tscboCode = new ToolStripComboBox();
            tscboProduct = new ToolStripComboBox();
            //tscboEntryForm = new ToolStripComboBox() { Text = "Product Specification" }; 
            //btnReport = new ToolStripButton() { Text = "Report" };

            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                tslCompany,
                tscboCompany,
                tslCode,
                tscboCode,
                tslProduct,
                tscboProduct
                //tscboEntryForm,
                //btnReport
            });

            //tsbtnAccept.Click += tsbtnAccept_Click;
            //tsbtnCancel.Click += tsbtnCancel_Click;
            //tsbtnReload.Click += tsbtnReload_Click;
            btnReport.Click += btnReport_Click;

            foreach (RowStyle style in this.tableLayoutPanel1.RowStyles)
            {
                if (style.SizeType == SizeType.Absolute)

                    // Adjust height using a helper method
                    style.Height = p96H(19);
            }
            // Set the customer filter property
            CustomerFilterOn = customerFilterOn;

            this.ResumeLayout();

        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            InjectionMouldReports.Reports.ViewReport(LastItemID.Value, "AttachedDocuments.rdlc");
        }

        public AttachedDocsDataEntry()
        {

        }

        private void tsbtnAccept_Click(object sender, EventArgs e)
        {
            // Save te data
            DoSave();

            // Set the dialog result and close the form
            this.DialogResult = DialogResult.Retry;
            this.Close();
        }

        private void tsbtnCancel_Click(object sender, EventArgs e)
        {
            // Cancel the current edit operation on the attached documents bindings source.
            bsAttachedDoc.CancelEdit();
        }

        private void DoSave()
        {
            DataSet ds = dsAttachedDoc;
            DataRowView drv = (DataRowView)this.bsManItems.Current;
            DataRow row = drv.Row;
            int currentID = (int)row["ItemID"];
            LastItemID = currentID;

            // End edit mode for the attached documents binding source.
            bsAttachedDoc.EndEdit();

            // Commit any changes in the DataGridView to the data source.
            dgvAttachedDocs.EndEdit();

            // Create an instance of AttachedDocDAL to handle database 
            AttachedDocDAL attachedDocDAL = new AttachedDocDAL();

            // Update the database to reflect changes in the "AttachedDoc" table.
            attachedDocDAL.UpdateAttachedDoc(dsAttachedDoc, "AttachedDoc");
        }

        /// <summary>
        /// Handles the Click event of the Product specification menu item.
        /// Navigates to the "SpecificationDataEntry" form after saving changes.
        /// </summary>
        /// <param name="sender"> The source of the event data. </param>
        /// <param name="e"> An EventArgs that contains the event data. </param>
        private void productSpecificationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set the next form to navigate to.
            NextForm = "SpecificationDataEntry";

            // Save any changes before navigating to te next form.
            DoSave();

            // Set the dialog result to Retry for navigation and close the current form.
            this.DialogResult = DialogResult.Retry;
        }

        /// <summary>
        /// Handles the Click event of the Product Assembly menu item.
        /// Navigates to the "AssemblyDataEntry" form after saving changes.
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> An EventArgs that contains the event data. </param>
        private void productAssemblyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set the next form to navigate to.
            NextForm = "AssemblyDataEntry";

            // Save any changes before navigating to the next form.
            DoSave();

            // Set the dialog result to Retry for navigation and close the current form.
            this.DialogResult = DialogResult.Retry;
        }

        /// <summary>
        /// Handles the Click event of the QC Instructions menu item.
        /// Navigates to the "QCDataEntry" form after saving changes.
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> An EventArgs that contains the event data. </param>
        private void qCInstructionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set the next form to navigate to. 
            NextForm = "QCDataEntry";

            // Save any changes before navigating to next form.
            DoSave();

            // Set the dialog result to Retry for navigation and close the current form.
            this.DialogResult = DialogResult.Retry;
        }

        /// <summary>
        /// Handles the Click event of the Product Packaging menu item.
        /// Navigates to the "PackagingDataEntry" form after saving changes.
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> An EventArgs that contains the event data. </param>
        private void productPackagingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set the next form to navigate to.
            NextForm = "PackagingDataEntry";

            // Save any changes before navigating to the form.
            DoSave();

            // Set the dialog result to Retry for navigation and close the current form.
            this.DialogResult = DialogResult.Retry;
        }

        /// <summary>
        /// Handles the Load event for the AttachedDocsDataEntry form.
        /// Initializes the UI componenets, sets up event handlers, and binds data.
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> An EventArgs that contains the event data. </param>
        private void AttachedDocsDataEntry_Load(object sender, EventArgs e)
        {
            // Add event handler for the Shown event to initialize certain aspects when the form is shown.
            this.Shown += AttachedDocsDataEntry_Shown;

            // Disable toolbar buttons initially.
            tsbtnDelete.Enabled = false;
            tsbtnAddNew.Enabled = false;
            btnReport.Enabled = false;

            btnNewRow.Size = new Size(p96W(24), p96H(20));
            btnNewRow.Image = RescaleImage((Bitmap)Properties.Resources.NewRow, btnNewRow.Width, btnNewRow.Height);
            btnNewRow.Click += btnNewRow_Click;

            tscboCompany.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            tscboCompany.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            tscboCompany.DropDownHeight = 400;
            tscboCompany.DropDownWidth = p96W(250);
            tscboCompany.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            tscboCompany.IntegralHeight = false;
            tscboCompany.MaxDropDownItems = 9;
            tscboCompany.MergeAction = System.Windows.Forms.MergeAction.Insert;
            tscboCompany.Name = "tscboCompany";
            tscboCompany.Size = new System.Drawing.Size(p96W(250), p96H(25));
            tscboCompany.Sorted = true;

            tscboCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            tscboCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            tscboCode.DropDownHeight = 400;
            tscboCode.DropDownWidth = p96W(150);
            tscboCode.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            tscboCode.IntegralHeight = false;
            tscboCode.MaxDropDownItems = 9;
            tscboCode.MergeAction = System.Windows.Forms.MergeAction.Insert;
            tscboCode.Name = "tscboCode";
            tscboCode.Size = new System.Drawing.Size(p96W(150), p96H(25));
            tscboCode.Sorted = true;

            tscboProduct.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            tscboProduct.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            tscboProduct.DropDownHeight = 400;
            tscboProduct.DropDownWidth = p96W(300);
            tscboProduct.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            tscboProduct.IntegralHeight = false;
            tscboProduct.MaxDropDownItems = 9;
            tscboProduct.MergeAction = System.Windows.Forms.MergeAction.Insert;
            tscboProduct.Name = "tscboProduct";
            tscboProduct.Size = new System.Drawing.Size(p96W(300), p96H(25));
            tscboProduct.Sorted = true;

            // Fetch data sets and bind them to the control on the form.
            GetDataSets();
            BindControls();
           
        }


        private void AttachedDocsDataEntry_Shown(object sender, EventArgs e)
        {
            // Set the bounds of the form using a rectangle with a specified size.
            Rectangle r = new Rectangle(5, 5, p96W(1175), p96H(1100));
            this.DesktopBounds = r;

            bindingNavigator1.Height = p96H(30);
            splitContainer1.SplitterDistance = p96H(55);
            splitContainer2.SplitterDistance = p96H(25);

            // Initialize the form's state based on filters and previous selections.
            SetFormState();
        }

        /// <summary>
        /// Configure s the state of the form, including customer and product filters,
        /// based on the provided filters and previous user interactions.
        /// </summary>
        private void SetFormState()
        {
            try
            {
                //disable NavigationBar controls
                foreach (Object o in bindingNavigator1.Items)
                {
                    if (o.GetType() == typeof(System.Windows.Forms.ToolStripComboBox))
                    {
                        ToolStripComboBox tscb = (ToolStripComboBox)o;
                        if (tscb.Name != "tscboCompany")
                            tscb.Enabled = false;
                    }
                    if (o.GetType() == typeof(ToolStripLabel))
                    {
                        ToolStripLabel lb = (ToolStripLabel)o;
                        if (lb.Text != "Company")
                            lb.ForeColor = SystemColors.GrayText;
                    }
                }

                //Populate Toolbar Customer dropdown                
                DataTable dt = (DataTable)dsAttachedDoc.Tables["Customer"].Copy();
                dt.TableName = "customer";
                tscboCompany.ComboBox.SelectedIndexChanged -= tscboCompany_SelectedIndexChanged;
                ChangedByCode = true;
                tscboCompany.ComboBox.DataSource = dt;
                tscboCompany.ComboBox.DisplayMember = "CUSTNAME";
                tscboCompany.ComboBox.ValueMember = "CustomerID";
                tscboCompany.ComboBox.SelectedIndex = -1;
                tscboCompany.ComboBox.SelectedIndexChanged += tscboCompany_SelectedIndexChanged;
                ChangedByCode = false;

                if (CustomerFilterOn && LastCustomerID.HasValue && LastItemID.HasValue)
                {
                    tscboCompany.ComboBox.SelectedIndexChanged -= tscboCompany_SelectedIndexChanged;
                    ChangedByCode = true;
                    tscboCompany.ComboBox.SelectedValue = LastCustomerID.Value;
                    SetProductFilter(LastCustomerID.Value, LastItemID.Value);
                    tscboCompany.ComboBox.SelectedIndexChanged += tscboCompany_SelectedIndexChanged;
                    ChangedByCode = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tscboCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tscboCode.SelectedIndex != -1 && !ChangedByCode)
            {
                int itemID = (int)tscboCode.ComboBox.SelectedValue;

                if (itemID != LastItemID)
                {
                    LastItemID = itemID;
                    if (bsManItems == null)
                    {
                        {
                            BindControls();
                            FormatAttachedDocGrid();
                            EnableGroups(true);
                        }
                    }
                    int itemIndex = bsManItems.Find("ItemID", itemID);
                    if (itemID != -1)
                    {
                        btnReport.Enabled = true;
                        bsManItems.Position = itemIndex;
                    }
                }
            }
        }

        /// <summary>
        /// Retrieves and initializes the datasets required for the form.
        /// This method fetches the attached document dataset from the data access layer.
        /// </summary>
        private void GetDataSets()
        {
            try
            {
                // Instantiate the data access layer for specification entries.
                SpecificationDataEntryDAL dal = new SpecificationDataEntryDAL();

                // Build and assign the dataset containing attached document data.
                dsAttachedDoc = dal.BuildAttachedDocDataset();
            }
            catch (Exception ex)
            {
                // Suppress exceptions (Consider logging the exception for debugging).
            }
        }

        /// <summary>
        /// Binds data sources to various controls on the form,
        /// including binding navigators, combo boxes, text boxes, and the data grid view.
        /// </summary>
        private void BindControls()
        {
            try
            {
                bsManItems = new BindingSource
                {
                    DataSource = dsAttachedDoc,
                    DataMember = "MAN_Items",
                    Sort = "ITEMDESC ASC"
                };

                // Bind product grade items based on the parent table from the dataset relation.
                bsProductGradeItem = new BindingSource();
                bsProductGradeItem.DataSource = dsAttachedDoc.Relations["ProductGradeItem"].ParentTable;

                // Bind customer product data from the dataset.
                bsCustomerProducts = new BindingSource();
                bsCustomerProducts.DataSource = dsAttachedDoc.Tables["CustomerProduct"];

                // Bind customer data from the dataset.
                bsCustomer = new BindingSource();
                bsCustomer.DataSource = dsAttachedDoc.Tables["Customer"];

                // Initialize the BindingNavigator and associate it with the MAN_Items BindingsSource.
                bindingNavigator1.BindingSource = new BindingSource();
                bindingNavigator1.BindingSource = bsManItems;

                // Attach an event handler to respond to changes in the current MAN_Items item.
                bsManItems.CurrentChanged += bsManItems_CurrentChanged;

                // Bind customer data to the company combo box.
                DataTable dt = dsAttachedDoc.Tables["Customer"];
                tscboCompany.ComboBox.DataSource = dt;

                // Display customer name.
                tscboCompany.ComboBox.DisplayMember = "CUSTNAME";

                // Use CustomerID as the value. 
                tscboCompany.ComboBox.ValueMember = "CustomerID";

                // Attach selection change event.
                tscboCompany.SelectedIndexChanged += tscboCompany_SelectedIndexChanged;

                // Bind product data to the company box.
                dt = dsAttachedDoc.Tables["Product"];

                // Temporarily detach the event.
                tscboProduct.ComboBox.SelectedIndexChanged -= tscboProduct_SelectedIndexChanged;
                tscboProduct.ComboBox.DataSource = dt;

                // Display item description
                tscboProduct.ComboBox.DisplayMember = "ITEMDESC";

                // Use ItemID as the value.
                tscboProduct.ComboBox.ValueMember = "ItemID";

                // Reattach the event.
                tscboProduct.SelectedIndexChanged += tscboProduct_SelectedIndexChanged;

                // Bind text boxes to specific fields in the MAN_Items and Customer tables.
                txtITEMNMBR.DataBindings.Add(new Binding("Text", bsManItems, "ITEMNMBR"));
                txtITEMDESC.DataBindings.Add(new Binding("Text", bsManItems, "ITEMDESC"));
                txtCustomer.DataBindings.Add(new Binding("Text", bsCustomer, "CUSTNAME"));

                // Initialize the BindingSource for attached documents.
                bsAttachedDoc = new BindingSource();

                // Attach an event handler for adding new records.
                bsAttachedDoc.AddingNew += bsAttachedDoc_AddingNew;
                bsAttachedDoc.DataSource = dsAttachedDoc.Tables["AttachedDoc"];

                // Bind the attached documents grid view to the MAN_Items table using the ItemAttachedDoc relation.
                dgvAttachedDocs.DataSource = bsManItems;
                dgvAttachedDocs.DataMember = "ItemAttachedDoc";

                // Attach events for data binding completion and data errors in the grid view.
                dgvAttachedDocs.DataBindingComplete += dgvAttachedDocs_DataBindingComplete;
                dgvAttachedDocs.DataError += dgvAttachedDocs_DataError;

                // Apply formatting to the attached document grid.
                FormatAttachedDocGrid();

            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event for the Product ComboBox in the ToolStrip.
        /// This method updates the current position of the MAN_Items BindingSource based on
        /// the selected product and enables the report button.
        /// </summary>
        /// <param name="sender"> The source of the event, typically the ComboBox. </param>
        /// <param name="e"> The event arguments.  </param>
        private void tscboProduct_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (tscboProduct.SelectedIndex != -1 && !ChangedByCode)
            {
                int itemID = (int)tscboProduct.ComboBox.SelectedValue;
                if (itemID != LastItemID)
                {
                    LastItemID = itemID;
                    if (bsManItems == null)
                    {
                        {
                            BindControls();
                            FormatAttachedDocGrid();
                            EnableGroups(true);
                        }
                    }
                    int itemIndex = bsManItems.Find("ItemID", itemID);
                    if (itemID != -1)
                    {
                        btnReport.Enabled = true;
                        bsManItems.Position = itemIndex;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the event when the current item in the MAN_Items BindingSource changes.
        /// This method refreshes the currently selected data in the form.
        /// </summary>
        /// <param name="sender"> The source  </param>
        /// <param name="e"></param>
        private void bsManItems_CurrentChanged(object sender, EventArgs e)
        {
            // Refresh the current data in the form based on the selected MAN_Items item.
            RefreshCurrent();
        }

        /// <summary>
        /// Handles the event when a new record is being added to the AttachedDoc BindingsSource.
        /// This method ensures that the "ItemID" field is automatically populated with the
        /// "ItemID" of the currently selected MAN_Items row.
        /// </summary>
        /// <param name="sender"> The source of the event, typically the BindingSource. </param>
        /// <param name="e"> The event arguments that allow overriding the new item. </param>
        private void bsAttachedDoc_AddingNew(object sender, AddingNewEventArgs e)
        {
            try
            {
                // Retrieve the current DataRowView from the MAN_Items BindingSource.
                DataRowView rowView = (DataRowView)this.bsManItems.Current;

                // Extract the DataRow from the DataRowView.
                DataRow row = rowView.Row;

                // Get the ItemID of the currently selected MAN_Items row.
                int itemID = (int)row["ItemID"];

                // Access the data source of the AttachedDoc BindingSource as a DataTable.
                DataTable dt = (DataTable)bsAttachedDoc.DataSource;

                // Set the default value for the "ItemID"
                dt.Columns["ItemID"].DefaultValue = itemID;
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Formats the attached document document DataGridView (dgvAttachedDocs) by adjusting
        /// various properties such as row selection mode, column visibility, and header styles.
        /// </summary>
        private void FormatAttachedDocGrid()
        {
            try
            {
                // Disables the option to add new rows manually in the DataGridView.
                dgvAttachedDocs.AllowUserToAddRows = false;

                // Sets the selection mode to allow the entire row to be selected.
                dgvAttachedDocs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                // Sets the width and height 
                dgvAttachedDocs.Width = p96H(600);
                dgvAttachedDocs.Height = p96H(100);

                // Hides the row headers in the DataGridView
                dgvAttachedDocs.RowHeadersVisible = false;

                // Clears any existing selection in the DataGridVIew.
                dgvAttachedDocs.ClearSelection();

                // Sets the height of the column header and disables resizing of the column headers.
                dgvAttachedDocs.ColumnHeadersHeight = p96H(19);
                dgvAttachedDocs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

                // Disables resizing of rows in the DataGridView.
                dgvAttachedDocs.AllowUserToResizeRows = false;

                // Disables resizing of the row headers.
                dgvAttachedDocs.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

                // Customizes the column header cell style.
                DataGridViewCellStyle style = dgvAttachedDocs.ColumnHeadersDefaultCellStyle;

                // Sets the background color of the headers.
                style.BackColor = Color.RosyBrown;

                // Sets the text color of the headers.
                style.ForeColor = Color.MidnightBlue;

                // Sets the font style of the headers 
                style.Font = new Font(dgvAttachedDocs.Font, FontStyle.Regular);

                // Sets the selection background color to match the header's 
                dgvAttachedDocs.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgvAttachedDocs.ColumnHeadersDefaultCellStyle.BackColor;

                // Sets the header border style.
                dgvAttachedDocs.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;

                // Sets the cell border style and grid color.
                dgvAttachedDocs.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                dgvAttachedDocs.GridColor = SystemColors.ActiveBorder;

                // Disables visual styles for headers, allowing for custom styling.
                dgvAttachedDocs.EnableHeadersVisualStyles = false;

                // Enables automatic column generation based on the data source.
                dgvAttachedDocs.AutoGenerateColumns = true;

                // Automatically resizes the columns based on their content.
                dgvAttachedDocs.AutoResizeColumns();

                // Sets the row size mode to adjust based on displayed cell content.
                dgvAttachedDocs.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;

                // Sets the width of the "DocFilePath" column.
                dgvAttachedDocs.Columns["DocFilePath"].Width = p96W(400);

                // Hides the "ItemID" and "AttachedDocID" columns from view.
                dgvAttachedDocs.Columns["ItemID"].Visible = false;
                dgvAttachedDocs.Columns["AttachedDocID"].Visible = false;

                // Customizes the hheader text for "last_updated_by" and "last_updated_on" columns.
                dgvAttachedDocs.Columns["last_updated_by"].HeaderText = "Updated By";
                dgvAttachedDocs.Columns["last_updated_on"].HeaderText = "Updated On";

                // Formats the "last_updated_on" column to display date and time in a specific format.
                dgvAttachedDocs.Columns["last_updated_on"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";

                // Creates a new image column for browsing files.
                DataGridViewImageColumn bc = new DataGridViewImageColumn();

                // Name of the new column.
                bc.Name = "Browse";

                // Sets the header text to empty.
                bc.HeaderText = string.Empty;

                // Defines the column as an image type.
                bc.ValueType = typeof(Image);

                // Inserts the new "Browse" image column at the 4th position (index 3).
                dgvAttachedDocs.Columns.Insert(3, bc);

                // Sets the width of the "Browse" column.
                dgvAttachedDocs.Columns["Browse"].Width = 30;

                // Adds event handlers for cell formatting, cell clicks, and mouse down events.
                dgvAttachedDocs.CellFormatting += dgvAttachedDocs_CellFormatting;
                dgvAttachedDocs.CellClick += dgvAttachedDocs_CellClick;
                dgvAttachedDocs.CellMouseDown += dgvAttachedDocs_CellMouseDown;

            }
            catch (Exception ex)
            {
                // Catches any exceptions and displays the error message in a message box.
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the DataGridView to customize the appearance of specific cells.
        /// Specifically, this method formats the "Browse" column by adding a tooltip and image.
        /// </summary>
        /// <param name="sender"> The source of the event, typically the DataGridView. </param>
        /// <param name="e"> Provides data for the CellFormatting event, including row and column indices. </param>
        private void dgvAttachedDocs_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                // Ensure the current row index is valid (not a header row)
                if (e.RowIndex < 0)
                    return;

                // Check if the column being formatted is the "Browse" column.
                if (dgvAttachedDocs.Columns[e.ColumnIndex].Name == "Browse")
                {
                    // Retrieve the specific cell being formatted.
                    DataGridViewCell cell = dgvAttachedDocs.Rows[e.RowIndex].Cells[e.ColumnIndex];

                    // Set a tooltip for the cell to indicates its purpose.
                    cell.ToolTipText = "Browse for file name.";

                    // Load the "browse" image from the application resources.
                    Image image = Properties.Resources.browse;

                    // Specify that the cell's value type is an image. 
                    cell.ValueType = typeof(Image);

                    // Set the value of the cell to the "browse" image. 
                    e.Value = image;
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Handles the CellMouseDown event of the DataGridView to provide a context menu
        /// when the user right-clicks on a cell.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvAttachedDocs_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Ensure that a valid cell is clicked (not a header cell).
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                // Check if the right mouse button was clicked.
                if (e.Button == MouseButtons.Right)
                {
                    // Retrieve the cell that was right-clicked 
                    DataGridViewCell clickedCell = (sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex];

                    // Select the clicked cell in the DataGridView.
                    this.dgvAttachedDocs.CurrentCell = clickedCell;

                    // Get the mouse position relative to the DataGridView.
                    var relativeMousePosition = dgvAttachedDocs.PointToClient(Cursor.Position);

                    // Retrieve the value of the "DocFilePath" column for the clicked row.
                    int fp1Index = dgvAttachedDocs.Columns["DocFilePath"].Index;
                    string fp1 = dgvAttachedDocs.Rows[e.RowIndex].Cells[fp1Index].Value.ToString();

                    // Create a new context menu strip for the DataGridView.
                    ContextMenuStrip cms = new ContextMenuStrip();
                    dgvAttachedDocs.ContextMenuStrip = cms;

                    // Add menu items based on the file path value.
                    if (fp1.Length > 0)
                        cms.Items.Add("&Open", Resources.show, new System.EventHandler(this.Open_Click));
                    cms.Items.Add("&Browse", Resources.browse, new System.EventHandler(this.Browse_Click));

                    // Add a separator to the context menu.
                    cms.Items.Add(new ToolStripSeparator());

                    // Add a "Delete Row" menu item.
                    cms.Items.Add("&Delete Row", Resources.delete, new System.EventHandler(this.Delete_Click));

                    // Display the context menu at the mouse position.
                    cms.Show(dgvAttachedDocs, relativeMousePosition);
                }
            }
        }

        /// <summary>
        /// Handles the "Open" menu item click event to open a file associated with the selected row in the DataGridView.
        /// </summary>
        /// <param name="sender"> The source of the event, typically the menu item. </param>
        /// <param name="e"> Provides data for the event. </param>
        private void Open_Click(Object sender, EventArgs e)
        {

            try
            {
                // Get the count of selected rows in the DataGridView.
                int rowCount = dgvAttachedDocs.Rows.GetRowCount(DataGridViewElementStates.Selected);

                // Ensure at least one row is selected.
                if (rowCount > 0)
                {
                    // Retrieve the index of the first selected row.
                    int rowIndex = dgvAttachedDocs.SelectedRows[0].Index;

                    // Get the file path from the "DocFilePath" column of the selected row.
                    string fileName = dgvAttachedDocs.Rows[rowIndex].Cells["DocFilePath"].Value.ToString();

                    // Create and configure a ProcessStartInfo object to open the file.
                    ProcessStartInfo info = new ProcessStartInfo();

                    // Set the file name to open.
                    info.FileName = fileName;

                    // Create a new process to start the file.
                    Process process = new Process();
                    process.StartInfo = info;

                    // Start the process.
                    process.Start();
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Handles the "Browse" menu item click event, allowing the user to select a file and update the "DocFilePath" column for the selected row in the DataGridView.
        /// </summary>
        /// <param name="sender"> The source of the event, typically the menu item. </param>
        /// <param name="e"> Provides data for the event. </param>
        private void Browse_Click(Object sender, EventArgs e)
        {
            // Get the name of the control that triggered the context menu.
            string scName = ((ContextMenuStrip)(((ToolStripMenuItem)sender).Owner)).SourceControl.Name;

            // Get the count of selected rows in the DataGridView.
            int rowCount = dgvAttachedDocs.Rows.GetRowCount(DataGridViewElementStates.Selected);

            // Proceed only if at least one row is selected
            if (rowCount > 0)
            {
                // Create and configure an OpenFileDialog for selecting files.
                OpenFileDialog fdlg = new OpenFileDialog();

                // Set the title of the dialog.
                fdlg.Title = "Attached Documents Files";

                // Default directory.
                fdlg.InitialDirectory = @"S:CONSOLIDATED PLASTICS\INJECTION MOULDING\Database\Images\";

                // File filter.
                fdlg.Filter = "Docx (*.docx)|*.docx|Excel (*.xlsx)|*.xlsx|PDF (*.pdf)|*.pdf|JPG (*.jpg)|*.jpg|PNG (*.png)|*.png|tif (*.tif)|*.tif|Bitmap (*.bmp)|*.bmp|All files (*.*)|*.*";

                // Default filter selection.
                fdlg.FilterIndex = 1;

                // Restore the last-used directory.
                fdlg.RestoreDirectory = true;

                // Show the dialog and check if the user selected a file.
                if (fdlg.ShowDialog() == DialogResult.OK)
                {
                    // Retrieve the selected file path.
                    string fileSelected = fdlg.FileName;

                    // Get the index of the selected row in the DataGridView.
                    int rowIndex = dgvAttachedDocs.SelectedRows[0].Index;

                    // Update the "DocFilePath" column of the selected row with the selected file path.
                    dgvAttachedDocs.Rows[rowIndex].Cells["DocFilePath"].Value = fileSelected;

                    // Commit changes to the DataGridView.
                    dgvAttachedDocs.EndEdit();
                }
            }
        }

        /// <summary>
        /// Handles the "Delete Row" context menu item click event.
        /// Deletes the currently selected row from the DataGridView and its associated data source after user confirmation.
        /// </summary>
        /// <param name="sender"> The source of the event, typically the menu item. </param>
        /// <param name="e"> Provides data for the event. </param>
        private void Delete_Click(Object sender, EventArgs e)
        {
            try
            {
                // Get the name of the control that triggered the context menu.
                string scName = ((ContextMenuStrip)(((ToolStripMenuItem)sender).Owner)).SourceControl.Name;

                // Confirm the delete operation with user.
                if (MessageBox.Show("Are you Sure?", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    // Proceed if the context menu was triggered for the DataGridView.
                    if (scName == "dgvAttachedDocs")
                    {
                        // Get the BindingManagerBase for the DataGridView's data source.
                        BindingManagerBase bm = this.dgvAttachedDocs.BindingContext[this.dgvAttachedDocs.DataSource, this.dgvAttachedDocs.DataMember];

                        // Retrieve the current DataRow associated with the selected row.
                        DataRow dr = ((DataRowView)bm.Current).Row;

                        // Delete the row from the data source and apply changes.
                        dr.Delete();
                        dr.EndEdit();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Handles the DataBindingComplete event for the DataGridView.
        /// Clears the current selection to ensure no rows are pre-selected after data binding is complete.
        /// </summary>
        /// <param name="sender"> The source of the event, typically the DataGridView. </param>
        /// <param name="e"> Provides data for the event. </param>
        private void dgvPackingImage_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Clear any selection in the DataGridView after data binding is complete.
            dgvAttachedDocs.ClearSelection();
        }

        /// <summary>
        /// Handles the CellClick event for the DataGridView.
        /// Provides functionality for browsing and selecting a file when the "Browse" button cell is clicked.
        /// </summary>
        /// <param name="sender"> The source of the event, typically the DataGridView. </param>
        /// <param name="e"> Provides data for the event, including the clicked cell's row and column indicates. </param>
        private void dgvAttachedDocs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Ensure the clicked cell is not a header cell.
                if (e.RowIndex < 0)
                    return;

                string filepath;

                // Filed name in the DataGridView where the file path will be updated.
                string fieldName = "DocFilePath";

                // Retrieve the current user's Windows username.
                string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

                // Get the current date and time.
                var now = DateTime.Now;

                // Check if the clicked column is the "Browse" button column.
                if (dgvAttachedDocs.Columns[e.ColumnIndex].Name == "Browse")
                {
                    // Initialize and configure the OpenFileDialog for selecting a file.
                    OpenFileDialog fdlg = new OpenFileDialog();
                    fdlg.Title = "Attached Documents Files";
                    fdlg.InitialDirectory = @"S:CONSOLIDATED PLASTICS\INJECTION MOULDING\Database\Images\";
                    fdlg.Filter = "Docx (*.docx)|*.docx|Excel (*.xlsx)|*.xlsx|PDF (*.pdf)|*.pdf|JPG (*.jpg)|*.jpg|PNG (*.png)|*.png|tif (*.tif)|*.tif|Bitmap (*.bmp)|*.bmp|All files (*.*)|*.*";
                    fdlg.FilterIndex = 1;
                    fdlg.RestoreDirectory = true;

                    // If the user selects a file and clicks OK.
                    if (fdlg.ShowDialog() == DialogResult.OK)
                    {
                        // Get the selected file path.
                        string fileSelected = fdlg.FileName;

                        // Update the file path in the respective cell of the DataGridView.
                        dgvAttachedDocs.Rows[e.RowIndex].Cells[fieldName].Value = fdlg.FileName;

                        // Commit the changes to the DataGridView.
                        dgvAttachedDocs.EndEdit();
                    }
                }
                else

                    // If the clicked cell is not part of the "Browse" column, exit the method.
                    return;


            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Handles the DataBindingComplete event of the DataGridView.
        /// Clears the current selection after data binding is complete to ensure no rows are pre-selected.
        /// </summary>
        /// <param name="sender"> The source of the event, typically the DataGridView. </param>
        /// <param name="e"> Provides data for the event, such as the state of the DataGridView after binding is complete. </param>
        private void dgvAttachedDocs_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Clears any selected rows after the DataGridView has finished binding to its data source.
            dgvAttachedDocs.ClearSelection();
        }

        /// <summary>
        /// Handles the Click event for the "New Row" button.
        /// Inserts a new row into the attached document's data  source, ends the edit, and sets the position to the new row.
        /// </summary>
        /// <param name="sender"> The source of the event, typically a button. </param>
        /// <param name="e"> Provides data for the event. </param>
        private void btnNewRow_Click(object sender, EventArgs e)
        {
            // Insert a new row into the data source managed by the BindingSource.
            bsAttachedDoc.AddNew();

            // Commits the addition of the new row.
            bsAttachedDoc.EndEdit();

            // Sets the current postion to the newly added row (last row in the data source).
            bsAttachedDoc.Position = bsAttachedDoc.Count - 1;

            // Retrieves the child table related to the attached document using the "ItemAttachedDoc" relation.
            DataTable ct = dsAttachedDoc.Relations["ItemAttachedDoc"].ChildTable;

            // Filters rows in the child table based on the LastItemID.
            DataRow[] foundRows = ct.Select("ItemID = " + LastItemID.ToString());
        }

        /// <summary>
        /// Handles the DataError event for the DataGridView.
        /// Catches and suppresses any data errors that occur during the display or interaction with DataGridView.
        /// </summary>
        /// <param name="sender"> The source of the event, typically the DataGridView. </param>
        /// <param name="e"> Provides data for event, such as the error context and exception. </param>
        private void dgvAttachedDocs_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        /// <summary>
        /// Refreshes the current data and updates the UI based on the selected item.
        /// It retrieves and updates the item, customer product grade, and attached document details.
        /// </summary>
        private void RefreshCurrent()
        {
            try
            {
                // Checks if there is current item in the binding source
                if (this.bsManItems.Current != null)
                {
                    // Retrieves the current DataRowView from the binding source and extracts the DataRow
                    DataRowView rowView = (DataRowView)this.bsManItems.Current;
                    DataRow row = rowView.Row;

                    // Tries to parse the ItemID from the DataRow; if it fails, it defaults to -999
                    int itemID = int.TryParse(row["ItemID"].ToString(), out itemID) ? itemID : -999;

                    // If the ItemID is invalid (-999), exit the method
                    if (itemID == -999)
                        return;
                                       
                    //reset product filter dropdown index
                    if (bsManItems.Filter != null)
                    {
                        tscboProduct.SelectedIndexChanged -= tscboProduct_SelectedIndexChanged;
                        ChangedByCode = true; //workaround:  unsubscribing doesn't work for navigation bar combobox !!!
                        SetDropDownIndex(tscboProduct, itemID);
                        SetDropDownIndex(tscboCode, itemID);
                        ChangedByCode = false;
                    }
                    
                    // Look for the corresponding CustomerProduct for the current ItemID
                    int cpIndex = bsCustomerProducts.Find("ItemID", itemID);
                    if (cpIndex != -1)
                    {
                        // Suspend data binding while making changes
                        bsCustomer.SuspendBinding();

                        // Set the position of the CustomerProduct binding source to the matching ItemID
                        bsCustomerProducts.Position = cpIndex;
                        DataRowView drv = (DataRowView)bsCustomerProducts.Current;
                        DataRow dr = drv.Row;

                        // Retrieve the associated CustomerID from the CustomerProduct row
                        int customerID = (int)dr["CustomerID"];

                        // Find the matching customer based on CustomerID
                        int custIndex = bsCustomerProducts.Find("CustomerID", customerID);
                        if (custIndex != -1)
                        {
                            // Resume data binding for the customer list and set the position to the found customer
                            bsCustomer.ResumeBinding();
                            bsCustomer.Position = custIndex;
                        }
                    }

                    // Suspend data binding for the product grade section
                    bsProductGradeItem.SuspendBinding();

                    // Try to parse the GradeID from the DataRow; if it fails, it defaults to -1
                    int gradeID = int.TryParse(row["GradeID"].ToString(), out gradeID) ? gradeID : -1;

                    // If a valid GradeID is found, set position to the matching product grade
                    if (gradeID != -1)
                    {
                        int pgIndex = bsProductGradeItem.Find("GradeID", gradeID);
                        if (pgIndex != -1)
                        {
                            // Resume data binding for product grade and set the position to the found grade
                            bsProductGradeItem.ResumeBinding();
                            bsProductGradeItem.Position = pgIndex;
                        }
                    }

                    // Suspend data binding for the attached documents section
                    bsAttachedDoc.SuspendBinding();

                    // Look for the corresponding attached document for the current ItemId.
                    int adID = bsAttachedDoc.Find("ItemID", itemID);
                    if (adID != -1)
                    {
                        // Resume data binding for attached documents and set the position to the matching ItemID
                        bsAttachedDoc.ResumeBinding();
                        bsAttachedDoc.Position = adID;
                    }
                    else
                    {
                        // If no attached document is found for the ItemId, add a new row to the attached document data source
                        bsAttachedDoc.AddNew();
                        bsAttachedDoc.EndEdit();
                        bsAttachedDoc.ResumeBinding();

                        // Set the position to the newly added row
                        bsAttachedDoc.Position = bsAttachedDoc.Count - 1;

                        // Enable UI elemnts for adding a new row
                        btnNewRow.Enabled = true;
                        lblAddNewRow.Enabled = true;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        /// <summary>
        /// Handles the event when the selected index of the company combo box changes.
        /// It updates the product filter based on the selected company.
        /// </summary>
        /// <param name="sender"> The object that raised the event. </param>
        /// <param name="e"> The event arguments. </param>
        private void tscboCompany_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            // ## gotcha !!! ##
            // toolstrip combo automatically changes selected index to 0 when datasource changes and has been
            // programmatically set to -1;  event still fires, even if unsubscribed!!!
            // workaround:  set ChangedByCode to true !!!
            // ###
            if (tscboCompany.SelectedIndex != -1 && !ChangedByCode)
            {
                int custID = (int)tscboCompany.ComboBox.SelectedValue;
                if (custID != LastCustomerID)
                {
                    SetProductFilter(custID);
                    LastCustomerID = custID;
                }
            }
        }


        /// <summary>
        /// Sets the product filter based on the selected customer and optionally an item ID.
        /// Filters the products to show only those related to the selected customer.
        /// </summary>
        /// <param name="custID"> The ID of the selected customer. </param>
        /// <param name="itemID"> The ID of the selected item (optional, default is 0). </param>
        private void SetProductFilter(int custID, int itemID = 0)
        {
            try
            {
                LastCustomerID = custID;
                CustomerFilterOn = true;
                DataTable dt = dsAttachedDoc.Tables["CustomerProduct"].Copy();
                DataView dv = new DataView(dt, "CustomerID = " + custID.ToString(), "CustomerID", DataViewRowState.CurrentRows);
                DataTable dt1 = dv.ToTable();
                DataTable dt2 = dsAttachedDoc.Tables["Product"];
                dt2.DefaultView.RowFilter = "";
                string rowFilter = "";
                if (dt1.Rows.Count == 0)
                {
                    MessageBox.Show("This customer has no products. You may wish to add this customer for an existing product",
                                            "Add customer for product", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    EnableGroups(false);
                }
                else
                {
                    var ids = dt1.AsEnumerable().Select(r => r.Field<int>("ItemID"));
                    //dt2.DefaultView.RowFilter = string.Format("ItemID in ({0})", string.Join(",", ids));
                    rowFilter = string.Format("ItemID in ({0})", string.Join(",", ids));
                    //itemID = ids.FirstOrDefault(); //sets itemID to first element, if not located
                    int defaultItemID = ids.FirstOrDefault();  //selects first item
                    LastItemID = (rowFilter.Contains(itemID.ToString())) ? itemID : defaultItemID;
                }

                //populate navigation bar Product dropdown
                DataView vp = new DataView(dt2);
                vp.RowFilter = rowFilter;
                vp.Sort = "ITEMDESC ASC";
                tscboProduct.SelectedIndexChanged -= tscboProduct_SelectedIndexChanged;  //doesnt work!!
                ChangedByCode = true;  //workaround to force exit to SelectedIndexChanged event  
                tscboProduct.ComboBox.DataBindings.Clear();                              
                tscboProduct.ComboBox.DataSource = vp;
                tscboProduct.ComboBox.ValueMember = "ItemID";
                tscboProduct.ComboBox.DisplayMember = "ITEMDESC";
                tscboProduct.ComboBox.SelectedIndexChanged += tscboProduct_SelectedIndexChanged;

                // populate navigation bar code dropdown
                DataView vc = new DataView(dt2);
                vc.RowFilter = rowFilter;
                vc.Sort = "ITEMNMBR ASC";
                tscboCode.ComboBox.SelectedIndexChanged -= tscboCode_SelectedIndexChanged;
                tscboCode.ComboBox.DataBindings.Clear();
                tscboCode.ComboBox.DataSource = vc;
                tscboCode.ComboBox.ValueMember = "ItemID";
                tscboCode.ComboBox.DisplayMember = "ITEMNMBR";
                tscboCode.ComboBox.SelectedIndexChanged += tscboCode_SelectedIndexChanged;
                ChangedByCode = false;

                if (dt1.Rows.Count > 0)
                {                    
                    if (bsManItems == null)
                    {
                        BindControls();
                        FormatAttachedDocGrid();
                    }
                    bsManItems.Filter = rowFilter;
                    int itemIndex = bsManItems.Find("ItemID", itemID);
                    if (itemID != -1)
                    {
                        EnableGroups(true);
                        bsManItems.Position = itemIndex;
                        tscboProduct.ComboBox.SelectedIndexChanged += tscboProduct_SelectedIndexChanged;
                        tscboCode.ComboBox.SelectedIndexChanged += tscboCode_SelectedIndexChanged;
                    }
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
            //gpGeneral.Enabled = flag;
            //gpQCInstruction.Enabled = flag;
            //dgvQCInstruction.Visible = flag;
            dgvAttachedDocs.Visible = flag; 

            //enable NavigationBar controls
            foreach (Object o in bindingNavigator1.Items)
            {
                if (o.GetType() == typeof(System.Windows.Forms.ToolStripComboBox))
                {
                    ToolStripComboBox tscb = (ToolStripComboBox)o;
                    if (tscb.Name != "tscboCompany")
                        tscb.Enabled = flag;
                }
                if (o.GetType() == typeof(ToolStripLabel))
                {
                    ToolStripLabel lb = (ToolStripLabel)o;
                    if (lb.Text != "Company")
                        lb.ForeColor = flag ? Color.Black : SystemColors.GrayText;
                }
            }

            btnReport.Enabled = flag;
        }

        private void SetDropDownIndex(ToolStripComboBox tscb, int itemID)
        {
            try
            {
                int selectedIndex = -1;
                for (int i = 0; i < tscb.ComboBox.Items.Count; i++)
                {
                    DataRowView drv = tscb.ComboBox.Items[i] as DataRowView;
                    DataRow row = drv.Row;

                    if (row != null)
                    {
                        //string displayValue = row["ITEMNMBR"].ToString();
                        //Debug.Print(displayValue);
                        int testItemID = Convert.ToInt32(row["ItemID"].ToString());
                        if (testItemID == itemID)
                        {
                            selectedIndex = i;
                            tscb.ComboBox.SelectedIndex = selectedIndex;
                            break;
                        }
                    }
                }
            }
            catch { }
        }
    }
}
