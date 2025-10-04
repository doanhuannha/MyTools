using EncodeFile.Helpers;
using EncodeFile.Models;
using System.IO;

namespace EncodeFile.Services
{
    public interface IPdfService
    {
        string Attach(string pdfPath, string dataFilePath, string locker);
        string Detach(string pdfPath, string detachFileName, string key);
    }
    public class PdfService : IPdfService
    {
        public string Attach(string pdfPath, string dataFilePath, string locker)
        {
            return FileHelper.AttachPdf(pdfPath, dataFilePath, locker);
        }
        public string Detach(string pdfPath, string detachFileName, string key)
        {
            return FileHelper.DetachPdf(pdfPath, detachFileName, key);
        }

    }
}