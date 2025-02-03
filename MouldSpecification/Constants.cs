using System;

namespace DataService
{
    ////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///   Contains a listing of constants used throughout the application
    /// </summary>
    ////////////////////////////////////////////////////////////////////////////
    public sealed class Constants
    {
        /// <summary>
        /// The value used to represent a null DateTime value
        /// </summary>
        public static DateTime NullDateTime = DateTime.MinValue;

        /// <summary>
        /// The value used to represent a null decimal value
        /// </summary>
        public static decimal NullDecimal = decimal.MinValue;

        /// <summary>
        /// The value used to represent a null double value
        /// </summary>
        public static double NullDouble = double.MinValue;

        /// <summary>
        /// The value used to represent a null Guid value
        /// </summary>
        public static Guid NullGuid = Guid.Empty;

        /// <summary>
        /// The value used to represent a null int value
        /// </summary>
        public static int NullInt = int.MinValue; //-2,147,483,648

        /// <summary>
        /// The value used to represent a null int16 value (smallint)
        /// </summary>
        public static int NullInt16 = Int16.MinValue; //-32768;

        /// <summary>
        /// The value used to represent a null byte value (
        /// </summary>
        public static int NullByte = Byte.MinValue; //0
       
        /// <summary>
        /// The value used to represent a null long value (int64) 
        /// </summary>
        public static long NullLong = long.MinValue; //-9,223,372,036,854,775,808

        /// <summary>
        /// The value used to represent a null float value
        /// </summary>
        public static float NullFloat = float.MinValue;

        /// <summary>
        /// The value used to represent a null string value
        /// </summary>
        public static string NullString = string.Empty;

        /// <summary>
        /// Maximum DateTime value allowed by SQL Server
        /// </summary>
        public static DateTime SqlMaxDate = new DateTime(9999, 1, 3, 23, 59, 59);

        /// <summary>
        /// Minimum DateTime value allowed by SQL Server
        /// </summary>
        public static DateTime SqlMinDate = new DateTime(1753, 1, 1, 00, 00, 00);
    }
}
