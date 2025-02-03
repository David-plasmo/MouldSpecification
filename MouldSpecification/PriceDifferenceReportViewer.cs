using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using DataService;

namespace MouldSpecification
{
    public partial class PriceDifferenceReportViewer : Form
    {
        public PriceDifferenceReportViewer()
        {
            InitializeComponent();
        }

        private void PriceDifferenceReportViewer_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            reportViewer1.Reset();
            ProductDataService pds = new ProductDataService();
            DataSet ds = pds.ReportPriceDifference();
            ReportDataSource rptsrc = new ReportDataSource("DataSet1", ds.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rptsrc);
            reportViewer1.LocalReport.ReportPath = "IMPriceDifference.rdlc";
            reportViewer1.LocalReport.Refresh();
            reportViewer1.RefreshReport();
            Cursor.Current = Cursors.Default;
        }
    }
}
