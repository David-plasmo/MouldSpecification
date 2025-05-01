using DataService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouldSpecification
{
    class MAN_ItemDAL :DataAccessBase
    {
        public int NewItemID { get; set; }  

        public void CopyToNew(int fromItemID, int customerID)
        {
            try
            {
                int newItemID = 0;

                SqlCommand cmd = null;
                ExecuteNonQuery(ref cmd, "CopyToNewIM",
                   CreateParameter("@FromItemID", SqlDbType.Int, fromItemID),
                   CreateParameter("@CustomerID", SqlDbType.Int, customerID),
                   CreateParameter("@ItemID", SqlDbType.Int, newItemID, ParameterDirection.InputOutput));

                newItemID = (int)cmd.Parameters["@ItemID"].Value;
                NewItemID = newItemID;                
            }
            catch (Exception ex) {MessageBox.Show(ex.Message); return; }
        }

        public void UpdateMAN_Item(DataSet ds, string tableName = "MAN_Items", string updateType = "Added")
        {
            try
            {
                DataViewRowState dvrs;
                DataRow[] rows;

                //Process new rows:-
                if (updateType == "Added")
                {
                    dvrs = DataViewRowState.Added;
                    rows = ds.Tables[tableName].Select("", "", dvrs);

                    for (int i = 0; i < rows.Length; i++)
                    {
                        DataRow dr = rows[i];
                        MAN_ItemDC dc = DAL.CreateItemFromRow<MAN_ItemDC>(dr);  //populate  dataclass                                       
                        MAN_Item_ups(dc);
                        dr.BeginEdit();
                        dr["ItemID"] = dc.ItemID;
                        dr["last_updated_by"] = dc.last_updated_by;
                        dr["last_updated_on"] = dc.last_updated_on;
                    }

                    return;
                }
                else
                {
                    //Process modified rows:-
                    dvrs = DataViewRowState.ModifiedCurrent;
                    rows = ds.Tables[tableName].Select("", "", dvrs);
                    for (int i = 0; i < rows.Length; i++)
                    {
                        DataRow dr = rows[i];
                        MAN_ItemDC dc = DAL.CreateItemFromRow<MAN_ItemDC>(dr);  //populate  dataclass                                       
                        MAN_Item_ups(dc);
                        dr.BeginEdit();                        
                        dr["last_updated_by"] = dc.last_updated_by;
                        dr["last_updated_on"] = dc.last_updated_on;
                    }

                    //process deleted rows:-                
                    dvrs = DataViewRowState.Deleted;
                    rows = ds.Tables[tableName].Select("", "", dvrs);
                    for (int i = 0; i < rows.Length; i++)
                    {
                        DataRow dr = rows[i];
                        if (dr["MachPrefID", DataRowVersion.Original] != null)
                        {
                            MAN_ItemDC dc = new MAN_ItemDC();
                            dc.ItemID = Convert.ToInt32(dr["ItemID", DataRowVersion.Original].ToString());
                            MAN_Item_del(dc);
                        }
                    }

                    //ds.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void MAN_Item_ups(MAN_ItemDC dc)
        {
            try
            {
                SqlCommand cmd = null;
                ExecuteNonQuery(ref cmd, "MAN_Item_ups",
                   CreateParameter("@ITEMNMBR", SqlDbType.Char, dc.ITEMNMBR),
                   CreateParameter("@ItemID", SqlDbType.Int, dc.ItemID, ParameterDirection.InputOutput),
                   CreateParameter("@ITEMDESC", SqlDbType.Char, dc.ITEMDESC),
                   CreateParameter("@AltCode", SqlDbType.VarChar, dc.AltCode),
                   CreateParameter("@ProductType", SqlDbType.VarChar, dc.ProductType),
                   CreateParameter("@GradeID", SqlDbType.Int, dc.GradeID),
                   CreateParameter("@ImageFile", SqlDbType.VarChar, dc.ImageFile),
                   CreateParameter("@ComponentWeight", SqlDbType.Decimal, dc.ComponentWeight),
                   CreateParameter("@SprueRunnerTotal", SqlDbType.Decimal, dc.SprueRunnerTotal),
                   CreateParameter("@TotalShotWeight", SqlDbType.Decimal, dc.TotalShotWeight),
                   CreateParameter("@CompDB", SqlDbType.VarChar, dc.CompDB),
                   CreateParameter("@ITMCLSCD", SqlDbType.Char, dc.ITMCLSCD),
                   CreateParameter("@CtnQty", SqlDbType.Int, dc.CtnQty),
                   CreateParameter("@CartonID", SqlDbType.Int, dc.CartonID),
                   CreateParameter("@LabelTypeID", SqlDbType.Int, dc.LabelTypeID),
                   CreateParameter("@BottleSize", SqlDbType.Char, dc.BottleSize),
                   CreateParameter("@Style", SqlDbType.Char, dc.Style),
                   CreateParameter("@NeckSize", SqlDbType.Char, dc.NeckSize),
                   CreateParameter("@Colour", SqlDbType.Char, dc.Colour),
                   CreateParameter("@DangerousGood", SqlDbType.Bit, dc.DangerousGood),
                   CreateParameter("@StockLine", SqlDbType.Bit, dc.StockLine),
                   CreateParameter("@last_updated_on", SqlDbType.DateTime2, dc.last_updated_on, ParameterDirection.InputOutput),
                   CreateParameter("@last_updated_by", SqlDbType.VarChar, dc.last_updated_by, ParameterDirection.InputOutput),
                   CreateParameter("@AdditionalNotes", SqlDbType.VarChar, dc.AdditionalNotes));


                dc.ItemID = (int)cmd.Parameters["@ItemID"].Value;
                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;
                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        public void MAN_Item_del(MAN_ItemDC dc)
        {
            try
            {
                SqlCommand cmd = null;
                ExecuteNonQuery(ref cmd, "MAN_Item_del",
                   CreateParameter("@ItemID", SqlDbType.Int, dc.ItemID));


            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }
    }
}
