using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DataService;
using System.Windows.Forms;

namespace MouldSpecification
{
    public class AdHocPriceQtyDAL
    {
        public static void InsertAdHocPriceQty(AdHocPriceQtyData dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("PlasmoIntegration.dbo.InsertAdHocPriceQty", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@AHPID", SqlDbType.Int, 4);
                cmd.Parameters["@AHPID"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@AHPID"].Value = dc.AHPID;
                cmd.Parameters.Add("@PmID", SqlDbType.Int, 4);
                cmd.Parameters["@PmID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PmID"].Value = dc.PmID;
                cmd.Parameters.Add("@Price", SqlDbType.Decimal, 9);
                cmd.Parameters["@Price"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Price"].Value = dc.Price;
                cmd.Parameters.Add("@DateChanged", SqlDbType.DateTime2, 8);
                cmd.Parameters["@DateChanged"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@DateChanged"].Value = dc.DateChanged;
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;
                cmd.Parameters.Add("@PricingQuantity", SqlDbType.Int, 4);
                cmd.Parameters["@PricingQuantity"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PricingQuantity"].Value = dc.PricingQuantity;
                cmd.Parameters.Add("@CustID", SqlDbType.Int, 4);
                cmd.Parameters["@CustID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CustID"].Value = dc.CustID;

                cmd.ExecuteNonQuery();

                dc.AHPID = (int)cmd.Parameters["@AHPID"].Value;
                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;
                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }

        }
        public static void UpdateAdHocPriceQty(AdHocPriceQtyData dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("PlasmoIntegration.dbo.UpdateAdHocPriceQty", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@AHPID", SqlDbType.Int, 4);
                cmd.Parameters["@AHPID"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@AHPID"].Value = dc.AHPID;
                cmd.Parameters.Add("@PmID", SqlDbType.Int, 4);
                cmd.Parameters["@PmID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PmID"].Value = dc.PmID;
                cmd.Parameters.Add("@Price", SqlDbType.Decimal, 9);
                cmd.Parameters["@Price"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Price"].Value = dc.Price;
                cmd.Parameters.Add("@DateChanged", SqlDbType.DateTime2, 8);
                cmd.Parameters["@DateChanged"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@DateChanged"].Value = dc.DateChanged;
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;
                cmd.Parameters.Add("@PricingQuantity", SqlDbType.Int, 4);
                cmd.Parameters["@PricingQuantity"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PricingQuantity"].Value = dc.PricingQuantity;
                cmd.Parameters.Add("@CustID", SqlDbType.Int, 4);
                cmd.Parameters["@CustID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CustID"].Value = dc.CustID;

                cmd.ExecuteNonQuery();

                dc.AHPID = (int)cmd.Parameters["@AHPID"].Value;
                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;
                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }
    }
}
