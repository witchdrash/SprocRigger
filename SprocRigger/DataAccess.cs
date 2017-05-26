using System;

namespace SprocRigger
{
    public class DataAccess
    {
        private readonly SqlImplementation _databaseImplementation;

        public DataAccess(SqlImplementation databaseImplementation)
        {
            _databaseImplementation = databaseImplementation;
        }

        public IDataInstruction Use(Func<ISprocInstanceBase> sprocInstance)
        {
            return new SqlDataInstruction(sprocInstance, _databaseImplementation);
        }

        public IDataInstruction Use(ISprocInstanceBase sprocInstance)
        {
            return new SqlDataInstruction(() => sprocInstance, _databaseImplementation);
        }
    }
}
