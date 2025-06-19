using DataService;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MouldSpecification
{
    internal class CustomerDAL : DataAccessBase
    {
        //selects customer details from any GP database, searching by company, customer code or name
        public DataSet SelectGPCustomer(string companyCode, string custNmbr, string custName)
        {
            try
            {
                SqlCommand cmd = null;
                SqlConnection cnx = new SqlConnection(GetConnectionString("PLASMO-DB-01"));
                cmd = new SqlCommand();
                cmd.Connection = cnx;
                return ExecuteDataSet(ref cmd, "PlasmoIntegration.dbo.SelectGPCustomer",
                   CreateParameter("@CompanyCode", SqlDbType.VarChar, companyCode),
                   CreateParameter("@CustNmbr", SqlDbType.VarChar, custNmbr),
                   CreateParameter("@CUSTNAME", SqlDbType.VarChar, custName));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataSet SelectCustomerClass()
        {  
            // Execute stored procedure to get customer classification.
            return ExecuteDataSet("[PlasmoIntegration].[dbo].[SelectCustomerClass]");
        }

        public DataSet SelectCustomer(string productType)
        {        
            // Execute stored procedure to get customer details.
            return ExecuteDataSet("[dbo].[SelectCustomer]",
                CreateParameter("@ProductType",SqlDbType.VarChar, productType));
        }

        public DataSet SelectCustomerIndex(string productType)
        {
            // Execute stored procedure to get customer details.
            return ExecuteDataSet("[dbo].[SelectCustomerIndex]",
                CreateParameter("@ProductType", SqlDbType.VarChar, productType));
        }

        public DataSet SelectCountry()
        {
            //populates cboCountry           
            return ExecuteDataSet("[PlasmoIntegration].[dbo].[SelectCountry]");
        }

        public DataSet SelectPostCodes()
        {
            //populates cboCountry           
            return ExecuteDataSet("[PlasmoIntegration].[dbo].[SelectPostCodes]");
        }

        public DataSet SelectState()
        {
            //populates cboCountry           
            return ExecuteDataSet("[PlasmoIntegration].[dbo].[SelectState]");
        }

        public DataSet SelectSuburb()
        {
            //populates cboCountry           
            return ExecuteDataSet("[PlasmoIntegration].[dbo].[SelectSuburb]");
        }

        public DataSet SelectShipMethod()
        {
            //populates cboCountry           
            return ExecuteDataSet("[dbo].[SelectShipMethod]");
        }

        public DataSet SelectAddressID()
        {
            //populates cboCountry           
            return ExecuteDataSet("[dbo].[SelectAddressID]");
        }

        public DataSet SelectPaymentTerms()
        {
            //populates cboCountry           
            return ExecuteDataSet("[dbo].[SelectPaymentTerms]");
        }

        public void UpdateCustomer(DataSet ds)
        {
            try
            {
                try
                {
                    
                    // Process added rows.
                    DataViewRowState dvrs = DataViewRowState.Added;
                    DataRow[] rows = ds.Tables[0].Select("", "", dvrs);

                    for (int i = 0; i < rows.Length; i++)
                    {
                        DataRow dr = rows[i];
                       
                        // Populate a CustomerDC object from the DataRow.
                        CustomerDC dc = DAL.CreateItemFromRow<CustomerDC>(dr);

                        // Insert or update the customer in the database.
                        Customer_ups(dc);

                        // Update the CustomerID in the DataRow after insertion.
                        dr["CustomerID"] = dc.CustomerID;

                    }

                    // Process modified rows.
                    dvrs = DataViewRowState.ModifiedCurrent;
                    rows = ds.Tables[0].Select("", "", dvrs);

                    // Iterate through modified rows
                    for (int i = 0; i < rows.Length; i++)
                    {
                        DataRow dr = rows[i];
                        
                        // Populate a CustomerDC object from the DataRow.
                        CustomerDC dc = DAL.CreateItemFromRow<CustomerDC>(dr);

                        // Update the customer in the database.
                        Customer_ups(dc);
                    }

                    // Process deleted rows.
                    dvrs = DataViewRowState.Deleted;
                    rows = ds.Tables[0].Select("", "", dvrs);

                    for (int i = 0; i < rows.Length; i++)
                    {
                        DataRow dr = rows[i];

                        // Retrieve the original CustomerID from the DataRow
                        int customerID = (int)dr["CustomerID", DataRowVersion.Original];
                        
                        // Delete Customer FK dependencies in the CustomerProduct table.                                                                       
                        ExecuteNonQuery("[dbo].[DeleteCustomerProduct]", CreateParameter("@CustomerID", SqlDbType.Int, customerID));

                        // Delete the customer record.
                        ExecuteNonQuery("[dbo].[DeleteCustomer]", CreateParameter("@CustomerID", SqlDbType.Int, customerID));
                    }

                    // Accept changes in the DataSet after all operations are complete.
                    ds.AcceptChanges();
                }
                catch (Exception ex)
                {
                    //Bubble up the error message to the calling method.
                    string msg = "Error from UpdateCustomer: " + ex.Message;
                    throw new Exception(msg);
                }
            }
            catch (Exception ex)
            {
                //throw;
            }
        }

        /// <summary>
        /// Insert or updates a customer record in the database.
        /// </summary>
        /// <param name="dc"> The CustomerDC object containing customer data. </param>
        public void Customer_ups(CustomerDC dc)
        {
            try
            {
                SqlCommand cmd = null;
                ExecuteNonQuery(ref cmd, "Customer_ups",
                   CreateParameter("@CUSTNAME", SqlDbType.Char, dc.CUSTNAME),
                   CreateParameter("@CustomerID", SqlDbType.Int, dc.CustomerID, ParameterDirection.InputOutput),
                   CreateParameter("@CompDB", SqlDbType.Char, dc.CompDB),
                   CreateParameter("@CUSTNMBR", SqlDbType.Char, dc.CUSTNMBR),
                   CreateParameter("@CUSTCLAS", SqlDbType.Char, dc.CUSTCLAS),
                   CreateParameter("@CNTCPRSN", SqlDbType.Char, dc.CNTCPRSN),
                   CreateParameter("@ADRSCODE", SqlDbType.Char, dc.ADRSCODE),
                   CreateParameter("@SHIPMTHD", SqlDbType.Char, dc.SHIPMTHD),
                   CreateParameter("@ADDRESS1", SqlDbType.Char, dc.ADDRESS1),
                   CreateParameter("@ADDRESS2", SqlDbType.Char, dc.ADDRESS2),
                   CreateParameter("@ADDRESS3", SqlDbType.Char, dc.ADDRESS3),
                   CreateParameter("@COUNTRY", SqlDbType.Char, dc.COUNTRY),
                   CreateParameter("@CITY", SqlDbType.Char, dc.CITY),
                   CreateParameter("@STATE", SqlDbType.Char, dc.STATE),
                   CreateParameter("@ZIP", SqlDbType.Char, dc.ZIP),
                   CreateParameter("@PHONE1", SqlDbType.Char, dc.PHONE1),
                   CreateParameter("@PHONE2", SqlDbType.Char, dc.PHONE2),
                   CreateParameter("@PHONE3", SqlDbType.Char, dc.PHONE3),
                   CreateParameter("@FAX", SqlDbType.Char, dc.FAX),
                   CreateParameter("@PYMTRMID", SqlDbType.Char, dc.PYMTRMID),
                   CreateParameter("@last_updated_by", SqlDbType.VarChar, dc.last_updated_by, ParameterDirection.InputOutput),
                   CreateParameter("@last_updated_on", SqlDbType.DateTime2, dc.last_updated_on, ParameterDirection.InputOutput));


                dc.CustomerID = (int)cmd.Parameters["@CustomerID"].Value;
                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }


        /// <summary>
        /// Checks if a customer has associated products in the database.
        /// </summary>
        /// <param name="customerID"> The ID of the customer to check. </param>
        /// <returns> True if the customer has associated products; otherwise, false. </returns>
        /// <exception cref="Exception"></exception>
        public bool CustomerHasProducts(int customerID)
        {
            try
            {
                // Execute stored procedure to check for associated products and get results.
                DataTable dt = ExecuteDataSet("dbo.SelectCustomerProduct", CreateParameter("@CustomerID", SqlDbType.Int, customerID)).Tables[0];

                // Return true if there are associated products.
                return (dt.Rows.Count > 0);
            }
            catch (Exception ex) 
            { 
                // Return false and bubble up the error message.
                return false;
                string msg = "Error from UpdateApplication: " + ex.Message;
                throw new Exception(msg);
            }             
        }
    }
}
