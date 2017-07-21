using System.Data.SqlClient;

namespace SprocRigger.Wrappers
{
    public class SqlConnectionWrapper : ISqlConnectionWrapper
    {
        private readonly SqlConnection _sqlConnection;

        public SqlConnectionWrapper(string connectionString)
        {
            _sqlConnection = new SqlConnection(connectionString);
        }

        public ISqlCommandWrapper CreateCommand()
        {
            return new SqlCommandWrapper(_sqlConnection.CreateCommand());
        }

        public void Open()
        {
            _sqlConnection.Open();
        }

        public void Close()
        {
            _sqlConnection.Close();
        }

        public ISqlTransactionWrapper BeginTransaction(string transactionName)
        {
            return new SqlTransactionWrapper(_sqlConnection.BeginTransaction(transactionName));
        }
    }
}