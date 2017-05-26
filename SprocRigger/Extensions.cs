using System.Data;
using System.Data.SqlClient;

namespace SprocRigger
{
    public static class Extensions
    {
        public static T GetFieldValue<T>(this SqlDataReader dataReader, string columnName)
        {
            var ordinal = dataReader.GetOrdinal(columnName);

            return dataReader.GetFieldValue<T>(ordinal);
        }

        public static object GetValue(this IDataReader dataReader, string columnName)
        {
            var ordinal = dataReader.GetOrdinal(columnName);

            return dataReader.GetValue(ordinal);
        }
    }
}