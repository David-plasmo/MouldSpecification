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
        /// Updates the Assembly Instructions in the database based on changes to the input DataSet, ds
        /// 
        /// Handles added, modified, and deleted rows.
        /// </summary>
        /// <param name="ds"> A DataSet containing a pivot table, modified by a DataGridView control. </param>
        /// <param name="tableName"> The name of the pivot table within the DataSet to process. </param>
        public void UpdateFromPivotTable(DataSet ds, string tableName)
        {
            try
            {
                // Process inserted rows
                DataViewRowState dvrs = DataViewRowState.Added;
                DataRow[] rows = ds.Tables[tableName].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];

                    // Check and add new row for AssemblyInstructionID1
                    if (dr["AssemblyInstructionID1"] != DBNull.Value && (int)dr["AssemblyInstructionID1"] <= 0)
                    {
                        if (dr["ItemID1"] != DBNull.Value && (int)dr["ItemID1"] > 0)
                        {
                            AssemblyInstructionDC dc = new AssemblyInstructionDC();
                            dc.ItemID = (int)dr["ItemID1"];
                            dc.InstructionNo = (int)dr["InstructionNo1"];
                            dc.AssemblyInstruction = dr["AssemblyInstruction1"].ToString();
                            dc.AssemblyImageFilePath = dr["AssemblyImageFilePath1"].ToString();

                            AssemblyInstruction_ups(dc);
                        }
                        // Check and add new row for AssemblyInstructionID2
                        if (dr["AssemblyInstructionID2"] != DBNull.Value && (int)dr["AssemblyInstructionID2"] <= 0)
                        {
                            if (dr["ItemID2"] != DBNull.Value && (int)dr["ItemID2"] > 0)
                            {
                                AssemblyInstructionDC dc = new AssemblyInstructionDC();
                                dc.ItemID = (int)dr["ItemID2"];
                                dc.InstructionNo = (int)dr["InstructionNo2"];
                                dc.AssemblyInstruction = dr["AssemblyInstruction2"].ToString();
                                dc.AssemblyImageFilePath = dr["AssemblyImageFilePath2"].ToString();

                                // Add to the database
                                AssemblyInstruction_ups(dc);
                            }
                        }
                    }
                }


                // Process modified rows
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[tableName].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];

                    // Check and update existing row for AssemblyInstructionID1
                    if (dr["AssemblyInstructionID1"] != DBNull.Value && (int)dr["AssemblyInstructionID1"] > 0)
                    {
                        if (dr["ItemID1"] != DBNull.Value && (int)dr["ItemID1"] > 0)
                        {
                            AssemblyInstructionDC dc = new AssemblyInstructionDC();
                            dc.AssemblyInstructionID = (int)dr["AssemblyInstructionID1"];
                            dc.ItemID = (int)dr["ItemID1"];
                            dc.InstructionNo = (int)dr["InstructionNo1"];
                            dc.AssemblyInstruction = dr["AssemblyInstruction1"].ToString();
                            dc.AssemblyImageFilePath = dr["AssemblyImageFilePath1"].ToString();

                            // Update the database
                            AssemblyInstruction_ups(dc);
                        }
                    }

                    // Check and update existing row for AssemblyInstructionID2
                    if (dr["AssemblyInstructionID2"] != DBNull.Value && (int)dr["AssemblyInstructionID2"] > 0)
                    {
                        if (dr["ItemID2"] != DBNull.Value && (int)dr["ItemID2"] > 0)
                        {
                            AssemblyInstructionDC dc = new AssemblyInstructionDC();
                            dc.AssemblyInstructionID = (int)dr["AssemblyInstructionID2"];
                            dc.ItemID = (int)dr["ItemID2"];
                            dc.InstructionNo = (int)dr["InstructionNo2"];
                            dc.AssemblyInstruction = dr["AssemblyInstruction2"].ToString();
                            dc.AssemblyImageFilePath = dr["AssemblyImageFilePath2"].ToString();

                            // Update the database
                            AssemblyInstruction_ups(dc);
                        }
                    }
                }

                // process deleted rows 
                dvrs = DataViewRowState.Deleted;                
                rows = ds.Tables[tableName].Select("", "", dvrs);

                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];

                    // Check and delete row for AssemblyInstructionID1
                    if (dr["AssemblyInstructionID1", DataRowVersion.Original] != null
                        && dr["AssemblyInstructionID1", DataRowVersion.Original].ToString().Length != 0)
                    {
                        AssemblyInstructionDC dc = new AssemblyInstructionDC();
                        dc.AssemblyInstructionID = Convert.ToInt32(dr["AssemblyInstructionID1", DataRowVersion.Original].ToString());

                        // Delete from the database
                        AssemblyInstruction_del(dc);
                    }

                    // Check and delete row for AssemblyInstructionID2
                    if (dr["AssemblyInstructionID2", DataRowVersion.Original] != null 
                        && dr["AssemblyInstructionID2", DataRowVersion.Original].ToString().Length != 0)
                    {
                        AssemblyInstructionDC dc = new AssemblyInstructionDC();
                        dc.AssemblyInstructionID = Convert.ToInt32(dr["AssemblyInstructionID2", DataRowVersion.Original].ToString());

                        // Delete from the database
                        AssemblyInstruction_del(dc);
                    }
                }


            }
            catch (Exception ex) { }

        }
    
        public void AssemblyInstruction_ups(AssemblyInstructionDC dc)
        {
            try
            {
                SqlCommand cmd = null;
                ExecuteNonQuery(ref cmd, "AssemblyInstruction_ups",
                   CreateParameter("@AssemblyInstructionID", SqlDbType.Int, dc.AssemblyInstructionID, ParameterDirection.InputOutput),
                   CreateParameter("@ItemID", SqlDbType.Int, dc.ItemID),
                   CreateParameter("@InstructionNo", SqlDbType.Int, dc.InstructionNo),
                   CreateParameter("@AssemblyInstruction", SqlDbType.VarChar, dc.AssemblyInstruction),
                   CreateParameter("@AssemblyImageFilePath", SqlDbType.VarChar, dc.AssemblyImageFilePath));


                dc.AssemblyInstructionID = (int)cmd.Parameters["@AssemblyInstructionID"].Value;
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        public void AssemblyInstruction_del(AssemblyInstructionDC dc)
        {
            try
            {
                SqlCommand cmd = null;
                ExecuteNonQuery(ref cmd, "AssemblyInstruction_del",
                   CreateParameter("@AssemblyInstructionID", SqlDbType.Int, dc.AssemblyInstructionID));


            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

    }
}
