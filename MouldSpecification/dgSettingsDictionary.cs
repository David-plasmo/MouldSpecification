using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace GridSettings

{
    public class dgSettingsDictionary : Dictionary<string, GridColumns>
    {
        public dgSettingsDictionary(string settingsType)
        {
            /*
                
             */
            try
            {
                DataSet ds = null;
                if (settingsType == "BM")
                {
                    ds = new DataService.ProductDataService().GetBMSetupGridSettings();
                }
                else if (settingsType == "IM")
                {
                    ds = new DataService.ProductDataService().GetIMSpecificationGridSettings();
                }

                DataViewRowState dvrs = DataViewRowState.CurrentRows;
                DataRow[] rows = ds.Tables[0].Select("", "", dvrs);

                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];

                    Add(dr["ColumnName"].ToString(),
                        new GridColumns
                        (dr["ColumnName"].ToString(),
                        dr["DataType"].ToString(),
                        dr["Group"].ToString(),
                        Convert.ToInt32(dr["Width"].ToString()),
                        dr["Heading"].ToString(),
                        dr["Alignment"].ToString(),
                        dr["Format"].ToString(),
                        Convert.ToInt32(dr["Seq"].ToString()),
                        Convert.ToInt32(dr["DisplayLines"].ToString()),
                        Convert.ToBoolean(dr["ReadOnly"].ToString())));
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }            
        }
    }
}
