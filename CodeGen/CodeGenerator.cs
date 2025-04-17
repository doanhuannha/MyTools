using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.InteropServices.JavaScript;

namespace BlueMoon.Common
{
    /* Sample Template
     
create proc [sp_{table_name}_Insert]
<,:@{col_name} {col_type}>
as
INSERT [{table_name}](<,:[{col_name}]>)
VALUES (<,:@{col_name}>)
     */
    public class CodeGenerator
    {
        class TB_STRUCT
        {
            public string TABLE_NAME { get; set; }
            public string COLUMN_NAME { get; set; }
            public string DATA_TYPE { get; set; }
            public bool PRIMARY_KEY { get; set; }
            public string CS_TYPE { get; set; }
            public int INDEX { get; set; }
        }
        static Regex s_regex_AllCols = new Regex(@"<all\((.*?)\):(.*?)>", RegexOptions.Compiled);
        static Regex s_regex_NorCols = new Regex(@"<nor\((.*?)\):(.*?)>", RegexOptions.Compiled);
        static Regex s_regex_PriCols = new Regex(@"<pri\((.*?)\):(.*?)>", RegexOptions.Compiled);
        static Regex s_regex_Now = new Regex(@"{now:(.*?)}", RegexOptions.Compiled);

        public async Task GenerateFromMSSQL(string connStr, string templateFile, string outputFolder, params string[] tableNames)
        {
            if (!outputFolder.EndsWith("\\")) outputFolder += "\\"; 
            FileInfo tplInfo = new FileInfo(templateFile);
            string templateStr = File.ReadAllText(templateFile);

            DataAcess dal = new DataAcess(connStr);

            var allCols = await dal.ExecuteTextCommandAsync<TB_STRUCT>(@"
;WITH CTE_PRIMARY_KEYS AS(
SELECT 
     KU.[TABLE_NAME]
    ,KU.[COLUMN_NAME]
FROM [INFORMATION_SCHEMA].[TABLE_CONSTRAINTS] AS TC 
JOIN [INFORMATION_SCHEMA].[KEY_COLUMN_USAGE] AS KU
    ON TC.[CONSTRAINT_TYPE] = 'PRIMARY KEY' 
    AND TC.[CONSTRAINT_NAME] = KU.[CONSTRAINT_NAME]
)
SELECT a.[TABLE_NAME],a.[COLUMN_NAME], 
[DATA_TYPE] + 
	CASE WHEN [CHARACTER_MAXIMUM_LENGTH] IS NULL THEN '' 
	ELSE 
		CASE [CHARACTER_MAXIMUM_LENGTH] WHEN -1 THEN '(max)' 
		ELSE '('+CAST([CHARACTER_MAXIMUM_LENGTH] AS VARCHAR)+')'
		END
	END
AS [DATA_TYPE],
CAST(IIF(c.[COLUMN_NAME] is null, 0, 1) AS BIT)  [PRIMARY_KEY],
(ROW_NUMBER() OVER(PARTITION BY a.[TABLE_NAME] ORDER BY (SELECT NULL)) - 1) [INDEX]
FROM [INFORMATION_SCHEMA].[TABLES] (NOLOCK) b, [INFORMATION_SCHEMA].[COLUMNS] (NOLOCK) a
LEFT JOIN [CTE_PRIMARY_KEYS] c ON c.[TABLE_NAME] = a.[TABLE_NAME] and c.[COLUMN_NAME] = a.[COLUMN_NAME]
WHERE a.[TABLE_NAME] = b.[TABLE_NAME] AND b.[TABLE_TYPE] = 'BASE TABLE'

 

");
            
            var pp = allCols.Where(p => tableNames.Length == 0 ? true : tableNames.Contains(p.TABLE_NAME)).ToList();
            allCols.Where(p => tableNames.Length == 0 ? true : tableNames.Contains(p.TABLE_NAME)).GroupBy(p => p.TABLE_NAME).ToList().ForEach(async tb =>
            {

                DataTable sampleDataType = await dal.ExecuteDatasetTextCommandAsync("select top 0 * from " + tb.Key);
                var outStr = templateStr;
                
                outStr = s_regex_AllCols.Replace(outStr, new MatchEvaluator(tb, sampleDataType).DoIt);
                outStr = s_regex_NorCols.Replace(outStr, new MatchEvaluator(tb.Where(p => !p.PRIMARY_KEY), sampleDataType).DoIt);
                outStr = s_regex_PriCols.Replace(outStr, new MatchEvaluator(tb.Where(p => p.PRIMARY_KEY), sampleDataType).DoIt);
                outStr = outStr.Replace("{table_name}", tb.Key);
                outStr = s_regex_Now.Replace(outStr, m => {
                    return string.Format(m.Value.Replace("{now:", "{0:"), DateTime.Now);
                });
                foreach (var col in tb)
                {
                    outStr = outStr
                                .Replace($"{{col_name_{col.INDEX}}}", col.COLUMN_NAME)
                                .Replace($"{{col_type_{col.INDEX}}}", col.DATA_TYPE)
                                .Replace($"{{cs_col_type_{col.INDEX}}}", col.CS_TYPE);
                }

                File.WriteAllText(outputFolder + Path.GetFileNameWithoutExtension(tplInfo.Name.Replace("{table_name}", tb.Key)), outStr);
            });
            
            
            
            
        }
        class MatchEvaluator
        {
            DataTable sampleDataType = null;
            IEnumerable<TB_STRUCT> tb = null;
            public MatchEvaluator(IEnumerable<TB_STRUCT> tb, DataTable sampleDataType) 
            { 
                this.tb = tb;
                this.sampleDataType = sampleDataType;
            }
            public string DoIt(Match m)
            {
                var delimiter = m.Groups[1].Value;
                delimiter = delimiter.Replace("[n]", "\r\n");
                var subTpl = m.Groups[2].Value;
                var outSub = "";
                foreach (var col in tb)
                {
                    var cs_type = sampleDataType.Columns[col.COLUMN_NAME].DataType.Name;
                    col.CS_TYPE = cs_type
                                .Replace("String", "string")
                                .Replace("Int32", "int")
                                .Replace("Int64", "long")
                                .Replace("Byte", "byte")
                                ;
                    outSub += (outSub == "" ? "" : delimiter)
                        + subTpl
                            .Replace("{col_name}", col.COLUMN_NAME)
                            .Replace("{col_type}", col.DATA_TYPE)
                            .Replace("{cs_col_type}", col.CS_TYPE)
                            ;
                }
                return outSub;
            }
        }
    }
    
}
