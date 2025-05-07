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
    public class EncodeFileViewModel: BaseViewModel
    {

        IFileService _fileService = null;
        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set
            {
                SetProperty(ref _filePath, value);
                FileMode = "[READY]";
                FileContent = "";
            }
        }
        private string _fileContent;
        public string FileContent
        {
            get => _fileContent;
            set => SetProperty(ref _fileContent, value);
        }

        private string _fileMode = "[READY]";
        public string FileMode
        {
            get => _fileMode;
            set => SetProperty(ref _fileMode, value);
        }

        public ICommand SelectFileCommand { get; }

        public ICommand ActionCommand { get; }
        
        public EncodeFileViewModel(IFileService fileService)
        {
            SelectFileCommand = new RelayCommand(SelectFile);
            ActionCommand = new RelayCommand(Action, HasFile);
            _fileService = fileService;
        }
        void SelectFile(object _)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            // Set filter for file extension and default file extension 
            //dlg.DefaultExt = ".png";
            //dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                FilePath = dlg.FileName;
                if (FilePath.EndsWith(".b64")) FileMode = "Decode";
                else FileMode = "Encode";
                FileContent = null; 
            }
        }
        void EncodeFile()
        {
            var r = _fileService.EncodeFile(FilePath);
            r.FilePath += ".b64";
            _fileService.SaveFile(r);
            FileContent = r.Base64Content;
        }
        void DecodeFile()
        {
            var r = _fileService.DecodeFile(FilePath);
            r.FilePath = r.FilePath.Replace(".b64", "");
            _fileService.SaveFile(r);
            FileContent = $"[File was decoded completely]\r\n[Save as: {r.FilePath}]";
        }
        async void Action(object _)
        {
            IsProcessing = true;

            await Task.Run(() =>
            {
                switch (FileMode)
                {
                    case "Encode": EncodeFile(); break;
                    case "Decode": DecodeFile(); break;
                }
            });
            
            IsProcessing = false;
        }
        private bool HasFile(object _) => !string.IsNullOrEmpty(FilePath);

        
    }
}
