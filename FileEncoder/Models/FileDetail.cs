using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncodeFile.Models
{
    public class FileDetail
    {
        public string FilePath { get; set; }
        public string Base64Content { get; set; }
        public byte[] FileContent { get; set; }
    }
}
