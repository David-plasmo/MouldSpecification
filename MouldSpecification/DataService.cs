using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Reflection;
using MouldSpecification;
using System.Collections.Generic;


namespace DataService
{

    public class ProductDataService : DataAccessBase
    {
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///	Creates a new DataService
        /// </summary>
        ////////////////////////////////////////////////////////////////////////
        public ProductDataService() : base() { }

        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///	Creates a new DataService and specifies a transaction with
        ///	which to operate
        /// </summary>
        ////////////////////////////////////////////////////////////////////////
        public ProductDataService(IDbTransaction txn) : base(txn) { }
        public IMSpecificationData LastAdded;
        public DataSet GetCPPriceQty()
        {
            try
            {
                DataSet ds = ExecuteDataSet("[PlasmoIntegration].[dbo].[SelectCPPriceQuantity]");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        public DataSet GetIMSpecificationGridSettings()
        {
            try
            {
                DataSet ds = ExecuteDataSet("GetIMSpecificationGridSettings");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        public DataSet SelectIMSpecification()
        {
            try
            {
                DataSet ds = ExecuteDataSet("SelectIMSpecification");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        public DataSet SelectCPCustomerDropdown()
        {
            try
            {
                DataSet ds = ExecuteDataSet("PlasmoIntegration.dbo.SelectCPCustomerDropdown");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        public DataSet SelectCPProductDropdown(string companyCode)
        {
            try
            {
                DataSet ds = ExecuteDataSet("PlasmoIntegration.dbo.SelectCPProductDropdown",
                    CreateParameter("@CompanyCode", SqlDbType.VarChar, companyCode));
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        public DataSet ReportIMSpecification(int imID)
        {
            try
            {
                DataSet ds = ExecuteDataSet("SelectIMSpecificationReport",
                CreateParameter("@ImID", SqlDbType.Int, imID));
                return ds; ;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
    }

        //create print queue for Blow Moulded products
        public DataSet GetBMSetupGridSettings()
        {
            try
            {
                DataSet ds = ExecuteDataSet("GetBMSetupGridSettings");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        public DataSet SelectBMSetup()
        {
            try
            {
                DataSet ds = ExecuteDataSet("SelectBMSetup");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        //public DataSet GetIMProductDropDownList(string companyCode, string custNmbr)
        //{
        //    try
        //    {
        //        DataSet ds = ExecuteDataSet("GetIMProductDropDownList",
        //            CreateParameter("@CompanyCode", SqlDbType.VarChar, companyCode),
        //            CreateParameter("@CUSTNMBR", SqlDbType.VarChar, custNmbr));
        //        return ds;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //        return null;
        //    }
        //}

        
        public DataSet GetReportPromptDropdown()
        {
            try
            {
                DataSet ds = ExecuteDataSet("PlasmoIntegration.dbo.GetReportPromptDropdown");
                    //CreateParameter("@ImID", SqlDbType.Int, imID));
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        //GetCustomerIndexByCompany
        public DataSet GetCustomerIndexByCompany(string companyCode)
        {
            try
            {
                DataSet ds = ExecuteDataSet("PlasmoIntegration.dbo.GetCustomerIndexByCompany",
                    CreateParameter("@CompanyCode", SqlDbType.VarChar, companyCode));
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
                
        public DataSet GetCompany()
        {
            try
            {
                DataSet ds = ExecuteDataSet("PlasmoIntegration.dbo.GetCompany");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public DataSet GetMaterialAndGrade()
        {
            try
            {
                DataSet ds = ExecuteDataSet("[PlasmoIntegration].[dbo].[GetMaterialAndGrade]");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public DataSet GetMaterialGradeRef()
        {
            try
            {
                DataSet ds = ExecuteDataSet("GetMaterialGradeRef");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public DataSet GetMasterBatchRef()
        {
            try
            {
                DataSet ds = ExecuteDataSet("GetMasterBatchRef");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public DataSet GetMachine(string mType)
        {
            try
            {
                DataSet ds = ExecuteDataSet("GetMachine",
                    CreateParameter("@MachineType", SqlDbType.VarChar, mType));
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public DataSet GetGPCustomerIndex()
        {
            try
            {
                DataSet ds = ExecuteDataSet("GetGPCustomerIndex");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public DataSet GetPallet()
        {
            try
            {
                DataSet ds = ExecuteDataSet("GetPallet");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public DataSet GetAdditiveType()
        {
            try
            {
                DataSet ds = ExecuteDataSet("GetAdditiveType");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public DataSet AdditiveDropDown()
        {
            try
            {
                DataSet ds = ExecuteDataSet("AdditiveDropdown");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public DataSet GetAdditive()
        {
            try
            {
                DataSet ds = ExecuteDataSet("GetAdditive");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public DataSet GetMaterial()
        {
            try
            {
                DataSet ds = ExecuteDataSet("[PlasmoIntegration].[dbo].[GetMaterial]");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public DataSet GetPastelCategory()
        {
            try
            {
                return ExecuteDataSet("BarTender.dbo.GetPastelCategory");
            }
            catch
            {
                throw;
            }
        }
        public DataSet GetPastelProductIndex()
        {
            try
            {
                return ExecuteDataSet("BarTender.dbo.GetPastelProductIndex");
            }
            catch
            {
                throw;
            }
        }
        public DataSet GetProductGrade()
        {
            try
            {
                DataSet ds = ExecuteDataSet("[PlasmoIntegration].[dbo].[GetProductGrade]");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public DataSet MasterBatchDropdown()
        {
            try
            {
                DataSet ds = ExecuteDataSet("MasterBatchDropdown");
                return ds;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public DataSet GetMBColour()
        {
            try
            {
                DataSet ds = ExecuteDataSet("GetMBColour");
                return ds;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataSet GetMBCode(string mbColour)
        {
            try
            {
                return ExecuteDataSet("GetMBCode", CreateParameter("@MBColour", SqlDbType.VarChar, mbColour));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        //public DataSet GetMaterialGrade(int? materialID)
        //{
        //    try
        //    {
        //        return ExecuteDataSet("GetMaterialGrade", CreateParameter("@MaterialID", SqlDbType.Int, materialID));
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return null;
        //    }
        //}
        public DataSet MaterialAndGradeDropdown()
        {
            try
            {
                return ExecuteDataSet("MaterialAndGradeDropdown");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public DataSet GetPalletRef()
        {
            try
            {
                return ExecuteDataSet("GetPalletRef");
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
                DataSet ds = ExecuteDataSet("GetLabelTypes");
                //ds.Tables[0].Columns["Code"].DefaultValue = defaultCode;
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }

        }
        public DataSet GetPastelMaster()
        {
            try
            {
                DataSet ds = ExecuteDataSet("BarTender.dbo.GetCPMasterListExport");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        public DataSet GetProductGradeRef()
        {
            try
            {
                return ExecuteDataSet("[PlasmoIntegration].[dbo].[GetProductGradeRef]");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataSet GetCartonPackagingDropdown()
        {
            try
            {
                return ExecuteDataSet("[PlasmoIntegration].[dbo].[GetCartonPackagingDropdown]");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public DataSet GetCartonSize()
        {
            try
            {
                return ExecuteDataSet("GetCartonSizes");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataSet GetIMSpecificationCost(int imID) //, string custNumbr)
        {
            try
            {
                return ExecuteDataSet("GetIMSpecificationCost",
                    CreateParameter("@ImID", SqlDbType.Int, imID)); //,
                    //CreateParameter("@PRCLEVEL", SqlDbType.VarChar, custNumbr));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public DataSet ReportPriceDifference()
        {
            try
            {
                return ExecuteDataSet("ReportPriceDifference");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        
        public DataSet GetCartonPackaging()
        {
            try
            {
                return ExecuteDataSet("[PlasmoIntegration].[dbo].[GetCartonPackaging]");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void UpdateIMSpecification(DataSet ds)
        {
            try
            {

                //Process new rows:-
                DataViewRowState dvrs = DataViewRowState.Added;
                DataRow[] rows = ds.Tables[0].Select("", "", dvrs);

                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    IMSpecificationData dc = DAL.CreateItemFromRow<IMSpecificationData>(dr);  //populate dataclass
                    dc.last_updated_on = DateTime.MinValue;
                    dc.last_updated_by = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    //InsertBlowMouldMachineSetup(dc);  // run insert stored procedure
                    IMSpecificationDAL.InsertInjectionMouldingSpecification(dc);
                    dr["last_updated_on"] = dc.last_updated_on;
                    dr["last_updated_by"] = dc.last_updated_by;
                    dr["ImID"] = dc.ImID;
                    this.LastAdded = dc;
                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    IMSpecificationData dc = DAL.CreateItemFromRow<IMSpecificationData>(dr);  //populate BMSetupData dataclass
                    IMSpecificationDAL.UpdateInjectionMouldingSpecification(dc);
                    dr["last_updated_on"] = dc.last_updated_on;
                    dr["last_updated_by"] = dc.last_updated_by;
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    if (dr["ImID", DataRowVersion.Original] != null)
                    {
                        ExecuteNonQuery("DeleteInjectionMouldingSpecification",
                          CreateParameter("@ImID", SqlDbType.Int, Convert.ToInt32(dr["ImID", DataRowVersion.Original].ToString())));
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
   
        public void UpdateBMSetup(DataSet ds)
        {
            try
            {

                //Process new rows:-
                DataViewRowState dvrs = DataViewRowState.Added;
                DataRow[] rows = ds.Tables[0].Select("", "", dvrs);
               
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];                                        
                    BMSetupData dc = DAL.CreateItemFromRow<BMSetupData>(dr);  //populate BMSetupData dataclass
                    dc.last_updated_on = DateTime.MinValue;
                    dc.last_updated_by = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    //InsertBlowMouldMachineSetup(dc);  // run insert stored procedure
                    BMSetupDAL.InsertBlowMouldMachineSetup(dc);
                    dr["last_updated_on"] = dc.last_updated_on;
                    dr["last_updated_by"] = dc.last_updated_by;
                    dr["SetupID"] = dc.SetupID;
                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    BMSetupData dc = DAL.CreateItemFromRow<BMSetupData>(dr);  //populate BMSetupData dataclass
                    BMSetupDAL.UpdateBlowMouldMachineSetup(dc);
                    dr["last_updated_on"] = dc.last_updated_on;
                    dr["last_updated_by"] = dc.last_updated_by;
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];                    
                    if (dr["SetupID", DataRowVersion.Original] != null)
                    {
                        ExecuteNonQuery("DeleteBlowMouldMachineSetup",
                          CreateParameter("@SetupID", SqlDbType.Int, Convert.ToInt32(dr["SetupID", DataRowVersion.Original].ToString())));
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
        public void UpdateCPPriceQty(DataSet ds)
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
                    AdHocPriceQtyData dc = DAL.CreateItemFromRow<AdHocPriceQtyData>(dr);  //populate AdHocPriceQtyData dataclass
                    dc.last_updated_on = DateTime.MinValue;
                    dc.last_updated_by = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    //dc.last_updated_on = DateTime.MinValue;
                    //dc = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    //InsertBlowMouldMachineSetup(dc);  // run insert stored procedure
                    //dr["last_updated_on"] = dc.last_updated_on;
                    //dr["last_updated_by"] = dc.last_updated_by;
                    AdHocPriceQtyDAL.InsertAdHocPriceQty(dc);
                    dr["AHPID"] = dc.AHPID;
                    dr["last_updated_on"] = dc.last_updated_on;
                    dr["last_updated_by"] = dc.last_updated_by;
                    
                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    AdHocPriceQtyData dc = DAL.CreateItemFromRow<AdHocPriceQtyData>(dr);  //populate BMSetupData dataclass
                    AdHocPriceQtyDAL.UpdateAdHocPriceQty(dc);
                    //dr["last_updated_on"] = dc.last_updated_on;
                    //dr["last_updated_by"] = dc.last_updated_by;
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    //AdHocPriceQtyDAL dc = DAL.CreateItemFromRow<AdHocPriceQtyDAL>(dr);  //populate BMSetupData dataclass
                    //MachineDAL.DeleteMachine(dc);
                    if (dr["AHPID", DataRowVersion.Original] != null)
                    {
                        ExecuteNonQuery("PlasmoIntegration.dbo.DeleteAdHocPriceQty",
                          CreateParameter("@AHPID", SqlDbType.Int, Convert.ToInt32(dr["AHPID", DataRowVersion.Original].ToString())));
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
    
        public void UpdateMachine(DataSet ds)
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
                    MachineData dc = DAL.CreateItemFromRow<MachineData>(dr);  //populate BMSetupData dataclass
                    //dc.last_updated_on = DateTime.MinValue;
                    dc.last_updated_by = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    //InsertBlowMouldMachineSetup(dc);  // run insert stored procedure
                    //dr["last_updated_on"] = dc.last_updated_on;
                    //dr["last_updated_by"] = dc.last_updated_by;
                    MachineDAL.InsertMachine(dc);
                    dr["MachineID"] = dc.MachineID;
                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    MachineData dc = DAL.CreateItemFromRow<MachineData>(dr);  //populate BMSetupData dataclass
                    MachineDAL.UpdateMachine(dc);
                    //dr["last_updated_on"] = dc.last_updated_on;
                    //dr["last_updated_by"] = dc.last_updated_by;
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    //MachineData dc = DAL.CreateItemFromRow<MachineData>(dr);  //populate BMSetupData dataclass
                    //MachineDAL.DeleteMachine(dc);
                    if (dr["MachineID", DataRowVersion.Original] != null)
                    {
                        ExecuteNonQuery("DeleteMachine",
                          CreateParameter("@MachineID", SqlDbType.Int, Convert.ToInt32(dr["MachineID", DataRowVersion.Original].ToString())));
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

        public void UpdateMaterial(DataSet ds)
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
                    MaterialData dc = DAL.CreateItemFromRow<MaterialData>(dr);  //populate dataclass     
                    dc.last_updated_on = DateTime.MinValue;
                    dc.last_updated_by = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    MaterialDAL.InsertMaterial(dc);
                    dr["MaterialID"] = dc.MaterialID;
                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    MaterialData dc = DAL.CreateItemFromRow<MaterialData>(dr);  //populate BMSetupData dataclass                   
                    MaterialDAL.UpdateMaterial(dc);
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];                   
                    if (dr["MaterialID", DataRowVersion.Original] != null)
                    {
                        ExecuteNonQuery("PlasmoIntegration.dbo.DeleteMaterial",
                          CreateParameter("@MaterialID", SqlDbType.Int, Convert.ToInt32(dr["MaterialID", DataRowVersion.Original].ToString())));
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

        public void UpdateMaterialGrade(DataSet ds)
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
                    MaterialGradeData dc = DAL.CreateItemFromRow<MaterialGradeData>(dr);  //populate  dataclass 
                    dc.last_updated_on = DateTime.MinValue;
                    dc.last_updated_by = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    MaterialGradeDAL.InsertMaterialGrade(dc);
                    dr["MaterialGradeID"] = dc.MaterialGradeID;
                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    MaterialGradeData dc = DAL.CreateItemFromRow<MaterialGradeData>(dr);  //populate BMSetupData dataclass                   
                    MaterialGradeDAL.UpdateMaterialGrade(dc);
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    if (dr["MaterialID", DataRowVersion.Original] != null)
                    {
                        ExecuteNonQuery("DeleteMaterialGrade",
                          CreateParameter("@MaterialGradeID", SqlDbType.Int, Convert.ToInt32(dr["MaterialGradeID", DataRowVersion.Original].ToString())));
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

        public void UpdateMasterBatch(DataSet ds)
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
                    MasterBatchData dc = DAL.CreateItemFromRow<MasterBatchData>(dr);  //populate  dataclass 
                    dc.last_updated_on = DateTime.MinValue;
                    dc.last_updated_by = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    MasterBatchDAL.InsertMasterBatch(dc);
                    dr["MBID"] = dc.MBID;
                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    MasterBatchData dc = DAL.CreateItemFromRow<MasterBatchData>(dr);  //populate BMSetupData dataclass                   
                    MasterBatchDAL.UpdateMasterBatch(dc);
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    if (dr["MBID", DataRowVersion.Original] != null)
                    {
                        ExecuteNonQuery("DeleteMasterBatch",
                          CreateParameter("@MBID", SqlDbType.Int, Convert.ToInt32(dr["MBID", DataRowVersion.Original].ToString())));
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

        public void UpdatePallet(DataSet ds)
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
                    PalletData dc = DAL.CreateItemFromRow<PalletData>(dr);  //populate  dataclass                   
                    PalletDAL.InsertPallet(dc);
                   
                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent ;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    PalletData dc = DAL.CreateItemFromRow<PalletData>(dr);  //populate  dataclass                   
                    PalletDAL.UpdatePallet(dc);
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    if (dr["PalletID", DataRowVersion.Original] != null)
                    {
                        ExecuteNonQuery("DeletePallet",
                          CreateParameter("@PalletID", SqlDbType.Int, Convert.ToInt32(dr["PalletID", DataRowVersion.Original].ToString())));
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
        public void UpdateProductGrade(DataSet ds)
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
                    ProductGradeData dc = DAL.CreateItemFromRow<ProductGradeData>(dr);  //populate  dataclass                   
                    ProductGradeDAL.InsertProductGrade(dc);

                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    ProductGradeData dc = DAL.CreateItemFromRow<ProductGradeData>(dr);  //populate  dataclass                   
                    ProductGradeDAL.UpdateProductGrade(dc);
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    if (dr["GradeID", DataRowVersion.Original] != null)
                    {
                        ExecuteNonQuery("[PlasmoIntegration].[dbo].[DeleteProductGrade]",
                          CreateParameter("@GradeID", SqlDbType.Int, Convert.ToInt32(dr["GradeID", DataRowVersion.Original].ToString())));
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
        public void UpdateAdditive(DataSet ds)
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
                    AdditiveData dc = DAL.CreateItemFromRow<AdditiveData>(dr);  //populate  dataclass                   
                    AdditiveDAL.InsertAdditive(dc);

                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    AdditiveData dc = DAL.CreateItemFromRow<AdditiveData>(dr);  //populate  dataclass                   
                    AdditiveDAL.UpdateAdditive(dc);
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    if (dr["ADDID", DataRowVersion.Original] != null)
                    {
                        ExecuteNonQuery("[DeleteAdditive]",
                          CreateParameter("@ADDID", SqlDbType.Int, Convert.ToInt32(dr["ADDID", DataRowVersion.Original].ToString())));
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
        public void UpdateCartonPackaging(DataSet ds)
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
                    CartonPackagingData dc = DAL.CreateItemFromRow<CartonPackagingData>(dr);  //populate  dataclass 
                    dc.last_updated_on = DateTime.MinValue;
                    dc.last_updated_by = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    CartonPackagingDAL.InsertCartonPackaging(dc);
                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];                    
                    CartonPackagingData dc = DAL.CreateItemFromRow<CartonPackagingData>(dr);  //populate  dataclass                           
                    CartonPackagingDAL.UpdateCartonPackaging(dc);
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    if (dr["CtnID", DataRowVersion.Original] != null)
                    {
                        ExecuteNonQuery("[PlasmoIntegration].[dbo].[DeleteCartonPackaging]",
                          CreateParameter("@CtnID", SqlDbType.Int, Convert.ToInt32(dr["CtnID", DataRowVersion.Original].ToString())));
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
        public void UpdatePastelMaster(DataSet ds)
        {
            try
            {

                //Process new rows:-
                ///CREATE PROCEDURE [dbo].[AddCPMasterListExport](@Code char(31), @Description varchar(101), 
                //@CategoryID int, @CtnQty int, @CtnSize varchar(11), @Grade char(2), @last_updated_on datetime2(7) output,
                //@last_updated_by varchar(50) output, @PastelID int output)

                DataViewRowState dvrs = DataViewRowState.Added;
                DataRow[] rows = ds.Tables[0].Select("", "", dvrs);
                int newPastelID = 0;
                string newLastUpdatedBy = "last_updated_by";
                DateTime newLastUpdatedOn = DateTime.MinValue;
                DateTime date = DateTime.MinValue;

                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];

                    ExecuteNonQuery("[PlasmoIntegration].[dbo].[AddCPMasterListExport]",
                        CreateParameter("@Code", SqlDbType.VarChar, dr["Code"].ToString()),
                        CreateParameter("@Description", SqlDbType.VarChar, dr["Description"].ToString()),
                        CreateParameter("@CategoryID", SqlDbType.Int, Convert.ToInt32(dr["CategoryID"].ToString())),
                        CreateParameter("@CtnQty", SqlDbType.Int, Convert.ToInt32(dr["CtnQty"].ToString())),                        
                        CreateParameter("@CtnSize", SqlDbType.VarChar, dr["CtnSize"].ToString()),
                        CreateParameter("@Grade", SqlDbType.VarChar, dr["Grade"].ToString()),
                        CreateParameter("@MaterialID", SqlDbType.Int, Convert.ToInt32(dr["MaterialID"].ToString())),
                        CreateParameter("@last_updated_on", SqlDbType.DateTime2, date, ParameterDirection.Output),
                        CreateParameter("@last_updated_by", SqlDbType.VarChar, newLastUpdatedBy, ParameterDirection.Output),
                        CreateParameter("@PastelID", SqlDbType.Int, newPastelID, ParameterDirection.Output));

                    if (DateTime.TryParse(this.DABCmd.Parameters["@last_updated_on"].Value.ToString(), out date))
                    {
                        newLastUpdatedOn = date;
                        dr["last_updated_on"] = newLastUpdatedOn;
                    }

                    newLastUpdatedBy = this.DABCmd.Parameters["@last_updated_by"].Value.ToString();
                    newPastelID = (int)this.DABCmd.Parameters["@PastelID"].Value;

                    dr["last_updated_by"] = newLastUpdatedBy;
                    dr["PastelID"] = newPastelID;
                }


                //Process modified rows:-
                //CREATE PROCEDURE [dbo].[UpdateCPMasterListExport](@PastelID int, @Code char(31), @Description varchar(101), 
                //@CategoryID int, @CtnQty int, @CtnSize varchar(11), @Grade char(2), @last_updated_on datetime2(7) output,
                //@last_updated_by varchar(50) output)

                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);

                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    date = Convert.ToDateTime(dr["last_updated_on"]);

                    ExecuteNonQuery("[PlasmoIntegration].[dbo].[UpdateCPMasterListExport]",
                        CreateParameter("@PastelID", SqlDbType.Int, Convert.ToInt32(dr["PastelID"].ToString())),
                        CreateParameter("@Code", SqlDbType.VarChar, dr["Code"].ToString()),
                        CreateParameter("@Description", SqlDbType.VarChar, dr["Description"].ToString()),
                        CreateParameter("@CategoryID", SqlDbType.Int, Convert.ToInt32(dr["CategoryID"].ToString())),
                        CreateParameter("@CtnQty", SqlDbType.Int, Convert.ToInt32(dr["CtnQty"].ToString())),                        
                        CreateParameter("@CtnSize", SqlDbType.VarChar, dr["CtnSize"].ToString()),
                        CreateParameter("@Grade", SqlDbType.VarChar, dr["Grade"].ToString()),
                        CreateParameter("@MaterialID", SqlDbType.Int, Convert.ToInt32(dr["MaterialID"].ToString())),
                        CreateParameter("@last_updated_on", SqlDbType.DateTime2, date, ParameterDirection.InputOutput),
                        CreateParameter("@last_updated_by", SqlDbType.VarChar, newLastUpdatedBy, ParameterDirection.InputOutput));

                    newLastUpdatedOn = (DateTime)this.DABCmd.Parameters["@last_updated_on"].Value;
                    newLastUpdatedBy = (string)this.DABCmd.Parameters["@last_updated_by"].Value;
                    dr["last_updated_on"] = newLastUpdatedOn;
                    dr["last_updated_by"] = newLastUpdatedBy;
                }


                //process deleted rows:-
                //CREATE PROCEDURE [dbo].[DeleteCPMasterListExport]( @PastelID int)	
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    // Console.WriteLine(dr["Run", DataRowVersion.Original].ToString());
                    if (dr["PastelID", DataRowVersion.Original] != null)
                    {
                        ExecuteNonQuery("[PlasmoIntegration].[dbo].[DeleteCPMasterListExport]",
                          CreateParameter("@PastelID", SqlDbType.Int, Convert.ToInt32(dr["PastelID", DataRowVersion.Original].ToString())));
                    }

                }

                ds.AcceptChanges();


            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                throw;
            }
        }

    }
}

