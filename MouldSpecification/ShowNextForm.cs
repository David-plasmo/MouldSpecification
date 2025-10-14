using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouldSpecification
{
    public class ShowNextForm
    {
        public static void ShowInputForm(string formName)
        {
            try
            {
                Form curForm = null;
                switch (formName)
                {
                    case "IMSpecificationDataEntry":

                        int? lastItemID = null;
                        int? lastCustomerID = null;
                        string nextForm = "SpecificationDataEntry";
                        bool customerFilterOn = true;

                    next_form: //enables navigating between specification, packaging and assembly data entry forms
                        if (nextForm == "SpecificationDataEntry")
                        {
                            SpecificationDataEntry sdeForm = new SpecificationDataEntry(lastItemID, lastCustomerID, customerFilterOn);
                            curForm = sdeForm;

                            while (sdeForm.ShowDialog() == DialogResult.Retry)
                            {
                                lastItemID = sdeForm.LastItemID;
                                lastCustomerID = sdeForm.LastCustomerID;
                                nextForm = sdeForm.NextForm; //enables opening other dataentry form   
                                customerFilterOn = sdeForm.CustomerFilterOn;

                                sdeForm.Dispose();
                                sdeForm = null;
                                if (nextForm != "SpecificationDataEntry")
                                    goto next_form;
                                sdeForm = new SpecificationDataEntry(lastItemID, lastCustomerID, customerFilterOn);
                            }
                            break;
                        }
                        else if (nextForm == "PackagingDataEntry")
                        {
                            PackagingDataEntry pdeForm = new PackagingDataEntry(lastItemID, lastCustomerID, customerFilterOn);
                            curForm = pdeForm;

                            while (pdeForm.ShowDialog() == DialogResult.Retry)
                            {
                                lastItemID = pdeForm.LastItemID;
                                lastCustomerID = pdeForm.LastCustomerID;
                                nextForm = pdeForm.NextForm; //enables opening other dataentry form   
                                customerFilterOn = pdeForm.CustomerFilterOn;
                                pdeForm.Dispose();
                                pdeForm = null;
                                if (nextForm != "PackagingDataEntry")
                                    goto next_form;
                                pdeForm = new PackagingDataEntry(lastItemID, lastCustomerID, customerFilterOn);
                            }
                            break;
                        }
                        else if (nextForm == "QCDataEntry")
                        {
                            QCDataEntry qdeForm = new QCDataEntry(lastItemID, lastCustomerID, customerFilterOn);
                            curForm = qdeForm;

                            while (qdeForm.ShowDialog() == DialogResult.Retry)
                            {
                                lastItemID = qdeForm.LastItemID;
                                nextForm = qdeForm.NextForm; //enables opening other dataentry form
                                customerFilterOn = qdeForm.CustomerFilterOn;
                                qdeForm.Dispose();
                                qdeForm = null;
                                if (nextForm != "QCDataEntry")
                                    goto next_form;
                                qdeForm = new QCDataEntry(lastItemID, lastCustomerID, customerFilterOn);
                            }
                            break;
                        }
                        else if (nextForm == "AttachedDocsDataEntry")
                        {
                            AttachedDocsDataEntry addeForm = new AttachedDocsDataEntry(lastItemID, lastCustomerID, customerFilterOn);
                            curForm = addeForm;

                            while (addeForm.ShowDialog() == DialogResult.Retry)
                            {
                                lastItemID = addeForm.LastItemID;
                                nextForm = addeForm.NextForm; //enables opening other dataentry form   
                                addeForm.Dispose();
                                addeForm = null;
                                if (nextForm != "AttachedDocsDataEntry")
                                    goto next_form;
                                addeForm = new AttachedDocsDataEntry(lastItemID, lastCustomerID);
                            }
                            break;
                        }

                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error showing form " + formName + ": " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
