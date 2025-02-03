using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;
using System.Windows.Forms;
using DataService;
using MouldSpecification;

namespace IMSpecification
{
    public partial class IMCostReportViewer : Form
    {
        //int PmID;
        //string CustNumbr;
        int ImID;

        public IMCostReportViewer(int imID) //, string custNumbr)
        {
            InitializeComponent();
            ImID = imID;
            //CustNumbr = custNumbr;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            reportViewer1.Reset();
            ReportDataSource rptsrc = new ReportDataSource("DataSet1", GetData().Tables[0]);
            this.reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rptsrc);
            reportViewer1.LocalReport.ReportPath = "IMCostReport.rdlc";
            reportViewer1.LocalReport.Refresh();
            reportViewer1.RefreshReport();
        }

        private DataSet GetData()
        {
            DataSet ds = new DataSet();
            try
            {
                //ProductDataService pds = new ProductDataService();
                //ds = pds.GetIMSpecificationCost(PmID, CustNumbr);
                MainFormDAL dal = new MainFormDAL();
                int ImID = 1;
                ds = dal.GetIMSpecificationCost(ImID);
                return ds;
                ////string constr = @"Data Source=.\Sql2005;Initial Catalog=Northwind;Integrated Security = true";
                //string constr = @"Data Source = DESKTOP-K49LO9F; Initial Catalog = DevPlasmoProduct; Integrated Security = True; Connection Timeout=60;";
                //using (SqlConnection con = new SqlConnection(constr))
                //{
                //    //using (SqlCommand cmd = new SqlCommand("SELECT TOP 20 * FROM customers"))
                //    using (SqlCommand cmd = new SqlCommand("GetIMSpecificationCost", con))
                //    {
                //        cmd.CommandType = CommandType.StoredProcedure;
                //        cmd.Parameters.Add("@PmID", SqlDbType.Int).Value = 5710;
                //        con.Open();

                //        SqlDataAdapter da = new SqlDataAdapter();
                //        da.SelectCommand = cmd;
                //        cmd.ExecuteNonQuery();
                //        da.Fill(ds);
                //        return ds;


                //    }
                //}

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return ds;
            }
        }
        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
