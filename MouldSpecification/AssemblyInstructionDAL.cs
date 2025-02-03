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
    /// Data Acess Layer class for handling Assembly Instructions.
    /// </summary>
    internal class AssemblyInstructionDAL : DataAccessBase
    {
        /// <summary>
        /// Updates the Assembly Instructions in the database based on the changes in a DataSet's pivot table.
        /// Handles added, modified, and deleted rows.
        /// </summary>
        /// <param name="ds"> The DataSet containing the pivot table. </param>
        /// <param name="tableName"> The name of the table within the DataSet to process. </param>
        public void UpdateFromPivotTable(DataSet ds, string tableName)
        {
            try
            {
                // Focus on newly added rows
                DataViewRowState dvrs = DataViewRowState.Added;

                // Retrieve rows in 'Added' state
                DataRow[] rows = ds.Tables[tableName].Select("", "", dvrs);

                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];

                    // Check and add new row for AssemblyInstructionID1
                    if (dr["AssemblyInstructionID1"] == DBNull.Value)
                    {
                        if (dr["ItemID1"] != DBNull.Value)
                        {
                            AssemblyInstructionDC dc = new AssemblyInstructionDC();
                            dc.ItemID = (int)dr["ItemID1"];
                            dc.InstructionNo = (int)dr["InstructionNo1"];
                            dc.AssemblyInstruction = dr["AssemblyInstruction1"].ToString();
                            dc.AssemblyImageFilePath = dr["AssemblyImageFilePath1"].ToString();

                            // Add ti the database
                            AddAssemblyInstruction(dc);
                        }
                        // Check and add new row for AssemblyInstructionID2
                        if (dr["AssemblyInstructionID2"] == DBNull.Value)
                        {
                            if (dr["ItemID2"] != DBNull.Value)
                            {
                                AssemblyInstructionDC dc = new AssemblyInstructionDC();
                                dc.ItemID = (int)dr["ItemID2"];
                                dc.InstructionNo = (int)dr["InstructionNo2"];
                                dc.AssemblyInstruction = dr["AssemblyInstruction2"].ToString();
                                dc.AssemblyImageFilePath = dr["AssemblyImageFilePath2"].ToString();

                                // Add to the database
                                AddAssemblyInstruction(dc);
                            }
                        }
                    }
                }
                

                // Focus on rows in 'Modified' state
                dvrs = DataViewRowState.ModifiedCurrent;

                // Retrieve rows in 'Modified' state
                rows = ds.Tables[tableName].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];

                    // Check and update existing row for AssemblyInstructionID1
                    if (dr["AssemblyInstructionID1"] != DBNull.Value)
                    {
                        if (dr["ItemID1"] != DBNull.Value)
                        {
                            AssemblyInstructionDC dc = new AssemblyInstructionDC();
                            dc.AssemblyInstructionID = (int)dr["AssemblyInstructionID1"];
                            dc.ItemID = (int)dr["ItemID1"];
                            dc.InstructionNo = (int)dr["InstructionNo1"];
                            dc.AssemblyInstruction = dr["AssemblyInstruction1"].ToString();
                            dc.AssemblyImageFilePath = dr["AssemblyImageFilePath1"].ToString();

                            // Update the database
                            UpdateAssemblyInstruction(dc);
                        }
                    }

                    // Check and update existing row for AssemblyInstructionID2
                    if (dr["AssemblyInstructionID2"] != DBNull.Value)
                    {
                        if (dr["ItemID2"] != DBNull.Value)
                        {
                            AssemblyInstructionDC dc = new AssemblyInstructionDC();
                            dc.AssemblyInstructionID = (int)dr["AssemblyInstructionID2"];
                            dc.ItemID = (int)dr["ItemID2"];
                            dc.InstructionNo = (int)dr["InstructionNo2"];
                            dc.AssemblyInstruction = dr["AssemblyInstruction2"].ToString();
                            dc.AssemblyImageFilePath = dr["AssemblyImageFilePath2"].ToString();

                            // Update the database
                            UpdateAssemblyInstruction(dc);
                        }                       
                    }
                }

                // Focus on rows in 'Deleted' state      
                dvrs = DataViewRowState.Deleted;

                // Retrieve rows in 'Deleted' state
                rows = ds.Tables[tableName].Select("", "", dvrs);

                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];

                    // Check and delete row for AssemblyInstructionID1
                    if (dr["AssemblyInstructionID1", DataRowVersion.Original] != null)
                    {
                        AssemblyInstructionDC dc = new AssemblyInstructionDC();
                        dc.AssemblyInstructionID = Convert.ToInt32(dr["AssemblyInstructionID1", DataRowVersion.Original].ToString());

                        // Delete from the database
                        DeleteAssemblyInstruction(dc);
                    }

                    // Check and delete row for AssemblyInstructionID2
                    if (dr["AssemblyInstructionID2", DataRowVersion.Original] != null)
                    {
                        AssemblyInstructionDC dc = new AssemblyInstructionDC();
                        dc.AssemblyInstructionID = Convert.ToInt32(dr["AssemblyInstructionID2", DataRowVersion.Original].ToString());

                        // Delete from the database
                        DeleteAssemblyInstruction(dc);
                    }
                }

                
            }
            catch (Exception ex) { }

        }

        /// <summary>
        /// Updates the Assembly Instructions in the database based on changes in a DataSet.
        /// Handles added, modified, and deleted rows.
        /// </summary>
        /// <param name="ds"> The Dataset containing the table with assembly instructions. </param>
        /// <param name="tableName"> The name of the table within the DataSet to process. </param>
        public void UpdateAssemblyInstruction(DataSet ds, string tableName)
        {
            try
            {
                // Focus on rows in the 'Added' state.
                DataViewRowState dvrs = DataViewRowState.Added;

                // Retrieve rows in 'Added' state
                DataRow[] rows = ds.Tables[tableName].Select("", "", dvrs);

                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];

                    // Create a data transfer object from the DataRow using DAL utility method
                    AssemblyInstructionDC dc = DAL.CreateItemFromRow<AssemblyInstructionDC>(dr);
                    
                    // Add the new assembly instruction to the database
                    AddAssemblyInstruction(dc);

                }

                // Focus on rows in the 'Modified' state
                dvrs = DataViewRowState.ModifiedCurrent;

                // Retrieve rows in 'Modified' state
                rows = ds.Tables[tableName].Select("", "", dvrs);

                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];

                    // Create a data transfer object from the DataRow using DAL utility  method
                    AssemblyInstructionDC dc = DAL.CreateItemFromRow<AssemblyInstructionDC>(dr);
                    
                    // Update the existing assembly instruction in the database
                    UpdateAssemblyInstruction(dc);
                }
                
                // Focus on rows in the 'Deleted' state
                dvrs = DataViewRowState.Deleted;

                // Retrieve rows in 'Deleted' state
                rows = ds.Tables[tableName].Select("", "", dvrs);


                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];

                    // Check if the AssemblyInstructionID exists in the original version of the deleted row
                    if (dr["AssemblyInstructionID", DataRowVersion.Original] != null)
                    {
                        AssemblyInstructionDC dc = new AssemblyInstructionDC();
                        dc.AssemblyInstructionID = Convert.ToInt32(dr["AssemblyInstructionID", DataRowVersion.Original].ToString());

                        // Deleted the assembly instruction from the database
                        DeleteAssemblyInstruction(dc);
                    }
                }
            }
            catch (Exception ex)
            {
                // Show the exception message in a message box
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Adds a new assembly instruction to the database.
        /// </summary>
        /// <param name="dc"> The <see cref="AssemblyInstructionDC"/> object containing the details of the assembly instruction to add. </param>
        public static void AddAssemblyInstruction(AssemblyInstructionDC dc)
        {
            try
            {
                // Declare the command object 
                System.Data.SqlClient.SqlCommand cmd = null;

                // Establish a connection to the database
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();

                // Intialize the command object for the "AddAssemblyInstruction" stored procedure
                cmd = new System.Data.SqlClient.SqlCommand("AddAssemblyInstruction", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // Assembly Instruction ID - input/output parameter
                cmd.Parameters.Add("@AssemblyInstructionID", SqlDbType.Int, 4);
                cmd.Parameters["@AssemblyInstructionID"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@AssemblyInstructionID"].Value = dc.AssemblyInstructionID;

                // Item ID - input parameter
                cmd.Parameters.Add("@ItemID", SqlDbType.Int, 4);
                cmd.Parameters["@ItemID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ItemID"].Value = dc.ItemID;

                // Instruction Number - input parameter
                cmd.Parameters.Add("@InstructionNo", SqlDbType.Int, 4);
                cmd.Parameters["@InstructionNo"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@InstructionNo"].Value = dc.InstructionNo;

                // Assembly Instruction text - input parameter
                cmd.Parameters.Add("@AssemblyInstruction", SqlDbType.VarChar, 300);
                cmd.Parameters["@AssemblyInstruction"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@AssemblyInstruction"].Value = dc.AssemblyInstruction;

                // Assembly Image File Patch - input parameter
                cmd.Parameters.Add("@AssemblyImageFilePath", SqlDbType.VarChar, 200);
                cmd.Parameters["@AssemblyImageFilePath"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@AssemblyImageFilePath"].Value = dc.AssemblyImageFilePath;

                // Execute the stored procedure
                cmd.ExecuteNonQuery();

                // Update the AssemblyInstructionID in the data object with the value returned by the stored procedure
                dc.AssemblyInstructionID = (int)cmd.Parameters["@AssemblyInstructionID"].Value;

                // Close the database connection
                connection.Close();
            }
            catch (Exception excp)
            {
                // Display an error message in case of an exception
                MessageBox.Show(excp.Message);
            }
        }

        /// <summary>
        /// Updates an existing assembly instruction in the database
        /// </summary>
        /// <param name="dc"> The <see cref="AssemblyInstructionDC"/> object containing the updated details of the assembly instruction. </param>
        public static void UpdateAssemblyInstruction(AssemblyInstructionDC dc)
        {
            try
            {
                // Declare the command object
                System.Data.SqlClient.SqlCommand cmd = null;
                 
                // Establish a connection to the database.
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();

                // Initialize the command object for the "UpdateAssemblyInstruction" stored procedure
                cmd = new System.Data.SqlClient.SqlCommand("UpdateAssemblyInstruction", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // Assembly Instruction ID - input parameter
                cmd.Parameters.Add("@AssemblyInstructionID", SqlDbType.Int, 4);
                cmd.Parameters["@AssemblyInstructionID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@AssemblyInstructionID"].Value = dc.AssemblyInstructionID;

                // Item ID - input paramter
                cmd.Parameters.Add("@ItemID", SqlDbType.Int, 4);
                cmd.Parameters["@ItemID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ItemID"].Value = dc.ItemID;

                // Instruction Number - input parameter
                cmd.Parameters.Add("@InstructionNo", SqlDbType.Int, 4);
                cmd.Parameters["@InstructionNo"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@InstructionNo"].Value = dc.InstructionNo;

                // Assembly Instruction text - input parameter 
                cmd.Parameters.Add("@AssemblyInstruction", SqlDbType.VarChar, 300);
                cmd.Parameters["@AssemblyInstruction"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@AssemblyInstruction"].Value = dc.AssemblyInstruction;

                // Assembly Image File Path - input parameter
                cmd.Parameters.Add("@AssemblyImageFilePath", SqlDbType.VarChar, 200);
                cmd.Parameters["@AssemblyImageFilePath"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@AssemblyImageFilePath"].Value = dc.AssemblyImageFilePath;

                // Execute the stored procedure
                cmd.ExecuteNonQuery();

                // Close the database connection
                connection.Close();
            }
            catch (Exception excp)
            {
                // Display an error message in case of an exception
                MessageBox.Show(excp.Message);
            }
        }

        /// <summary>
        /// Delete an existing assembly instruction from the database.
        /// </summary>
        /// <param name="dc"> The <see cref="AssemblyInstructionDC"/> object containing the details of the assembly instruction to delete. </param>
        public static void DeleteAssemblyInstruction(AssemblyInstructionDC dc)
        {
            try
            {
                // Declare the command object
                System.Data.SqlClient.SqlCommand cmd = null;

                // Establish a connection to the database
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();

                // Initialize the command object for the "DeleteAssemblyInstruction" stored procedure
                cmd = new System.Data.SqlClient.SqlCommand("DeleteAssemblyInstruction", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // Assembly Instruction ID - input parameter 
                cmd.Parameters.Add("@AssemblyInstructionID", SqlDbType.Int, 4);
                cmd.Parameters["@AssemblyInstructionID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@AssemblyInstructionID"].Value = dc.AssemblyInstructionID;

                // Execute the stored procedure 
                cmd.ExecuteNonQuery();

                // Close the database connection
                connection.Close();
            }
            catch (Exception excp)
            {
                // Display an error message in case of an exception
                MessageBox.Show(excp.Message);
            }
        }

    }
}
