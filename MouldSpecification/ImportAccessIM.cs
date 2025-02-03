using System;
using System.Windows.Forms;

namespace MouldSpecification
{
    public partial class ImportAccessIM : Form
    {
        public ImportAccessIM()
        {
            InitializeComponent();
            string[] taskItems =
            {
              "Updating work tables",
              "Scanning image folders",
              "Updating product details...",
              "Updating masterbatch...",
              "Updating machine preference...",
              "Updating material composition...",
              "Importing Customers...",
              "Updating customer costing...",
              "Updating IM specifications...",
              "Updating Quality Control...",
              "Updating Packaging..."
            };

            checkedTaskList.Items.AddRange(taskItems);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ImportAccessIM_Shown(object sender, EventArgs e)
        {
            try
            {
                UseWaitCursor = true;
                Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                MainFormDAL dal = new MainFormDAL();
                dal.UpdateAccessWorkTables();

                checkedTaskList.SetItemChecked(0, true);
                Application.DoEvents();

                dal.UpdateAttachmentFilepaths();
                checkedTaskList.SetItemChecked(1, true);
                Application.DoEvents();

                dal.UpdateFromAccessImport();
                checkedTaskList.SelectedIndex = 2;
                checkedTaskList.Items[2] = "Imported Product details"
                    + " - rows added: " + dal.RowsAdded.ToString()
                    + "; updated: " + dal.RowsUpdated;
                checkedTaskList.SetItemChecked(2, true);
                Application.DoEvents();

                dal.ImportMasterBatchComp();
                checkedTaskList.SetItemChecked(3, true);
                checkedTaskList.Items[3] = "Imported Masterbatch"
                    + " - rows added: " + dal.RowsAdded.ToString()
                    + "; updated: " + dal.RowsUpdated;
                Application.DoEvents();

                dal.ImportMachinePref();
                checkedTaskList.SetItemChecked(4, true);
                checkedTaskList.Items[4] = "Imported Machine Preference:"
                    + " - rows added: " + dal.RowsAdded.ToString()
                    + "; updated: " + dal.RowsUpdated;
                Application.DoEvents();

                dal.ImportMaterialComp();
                checkedTaskList.SetItemChecked(5, true);
                checkedTaskList.Items[5] = "Imported Material Composition"
                    + " - rows added: " + dal.RowsAdded.ToString()
                    + "; updated: " + dal.RowsUpdated;
                Application.DoEvents();

                dal.ImportCustomer();
                checkedTaskList.SetItemChecked(6, true);
                checkedTaskList.Items[6] = "Imported Customer"
                    + " - rows added: " + dal.RowsAdded.ToString()
                    + "; updated: " + dal.RowsUpdated;
                Application.DoEvents();

                dal.ImportCustomerCosting();
                checkedTaskList.SetItemChecked(7, true);
                checkedTaskList.Items[7] = "Imported Customer Costing"
                    + " - rows added: " + dal.RowsAdded.ToString()
                    + "; updated: " + dal.RowsUpdated;
                Application.DoEvents();

                dal.ImportIMSpecification();
                checkedTaskList.SetItemChecked(8, true);
                checkedTaskList.Items[8] = "Imported IM Specification"
                    + " - rows added: " + dal.RowsAdded.ToString()
                    + "; updated: " + dal.RowsUpdated;
                Application.DoEvents();

                dal.ImportQualityControl();
                checkedTaskList.SetItemChecked(9, true);
                checkedTaskList.Items[9] = "Imported Quality Control"
                    + " - rows added: " + dal.RowsAdded.ToString()
                    + "; updated: " + dal.RowsUpdated;
                Application.DoEvents();

                dal.ImportPackaging();
                checkedTaskList.SetItemChecked(10, true);
                checkedTaskList.Items[10] = "Imported Packaging"
                    + " - rows added: " + dal.RowsAdded.ToString()
                    + "; updated: " + dal.RowsUpdated;
                Application.DoEvents();

                UseWaitCursor = false;
                Cursor = Cursors.Default;

                lblResult.Text = "Import Completed.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
