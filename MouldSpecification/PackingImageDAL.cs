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
    internal class PackingImageDAL : DataAccessBase
    {
        public void UpdatePackingImage(DataSet ds, string tableName)
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
                    PackingImageDC dc = DAL.CreateItemFromRow<PackingImageDC>(dr);  //populate  dataclass                   
                    AddPackingImage(dc);

                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[tableName].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    PackingImageDC dc = DAL.CreateItemFromRow<PackingImageDC>(dr);  //populate  dataclass                   
                    UpdatePackingImage(dc);
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[tableName].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    if (dr["PackingImageID", DataRowVersion.Original] != null)
                    {
                        PackingImageDC dc = new PackingImageDC();
                        dc.PackingImageID = Convert.ToInt32(dr["PackingImageID", DataRowVersion.Original].ToString());
                        DeletePackingImage(dc);
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

        public static void AddPackingImage(PackingImageDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("AddPackingImage", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@PackingImageID", SqlDbType.Int, 4);
                cmd.Parameters["@PackingImageID"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@PackingImageID"].Value = dc.PackingImageID;
                cmd.Parameters.Add("@ItemID", SqlDbType.Int, 4);
                cmd.Parameters["@ItemID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ItemID"].Value = dc.ItemID;
                cmd.Parameters.Add("@PackingImageFilepath1", SqlDbType.VarChar, 200);
                cmd.Parameters["@PackingImageFilepath1"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PackingImageFilepath1"].Value = dc.PackingImageFilepath1;
                cmd.Parameters.Add("@PackingImageFilepath2", SqlDbType.VarChar, 200);
                cmd.Parameters["@PackingImageFilepath2"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PackingImageFilepath2"].Value = dc.PackingImageFilepath2;
                cmd.Parameters.Add("@PackingImageFilepath3", SqlDbType.VarChar, 200);
                cmd.Parameters["@PackingImageFilepath3"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PackingImageFilepath3"].Value = dc.PackingImageFilepath3;

                cmd.ExecuteNonQuery();

                dc.PackingImageID = (int)cmd.Parameters["@PackingImageID"].Value;
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        public static void UpdatePackingImage(PackingImageDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("UpdatePackingImage", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@PackingImageID", SqlDbType.Int, 4);
                cmd.Parameters["@PackingImageID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PackingImageID"].Value = dc.PackingImageID;
                cmd.Parameters.Add("@ItemID", SqlDbType.Int, 4);
                cmd.Parameters["@ItemID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ItemID"].Value = dc.ItemID;
                cmd.Parameters.Add("@PackingImageFilepath1", SqlDbType.VarChar, 200);
                cmd.Parameters["@PackingImageFilepath1"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PackingImageFilepath1"].Value = dc.PackingImageFilepath1;
                cmd.Parameters.Add("@PackingImageFilepath2", SqlDbType.VarChar, 200);
                cmd.Parameters["@PackingImageFilepath2"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PackingImageFilepath2"].Value = dc.PackingImageFilepath2;
                cmd.Parameters.Add("@PackingImageFilepath3", SqlDbType.VarChar, 200);
                cmd.Parameters["@PackingImageFilepath3"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PackingImageFilepath3"].Value = dc.PackingImageFilepath3;

                cmd.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        public static void DeletePackingImage(PackingImageDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("DeletePackingImage", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@PackingImageID", SqlDbType.Int, 4);
                cmd.Parameters["@PackingImageID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PackingImageID"].Value = dc.PackingImageID;

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
