using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Reflection;

namespace bluemoon.tool.db
{
    public class ImportDataAgent
    {
        string _importFolder, _filePattern;
        char _delimiter;
        byte _skipRows;
        Action<string> _log;
        int _lineCountLimited = 100, _groupCountLimited = 7;
        public ImportDataAgent(string folderPath, string pattern = "*", char delimiter = '|', byte skipRows = 1, Action<string> log = null)
        {
            _importFolder = folderPath;
            _filePattern = pattern;
            _delimiter = delimiter;
            _skipRows = skipRows;
            _log = log;
        }
        public bool IncludeFileName { get; set; }
        
        public async Task<int> Import(string connStr, string tableName, string[] columns = null, int[] dataIndices = null)
        {
            _importFolder = _importFolder.Replace("/", "\\").TrimEnd('\\') + "\\";
            var files = Directory.GetFiles(_importFolder, _filePattern);
            if (columns == null)
            {
                columns = GetColumns(files[0], _delimiter);
                dataIndices = new int[columns.Length];
                for (var i = 0; i < dataIndices.Length; i++) dataIndices[i] = i;
            }
            if (IncludeFileName)
            {
                columns = columns.Append("FileName").ToArray();
            }
            await EnsureDataTableExisting(connStr, tableName, columns);
            Log($"Hello, import data from {_importFolder}{_filePattern}: {files.Length} file(s).");
            int totalCnt = 0;
            foreach (var file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                Log($"Processing file {fileInfo.Name}:");
                var lineCnt = await ImportFile(connStr, tableName, columns, fileInfo, dataIndices, _skipRows);
                Log($"\tImported: {lineCnt} rows");
                totalCnt += lineCnt;
            }
            Log($"Total imported: {totalCnt} rows");
            return totalCnt;
        }
        void Log(string msg)
        {
            if (_log != null) _log(msg);
        }
        async Task<int> ImportFile(string connStr, string tableName, string[] columns, FileInfo fileInfo, int[] dataColIndices, byte skipRows)
        {
            SqlClient db = new SqlClient(connStr);
            StreamReader stream = new StreamReader(fileInfo.FullName);
            string line = null;
            int lineCnt = -skipRows;
            List<Task<bool>> taskList = new List<Task<bool>>();
            StringBuilder sb = new StringBuilder();
            while ((line = stream.ReadLine()) != null)
            {
                lineCnt++;
                if (lineCnt < skipRows)
                {
                    continue;
                }

                sb.AppendLine(BuildInsertQuery(fileInfo.Name, line, _delimiter, tableName, columns, dataColIndices, IncludeFileName));
                if (lineCnt % _lineCountLimited == 0)
                {

                    taskList.Add(db.ExecuteNonQuery(sb.ToString()));
                    sb.Clear();
                    await Task.Delay(100);
                    if (taskList.Count >= _groupCountLimited)
                    {
                        Log($"\t::Wait for inserting...{lineCnt} rows");
                        await Task.WhenAll<bool>(taskList);
                        taskList.Clear();
                    }
                }
            }
            stream.Close();
            if (sb.Length > 0)
            {

                taskList.Add(db.ExecuteNonQuery(sb.ToString()));
                sb.Clear();
                Log($"\t::Wait for inserting...{lineCnt} row{(lineCnt == 1 ? "" : "s")}");
                await Task.WhenAll(taskList);
                taskList.Clear();
            }
            return lineCnt;
        }
        async Task EnsureDataTableExisting(string connStr, string name, string[] columns)
        {
            SqlClient db = new SqlClient(connStr);
            var existing = await db.ExecuteQuery<int>($"SELECT TOP 1 1 FROM [INFORMATION_SCHEMA].[TABLES] WHERE [TABLE_TYPE] = 'BASE TABLE' AND [TABLE_NAME] = '{name}';");
            if (existing != 1)
            {
                Log($"Creating new table named '{name}'...");
                await db.ExecuteNonQuery($"CREATE TABLE [dbo].[{name}] ( [Id] INT PRIMARY KEY IDENTITY(1,1), {string.Join(",", columns.Select(c => $"[{c}] nvarchar(128)"))});");
            }
        }
        static string BuildInsertQuery(string fileName, string line, char delimiter, string tableName, string[] columns, int[] dataColIndex, bool includeFileName)
        {
            if (string.IsNullOrEmpty(line)) return "";
            line = line.Replace("'", "");
            line = line.Replace("--", "");
            string[] data = line.Split(delimiter);
            string r = null;
            if(includeFileName) r = $"insert into {tableName} ({string.Join(",", columns)}) values({string.Join(",", dataColIndex.Select(i => i >= data.Length ? "null" : "'" + data[i] + "'").ToArray())}, '{fileName}');";
            else r = $"insert into {tableName} ({string.Join(",", columns)}) values({string.Join(",", dataColIndex.Select(i => i >= data.Length ? "null" : "'" + data[i] + "'").ToArray())});";
            return r;
        }
        static string[] GetColumns(string file, char delimiter)
        {
            string s = null;
            using (var reader = new StreamReader(file))
            {
                s = reader.ReadLine();
            }
            var reg = new Regex("[^a-zA-Z0-9_. ]+", RegexOptions.Compiled);
            return s.Replace(" ", "_").Split(delimiter).Select(c => reg.Replace(c, "")).ToArray();
        }
        class SqlClient
        {
            string _connStr = null;
            public SqlClient(string connStr)
            {
                _connStr = connStr;
            }
            async Task<SqlConnection> GetConnection()
            {
                var conn = (SqlConnection)SqlClientFactory.Instance.CreateConnection();
                conn.ConnectionString = _connStr;
                await conn.OpenAsync();
                return conn;
            }
            public async Task<bool> ExecuteNonQuery(string sql)
            {
                using (var conn = await GetConnection())
                {
                    var cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
            public async Task<T> ExecuteQuery<T>(string sql)
            {
                using (var conn = await GetConnection())
                {
                    var cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    var r = await cmd.ExecuteScalarAsync();
                    return r == null ? default(T) : (T)r;
                }
            }
            
        }
        

        
    }
}
