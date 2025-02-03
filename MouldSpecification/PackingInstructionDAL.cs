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
    internal class PackingInstructionDAL : DataAccessBase
    {
        public void UpdatePackingInstruction(DataSet ds, string tableName)
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
                    PackingInstructionDC dc = DAL.CreateItemFromRow<PackingInstructionDC>(dr);  //populate  dataclass                   
                    AddPackingInstruction(dc);

                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[tableName].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    PackingInstructionDC dc = DAL.CreateItemFromRow<PackingInstructionDC>(dr);  //populate  dataclass                   
                    UpdatePackingInstruction(dc);
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[tableName].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    if (dr["PackingInstructionID", DataRowVersion.Original] != null)
                    {
                        PackingInstructionDC dc = new PackingInstructionDC();
                        dc.PackingInstructionID = Convert.ToInt32(dr["PackingInstructionID", DataRowVersion.Original].ToString());
                        DeletePackingInstruction(dc);
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

        public static void AddPackingInstruction(PackingInstructionDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("AddPackingInstruction", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@PackingInstructionID", SqlDbType.Int, 4);
                cmd.Parameters["@PackingInstructionID"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@PackingInstructionID"].Value = dc.PackingInstructionID;
                cmd.Parameters.Add("@ItemID", SqlDbType.Int, 4);
                cmd.Parameters["@ItemID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ItemID"].Value = dc.ItemID;
                cmd.Parameters.Add("@InstructionNo", SqlDbType.Int, 4);
                cmd.Parameters["@InstructionNo"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@InstructionNo"].Value = dc.InstructionNo;
                cmd.Parameters.Add("@PackingInstruction", SqlDbType.VarChar, 300);
                cmd.Parameters["@PackingInstruction"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PackingInstruction"].Value = dc.PackingInstruction;

                cmd.ExecuteNonQuery();

                dc.PackingInstructionID = (int)cmd.Parameters["@PackingInstructionID"].Value;
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        public static void UpdatePackingInstruction(PackingInstructionDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("UpdatePackingInstruction", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@PackingInstructionID", SqlDbType.Int, 4);
                cmd.Parameters["@PackingInstructionID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PackingInstructionID"].Value = dc.PackingInstructionID;
                cmd.Parameters.Add("@ItemID", SqlDbType.Int, 4);
                cmd.Parameters["@ItemID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ItemID"].Value = dc.ItemID;
                cmd.Parameters.Add("@InstructionNo", SqlDbType.Int, 4);
                cmd.Parameters["@InstructionNo"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@InstructionNo"].Value = dc.InstructionNo;
                cmd.Parameters.Add("@PackingInstruction", SqlDbType.VarChar, 300);
                cmd.Parameters["@PackingInstruction"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PackingInstruction"].Value = dc.PackingInstruction;

                cmd.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        public static void DeletePackingInstruction(PackingInstructionDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("DeletePackingInstruction", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@PackingInstructionID", SqlDbType.Int, 4);
                cmd.Parameters["@PackingInstructionID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PackingInstructionID"].Value = dc.PackingInstructionID;

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
