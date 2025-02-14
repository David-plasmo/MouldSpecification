using DataService;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MouldSpecification
{
    class MasterBatchDAL : DataAccessBase
    {       
        public DataSet SelectMasterBatch()
        {
            try
            {
                DataSet ds = ExecuteDataSet("[dbo].[SelectMasterBatch]");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataSet SelectMBColour()
        {
            try
            {
                DataSet ds = ExecuteDataSet("[dbo].[SelectMBColour]");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public void UpdateMasterBatch(DataSet ds)
        {
            try
            {

                //Process new rows:-
                DataViewRowState dvrs = DataViewRowState.Added;
                DataRow[] rows = ds.Tables[0].Select("", "", dvrs);
                //MasterBatchDAL md = new MasterBatchDAL();

                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    MasterBatchDC dc = DAL.CreateItemFromRow<MasterBatchDC>(dr);  //populate  dataclass                   
                    AddMasterBatch(dc);

                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    MasterBatchDC dc = DAL.CreateItemFromRow<MasterBatchDC>(dr);  //populate  dataclass                   
                    UpdateMasterBatch(dc);
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    if (dr["MBID", DataRowVersion.Original] != null)
                    {
                        MasterBatchDC dc = new MasterBatchDC();
                        dc.MBID = Convert.ToInt32(dr["MBID", DataRowVersion.Original].ToString());
                        DeleteMasterBatch(dc);
                    }
                }
                ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void AddMasterBatch(MasterBatchDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("AddMasterBatch", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@MBID", SqlDbType.Int, 4);
                cmd.Parameters["@MBID"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@MBID"].Value = dc.MBID;
                cmd.Parameters.Add("@MBCode", SqlDbType.VarChar, 20);
                cmd.Parameters["@MBCode"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MBCode"].Value = dc.MBCode;
                cmd.Parameters.Add("@MBColour", SqlDbType.VarChar, 50);
                cmd.Parameters["@MBColour"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MBColour"].Value = dc.MBColour;
                cmd.Parameters.Add("@CostPerKg", SqlDbType.Decimal, 9);
                cmd.Parameters["@CostPerKg"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CostPerKg"].Value = dc.CostPerKg;
                cmd.Parameters.Add("@Comment", SqlDbType.VarChar, 100);
                cmd.Parameters["@Comment"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Comment"].Value = dc.Comment;
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;
                cmd.Parameters.Add("@Supplier", SqlDbType.VarChar, 100);
                cmd.Parameters["@Supplier"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Supplier"].Value = dc.Supplier;

                cmd.ExecuteNonQuery();

                dc.MBID = (int)cmd.Parameters["@MBID"].Value;
                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        public static void UpdateMasterBatch(MasterBatchDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("UpdateMasterBatch", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@MBID", SqlDbType.Int, 4);
                cmd.Parameters["@MBID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MBID"].Value = dc.MBID;
                cmd.Parameters.Add("@MBCode", SqlDbType.VarChar, 20);
                cmd.Parameters["@MBCode"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MBCode"].Value = dc.MBCode;
                cmd.Parameters.Add("@MBColour", SqlDbType.VarChar, 50);
                cmd.Parameters["@MBColour"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MBColour"].Value = dc.MBColour;
                cmd.Parameters.Add("@CostPerKg", SqlDbType.Decimal, 9);
                cmd.Parameters["@CostPerKg"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CostPerKg"].Value = dc.CostPerKg;
                cmd.Parameters.Add("@Comment", SqlDbType.VarChar, 100);
                cmd.Parameters["@Comment"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Comment"].Value = dc.Comment;
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;
                cmd.Parameters.Add("@Supplier", SqlDbType.VarChar, 100);
                cmd.Parameters["@Supplier"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Supplier"].Value = dc.Supplier;

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

        public static void DeleteMasterBatch(MasterBatchDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("DeleteMasterBatch", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@MBID", SqlDbType.Int, 4);
                cmd.Parameters["@MBID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MBID"].Value = dc.MBID;

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
