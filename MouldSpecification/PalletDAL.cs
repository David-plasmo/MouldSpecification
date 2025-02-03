using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DataService;

namespace MouldSpecification
{
    class PalletDAL
    {
        public static int InsertPallet(PalletData dc)
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
                    cmd = new System.Data.SqlClient.SqlCommand("InsertPallet", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@RETURN_VALUE", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@RETURN_VALUE"].Direction = System.Data.ParameterDirection.ReturnValue;
                    cmd.Parameters["@RETURN_VALUE"].Value = RETURN_VALUE;
                    cmd.Parameters.Add("@PalletID", System.Data.SqlDbType.Int);
                    cmd.Parameters["@PalletID"].Direction = System.Data.ParameterDirection.InputOutput;
                    cmd.Parameters["@PalletID"].Value = dc.PalletID;
                    cmd.Parameters.Add("@Pallet", System.Data.SqlDbType.VarChar, 50);
                    cmd.Parameters["@Pallet"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@Pallet"].Value = dc.Pallet;
                   
                    cmd.ExecuteNonQuery();
                    
                    // The Parameter @RETURN_VALUE is not an output type
                    // The Parameter @Pallet is not an output type
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

        public static int UpdatePallet(PalletData dc)
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
                    cmd = new System.Data.SqlClient.SqlCommand("UpdatePallet", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@RETURN_VALUE", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@RETURN_VALUE"].Direction = System.Data.ParameterDirection.ReturnValue;
                    cmd.Parameters["@RETURN_VALUE"].Value = RETURN_VALUE;
                    cmd.Parameters.Add("@PalletID", System.Data.SqlDbType.Int);
                    cmd.Parameters["@PalletID"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@PalletID"].Value = dc.PalletID;
                    cmd.Parameters.Add("@Pallet", System.Data.SqlDbType.VarChar, 50);
                    cmd.Parameters["@Pallet"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@Pallet"].Value = dc.Pallet;
                   
                    cmd.ExecuteNonQuery();
                    
                    // The Parameter @RETURN_VALUE is not an output type
                    // The Parameter @Pallet is not an output type
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
