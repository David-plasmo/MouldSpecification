using DataService;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MouldSpecification
{
    /// <summary>
    /// Data Access Layer (DAL) class for managing additive cost operations.
    /// Inherits from <see cref="DataAccessBase"/>
    /// </summary>
    class AdditiveCostDAL : DataAccessBase
    {
        /// <summary>
        /// Retrieves additive cost data from the database.
        /// Executes the stored procedure "SelectAdditive" and returns the result as a <see cref="DataSet"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="DataSet"/> containing the additive cost data, or <c> null </c> if an exception occurs.
        /// </returns>
        public DataSet SelectAdditiveCost()
        {
            try
            {
                // Execute the stored procedure to retrieve additive cost data.
                DataSet ds = ExecuteDataSet("SelectAdditive");
                return ds;
            }
            catch (Exception ex)
            {
                // Show an error message if an exception occurs.
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Updates the additive cost data in the database based on the changes in the provided <see cref="DataSet"/>.
        /// Handles added, modified, and deleted rows in the dataset.
        /// </summary>
        /// <param name="ds"> The <see cref="DataSet"/> containing changes to the additive cost data. </param>
        public void UpdateAdditive(DataSet ds)
        {
            try
            {
                // Process added rows in the dataset.
                DataViewRowState dvrs = DataViewRowState.Added;
                DataRow[] rows = ds.Tables[0].Select("", "", dvrs);

                for (int i = 0; i < rows.Length; i++)
                {
                    // Create a data contract object for each added row.
                    DataRow dr = rows[i];
                    AdditiveCostDC dc = DAL.CreateItemFromRow<AdditiveCostDC>(dr);
                    
                    // Add the new additive data to the database.
                    AddAdditive(dc);

                }

                // Process modified rows in the dataset.
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    // Create a data contract object for each modified row.
                    DataRow dr = rows[i];
                    AdditiveCostDC dc = DAL.CreateItemFromRow<AdditiveCostDC>(dr);      
                    
                    // Update the additive data in the database.
                    UpdateAdditive(dc);
                }
                
                // Process deleted rows in the dataset.
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    // Retrieve the original ID of the deleted row.
                    DataRow dr = rows[i];
                    if (dr["AdditiveID", DataRowVersion.Original] != null)
                    {
                        AdditiveCostDC dc = new AdditiveCostDC();
                        dc.AdditiveID = Convert.ToInt32(dr["AdditiveID", DataRowVersion.Original].ToString());

                        // Delete the additive data from the database.
                        DeleteAdditive(dc);
                    }
                }
                // Accept all changes in the dataset to mark the update as complete.
                ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                // Show an error message if an exception occurs.
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Adds a new additive record to the database by executing the "AddAdditive" stored procedure.
        /// Updates the AdditiveID, last_updated_by, and last_updated_on fields of the provided <see cref="AdditiveCostDC"/> object.
        /// </summary>
        /// <param name="dc">
        /// An instance of <see cref="AdditiveCostDC"/> containing the additive details to be inserted into the database.
        /// </param>
        public static void AddAdditive(AdditiveCostDC dc)
        {
            try
            {
                // Declare a SQL command and a connection to the database.
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());

                // Open the database connection.
                connection.Open();

                // Intialize the SQL command and set the stored procedure to execute.
                cmd = new System.Data.SqlClient.SqlCommand("AddAdditive", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // Add and configure parameters for the stored procedure.
                // Parameter for AdditiveID (input/output parameter).
                cmd.Parameters.Add("@AdditiveID", SqlDbType.Int, 4);
                cmd.Parameters["@AdditiveID"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@AdditiveID"].Value = dc.AdditiveID;

                // Parameter for Additive name.
                cmd.Parameters.Add("@Additive", SqlDbType.VarChar, 50);
                cmd.Parameters["@Additive"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Additive"].Value = dc.Additive;

                // Parameter for Additive Code.
                cmd.Parameters.Add("@AdditiveCode", SqlDbType.VarChar, 50);
                cmd.Parameters["@AdditiveCode"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@AdditiveCode"].Value = dc.AdditiveCode;

                // Parameter for Type
                cmd.Parameters.Add("@Type", SqlDbType.VarChar, 50);
                cmd.Parameters["@Type"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Type"].Value = dc.Type;

                // parameter for Description
                cmd.Parameters.Add("@Description", SqlDbType.VarChar, 100);
                cmd.Parameters["@Description"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Description"].Value = dc.Description;

                // Parameter for Cost per Kg.
                cmd.Parameters.Add("@CostPerKg", SqlDbType.Decimal, 9);
                cmd.Parameters["@CostPerKg"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CostPerKg"].Value = dc.CostPerKg;

                // Parameter for Comment.
                cmd.Parameters.Add("@Comment", SqlDbType.VarChar, 100);
                cmd.Parameters["@Comment"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Comment"].Value = dc.Comment;

                // Parameter for Supplier.
                cmd.Parameters.Add("@Supplier", SqlDbType.VarChar, 100);
                cmd.Parameters["@Supplier"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Supplier"].Value = dc.Supplier;

                // Parameter for last_updated_by (input/output parameter).
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;

                // Parameter for last_updated_on (input/ output parameter).
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;

                // Execute the stored procedure.
                cmd.ExecuteNonQuery();

                // Retrieve updated values for output parameters and assign them back to the data contract.
                dc.AdditiveID = (int)cmd.Parameters["@AdditiveID"].Value;
                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;

                // Close the database connection.
                connection.Close();
            }
            catch (Exception excp)
            {
                // Display an error message if an exception occurs.
                MessageBox.Show(excp.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dc"></param>
        public static void UpdateAdditive(AdditiveCostDC dc)
        {
            try
            {
                // Declare a SQL command and a connection to the database.
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());

                // Open the database connection.
                connection.Open();

                // Intialize the SQl command and set the stored procedure to execute.
                cmd = new System.Data.SqlClient.SqlCommand("UpdateAdditive", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // Add and configure parameters for the stored procedure.
                // parameter for AdditiveID (input parameter).
                cmd.Parameters.Add("@AdditiveID", SqlDbType.Int, 4);
                cmd.Parameters["@AdditiveID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@AdditiveID"].Value = dc.AdditiveID;

                // Parameter for Additive name.
                cmd.Parameters.Add("@Additive", SqlDbType.VarChar, 50);
                cmd.Parameters["@Additive"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Additive"].Value = dc.Additive;

                // Parameter for Additive code.
                cmd.Parameters.Add("@AdditiveCode", SqlDbType.VarChar, 50);
                cmd.Parameters["@AdditiveCode"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@AdditiveCode"].Value = dc.AdditiveCode;

                // Parameter for Type.
                cmd.Parameters.Add("@Type", SqlDbType.VarChar, 50);
                cmd.Parameters["@Type"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Type"].Value = dc.Type;

                // Parameter for Description.
                cmd.Parameters.Add("@Description", SqlDbType.VarChar, 100);
                cmd.Parameters["@Description"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Description"].Value = dc.Description;

                // Parameter for Cost per Kg.
                cmd.Parameters.Add("@CostPerKg", SqlDbType.Decimal, 9);
                cmd.Parameters["@CostPerKg"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CostPerKg"].Value = dc.CostPerKg;

                // Parameter for Comment.
                cmd.Parameters.Add("@Comment", SqlDbType.VarChar, 100);
                cmd.Parameters["@Comment"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Comment"].Value = dc.Comment;

                // Parameter for Supplier.
                cmd.Parameters.Add("@Supplier", SqlDbType.VarChar, 100);
                cmd.Parameters["@Supplier"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Supplier"].Value = dc.Supplier;

                // Parameter for last_updated_by (input/output parameter).
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;

                // Parameter for last_updated_on (input/output parameter).
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;

                // Execute the stored procedure.
                cmd.ExecuteNonQuery();

                // Retrieve updated values for output parameters and assign them back to the data contract.
                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;

                // Close the database connection.
                connection.Close();
            }
            catch (Exception excp)
            {
                // Display an error message if an exception occurs.
                MessageBox.Show(excp.Message);
            }
        }

        /// <summary>
        /// Deletes an additive record from the database by executing the "DeleteAdditive" stored procedure.
        /// </summary>
        /// <param name="dc">
        /// An instance of <see cref="AdditiveCostDC"/> containing the ID of the additive to delete.
        /// The <c>AdditiveID</c> property must be set before calling this method.
        /// </param>
        public static void DeleteAdditive(AdditiveCostDC dc)
        {
            try
            {
                // Declare a SQL command and a connection to the database.
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());

                // Open the database connection.
                connection.Open();

                // Initialize the SQL command and set the stored  procedure to execute.
                cmd = new System.Data.SqlClient.SqlCommand("DeleteAdditive", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // Add the parameter for AdditiveID (input parameter).
                cmd.Parameters.Add("@AdditiveID", SqlDbType.Int, 4);
                cmd.Parameters["@AdditiveID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@AdditiveID"].Value = dc.AdditiveID;

                // Close the stored procedure to delete the record.
                cmd.ExecuteNonQuery();

                // Close the database connection after execution.
                connection.Close();
            }
            catch (Exception excp)
            {
                // Display an error message if an exception occurs during execution.
                MessageBox.Show(excp.Message);
            }
        }
    }
}
