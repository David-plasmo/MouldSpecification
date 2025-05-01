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
        public void UpdateMaterialGrade(DataSet ds)
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
                    MaterialGrade_ups(dc);

                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    MaterialGradeDC dc = DAL.CreateItemFromRow<MaterialGradeDC>(dr);  //populate  dataclass                   
                    MaterialGrade_ups(dc);
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
                        MaterialGrade_del(dc);
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

        public void MaterialGrade_ups(MaterialGradeDC dc)
        {
            try
            {
                SqlCommand cmd = null;
                ExecuteNonQuery(ref cmd, "MaterialGrade_ups",
                   CreateParameter("@MaterialGradeID", SqlDbType.Int, dc.MaterialGradeID, ParameterDirection.InputOutput),
                   CreateParameter("@MaterialID", SqlDbType.Int, dc.MaterialID),
                   CreateParameter("@MaterialGrade", SqlDbType.VarChar, dc.MaterialGrade),
                   CreateParameter("@CostPerKg", SqlDbType.Decimal, dc.CostPerKg),
                   CreateParameter("@Supplier", SqlDbType.VarChar, dc.Supplier),
                   CreateParameter("@Comment", SqlDbType.VarChar, dc.Comment),
                   CreateParameter("@MachineType", SqlDbType.VarChar, dc.MachineType),
                   CreateParameter("@last_updated_by", SqlDbType.VarChar, dc.last_updated_by, ParameterDirection.InputOutput),
                   CreateParameter("@last_updated_on", SqlDbType.DateTime2, dc.last_updated_on, ParameterDirection.InputOutput));


                dc.MaterialGradeID = (int)cmd.Parameters["@MaterialGradeID"].Value;
                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        public void MaterialGrade_del(MaterialGradeDC dc)
        {
            try
            {
                SqlCommand cmd = null;
                ExecuteNonQuery(ref cmd, "MaterialGrade_del",
                   CreateParameter("@MaterialGradeID", SqlDbType.Int, dc.MaterialGradeID));


            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }
    }
}
