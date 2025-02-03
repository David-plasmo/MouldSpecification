using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MouldSpecification
{
    /// <summary>
    /// A form to manage customer information for Injection Moulded products.
    /// </summary>    
    public partial class Customer : Form
    {       
        public int CustomerID { get; set; } 

        DataSet dsCustomer, dsPostCode;
        DataTable dtCustomer, dtPostCode;
        DataView selectedCity, selectedPostCode;
        BindingSource bsCustomer;

        int lastCustID;      // tracks when navigation selector changes to different customer
        bool ignoreSelect;   // flag to ignore SelectedIndexChanged event in tscboCustomer

        public Customer(int customerID)
        {
            InitializeComponent();
            CustomerID = customerID;               
        }
        
        private void Customer_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                CustomerDAL dal = new CustomerDAL();
                dsCustomer = dal.SelectCustomer("IM");               
                dsCustomer.AcceptChanges();  // only changes made hereafter will count as changes. 
                dtCustomer = dsCustomer.Tables[0];
                dtCustomer.TableName = "Customer";
                //selectedCustomer = dtCustomer.DefaultView;
                //selectedCustomer.RowFilter = "CustomerID = " + CustomerID.ToString();

                // Create a binding source for navigation
                bsCustomer = new BindingSource();
                bsCustomer.DataSource = dtCustomer;
                bnCustomer.BindingSource = new BindingSource();
                bnCustomer.BindingSource =  bsCustomer;
                bsCustomer.AddingNew += bsCustomer_AddingNew;
                bsCustomer.CurrentChanged += bsCustomer_CurrentChanged;

                tscboCustomer.ComboBox.DataSource = dal.SelectCustomerIndex("IM").Tables[0];               
                tscboCustomer.ComboBox.DisplayMember = "CUSTNAME";
                tscboCustomer.ComboBox.ValueMember = "CustomerID";
                                    
                // Bind the ComboBox control for company database selection - items are hardcoded: PL, CP, AN
                cboCompDB.DataBindings.Add(new Binding("SelectedItem", bsCustomer, "CompDB"));

                // Bind customer name and number text boxes
                txtCUSTNAME.DataBindings.Add(new Binding("Text", bsCustomer, "CUSTNAME", true));
                txtCUSTNMBR.DataBindings.Add(new Binding("Text", bsCustomer, "CUSTNMBR", true));

                // Load and bind comboboxes
                //   Customer class
                DataTable dt = dal.SelectCustomerClass().Tables[0];
                cboCUSTCLAS.DataSource = dt;
                cboCUSTCLAS.DisplayMember = "CLASSID";
                cboCUSTCLAS.ValueMember = "CLASSID";
                cboCUSTCLAS.DataBindings.Add(new Binding("SelectedValue", bsCustomer, "CUSTCLAS"));
                cboCUSTCLAS.SelectedIndex = -1;

                //   Shipping method
                dt = dal.SelectShipMethod().Tables[0];
                cboSHIPMTHD.DataSource = dt;
                cboSHIPMTHD.DisplayMember = "SHIPMTHD";
                cboSHIPMTHD.ValueMember = "SHIPMTHD";
                cboSHIPMTHD.DataBindings.Add(new Binding("SelectedValue", bsCustomer, "SHIPMTHD"));

                //   Address ID
                dt = dal.SelectAddressID().Tables[0];
                cboADRSCODE.DataSource = dt;
                cboADRSCODE.DisplayMember = "ADRSCODE";
                cboADRSCODE.ValueMember = "ADRSCODE";
                cboADRSCODE.DataBindings.Add(new Binding("SelectedValue", bsCustomer, "ADRSCODE"));

                //   Payment terms
                dt = dal.SelectPaymentTerms().Tables[0];
                cboPYMTRID.DataSource = dt;
                cboPYMTRID.DisplayMember = "PYMTRMID";
                cboPYMTRID.ValueMember = "PYMTRMID";
                cboPYMTRID.DataBindings.Add(new Binding("SelectedValue", bsCustomer, "PYMTRMID"));

                //   Country
                dt = dal.SelectCountry().Tables[0];
                cboCountry.DataSource = dt;
                cboCountry.DisplayMember = "Country";
                cboCountry.ValueMember = "Country";
                cboCountry.DataBindings.Add(new Binding("SelectedValue", bsCustomer, "COUNTRY"));
                cboCountry.SelectedIndexChanged += cboCountry_SelectedIndexChanged;

                //   State (valid only for Australia)                
                dt = dal.SelectState().Tables[0];                
                cboState.DataSource = dt;
                cboState.DisplayMember = "state";
                cboState.ValueMember = "state";
                cboState.DataBindings.Add(new Binding("SelectedValue", bsCustomer, "STATE"));
                cboState.SelectedIndexChanged += cboState_SelectedIndexChanged;
                cboState.SelectedIndex = -1;

                //   City
                dt = dal.SelectSuburb().Tables[0];
                selectedCity = dt.DefaultView;                
                cboCity.DataSource = dt;
                cboCity.DisplayMember = "suburb";
                cboCity.ValueMember = "suburb";
                cboCity.DataBindings.Add(new Binding("SelectedValue", bsCustomer, "CITY"));
                cboCity.SelectedIndexChanged += cboCity_SelectedIndexChanged;
                cboCity.SelectedIndex = -1;

                //   post code
                dtPostCode = dal.SelectPostCodes().Tables[0];
                selectedPostCode = dtPostCode.DefaultView;
                cboPostCode.DataSource = dtPostCode;               
                cboPostCode.DisplayMember = "postcode";
                cboPostCode.ValueMember = "postcode";
                cboPostCode.DataBindings.Add(new Binding("SelectedValue", bsCustomer, "ZIP"));
                cboPostCode.SelectedIndexChanged += cboPostCode_SelectedIndexChanged;
                cboPostCode.SelectedIndex = -1;

                //bind textboxes               
                txtCNTCPRSN.DataBindings.Add(new Binding("Text", bsCustomer, "CNTCPRSN", true));                
                txtADDRESS1.DataBindings.Add(new Binding("Text", bsCustomer, "ADDRESS1", true));
                txtADDRESS2.DataBindings.Add(new Binding("Text", bsCustomer, "ADDRESS2", true));
                txtADDRESS3.DataBindings.Add(new Binding("Text", bsCustomer, "ADDRESS3", true));                
                txtPHONE1.DataBindings.Add(new Binding("Text", bsCustomer, "PHONE1", true));
                txtPHONE2.DataBindings.Add(new Binding("Text", bsCustomer, "PHONE2", true));
                txtPHONE3.DataBindings.Add(new Binding("Text", bsCustomer, "PHONE3", true));
                txtFAX.DataBindings.Add(new Binding("Text", bsCustomer, "FAX", true));                
                txtlast_updated_by.DataBindings.Add(new Binding("Text", bsCustomer, "last_updated_by", true));
                
                //readonly 
                txtlast_updated_on.DataBindings.Add(new Binding("Text", bsCustomer, "last_updated_on", true, DataSourceUpdateMode.Never, "dd-MM-yyyy HH:mm:ss"));

                Cursor.Current = Cursors.Default;

            }   

            // Display an error message if data binding fails.
            catch (Exception ex) { MessageBox.Show(ex.Message, "Customer_Load", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void cboCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCountry.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)cboCountry.SelectedItem;
                string country = drv["Country"].ToString();
                cboState.Enabled = (country == "Australia") ? true : false;
                cboCity.Enabled = (country == "Australia") ? true : false;
                cboPostCode.Enabled = (country == "Australia") ? true : false;
                cboState.SelectedIndex = -1;
                cboCity.SelectedIndex = -1;
                cboPostCode.SelectedIndex = -1;
            }
        }

        private void cboState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboState != null && cboState.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)cboState.SelectedItem;
                string state = drv["state"].ToString();
                selectedCity.RowFilter = "state = '" + state + "'";
                drv = (DataRowView)bsCustomer.Current;
                DataRow row = drv.Row;
                row["STATE"] = state;
                cboCity.SelectedIndex = -1;
                cboPostCode.SelectedIndex = -1;
            }
        }

        private void cboCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCity != null && cboCity.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)cboCity.SelectedItem;
                string suburb = drv["suburb"].ToString();
                string state = drv["state"].ToString();
                selectedPostCode.RowFilter = ("state = '" + state + "' AND suburb = '" + suburb + "'");
                //string postcode = drv["postcode"].ToString();
                drv = (DataRowView)bsCustomer.Current;
                DataRow row = drv.Row;
                row["CITY"] = suburb;
                row["STATE"] = state;
                //row["ZIP"] = postcode;
                bsCustomer.EndEdit(); 
                cboPostCode.SelectedIndex = -1;
            }
        }

        private void cboPostCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPostCode != null && cboPostCode.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)cboPostCode.SelectedItem;
                string suburb = drv["suburb"].ToString();
                string state = drv["state"].ToString();
                string postcode = drv["postcode"].ToString();
                //selectedCity.RowFilter = ("state = '" + state + "' AND suburb = '" + suburb + "'");
                //string postcode = drv["postcode"].ToString();
                drv = (DataRowView)bsCustomer.Current;
                DataRow row = drv.Row;
                row["CITY"] = suburb;
                row["STATE"] = state;
                row["ZIP"] = postcode;
                bsCustomer.EndEdit();
            }
        }


        private void tscboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(cboCustomer.SelectedValue.ToString());
            if (!ignoreSelect && tscboCustomer.ComboBox != null && tscboCustomer.ComboBox.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)tscboCustomer.ComboBox.SelectedItem;
                int valueOfItem = (int)drv["CustomerID"];
                //selectedCustomer.RowFilter = "CustomerID = " + valueOfItem.ToString();
                int itemFound = bsCustomer.Find("CustomerID", valueOfItem);
                if (itemFound != -1)
                    bsCustomer.Position = itemFound;
            }
        }

        private void bsCustomer_AddingNew(object sender, AddingNewEventArgs e)
        {
            dtCustomer.Columns["CompDB"].DefaultValue = "CP";
            dtCustomer.Columns["COUNTRY"].DefaultValue = "Australia";
            dtCustomer.Columns["SHIPMTHD"].DefaultValue = "FOB";
            dtCustomer.Columns["PYMTRMID"].DefaultValue = "30 days after EOM";
            dtCustomer.Columns["ADRSCODE"].DefaultValue = "PHYSICAL";
            dtCustomer.Columns["CustomerID"].DefaultValue = -1;
        }

        private void bsCustomer_CurrentChanged(object sender, EventArgs e)
        {
            DataRowView drv = (DataRowView)bsCustomer.Current;
            DataRow row = (DataRow)drv.Row;
            
            if (row["CompDB"].ToString().Length == 0)
                row["CompDB"] = "CP";
            if (row["COUNTRY"].ToString().Length == 0)            
                row["COUNTRY"] = "Austalia";
            if (row["SHIPMTHD"].ToString().Length == 0)
                row["SHIPMTHD"] = "FOB";
            if (row["PYMTRMID"].ToString().Length == 0)
                row["PYMTRMID"] = "30 days after EOM";
            if (row["ADRSCODE"].ToString().Length == 0)
                row["ADRSCODE"] = "PHYSICAL";
            row.EndEdit();
            bsCustomer.EndEdit(); 
            bsCustomer.ResumeBinding();

            //sync toolbar customer dropdown with current customer
            int curCustomerID = (int)row["CustomerID"];
            if (curCustomerID != -1 && curCustomerID != CustomerID)
            {
                //this doesn't work !!!
                //tscboCustomer.SelectedIndexChanged -= tscboCustomer_SelectedIndexChanged;
                //try this 
                ignoreSelect = true;
                tscboCustomer.ComboBox.SelectedValue = curCustomerID;
                ignoreSelect = false;
                CustomerID = curCustomerID;
                //tscboCustomer.SelectedIndexChanged += tscboCustomer_SelectedIndexChanged;
                tscboCustomer.Enabled = true;
            }
            //sets form for new customer
            else if (curCustomerID == -1)
            {
                ////doesn't work -- selectedindex will reset to 0
                //ignoreSelect = true;  
                //tscboCustomer.ComboBox.SelectedIndex = -1; 
                //ignoreSelect = false;
                
                //disable instead
                tscboCustomer.Enabled = false;

            }                
        }


        private void SaveEdits()
        {
            try
            {
                bsCustomer.EndEdit();
                new CustomerDAL().UpdateCustomer(dsCustomer);
            }
            catch (Exception ex) 
            {                 
                MessageBox.Show(ex.Message, "Customer.SaveEdits", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void Customer_Shown(object sender, EventArgs e)
        {

            //synchronise toolbar customer cbo with input CustomerID
            ignoreSelect = false;
            tscboCustomer.ComboBox.SelectedIndexChanged += tscboCustomer_SelectedIndexChanged;
            tscboCustomer.ComboBox.SelectedValue = CustomerID;                        
        }

        private void BsCustomer_CurrentChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            SaveEdits();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tsbtnAccept_Click(object sender, EventArgs e)
        {
            // Save changes made to the customer data.
            SaveEdits();
        }
        
        private void tsbtnCancel_Click(object sender, EventArgs e)
        {
            // Close the form without saving changes.
            this.Close();
        }

        /// <remarks>
        /// Prompts the user for confirmation before deleting the currently selected customer.
        /// If the customer has associated products, an additional confirmation prompt is shown.
        /// If confirmed, the current record is removed from the binding source.
        /// </remarks>
        private void tsbtnDelete_Click(object sender, EventArgs e)
        {
            // Flag to determine if the delete action should be canceled
            bool cancelDelete = true;

            // Retrieve the BindingSource associated with the BindingNavigator
            BindingSource bs = this.bnCustomer.BindingSource;

            // Display confirmation prompt for the delete action
            if (MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // Proceed with deletion unless overridden later.
                cancelDelete = false;    
                
                if (bs != null)
                {
                    // Ensure there is a selected position in the binding source
                    if (bsCustomer.Position != -1)
                    {
                        // Get the current DataRow from the binding source
                        DataRow row = ((DataRowView)bsCustomer.Current).Row;
                        int customerID = (int)row["CustomerID"];

                        // Check if the customer has associated products
                        if (new CustomerDAL().CustomerHasProducts(customerID))
                        {
                            // Display additional confirmation prompt for customer with products
                            if (MessageBox.Show("This Customer has products. Confirm Delete?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                // Proceed with deletion
                                cancelDelete = false;
                            }
                            else

                                // Cancel deletion
                                cancelDelete = true;
                        }
                    }
                }
            }

            // Remove the current record from the binding source if deletion is confirmed
            if (!cancelDelete)

               bsCustomer.RemoveCurrent();      
        }
    }
}
