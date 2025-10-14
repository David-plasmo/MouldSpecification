using ApplicationAccessControl;
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

#if DEBUG
            //args = new[] { "Edit" };
#endif
            if (args.Length == 0)
            {
                // Start the main application form 
                ShowNextForm.ShowInputForm("IMSpecificationDataEntry");
            }            
            else if (args[0] == "AppendApplicationObjects")
            {
                ApplicationAccess aa = new ApplicationAccess();
                aa.Edit("MouldSpecification");
            }

            return;

            
        }
    }
}
