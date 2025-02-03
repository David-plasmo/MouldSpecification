using DataService;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MouldSpecification
{
    internal class ProductDetailsDAL : DataAccessBase
    {        
        public string NewFormattedCode()
        {
            try
            {
                DataSet ds =  ExecuteDataSet("[dbo].[GetNewFormattedCode]");
                DataTable dt = ds.Tables[0];
                DataRow dr = dt.Rows[0];
                return dr[0].ToString();
            }
            catch (Exception ex) { return null; }
        }

        public DataSet SelectMAN_Item(int? itemID, int? custID)
        {
            try
            {
                return ExecuteDataSet("selectMAN_Item",
                    CreateParameter("ItemID", SqlDbType.Int, itemID),
                    CreateParameter("CustomerID", SqlDbType.Int, custID));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataSet GetCompany()
        {
            try
            {
                return ExecuteDataSet("[PlasmoIntegration].[dbo].[GetCompany]");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;

            }
        }

        public DataSet GetItemClassByCompany(string compCode)
        {
            try
            {
                return ExecuteDataSet("[dbo].[SelectItemClassByCompany]",
                                        CreateParameter("@CompanyCode", SqlDbType.VarChar, compCode));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataSet GetCartonSizes()
        {
            try
            {
                return ExecuteDataSet("[PlasmoIntegration].[dbo].[GetCartonSizes]");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataSet GetProductGrade()
        {
            try
            {
                return ExecuteDataSet("[PlasmoIntegration].[dbo].[GetProductGrade]");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataSet GetLabelTypes()
        {
            try
            {
                return ExecuteDataSet("[BarTender].[dbo].[GetLabelTypes]");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataSet LookupUSCATVLS(int uscatNum)
        {
            try
            {
                return ExecuteDataSet("[PlasmoIntegration].[dbo].[LookupUSCATVLS]",
                    CreateParameter("@UscatNum", SqlDbType.Int, uscatNum));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public string CheckDependencies(int itemID)
        {
            try
            {
                int dcount = 0;
                string[] msg = new string[7];
                string msgOut = null;

                DataSet ds = ExecuteDataSet("dbo.SelectMaterialComp", CreateParameter("@ItemID", SqlDbType.Int, itemID));
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        msg[dcount] = "Material Composition";
                        dcount++;
                    }
                }

                ds = ExecuteDataSet("dbo.SelectMasterBatchComp", CreateParameter("@ItemID", SqlDbType.Int, itemID));
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        msg[dcount] = "MasterBatch Composition";
                        dcount++;
                    }
                }

                ds = ExecuteDataSet("dbo.SelectMachinePref", CreateParameter("@ItemID", SqlDbType.Int, itemID));
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        msg[dcount] = "Machine Preference";
                        dcount++;
                    }
                }

                ds = ExecuteDataSet("dbo.SelectCustomerCosting", CreateParameter("@ItemID", SqlDbType.Int, itemID));
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        msg[dcount] = "Customer Costing";
                        dcount++;
                    }
                }

                ds = ExecuteDataSet("dbo.SelectInjectionMouldSpecification", CreateParameter("@ItemID", SqlDbType.Int, itemID));
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        msg[dcount] = "Injection Mould Specification";
                        dcount++;
                    }
                }

                ds = ExecuteDataSet("dbo.SelectQualityControl", CreateParameter("@ItemID", SqlDbType.Int, itemID));
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        msg[dcount] = "Quality Control";
                        dcount++;
                    }
                }

                ds = ExecuteDataSet("dbo.SelectPackaging", CreateParameter("@ItemID", SqlDbType.Int, itemID));
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        msg[dcount] = "Packaging";
                        dcount++;
                    }
                }

                if (dcount == 0)
                {
                    return null;
                }
                else
                {
                    msgOut = "Are you sure?  This item will also be removed from the following places:-" + Environment.NewLine;
                    for (int i = 0; i < dcount; i++)
                    {
                        msgOut += " * " + msg[i].ToString() + Environment.NewLine;
                    }
                    return msgOut;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void DeleteProductDetails(int itemID)
        {
            try
            {
                //delete item from dependent tables
                ExecuteNonQuery("dbo.DeleteInjectionMouldSpecification", CreateParameter("@ItemID", SqlDbType.Int, itemID));
                ExecuteNonQuery("dbo.DeleteMachinePref", CreateParameter("@ItemID", SqlDbType.Int, itemID));
                ExecuteNonQuery("dbo.DeleteMasterBatchComp", CreateParameter("@ItemID", SqlDbType.Int, itemID));
                ExecuteNonQuery("dbo.DeleteMaterialComp", CreateParameter("@ItemID", SqlDbType.Int, itemID));
                ExecuteNonQuery("dbo.DeleteQualityControl", CreateParameter("@ItemID", SqlDbType.Int, itemID));
                ExecuteNonQuery("dbo.DeleteQCInstruction", CreateParameter("@ItemID", SqlDbType.Int, itemID));
                ExecuteNonQuery("dbo.DeletePackaging", CreateParameter("@ItemID", SqlDbType.Int, itemID));
                ExecuteNonQuery("dbo.DeletePackingImage", CreateParameter("@ItemID", SqlDbType.Int, itemID));
                ExecuteNonQuery("dbo.DeletePackingInstruction", CreateParameter("@ItemID", SqlDbType.Int, itemID));
                ExecuteNonQuery("dbo.DeleteReworkInstruction", CreateParameter("@ItemID", SqlDbType.Int, itemID));
                ExecuteNonQuery("dbo.DeleteAssemblyInstruction", CreateParameter("@ItemID", SqlDbType.Int, itemID));
                ExecuteNonQuery("dbo.DeleteCustomerCosting", CreateParameter("@ItemID", SqlDbType.Int, itemID));
                ExecuteNonQuery("dbo.DeleteCustomerProduct", CreateParameter("@ItemID", SqlDbType.Int, itemID));
                ExecuteNonQuery("dbo.DeleteAttachedDoc", CreateParameter("@ItemID", SqlDbType.Int, itemID));

                //delete from main table
                ExecuteNonQuery("dbo.DeleteMAN_Item", CreateParameter("@ItemID", SqlDbType.Int, itemID));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void UpdateMAN_Item(DataSet ds, string optionalTableName = "default",
            string optionalRowState = "default", bool optionalAcceptChanges = false)
        {
            try
            {
                DataViewRowState dvrs;
                DataRow[] rows;

                //Process new rows:-
                if (optionalRowState == "default" || optionalRowState == "Added")
                {
                    dvrs = DataViewRowState.Added;
                    rows = (optionalTableName == "default")
                        ? ds.Tables[0].Select("", "", dvrs)
                        : ds.Tables[optionalTableName].Select("", "", dvrs);
                    for (int i = 0; i < rows.Length; i++)
                    {
                        DataRow dr = rows[i];
                        ProductDetailsDC dc = DAL.CreateItemFromRow<ProductDetailsDC>(dr);  //populate  dataclass                   
                        AddMAN_Item(dc);

                        //New primary keys are programmatically assigned a negative sequential number to
                        //allow editing of child tables.  On input to the database, a new key is re-assigned
                        //by the database and returned in an output parameter as a positive number.
                        //
                        //AddMAN_Item captures the new key and statements below update the input dataset.
                        //Any child table with this foreign key will need to be updated with the new value.
                        //
                        //The input argument optionAcceptChanges should also be set to false, to enable
                        //subsequent processing of original and new primary key values.

                        dr.BeginEdit();
                        dr["ItemID"] = dc.ItemID;
                        dr["last_updated_by"] = dc.last_updated_by;
                        dr["last_updated_on"] = dc.last_updated_on;
                        //dr.EndEdit();
                    }
                }
                if (optionalRowState == "default" || optionalRowState == "Modify/Delete")
                {
                    //process modified rows
                    dvrs = DataViewRowState.ModifiedCurrent;
                    rows = (optionalTableName == "default")
                        ? ds.Tables[0].Select("", "", dvrs)
                        : ds.Tables[optionalTableName].Select("", "", dvrs);

                    //test to verify actual changes have occurred!!!!

                    for (int i = 0; i < rows.Length; i++)
                    {
                        DataRow dr = rows[i];
                        bool hasChanged = false;
                        if (dr.HasVersion(DataRowVersion.Original))
                        {
                            for (int j = 0; j < dr.Table.Columns.Count; j++)
                            {
                                string colName = dr.Table.Columns[j].ColumnName;
                                string origValue = dr[j, DataRowVersion.Original].ToString();
                                string curValue = dr[j, DataRowVersion.Current].ToString();
                                //MessageBox.Show(i.ToString() + " " + colName + ":  Original = " + origValue + ", Current = " + curValue);
                                if (origValue.Length != 0 && curValue.Length != 0 && curValue != origValue)
                                {
                                    //MessageBox.Show(colName + ":  Original = " + ", Current = " + curValue);
                                    hasChanged = true;
                                    break;
                                }
                                else if (origValue.Length == 0 && curValue.Length != 0 )
                                {
                                    //MessageBox.Show(colName + ":  Original = " + ", Current = " + curValue);
                                    hasChanged = true;
                                    break;
                                }
                                //if (origValue.Length != 0 && curValue.Length == 0 && colName == "ItemID")
                                //{
                                //    //MessageBox.Show(colName + ":  Original = " + ", Current = " + curValue);
                                //    hasChanged = true;
                                //    dr[j] = (int)dr[j, DataRowVersion.Original];
                                //    break;
                                //}
                                //}
                            }
                            if (hasChanged)
                            {
                                ProductDetailsDC dc = DAL.CreateItemFromRow<ProductDetailsDC>(dr);  //populate  dataclass

                                UpdateMAN_Item(dc);
                                dr["last_updated_by"] = dc.last_updated_by;
                                dr["last_updated_on"] = dc.last_updated_on;
                            }
                        }

                        //dr.EndEdit();
                    }

                }

                //process deleted rows:-                
                //dvrs = DataViewRowState.Deleted;
                else if (optionalRowState == "default" || optionalRowState == "Modified/Deleted")
                {
                    dvrs = DataViewRowState.Deleted;
                    rows = (optionalTableName == "default")
                    ? ds.Tables[0].Select("", "", dvrs)
                    : ds.Tables[optionalTableName].Select("", "", dvrs);
                    for (int i = 0; i < rows.Length; i++)
                    {
                        DataRow dr = rows[i];
                        if (dr["ItemID", DataRowVersion.Original] != null)
                        {
                            ProductDetailsDC dc = new ProductDetailsDC();
                            dc.ItemID = Convert.ToInt32(dr["ItemID", DataRowVersion.Original].ToString());
                            DeleteMAN_Item(dc);
                        }
                    }
                }

                if (optionalAcceptChanges)
                {
                    ds.AcceptChanges();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
        }

        public static void AddMAN_Item(ProductDetailsDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("AddMAN_Item", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@ItemID", SqlDbType.Int, 4);
                cmd.Parameters["@ItemID"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@ItemID"].Value = dc.ItemID;
                cmd.Parameters.Add("@ITEMNMBR", SqlDbType.Char, 31);
                cmd.Parameters["@ITEMNMBR"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ITEMNMBR"].Value = dc.ITEMNMBR;
                cmd.Parameters.Add("@ITEMDESC", SqlDbType.Char, 101);
                cmd.Parameters["@ITEMDESC"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ITEMDESC"].Value = dc.ITEMDESC;
                cmd.Parameters.Add("@AltCode", SqlDbType.Char, 31);
                cmd.Parameters["@AltCode"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@AltCode"].Value = dc.AltCode;
                cmd.Parameters.Add("@ProductType", SqlDbType.Char, 31);
                cmd.Parameters["@ProductType"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ProductType"].Value = dc.ProductType;
                cmd.Parameters.Add("@GradeID", SqlDbType.Int, 4);
                cmd.Parameters["@GradeID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@GradeID"].Value = dc.GradeID;
                cmd.Parameters.Add("@ImageFile", SqlDbType.VarChar, 200);
                cmd.Parameters["@ImageFile"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ImageFile"].Value = dc.ImageFile;
                cmd.Parameters.Add("@ComponentWeight", SqlDbType.Decimal, 9);
                cmd.Parameters["@ComponentWeight"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ComponentWeight"].Value = dc.ComponentWeight;
                cmd.Parameters.Add("@SprueRunnerTotal", SqlDbType.Decimal, 9);
                cmd.Parameters["@SprueRunnerTotal"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@SprueRunnerTotal"].Value = dc.SprueRunnerTotal;
                cmd.Parameters.Add("@TotalShotWeight", SqlDbType.Decimal, 9);
                cmd.Parameters["@TotalShotWeight"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@TotalShotWeight"].Value = dc.TotalShotWeight;
                cmd.Parameters.Add("@CompDB", SqlDbType.VarChar, 5);
                cmd.Parameters["@CompDB"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CompDB"].Value = dc.CompDB;
                cmd.Parameters.Add("@ITMCLSCD", SqlDbType.Char, 11);
                cmd.Parameters["@ITMCLSCD"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ITMCLSCD"].Value = dc.ITMCLSCD;
                cmd.Parameters.Add("@CtnQty", SqlDbType.Int, 4);
                cmd.Parameters["@CtnQty"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CtnQty"].Value = dc.CtnQty;
                cmd.Parameters.Add("@CartonID", SqlDbType.Int, 4);
                cmd.Parameters["@CartonID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CartonID"].Value = dc.CartonID;
                cmd.Parameters.Add("@Comments", SqlDbType.VarChar, 200);
                cmd.Parameters["@Comments"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Comments"].Value = dc.Comments;
                cmd.Parameters.Add("@SpecificationFile", SqlDbType.VarChar, 200);
                cmd.Parameters["@SpecificationFile"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@SpecificationFile"].Value = dc.SpecificationFile;
                cmd.Parameters.Add("@LabelTypeID", SqlDbType.Int, 4);
                cmd.Parameters["@LabelTypeID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@LabelTypeID"].Value = dc.LabelTypeID;
                cmd.Parameters.Add("@BottleSize", SqlDbType.Char, 11);
                cmd.Parameters["@BottleSize"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@BottleSize"].Value = dc.BottleSize;
                cmd.Parameters.Add("@Style", SqlDbType.Char, 11);
                cmd.Parameters["@Style"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Style"].Value = dc.Style;
                cmd.Parameters.Add("@NeckSize", SqlDbType.Char, 11);
                cmd.Parameters["@NeckSize"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@NeckSize"].Value = dc.NeckSize;
                cmd.Parameters.Add("@Colour", SqlDbType.Char, 11);
                cmd.Parameters["@Colour"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Colour"].Value = dc.Colour;
                cmd.Parameters.Add("@DangerousGood", SqlDbType.Bit, 1);
                cmd.Parameters["@DangerousGood"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@DangerousGood"].Value = dc.DangerousGood;
                cmd.Parameters.Add("@StockLine", SqlDbType.Bit, 1);
                cmd.Parameters["@StockLine"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@StockLine"].Value = dc.StockLine;
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;

                cmd.ExecuteNonQuery();

                dc.ItemID = (int)cmd.Parameters["@ItemID"].Value;
                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;
                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        public static void UpdateMAN_Item(ProductDetailsDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("UpdateMAN_Item", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@ItemID", SqlDbType.Int, 4);
                cmd.Parameters["@ItemID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ItemID"].Value = dc.ItemID;
                cmd.Parameters.Add("@ITEMNMBR", SqlDbType.Char, 31);
                cmd.Parameters["@ITEMNMBR"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ITEMNMBR"].Value = dc.ITEMNMBR;
                cmd.Parameters.Add("@ITEMDESC", SqlDbType.Char, 101);
                cmd.Parameters["@ITEMDESC"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ITEMDESC"].Value = dc.ITEMDESC;
                cmd.Parameters.Add("@AltCode", SqlDbType.Char, 31);
                cmd.Parameters["@AltCode"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@AltCode"].Value = dc.AltCode;
                cmd.Parameters.Add("@ProductType", SqlDbType.Char, 31);
                cmd.Parameters["@ProductType"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ProductType"].Value = dc.ProductType;
                cmd.Parameters.Add("@GradeID", SqlDbType.Int, 4);
                cmd.Parameters["@GradeID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@GradeID"].Value = dc.GradeID;
                cmd.Parameters.Add("@ImageFile", SqlDbType.VarChar, 200);
                cmd.Parameters["@ImageFile"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ImageFile"].Value = dc.ImageFile;
                cmd.Parameters.Add("@ComponentWeight", SqlDbType.Decimal, 9);
                cmd.Parameters["@ComponentWeight"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ComponentWeight"].Value = dc.ComponentWeight;
                cmd.Parameters.Add("@SprueRunnerTotal", SqlDbType.Decimal, 9);
                cmd.Parameters["@SprueRunnerTotal"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@SprueRunnerTotal"].Value = dc.SprueRunnerTotal;
                cmd.Parameters.Add("@TotalShotWeight", SqlDbType.Decimal, 9);
                cmd.Parameters["@TotalShotWeight"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@TotalShotWeight"].Value = dc.TotalShotWeight;
                cmd.Parameters.Add("@CompDB", SqlDbType.VarChar, 5);
                cmd.Parameters["@CompDB"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CompDB"].Value = dc.CompDB;
                cmd.Parameters.Add("@ITMCLSCD", SqlDbType.Char, 11);
                cmd.Parameters["@ITMCLSCD"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ITMCLSCD"].Value = dc.ITMCLSCD;
                cmd.Parameters.Add("@CtnQty", SqlDbType.Int, 4);
                cmd.Parameters["@CtnQty"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CtnQty"].Value = dc.CtnQty;
                cmd.Parameters.Add("@CartonID", SqlDbType.Int, 4);
                cmd.Parameters["@CartonID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CartonID"].Value = dc.CartonID;
                cmd.Parameters.Add("@Comments", SqlDbType.VarChar, 200);
                cmd.Parameters["@Comments"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Comments"].Value = dc.Comments;
                cmd.Parameters.Add("@SpecificationFile", SqlDbType.VarChar, 200);
                cmd.Parameters["@SpecificationFile"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@SpecificationFile"].Value = dc.SpecificationFile;
                cmd.Parameters.Add("@LabelTypeID", SqlDbType.Int, 4);
                cmd.Parameters["@LabelTypeID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@LabelTypeID"].Value = dc.LabelTypeID;
                cmd.Parameters.Add("@BottleSize", SqlDbType.Char, 11);
                cmd.Parameters["@BottleSize"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@BottleSize"].Value = dc.BottleSize;
                cmd.Parameters.Add("@Style", SqlDbType.Char, 11);
                cmd.Parameters["@Style"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Style"].Value = dc.Style;
                cmd.Parameters.Add("@NeckSize", SqlDbType.Char, 11);
                cmd.Parameters["@NeckSize"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@NeckSize"].Value = dc.NeckSize;
                cmd.Parameters.Add("@Colour", SqlDbType.Char, 11);
                cmd.Parameters["@Colour"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Colour"].Value = dc.Colour;
                cmd.Parameters.Add("@DangerousGood", SqlDbType.Bit, 1);
                cmd.Parameters["@DangerousGood"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@DangerousGood"].Value = dc.DangerousGood;
                cmd.Parameters.Add("@StockLine", SqlDbType.Bit, 1);
                cmd.Parameters["@StockLine"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@StockLine"].Value = dc.StockLine;
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;

                cmd.ExecuteNonQuery();

                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;
                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }


        public static void DeleteMAN_Item(ProductDetailsDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("DeleteMAN_Item", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@ItemID", SqlDbType.Int, 4);
                cmd.Parameters["@ItemID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ItemID"].Value = dc.ItemID;

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
