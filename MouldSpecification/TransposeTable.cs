using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DataService
{
    /// <summary>
    /// Creates a new DataTable with row count = column count of original table
    ///  and column count = row count of original table. 
    /// </summary>
    /// <param name="dt">Original DataTable to transpose</param>
    /// <returns>A transposed DataTable</returns>
    /// 
    public class TransposeTable
    {
        public DataTable TransposeDT(DataTable dt)
        {
            DataTable transposedTable = new DataTable();

            DataColumn firstColumn = new DataColumn(dt.Columns[0].ColumnName);
            transposedTable.Columns.Add(firstColumn);

            //Add a column for each row in first data table
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataColumn dc = new DataColumn(dt.Rows[i][0].ToString());
                transposedTable.Columns.Add(dc);
            }

            for (int j = 1; j < dt.Columns.Count; j++)
            {
                DataRow dr = transposedTable.NewRow();
                dr[0] = dt.Columns[j].ColumnName;

                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    dr[k + 1] = dt.Rows[k][j];
                }

                transposedTable.Rows.Add(dr);
            }

            return transposedTable;
        }
    }
    
}
