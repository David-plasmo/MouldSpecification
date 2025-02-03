using DataService;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MouldSpecification
{
    internal class CustomerPriceQtyDAL : DataAccessBase
    {
        public DataSet SelectCustomerCosting(int? itemID, int? custID)
        {
            try
            {
                return ExecuteDataSet("SelectCustomerCosting",
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

        public void UpdateCustomerCosting(DataSet ds)
        {
            try
            {

                //Process new rows:-
                DataViewRowState dvrs = DataViewRowState.Added;
                DataRow[] rows = ds.Tables[0].Select("", "", dvrs);
                //MachineDAL md = new MachineDAL();

                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    CustomerPriceQtyDC dc = DAL.CreateItemFromRow<CustomerPriceQtyDC>(dr);  //populate  dataclass                   
                    AddCustomerCosting(dc);

                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    CustomerPriceQtyDC dc = DAL.CreateItemFromRow<CustomerPriceQtyDC>(dr);  //populate  dataclass                   
                    UpdateCustomerCosting(dc);
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    if (dr["CostID", DataRowVersion.Original] != null)
                    {
                        CustomerPriceQtyDC dc = new CustomerPriceQtyDC();
                        dc.CostID = Convert.ToInt32(dr["CostID", DataRowVersion.Original].ToString());
                        DeleteCustomerCosting(dc);
                    }
                }
                ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
        }

        public static void AddCustomerCosting(CustomerPriceQtyDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("AddCustomerCosting", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@CostID", SqlDbType.Int, 4);
                cmd.Parameters["@CostID"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@CostID"].Value = dc.CostID;
                cmd.Parameters.Add("@ItemID", SqlDbType.Int, 4);
                cmd.Parameters["@ItemID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ItemID"].Value = dc.ItemID;
                cmd.Parameters.Add("@CustomerID", SqlDbType.Int, 4);
                cmd.Parameters["@CustomerID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CustomerID"].Value = dc.CustomerID;
                cmd.Parameters.Add("@PricingQty", SqlDbType.Int, 4);
                cmd.Parameters["@PricingQty"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PricingQty"].Value = dc.PricingQty;
                cmd.Parameters.Add("@CalculatedPrice", SqlDbType.Decimal, 9);
                cmd.Parameters["@CalculatedPrice"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CalculatedPrice"].Value = dc.CalculatedPrice;
                cmd.Parameters.Add("@CurrentPrice", SqlDbType.Decimal, 9);
                cmd.Parameters["@CurrentPrice"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CurrentPrice"].Value = dc.CurrentPrice;
                cmd.Parameters.Add("@DateChanged", SqlDbType.DateTime2, 8);
                cmd.Parameters["@DateChanged"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@DateChanged"].Value = dc.DateChanged;
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;

                cmd.ExecuteNonQuery();

                dc.CostID = (int)cmd.Parameters["@CostID"].Value;
                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        public static void UpdateCustomerCosting(CustomerPriceQtyDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("UpdateCustomerCosting", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@CostID", SqlDbType.Int, 4);
                cmd.Parameters["@CostID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CostID"].Value = dc.CostID;
                cmd.Parameters.Add("@ItemID", SqlDbType.Int, 4);
                cmd.Parameters["@ItemID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ItemID"].Value = dc.ItemID;
                cmd.Parameters.Add("@CustomerID", SqlDbType.Int, 4);
                cmd.Parameters["@CustomerID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CustomerID"].Value = dc.CustomerID;
                cmd.Parameters.Add("@PricingQty", SqlDbType.Int, 4);
                cmd.Parameters["@PricingQty"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PricingQty"].Value = dc.PricingQty;
                cmd.Parameters.Add("@CalculatedPrice", SqlDbType.Decimal, 9);
                cmd.Parameters["@CalculatedPrice"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CalculatedPrice"].Value = dc.CalculatedPrice;
                cmd.Parameters.Add("@CurrentPrice", SqlDbType.Decimal, 9);
                cmd.Parameters["@CurrentPrice"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CurrentPrice"].Value = dc.CurrentPrice;
                cmd.Parameters.Add("@DateChanged", SqlDbType.DateTime2, 8);
                cmd.Parameters["@DateChanged"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@DateChanged"].Value = dc.DateChanged;
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

        public static void DeleteCustomerCosting(CustomerPriceQtyDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("DeleteCustomerCosting", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@CostID", SqlDbType.Int, 4);
                cmd.Parameters["@CostID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CostID"].Value = dc.CostID;

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
