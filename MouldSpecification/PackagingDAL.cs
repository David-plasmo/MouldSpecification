using DataService;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MouldSpecification
{
    internal class PackagingDAL : DataAccessBase
    {
        public DataSet SelectPackaging(int? itemID, int? custID)
        {
            try
            {
                return ExecuteDataSet("SelectPackaging",
                    CreateParameter("@ItemID", SqlDbType.Int, itemID),
                    CreateParameter("@CustomerID", SqlDbType.Int, custID));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void UpdatePackaging(DataSet ds,  string tableName = "Packaging")
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
                    PackagingDC dc = DAL.CreateItemFromRow<PackagingDC>(dr);  //populate  dataclass                   
                    AddPackaging(dc);

                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[tableName].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    PackagingDC dc = DAL.CreateItemFromRow<PackagingDC>(dr);  //populate  dataclass                   
                    UpdatePackaging(dc);
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[tableName].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    if (dr["PackingID", DataRowVersion.Original] != null)
                    {
                        PackagingDC dc = new PackagingDC();
                        dc.PackingID = Convert.ToInt32(dr["PackingID", DataRowVersion.Original].ToString());
                        DeletePackaging(dc);
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

        public static void AddPackaging(PackagingDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("AddPackaging", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@PackingID", SqlDbType.Int, 4);
                cmd.Parameters["@PackingID"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@PackingID"].Value = dc.PackingID;
                cmd.Parameters.Add("@ItemID", SqlDbType.Int, 4);
                cmd.Parameters["@ItemID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ItemID"].Value = dc.ItemID;
                cmd.Parameters.Add("@CtnID", SqlDbType.Int, 4);
                cmd.Parameters["@CtnID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CtnID"].Value = dc.CtnID;
                cmd.Parameters.Add("@PalletID", SqlDbType.Int, 4);
                cmd.Parameters["@PalletID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PalletID"].Value = dc.PalletID;
                cmd.Parameters.Add("@PackedInCtn", SqlDbType.Bit, 1);
                cmd.Parameters["@PackedInCtn"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PackedInCtn"].Value = dc.PackedInCtn;
                cmd.Parameters.Add("@CtnQty", SqlDbType.Int, 4);
                cmd.Parameters["@CtnQty"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CtnQty"].Value = dc.CtnQty;
                cmd.Parameters.Add("@Liner", SqlDbType.Bit, 1);
                cmd.Parameters["@Liner"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Liner"].Value = dc.Liner;
                cmd.Parameters.Add("@InnerBag", SqlDbType.Bit, 1);
                cmd.Parameters["@InnerBag"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@InnerBag"].Value = dc.InnerBag;
                cmd.Parameters.Add("@BagQty", SqlDbType.Int, 4);
                cmd.Parameters["@BagQty"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@BagQty"].Value = dc.BagQty;
                cmd.Parameters.Add("@PackingStyle", SqlDbType.VarChar, 200);
                cmd.Parameters["@PackingStyle"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PackingStyle"].Value = dc.PackingStyle;
                cmd.Parameters.Add("@PackedOnPallet", SqlDbType.Bit, 1);
                cmd.Parameters["@PackedOnPallet"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PackedOnPallet"].Value = dc.PackedOnPallet;
                cmd.Parameters.Add("@PalQty", SqlDbType.Int, 4);
                cmd.Parameters["@PalQty"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PalQty"].Value = dc.PalQty;
                cmd.Parameters.Add("@CtnsPerPallet", SqlDbType.Int, 4);
                cmd.Parameters["@CtnsPerPallet"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CtnsPerPallet"].Value = dc.CtnsPerPallet;
                cmd.Parameters.Add("@PalletCover", SqlDbType.Bit, 1);
                cmd.Parameters["@PalletCover"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PalletCover"].Value = dc.PalletCover;
                cmd.Parameters.Add("@Wrapping", SqlDbType.VarChar, 50);
                cmd.Parameters["@Wrapping"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Wrapping"].Value = dc.Wrapping;
                cmd.Parameters.Add("@LabelInnerBag", SqlDbType.VarChar, 200);
                cmd.Parameters["@LabelInnerBag"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@LabelInnerBag"].Value = dc.LabelInnerBag;
                cmd.Parameters.Add("@BarcodeLabel", SqlDbType.VarChar, 100);
                cmd.Parameters["@BarcodeLabel"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@BarcodeLabel"].Value = dc.BarcodeLabel;
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;

                cmd.ExecuteNonQuery();

                dc.PackingID = (int)cmd.Parameters["@PackingID"].Value;
                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;
                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        public static void UpdatePackaging(PackagingDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("UpdatePackaging", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@PackingID", SqlDbType.Int, 4);
                cmd.Parameters["@PackingID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PackingID"].Value = dc.PackingID;
                cmd.Parameters.Add("@ItemID", SqlDbType.Int, 4);
                cmd.Parameters["@ItemID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ItemID"].Value = dc.ItemID;
                cmd.Parameters.Add("@CtnID", SqlDbType.Int, 4);
                cmd.Parameters["@CtnID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CtnID"].Value = dc.CtnID;
                cmd.Parameters.Add("@PalletID", SqlDbType.Int, 4);
                cmd.Parameters["@PalletID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PalletID"].Value = dc.PalletID;
                cmd.Parameters.Add("@PackedInCtn", SqlDbType.Bit, 1);
                cmd.Parameters["@PackedInCtn"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PackedInCtn"].Value = dc.PackedInCtn;
                cmd.Parameters.Add("@CtnQty", SqlDbType.Int, 4);
                cmd.Parameters["@CtnQty"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CtnQty"].Value = dc.CtnQty;
                cmd.Parameters.Add("@Liner", SqlDbType.Bit, 1);
                cmd.Parameters["@Liner"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Liner"].Value = dc.Liner;
                cmd.Parameters.Add("@InnerBag", SqlDbType.Bit, 1);
                cmd.Parameters["@InnerBag"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@InnerBag"].Value = dc.InnerBag;
                cmd.Parameters.Add("@BagQty", SqlDbType.Int, 4);
                cmd.Parameters["@BagQty"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@BagQty"].Value = dc.BagQty;
                cmd.Parameters.Add("@PackingStyle", SqlDbType.VarChar, 200);
                cmd.Parameters["@PackingStyle"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PackingStyle"].Value = dc.PackingStyle;
                cmd.Parameters.Add("@PackedOnPallet", SqlDbType.Bit, 1);
                cmd.Parameters["@PackedOnPallet"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PackedOnPallet"].Value = dc.PackedOnPallet;
                cmd.Parameters.Add("@PalQty", SqlDbType.Int, 4);
                cmd.Parameters["@PalQty"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PalQty"].Value = dc.PalQty;
                cmd.Parameters.Add("@CtnsPerPallet", SqlDbType.Int, 4);
                cmd.Parameters["@CtnsPerPallet"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CtnsPerPallet"].Value = dc.CtnsPerPallet;
                cmd.Parameters.Add("@PalletCover", SqlDbType.Bit, 1);
                cmd.Parameters["@PalletCover"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PalletCover"].Value = dc.PalletCover;
                cmd.Parameters.Add("@Wrapping", SqlDbType.VarChar, 50);
                cmd.Parameters["@Wrapping"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Wrapping"].Value = dc.Wrapping;
                cmd.Parameters.Add("@LabelInnerBag", SqlDbType.VarChar, 200);
                cmd.Parameters["@LabelInnerBag"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@LabelInnerBag"].Value = dc.LabelInnerBag;
                cmd.Parameters.Add("@BarcodeLabel", SqlDbType.VarChar, 100);
                cmd.Parameters["@BarcodeLabel"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@BarcodeLabel"].Value = dc.BarcodeLabel;
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



        public static void DeletePackaging(PackagingDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("DeletePackaging", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@PackingID", SqlDbType.Int, 4);
                cmd.Parameters["@PackingID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PackingID"].Value = dc.PackingID;

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
