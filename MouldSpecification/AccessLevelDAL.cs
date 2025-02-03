using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DataService;
using System.Windows.Forms;
using System.Data;
using System.Collections;
using System.Xml.Linq;

namespace PlasmoAdmin
{
    /// <summary>
    /// Data Access Layer for handling access level-related operations.
    /// </summary>
    internal class AccessLevelDAL :DataAccessBase
    {
        private string sql;

        /// <summary>
        /// Retrieves application access data.
        /// </summary>
        /// <returns>A DataSet containing application access data.</returns>
        public DataSet GetAppAccess()
        {
            return ExecuteDataSet("PlasmoAdmin.dbo.GetAppAccess");
        }

        /// <summary>
        /// Retrieves application access data.
        /// </summary>
        /// <param name="runSQL">A boolean indicating whether to execute SQL.</param>
        /// <returns>A DataSet containing application access data.</returns>
        public DataSet GetAppAccess(bool runSQL)
        {
            DataSet ds = null;
            try
            {
                SqlDataAdapter adapter;
                ds = new DataSet();
                if (sql != null)
                {
                    //create connection object
                    SqlConnection connection = new SqlConnection(GetConnectionString());
                    connection.Open();

                    //Adapter bind to query and connection object
                    adapter = new SqlDataAdapter(sql, connection);

                    //fill the dataset
                    adapter.Fill(ds);     
                }
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Retrieves permission levels.
        /// </summary>
        /// <returns>A DataSet containing permission levels.</returns>
        public DataSet GetPermissionLevel()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataColumn dc = dt.Columns.Add("PermissionLevel", typeof(byte));
            dt.Columns.Add("DisplayValue", typeof(string));
            dt.Rows.Add(1, "Read Only");
            dt.Rows.Add(2, "Update");
            ds.Tables.Add(dt);
            return ds;
        }

        /// <summary>
        /// Retrieves application names.
        /// </summary>
        /// <returns>A DataSet containing application names.</returns>
        public DataSet GetAppNames()
        {
            return ExecuteDataSet("dbo.GetAppNames");
        }

        /// <summary>
        /// Retrieves person data.
        /// </summary>
        /// <returns>A DataSet containing person data.</returns>
        public DataSet GetPerson()
        {
            return ExecuteDataSet("dbo.GetPerson");
        }

        /// <summary>
        /// Retrieves input form IDs.
        /// </summary>
        /// <returns>A DataSet containing input form IDs.</returns>
        public DataSet GetInputFormID()
        {
            return ExecuteDataSet("dbo.GetInputFormID");
        }

        /// <summary>
        /// Retrieves form names by application ID.
        /// </summary>
        /// <param name="appID">The application ID.</param>
        /// <returns>A DataSet containing form names.</returns>
        public DataSet GetFormNameByAppID(int appID)
        {
            return ExecuteDataSet("dbo.GetFormNameByAppID", CreateParameter("@AppID", SqlDbType.Int, appID));
        }

        /// <summary>
        /// Builds SQL query based on filter and sort parameters.
        /// </summary>
        /// <param name="fPersonID">Filter for person ID.</param>
        /// <param name="fAppID">Filter for application ID.</param>
        /// <param name="fFormID">Filter for form ID.</param>
        /// <param name="fPermission">Filter for permission level.</param>
        /// <param name="sPersonID">Sort for person ID.</param>
        /// <param name="sAppID">Sort for application ID.</param>
        /// <param name="sFormID">Sort for form ID.</param>
        /// <param name="sPermission">Sort for permission level.</param>
        public void BuildSql(string fPersonID, string fAppID, string fFormID, string fPermission,
                             string sPersonID, string sAppID, string sFormID, string sPermission)
        {

            string select =
                @"SELECT [LevelID]
                  ,al.[PersonID]
	              ,al.[AppID]
	              ,al.[FormID]
                  ,[PermissionLevel]
                  FROM [dbo].[AccessLevel] al 
                  INNER JOIN [dbo].[Person] p ON al.PersonID = p.PersonID 
                  INNER JOIN [dbo].[Application] ap ON al.AppID = ap.AppID
                  INNER JOIN [dbo].[InputForm] f ON al.FormID = f.FormID  ";
            
            string where = "";
            if (fPersonID.Length != 0)
                where = "WHERE " + fPersonID;
            if (fAppID.Length != 0)
                where += (where.Length != 0) ? " AND " + fAppID : "WHERE " + fAppID;
            if (fFormID.Length != 0)
                where += (where.Length != 0) ? " AND " + fFormID : "WHERE " + fFormID;
            if (fPermission.Length != 0)
                where += (where.Length != 0) ? " AND " + fPermission  : "WHERE " + fPermission;

            string sort = "";
            string sortDir = "";
            if (sPersonID.Length != 0)
            {
                sortDir = (sPersonID == "SortAZ") ? "ASC" : "DESC";
                sort = " ORDER BY p.Name " + sortDir;
            }
            if (sAppID.Length != 0)
            {
                sortDir = (sAppID == "SortAZ") ? "ASC" : "DESC";
                sort += (sort.Length != 0 )
                    ? ", ap.AppName " + sortDir
                    : " ORDER BY ap.AppName " + sortDir;
            }               
            if (sFormID.Length != 0)
            {
                sortDir = (sFormID == "SortAZ") ? "ASC" : "DESC";
                sort += (sort.Length != 0)
                    ? ", f.FormName " + sortDir
                    : " ORDER BY f.FormName " + sortDir;
            }                
            if (sPermission.Length != 0)
            {
                sortDir = (sPermission == "SortAZ") ? "ASC" : "DESC";
                sort += (sort.Length != 0)
                    ? ", al.PermissionLevel " + sortDir
                    : " ORDER BY f.FormName " + sortDir;
            }
                
            //insert table aliases into where string
            where = where.Replace("PersonID", "al.PersonID");
            where = where.Replace("AppID", "al.AppID");
            where = where.Replace("FormID", "al.FormID");

            //ORDER BY p.Name, ap.AppName, f.FormName"
            if (where.Length != 0)
                select += where;
            if (sort.Length != 0)
                select += sort;

            sql = select;

        }


        /// <summary>
        /// Updates the access level data based on changes in the provided DataSet.
        /// </summary>
        /// <param name="ds">The DataSet containing the updated access level data.</param>
        public void UpdateAccessLevel(DataSet ds)
        {
            try
            {
                
                // Instantiate AccessLevelDC class
                AccessLevelDC dal = new AccessLevelDC();
                // Set DataViewRowState to Added to filter new rows
                DataViewRowState dvrs = DataViewRowState.Added; 
                DataRow[] rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    // Get current new row
                    DataRow dr = rows[i];
                    // Create AccessLevelDC object from DataRow
                    AccessLevelDC dc = DAL.CreateItemFromRow<AccessLevelDC>(dr);
                    // Add new access level to the database
                    AddAccessLevel(dc);
                    // Update the LevelID of the corresponding row in the DataSet
                    dr["LevelID"] = dc.LevelID;

                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    AccessLevelDC dc = DAL.CreateItemFromRow<AccessLevelDC>(dr);  //populate JobRun dataclass                   
                    UpdateAccessLevel(dc);
                }

                // Process deleted rows
                // // Set DataViewRowState to Deleted to filter deleted rows
                dvrs = DataViewRowState.Deleted;
                // Get an array of deleted rows from the DataSet
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    // Get current deleted row
                    DataRow dr = rows[i];
                    // Get the original LevelID of the deleted row
                    int id = (int)dr["ItemVend_ID", DataRowVersion.Original];
                    // Instantiate AccessLevelDC object
                    AccessLevelDC dc = new AccessLevelDC();
                    // Set the LevelID of the AccessLevelDC object
                    dc.LevelID = id;
                    // Delete the access level from the database
                    DeleteAccessLevel(dc);
                }

                // Accept changes to the DataSet
                ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                // Display error message if an exception occurs
                MessageBox.Show(ex.Message);
                //throw;
            }
        }


        /// <summary>
        /// Adds a new access level to the database.
        /// </summary>
        /// <param name="dc">The AccessLevelDC object containing the access level information to be added.</param>
        public static void AddAccessLevel(AccessLevelDC dc)
        {
            try
            {
                // Initialize SqlCommand and SqlConnection objects
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                // Open the database connection
                connection.Open();
                // Initialize SqlCommand with the stored procedure name and connection
                cmd = new System.Data.SqlClient.SqlCommand("AddAccessLevel", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // Add parameters to the stored procedure
                cmd.Parameters.Add("@PermissionLevel", SqlDbType.TinyInt, 1);
                cmd.Parameters["@PermissionLevel"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PermissionLevel"].Value = dc.PermissionLevel;
                cmd.Parameters.Add("@AppID", SqlDbType.Int, 4);
                cmd.Parameters["@AppID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@AppID"].Value = dc.AppID;
                cmd.Parameters.Add("@PersonID", SqlDbType.Int, 4);
                cmd.Parameters["@PersonID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PersonID"].Value = dc.PersonID;
                cmd.Parameters.Add("@FormID", SqlDbType.Int, 4);
                cmd.Parameters["@FormID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@FormID"].Value = dc.FormID;
                cmd.Parameters.Add("@LevelID", SqlDbType.Int, 4);
                cmd.Parameters["@LevelID"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@LevelID"].Value = dc.LevelID;

                // Execute the stored procedure
                cmd.ExecuteNonQuery();
                // Retrieve the generated LevelID from the output parameter
                dc.LevelID = (int)cmd.Parameters["@LevelID"].Value;
                // Close the database connection
                connection.Close();
            }
            catch (Exception excp)
            {
                // Display error message if an exception occurs
                MessageBox.Show(excp.Message);
            }

            //Update

           
        }

        /// <summary>
        /// Updates an existing access level in the database.
        /// </summary>
        /// <param name="dc">The AccessLevelDC object containing the updated access level information.</param>
        public static void UpdateAccessLevel(AccessLevelDC dc)
        {
            try
            {
                // Initialize SqlCommand and SqlConnection objects
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                // Open the database connection
                connection.Open();
                // Initialize SqlCommand with the stored procedure name and connection
                cmd = new System.Data.SqlClient.SqlCommand("UpdateAccessLevel", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // Add parameters to the stored procedure
                cmd.Parameters.Add("@PermissionLevel", SqlDbType.TinyInt, 1);
                cmd.Parameters["@PermissionLevel"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PermissionLevel"].Value = dc.PermissionLevel;
                cmd.Parameters.Add("@AppID", SqlDbType.Int, 4);
                cmd.Parameters["@AppID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@AppID"].Value = dc.AppID;
                cmd.Parameters.Add("@PersonID", SqlDbType.Int, 4);
                cmd.Parameters["@PersonID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PersonID"].Value = dc.PersonID;
                cmd.Parameters.Add("@FormID", SqlDbType.Int, 4);
                cmd.Parameters["@FormID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@FormID"].Value = dc.FormID;
                cmd.Parameters.Add("@LevelID", SqlDbType.Int, 4);
                cmd.Parameters["@LevelID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@LevelID"].Value = dc.LevelID;

                // Execute the stored procedure to update the access level
                cmd.ExecuteNonQuery();

                // Close the database connection
                connection.Close();
            }
            catch (Exception excp)
            {
                // Display error message if an exception occurs
                MessageBox.Show(excp.Message);
            }
        }


        /// <summary>
        /// Deletes an access level from the database.
        /// </summary>
        /// <param name="dc">The AccessLevelDC object containing the access level ID to be deleted.</param>
        public static void DeleteAccessLevel(AccessLevelDC dc)
        {
            try
            {
                // Initialize SqlCommand and SqlConnection objects
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                // Open the database connection
                connection.Open();
                // Initialize SqlCommand with the stored procedure name and connection
                cmd = new System.Data.SqlClient.SqlCommand("DeleteAccessLevel", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // Add parameters to the stored procedure
                cmd.Parameters.Add("@LevelID", SqlDbType.Int, 4);
                cmd.Parameters["@LevelID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@LevelID"].Value = dc.LevelID;

                // Execute the stored procedure to delete the access level
                cmd.ExecuteNonQuery();
                // Close the database connection
                connection.Close();
            }
            catch (Exception excp)
            {
                // Display error message if an exception occurs
                MessageBox.Show(excp.Message);
            }
        }
    }
}
