using DataService;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MouldSpecification
{
    class MachineDAL : DataAccessBase
    {
        public DataSet SelectMachine(string machType)
        {
            try
            {
                DataSet ds = ExecuteDataSet("dbo.SelectMachine",
                    CreateParameter("@Type", SqlDbType.VarChar, machType));
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void UpdateMachine(DataSet ds)
        {
            try
            {

                //Process new rows:-
                DataViewRowState dvrs = DataViewRowState.Added;
                DataRow[] rows = ds.Tables[0].Select("", "", dvrs);
                //MachineDAL md = new MachineDAL();

                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    MachineDC dc = DAL.CreateItemFromRow<MachineDC>(dr);  //populate  dataclass                   
                    AddMachine(dc);

                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    MachineDC dc = DAL.CreateItemFromRow<MachineDC>(dr);  //populate  dataclass                   
                    UpdateMachine(dc);
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    if (dr["MachineID", DataRowVersion.Original] != null)
                    {
                        MachineDC dc = new MachineDC();
                        dc.MachineID = Convert.ToInt32(dr["MachineID", DataRowVersion.Original].ToString());
                        DeleteMachine(dc);
                    }
                }
                ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void AddMachine(MachineDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("AddMachine", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@MachineID", SqlDbType.Int, 4);
                cmd.Parameters["@MachineID"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@MachineID"].Value = dc.MachineID;
                cmd.Parameters.Add("@Machine", SqlDbType.VarChar, 50);
                cmd.Parameters["@Machine"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Machine"].Value = dc.Machine;
                cmd.Parameters.Add("@Capacity", SqlDbType.VarChar, 50);
                cmd.Parameters["@Capacity"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Capacity"].Value = dc.Capacity;
                cmd.Parameters.Add("@Type", SqlDbType.Char, 2);
                cmd.Parameters["@Type"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Type"].Value = dc.Type;
                cmd.Parameters.Add("@CostPerHour", SqlDbType.Decimal, 9);
                cmd.Parameters["@CostPerHour"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CostPerHour"].Value = dc.CostPerHour;
                cmd.Parameters.Add("@Comment", SqlDbType.VarChar, 200);
                cmd.Parameters["@Comment"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Comment"].Value = dc.Comment;
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;

                cmd.ExecuteNonQuery();

                dc.MachineID = (int)cmd.Parameters["@MachineID"].Value;
                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;
                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        public static void UpdateMachine(MachineDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("UpdateMachine", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@MachineID", SqlDbType.Int, 4);
                cmd.Parameters["@MachineID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MachineID"].Value = dc.MachineID;
                cmd.Parameters.Add("@Machine", SqlDbType.VarChar, 50);
                cmd.Parameters["@Machine"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Machine"].Value = dc.Machine;
                cmd.Parameters.Add("@Capacity", SqlDbType.VarChar, 50);
                cmd.Parameters["@Capacity"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Capacity"].Value = dc.Capacity;
                cmd.Parameters.Add("@Type", SqlDbType.Char, 2);
                cmd.Parameters["@Type"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Type"].Value = dc.Type;
                cmd.Parameters.Add("@CostPerHour", SqlDbType.Decimal, 9);
                cmd.Parameters["@CostPerHour"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CostPerHour"].Value = dc.CostPerHour;
                cmd.Parameters.Add("@Comment", SqlDbType.VarChar, 200);
                cmd.Parameters["@Comment"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Comment"].Value = dc.Comment;
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;

                cmd.ExecuteNonQuery();

                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;
                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        public static void DeleteMachine(MachineDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("DeleteMachine", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@MachineID", SqlDbType.Int, 4);
                cmd.Parameters["@MachineID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MachineID"].Value = dc.MachineID;

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
