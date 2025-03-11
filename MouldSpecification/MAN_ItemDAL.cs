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
        public void MAN_Item_ups(MAN_ItemDC dc)
        {
            try
            {
                //convert any zeros to null, for foreign keys
                dc.LabelTypeID = (dc.LabelTypeID.HasValue ? (dc.LabelTypeID.Value > 0 ? dc.LabelTypeID :null) : null);
                dc.CartonID = (dc.CartonID.HasValue ? (dc.CartonID.Value > 0 ? dc.CartonID : null) : null);

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
                   CreateParameter("@last_updated_by", SqlDbType.VarChar, dc.last_updated_by, ParameterDirection.InputOutput));


                dc.ItemID = (int)cmd.Parameters["@ItemID"].Value;
                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;
                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
            }
            catch (Exception excp)
            {
                MessageBox.Show("MAN_ItemDAL: " + excp.Message, "MAN_Item_ups",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
