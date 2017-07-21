using System;
using System.Collections.Generic;
using System.Linq;
using UglyMapper;

namespace SprocRigger
{
    public interface ISprocOperationDefinition { }

    public abstract class SprocInstanceBase : ISprocInstanceBase
    {
        private readonly IMappingRegistry _mappingRegistry;
        private readonly ICollection<DataResults> _results = new List<DataResults>();
        private readonly Dictionary<string, QueryParameter> _dictionaryParameters = new Dictionary<string, QueryParameter>();
        public string Name { get; }
        public ExecuteType ExecutionType { get; }
        public ICollection<QueryParameter> Parameters => _dictionaryParameters.Values;

        protected SprocInstanceBase(string name, ExecuteType executionType, IMappingRegistry mappingRegistry = null)
        {
            Name = name;
            ExecutionType = executionType;
            _mappingRegistry = mappingRegistry;
        }

        public void AddDataResults(DataResults dataResults)
        {
            _results.Add(dataResults);
        }

        public ICollection<T> GetCollection<T>(BaseMapperConfiguration<ICollection<DataResults>, ICollection<T>> mapper)
        {
            return this.GetCollection((x) => mapper.Map(x));
        }

        public ICollection<T> GetCollection<T>(Func<ICollection<DataResults>, ICollection<T>> mappingFunc)
        {
            return mappingFunc(_results);
        }

        public ICollection<T> GetCollection<T>()
        {
            return _mappingRegistry.Find<T>(Name)(_results);
        }

        public T GetSingle<T>()
        {
            return _mappingRegistry.Find<T>(Name)(_results).FirstOrDefault();
        }

        public ICollection<T> GetCollection<T>(IMappingRegistry mappingRegistry)
        {
            return mappingRegistry.Find<T>(Name)(_results);
        }

        public T GetSingle<T>(Func<ICollection<DataResults>, ICollection<T>> mappingFunc)
        {
            return mappingFunc(_results).FirstOrDefault();
        }

        public T GetSingle<T>(Func<ICollection<DataResults>, ICollection<T>> mappingFunc, int index)
        {
            return mappingFunc(_results).Skip(index).First();
        }

        public ICollection<T> GetCollection<T>(Func<DataResults, T> mappingFunc)
        {
            return _results.Select(mappingFunc).ToList();
        }

        public T GetScalar<T>()
        {
            return _results.GetScalar<T>();
        }

        public ISprocFluent<T> UseMapper<T>(BaseMapperConfiguration<ICollection<DataResults>, ICollection<T>> mapper)
        {
            return new SprocFluent<T>(this, mapper);
        }

        public ISprocFluent<T> UseMapper<T>(Func<ICollection<DataResults>, ICollection<T>> mapper)
        {
            return new SprocFluent<T>(this, mapper);
        }

        protected void AddRequiredParameter(QueryParameter value)
        {
            AddDictionaryValue(value);
        }

        protected T GetRequiredParameter<T>(string parameterName) => (T)_dictionaryParameters[parameterName].Value;

        protected void SetRequiredParameter<T>(string parameterName, T value) => _dictionaryParameters[parameterName].Value = value;

        protected void SetOptionalParameter(QueryParameter value) => AddDictionaryValue(value);

        private void AddDictionaryValue(QueryParameter value)
        {
            if (_dictionaryParameters.ContainsKey(value.Name))
                _dictionaryParameters[value.Name].Value = value;
            else
                _dictionaryParameters.Add(value.Name, value);
        }

        protected TOut GetOptionalParameter<TOut>(string parameterName)
        {
            if (_dictionaryParameters.ContainsKey(parameterName))
                return (TOut) _dictionaryParameters[parameterName].Value;

            return default(TOut);
        }
    }

    public class SprocFluent<T> : ISprocFluent<T>
    {
        private readonly SprocInstanceBase _sprocInstanceBase;
        private readonly Func<ICollection<DataResults>, ICollection<T>> _mapper;

        public SprocFluent(SprocInstanceBase sprocInstanceBase, BaseMapperConfiguration<ICollection<DataResults>, ICollection<T>> mapper) : this(sprocInstanceBase, (x) => mapper.Map(x))
        {
            
        }

        public SprocFluent(SprocInstanceBase sprocInstanceBase, Func<ICollection<DataResults>, ICollection<T>> mapper)
        {
            _sprocInstanceBase = sprocInstanceBase;
            _mapper = mapper;
        }

        public ICollection<T> GetCollection()
        {
            return _sprocInstanceBase.GetCollection(_mapper);
        }

        public T GetSingle()
        {
            return _sprocInstanceBase.GetSingle(_mapper);
        }

        public T GetSingle(int index)
        {
            return _sprocInstanceBase.GetSingle(_mapper, index);
        }
    }
}