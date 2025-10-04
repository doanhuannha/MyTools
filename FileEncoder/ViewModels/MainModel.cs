using EncodeFile.Models;
using EncodeFile.Services;
using EncodeFile.Helpers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace EncodeFile.ViewModels
{
    public class MainModel : BaseViewModel
    {
        public MainModel(Base64FileModel b64, PdfFileModel pdf)
        {
            b64.Parent = this;
            B64 = b64;

            pdf.Parent = this;
            Pdf = pdf;


        }
        public Base64FileModel B64 {  get; set; }
        public PdfFileModel Pdf { get; set; }
    }
}
