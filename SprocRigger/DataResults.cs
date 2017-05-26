using System.Collections.Generic;
using System.Linq;

namespace SprocRigger
{
    public class DataResults
    {
        private readonly List<DataResult> _collection = new List<DataResult>();
        public DataResult this[string key] => _collection.FirstOrDefault(x => x.Name == key);
        public DataResult this[int index] => _collection[index];

        public void Add(DataResult value)
        {
            _collection.Add(value);
        }
    }
}