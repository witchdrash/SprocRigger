using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SprocRigger
{
    public static class Extensions
    {
        public static object GetValue(this IDataReader dataReader, string columnName)
        {
            var ordinal = dataReader.GetOrdinal(columnName);

            return dataReader.GetValue(ordinal);
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            enumerable.ForEach((x, index) => { action(x); });
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T, int> action)
        {
            var asList = enumerable.ToList();
            for (var index = 0; index < asList.Count; index++)
            {
                action(asList[index], index);
            }
        }

        public static T GetScalar<T>(this ICollection<DataResults> collection)
        {
            return collection.First()["scalar"].Value<T>();
        }
    }
}