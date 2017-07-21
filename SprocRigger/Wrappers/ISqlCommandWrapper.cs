using System.Data;
using System.Data.SqlClient;

namespace SprocRigger.Wrappers
{
    public interface ISqlCommandWrapper
    {
        ISqlTransactionWrapper Transaction { get; set; }
        string CommandText { get; set; }
        CommandType CommandType { get; set; }
        SqlParameterCollection Parameters { get; }
        void ExecuteNonQuery();
        IDataReader ExecuteReader();
        object ExecuteScalar();
    }
}