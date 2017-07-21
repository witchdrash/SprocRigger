using System;
using SprocRigger.Wrappers;

namespace SprocRigger
{
    public interface ISprocRiggerFactory
    {
        IDataAccess Get(string connectionInformation, Implementations type);
    }

    public class SprocRiggerFactory : ISprocRiggerFactory
    {
        public IDataAccess Get(string connectionInformation, Implementations type)
        {
            switch (type)
            {
                case Implementations.Sql:
                    return new DataAccess(new SqlImplementation(new SqlConnectionWrapper(connectionInformation)));
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}