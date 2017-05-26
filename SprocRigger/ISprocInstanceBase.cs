using System.Collections.Generic;

namespace SprocRigger
{
    public interface ISprocInstanceBase
    {
        string Name { get; }
        ICollection<QueryParameter> Parameters { get; }
        ExecuteType ExecutionType { get; }
        void AddDataResults(DataResults dataResults);
    }
}