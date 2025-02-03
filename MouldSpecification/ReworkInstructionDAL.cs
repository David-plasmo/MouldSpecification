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
    internal class ReworkInstructionDAL : DataAccessBase
    {
        public void UpdateReworkInstruction(DataSet ds, string tableName)
        {
            try
            {

                //Process new rows:-
                DataViewRowState dvrs = DataViewRowState.Added;
                DataRow[] rows = ds.Tables[tableName].Select("", "", dvrs);
                //MachineDAL md = new MachineDAL();

                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    ReworkInstructionDC dc = DAL.CreateItemFromRow<ReworkInstructionDC>(dr);  //populate  dataclass                   
                    AddReworkInstruction(dc);

                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[tableName].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    ReworkInstructionDC dc = DAL.CreateItemFromRow<ReworkInstructionDC>(dr);  //populate  dataclass                   
                    UpdateReworkInstruction(dc);
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[tableName].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    if (dr["ReworkInstructionID", DataRowVersion.Original] != null)
                    {
                        ReworkInstructionDC dc = new ReworkInstructionDC();
                        dc.ReworkInstructionID = Convert.ToInt32(dr["ReworkInstructionID", DataRowVersion.Original].ToString());
                        DeleteReworkInstruction(dc);
                    }
                }
                //ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
        }

        public static void AddReworkInstruction(ReworkInstructionDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("AddReworkInstruction", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@ReworkInstructionID", SqlDbType.Int, 4);
                cmd.Parameters["@ReworkInstructionID"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@ReworkInstructionID"].Value = dc.ReworkInstructionID;
                cmd.Parameters.Add("@ItemID", SqlDbType.Int, 4);
                cmd.Parameters["@ItemID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ItemID"].Value = dc.ItemID;
                cmd.Parameters.Add("@InstructionNo", SqlDbType.Int, 4);
                cmd.Parameters["@InstructionNo"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@InstructionNo"].Value = dc.InstructionNo;
                cmd.Parameters.Add("@ReworkInstruction", SqlDbType.VarChar, 300);
                cmd.Parameters["@ReworkInstruction"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ReworkInstruction"].Value = dc.ReworkInstruction;

                cmd.ExecuteNonQuery();

                dc.ReworkInstructionID = (int)cmd.Parameters["@ReworkInstructionID"].Value;
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        public static void UpdateReworkInstruction(ReworkInstructionDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("UpdateReworkInstruction", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@ReworkInstructionID", SqlDbType.Int, 4);
                cmd.Parameters["@ReworkInstructionID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ReworkInstructionID"].Value = dc.ReworkInstructionID;
                cmd.Parameters.Add("@ItemID", SqlDbType.Int, 4);
                cmd.Parameters["@ItemID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ItemID"].Value = dc.ItemID;
                cmd.Parameters.Add("@InstructionNo", SqlDbType.Int, 4);
                cmd.Parameters["@InstructionNo"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@InstructionNo"].Value = dc.InstructionNo;
                cmd.Parameters.Add("@ReworkInstruction", SqlDbType.VarChar, 300);
                cmd.Parameters["@ReworkInstruction"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ReworkInstruction"].Value = dc.ReworkInstruction;

                cmd.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        public static void DeleteReworkInstruction(ReworkInstructionDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("DeleteReworkInstruction", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@ReworkInstructionID", SqlDbType.Int, 4);
                cmd.Parameters["@ReworkInstructionID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ReworkInstructionID"].Value = dc.ReworkInstructionID;

                cmd.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }
    }
}
