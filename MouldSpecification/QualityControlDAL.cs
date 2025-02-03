using DataService;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MouldSpecification
{
    internal class QualityControlDAL : DataAccessBase
    {
        public DataSet SelectQualityControl(int? itemID, int? custID)
        {
            try
            {
                return ExecuteDataSet("SelectQualityControl",
                    CreateParameter("@ItemID", SqlDbType.Int, itemID),
                    CreateParameter("@CustomerID", SqlDbType.Int, custID));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void UpdateQualityControl(DataSet ds, string optionalTableName = "default")
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
                    QualityControlDC dc = DAL.CreateItemFromRow<QualityControlDC>(dr);  //populate  dataclass                   
                    AddQualityControl(dc);
                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = (optionalTableName == "default")
                    ? ds.Tables[0].Select("", "", dvrs)
                    : ds.Tables[optionalTableName].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    QualityControlDC dc = DAL.CreateItemFromRow<QualityControlDC>(dr);  //populate  dataclass                   
                    UpdateQualityControl(dc);
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = (optionalTableName == "default")
                    ? ds.Tables[0].Select("", "", dvrs)
                    : ds.Tables[optionalTableName].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    if (dr["QualityControlID", DataRowVersion.Original] != null)
                    {
                        QualityControlDC dc = new QualityControlDC();
                        dc.QualityControlID = Convert.ToInt32(dr["QualityControlID", DataRowVersion.Original].ToString());
                        DeleteQualityControl(dc);
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

        public static void AddQualityControl(QualityControlDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("AddQualityControl", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@ItemID", SqlDbType.Int, 4);
                cmd.Parameters["@ItemID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ItemID"].Value = dc.ItemID;
                cmd.Parameters.Add("@QualityControlID", SqlDbType.Int, 4);
                cmd.Parameters["@QualityControlID"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@QualityControlID"].Value = dc.QualityControlID;
                cmd.Parameters.Add("@FinishedPTQC", SqlDbType.VarChar, 255);
                cmd.Parameters["@FinishedPTQC"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@FinishedPTQC"].Value = dc.FinishedPTQC;
                cmd.Parameters.Add("@ProductSample", SqlDbType.Bit, 1);
                cmd.Parameters["@ProductSample"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ProductSample"].Value = dc.ProductSample;
                cmd.Parameters.Add("@CertificateOfConformance", SqlDbType.Bit, 1);
                cmd.Parameters["@CertificateOfConformance"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CertificateOfConformance"].Value = dc.CertificateOfConformance;
                cmd.Parameters.Add("@Notes", SqlDbType.VarChar, 255);
                cmd.Parameters["@Notes"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Notes"].Value = dc.Notes;
                cmd.Parameters.Add("@LabelIcon", SqlDbType.VarChar, 255);
                cmd.Parameters["@LabelIcon"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@LabelIcon"].Value = dc.LabelIcon;
                cmd.Parameters.Add("@Costing", SqlDbType.NVarChar, 510);
                cmd.Parameters["@Costing"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Costing"].Value = dc.Costing;
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;

                cmd.ExecuteNonQuery();

                dc.QualityControlID = (int)cmd.Parameters["@QualityControlID"].Value;
                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        public static void UpdateQualityControl(QualityControlDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("UpdateQualityControl", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@ItemID", SqlDbType.Int, 4);
                cmd.Parameters["@ItemID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ItemID"].Value = dc.ItemID;
                cmd.Parameters.Add("@QualityControlID", SqlDbType.Int, 4);
                cmd.Parameters["@QualityControlID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@QualityControlID"].Value = dc.QualityControlID;
                cmd.Parameters.Add("@FinishedPTQC", SqlDbType.VarChar, 255);
                cmd.Parameters["@FinishedPTQC"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@FinishedPTQC"].Value = dc.FinishedPTQC;
                cmd.Parameters.Add("@ProductSample", SqlDbType.Bit, 1);
                cmd.Parameters["@ProductSample"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ProductSample"].Value = dc.ProductSample;
                cmd.Parameters.Add("@CertificateOfConformance", SqlDbType.Bit, 1);
                cmd.Parameters["@CertificateOfConformance"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CertificateOfConformance"].Value = dc.CertificateOfConformance;
                cmd.Parameters.Add("@Notes", SqlDbType.VarChar, 255);
                cmd.Parameters["@Notes"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Notes"].Value = dc.Notes;
                cmd.Parameters.Add("@LabelIcon", SqlDbType.VarChar, 255);
                cmd.Parameters["@LabelIcon"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@LabelIcon"].Value = dc.LabelIcon;
                cmd.Parameters.Add("@Costing", SqlDbType.NVarChar, 510);
                cmd.Parameters["@Costing"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Costing"].Value = dc.Costing;
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


        public static void DeleteQualityControl(QualityControlDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("DeleteQualityControl", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@QualityControlID", SqlDbType.Int, 4);
                cmd.Parameters["@QualityControlID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@QualityControlID"].Value = dc.QualityControlID;

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
