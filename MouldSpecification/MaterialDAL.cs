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
    public class MaterialDAL
    {
        public static int InsertMaterial(MaterialData dc)
        {
            int RETURN_VALUE = 0;
            SqlCommand cmd = null;
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
                    cmd = new System.Data.SqlClient.SqlCommand("PlasmoIntegration.dbo.InsertMaterial", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@RETURN_VALUE", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@RETURN_VALUE"].Direction = System.Data.ParameterDirection.ReturnValue;
                    cmd.Parameters["@RETURN_VALUE"].Value = RETURN_VALUE;
                    cmd.Parameters.Add("@MaterialID", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@MaterialID"].Direction = System.Data.ParameterDirection.InputOutput;
                    cmd.Parameters["@MaterialID"].Value = dc.MaterialID;
                    cmd.Parameters.Add("@ShortDesc", System.Data.SqlDbType.VarChar, 20);
                    cmd.Parameters["@ShortDesc"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@ShortDesc"].Value = dc.ShortDesc;
                    cmd.Parameters.Add("@Description", System.Data.SqlDbType.VarChar, 50);
                    cmd.Parameters["@Description"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@Description"].Value = dc.Description;
                    cmd.Parameters.Add("@Comment", System.Data.SqlDbType.VarChar, 50);
                    cmd.Parameters["@Comment"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@Comment"].Value = dc.Comment;
                    cmd.Parameters.Add("@last_updated_by", System.Data.SqlDbType.VarChar, 50);
                    cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                    cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;
                    cmd.Parameters.Add("@last_updated_on", System.Data.SqlDbType.DateTime2,7);
                    cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                    cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;
                    cmd.ExecuteNonQuery();
                        
                    // The Parameter @RETURN_VALUE is not an output type
                    dc.MaterialID = ((int)(cmd.Parameters["@MaterialID"].Value));
                    dc.last_updated_by = ((string)cmd.Parameters["@last_updated_by"].Value);
                    dc.last_updated_on = ((System.DateTime)cmd.Parameters["@last_updated_on"].Value);
                    // The Parameter @ShortDesc is not an output type
                    // The Parameter @Description is not an output type
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

        public static int UpdateMaterial(MaterialData dc)
        {
            int RETURN_VALUE = 0;
            SqlCommand cmd = null;
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
                    cmd = new System.Data.SqlClient.SqlCommand("PlasmoIntegration.dbo.UpdateMaterial", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@RETURN_VALUE", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@RETURN_VALUE"].Direction = System.Data.ParameterDirection.ReturnValue;
                    cmd.Parameters["@RETURN_VALUE"].Value = RETURN_VALUE;
                    cmd.Parameters.Add("@MaterialID", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@MaterialID"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@MaterialID"].Value = dc.MaterialID;
                    cmd.Parameters.Add("@ShortDesc", System.Data.SqlDbType.VarChar, 20);
                    cmd.Parameters["@ShortDesc"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@ShortDesc"].Value = dc.ShortDesc;
                    cmd.Parameters.Add("@Description", System.Data.SqlDbType.VarChar, 50);
                    cmd.Parameters["@Description"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@Description"].Value = dc.Description;                    
                    cmd.Parameters.Add("@Comment", System.Data.SqlDbType.VarChar, 50);
                    cmd.Parameters["@Comment"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@Comment"].Value = dc.Comment;
                    cmd.Parameters.Add("@last_updated_by", System.Data.SqlDbType.VarChar, 50);
                    cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;
                    cmd.Parameters.Add("@last_updated_on", System.Data.SqlDbType.DateTime2, 7);
                    cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                    cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;
                    cmd.ExecuteNonQuery();
                    //dc.MaterialID = ((int)(cmd.Parameters["@MaterialID"].Value));
                    //dc.last_updated_by = ((string)cmd.Parameters["@last_updated_by"].Value);
                    dc.last_updated_on = ((System.DateTime)cmd.Parameters["@last_updated_on"].Value);
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
