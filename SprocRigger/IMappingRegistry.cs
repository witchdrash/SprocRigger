using System;
using System.Collections.Generic;

namespace SprocRigger
{
    public interface IMappingRegistry
    {
        void Register<T>(string storedProcedureName, Func<ICollection<DataResults>, ICollection<T>> mappingFunction);
        Func<ICollection<DataResults>, ICollection<T>> Find<T>(string storedProcedureName);
        void RegisterSimple<T>(string storedProcedureName, Func<DataResults, T> mappingFunction);
    }
}