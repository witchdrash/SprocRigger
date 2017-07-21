using System;

namespace SprocRigger
{
    public interface IDataAccess
    {
        /// <summary>
        /// Creates an empty IDataInstruction
        /// </summary>
        /// <returns></returns>
        IDataInstruction Create();

        /// <summary>
        /// Equivilent of calling Create().Use(abc)
        /// </summary>
        /// <param name="sprocInstance"></param>
        /// <returns></returns>
        IDataInstruction Use(Func<ISprocInstanceBase> sprocInstance);

        IDataInstruction Use(ISprocInstanceBase sprocInstance);
        void ExecuteImmediately(ISprocInstanceBase sprocInstance);
    }

    public class DataAccess : IDataAccess
    {
        private readonly ISqlImplementation _databaseImplementation;

        public DataAccess(ISqlImplementation databaseImplementation)
        {
            _databaseImplementation = databaseImplementation;
        }

        /// <summary>
        /// Creates an empty IDataInstruction
        /// </summary>
        /// <returns></returns>
        public IDataInstruction Create()
        {
            return new SqlDataInstruction(_databaseImplementation);
        }

        /// <summary>
        /// Equivilent of calling Create().Use(abc)
        /// </summary>
        /// <param name="sprocInstance"></param>
        /// <returns></returns>
        public IDataInstruction Use(Func<ISprocInstanceBase> sprocInstance)
        {
            return new SqlDataInstruction(sprocInstance, _databaseImplementation);
        }

        public IDataInstruction Use(ISprocInstanceBase sprocInstance)
        {
            return new SqlDataInstruction(() => sprocInstance, _databaseImplementation);
        }

        public void ExecuteImmediately(ISprocInstanceBase sprocInstance)
        {
            new SqlDataInstruction(() => sprocInstance, _databaseImplementation).Execute();
        }
    }
}
