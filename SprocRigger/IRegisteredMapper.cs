using System;
using System.Collections.Generic;

namespace SprocRigger
{
    public interface IRegisteredMapper
    {
        Func<ICollection<DataResults>, ICollection<T>> Matches<T>(string storedProcedureName);
    }
}