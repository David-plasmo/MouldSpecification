﻿using DataService;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MouldSpecification
{
    internal class MaterialCompDAL : DataAccessBase
    {
        public DataSet SelectMaterialGradeByType()
        {
            try
            {
                return ExecuteDataSet("SelectMaterialGradeByType");
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
                return ExecuteDataSet("SelectMaterialGrade");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataSet SelectMaterial()
        {
            try
            {
                return ExecuteDataSet("SelectMaterial");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataSet SelectMaterialComp(int? itemID, int? custID)
        {
            try
            {
                return ExecuteDataSet("SelectMaterialComp",
                    CreateParameter("@ItemID", SqlDbType.Int, itemID),
                    CreateParameter("@CustomerID", SqlDbType.Int, custID));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataSet SelectMAN_ItemIndex()
        {
            try
            {
                return ExecuteDataSet("SelectMAN_ItemIndex");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataSet GetPolymer123()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                DataColumn dc = new DataColumn("PolymerNo");
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

        public void UpdateMaterialComp(DataSet ds, string optionalTableName = "default")
        {
            try
            {
                //Process new rows:-
                DataViewRowState dvrs = DataViewRowState.Added;
                //DataRow[] rows = ds.Tables[0].Select("", "", dvrs);
                DataRow[] rows = (optionalTableName == "default")
                    ? ds.Tables[0].Select("", "", dvrs)
                    : ds.Tables[optionalTableName].Select("", "", dvrs);

                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    MaterialCompDC dc = DAL.CreateItemFromRow<MaterialCompDC>(dr);  //populate  dataclass                   
                    AddMaterialComp(dc);

                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = (optionalTableName == "default")
                    ? ds.Tables[0].Select("", "", dvrs)
                    : ds.Tables[optionalTableName].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    MaterialCompDC dc = DAL.CreateItemFromRow<MaterialCompDC>(dr);  //populate  dataclass                   
                    UpdateMaterialComp(dc);
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = (optionalTableName == "default")
                    ? ds.Tables[0].Select("", "", dvrs)
                    : ds.Tables[optionalTableName].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    if (dr["MaterialCompID", DataRowVersion.Original] != null)
                    {
                        MaterialCompDC dc = new MaterialCompDC();
                        dc.MaterialCompID = Convert.ToInt32(dr["MaterialCompID", DataRowVersion.Original].ToString());
                        DeleteMaterialComp(dc);
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

        public static void AddMaterialComp(MaterialCompDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("AddMaterialComp", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //will block a blank row
                if (!dc.MaterialGradeID.HasValue)
                    return;

                cmd.Parameters.Add("@MaterialCompID", SqlDbType.Int, 4);
                cmd.Parameters["@MaterialCompID"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@MaterialCompID"].Value = dc.MaterialCompID;
                cmd.Parameters.Add("@MaterialGradeID", SqlDbType.Int, 4);
                cmd.Parameters["@MaterialGradeID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MaterialGradeID"].Value = dc.MaterialGradeID;
                //cmd.Parameters.Add("@MaterialID", SqlDbType.Int, 4);
                //cmd.Parameters["@MaterialID"].Direction = System.Data.ParameterDirection.Input;
                //cmd.Parameters["@MaterialID"].Value = dc.MaterialID;
                cmd.Parameters.Add("@ItemID", SqlDbType.Int, 4);
                cmd.Parameters["@ItemID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ItemID"].Value = dc.ItemID;
                cmd.Parameters.Add("@Polymer123", SqlDbType.Int, 4);
                cmd.Parameters["@Polymer123"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Polymer123"].Value = dc.Polymer123;
                cmd.Parameters.Add("@PolymerPercent", SqlDbType.Real, 4);
                cmd.Parameters["@PolymerPercent"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PolymerPercent"].Value = dc.PolymerPercent;
                cmd.Parameters.Add("@RegrindMaxPC", SqlDbType.Real, 4);
                cmd.Parameters["@RegrindMaxPC"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@RegrindMaxPC"].Value = dc.RegrindMaxPC;
                cmd.Parameters.Add("@IsActive", SqlDbType.Bit, 1);
                cmd.Parameters["@IsActive"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@IsActive"].Value = dc.IsActive;
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;

                cmd.ExecuteNonQuery();

                dc.MaterialCompID = (int)cmd.Parameters["@MaterialCompID"].Value;
                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        public static void UpdateMaterialComp(MaterialCompDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("UpdateMaterialComp", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@MaterialCompID", SqlDbType.Int, 4);
                cmd.Parameters["@MaterialCompID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MaterialCompID"].Value = dc.MaterialCompID;
                cmd.Parameters.Add("@MaterialGradeID", SqlDbType.Int, 4);
                cmd.Parameters["@MaterialGradeID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MaterialGradeID"].Value = dc.MaterialGradeID;
                cmd.Parameters.Add("@ItemID", SqlDbType.Int, 4);
                cmd.Parameters["@ItemID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ItemID"].Value = dc.ItemID;
                cmd.Parameters.Add("@Polymer123", SqlDbType.Int, 4);
                cmd.Parameters["@Polymer123"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Polymer123"].Value = dc.Polymer123;
                cmd.Parameters.Add("@PolymerPercent", SqlDbType.Real, 4);
                cmd.Parameters["@PolymerPercent"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PolymerPercent"].Value = dc.PolymerPercent;
                cmd.Parameters.Add("@RegrindMaxPC", SqlDbType.Real, 4);
                cmd.Parameters["@RegrindMaxPC"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@RegrindMaxPC"].Value = dc.RegrindMaxPC;
                cmd.Parameters.Add("@IsActive", SqlDbType.Bit, 1);
                cmd.Parameters["@IsActive"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@IsActive"].Value = dc.IsActive;
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

        public static void DeleteMaterialComp(MaterialCompDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("DeleteMaterialComp", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@MaterialCompID", SqlDbType.Int, 4);
                cmd.Parameters["@MaterialCompID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MaterialCompID"].Value = dc.MaterialCompID;

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
