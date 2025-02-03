using DataService;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MouldSpecification
{
    class FixedCostDAL : DataAccessBase
    {
        public DataSet SelectFixedCost()
        {
            DataSet ds = ExecuteDataSet("dbo.SelectInjectionMouldFixedCost");
            return ds;
        }

        public void UpdateFixedCost(DataSet ds)
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
                    FixedCostDC dc = DAL.CreateItemFromRow<FixedCostDC>(dr);  //populate  dataclass                   
                    AddInjectionMouldFixedCost(dc);

                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    FixedCostDC dc = DAL.CreateItemFromRow<FixedCostDC>(dr);  //populate  dataclass                   
                    UpdateInjectionMouldFixedCost(dc);
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    if (dr["FixedCostID", DataRowVersion.Original] != null)
                    {
                        FixedCostDC dc = new FixedCostDC();
                        dc.FixedCostID = Convert.ToInt32(dr["FixedCostID", DataRowVersion.Original].ToString());
                        DeleteInjectionMouldFixedCost(dc);
                    }
                }
                ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
        }

        public static void AddInjectionMouldFixedCost(FixedCostDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("AddInjectionMouldFixedCost", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@FixedCostID", SqlDbType.Int, 4);
                cmd.Parameters["@FixedCostID"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@FixedCostID"].Value = dc.FixedCostID;
                cmd.Parameters.Add("@FixedCostDesc", SqlDbType.VarChar, 30);
                cmd.Parameters["@FixedCostDesc"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@FixedCostDesc"].Value = dc.FixedCostDesc;
                cmd.Parameters.Add("@FixedCost", SqlDbType.Decimal, 9);
                cmd.Parameters["@FixedCost"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@FixedCost"].Value = dc.FixedCost;
                cmd.Parameters.Add("@Comment", SqlDbType.VarChar, 100);
                cmd.Parameters["@Comment"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Comment"].Value = dc.Comment;
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;

                cmd.ExecuteNonQuery();

                dc.FixedCostID = (int)cmd.Parameters["@FixedCostID"].Value;
                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        public static void UpdateInjectionMouldFixedCost(FixedCostDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("UpdateInjectionMouldFixedCost", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@FixedCostID", SqlDbType.Int, 4);
                cmd.Parameters["@FixedCostID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@FixedCostID"].Value = dc.FixedCostID;
                cmd.Parameters.Add("@FixedCostDesc", SqlDbType.VarChar, 30);
                cmd.Parameters["@FixedCostDesc"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@FixedCostDesc"].Value = dc.FixedCostDesc;
                cmd.Parameters.Add("@FixedCost", SqlDbType.Decimal, 9);
                cmd.Parameters["@FixedCost"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@FixedCost"].Value = dc.FixedCost;
                cmd.Parameters.Add("@Comment", SqlDbType.VarChar, 100);
                cmd.Parameters["@Comment"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Comment"].Value = dc.Comment;
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

        public static void DeleteInjectionMouldFixedCost(FixedCostDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("DeleteInjectionMouldFixedCost", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@FixedCostID", SqlDbType.Int, 4);
                cmd.Parameters["@FixedCostID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@FixedCostID"].Value = dc.FixedCostID;

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
