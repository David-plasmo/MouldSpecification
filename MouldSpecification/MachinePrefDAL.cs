using DataService;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MouldSpecification
{
    public class MachinePrefDAL : DataAccessBase
    {
        public DataSet GetMachineIndex()
        {
            try
            {
                return ExecuteDataSet("GetMachineIndex");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataSet SelectMachinePref(int? itemID, int? custID)
        {
            try
            {
                return ExecuteDataSet("SelectMachinePref",
                    CreateParameter("@ItemID", SqlDbType.Int, itemID),
                    CreateParameter("@CustomerID", SqlDbType.Int, custID));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataSet GetMachineABC()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                DataColumn dc = new DataColumn("Preference");
                dt.Columns.Add(dc);
                dt.Rows.Add("A");
                dt.Rows.Add("B");
                dt.Rows.Add("C");
                ds.Tables.Add(dt);
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void UpdateMachinePref(DataSet ds, string optionalTableName = "default")
        {
            try
            {

                //Process new rows:-
                DataViewRowState dvrs = DataViewRowState.Added;
                DataRow[] rows = (optionalTableName == "default")
                    ? ds.Tables[0].Select("", "", dvrs)
                    : ds.Tables[optionalTableName].Select("", "", dvrs);

                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    MachinePrefDC dc = DAL.CreateItemFromRow<MachinePrefDC>(dr);  //populate  dataclass                   
                    AddMachinePref(dc);

                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = (optionalTableName == "default")
                    ? ds.Tables[0].Select("", "", dvrs)
                    : ds.Tables[optionalTableName].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    MachinePrefDC dc = DAL.CreateItemFromRow<MachinePrefDC>(dr);  //populate  dataclass                   
                    UpdateMachinePref(dc);
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = (optionalTableName == "default")
                    ? ds.Tables[0].Select("", "", dvrs)
                    : ds.Tables[optionalTableName].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    if (dr["MachPrefID", DataRowVersion.Original] != null)
                    {
                        MachinePrefDC dc = new MachinePrefDC();
                        dc.MachPrefID = Convert.ToInt32(dr["MachPrefID", DataRowVersion.Original].ToString());
                        DeleteMachinePref(dc);
                    }
                }

                //ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void AddMachinePref(MachinePrefDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("AddMachinePref", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@MachPrefID", SqlDbType.Int, 4);
                cmd.Parameters["@MachPrefID"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@MachPrefID"].Value = dc.MachPrefID;
                cmd.Parameters.Add("@MachineID", SqlDbType.Int, 4);
                cmd.Parameters["@MachineID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MachineID"].Value = dc.MachineID;
                cmd.Parameters.Add("@ProgramNo", SqlDbType.Int, 4);
                cmd.Parameters["@ProgramNo"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ProgramNo"].Value = dc.ProgramNo;
                cmd.Parameters.Add("@ItemID", SqlDbType.Int, 4);
                cmd.Parameters["@ItemID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ItemID"].Value = dc.ItemID;
                cmd.Parameters.Add("@MachineABC", SqlDbType.Char, 1);
                cmd.Parameters["@MachineABC"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MachineABC"].Value = dc.MachineABC;
                cmd.Parameters.Add("@CycleTime", SqlDbType.Float, 8);
                cmd.Parameters["@CycleTime"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CycleTime"].Value = dc.CycleTime;
                cmd.Parameters.Add("@NoPartsPerHour", SqlDbType.Int, 4);
                cmd.Parameters["@NoPartsPerHour"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@NoPartsPerHour"].Value = dc.NoPartsPerHour;
                cmd.Parameters.Add("@IsPreferred", SqlDbType.Bit, 1);
                cmd.Parameters["@IsPreferred"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@IsPreferred"].Value = dc.IsPreferred;
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;

                cmd.ExecuteNonQuery();

                dc.MachPrefID = (int)cmd.Parameters["@MachPrefID"].Value;
                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        public static void UpdateMachinePref(MachinePrefDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("UpdateMachinePref", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@MachPrefID", SqlDbType.Int, 4);
                cmd.Parameters["@MachPrefID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MachPrefID"].Value = dc.MachPrefID;
                cmd.Parameters.Add("@MachineID", SqlDbType.Int, 4);
                cmd.Parameters["@MachineID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MachineID"].Value = dc.MachineID;
                cmd.Parameters.Add("@ProgramNo", SqlDbType.Int, 4);
                cmd.Parameters["@ProgramNo"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ProgramNo"].Value = dc.ProgramNo;
                cmd.Parameters.Add("@ItemID", SqlDbType.Int, 4);
                cmd.Parameters["@ItemID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ItemID"].Value = dc.ItemID;
                cmd.Parameters.Add("@MachineABC", SqlDbType.Char, 1);
                cmd.Parameters["@MachineABC"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MachineABC"].Value = dc.MachineABC;
                cmd.Parameters.Add("@CycleTime", SqlDbType.Float, 8);
                cmd.Parameters["@CycleTime"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CycleTime"].Value = dc.CycleTime;
                cmd.Parameters.Add("@NoPartsPerHour", SqlDbType.Int, 4);
                cmd.Parameters["@NoPartsPerHour"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@NoPartsPerHour"].Value = dc.NoPartsPerHour;
                cmd.Parameters.Add("@IsPreferred", SqlDbType.Bit, 1);
                cmd.Parameters["@IsPreferred"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@IsPreferred"].Value = dc.IsPreferred;
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;

                cmd.ExecuteNonQuery();

                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }
        public static void DeleteMachinePref(MachinePrefDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("DeleteMachinePref", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@MachPrefID", SqlDbType.Int, 4);
                cmd.Parameters["@MachPrefID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MachPrefID"].Value = dc.MachPrefID;

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



