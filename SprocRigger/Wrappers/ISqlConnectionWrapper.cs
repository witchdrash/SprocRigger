namespace SprocRigger.Wrappers
{
    public interface ISqlConnectionWrapper
    {
        ISqlCommandWrapper CreateCommand();
        void Open();
        void Close();
        ISqlTransactionWrapper BeginTransaction(string transactionName);
    }
}