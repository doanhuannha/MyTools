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
   
    public class PdfFileModel : BaseViewModel
    {
        const string MODE_READY = "[READY]";
        const string MODE_ATTACH = "Attach";
        const string MODE_DETACH = "Detach";
        IPdfService _fileService = null;
        private string _pdfFilePath;
        public string PdfFilePath
        {
            get => _pdfFilePath;
            set
            {
                SetProperty(ref _pdfFilePath, value);
                FileMode = MODE_READY;
            }
        }

        

        private string _secret;
        public string Secret
        {
            get => _secret;
            set
            {
                SetProperty(ref _secret, value);
            }
        }

        private string _dataFilePath;
        public string DataFilePath
        {
            get => _dataFilePath;
            set
            {
                SetProperty(ref _dataFilePath, value);
            }
        }

        private string _detachFileName;
        public string DetachFileName
        {
            get => _detachFileName;
            set
            {
                SetProperty(ref _detachFileName, value);
            }
        }

        private string _fileMode = MODE_READY;
        public string FileMode
        {
            get => _fileMode;
            set {
                SetProperty(ref _fileMode, value);
                OnPropertyChanged(nameof(AttachMode));
                OnPropertyChanged(nameof(DetachMode));
            }
        }

        private string _resultMessage = "...";
        public string ResultMessage
        {
            get => _resultMessage;
            set
            {
                SetProperty(ref _resultMessage, value);
            }
        }

        public bool AttachMode
        {
            get => _fileMode == MODE_ATTACH;
        }

        public bool DetachMode
        {
            get => _fileMode == MODE_DETACH;
        }

        public ICommand SelectPdfFileCommand { get; }
        public ICommand SelectDataFileCommand { get; }

        public ICommand ActionCommand { get; }
        
        public PdfFileModel(IPdfService fileService)
        {
            SelectPdfFileCommand = new RelayCommand(SelectPdfFile);
            SelectDataFileCommand = new RelayCommand(SelectDataFile);
            ActionCommand = new RelayCommand(Action, HasFile);
            _fileService = fileService;
        }
        void SelectPdfFile(object _)
        {
            SelectFile(true);
        }
        void SelectDataFile(object _)
        {
            SelectFile(false);
        }
        void SelectFile(bool pdfMode)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            // Set filter for file extension and default file extension 
            if (pdfMode)
            {
                dlg.DefaultExt = ".pdf";
                dlg.Filter = "Pdf (*.pdf)|*.pdf";
            }
            

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                if (pdfMode)
                {
                    PdfFilePath = dlg.FileName;
                    if (PdfFilePath.Contains(".out.")) FileMode = MODE_DETACH;
                    else FileMode = MODE_ATTACH;

                    switch (FileMode)
                    {
                        case MODE_DETACH: DataFilePath = ""; break;
                        case MODE_ATTACH: DetachFileName = ""; break;
                    }
                }
                else
                {
                    DataFilePath = dlg.FileName;
                }
                
            }
        }
        void AttachFile()
        {
            var outFilePath = _fileService.Attach(PdfFilePath, DataFilePath, Secret);
            ResultMessage = $"Output file: {outFilePath}";


        }
        void DetachFile()
        {
            var outFilePath = _fileService.Detach(PdfFilePath, DetachFileName, Secret);
            ResultMessage = $"Output file: {outFilePath}";
        }
        async void Action(object _)
        {
            IsProcessing = true;
            await Task.Run(() =>
            {
                switch (FileMode)
                {
                    case MODE_DETACH: DetachFile(); break;
                    case MODE_ATTACH: AttachFile(); break;
                }
            });
            
            IsProcessing = false;
        }
        private bool HasFile(object _)
        {
            switch (FileMode)
            {
                case MODE_DETACH: return !string.IsNullOrEmpty(PdfFilePath) && !string.IsNullOrEmpty(DetachFileName) && !string.IsNullOrEmpty(Secret);
                case MODE_ATTACH: return !string.IsNullOrEmpty(PdfFilePath) && !string.IsNullOrEmpty(DataFilePath) && !string.IsNullOrEmpty(Secret);
            }
             return false;
        }

        
    }
}
