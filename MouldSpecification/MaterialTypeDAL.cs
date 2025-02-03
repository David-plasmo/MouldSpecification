using DataService;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace MouldSpecification
{
    class MaterialTypeDAL : DataAccessBase
    {
        public DataSet GetMaterial()
        {
            try
            {
                DataSet ds = ExecuteDataSet("[dbo].[GetMaterial]");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public bool CheckDependencies(int materialID)
        {
            try
            {
                DataSet ds = ExecuteDataSet("[dbo].[SelectMaterialGradeByMaterial]",
                    CreateParameter("@MaterialID", SqlDbType.Int, materialID));
                return (ds.Tables[0].Rows.Count > 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public void UpdateMaterial(DataSet ds)
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
                    MaterialTypeDC dc = DAL.CreateItemFromRow<MaterialTypeDC>(dr);  //populate  dataclass                   
                    AddMaterial(dc);

                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    MaterialTypeDC dc = DAL.CreateItemFromRow<MaterialTypeDC>(dr);  //populate  dataclass                   
                    UpdateMaterial(dc);
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    if (dr["MaterialID", DataRowVersion.Original] != null)
                    {
                        MaterialTypeDC dc = new MaterialTypeDC();
                        dc.MaterialID = Convert.ToInt32(dr["MaterialID", DataRowVersion.Original].ToString());
                        DeleteMaterial(dc);
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

        public static void AddMaterial(MaterialTypeDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("AddMaterial", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@MaterialID", SqlDbType.Int, 4);
                cmd.Parameters["@MaterialID"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@MaterialID"].Value = dc.MaterialID;
                cmd.Parameters.Add("@ShortDesc", SqlDbType.VarChar, 20);
                cmd.Parameters["@ShortDesc"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ShortDesc"].Value = dc.ShortDesc;
                cmd.Parameters.Add("@Description", SqlDbType.VarChar, 50);
                cmd.Parameters["@Description"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Description"].Value = dc.Description;
                cmd.Parameters.Add("@Comment", SqlDbType.VarChar, 100);
                cmd.Parameters["@Comment"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Comment"].Value = dc.Comment;
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;

                cmd.ExecuteNonQuery();

                dc.MaterialID = (int)cmd.Parameters["@MaterialID"].Value;
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        public static void UpdateMaterial(MaterialTypeDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("UpdateMaterial", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@MaterialID", SqlDbType.Int, 4);
                cmd.Parameters["@MaterialID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MaterialID"].Value = dc.MaterialID;
                cmd.Parameters.Add("@ShortDesc", SqlDbType.VarChar, 20);
                cmd.Parameters["@ShortDesc"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ShortDesc"].Value = dc.ShortDesc;
                cmd.Parameters.Add("@Description", SqlDbType.VarChar, 50);
                cmd.Parameters["@Description"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Description"].Value = dc.Description;
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

        public static void DeleteMaterial(MaterialTypeDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("DeleteMaterial", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@MaterialID", SqlDbType.Int, 4);
                cmd.Parameters["@MaterialID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MaterialID"].Value = dc.MaterialID;

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
