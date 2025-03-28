using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static Utils.DrawingUtils;

namespace MouldSpecification
{
    /// <summary>
    /// Represents the form for managing and displaying additive cost information.
    /// </summary>
    public partial class AdditiveCost : Form
    {
        /// <summary>
        /// Indicates whether the form is currently in a loading state.
        /// </summary>
        bool bIsLoading = true;

        /// <summary>
        /// Indicates whether the form is currently ina loading state.
        /// </summary>
        bool nonNumberEntered;

        /// <summary>
        /// Holds the data set containing additive cost information.
        /// </summary>
        DataSet dsAdditive;

        /// <summary>
        /// Stores the screen resolution size for dynamic UI adjusments.
        /// </summary>
        Size screenRes = ScreenRes();

        /// <summary>
        /// Intializes a new instance of the <see cref="AdditiveCost"/> form.
        /// </summary>
        public AdditiveCost()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event handler for the form's Load event.
        /// Initializes data and componenets when the form loads.
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> An object that contains event data. </param>
        private void AdditiveForm_Load(object sender, EventArgs e)
        {
            // empty method
        }

        /// <summary>
        /// Loads data into the grid view and applies formatting to its columns and headers.
        /// </summary>
        private void LoadGrid()
        {
            // Fetch additive  cost data using the DAL (Data Access Layer).
            dsAdditive = new AdditiveCostDAL().SelectAdditiveCost();

            // Apply styling to the DataGridView's header.
            DataGridViewCellStyle style = dgvEdit.ColumnHeadersDefaultCellStyle;

            // Set header background color 
            style.BackColor = Color.Navy;

            // Set header text color.
            style.ForeColor = Color.White;

            // Make header text bold.
            style.Font = new Font(dgvEdit.Font, FontStyle.Bold);

            // Set header border style.
            dgvEdit.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;

            // Set cell border style.
            dgvEdit.CellBorderStyle = DataGridViewCellBorderStyle.Single;

            // Set grid line color.
            dgvEdit.GridColor = SystemColors.ActiveBorder;

            // Disable default header styles.
            dgvEdit.EnableHeadersVisualStyles = false;

            // Automatically generate columns from the data source.
            dgvEdit.AutoGenerateColumns = true;

            // Enable header text wrapping.
            dgvEdit.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // Adjust row header width based on screen resolution.
            dgvEdit.RowHeadersWidth = Convert.ToInt32(26 * screenRes.Width / 96);

            // Bind the data set to the DataGridView.
            dgvEdit.DataSource = dsAdditive.Tables[0];

            // Set custom column header and formats.
            dgvEdit.Columns["CostPerKg"].HeaderText = "Cost/kg     $";

            // Align cell contents to the right.
            dgvEdit.Columns["CostPerKg"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // Format as a number with 3 decimal places.
            dgvEdit.Columns["CostPerKg"].DefaultCellStyle.Format = "N3";

            // Set column width.
            dgvEdit.Columns["CostPerKg"].Width = 120;

            // Set widths for other columns.
            dgvEdit.Columns["Additive"].Width = 400;
            dgvEdit.Columns["Comment"].Width = 400;
            dgvEdit.Columns["Description"].Width = 400;

            // Set custom header text for specific columns.
            dgvEdit.Columns["AdditiveCOde"].HeaderText = "Additive Code";

            // Hide columns that should not be displayed to the user.
            dgvEdit.Columns["AdditiveID"].Visible = false;
            dgvEdit.Columns["last_updated_by"].Visible = false;
            dgvEdit.Columns["last_updated_on"].Visible = false;
        }

        /// <summary>
        /// Handles the event when a user attempts to delete a row in the DataGridView.
        /// Displays a confirmation dialog before allowing the deletion.
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> Provides data for the <see cref="DataGridViewRowCancelEventArgs"/> event. </param>
        private void dgvEdit_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            // Check if the row being deleted is not a new row.
            if (!e.Row.IsNewRow)
            {
                // Show a confirmation dialog to confirm deletion 
                DialogResult response = MessageBox.Show("Are you sure?", "Delete row?",MessageBoxButtons.YesNo,MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                // If the user selects 'No', cancel the row deletion.
                if (response == DialogResult.No)
                    e.Cancel = true;
            }
        }

        /// <summary>
        /// Handles the event triggered when the form is closed.
        /// Ensures data integrity by validating and saving changes made in the DataGridView.
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> Provides data for the <see cref="FormClosedEventArgs"/> event. </param>
        private void AdditiveCost_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                // Check if the current row in the DataGridView is being edited.
                if (dgvEdit.IsCurrentRowDirty)
                {
                    // Validate the current row in the DataGridView is being edited.
                    this.Validate();
                }

                // End any edits being made in the DataGridView.
                dgvEdit.EndEdit();

                // Clear the DataGridView's data source to release resources.
                dgvEdit.DataSource = null;

                // Save the changes in the dataset to the database using the DAL.
                new AdditiveCostDAL().UpdateAdditive(dsAdditive);

            }
            catch
            {
                // Rethrow the exception for higher-level handling.
                throw;
            }
        }

        /// <summary>
        /// Validates cell values in the DataGridView during editing to prevent duplicate entries.
        /// Ensures the uniqueness of values in the "Additive" and "AdditiveCode" columns.
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> Provides data for the <see cref="DataGridViewCellValidatingEventArgs"/> event. </param>
        private void dgvEdit_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // Check if the cell being validated belongs to the "Additive" column.
            if (dgvEdit.Columns[e.ColumnIndex].DataPropertyName == "Additive"
                && e.FormattedValue.ToString() != dgvEdit.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString())
            {
                // Get the data source of the DataGridView as a DataTable.
                DataTable dt = (DataTable)dgvEdit.DataSource;

                // Check for rows with the same "Additive" value as the new input.
                DataRow[] rows = dt.Select("Additive = '" + e.FormattedValue + "'");

                // If duplicate rows exist, show an error message and cancel the edit.
                if (rows.Length > 0)
                {
                    MessageBox.Show("This Additive is already used.");
                    e.Cancel = true;
                }
            }

            // Check if the cell being validated belongs to the "AdditiveCode" column. 
            if (dgvEdit.Columns[e.ColumnIndex].DataPropertyName == "AdditiveCode"
                && e.FormattedValue.ToString() != dgvEdit.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString())
            {
                // Get the data source of the DataGridView as a DataTable.
                DataTable dt = (DataTable)dgvEdit.DataSource;

                // Check for rows  with the same "AdditiveCode" value as the new input.
                DataRow[] rows = dt.Select("AdditiveCode = '" + e.FormattedValue + "'");

                // If duplicate rows exist, show an error message and cancel the edit.
                if (rows.Length > 0)
                {
                    MessageBox.Show("This AdditiveCode is already used.");
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Event handler for the form's Shown event.
        /// Sets the form's size and loads the grid with data when the form is displayed.
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> An object that contains event data. </param>
        private void AdditiveCost_Shown(object sender, EventArgs e)
        {
            // Set the form's size to a fixed resolution.
            this.Size = new Size(1500, 1500);
            this.splitContainer1.SplitterDistance = p96H(40);

            // Load data into the grid and apply formatting.
            LoadGrid();
        }
    }
}
