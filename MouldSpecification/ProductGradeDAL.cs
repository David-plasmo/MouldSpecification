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
    class ProductGradeDAL
    {
        public static int InsertProductGrade(ProductGradeData dc)
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
                    cmd = new System.Data.SqlClient.SqlCommand("[PlasmoIntegration].[dbo].[InsertProductGrade]", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@RETURN_VALUE", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@RETURN_VALUE"].Direction = System.Data.ParameterDirection.ReturnValue;
                    cmd.Parameters["@RETURN_VALUE"].Value = RETURN_VALUE;
                    cmd.Parameters.Add("@GradeID", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@GradeID"].Direction = System.Data.ParameterDirection.InputOutput;
                    cmd.Parameters["@GradeID"].Value = dc.GradeID;
                    cmd.Parameters.Add("@ShortDesc", System.Data.SqlDbType.Char, 2);
                    cmd.Parameters["@ShortDesc"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@ShortDesc"].Value = dc.ShortDesc;
                    cmd.Parameters.Add("@Description", System.Data.SqlDbType.VarChar, 50);
                    cmd.Parameters["@Description"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@Description"].Value = dc.Description;
                    cmd.Parameters.Add("@ImagePath", System.Data.SqlDbType.VarChar, 150);
                    cmd.Parameters["@ImagePath"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@ImagePath"].Value = dc.ImagePath;
                    cmd.ExecuteNonQuery();
                    
                    // The Parameter @RETURN_VALUE is not an output type
                    dc.GradeID = ((int)(cmd.Parameters["@GradeID"].Value));
                    
                    // The Parameter @ShortDesc is not an output type
                    // The Parameter @Description is not an output type
                    // The Parameter @ImagePath is not an output type
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


        public static int UpdateProductGrade(ProductGradeData dc)
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
                    cmd = new System.Data.SqlClient.SqlCommand("[PlasmoIntegration].[dbo].[UpdateProductGrade]", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@RETURN_VALUE", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@RETURN_VALUE"].Direction = System.Data.ParameterDirection.ReturnValue;
                    cmd.Parameters["@RETURN_VALUE"].Value = RETURN_VALUE;
                    cmd.Parameters.Add("@GradeID", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@GradeID"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@GradeID"].Value = dc.GradeID;
                    cmd.Parameters.Add("@ShortDesc", System.Data.SqlDbType.Char, 2);
                    cmd.Parameters["@ShortDesc"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@ShortDesc"].Value = dc.ShortDesc;
                    cmd.Parameters.Add("@Description", System.Data.SqlDbType.VarChar, 50);
                    cmd.Parameters["@Description"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@Description"].Value = dc.Description;
                    cmd.Parameters.Add("@ImagePath", System.Data.SqlDbType.VarChar, 150);
                    cmd.Parameters["@ImagePath"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@ImagePath"].Value = dc.ImagePath;
                    
                    cmd.ExecuteNonQuery();
                    
                    // The Parameter @RETURN_VALUE is not an output type
                    // The Parameter @GradeID is not an output type
                    // The Parameter @ShortDesc is not an output type
                    // The Parameter @Description is not an output type
                    // The Parameter @ImagePath is not an output type
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
