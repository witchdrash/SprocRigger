using System;
using System.Collections.Generic;
using UglyMapper;

namespace SprocRigger
{
    public interface ISprocFluent<T>
    {
        ICollection<T> GetCollection();
        T GetSingle();
        T GetSingle(int index);
    }

    public interface ISprocInstanceBase
    {
        string Name { get; }
        ExecuteType ExecutionType { get; }
        ICollection<QueryParameter> Parameters { get; }
        void AddDataResults(DataResults dataResults);
        ICollection<T> GetCollection<T>(BaseMapperConfiguration<ICollection<DataResults>, ICollection<T>> mapper);
        ICollection<T> GetCollection<T>(Func<ICollection<DataResults>, ICollection<T>> mappingFunc);
        ICollection<T> GetCollection<T>();
        T GetSingle<T>();
        ICollection<T> GetCollection<T>(IMappingRegistry mappingRegistry);
        T GetSingle<T>(Func<ICollection<DataResults>, ICollection<T>> mappingFunc);
        T GetSingle<T>(Func<ICollection<DataResults>, ICollection<T>> mappingFunc, int index);
        ICollection<T> GetCollection<T>(Func<DataResults, T> mappingFunc);
        T GetScalar<T>();
        ISprocFluent<T> UseMapper<T>(BaseMapperConfiguration<ICollection<DataResults>, ICollection<T>> mapper);
        ISprocFluent<T> UseMapper<T>(Func<ICollection<DataResults>, ICollection<T>> mapper);
    }
}