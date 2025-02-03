using DataService;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MouldSpecification
{
    class MaterialGradeDAL : DataAccessBase
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

        public DataSet SelectMaterialGrade()
        {
            try
            {
                DataSet ds = ExecuteDataSet("[dbo].[SelectMaterialGrade]");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public static void UpdateMaterialGrade(DataSet ds)
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
                    MaterialGradeDC dc = DAL.CreateItemFromRow<MaterialGradeDC>(dr);  //populate  dataclass                   
                    AddMaterialGrade(dc);

                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    MaterialGradeDC dc = DAL.CreateItemFromRow<MaterialGradeDC>(dr);  //populate  dataclass                   
                    UpdateMaterialGrade(dc);
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    if (dr["MaterialGradeID", DataRowVersion.Original] != null)
                    {
                        //ExecuteNonQuery("[DeleteMaterialGrade]",
                        //  this.CreateParameter("@MaterialGradeID", SqlDbType.Int, 
                        //  Convert.ToInt32(dr["MaterialGradeID", DataRowVersion.Original].ToString())));
                        //MaterialGradeDC dc = DAL.CreateItemFromRow<MaterialGradeDC>(dr);  //populate  dataclass
                        MaterialGradeDC dc = new MaterialGradeDC();
                        dc.MaterialGradeID = Convert.ToInt32(dr["MaterialGradeID", DataRowVersion.Original].ToString());
                        DeleteMaterialGrade(dc);
                    }
                }
                ds.AcceptChanges();
                ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
        }

        public static void AddMaterialGrade(MaterialGradeDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("AddMaterialGrade", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@MaterialGradeID", SqlDbType.Int, 4);
                cmd.Parameters["@MaterialGradeID"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@MaterialGradeID"].Value = dc.MaterialGradeID;
                cmd.Parameters.Add("@MaterialID", SqlDbType.Int, 4);
                cmd.Parameters["@MaterialID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MaterialID"].Value = dc.MaterialID;
                cmd.Parameters.Add("@MaterialGrade", SqlDbType.VarChar, 50);
                cmd.Parameters["@MaterialGrade"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MaterialGrade"].Value = dc.MaterialGrade;
                cmd.Parameters.Add("@CostPerKg", SqlDbType.Decimal, 9);
                cmd.Parameters["@CostPerKg"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CostPerKg"].Value = dc.CostPerKg;
                cmd.Parameters.Add("@Supplier", SqlDbType.VarChar, 100);
                cmd.Parameters["@Supplier"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Supplier"].Value = dc.Supplier;
                cmd.Parameters.Add("@Comment", SqlDbType.VarChar, 100);
                cmd.Parameters["@Comment"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Comment"].Value = dc.Comment;
                cmd.Parameters.Add("@AdditionalNotes", SqlDbType.VarChar, 100);
                cmd.Parameters["@AdditionalNotes"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@AdditionalNotes"].Value = dc.AdditionalNotes;
                cmd.Parameters.Add("@MachineType", SqlDbType.VarChar, 100);
                cmd.Parameters["@MachineType"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MachineType"].Value = dc.AdditionalNotes;
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;

                cmd.ExecuteNonQuery();

                dc.MaterialGradeID = (int)cmd.Parameters["@MaterialGradeID"].Value;
                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        public static void UpdateMaterialGrade(MaterialGradeDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("UpdateMaterialGrade", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@MaterialGradeID", SqlDbType.Int, 4);
                cmd.Parameters["@MaterialGradeID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MaterialGradeID"].Value = dc.MaterialGradeID;
                cmd.Parameters.Add("@MaterialID", SqlDbType.Int, 4);
                cmd.Parameters["@MaterialID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MaterialID"].Value = dc.MaterialID;
                cmd.Parameters.Add("@MaterialGrade", SqlDbType.VarChar, 50);
                cmd.Parameters["@MaterialGrade"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MaterialGrade"].Value = dc.MaterialGrade;
                cmd.Parameters.Add("@CostPerKg", SqlDbType.Decimal, 9);
                cmd.Parameters["@CostPerKg"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CostPerKg"].Value = dc.CostPerKg;
                cmd.Parameters.Add("@Supplier", SqlDbType.VarChar, 100);
                cmd.Parameters["@Supplier"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Supplier"].Value = dc.Supplier;
                cmd.Parameters.Add("@Comment", SqlDbType.VarChar, 100);
                cmd.Parameters["@Comment"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Comment"].Value = dc.Comment;
                cmd.Parameters.Add("@AdditionalNotes", SqlDbType.VarChar, 2);
                cmd.Parameters["@AdditionalNotes"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@AdditionalNotes"].Value = dc.MachineType;
                cmd.Parameters.Add("@MachineType", SqlDbType.VarChar, 100);
                cmd.Parameters["@MachineType"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MachineType"].Value = dc.AdditionalNotes;
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

        public static void DeleteMaterialGrade(MaterialGradeDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("DeleteMaterialGrade", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@MaterialGradeID", SqlDbType.Int, 4);
                cmd.Parameters["@MaterialGradeID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MaterialGradeID"].Value = dc.MaterialGradeID;

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
