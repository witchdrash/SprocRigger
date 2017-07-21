using System.Data;
using System.Data.SqlClient;

namespace SprocRigger.Wrappers
{
    public class SqlCommandWrapper : ISqlCommandWrapper
    {
        private readonly SqlCommand _sqlCommand;

        public SqlCommandWrapper(SqlCommand sqlCommand)
        {
            _sqlCommand = sqlCommand;
        }

        public ISqlTransactionWrapper Transaction
        {
            get => new SqlTransactionWrapper(_sqlCommand.Transaction);
            set => _sqlCommand.Transaction = ((SqlTransactionWrapper) value).Raw();
        }

        public string CommandText
        {
            get => _sqlCommand.CommandText;
            set => _sqlCommand.CommandText = value;
        }

        public CommandType CommandType
        {
            get => _sqlCommand.CommandType;
            set => _sqlCommand.CommandType = value;
        }

        public SqlParameterCollection Parameters => _sqlCommand.Parameters;

        public void ExecuteNonQuery()
        {
            _sqlCommand.ExecuteNonQuery();
        }

        public IDataReader ExecuteReader()
        {
            return _sqlCommand.ExecuteReader();
        }

        public object ExecuteScalar()
        {
            return _sqlCommand.ExecuteScalar();
        }
    }
}