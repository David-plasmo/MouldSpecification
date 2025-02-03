using System;
using System.Data;
using System.Reflection;

namespace MouldSpecification
{
    public static class Extensions
    {
        public static int RowId(this DataRow row)
        {
            FieldInfo fieldInfo = row.GetType().GetField("_rowID",
                BindingFlags.NonPublic | BindingFlags.Instance);
            if (fieldInfo != null)
                return Convert.ToInt32(fieldInfo.GetValue(row));
            else
                return -1;
        }
    }
}
