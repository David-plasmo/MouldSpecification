using Microsoft.Office.Interop.Excel;
//using Microsoft.Reporting.WinForms;
//using Microsoft.ReportingServices.Interfaces;
using ReportBuilder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace MouldSpecification
{
    public partial class IMSpecificationReport : Form
    {
        //ReportViewer reportViewer1 = new ReportViewer();
        ReportDAL dal = new ReportDAL();
        int customerID, itemID;

        public IMSpecificationReport()
        {
            InitializeComponent();
            //reportViewer1.Dock = DockStyle.Fill;
            //this.splitContainer1.Panel2.Controls.Add(reportViewer1);
            //this.Size = new Size(1500, 1000);
        }

        private void IMSpecificationReport_Load(object sender, EventArgs e)
        {
            DataSet ds = dal.SelectCustomerByCosting();
            cboCustomer.DataSource = ds.Tables[0];
            cboCustomer.DisplayMember = "CUSTNAME";
            cboCustomer.ValueMember = "CustomerID";
            cboCustomer.SelectedIndexChanged += cboCustomer_SelectedIndexChanged;
            btnShowReport.Enabled = false;
            cboProductCode.Enabled = false;
            label2.Enabled = false;
        }

        private void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboCustomer.SelectedIndex != -1) 
            {
                btnShowReport.Enabled = false;
                label2.Enabled = false;
                int custID = (int)cboCustomer.SelectedValue;
                DataSet ds = dal.SelectItemByCustomer(custID);
                if(ds!= null) 
                {
                    cboProductCode.SelectedIndexChanged -= cboProductCode_SelectedIndexChanged;
                    cboProductCode.SelectedIndex = -1;
                    cboProductCode.DataSource = ds.Tables[0];
                    cboProductCode.DisplayMember = "ITEMNMBR";
                    cboProductCode.ValueMember = "ItemID";
                    cboProductCode.Enabled = true;
                    customerID = custID;
                    label2.Enabled = true;
                    cboProductCode.SelectedIndexChanged += cboProductCode_SelectedIndexChanged;
                }
            }
            

        }

        private void cboProductCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCustomer.SelectedIndex != -1)
            {
                itemID = (int)cboProductCode.SelectedValue;
                btnShowReport.Enabled = true;
            }
                 
        }

        
        private void btnShowReport_Click(object sender, EventArgs e)
        {
            if (itemID > 0 && customerID > 0)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    string sFile = System.IO.Path.Combine(sCurrentDirectory, @"reports\IMCostingSheet.xlsm");
                    //MessageBox.Show(sFile);
                    string sFilePath = Path.GetFullPath(sFile);

                    string data = null;
                    int i = 0;
                    int j = 0;


                    Microsoft.Office.Interop.Excel.Application xlApp;
                    Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                    Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
                    object misValue = System.Reflection.Missing.Value;

                    xlApp = new Microsoft.Office.Interop.Excel.Application();
                    xlWorkBook = xlApp.Workbooks.Open(sFilePath, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                    //xlWorkBook = xlApp.Workbooks.Open("csharp.net-informations.xls", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                    //xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                    xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets["ItemCostData"];
                    //MessageBox.Show(xlWorkSheet.get_Range("A1", "A1").Value2.ToString());

                    //populate component item costs
                    DataSet ds = dal.GetIMSpecificationCost(customerID, itemID);
                    if (ds != null)
                    {
                        for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                        {
                            for (j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                            {
                                data = ds.Tables[0].Rows[i].ItemArray[j].ToString();
                                xlWorkSheet.Cells[i + 2, j + 1] = data;
                            }
                        }

                        

                        //populate customer cost quantities
                        ds = dal.GetIMCostByPriceQtyReport(customerID, itemID);
                        if (ds != null)
                        {
                            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets["Costing Sheet"];
                            var y = xlWorkBook.Names.Item("No_of_Parts").RefersToRange.Address;
                            

                            for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                            {
                                int row = i + 10;
                                DataRow dr = ds.Tables[0].Rows[i];
                                //data = ds.Tables[0].Rows[i].ItemArray.ToString();
                                for (j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                                {
                                    data = dr[j].ToString();
                                    int col = j + 6;
                                    xlWorkSheet.Cells[row, col] = data;
                                }
                            }
                        }

                        Cursor.Current = Cursors.Default;
                        //xlWorkBook.SaveAs("csharp.net-informations.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                        var dialog = new OpenFileDialog();
                        dialog.Title = "Output FileName";
                        dialog.CheckPathExists = true;  
                        dialog.CheckFileExists  = false;
                        dialog.Filter = "pdf files(*.pdf)|*.pdf|All files(*.*)|*.*";
                        dialog.InitialDirectory = sCurrentDirectory + @"\reports";
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            //MessageBox.Show(dialog.FileName);
                            Cursor.Current = Cursors.WaitCursor;
                            
                            xlWorkBook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, dialog.FileName);
                            xlWorkBook.Close(false, misValue, misValue);
                            xlApp.Quit();
                            System.Diagnostics.Process.Start(dialog.FileName);
                        }
                        releaseObject(xlWorkSheet);
                        releaseObject(xlWorkBook);
                        releaseObject(xlApp);
                        Cursor.Current = Cursors.Default;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                /*
                object Nothing = System.Reflection.Missing.Value;
                var app = new Microsoft.Office.Interop.Excel.Application();
                app.Visible = false;
                Microsoft.Office.Interop.Excel.Workbook workBook = app.Workbooks.Add(Nothing);
                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.Sheets[1];
                worksheet.Name = "WorkSheet";
                // Write data
                worksheet.Cells[1, 1] = "FileName";
                worksheet.Cells[1, 2] = "FindString";
                worksheet.Cells[1, 3] = "ReplaceString";


                // Show save file dialog
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    worksheet.SaveAs(saveFileDialog.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing);
                    workBook.Close(false, Type.Missing, Type.Missing);
                    app.Quit();
                }
                */
            }

        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
