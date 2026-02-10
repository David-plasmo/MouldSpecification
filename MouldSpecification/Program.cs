using System;
using System.Windows.Forms;


namespace MouldSpecification
{
    static class Program
    {
        //public static string NextForm = "IMSpecificationMain";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ShowNextForm.ShowInputForm("IMSpecificationDataEntry");
            
            return;

            
        }
    }
}
