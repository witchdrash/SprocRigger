using System;
using System.Collections.Generic;

namespace SprocRigger
{
    public class SqlDataInstruction : IDataInstruction
    {
        private readonly List<Func<ISprocInstanceBase>> _spocInstances = new List<Func<ISprocInstanceBase>>();
        private readonly ISqlImplementation _databaseImplementation;
        public SqlDataInstruction(Func<ISprocInstanceBase> sprocInstance, ISqlImplementation databaseImplementation):this(databaseImplementation)
        {
            _spocInstances.Add(sprocInstance);
        }

        public SqlDataInstruction(ISqlImplementation databaseImplementation)
        {
            _databaseImplementation = databaseImplementation;
        }

        public IDataInstruction Use(ISprocInstanceBase sprocInstance)
        {
            _spocInstances.Add(() => sprocInstance);
            return this;
        }

        public IDataInstruction Use(Func<ISprocInstanceBase> sprocInstance)
        {
            _spocInstances.Add(sprocInstance);
            return this;
        }
        
        public void Execute()
        {
            _databaseImplementation.OpenConnection();
            var transaction = _databaseImplementation.CreateTransaction();

            foreach (var operation in _spocInstances)
            {
                _databaseImplementation.Execute(operation(), transaction);
            }

            transaction.Commit();
            _databaseImplementation.CloseConnection();
        }
    }
}