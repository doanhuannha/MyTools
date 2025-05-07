using EncodeFile.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncodeFile.Services
{
    public class FileService : IFileService
    {


        public FileDetail EncodeFile(string filePath)
        {
            FileDetail file = new FileDetail() { FilePath = filePath };
            file.Base64Content = ToBase64File(filePath);
            return file;
        }
        public FileDetail DecodeFile(string filePath)
        {
            FileDetail file = new FileDetail() { FilePath = filePath };
            file.FileContent = FromBase64File(filePath);
            return file;
        }
        public bool SaveFile(FileDetail detail)
        {
            if (!string.IsNullOrEmpty(detail.Base64Content)) File.WriteAllText(detail.FilePath, detail.Base64Content);
            else if (detail.FileContent != null) File.WriteAllBytes(detail.FilePath, detail.FileContent);
            return true;
        }
        static string ToBase64File(string filePath)
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
        static byte[] FromBase64File(string filePath)
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
    }
}
