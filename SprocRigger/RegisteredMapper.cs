using System;
using System.Collections.Generic;
using System.Linq;

namespace SprocRigger
{
    public class RegisteredMapper<T> : IRegisteredMapper
    {
        private readonly string _storedProcedureName;
        private readonly Func<ICollection<DataResults>, ICollection<T>> _mappingFunction;

        public RegisteredMapper(string storedProcedureName, Func<ICollection<DataResults>, ICollection<T>> mappingFunction)
        {
            _storedProcedureName = storedProcedureName;
            _mappingFunction = mappingFunction;
        }

        public RegisteredMapper(string storedProcedureName, Func<DataResults, T> mappingFunction)
        {
            _storedProcedureName = storedProcedureName;
            _mappingFunction = results => results.Select(mappingFunction).ToList();
        }

        public Func<ICollection<DataResults>, ICollection<T1>> Matches<T1>(string storedProcedureName)
        {
            if (typeof(T) == typeof(T1) && _storedProcedureName.Equals(storedProcedureName, StringComparison.CurrentCultureIgnoreCase))
                return (Func<ICollection<DataResults>, ICollection<T1>>)_mappingFunction;

            return null;
        }
    }
}