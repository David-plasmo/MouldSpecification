using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static Utils.DrawingUtils;

namespace MouldSpecification
{
    /// <summary>
    /// Represents the Customer Price and Quantity form.
    /// This form allows the user to manage price and quantity details for customers.
    /// </summary>
    public partial class CustomerPriceQty : Form
    {
        /// <summary>
        /// Dataset to hold customer price and quantit data.
        /// </summary>
        DataSet dsCustomerPriceQty;

        /// <summary>
        /// Indicates whether a non-numeric key was entered.
        /// </summary>
        bool nonNumberEntered;

        /// <summary>
        /// Holds the current item ID.
        /// </summary>
        int? curItemID = null;

        /// <summary>
        /// Holds the current customer ID.
        /// </summary>
        int? curCustID = null;

        /// <summary>
        /// Stores the screen resolution.
        /// </summary>
        Size screenRes = ScreenRes();

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerPriceQty"/> class with specified item and customer IDs.
        /// </summary>
        /// <param name="itemID"> The item ID to initialize with. </param>
        /// <param name="custID"> The customer ID to initialize with. </param>
        public CustomerPriceQty(int? itemID, int? custID)
        {
            InitializeComponent();

            // Attach event handlers
            this.Load += CustomerPriceQty_Load;
            this.dgvEdit.KeyDown += new KeyEventHandler(dgvEdit_KeyDown);
            this.dgvEdit.PreviewKeyDown += new PreviewKeyDownEventHandler(dgvEdit_PreviewKeyDown);

            // Assign item and customer IDs if provided
            if (itemID != null) { curItemID = itemID; }
            if (custID != null) { curCustID = custID; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerPriceQty"/> class.
        /// </summary>
        public CustomerPriceQty()
        {            
        }

        /// <summary>
        /// Handles the PreviewKeyDown event of the dgvEdit DataGridView.
        /// Allows for specific handling of key events on the "DateChanged" column.
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> The <see cref="PreviewKeyDownEventArgs"/> instance containing the event data. </param>
        void dgvEdit_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // Check if the current cell belongs to the "DataChanged" column.
            if (dgvEdit.CurrentCell.OwningColumn.Name == "DateChanged")
            {
                // 
                if (e.KeyData == Keys.Delete)
                {
                    //MessageBox.Show("Preview=" + e.KeyData.ToString());
                    KeyEventArgs kd = new KeyEventArgs(e.KeyData);
                    cec_KeyDown(sender, kd);
                }
            }

        }

        /// <summary>
        /// Handles the KeyDown event for the DataGridView control 'dgvEdit'. 
        /// Allows clearing the values of the currently selected cell when the Delete key is pressed.
        /// </summary>
        /// <param name="sender"> The source of the event (the DataGridView). </param>
        /// <param name="e"> Provides data for the KeyDown event. </param>
        private void dgvEdit_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Delete key was pressed.
            if (e.KeyCode == Keys.Delete)
            { 
                // Set the values of the currently selected cell to null (clear the cell).
                dgvEdit.CurrentCell.Value = null; 
            }
        }

        /// <summary>
        /// Handles the Load event for the 'CustomerPriceQty' form.
        /// Initializes the form when it is loaded.
        /// </summary>
        /// <param name="sender"> The source of the event (the form). </param>
        /// <param name="e"> Provides data for the Load event. </param>
        private void CustomerPriceQty_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Refreshes and configures the 'dgvEdit' DataGridView with updated data,
        /// including formatting, adding custom columns, and setting default values.
        /// </summary>
        private void RefreshGrid()
        {
            try
            {
                // Disable the ability to add new rows temporarily
                dgvEdit.AllowUserToAddRows = false;

                // Retriew updated data for the grid
                CustomerPriceQtyDAL dal = new CustomerPriceQtyDAL();
                dsCustomerPriceQty = dal.SelectCustomerCosting(curItemID, curCustID);

                // Check if current item and customer IDs are valid
                if (curItemID != null & curCustID != null)
                {
                    // Set default values for the data table.
                    DataTable dtPQ = dsCustomerPriceQty.Tables[0];
                    dtPQ.Columns["ItemID"].DefaultValue = curItemID;
                    dtPQ.Columns["CustomerID"].DefaultValue = curCustID;
                    dgvEdit.AllowUserToAddRows = true;
                }

                // Configure column header styles
                DataGridViewCellStyle style = dgvEdit.ColumnHeadersDefaultCellStyle;
                style.BackColor = Color.Navy;
                style.ForeColor = Color.White;
                style.Font = new Font(dgvEdit.Font, FontStyle.Bold);

                dgvEdit.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                dgvEdit.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                dgvEdit.GridColor = SystemColors.ActiveBorder;
                dgvEdit.EnableHeadersVisualStyles = false;
                dgvEdit.AutoGenerateColumns = true;
                dgvEdit.DataSource = dsCustomerPriceQty.Tables[0];
                dgvEdit.Columns["CostID"].Visible = false;
                dgvEdit.Columns["ItemID"].Visible = false;
                dgvEdit.Columns["last_updated_by"].Visible = false;
                dgvEdit.Columns["last_updated_on"].Visible = false;
                dgvEdit.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                dgvEdit.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
                dgvEdit.RowHeadersWidth = Convert.ToInt32(26 * screenRes.Width / 96);

                //setup dropdown columns for code and description
                DataGridViewComboBoxColumn cbcCode = new DataGridViewComboBoxColumn();
                DataSet dsManItemIndex = dal.SelectMAN_ItemIndex();
                DataTable dt = dsManItemIndex.Tables[0];
                cbcCode.DataSource = dt;
                cbcCode.ValueMember = "ItemID";
                cbcCode.DataPropertyName = "ItemID";
                cbcCode.DisplayMember = "ITEMNMBR";
                cbcCode.Name = "Code";
                dgvEdit.Columns.Insert(2, cbcCode);
                dgvEdit.Columns["Code"].HeaderText = "Code";
                cbcCode.DisplayStyleForCurrentCellOnly = true;
                cbcCode.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

                DataGridViewComboBoxColumn cbcDescription = new DataGridViewComboBoxColumn();
                cbcDescription.DataSource = dt;
                cbcDescription.ValueMember = "ItemID";
                cbcDescription.DataPropertyName = "ItemID";
                cbcDescription.DisplayMember = "ITEMDESC";
                cbcDescription.Name = "Description";
                dgvEdit.Columns.Insert(3, cbcDescription);
                dgvEdit.Columns["Description"].HeaderText = "Description";
                cbcDescription.DisplayStyleForCurrentCellOnly = true;
                cbcDescription.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

                //setup Customer dropdown
                DataGridViewComboBoxColumn cbcCustomer = new DataGridViewComboBoxColumn();
                DataSet dsCustomer = new MainFormDAL().SelectCustomerIndex();
                dt = dsCustomer.Tables[0];
                cbcCustomer.DataSource = dt;
                cbcCustomer.ValueMember = "CustomerID";
                cbcCustomer.DataPropertyName = "CustomerID";
                cbcCustomer.DisplayMember = "CUSTNAME";
                cbcCustomer.Name = "Customer";
                dgvEdit.Columns.Remove("CustomerID");
                dgvEdit.Columns.Insert(4, cbcCustomer);
                dgvEdit.Columns["Customer"].HeaderText = "Customer";
                cbcCustomer.DisplayStyleForCurrentCellOnly = true;
                cbcCustomer.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;

                //setup calendar control column for DateChanged
                CalendarColumn calDateChanged = new CalendarColumn();
                calDateChanged.Name = "DateChanged";
                calDateChanged.DataPropertyName = "DateChanged";
                dgvEdit.Columns.Remove("DateChanged");
                dgvEdit.Columns.Add(calDateChanged);

                dgvEdit.Columns["PricingQty"].HeaderText = "Pricing Qty";
                dgvEdit.Columns["CalculatedPrice"].HeaderText = "Calculated Price $AUS";
                dgvEdit.Columns["CurrentPrice"].HeaderText = "Current Price $AUS";
                dgvEdit.Columns["DateChanged"].HeaderText = "Date Changed";

                dgvEdit.Columns["PricingQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvEdit.Columns["CalculatedPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvEdit.Columns["CurrentPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvEdit.Columns["DateChanged"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvEdit.Columns["PricingQty"].DefaultCellStyle.Format = "N0";
                dgvEdit.Columns["CalculatedPrice"].DefaultCellStyle.Format = "N3";
                dgvEdit.Columns["CurrentPrice"].DefaultCellStyle.Format = "N3";
                dgvEdit.Columns["DateChanged"].DefaultCellStyle.Format = "dd/MM/yyyy";

                // Set read-only properties and background colors for specific columns.
                dgvEdit.Columns["Code"].ReadOnly = true;
                dgvEdit.Columns["Description"].ReadOnly = true;
                dgvEdit.Columns["CalculatedPrice"].ReadOnly = true;
                dgvEdit.Columns["Code"].DefaultCellStyle.BackColor = Color.LightGray;
                dgvEdit.Columns["Description"].DefaultCellStyle.BackColor = Color.LightGray;

                // Attach event handler for editing control.
                this.dgvEdit.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvEdit_EditingControlShowing);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the 'dgvEdit' DataGridView.
        /// Dynamically attaches key event handlers to the editing control based on the column being edited.
        /// </summary>
        /// <param name="sender"> The source of the event (the DataGridView). </param>
        /// <param name="e"> Provides data for the EditingControlShowing event. </param>
        private void dgvEdit_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // Check if the current cell belongs to the "PricingQty" or "CurrentPrice" columns
            if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "PricingQty" || dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "CurrentPrice")
            {
                // Cast the editing control to a text box
                DataGridViewTextBoxEditingControl ec = (DataGridViewTextBoxEditingControl)e.Control;

                // Detach and reattach the KeyPress event handler to ensure it's not added multiple times.
                ec.KeyPress -= ec_KeyPress;
                ec.KeyPress += new KeyPressEventHandler(ec_KeyPress);

                // Detach and reattach the KeyDown event handler for additional handling
                ec.KeyDown -= ec_KeyDown;
                ec.KeyDown += new KeyEventHandler(ec_KeyDown);
            }

            // Check if the current cell belongs to te "DateChanged" column
            if (dgvEdit.Columns[dgvEdit.CurrentCell.ColumnIndex].Name == "DateChanged")
            {
                // Cast the editing control to a custom CalendarEditingControl
                CalendarEditingControl cec = (CalendarEditingControl)e.Control;

                // Detach and reattach the KeyDown event handler.
                cec.KeyDown -= cec_KeyDown;
                cec.KeyDown += new KeyEventHandler(cec_KeyDown);
            }
        }

        /// <summary>
        /// Handles the KeyDown event for the calendar editing control.
        /// Allows deleting the value in the cell by pressing the Delete key.
        /// </summary>
        /// <param name="sender"> The source of the event (the calendar editing control). </param>
        /// <param name="e"> Provides data for the KeyDown event. </param>
        private void cec_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Delete key was pressed
            if (e.KeyData == Keys.Delete)
            {
                // Set the current cell's value to DBNull
                dgvEdit.CurrentCell.Value = DBNull.Value;

                // Validate the changes and end the cell's edit mode
                this.Validate();
                dgvEdit.EndEdit();
            }
        }

        /// <summary>
        /// Handles the KeyPress event for the text box editing control.
        /// Prevents the input of invalid characters based on the 'nonNumberEntered' flag.
        /// </summary>
        /// <param name="sender"> The source of the event (the box editing control). </param>
        /// <param name="e"> Provides data for the KeyPress event. </param>
        private void ec_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow input by default
            e.Handled = false;

            // If a non-numeric key was pressed, suppress the keypress
            if (nonNumberEntered == true)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles the KeyDown event for the text box editing control.
        /// Detects if a non-numeric key was pressed and sets the 'nonNumberentered' flag accordingly.
        /// </summary>
        /// <param name="sender"> the source of the event (the text box editing control). </param>
        /// <param name="e"> Provides data for the KeyDown event. </param>
        private void ec_KeyDown(object sender, KeyEventArgs e)
        {
            // Initialize the flag to false at the start of the method.
            nonNumberEntered = false;

            // Check if the pressed key is not a number key, minus key, or decimal key.
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9 && e.KeyCode != Keys.OemMinus && e.KeyCode != Keys.OemPeriod)
            {
                // Check if the pressed key is not a number from the numeric keypad.
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    // Check if the pressed key is not a backspace.
                    if (e.KeyCode != Keys.Back)
                    {
                        // Set the flag to true for invalid input
                        nonNumberEntered = true;
                    }
                }
            }
        }

        /// <summary>
        /// Saves changes made in the DataGridView to the database.
        /// Ensures any pending changes are validated and updated in the dataset before calling the update method.
        /// </summary>
        private void SaveGrid()
        {
            try
            {
                // Check if the dataset is not null.
                if (dsCustomerPriceQty != null)
                {
                    // Validate the current row if it is dirty (modified but not saved)
                    if (dgvEdit.IsCurrentRowDirty)
                    {
                        // Perform control validation
                        this.Validate();
                    }

                    // End edit mode for the DataGridView
                    dgvEdit.EndEdit();

                    // Update the database with the changes in the dataset
                    new CustomerPriceQtyDAL().UpdateCustomerCosting(dsCustomerPriceQty);
                }
            }
            catch (Exception ex)
            {
                // Display any exceptions encountered during the save process
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Handles the Sown event of the form.
        /// Refreshes the DataGridView by reloading data from the database when the form is diplayed.
        /// </summary>
        /// <param name="sender"> The source of the event (the form). </param>
        /// <param name="e"> Provides data for the Shown event. </param>
        private void CustomerPriceQty_Shown(object sender, EventArgs e)
        {
            // Referesh the grid to load the latest data
            RefreshGrid();
        }

        /// <summary>
        /// Handles the FormClosed event of the form.
        /// Ensures that changes in the grid are saved when te form is closed.
        /// </summary>
        /// <param name="sender"> The source of the event (the form). </param>
        /// <param name="e"> Provides data for the FormClosed event. </param>
        private void CustomerPriceQty_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Save changes to the grid when the form is closed
            SaveGrid();
        }

        /// <summary>
        /// Handles the event triggered when a user attempts to delete a row in the DataGridView.
        /// Prompts the user with a confirmation dialog before allowing the deletion.
        /// </summary>
        /// <param name="sender"> The source of the event, typically the DataGridView. </param>
        /// <param name="e"> Provides data for the RowDeleting event. </param>
        private void dgvEdit_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            // Display a confirmation dialog to the user
            if (MessageBox.Show("Confirm Delete?", "Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                // If the user confirms, allow the deletion
                e.Cancel = false;
            }
            else
            {
                // If the user cancels, prevent the deletion.
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Handles the DataError event for the DataGridView.
        /// Prevents unhandled exceptions from crashing the application during data-binding errors.
        /// </summary>
        /// <param name="sender"> The source of the event, typically the DataGridView. </param>
        /// <param name="e"> Provides data for the DataError event. </param>
        private void dgvEdit_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
    }
}
