namespace SprocRigger.Wrappers
{
    public interface ISqlTransactionWrapper
    {
        void Commit();
        void Rollback();
    }
}