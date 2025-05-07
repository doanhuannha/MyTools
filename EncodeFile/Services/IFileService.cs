using EncodeFile.Models;

namespace EncodeFile.Services
{
    public interface IFileService
    {
        FileDetail DecodeFile(string filePath);
        FileDetail EncodeFile(string filePath);
        bool SaveFile(FileDetail detail);
    }
}