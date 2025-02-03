using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataService;
using System.Diagnostics;
using System.Configuration;
using System.Globalization;
using MouldSpecification;

namespace IMSpecification
{
    public partial class ReportPrompt : Form
    {
        DataSet dsCompany = null;
        bool bIsLoading = true;
        //public int PmID;
        public int ImID;
        //public string CustNumbr;
        //public string CurrentCompanyCode;
        public ReportPrompt(int imID)
        {
            InitializeComponent();
            ImID = imID;
        }

        private void EMCostReportPrompt_Load(object sender, EventArgs e)
        {
            try
            {
                ProductDataService pds = new ProductDataService();

                //-->GetReportPromptDropdown 

                //dsCompany = pds.GetCompany();
                //cboReportCompany.ValueMember = "CMPANYID";
                //cboReportCompany.DisplayMember = "CompanyName";
                //cboReportCompany.DataSource = dsCompany.Tables[0];
                //cboReportCompany.ResetText();
                //cboReportCompany.SelectedIndexChanged += new System.EventHandler(cboReportCompany_SelectedIndexChanged);
                //cboCustomer.Enabled = false;
                //cboProduct.Enabled = false;
                btnShowReport.Enabled = false;
                bIsLoading = false;
                this.Width = 1000;
                cboProduct.SelectedIndexChanged -= new System.EventHandler(cboProduct_SelectedIndexChanged);
                pds = new ProductDataService();
                //ds = pds.GetIMProductDropDownList(CurrentCompanyCode, CustNumbr);
                DataSet ds = pds.GetReportPromptDropdown();
                cboProduct.ValueMember = "ImID";
                cboProduct.DisplayMember = "DisplayValue";
                cboProduct.DataSource = ds.Tables[0];
                cboProduct.Font = new Font("Lucida Sans Typewriter", 10);
                cboProduct.DropDownWidth = 410;
                cboProduct.SelectedIndexChanged += new System.EventHandler(cboProduct_SelectedIndexChanged);
                cboProduct.Enabled = true;
                if (ImID > -1)
                {
                    bIsLoading = false;
                    //int index = cboProduct.Items.IndexOf(ImID);
                    cboProduct.SelectedValue = ImID;
                }
                else
                {
                    cboProduct.ResetText();
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void cboReportCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            //    if (!bIsLoading)
            //    {                
            //        cboCustomer.SelectedIndexChanged -= new System.EventHandler(cboCustomer_SelectedIndexChanged);
            //        int pmID = (int)cboReportCompany.SelectedValue;
            //        DataTable table = dsCompany.Copy().Tables[0];
            //        DataRow[] row = table.Select("CMPANYID = " + pmID.ToString());
            //        string compCode = row[0]["CompanyCode"].ToString();
            //        CurrentCompanyCode = compCode;
            //        ProductDataService pds = new ProductDataService();
            //        DataSet ds = pds.GetCustomerIndexByCompany(compCode);
            //        if (ds != null )
            //        {
            //            cboCustomer.ValueMember = "CUSTNMBR";
            //            cboCustomer.DisplayMember = "Customer";
            //            cboCustomer.DataSource = ds.Tables[0];
            //            cboCustomer.ResetText();
            //            cboCustomer.SelectedIndexChanged += new System.EventHandler(cboCustomer_SelectedIndexChanged);
            //            cboCustomer.Enabled = true;

            //            CustNumbr = null;
            //            cboProduct.SelectedIndexChanged -= new System.EventHandler(cboProduct_SelectedIndexChanged);
            //            pds = new ProductDataService();
            //            ds = pds.GetIMProductDropDownList(CurrentCompanyCode, CustNumbr);
            //            cboProduct.ValueMember = "PmID";
            //            cboProduct.DisplayMember = "Code";
            //            cboProduct.DataSource = ds.Tables[0];
            //            cboProduct.ResetText();
            //            cboProduct.SelectedIndexChanged += new System.EventHandler(cboProduct_SelectedIndexChanged);
            //            cboProduct.Enabled = true;


            //            //btnShowReport.Enabled = false;
            //            btnAssemblyFS.Enabled = false;
            //            btnIMCostCalculation.Enabled = false;
            //            //btnIMPriceDifference.Enabled = true;
            //            btnPackagingFS.Enabled = false;
            //            //btnPackagingIM.Enabled = false;
            //            //btnSpecificationFS.Enabled = false;
            //            //btnSpecificationIM.Enabled = false;
            //            btnQCInstructionIM.Enabled = false;
            //            btnShowReport.Visible = false;
            //        }               
            //    }

            }
            private void cboProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bIsLoading && cboProduct.SelectedValue != null)
            {
                int imID = (int)cboProduct.SelectedValue;
                ImID = imID;
                //btnShowReport.Enabled = true;
                btnAssemblyFS.Enabled = true;
                btnIMCostCalculation.Enabled = true;
                btnIMPriceDifference.Enabled = true;
                btnPackagingFS.Enabled = true;
                btnPackagingIM.Enabled = true;
                //btnSpecificationFS.Enabled = true;
                //btnSpecificationIM.Enabled = true;
                btnQCInstructionIM.Enabled = true;
                btnShowReport.Visible = false;

            }
            
        }

        private void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            //    if (!bIsLoading && cboCustomer.SelectedValue != null)
            //    {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           
            //        string custNmbr = cboCustomer.SelectedValue.ToString();
            //        CustNumbr = custNmbr;
            //        cboProduct.SelectedIndexChanged -= new System.EventHandler(cboProduct_SelectedIndexChanged);
            //        ProductDataService pds = new ProductDataService();
            //        DataSet ds = pds.GetIMProductDropDownList(CurrentCompanyCode, custNmbr);
            //        cboProduct.ValueMember = "PmID";
            //        cboProduct.DisplayMember = "Code";
            //        cboProduct.DataSource = ds.Tables[0];
            //        cboProduct.ResetText();
            //        cboProduct.SelectedIndexChanged += new System.EventHandler(cboProduct_SelectedIndexChanged);
            //        cboProduct.Enabled = true;
            //        btnShowReport.Enabled = false;
            //    }

       }

        private void btnShowReport_Click(object sender, EventArgs e)
        {
            //IMCostReportViewer v = new IMCostReportViewer(PmID, CustNumbr);
            //v.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIMCostCalculation_Click(object sender, EventArgs e)
        {
            IMCostReportViewer v = new IMCostReportViewer(ImID);
            v.ShowDialog();
        }

        private void btnAssemblyFS_Click(object sender, EventArgs e)
        {
            IMSpecificationReportViewer v = new IMSpecificationReportViewer("ProductAssembly", ImID); //PmID, CustNumbr);
            v.ShowDialog();
        }

        public void RunReport(string sReportName)
        {
            //    try
            //    {
            //        //var p = new System.Diagnostics.Process();
            //        //var appSettings = ConfigurationManager.AppSettings;                
            //        //p.StartInfo.FileName = appSettings["ReportsExePath"] ?? "Not Found";
            //        //string reportsAccdbPath = appSettings["ReportsAccdbPath"] ?? "Not Found";
            //        //p.StartInfo.Arguments =  " \"" + reportsAccdbPath + sReportName;
            //        ////p.StartInfo.Arguments = sReportName;
            //        ////MessageBox.Show(p.StartInfo.Arguments);
            //        //p.Start();
            //        IMSpecificationReportViewer v = new IMSpecificationReportViewer("ProductAssembly",PmID, CustNumbr);
            //        v.ShowDialog();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }

        }

        private void btnIMPriceDifference_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            PriceDifferenceReportViewer f = new PriceDifferenceReportViewer();
            f.ShowDialog();
        }

        private void btnPackagingFS_Click(object sender, EventArgs e)
        {
            //string sReportName = " \"" + " /runtime /CMD PackagingFS;" + PmID.ToString() + ";" + CustNumbr;            
            //RunReport(sReportName);
            IMSpecificationReportViewer v = new IMSpecificationReportViewer("ProductPackaging", ImID); //PmID, CustNumbr);
            v.ShowDialog();
        }

        private void btnPackagingIM_Click(object sender, EventArgs e)
        {
            //string sReportName = " \"" + " /runtime /CMD PackagingIM;" + PmID.ToString() + ";" + CustNumbr;
            //RunReport(sReportName);
            IMSpecificationReportViewer v = new IMSpecificationReportViewer("ProductSpecification", ImID); // PmID, CustNumbr);
            v.ShowDialog();
        }

        //private void btnSpecificationFS_Click(object sender, EventArgs e)
        //{
        //    string sReportName = " \"" + " /runtime /CMD SpecificationFS;" + PmID.ToString() + ";" + CustNumbr;
        //    RunReport(sReportName);
        //}

        private void btnQCInstructionIM_Click(object sender, EventArgs e)
        {
            //string sReportName = " \"" + " /runtime /CMD QCInstructionIM;" + PmID.ToString() + ";" + CustNumbr;
            //RunReport(sReportName);
            IMSpecificationReportViewer v = new IMSpecificationReportViewer("QCInstruction", ImID); // PmID, CustNumbr);
            v.ShowDialog();
        }

        //private void btnSpecificationIM_Click(object sender, EventArgs e)
        //{
        //    string sReportName = " \"" + " /runtime /CMD SpecificationIM;" + PmID.ToString() + ";" + CustNumbr;
        //    RunReport(sReportName);
        //}
    }
}
