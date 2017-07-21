using System;
using System.Collections.Generic;
using System.Linq;

namespace SprocRigger
{
    public class MappingRegistry : IMappingRegistry
    {
        readonly List<IRegisteredMapper> _mappers = new List<IRegisteredMapper>();

        public void Register<T>(string storedProcedureName, Func<ICollection<DataResults>, ICollection<T>> mappingFunction)
        {
            _mappers.Add(new RegisteredMapper<T>(storedProcedureName, mappingFunction));
        }

        public Func<ICollection<DataResults>, ICollection<T>> Find<T>(string storedProcedureName)
        {
            return _mappers.Select(x => x.Matches<T>(storedProcedureName)).First(x => x != null);
        }

        public void RegisterSimple<T>(string storedProcedureName, Func<DataResults, T> mappingFunction)
        {
            _mappers.Add(new RegisteredMapper<T>(storedProcedureName, mappingFunction));
        }
    }
}