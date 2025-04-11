// See https://aka.ms/new-console-template for more information
// encodeFile -enc ""
// encodeFile -dec ""
byte paramCount = (byte)args.Length;
string filePath = null, mode = "help";
switch (paramCount)
{
    case 1:
        {
            if (File.Exists(args[0]))
            {
                filePath = args[0];
                mode = "encode";
            }
        }
        break;
        case 2: 
        {
            if (File.Exists(args[1]))
            {
                filePath = args[1];
                switch (args[0])
                {
                    case "-e": mode = "encode"; break;
                    case "-d": mode = "decode"; break;
                }
            }
            
        }
        break;
}
switch (mode)
{
    case "help":
        Console.WriteLine("Base64 encode a file: ");
        Console.WriteLine("encodeFile [file_path]");
        Console.WriteLine("or");
        Console.WriteLine("encodeFile -e [file_path]");
        Console.WriteLine("Base64 decode a base 64 file: ");
        Console.WriteLine("encodeFile -d [file_path]");
        break;
    case "encode":
        ToBase64File(filePath);
        Console.WriteLine($"File is encoded sucessfully");
        break;
    case "decode":
        FromBase64File(filePath);
        Console.WriteLine($"File is decoded sucessfully");
        break;
}
static void ToBase64File(string filePath)
{
    StreamWriter outStream = new StreamWriter(filePath + ".b64", false);
    FileStream inStream = new FileStream(filePath, FileMode.Open);
    byte[] buf = new byte[1024];
    int readByte = 0;
    while ((readByte = inStream.Read(buf, 0, buf.Length)) > 0)
    {
        outStream.WriteLine(Convert.ToBase64String(buf, 0, readByte));
    }
    inStream.Close();
    outStream.Close();
}
static void FromBase64File(string filePath)
{
    FileStream outStream = new FileStream(filePath.Replace(".b64", ""), FileMode.OpenOrCreate);
    StreamReader inStream = new StreamReader(filePath);
    byte[] buf = new byte[1024];
    string line;
    while ((line = inStream.ReadLine()) != null)
    {
        outStream.Write(Convert.FromBase64String(line));
    }
    inStream.Close();
    outStream.Close();
}