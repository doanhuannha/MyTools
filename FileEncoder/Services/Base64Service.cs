using EncodeFile.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using EncodeFile.Helpers;

namespace EncodeFile.Services
{

    public interface IBase64Service
    {
        FileDetail DecodeFile(string filePath);
        FileDetail EncodeFile(string filePath);
        bool SaveFile(FileDetail detail);
    }
    public class Base64Service : IBase64Service
    {
        public FileDetail EncodeFile(string filePath)
        {
            FileDetail file = new FileDetail() { FilePath = filePath };
            file.Base64Content = filePath.ToBase64File();
            return file;
        }
        public FileDetail DecodeFile(string filePath)
        {
            FileDetail file = new FileDetail() { FilePath = filePath };
            file.FileContent = filePath.FromBase64File();
            return file;
        }
        public bool SaveFile(FileDetail detail)
        {
            if (!string.IsNullOrEmpty(detail.Base64Content)) File.WriteAllText(detail.FilePath, detail.Base64Content);
            else if (detail.FileContent != null) File.WriteAllBytes(detail.FilePath, detail.FileContent);
            return true;
        }
        
    }
}
