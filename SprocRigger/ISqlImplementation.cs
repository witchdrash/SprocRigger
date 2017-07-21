using SprocRigger.Wrappers;

namespace SprocRigger
{
    public interface ISqlImplementation
    {
        void Execute(ISprocInstanceBase configuration, ISqlTransactionWrapper transaction);
        void OpenConnection();
        void CloseConnection();
        ISqlTransactionWrapper CreateTransaction();
    }
}