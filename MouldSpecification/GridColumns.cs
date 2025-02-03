using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridSettings
{
    public class GridColumns
    {        
        public string ColumnName { get; set; }
        public string DataType { get; set; }
        public string Group { get; set; }
        public int Width { get; set; }
        public string Heading { get; set; }
        public string Alignment { get; set; }
        public string Format { get; set; }
        public int Seq { get; set; }
        public int DisplayLines { get; set; }
        public bool ReadOnly { get; set; }


        public GridColumns(string columnName, string dataType, string group, int width, 
            string heading, string alignment, string format, int seq, int displayLines, bool readOnly)
        { 
            try
            {
                ColumnName = columnName;
                DataType = dataType;
                Group = group;
                Width = width;
                Heading = heading;
                Alignment = alignment;
                Format = format;
                Seq = seq;
                DisplayLines = displayLines;
                ReadOnly = readOnly;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
