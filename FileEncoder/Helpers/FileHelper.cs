using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EncodeFile.Helpers
{
    public static class FileHelper
    {
        static Regex s_reg_Obj = new Regex(@"(\d+) 0 obj", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        static Regex s_reg_Xref_Unix = new Regex(@"startxref\n(\d+)\n", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled);
        static Regex s_reg_Xref_Window = new Regex(@"startxref\r\n(\d+)\r\n", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled);
        static void Transform(byte[] data, string key, int from = 0, int length = -1)
        {
            if (length == -1) length = data.Length;
            byte[] k = Encoding.ASCII.GetBytes(key);
            for (int i = from; i < from + length; i++) data[i] = (byte)((i - from) ^ data[i] ^ ~k[(i - from) % k.Length] ^ length);
        }
        public static string DetachPdf(this string pdfFilePath, string detachFileName, string skey)
        {
            byte[] buf = File.ReadAllBytes(pdfFilePath);
            var s = Encoding.ASCII.GetString(buf);
            var ms = s_reg_Obj.Matches(s);
            var m = ms.Zip(ms.Skip(1), (match, next) => new { match, next, id = int.Parse(match.Groups[1].Value) }).OrderByDescending(g =>
            g.id).FirstOrDefault();

            var ss = Encoding.ASCII.GetString(buf, m.match.Index, m.next.Index - m.match.Index);
            Regex s_reg_2 = new Regex(@"/Length (\d+)>>stream", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            var mm = s_reg_2.Match(ss);
            int dataIndex = m.match.Index + mm.Index + mm.Length + 1;
            int dataLength = int.Parse(mm.Groups[1].Value);
            //buf = new Span<byte>(buf, dataIndex, dataLength).ToArray();
            Transform(buf, skey, dataIndex, dataLength);
            ss = Encoding.ASCII.GetString(buf, dataIndex, dataLength);
            var outPath = Path.Combine(new FileInfo(pdfFilePath).DirectoryName, detachFileName);
            var ofs = File.Open(outPath, FileMode.Create, FileAccess.Write,
            FileShare.Read);
            ofs.Write(buf, dataIndex, dataLength);
            ofs.Close();
            return outPath;
        }
        public static string AttachPdf(this string pdfFilePath, string dataFilePath, string skey)
        {
            bool unixLineFeed = false;
            string outPath = pdfFilePath.TrimEnd(".pdf".ToCharArray()) + ".tmp";
            byte[] raw = File.ReadAllBytes(dataFilePath);
            var ofs = File.Open(outPath, FileMode.Create, FileAccess.Write, FileShare.Read);
            byte[] buf = File.ReadAllBytes(pdfFilePath);
            var s = Encoding.ASCII.GetString(buf);
            unixLineFeed = !s.Substring(0, 16).Contains("\r\n");
            var ms = s_reg_Obj.Matches(s);
            int maxId = ms.Select(m => int.Parse(m.Groups[1].Value)).OrderDescending().FirstOrDefault();
            int pos = ms.Count - 1;// Random.Shared.Next(ms.Count); ;
            var m = ms[pos];
            //append string
            ofs.Write(buf, 0, m.Index);
            //header
            int offset = 0;
            byte[] data = Encoding.ASCII.GetBytes(@$"{maxId + Random.Shared.Next(256)} 0 obj
<</Filter/FlateDecode/Length {raw.Length}>>stream
".Replace("\r\n", unixLineFeed ? "\n" : "\r\n"));
            offset += data.Length;
            ofs.Write(data, 0, data.Length);
            //main data
            Transform(raw, skey);
            offset += raw.Length;
            ofs.Write(raw, 0, raw.Length);
            //footer
            data = Encoding.ASCII.GetBytes(@"
endstream
endobj
".Replace("\r\n", unixLineFeed ? "\n" : "\r\n"));
            offset += data.Length;
            ofs.Write(data, 0, data.Length);
            ofs.Write(buf, m.Index, buf.Length - m.Index);
            //
            ofs.Flush();
            ofs.Close();
            //edit xref
            buf = File.ReadAllBytes(outPath);
            s = Encoding.ASCII.GetString(buf);

            m = unixLineFeed? s_reg_Xref_Unix.Match(s) : s_reg_Xref_Window.Match(s);
            int newxref = int.Parse(m.Groups[1].Value) + offset;
            try { File.Delete(outPath); } catch { }
            outPath = pdfFilePath.TrimEnd(".pdf".ToCharArray()) + ".out.pdf";
            ofs = File.Open(outPath, FileMode.Create, FileAccess.Write, FileShare.Read);
            ofs.Write(buf, 0, m.Index);
            data = Encoding.ASCII.GetBytes($"startxref\n{newxref}\n%%EOF\n");
            ofs.Write(data, 0, data.Length);
            ofs.Close();
            var time = File.GetCreationTime(pdfFilePath);
            File.SetCreationTime(outPath, time);
            File.SetLastWriteTime(outPath, time);

            return outPath;
        }

        public static string ToBase64File(this string filePath, bool largeFile = false)
        {
            if (largeFile)
            {
                StringBuilder sb = new StringBuilder();
                FileStream inStream = new FileStream(filePath, FileMode.Open);
                byte[] buf = new byte[1024];
                int readByte = 0;
                while ((readByte = inStream.Read(buf, 0, buf.Length)) > 0)
                {
                    sb.AppendLine(Convert.ToBase64String(buf, 0, readByte));
                }
                inStream.Close();
                return sb.ToString();
            }
            else return Convert.ToBase64String(File.ReadAllBytes(filePath));
        }
        public static byte[] FromBase64File(this string filePath, bool largeFile = false)
        {
            if (largeFile)
            {
                MemoryStream outStream = new MemoryStream();
                StreamReader inStream = new StreamReader(filePath);
                byte[] buf = new byte[1024];
                string line;
                while ((line = inStream.ReadLine()) != null)
                {
                    outStream.Write(Convert.FromBase64String(line));
                }
                inStream.Close();
                byte[] result = outStream.ToArray();
                outStream.Close();
                return result;
            }
            else return Convert.FromBase64String(File.ReadAllText(filePath));
        }
    }
}
