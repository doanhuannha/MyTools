using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.IO;

namespace BlueMoon.Common
{
    public class DataAcess
    {
        private string _connStr = null;
        public DataAcess(string connectionString)
        {
            _connStr = connectionString;
        }
        
        protected async Task<SqlConnection> GetConnectionAsync()
        {
            var conn = (SqlConnection)SqlClientFactory.Instance.CreateConnection();
            conn.ConnectionString = _connStr;
            await conn.OpenAsync();
            return conn;
        }
        private SqlCommand BuildCommand(SqlConnection conn, string spa, (string name, object value)[] parameters)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = spa;
            if (parameters != null)
            {
                foreach (var p in parameters)
                {
                    cmd.Parameters.Add(new SqlParameter(p.name, p.value ?? DBNull.Value));
                }
            }

            return cmd;
        }

        public const string RETURN_VALUE = "RETURN_VALUE";
        private SqlParameter[] BuildOutputParameters((string name, object value)[] outParams)
        {
            SqlParameter[] outValues = null;
            if (outParams != null)
            {
                outValues = new SqlParameter[outParams.Length];
                for (int i = 0; i < outParams.Length; i++)
                {
                    if (outParams[i].name == RETURN_VALUE) outValues[i] = new SqlParameter() { Direction = ParameterDirection.ReturnValue };
                    else outValues[i] = new SqlParameter() { ParameterName = outParams[i].name, Direction = ParameterDirection.Output };
                }
            }
            return outValues;
        }
        private void AssignOutputValues(SqlParameter[] outValues, (string name, object value)[] outParams)
        {
            if (outParams != null)
            {
                for (int i = 0; i < outParams.Length; i++)
                {
                    outParams[i].value = outValues[i].Value;
                }
            }
        }
        public async Task<bool> ExecuteProcedureAsync(string spa, (string name, object value)[] parameters, (string name, object value)[] outParams = null)
        {
            using (var conn = await GetConnectionAsync())
            {
                var cmd = BuildCommand(conn, spa, parameters);
                SqlParameter[] outValues = BuildOutputParameters(outParams);
                if (outValues != null) cmd.Parameters.AddRange(outValues);
                var result = await cmd.ExecuteNonQueryAsync() > 0;
                AssignOutputValues(outValues, outParams);
                return result;
            }
        }
        protected async Task<List<T>> ExecuteProcedureAsync<T>(string spa, (string name, object value)[] parameters, (string name, object value)[] outParams = null) where T : new()
        {
            var tb = await ExecuteDatasetProcedureAsync(spa, parameters, outParams);
            return tb.To<T>();
        }
        public async Task<DataTable> ExecuteDatasetProcedureAsync(string spa, (string name, object value)[] parameters, (string name, object value)[] outParams = null)
        {
            using (var conn = await GetConnectionAsync())
            {
                var cmd = BuildCommand(conn, spa, parameters);
                SqlParameter[] outValues = BuildOutputParameters(outParams);
                if (outValues != null) cmd.Parameters.AddRange(outValues);
                var reader = await cmd.ExecuteReaderAsync();
                AssignOutputValues(outValues, outParams);
                var result = new DataTable("TB");
                result.Load(reader);
                return result;
            }
        }
        public async Task<List<T>> ExecuteDatasetProcedureAsync<T>(string spa, (string name, object value)[] parameters, (string name, object value)[] outParams = null) where T: new()
        {
            var result = await ExecuteDatasetProcedureAsync(spa, parameters, outParams);
            return result.To<T>();
        }
        public async Task<bool> ExecuteTextCommandAsync(string txtCmd)
        {
            using (var conn = await GetConnectionAsync())
            {
                var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = txtCmd;
                return await cmd.ExecuteNonQueryAsync() > 0;
            }
        }
        public async Task<DataTable> ExecuteDatasetTextCommandAsync(string txtCmd)
        {
            using (var conn = await GetConnectionAsync())
            {
                var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = txtCmd;
                var reader = await cmd.ExecuteReaderAsync();
                var result = new DataTable("TB");
                result.Load(reader);
                return result;
            }
        }
        public async Task<List<T>> ExecuteTextCommandAsync<T>(string txtCmd) where T : new()
        {
            var result = await ExecuteDatasetTextCommandAsync(txtCmd);
            return result.To<T>();
        }
    }
    static class ObjectExtension
    {
        public static string ToDisplayString(this (string name, object value)[] ps)
        {
            if (ps == null) return "[Empty]";
            else return ps.Select(p => string.Format("@{0}={1}", p.name, p.value)).Aggregate((r, p) => r + "," + p);
        }
        public class ColumnNameAttribute : Attribute
        {
            public string Name { get; set; }
            public ColumnNameAttribute(string name)
            {
                Name = name;
            }
        }
        public static List<T> To<T>(this DataTable dataTable) where T : new()
        {
            var objectList = new List<T>();

            if (dataTable == null) return objectList;
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);
            foreach (DataRow row in dataTable.Rows)
            {
                var obj = new T();

                foreach (var pp in props)
                {
                    if (pp != null && pp.CanWrite)
                    {
                        string colName = pp.Name;
                        var att = pp.GetCustomAttribute<ColumnNameAttribute>();
                        if (att != null)
                        {
                            colName = att.Name;
                            if (!dataTable.Columns.Contains(colName) && dataTable.Columns.Contains(pp.Name))
                            {
                                colName = pp.Name;
                            }
                        }
                        if(dataTable.Columns.Contains(colName)) pp.SetValue(obj, row[colName] == DBNull.Value ? null : Convert.ChangeType(row[colName], pp.PropertyType));
                    }
                }

                objectList.Add(obj);
            }

            return objectList;
        }
    }
}
