using DataService;
using DataService;
using System;
using System.Collections.Generic;
using System.Data;
using DataService;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MouldSpecification
{
    /// <summary>
    /// Handles the update operations for attached documents in a dataset.
    /// </summary>
    internal class AttachedDocDAL : DataAccessBase
    {
        /// <summary>
        /// Updates the attached documents by processing added, modified, and deleted rows.
        /// </summary>
        /// <param name="ds"> The dataset containing the data to be updated. </param>
        /// <param name="tableName"> The name of the table within the dataset to be processed. </param>
        public void UpdateAttachedDoc(DataSet ds, string tableName)
        {
            try
            {
                // Process rows that have been added.
                DataViewRowState dvrs = DataViewRowState.Added;
                DataRow[] rows = ds.Tables[tableName].Select("", "", dvrs);

                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];

                    // Create a new AttachedDocDC object from the added DataRow.
                    AttachedDocDC dc = DAL.CreateItemFromRow<AttachedDocDC>(dr);
                    
                    // Add the attached document to the database.
                    AddAttachedDoc(dc);
                }


                // Process rows that have been modified.
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[tableName].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];

                    // Create an AttachedDocDC object from the modified DataRow.
                    AttachedDocDC dc = DAL.CreateItemFromRow<AttachedDocDC>(dr);
                    
                    // Update the attached document in the database.
                    UpdateAttachedDoc(dc);
                }
              
                // Process rows that have been marked for deletion.
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[tableName].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];

                    // Check if the primary key for the deleted row is available.
                    if (dr["AttachedDocID", DataRowVersion.Original] != null)
                    {
                        // Create a new Attached
                        AttachedDocDC dc = new AttachedDocDC();
                        dc.AttachedDocID = Convert.ToInt32(dr["AttachedDocID", DataRowVersion.Original].ToString());

                        // Delete the attached document from the database.
                        DeleteAttachedDoc(dc);
                    }
                }

                // Accept changes to the dataset to confirm all updates.
                ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                // Display the error message in case of an exception.
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Adds a new attached document record to the database.
        /// </summary>
        /// <param name="dc"> An instance of <see cref="AttachedDocDC"/> containing the data for the new document. </param>
        public static void AddAttachedDoc(AttachedDocDC dc)
        {
            try
            {
                // Intialize the SQL command and database connection.
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();

                // Create a new SQL command for the "AddAttachedDoc" stored procedure.
                cmd = new System.Data.SqlClient.SqlCommand("AddAttachedDoc", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // The ID of the related item, set as an input parameter.
                cmd.Parameters.Add("@AttachedDocID", SqlDbType.Int, 4);
                cmd.Parameters["@AttachedDocID"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@AttachedDocID"].Value = dc.AttachedDocID;
                 
                // The file path of the document, set as an input-output parameter.
                cmd.Parameters.Add("@ItemID", SqlDbType.Int, 4);
                cmd.Parameters["@ItemID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ItemID"].Value = dc.ItemID;

                // The file path of the document, set as an input parameter.
                cmd.Parameters.Add("@DocFilepath", SqlDbType.VarChar, 100);
                cmd.Parameters["@DocFilepath"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@DocFilepath"].Value = dc.DocFilepath;

                // The user who last updated the document, set as an input parameter.
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;

                // The data and time the document was last update, set as an input-output parameter. 
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;

                // Execute the stored procedure.
                cmd.ExecuteNonQuery();

                // Retrieve the updated values of output parameters from the database.
                dc.AttachedDocID = (int)cmd.Parameters["@AttachedDocID"].Value;
                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;

                // Close the database connection.
                connection.Close();
            }
            catch (Exception excp)
            {
                // Display an error message in case of an exception.
                MessageBox.Show(excp.Message);
            }
        }

        /// <summary>
        /// Updates an existing attached document record in the database.
        /// </summary>
        /// <param name="dc"> An instance of <see cref="AttachedDocDC"/> containing the updated data for the document. </param>
        public static void UpdateAttachedDoc(AttachedDocDC dc)
        {
            try
            {
                // Initialize the SQL command and database connection.
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();

                // Create a new SQL command for the "UpdateAttachedDoc" stored procedure.
                cmd = new System.Data.SqlClient.SqlCommand("UpdateAttachedDoc", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // The ID of the attached document, used to identify the record to update.
                cmd.Parameters.Add("@AttachedDocID", SqlDbType.Int, 4);
                cmd.Parameters["@AttachedDocID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@AttachedDocID"].Value = dc.AttachedDocID;

                // The ID of the related item, set as an input parameter.
                cmd.Parameters.Add("@ItemID", SqlDbType.Int, 4);
                cmd.Parameters["@ItemID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ItemID"].Value = dc.ItemID;

                // The file path of the document, set as input parameter.
                cmd.Parameters.Add("@DocFilepath", SqlDbType.VarChar, 100);
                cmd.Parameters["@DocFilepath"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@DocFilepath"].Value = dc.DocFilepath;

                // The user who last updated the document, set as an input-output parameter. 
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;

                // The date and time the document was last updated, set as an input-output parameter.
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;

                // Execute the stored procedure.
                cmd.ExecuteNonQuery();

                // Retrieve the updated values of output parameter from the database.
                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;

                // Close the database connection.
                connection.Close();
            }
            catch (Exception excp)
            {
                // Display an error message in case of an exception
                MessageBox.Show(excp.Message);
            }
        }

        /// <summary>
        /// Deleted an attached document record from the database.
        /// </summary>
        /// <param name="dc"> An instance of <see cref="AttachedDocDC"/> containing the ID of the document to be deleted. </param>
        public static void DeleteAttachedDoc(AttachedDocDC dc)
        {
            try
            {
                // Initialize the SQL command and database connection.
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();

                // Create a new SQL command for the "DeleteAttachedDoc" stored procedure.
                cmd = new System.Data.SqlClient.SqlCommand("DeleteAttachedDoc", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // The ID of the attached document to delete.
                cmd.Parameters.Add("@AttachedDocID", SqlDbType.Int, 4);
                cmd.Parameters["@AttachedDocID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@AttachedDocID"].Value = dc.AttachedDocID;

                // Execute the stored procedure.
                cmd.ExecuteNonQuery();

                // Close the database connection.
                connection.Close();
            }
            catch (Exception excp)
            {
                // Display an error message in case of an exception.
                MessageBox.Show(excp.Message);
            }
        }

    }
}
