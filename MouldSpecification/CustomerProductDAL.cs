using DataService;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace MouldSpecification
{
    public class CustomerProductDAL : DataAccessBase
    {
        public DataSet CustomerProductDS()
        {
            try
            {
                return ExecuteDataSet("dbo.SelectCustomerProduct");
            }
            catch (System.Exception e) { return null; }            
        }

        public void UpdateCustomerProduct(DataSet ds, string tableName)
        {
            try
            {

                //Process new rows:-
                DataViewRowState dvrs = DataViewRowState.Added;
                DataRow[] rows = ds.Tables[tableName].Select("", "", dvrs);               

                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    CustomerProductDC dc = DAL.CreateItemFromRow<CustomerProductDC>(dr);  //populate  dataclass
                    if (dc.CustomerID <= 0) { throw new Exception("UpdateCustomerProduct:  CustomerID must be > 0"); }
                    //if (dc.ItemID <= 0) { throw new Exception("UpdateCustomerProduct:  ItemID must be > 0"); }                                                                      //
                    CustomerProduct_Ups(dc);
                    dr.BeginEdit();
                    dr["CustomerProductID"] = dc.CustomerProductID;
                    dr.EndEdit();
                }
                if (rows.Length > 0) ds.AcceptChanges();

                //Process modified rows: -
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[tableName].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    CustomerProductDC dc = DAL.CreateItemFromRow<CustomerProductDC>(dr);  //populate  dataclass
                    if (dc.CustomerID <= 0) { throw new Exception("UpdateCustomerProduct:  CustomerID must be > 0"); }
                    if (dc.ItemID <= 0) { throw new Exception("UpdateCustomerProduct:  ItemID must be > 0"); }                                                                    //
                    CustomerProduct_Ups(dc);
                }
                

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[tableName].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    if (dr["CustomerProductID", DataRowVersion.Original] != null)                        
                    {
                        CustomerProductDC dc = new CustomerProductDC();
                        dc.CustomerProductID = Convert.ToInt32(dr["CustomerProductID", DataRowVersion.Original].ToString());
                        CustomerProduct_Del(dc);
                    }
                }

                //ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                throw;
            }
        }

        public void CustomerProduct_Ups(CustomerProductDC dc)
        {
            try
            {
                if (dc.CustomerID <= 0) {  throw new Exception("CustomerProduct_Ups:  CustomerID must be > 0"); }
                if (dc.ItemID <= 0) { throw new Exception("CustomerProduct_Ups:  ItemID must be > 0"); }
                SqlCommand cmd = null;
                ExecuteNonQuery(ref cmd, "CustomerProduct_Ups",
                   CreateParameter("@CustomerID", SqlDbType.Int, dc.CustomerID),
                   CreateParameter("@ItemID", SqlDbType.Int, dc.ItemID),
                   CreateParameter("@CustomerProductID", SqlDbType.Int, dc.CustomerProductID, ParameterDirection.InputOutput));


                dc.CustomerProductID = (int)cmd.Parameters["@CustomerProductID"].Value;
            }
            catch (Exception excp)
            {
                throw;
            }
        }

        public void CustomerProduct_Del(CustomerProductDC dc)
        {
            try
            {
                SqlCommand cmd = null;
                ExecuteNonQuery(ref cmd, "CustomerProduct_Del",
                   CreateParameter("@CustomerProductID", SqlDbType.Int, dc.CustomerProductID));


            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

    }
}
