using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLib
{
    public class GenericDatabase
    {
        private static DbCommand CreateCommand(string commandText, CommandType commandType, List<DbParameter> listParameter)
        {
            var factory = DbProviderFactories.GetFactory(ConnectionDB.ProviderName);
            var connection = factory.CreateConnection();

            connection.ConnectionString = ConnectionDB.ConnectionString;
            var command = connection.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            if (listParameter?.Count > 0)
            {
                foreach (var parameterItem in listParameter)
                {
                    command.Parameters.Add(parameterItem);
                }
            }

            return command;
        }

        public static DbParameter CreateParameter(string parameterName, DbType parameterType, Object parameterValue)
        {
            var factory = DbProviderFactories.GetFactory(ConnectionDB.ProviderName);
            var parameter = factory.CreateParameter();
            if (parameter != null)
            {
                parameter.ParameterName = parameterName;
                parameter.DbType = parameterType;
                parameter.Value = parameterValue;
            }

            return parameter;
        }

        public static Object ExecuteCommand(string commandText, CommandType commandType, List<DbParameter> listParameter, ExecutionType executionType)
        {
            var command = CreateCommand(commandText, commandType, listParameter);
            Object objectReturn = null;

            try
            {
                command.Connection.Open();

                if(command.Connection.State == ConnectionState.Open)
                {
                    switch (executionType)
                    {
                        case ExecutionType.ExecuteNonQuery:
                            objectReturn = command.ExecuteNonQuery();
                            break;
                        case ExecutionType.ExecuteReader:
                            objectReturn = command.ExecuteReader();
                            break;
                        case ExecutionType.ExecuteScalar:
                            objectReturn = command.ExecuteScalar();
                            break;
                        case ExecutionType.ExecuteDataTable:
                            var table = new DataTable();
                            var reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                table.Load(reader);                                
                            }
                            reader.Close();

                            objectReturn = table;
                            break;
                    }
                }
                else
                {
                    throw new Exception("Problema na consulta ao banco de dados, conexão não estabelecida.");
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Erro ao executar command: Execute command. Exception: " + exception.Message);
            }
            finally
            {
                if (command.Connection.State == ConnectionState.Open)
                    command.Connection.Close();
            }
            
            return objectReturn;
        }

        public enum ExecutionType
        {
            ExecuteNonQuery,
            ExecuteReader,
            ExecuteScalar,
            ExecuteDataTable
        }
    }
}
