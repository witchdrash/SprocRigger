using System;
using System.Data;
using System.Data.SqlClient;

namespace SprocRigger
{
    public class QueryParameter
    {
        private readonly ParameterDirection _direction;
        private readonly TypeMapper _typeMapper;
        private bool _set;
        private object _value;
        private readonly Type _type;

        public QueryParameter(string name, ParameterDirection direction)
        {
            Name = name;
            _direction = direction;
        }

        public QueryParameter(string name, ParameterDirection direction, Type type) : this(name, direction)
        {
            _type = type;
            _typeMapper = new TypeMapper();
        }

        public QueryParameter(string name, ParameterDirection direction, object value) : this(name, direction, value.GetType())
        {
            _set = true;
            Value = value;
        }

        public QueryParameter(string name, ParameterDirection direction, object value, Type type) : this(name, direction, type)
        {
            _set = true;
            Value = value;
        }

        public string Name { get; }

        public object Value
        {
            get => _value;
            set
            {
                _value = value;
                _set = true;
            }
        }

        public void AddParameter(SqlParameterCollection collection)
        {
            if (_direction == ParameterDirection.Output)
            {
                collection.Add(new SqlParameter
                {
                    ParameterName = $"@{Name}",
                    Direction = _direction,
                    DbType = _typeMapper.GetDbType(_type)
                });
                return;
            }

            if (!_set)
                return;

            collection.Add(new SqlParameter($"@{Name}", Value)
            {
                Direction = _direction
            });
        }
    }
}