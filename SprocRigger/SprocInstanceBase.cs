using System.Collections.Generic;
using UglyMapper;

namespace SprocRigger
{
    public abstract class SprocInstanceBase<T> : ISprocInstanceBase
    {
        private readonly ICollection<DataResults> _results = new List<DataResults>();
        protected Dictionary<string, QueryParameter> DictionaryParameters = new Dictionary<string, QueryParameter>();
        public string Name { get; }
        public ExecuteType ExecutionType { get; }
        public ICollection<QueryParameter> Parameters => DictionaryParameters.Values;

        public void AddDataResults(DataResults dataResults)
        {
            _results.Add(dataResults);
        }

        protected SprocInstanceBase(string name, ExecuteType executionType)
        {
            Name = name;
            ExecutionType = executionType;
        }

        public T Results(BaseMapperConfiguration<ICollection<DataResults>, T> mapper)
        {
            return mapper.Map(_results);
        }
    }
}