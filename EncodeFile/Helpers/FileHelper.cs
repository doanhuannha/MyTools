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
        static Regex s_reg_Xref = new Regex(@"startxref\n(\d+)\n", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled);
        static void Transform(byte[] data, string key, int from = 0, int length = -1)
        {
            if (length == -1) length = data.Length;
            byte[] k = Encoding.ASCII.GetBytes(key);
            for (int i = from; i < from + length; i++) data[i] = (byte)((i - from) ^ data[i] ^ ~k[(i - from) % k.Length] ^ length);
        }
        public static void DetachPdf(this string pdfFilePath, string detachFileName, string skey)
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
            var ofs = File.Open(Path.Combine(new FileInfo(pdfFilePath).DirectoryName, detachFileName), FileMode.Create, FileAccess.Write,
            FileShare.Read);
            ofs.Write(buf, dataIndex, dataLength);
            ofs.Close();
        }
        public static void AttachPdf(this string pdfFilePath, string dataFilePath, string skey)
        {
            string outPath = pdfFilePath.TrimEnd(".pdf".ToCharArray()) + ".tmp";
            byte[] raw = File.ReadAllBytes(dataFilePath);
            var ofs = File.Open(outPath, FileMode.Create, FileAccess.Write, FileShare.Read);
            byte[] buf = File.ReadAllBytes(pdfFilePath);
            var s = Encoding.ASCII.GetString(buf);
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
".Replace("\r\n", "\n"));
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
".Replace("\r\n", "\n"));
            offset += data.Length;
            ofs.Write(data, 0, data.Length);
            ofs.Write(buf, m.Index, buf.Length - m.Index);
            //
            ofs.Flush();
            ofs.Close();
            //edit xref
            buf = File.ReadAllBytes(outPath);
            s = Encoding.ASCII.GetString(buf);
            m = s_reg_Xref.Match(s);
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
        }
    }
}
