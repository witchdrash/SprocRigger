using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using SprocRigger.Wrappers;

namespace SprocRigger
{
    public class SqlImplementation : ISqlImplementation
    {
        private readonly ISqlConnectionWrapper _sqlConnection;

        public SqlImplementation(ISqlConnectionWrapper sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public virtual void Execute(ISprocInstanceBase configuration, ISqlTransactionWrapper transaction)
        {
            var command = _sqlConnection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = configuration.Name;
            command.CommandType = CommandType.StoredProcedure;

            foreach (var parameter in configuration.Parameters)
            {
                parameter.AddParameter(command.Parameters);
            }

            switch (configuration.ExecutionType)
            {
                case ExecuteType.NonQuery:
                    command.ExecuteNonQuery();
                    break;

                case ExecuteType.Collection:
                    BuildCollectionResult(configuration, command);
                    break;

                case ExecuteType.Scalar:
                    BuildScalarResult(configuration, command);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            ProcessParameters(configuration, command);
        }

        private static void ProcessParameters(ISprocInstanceBase configuration, ISqlCommandWrapper command)
        {
            foreach (SqlParameter parameter in command.Parameters)
            {
                UpdateOutputParameter(configuration, parameter);
            }
        }

        private static void UpdateOutputParameter(ISprocInstanceBase configuration, SqlParameter parameter)
        {
            if (parameter.Direction == ParameterDirection.Input || parameter.Direction == ParameterDirection.ReturnValue)
                return;

            configuration.Parameters.First(x => x.Name == parameter.ParameterName.Replace("@", "")).Value = parameter.Value;
        }

        private static void BuildScalarResult(ISprocInstanceBase configuration, ISqlCommandWrapper command)
        {
            var result = command.ExecuteScalar();
            var dataResults = new DataResults();
            var dataResult = new DataResult("scalar", result);
            dataResults.Add(dataResult);
            configuration.AddDataResults(dataResults);
        }

        private static void BuildCollectionResult(ISprocInstanceBase configuration, ISqlCommandWrapper command)
        {
            using (var dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    var columns = GetColumnNames(dataReader);

                    var dataResults = new DataResults();
                    foreach (var column in columns)
                    {
                        var dataResult = new DataResult(column, dataReader.GetValue(column));
                        dataResults.Add(dataResult);
                    }
                    configuration.AddDataResults(dataResults);
                }
            }
        }

        private static ICollection<string> GetColumnNames(IDataRecord x)
        {
            var columns = new List<string>();
            for (var i = 0; i < x.FieldCount; i++)
            {
                columns.Add(x.GetName(i));
            }
            return columns;
        }

        public void OpenConnection()
        {
            _sqlConnection.Open();
        }

        public void CloseConnection()
        {
            _sqlConnection.Close();
        }

        public ISqlTransactionWrapper CreateTransaction()
        {
            return _sqlConnection.BeginTransaction(GetTransactionName());
        }

        private static string GetTransactionName()
        {
            return Guid.NewGuid().ToString().Substring(0, 20);
        }
    }
}