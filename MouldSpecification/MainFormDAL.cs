using DataService;
using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace MouldSpecification
{
    public class MainFormDAL : DataAccessBase
    {
        public int RowsAdded { get; set; }
        public int RowsUpdated { get; set; }

        public void UpdateAccessWorkTables()
        {
            try
            {
                //Connect to Access work database
                var appSettings = ConfigurationManager.AppSettings;
                string relativePath = appSettings["MSAccessWork"];
                string directory = AppDomain.CurrentDomain.BaseDirectory;
                // add data folder and access filename here ...
                string accessWorkPath = @directory + @relativePath;
                string conStr = ConfigurationManager.ConnectionStrings["AccessWork"].ConnectionString;

                //substitute access workpath into connection string
                conStr = conStr.Replace("{MSAccessWork}", accessWorkPath);

                OleDbConnection conDB = new OleDbConnection(conStr);
                conDB.Open();
                string sqlDrop = "DROP TABLE [Injection Moulding Specifications]";
                OleDbCommand cmd = new OleDbCommand(sqlDrop, conDB);

                //Attemp delete and ignore error if not existing
                try { cmd.ExecuteNonQuery(); } catch { }

                conDB.Close();

                //Connect to production Access Injection Mould Specifications database and insert production table into work table
                conStr = ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
                conDB = new OleDbConnection(conStr);
                conDB.Open();

                //string sqlScriptPath = appSettings["ScriptsFolder"] + "AccessMakeTable.sql";
                relativePath = appSettings["MSAccessScript"];
                string sqlScriptPath = @directory + @relativePath;
                string sqlQry = File.ReadAllText(@sqlScriptPath);
                sqlQry = sqlQry.Replace("<InputPathToWork.accdb>", accessWorkPath);

                cmd = new OleDbCommand(sqlQry, conDB);
                cmd.ExecuteNonQuery();

                conDB.Close();
                conDB.Dispose();
                cmd.Dispose();

                //code below doesnt work.  Try executing in Access vba through command line 
                ////clear the attached sqlServer table in work
                //sqlQry = "DELETE * FROM [dbo_Injection Moulding Specifications];";
                //cmd = new OleDbCommand(sqlQry, conDB);
                //cmd.ExecuteNonQuery();

                ////append to attached sqlServer table in work
                //sqlQry = "INSERT INTO [dbo_Injection Moulding Specifications] " +
                //    "SELECT [Injection Moulding Specifications].* " +
                //    "FROM [Injection Moulding Specifications];";
                //cmd = new OleDbCommand(sqlQry, conDB);
                //cmd.ExecuteNonQuery();


                //Copy the Access work table to a linked table in SQLServer 
                ProcessStartInfo info = new ProcessStartInfo("msaccess.exe");
                string cmdArguments = accessWorkPath + " /cmd IM";
                info.Arguments = @cmdArguments;
                info.WindowStyle = ProcessWindowStyle.Minimized;
                Process proc = Process.Start(info);
                proc.WaitForExit();
                proc.Dispose();

                ////Connect to SQLServer and run dtsx to import Access table into
                ////Injection Moulding sqlServer table, with same name, columns and compatible datatypes.
                //ExecuteNonQuery("AccessImport.dbo.RunDTSXPackage",
                //    CreateParameter("@DtsxPath",SqlDbType.VarChar, acessDtsxPath),
                //    CreateParameter("@SpecType", SqlDbType.VarChar, "IM"));

                ////process imported table into the SQL Server Injection Mould Specification data model
                ////ExecuteNonQuery("ImportMAN_Item");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateAttachmentFilepaths()
        {
            try
            {
                //get image filepaths
                var appSettings = ConfigurationManager.AppSettings;
                string imageFolderDir = appSettings["QCImageFolderDir"];
                string[] filePaths = Directory.GetFiles(@imageFolderDir, "*.*",
                    SearchOption.AllDirectories);

                //process image filepaths into a DataTable
                System.Data.DataTable dt = new System.Data.DataTable("ImageFilePath");
                dt.Columns.Add(new DataColumn("FilePath", typeof(string)));
                foreach (string fp in filePaths)
                {
                    DataRow dr = dt.NewRow();
                    dr["FilePath"] = fp;
                    dt.Rows.Add(dr);
                }

                //get other image filepaths  OtherImages
                string otherImageFolderDir = appSettings["OtherImages"];
                filePaths = Directory.GetFiles(@otherImageFolderDir, "*.*",
                    SearchOption.AllDirectories);

                //add to DataTable
                foreach (string fp in filePaths)
                {
                    DataRow dr = dt.NewRow();
                    dr["FilePath"] = fp;
                    dt.Rows.Add(dr);
                }

                //get attachment filepaths
                string attachmentFolderDir = appSettings["QCSpecialInstructionDir"];
                filePaths = Directory.GetFiles(@attachmentFolderDir, "*.*",
                    SearchOption.AllDirectories);

                //add to DataTable
                foreach (string fp in filePaths)
                {
                    DataRow dr = dt.NewRow();
                    dr["FilePath"] = fp;
                    dt.Rows.Add(dr);
                }

                //clear current image filepath table on server
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("dbo.TruncateImageFilePaths", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();


                //copy new DataTable to server
                using (var bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = "dbo.ImageFilePath";
                    bulkCopy.WriteToServer(dt);
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error from UpdateAttachmentFilepaths: ", ex);
            }
        }

        public void UpdateFromAccessImport()
        {
            try
            {

                int rowsAdded = 0;
                int rowsUpdated = 0;
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();

                cmd = new System.Data.SqlClient.SqlCommand("UpdateFromAccessImport", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@RowsAdded", SqlDbType.Int, 4);
                cmd.Parameters["@RowsAdded"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@RowsAdded"].Value = rowsAdded;

                cmd.Parameters.Add("@RowsUpdated", SqlDbType.Int, 4);
                cmd.Parameters["@RowsUpdated"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@RowsUpdated"].Value = rowsUpdated;

                cmd.ExecuteNonQuery();

                RowsAdded = (int)cmd.Parameters["@RowsAdded"].Value;
                RowsUpdated = (int)cmd.Parameters["@RowsUpdated"].Value;

                connection.Close();

            }
            catch (Exception ex) { throw new Exception("Error from UpdateFromAccessImport: ", ex); }
        }

        public void ImportMasterBatchComp()
        {
            try
            {
                //ExecuteNonQuery("dbo.ImportMasterBatchComp");

                int rowsAdded = 0;
                int rowsUpdated = 0;
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();

                cmd = new System.Data.SqlClient.SqlCommand("dbo.ImportMasterBatchComp", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@RowsAdded", SqlDbType.Int, 4);
                cmd.Parameters["@RowsAdded"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@RowsAdded"].Value = rowsAdded;

                cmd.Parameters.Add("@RowsUpdated", SqlDbType.Int, 4);
                cmd.Parameters["@RowsUpdated"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@RowsUpdated"].Value = rowsUpdated;

                cmd.ExecuteNonQuery();

                RowsAdded = (int)cmd.Parameters["@RowsAdded"].Value;
                RowsUpdated = (int)cmd.Parameters["@RowsUpdated"].Value;

                connection.Close();
            }
            catch (Exception ex) { throw new Exception("Error from ImportMasterBatchComp:  ", ex); }
        }

        public void ImportMachinePref()
        {
            try
            {
                //ExecuteNonQuery("dbo.ImportMachinePref"); 

                int rowsAdded = 0;
                int rowsUpdated = 0;
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();

                cmd = new System.Data.SqlClient.SqlCommand("dbo.ImportMachinePref", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@RowsAdded", SqlDbType.Int, 4);
                cmd.Parameters["@RowsAdded"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@RowsAdded"].Value = rowsAdded;

                cmd.Parameters.Add("@RowsUpdated", SqlDbType.Int, 4);
                cmd.Parameters["@RowsUpdated"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@RowsUpdated"].Value = rowsUpdated;

                cmd.ExecuteNonQuery();

                RowsAdded = (int)cmd.Parameters["@RowsAdded"].Value;
                RowsUpdated = (int)cmd.Parameters["@RowsUpdated"].Value;

                connection.Close();

            }
            catch (Exception ex) { throw new Exception("Error from ImportMachinePref:  ", ex); }
        }

        public void ImportMaterialComp()
        {
            try
            {
                //ExecuteNonQuery("dbo.ImportMaterialComp"); 

                int rowsAdded = 0;
                int rowsUpdated = 0;
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();

                cmd = new System.Data.SqlClient.SqlCommand("dbo.ImportMaterialComp", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@RowsAdded", SqlDbType.Int, 4);
                cmd.Parameters["@RowsAdded"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@RowsAdded"].Value = rowsAdded;

                cmd.Parameters.Add("@RowsUpdated", SqlDbType.Int, 4);
                cmd.Parameters["@RowsUpdated"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@RowsUpdated"].Value = rowsUpdated;

                cmd.ExecuteNonQuery();

                RowsAdded = (int)cmd.Parameters["@RowsAdded"].Value;
                RowsUpdated = (int)cmd.Parameters["@RowsUpdated"].Value;

                connection.Close();


            }
            catch (Exception ex) { throw new Exception("Error from ImportMaterialComp:  ", ex); }
        }

        public void ImportCustomer()
        {
            try
            {
                //ExecuteNonQuery("dbo.ImportCustomer");

                int rowsAdded = 0;
                int rowsUpdated = 0;
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();

                cmd = new System.Data.SqlClient.SqlCommand("dbo.ImportCustomer", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@RowsAdded", SqlDbType.Int, 4);
                cmd.Parameters["@RowsAdded"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@RowsAdded"].Value = rowsAdded;

                cmd.Parameters.Add("@RowsUpdated", SqlDbType.Int, 4);
                cmd.Parameters["@RowsUpdated"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@RowsUpdated"].Value = rowsUpdated;

                cmd.ExecuteNonQuery();

                RowsAdded = (int)cmd.Parameters["@RowsAdded"].Value;
                RowsUpdated = (int)cmd.Parameters["@RowsUpdated"].Value;

                connection.Close();
            }
            catch (Exception ex) { throw new Exception("Error from ImportCustomer:  ", ex); }
        }

        public void ImportCustomerCosting()
        {
            try
            {
                //ExecuteNonQuery("dbo.ImportCustomerCosting");

                int rowsAdded = 0;
                int rowsUpdated = 0;
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();

                cmd = new System.Data.SqlClient.SqlCommand("dbo.ImportCustomerCosting", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@RowsAdded", SqlDbType.Int, 4);
                cmd.Parameters["@RowsAdded"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@RowsAdded"].Value = rowsAdded;

                cmd.Parameters.Add("@RowsUpdated", SqlDbType.Int, 4);
                cmd.Parameters["@RowsUpdated"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@RowsUpdated"].Value = rowsUpdated;

                cmd.ExecuteNonQuery();

                RowsAdded = (int)cmd.Parameters["@RowsAdded"].Value;
                RowsUpdated = (int)cmd.Parameters["@RowsUpdated"].Value;

                connection.Close();
            }
            catch (Exception ex) { throw new Exception("Error from ImportCustomerCosting:  ", ex); }
        }

        public void ImportIMSpecification()
        {
            try
            {
                //ExecuteNonQuery("dbo.ImportIMSpecification")

                int rowsAdded = 0;
                int rowsUpdated = 0;
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();

                cmd = new System.Data.SqlClient.SqlCommand("dbo.ImportIMSpecification", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@RowsAdded", SqlDbType.Int, 4);
                cmd.Parameters["@RowsAdded"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@RowsAdded"].Value = rowsAdded;

                cmd.Parameters.Add("@RowsUpdated", SqlDbType.Int, 4);
                cmd.Parameters["@RowsUpdated"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@RowsUpdated"].Value = rowsUpdated;

                cmd.ExecuteNonQuery();

                RowsAdded = (int)cmd.Parameters["@RowsAdded"].Value;
                RowsUpdated = (int)cmd.Parameters["@RowsUpdated"].Value;

                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error from ImportIMSpecification:  ", ex);
            }
        }

        public void ImportQualityControl()
        {
            try
            {
                //ExecuteNonQuery("dbo.ImportQualityControl");

                int rowsAdded = 0;
                int rowsUpdated = 0;
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();

                cmd = new System.Data.SqlClient.SqlCommand("dbo.ImportQualityControl", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@RowsAdded", SqlDbType.Int, 4);
                cmd.Parameters["@RowsAdded"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@RowsAdded"].Value = rowsAdded;

                cmd.Parameters.Add("@RowsUpdated", SqlDbType.Int, 4);
                cmd.Parameters["@RowsUpdated"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@RowsUpdated"].Value = rowsUpdated;

                cmd.ExecuteNonQuery();

                RowsAdded = (int)cmd.Parameters["@RowsAdded"].Value;
                RowsUpdated = (int)cmd.Parameters["@RowsUpdated"].Value;

                connection.Close();

            }
            catch (Exception ex) { throw new Exception("Error from ImportQualityControl:  ", ex); }
        }

        public void ImportPackaging()
        {
            try
            {
                ExecuteNonQuery("dbo.ImportPackaging");

                int rowsAdded = 0;
                int rowsUpdated = 0;
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();

                cmd = new System.Data.SqlClient.SqlCommand("dbo.ImportPackaging", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@RowsAdded", SqlDbType.Int, 4);
                cmd.Parameters["@RowsAdded"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@RowsAdded"].Value = rowsAdded;

                cmd.Parameters.Add("@RowsUpdated", SqlDbType.Int, 4);
                cmd.Parameters["@RowsUpdated"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@RowsUpdated"].Value = rowsUpdated;

                cmd.ExecuteNonQuery();

                RowsAdded = (int)cmd.Parameters["@RowsAdded"].Value;
                RowsUpdated = (int)cmd.Parameters["@RowsUpdated"].Value;

                connection.Close();

            }
            catch (Exception ex) { throw new Exception("Error from ImportPackaging:  ", ex); }
        }

        public DataSet SelectItemByCustomer(int? custID)
        {
            try
            {
                return ExecuteDataSet("[dbo].[SelectItemByCustomer]",
                    CreateParameter("@CustomerID", SqlDbType.Int, custID));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        //### temporary -- will move to a new dal for reports when ready...
        public DataSet GetIMSpecificationCost(int imID) //, string custNumbr)
        {
            try
            {
                return ExecuteDataSet("SelectIMSpecificationCost",
                    CreateParameter("@ItemID", SqlDbType.Int, imID));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        //public DataSet SelectMAN_ItemIndex(int? custID)
        public DataSet SelectMAN_ItemIndex()
        {
            try
            {
                //return ExecuteDataSet("SelectMAN_ItemIndex",
                //    CreateParameter("@CustomerID", SqlDbType.Int, custID));
                return ExecuteDataSet("SelectMAN_ItemIndex");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataSet SelectCustomerIndex()
        {
            try
            {
                return ExecuteDataSet("SelectCustomerIndex");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataSet SelectCustomerIndex(string productType)
        {
            try
            {
                return ExecuteDataSet("SelectCustomerIndex", 
                    CreateParameter("@ProductType", SqlDbType.VarChar, productType));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataSet GetAdditive123()
        {
            try
            {
                DataSet ds = new DataSet();
                System.Data.DataTable dt = new System.Data.DataTable();
                DataColumn dc = new DataColumn("Additive");
                dt.Columns.Add(dc);
                dt.Rows.Add("1");
                dt.Rows.Add("2");
                dt.Rows.Add("3");
                ds.Tables.Add(dt);
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataSet GetMB123()
        {
            try
            {
                DataSet ds = new DataSet();
                System.Data.DataTable dt = new System.Data.DataTable();
                DataColumn dc = new DataColumn("MasterBatch");
                dt.Columns.Add(dc);
                dt.Rows.Add("1");
                dt.Rows.Add("2");
                dt.Rows.Add("3");
                ds.Tables.Add(dt);
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataSet SelectMaterial()
        {
            try
            {
                return ExecuteDataSet("SelectMaterial");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataSet SelectItemClassByCompany(string compCode)
        {
            try
            {
                return ExecuteDataSet("[PlasmoIntegration].[dbo].[SelectItemClassByCompany]",
                    CreateParameter("@CompanyCode", SqlDbType.VarChar, compCode));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        /*
        public bool HasCodeDuplicate(DataSet ds)
        {
            try
            {
                bool result = false;
                if (ds == null)
                    return false;

                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count < 2)
                    return false;

                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        DataRow rowi = dt.Rows[i];
                        DataRow rowj = dt.Rows[j];
                        string codei = rowi["ITEMNMBR"].ToString();
                        string codej = rowj["ITEMNMBR"].ToString();
                        string compi = rowi["CompDB"].ToString();
                        string compj = rowj["CompDB"].ToString();
                        if (codei == null || codej == null)
                        {
                            MessageBox.Show("Code cannot be empty");
                            result = true;
                            break;
                        }
                        if (compi == null || compj == null)
                        {
                            MessageBox.Show("Company cannot be empty");
                            result = true;
                            break;
                        }
                        if (codei == codej && compi == compj)
                        {
                            MessageBox.Show("Code " + codei + " and Company " + compi + " are duplicated." );
                            result = true;
                            break;
                        }
                    }
                    if (result)
                        break;
                }
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return true;
            }    
        }
        */

















        public void UpdateMaterialComp(DataSet ds)
        {
            //MessageBox.Show("todo:  save MaterialComp");
        }

        public void UpdateAdditiveComp(DataSet ds)
        {
            //MessageBox.Show("todo:  save AdditiveComp");
        }

        public void UpdateMachinePref(DataSet ds)
        {
            //MessageBox.Show("todo:  save MachinePref");
        }

        public void UpdateCosting(DataSet ds)
        {
            //MessageBox.Show("todo:  save CustomerPriceCosting");
        }

        public void UpdateIMSpecification(DataSet ds)
        {
            //MessageBox.Show("todo:  save InjectionMouldSpecification");
        }

        public void UpdateQualityControl(DataSet ds)
        {
            //MessageBox.Show("todo:  save QualityControl");
        }

        public void UpdatePackaging(DataSet ds)
        {
            //MessageBox.Show("todo:  save Packaging");
        }
    }
}
