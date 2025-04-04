using DataService;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MouldSpecification
{
    class MasterBatchCompDAL : DataAccessBase
    {
        public DataSet GetMB123()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                DataColumn dc = new DataColumn("MB123");
                dt.Columns.Add(dc);
                dt.Rows.Add("1");
                dt.Rows.Add("2");
                dt.Rows.Add("3");
                ds.Tables.Add(dt);
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }


        public DataSet SelectMasterBatchComp(int? itemID, int? custID)
        {
            try
            {
                return ExecuteDataSet("SelectMasterBatchComp",
                    CreateParameter("@ItemID", SqlDbType.Int, itemID),
                    CreateParameter("@CustomerID", SqlDbType.Int, custID));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataSet SelectMasterBatchByColour()
        {
            try
            {
                return ExecuteDataSet("SelectMasterBatchByColour");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataSet GetAdditiveCode()
        {
            try
            {
                DataSet ds = ExecuteDataSet("GetAdditiveCode");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void UpdateMasterBatchComp(DataSet ds, string tableName = "MasterBatchComp")
        {
            try
            {

                //Process new rows:-
                DataViewRowState dvrs = DataViewRowState.Added;
                //DataRow[] rows = (optionalTableName == "default")
                //    ? ds.Tables[0].Select("", "", dvrs)
                //    : ds.Tables[optionalTableName].Select("", "", dvrs);
                //MachineDAL md = new MachineDAL();
                DataRow[] rows = ds.Tables[tableName].Select("", "", dvrs);

                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    MasterBatchCompDC dc = DAL.CreateItemFromRow<MasterBatchCompDC>(dr);  //populate  dataclass
                    //Give null value for foreign key AdditiveID
                    //If blank was selected from dropdown its value will be zero
                    //if (dc.AdditiveID != null && dc.AdditiveID == 0)
                    //    dc.AdditiveID = null;
                    //AddMasterBatchComp(dc);
                    MasterBatchComp_ups(dc);
                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                //rows = (optionalTableName == "default")
                //    ? ds.Tables[0].Select("", "", dvrs)
                //    : ds.Tables[optionalTableName].Select("", "", dvrs);
                rows = ds.Tables[tableName].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    MasterBatchCompDC dc = DAL.CreateItemFromRow<MasterBatchCompDC>(dr);
                    //give null value for foreign key AdditiveID
                    //If blank was selected from dropdown its value will be zero
                    //if (dc.AdditiveID != null && dc.AdditiveID == 0)
                    //    dc.AdditiveID = null;
                    //UpdateMasterBatchComp(dc);
                    MasterBatchComp_ups(dc);
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                //rows = (optionalTableName == "default")
                //    ? ds.Tables[0].Select("", "", dvrs)
                //    : ds.Tables[optionalTableName].Select("", "", dvrs);
                rows = ds.Tables[tableName].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    if (dr["MBCompID", DataRowVersion.Original] != null)
                    {
                        MasterBatchCompDC dc = new MasterBatchCompDC();
                        dc.MBCompID = Convert.ToInt32(dr["MBCompID", DataRowVersion.Original].ToString());
                        //DeleteMasterBatchComp(dc);
                        MasterBatchComp_del(dc);
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

        public void MasterBatchComp_ups(MasterBatchCompDC dc)
        {
            try
            {
                SqlCommand cmd = null;
                ExecuteNonQuery(ref cmd, "MasterBatchComp_ups",
                   CreateParameter("@MBCompID", SqlDbType.Int, dc.MBCompID, ParameterDirection.InputOutput),
                   CreateParameter("@MBID", SqlDbType.Int, dc.MBID),
                   CreateParameter("@ItemID", SqlDbType.Int, dc.ItemID),
                   CreateParameter("@MB123", SqlDbType.Int, dc.MB123),
                   CreateParameter("@MBPercent", SqlDbType.Real, dc.MBPercent),
                   CreateParameter("@IsPreferred", SqlDbType.Bit, dc.IsPreferred),
                   CreateParameter("@AdditiveID", SqlDbType.Int, dc.AdditiveID),
                   CreateParameter("@AdditivePC", SqlDbType.Real, dc.AdditivePC),
                   CreateParameter("@last_updated_by", SqlDbType.VarChar, dc.last_updated_by, ParameterDirection.InputOutput),
                   CreateParameter("@last_updated_on", SqlDbType.DateTime2, dc.last_updated_on, ParameterDirection.InputOutput));


                dc.MBCompID = (int)cmd.Parameters["@MBCompID"].Value;
                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        public void MasterBatchComp_del(MasterBatchCompDC dc)
        {
            try
            {
                SqlCommand cmd = null;
                ExecuteNonQuery(ref cmd, "MasterBatchComp_del",
                   CreateParameter("@MBCompID", SqlDbType.Int, dc.MBCompID));


            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        /*
        public static void AddMasterBatchComp(MasterBatchCompDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("AddMasterBatchComp", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@MBCompID", SqlDbType.Int, 4);
                cmd.Parameters["@MBCompID"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@MBCompID"].Value = dc.MBCompID;
                cmd.Parameters.Add("@MBID", SqlDbType.Int, 4);
                cmd.Parameters["@MBID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MBID"].Value = dc.MBID;
                cmd.Parameters.Add("@ItemID", SqlDbType.Int, 4);
                cmd.Parameters["@ItemID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ItemID"].Value = dc.ItemID;
                cmd.Parameters.Add("@MB123", SqlDbType.Int, 4);
                cmd.Parameters["@MB123"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MB123"].Value = dc.MB123;
                cmd.Parameters.Add("@MBPercent", SqlDbType.Real, 4);
                cmd.Parameters["@MBPercent"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MBPercent"].Value = dc.MBPercent;
                cmd.Parameters.Add("@IsPreferred", SqlDbType.Bit, 1);
                cmd.Parameters["@IsPreferred"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@IsPreferred"].Value = dc.IsPreferred;
                cmd.Parameters.Add("@AdditiveID", SqlDbType.Int, 4);
                cmd.Parameters["@AdditiveID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@AdditiveID"].Value = dc.AdditiveID;
                cmd.Parameters.Add("@AdditivePC", SqlDbType.Real, 4);
                cmd.Parameters["@AdditivePC"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@AdditivePC"].Value = dc.AdditivePC;
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;

                cmd.ExecuteNonQuery();

                dc.MBCompID = (int)cmd.Parameters["@MBCompID"].Value;
                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        public static void UpdateMasterBatchComp(MasterBatchCompDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("UpdateMasterBatchComp", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@MBCompID", SqlDbType.Int, 4);
                cmd.Parameters["@MBCompID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MBCompID"].Value = dc.MBCompID;
                cmd.Parameters.Add("@MBID", SqlDbType.Int, 4);
                cmd.Parameters["@MBID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MBID"].Value = dc.MBID;
                cmd.Parameters.Add("@ItemID", SqlDbType.Int, 4);
                cmd.Parameters["@ItemID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ItemID"].Value = dc.ItemID;
                cmd.Parameters.Add("@MB123", SqlDbType.Int, 4);
                cmd.Parameters["@MB123"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MB123"].Value = dc.MB123;
                cmd.Parameters.Add("@MBPercent", SqlDbType.Real, 4);
                cmd.Parameters["@MBPercent"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MBPercent"].Value = dc.MBPercent;
                cmd.Parameters.Add("@IsPreferred", SqlDbType.Bit, 1);
                cmd.Parameters["@IsPreferred"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@IsPreferred"].Value = dc.IsPreferred;
                cmd.Parameters.Add("@AdditiveID", SqlDbType.Int, 4);
                cmd.Parameters["@AdditiveID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@AdditiveID"].Value = dc.AdditiveID;
                cmd.Parameters.Add("@AdditivePC", SqlDbType.Real, 4);
                cmd.Parameters["@AdditivePC"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@AdditivePC"].Value = dc.AdditivePC;
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

        public static void DeleteMasterBatchComp(MasterBatchCompDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("DeleteMasterBatchComp", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@MBCompID", SqlDbType.Int, 4);
                cmd.Parameters["@MBCompID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MBCompID"].Value = dc.MBCompID;

                cmd.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }
        */
    }
}
