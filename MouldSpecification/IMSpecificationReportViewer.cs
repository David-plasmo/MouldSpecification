using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using DataService;

namespace MouldSpecification
{
    public partial class IMSpecificationReportViewer : Form
    {
        string ReportName;
        int ImID;
        //string CustNumbr;
        const string blankPath = "file:///" + "S:\\CONSOLIDATED PLASTICS\\INJECTION MOULDING\\Database\\Images\\BLANK.jpg";
        string searchDir = "S:\\WEBSITE_PLASMONEW\\Images\\Product-Catalogue";
        string newPath;

        public IMSpecificationReportViewer(string reportName, int imID)
        {
            InitializeComponent();
            ReportName = reportName;
            ImID = imID;
            //CustNumbr = custNumbr;
        }

        private void IMSpecificationReportViewer_Load(object sender, EventArgs e)
        {
            string imagePath;
            string fileName;
            string field1Path;
            string newPath;
            ReportDataSource rptsrc;
            ReportParameter p;
            ProductDataService pds;
            DataSet ds;
#if DEBUG
            searchDir = "E:\\Product-Catalogue";
#endif
            try
            {
                switch (ReportName)
                {
                    case "ProductAssembly":
                        Cursor.Current = Cursors.WaitCursor;
                        reportViewer1.Reset();
                        pds = new ProductDataService();
                        //ds = pds.ReportIMSpecification(PmID, CustNumbr);
                        ds = pds.ReportIMSpecification(ImID);
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            MessageBox.Show("No data");
                            break;
                        }
                        //string blankPath = "file:///" + "S:\\CONSOLIDATED PLASTICS\\INJECTION MOULDING\\Database\\Images\\BLANK.jpg";
                        //imagePath = "file:///" + ds.Tables[0].Rows[0]["Image"].ToString();
                        imagePath = ds.Tables[0].Rows[0]["Image"].ToString();
                        if (File.Exists(imagePath))
                            imagePath = "file:///" + imagePath;
                        else
                        {
                            fileName = Path.GetFileName(imagePath);
                            newPath = "";
                            DirSearch(searchDir);
                            if (File.Exists(newPath))
                                imagePath = "file:///" + newPath;
                            else
                                imagePath = blankPath;
                        }
                        
                        field1Path = ds.Tables[0].Rows[0]["Field1"].ToString();
                        if (File.Exists(field1Path))
                            field1Path = "file:///" + field1Path;
                        else
                            field1Path = blankPath;
                        string assemblyImage1Path = ds.Tables[0].Rows[0]["AssemblyImage1"].ToString();
                        if (assemblyImage1Path.Length > 0)
                            assemblyImage1Path = "file:///" + assemblyImage1Path;
                        else
                            assemblyImage1Path = blankPath;
                        string assemblyImage2Path = ds.Tables[0].Rows[0]["AssemblyImage2"].ToString();
                        if (assemblyImage2Path.Length > 0)
                            assemblyImage2Path = "file:///" + assemblyImage2Path;
                        else
                            assemblyImage2Path = blankPath;
                        string assemblyImage3Path = ds.Tables[0].Rows[0]["AssemblyImage3"].ToString();
                        if (assemblyImage3Path.Length > 0)
                            assemblyImage3Path = "file:///" + assemblyImage3Path;
                        else
                            assemblyImage3Path = blankPath;
                        string assemblyImage4Path = ds.Tables[0].Rows[0]["AssemblyImage4"].ToString();
                        if (assemblyImage4Path.Length > 0)
                            assemblyImage4Path = "file:///" + assemblyImage4Path;
                        else
                            assemblyImage4Path = blankPath;
                        string assemblyImage5Path = ds.Tables[0].Rows[0]["AssemblyImage5"].ToString();
                        if (assemblyImage5Path.Length > 0)
                            assemblyImage5Path = "file:///" + assemblyImage4Path;
                        else
                            assemblyImage5Path = blankPath;
                        string assemblyImage6Path = ds.Tables[0].Rows[0]["AssemblyImage6"].ToString();
                        if (assemblyImage6Path.Length > 0)
                            assemblyImage6Path = "file:///" + assemblyImage6Path;
                        else
                            assemblyImage6Path = blankPath;

                        rptsrc = new ReportDataSource("DataSet1", ds.Tables[0]);
                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(rptsrc);
                        reportViewer1.LocalReport.ReportPath = "ProductAssemblySheet.rdlc";
                        reportViewer1.ProcessingMode = ProcessingMode.Local;
                        reportViewer1.LocalReport.EnableExternalImages = true;
                        //ReportParameter[] p = new ReportParameter[0];
                        //p[0] = new ReportParameter("ImagePar", @imagePath);
                        //p[0] = new ReportParameter("ImageParam", @imagePath);
                        p = new ReportParameter();                        
                        p.Name = "ImagePar";
                        p.Values.Add(imagePath);
                        reportViewer1.LocalReport.SetParameters(p);
                        p = new ReportParameter();
                        p.Name = "Field1Par";
                        p.Values.Add(field1Path);
                        reportViewer1.LocalReport.SetParameters(p);
                        //p = new ReportParameter();
                        //p.Name = "Field1VisiblePar";
                        //bool setVisible = (field1Path != "file:///");
                        //p.Values.Add(setVisible.ToString());
                        //p.Values.Add("false");
                        //reportViewer1.LocalReport.SetParameters(p);
                        p = new ReportParameter();
                        p.Name = "AssemblyImage1Par";
                        p.Values.Add(assemblyImage1Path);
                        reportViewer1.LocalReport.SetParameters(p);
                        p = new ReportParameter();
                        p.Name = "AssemblyImage2Par";
                        p.Values.Add(assemblyImage2Path);
                        reportViewer1.LocalReport.SetParameters(p);
                        p = new ReportParameter();
                        p.Name = "AssemblyImage3Par";
                        p.Values.Add(assemblyImage3Path);
                        reportViewer1.LocalReport.SetParameters(p);
                        p = new ReportParameter();
                        p.Name = "AssemblyImage3Par";
                        p.Values.Add(assemblyImage3Path);
                        reportViewer1.LocalReport.SetParameters(p);
                        p = new ReportParameter();
                        p.Name = "AssemblyImage4Par";
                        p.Values.Add(assemblyImage4Path);
                        reportViewer1.LocalReport.SetParameters(p);
                        p = new ReportParameter();
                        p.Name = "AssemblyImage5Par";
                        p.Values.Add(assemblyImage5Path);
                        reportViewer1.LocalReport.SetParameters(p);
                        this.reportViewer1.RefreshReport();
                        p = new ReportParameter();
                        p.Name = "AssemblyImage6Par";
                        p.Values.Add(assemblyImage6Path);
                        reportViewer1.LocalReport.SetParameters(p);

                        this.reportViewer1.RefreshReport();
                        break;

                    case "ProductPackaging":
                        Cursor.Current = Cursors.WaitCursor;
                        reportViewer1.Reset();
                        pds = new ProductDataService();
                        //ds = pds.ReportIMSpecification(PmID, CustNumbr);
                        ds = pds.ReportIMSpecification(ImID);
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            MessageBox.Show("No data");
                            break;
                        }
                        //blankPath = "file:///" + "S:\\CONSOLIDATED PLASTICS\\INJECTION MOULDING\\Database\\Images\\BLANK.jpg";
                        imagePath = ds.Tables[0].Rows[0]["Image"].ToString();
                        if (File.Exists(imagePath))
                            imagePath = "file:///" + imagePath;
                        else
                        {
                            fileName = Path.GetFileName(imagePath);
                            newPath = "";
                            DirSearch(searchDir);
                            if (File.Exists(newPath))
                                imagePath = "file:///" + newPath;
                            else
                                imagePath = blankPath;
                        }
                        string packingImage1Path = ds.Tables[0].Rows[0]["PackingImage1"].ToString();
                        if (File.Exists(packingImage1Path))
                            packingImage1Path = "file:///" + packingImage1Path;
                        else
                            packingImage1Path = blankPath;
                        string packingImage2Path = ds.Tables[0].Rows[0]["PackingImage2"].ToString();
                        if (File.Exists(packingImage2Path))
                            packingImage2Path = "file:///" + packingImage2Path;
                        else
                            packingImage2Path = blankPath;
                        string packingImage3Path = ds.Tables[0].Rows[0]["PackingImage3"].ToString();
                        if (File.Exists(packingImage3Path))
                            packingImage3Path = "file:///" + packingImage3Path;
                        else
                            packingImage3Path = blankPath;
                        
                        field1Path = ds.Tables[0].Rows[0]["Field1"].ToString();
                        if (File.Exists(field1Path))
                            field1Path = "file:///" + field1Path;
                        else
                            field1Path = blankPath;

                        rptsrc = new ReportDataSource("DataSet1", ds.Tables[0]);
                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(rptsrc);
                        reportViewer1.LocalReport.ReportPath = "ProductPackagingSheet.rdlc";
                        reportViewer1.ProcessingMode = ProcessingMode.Local;
                        reportViewer1.LocalReport.EnableExternalImages = true;
             
                        p = new ReportParameter();
                        p.Name = "ImagePar";
                        p.Values.Add(imagePath);
                        reportViewer1.LocalReport.SetParameters(p);
                        p = new ReportParameter();
                        p.Name = "Field1Par";
                        p.Values.Add(field1Path);
                        reportViewer1.LocalReport.SetParameters(p);
                        p = new ReportParameter();
                        p.Name = "PackingImage1Par";
                        p.Values.Add(packingImage1Path);
                        reportViewer1.LocalReport.SetParameters(p);
                        p = new ReportParameter();
                        p.Name = "PackingImage2Par";
                        p.Values.Add(packingImage2Path);
                        reportViewer1.LocalReport.SetParameters(p);
                        p = new ReportParameter();
                        p.Name = "PackingImage3Par";
                        p.Values.Add(packingImage3Path);
                        reportViewer1.LocalReport.SetParameters(p);
                                            
                        this.reportViewer1.RefreshReport();
                        break;

                    case "ProductSpecification":
                        Cursor.Current = Cursors.WaitCursor;
                        reportViewer1.Reset();
                        pds = new ProductDataService();
                        // ds = pds.ReportIMSpecification(PmID, CustNumbr);
                        ds = pds.ReportIMSpecification(ImID);
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            MessageBox.Show("No data");
                            break;
                        }
                        //blankPath = "file:///" + "S:\\CONSOLIDATED PLASTICS\\INJECTION MOULDING\\Database\\Images\\BLANK.jpg";
                        imagePath = ds.Tables[0].Rows[0]["Image"].ToString();
                        if (File.Exists(imagePath))
                            imagePath = "file:///" + imagePath;
                        else
                        {
                            fileName = Path.GetFileName(imagePath);
                            newPath = "";
                            DirSearch(searchDir);
                            if (File.Exists(newPath))
                                imagePath = "file:///" + newPath;
                            else
                                imagePath = blankPath;
                        }
                        field1Path = ds.Tables[0].Rows[0]["Field1"].ToString();
                        if (File.Exists(field1Path))
                            field1Path = "file:///" + field1Path;
                        else
                            field1Path = blankPath;

                        rptsrc = new ReportDataSource("DataSet1", ds.Tables[0]);
                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(rptsrc);
                        reportViewer1.LocalReport.ReportPath = "ProductSpecificationSheet.rdlc";
                        reportViewer1.ProcessingMode = ProcessingMode.Local;
                        reportViewer1.LocalReport.EnableExternalImages = true;

                        p = new ReportParameter();
                        p.Name = "ImagePar";
                        p.Values.Add(imagePath);
                        reportViewer1.LocalReport.SetParameters(p);
                        p = new ReportParameter();
                        p.Name = "Field1Par";
                        p.Values.Add(field1Path);
                        reportViewer1.LocalReport.SetParameters(p);
                        p = new ReportParameter();
                        
                        this.reportViewer1.RefreshReport();
                        break;

                    case "QCInstruction":
                        Cursor.Current = Cursors.WaitCursor;
                        reportViewer1.Reset();
                        pds = new ProductDataService();
                        //ds = pds.ReportIMSpecification(PmID, CustNumbr);
                        ds = pds.ReportIMSpecification(ImID);
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            MessageBox.Show("No data");
                            break;
                        }
                        string QCImage1Path = ds.Tables[0].Rows[0]["QCImage1"].ToString();
                        if (File.Exists(QCImage1Path))
                            QCImage1Path = "file:///" + QCImage1Path;
                        else
                            QCImage1Path = blankPath;
                        string QCImage2Path = ds.Tables[0].Rows[0]["QCImage2"].ToString();
                        if (File.Exists(QCImage2Path))
                            QCImage2Path = "file:///" + QCImage2Path;
                        else
                            QCImage2Path = blankPath;
                        string QCImage3Path = ds.Tables[0].Rows[0]["QCImage3"].ToString();
                        if (File.Exists(QCImage3Path))
                            QCImage3Path = "file:///" + QCImage3Path;
                        else
                            QCImage3Path = blankPath;
                        string QCImage4Path = ds.Tables[0].Rows[0]["QCImage4"].ToString();
                        if (File.Exists(QCImage4Path))
                            QCImage4Path = "file:///" + QCImage4Path;
                        else
                            QCImage4Path = blankPath;
                        

                        rptsrc = new ReportDataSource("DataSet1", ds.Tables[0]);
                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(rptsrc);
                        reportViewer1.LocalReport.ReportPath = "QCInstructions.rdlc";
                        reportViewer1.ProcessingMode = ProcessingMode.Local;
                        reportViewer1.LocalReport.EnableExternalImages = true;

                        p = new ReportParameter();
                        p.Name = "QCImage1Par";
                        p.Values.Add(QCImage1Path);
                        reportViewer1.LocalReport.SetParameters(p);
                        p = new ReportParameter();
                        p.Name = "QCImage2Par";
                        p.Values.Add(QCImage2Path);
                        reportViewer1.LocalReport.SetParameters(p);
                        p = new ReportParameter();
                        p.Name = "QCImage3Par";
                        p.Values.Add(QCImage3Path);
                        reportViewer1.LocalReport.SetParameters(p);
                        p.Name = "QCImage4Par";
                        p.Values.Add(QCImage3Path);
                        reportViewer1.LocalReport.SetParameters(p);

                        this.reportViewer1.RefreshReport();
                        break;

                    default:
                        Console.WriteLine("Nothing");
                        break;
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            void DirSearch(string sDir)
            {
                try
                {
                    foreach (string d in Directory.GetDirectories(sDir))
                    {
                        foreach (string f in Directory.GetFiles(d, fileName))
                        {
                            //lstFilesFound.Items.Add(f);
                            newPath =  f.ToString();
                            return;
                        }
                        DirSearch(d);
                    }
                }
                catch (System.Exception excpt)
                {
                    Console.WriteLine(excpt.Message);
                }
            }

        }
    }
}
