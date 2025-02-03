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
            //args = new[] { "AppendApplicationObjects" };
#endif
            if (args.Length == 0)
            {
                // Start the main application form (PriceEnquiry)
                Application.Run(new MainForm());
            }            
            else if (args[0] == "AppendApplicationObjects")
            {
                ApplicationAccess aa = new ApplicationAccess();
                aa.AppendApplicationObjectList("MouldSpecification");
            }

            return;

            
        }
    }
}
