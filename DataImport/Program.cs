// See https://aka.ms/new-console-template for more information
using bluemoon.tool.db;


const string CONN_STR = "Data Source=.;Initial Catalog=TestDb;Integrated Security=SSPI;Connection Timeout=60";


//*
const string IMPORT_FOLDER = @"\Test1";
const string FILE_PATTERN = "*";
const char DELIMITER = '\t';
const byte LINES_2_SKIP = 1;
const string TABLE_NAME = "DB_BankRejects_New";
string[] COLUMNS = { "CreationDate", "Currency", "TransactionDetails", "MandateReference", "OriginalCounterpartyName", "OriginalAmount", "Reason", "CounterpartySortCodeValue", "OriginalCounterpartyAccountNumber", "NewCounterpartyAccountNumber", "SWIFT" };
int[] FILE_COLUMN_INDEX = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

//*/

/*
const string IMPORT_FOLDER = @"\Test2";
const string FILE_PATTERN = "*";
const char DELIMITER = '|';
const byte SKIP_FIRST_LINE = 1;
const string TABLE_NAME = "DB_BankFiles_New";
string[] COLUMNS = { "ReferenceNumber", "Amount", "CustName", "IBAN", "BankAccount"};

int[] FILE_COLUMN_INDEX = { 3, 2, 10, 6, 7 };
//*/
ImportDataAgent agent = new ImportDataAgent(IMPORT_FOLDER, FILE_PATTERN, DELIMITER, LINES_2_SKIP, Console.WriteLine) { IncludeFileName = true };
await agent.Import(CONN_STR, TABLE_NAME);
