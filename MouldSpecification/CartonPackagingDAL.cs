using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataService;

namespace MouldSpecification
{
    public class CartonPackagingDAL
    { 
        //public static int InsertCartonPackaging(System.Data.SqlClient.SqlConnection connection, System.Data.DataTable table, ref int CtnID, string CartonType, decimal CartonCost, string LinerType, decimal LinerCost, string InnerBag, decimal InnerBagCost, string Comment, ref string last_updated_by, ref System.DateTime last_updated_on)
        public static int InsertCartonPackaging(CartonPackagingData dc)
        {
            int RETURN_VALUE = 0;
            System.Data.SqlClient.SqlCommand cmd = null;
            //System.Data.SqlClient.SqlDataReader reader = null;
            SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
            if ((connection == null))
            {
                throw new System.ArgumentException("The connection object cannot be null");
            }
            else
            {
                if ((connection.State == System.Data.ConnectionState.Closed))
                {
                    connection.Open();
                    cmd = new System.Data.SqlClient.SqlCommand("PlasmoIntegration.dbo.InsertCartonPackaging", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@RETURN_VALUE", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@RETURN_VALUE"].Direction = System.Data.ParameterDirection.ReturnValue;
                    cmd.Parameters["@RETURN_VALUE"].Value = RETURN_VALUE;
                    cmd.Parameters.Add("@CtnID", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@CtnID"].Direction = System.Data.ParameterDirection.InputOutput;
                    cmd.Parameters["@CtnID"].Value = dc.CtnID;
                    cmd.Parameters.Add("@GPCartonID", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@GPCartonID"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@GPCartonID"].Value = dc.GPCartonID;
                    cmd.Parameters.Add("@CartonType", System.Data.SqlDbType.VarChar, 30);
                    cmd.Parameters["@CartonType"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@CartonType"].Value = dc.CartonType;
                    cmd.Parameters.Add("@CartonCost", System.Data.SqlDbType.Decimal, 0);
                    cmd.Parameters["@CartonCost"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@CartonCost"].Value = dc.CartonCost;
                    cmd.Parameters.Add("@LinerType", System.Data.SqlDbType.VarChar, 30);
                    cmd.Parameters["@LinerType"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@LinerType"].Value = dc.LinerType;
                    cmd.Parameters.Add("@LinerCost", System.Data.SqlDbType.Decimal, 0);
                    cmd.Parameters["@LinerCost"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@LinerCost"].Value = dc.LinerCost;
                    cmd.Parameters.Add("@InnerBag", System.Data.SqlDbType.VarChar, 30);
                    cmd.Parameters["@InnerBag"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@InnerBag"].Value = dc.InnerBag;
                    cmd.Parameters.Add("@InnerBagCost", System.Data.SqlDbType.Decimal, 0);
                    cmd.Parameters["@InnerBagCost"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@InnerBagCost"].Value = dc.InnerBagCost;
                    //cmd.Parameters.Add("@WidthMM", System.Data.SqlDbType.Real, 0);
                    //cmd.Parameters["@WidthMM"].Direction = System.Data.ParameterDirection.Input;
                    //cmd.Parameters["@WidthMM"].Value = dc.WidthMM;
                    //cmd.Parameters.Add("@HeightMM", System.Data.SqlDbType.Real, 0);
                    //cmd.Parameters["@HeightMM"].Direction = System.Data.ParameterDirection.Input;
                    //cmd.Parameters["@HeightMM"].Value = dc.HeightMM;
                    //cmd.Parameters.Add("@DepthMM", System.Data.SqlDbType.Real, 0);
                    //cmd.Parameters["@DepthMM"].Direction = System.Data.ParameterDirection.Input;
                    //cmd.Parameters["@DepthMM"].Value = dc.DepthMM;
                    //cmd.Parameters.Add("@PalletQty", System.Data.SqlDbType.Int, 0);
                    //cmd.Parameters["@PalletQty"].Direction = System.Data.ParameterDirection.Input;
                    //cmd.Parameters["@PalletQty"].Value = dc.PalletQty;
                    cmd.Parameters.Add("@Comment", System.Data.SqlDbType.VarChar, 100);
                    cmd.Parameters["@Comment"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@Comment"].Value = dc.Comment;
                    cmd.Parameters.Add("@last_updated_by", System.Data.SqlDbType.VarChar, 50);
                    cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                    cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;
                    cmd.Parameters.Add("@last_updated_on", System.Data.SqlDbType.DateTime2, 0);
                    cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                    cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;
                                       
                    cmd.ExecuteNonQuery();

                    // The Parameter @RETURN_VALUE is not an output type
                    dc.CtnID = ((int)(cmd.Parameters["@CtnID"].Value));
                    // The Parameter @CartonType is not an output type
                    // The Parameter @CartonCost is not an output type
                    // The Parameter @LinerType is not an output type
                    // The Parameter @LinerCost is not an output type
                    // The Parameter @InnerBag is not an output type
                    // The Parameter @InnerBagCost is not an output type
                    // The Parameter @Comment is not an output type
                    dc.last_updated_by = ((string)(cmd.Parameters["@last_updated_by"].Value));
                    dc.last_updated_on = ((System.DateTime)(cmd.Parameters["@last_updated_on"].Value));
                    connection.Close();
                    RETURN_VALUE = ((int)(cmd.Parameters["@RETURN_VALUE"].Value));
                    return RETURN_VALUE;
                }
                else
                {
                    throw new System.ArgumentException("The connection must be closed when calling this method.");
                }
            }
        }

        //public static int UpdateCartonPackaging(System.Data.SqlClient.SqlConnection connection, System.Data.DataTable table, int CtnID, string CartonType, decimal CartonCost, string LinerType, decimal LinerCost, string InnerBag, decimal InnerBagCost, string Comment, ref string last_updated_by, ref System.DateTime last_updated_on)
        public static int UpdateCartonPackaging(CartonPackagingData dc)
        {
            int RETURN_VALUE = 0;
            System.Data.SqlClient.SqlCommand cmd = null;
            //System.Data.SqlClient.SqlDataReader reader = null;
            SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
            if ((connection == null))
            {
                throw new System.ArgumentException("The connection object cannot be null");
            }
            else
            {
                if ((connection.State == System.Data.ConnectionState.Closed))
                {
                    connection.Open();
                    cmd = new System.Data.SqlClient.SqlCommand("PlasmoIntegration.dbo.UpdateCartonPackaging", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@RETURN_VALUE", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@RETURN_VALUE"].Direction = System.Data.ParameterDirection.ReturnValue;
                    cmd.Parameters["@RETURN_VALUE"].Value = RETURN_VALUE;
                    cmd.Parameters.Add("@CtnID", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@CtnID"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@CtnID"].Value = dc.CtnID;
                    cmd.Parameters.Add("@GPCartonID", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@GPCartonID"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@GPCartonID"].Value = dc.GPCartonID;
                    cmd.Parameters.Add("@CartonType", System.Data.SqlDbType.VarChar, 30);
                    cmd.Parameters["@CartonType"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@CartonType"].Value = dc.CartonType;
                    cmd.Parameters.Add("@CartonCost", System.Data.SqlDbType.Decimal, 0);
                    cmd.Parameters["@CartonCost"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@CartonCost"].Value = dc.CartonCost;
                    cmd.Parameters.Add("@LinerType", System.Data.SqlDbType.VarChar, 30);
                    cmd.Parameters["@LinerType"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@LinerType"].Value = dc.LinerType;
                    cmd.Parameters.Add("@LinerCost", System.Data.SqlDbType.Decimal, 0);
                    cmd.Parameters["@LinerCost"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@LinerCost"].Value = dc.LinerCost;
                    cmd.Parameters.Add("@InnerBag", System.Data.SqlDbType.VarChar, 30);
                    cmd.Parameters["@InnerBag"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@InnerBag"].Value = dc.InnerBag;
                    cmd.Parameters.Add("@InnerBagCost", System.Data.SqlDbType.Decimal, 0);
                    cmd.Parameters["@InnerBagCost"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@InnerBagCost"].Value = dc.InnerBagCost;
                    //cmd.Parameters.Add("@WidthMM", System.Data.SqlDbType.Real, 0);
                    //cmd.Parameters["@WidthMM"].Direction = System.Data.ParameterDirection.Input;
                    //cmd.Parameters["@WidthMM"].Value = dc.WidthMM;
                    //cmd.Parameters.Add("@HeightMM", System.Data.SqlDbType.Real, 0);
                    //cmd.Parameters["@HeightMM"].Direction = System.Data.ParameterDirection.Input;
                    //cmd.Parameters["@HeightMM"].Value = dc.HeightMM;
                    //cmd.Parameters.Add("@DepthMM", System.Data.SqlDbType.Real, 0);
                    //cmd.Parameters["@DepthMM"].Direction = System.Data.ParameterDirection.Input;
                    //cmd.Parameters["@DepthMM"].Value = dc.DepthMM;
                    //cmd.Parameters.Add("@PalletQty", System.Data.SqlDbType.Int, 0);
                    //cmd.Parameters["@PalletQty"].Direction = System.Data.ParameterDirection.Input;
                    //cmd.Parameters["@PalletQty"].Value = dc.PalletQty;
                    cmd.Parameters.Add("@Comment", System.Data.SqlDbType.VarChar, 100);
                    cmd.Parameters["@Comment"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@Comment"].Value = dc.Comment;
                    cmd.Parameters.Add("@last_updated_by", System.Data.SqlDbType.VarChar, 50);
                    cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                    cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;
                    cmd.Parameters.Add("@last_updated_on", System.Data.SqlDbType.DateTime2, 0);
                    cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                    cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;
                    
                    cmd.ExecuteNonQuery();
                    
                    // The Parameter @RETURN_VALUE is not an output type
                    // The Parameter @CtnID is not an output type
                    // The Parameter @CartonType is not an output type
                    // The Parameter @CartonCost is not an output type
                    // The Parameter @LinerType is not an output type
                    // The Parameter @LinerCost is not an output type
                    // The Parameter @InnerBag is not an output type
                    // The Parameter @InnerBagCost is not an output type
                    // The Parameter @Comment is not an output type
                    
                    dc.last_updated_by = ((string)(cmd.Parameters["@last_updated_by"].Value));
                    dc.last_updated_on = ((System.DateTime)(cmd.Parameters["@last_updated_on"].Value));
                    connection.Close();
                    RETURN_VALUE = ((int)(cmd.Parameters["@RETURN_VALUE"].Value));
                    return RETURN_VALUE;
                }
                else
                {
                    throw new System.ArgumentException("The connection must be closed when calling this method.");
                }
            }
        }
    }
}
