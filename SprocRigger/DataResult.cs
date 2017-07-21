using System;

namespace SprocRigger
{
    public class DataResult
    {
        private readonly object _value;

        public DataResult(string name, object value)
        {
            _value = value;
            Name = name;
        }

        public string Name { get; }

        public T Value<T>()
        {
            if (_value is DBNull && default(T) == null)
                return default(T);

            return (T)_value;
        }
    }
}