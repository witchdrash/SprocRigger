using System.Data.SqlClient;

namespace SprocRigger.Wrappers
{
    public class SqlTransactionWrapper : ISqlTransactionWrapper
    {
        private readonly SqlTransaction _sqlTransaction;

        public SqlTransactionWrapper(SqlTransaction sqlTransaction)
        {
            _sqlTransaction = sqlTransaction;
        }

        public SqlTransaction Raw()
        {
            return _sqlTransaction;
        }

        public void Commit()
        {
            _sqlTransaction.Commit();
        }

        public void Rollback()
        {
            _sqlTransaction.Rollback();
        }
    }
}