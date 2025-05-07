using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ImportData.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static System.Net.WebRequestMethods;

namespace ImportData.ViewModels
{
    public partial class ListBoxViewModel : ObservableObject
    {
        public ListBoxViewModel(string selectedFolder)
        {
            var files = Directory.GetFiles(selectedFolder);
            foreach (var f in files)
            {
                Files.Add(new FileInfo(f));
            }
        }


        [ObservableProperty]
        private ObservableCollection<FileInfo> _files = new();

        [RelayCommand]
        private void RemoveFolder()
        {
            bool itemRemoved = false;
            while (SelectedFiles.Count > 0)
            {
                itemRemoved = true;
                Files.Remove(SelectedFiles[0]);
            }
            if(itemRemoved) OnPropertyChanged(nameof(Files));

        }

        [ObservableProperty]
        private ObservableCollection<FileInfo> _selectedFiles = new();

    }
    public partial class MainViewModel : ObservableObject
    {

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ImportDataCommand))]
        private string _connString = "Data Source=.;Initial Catalog=MISC;Integrated Security=True;Trust Server Certificate=True";


        [ObservableProperty]
        private bool _isRunning = false;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ImportDataCommand))]
        private string _tableName;

        [ObservableProperty]
        private string _delimiterChar = "|";

        [ObservableProperty]
        private byte _lineToSkip = 1;

        [ObservableProperty]
        private string _message;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ImportDataCommand))]
        private string _selectedFolder;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ImportDataCommand))]
        private ListBoxViewModel _folderData;



        [RelayCommand]
        private void SelectFolder()
        {
            
            OpenFolderDialog dlg = new OpenFolderDialog();
            dlg.Multiselect = false;

            dlg.InitialDirectory = SelectedFolder;
            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                
                FolderData = new ListBoxViewModel(dlg.FolderName);
                FolderData.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == nameof(FolderData.Files))
                    {
                        ImportDataCommand.NotifyCanExecuteChanged();
                    }
                };
                SelectedFolder = dlg.FolderName;
            }
        }
        [RelayCommand(CanExecute = nameof(IsReady))]
        private async Task ImportData()
        {

            IsRunning = true;
            char delimiter = '|';
            if (DelimiterChar.Length == 1) delimiter = DelimiterChar[0];
            else if (DelimiterChar.StartsWith("\\"))
            {
                var v = DelimiterChar.Substring(1);
                switch (v)
                {
                    case "r": delimiter = '\r'; break;
                    case "n": delimiter = '\n'; break;
                    case "t": delimiter = '\t'; break;
                    default:
                        delimiter = (char)byte.Parse(DelimiterChar.Substring(1));
                        break;
                }
            }

            DataService ds = new DataService(FolderData.Files.Select(f => f.FullName).ToArray(), delimiter, LineToSkip);
            var cnt = await ds.Import(ConnString, TableName);
            Message = $"{cnt} row(s) were imported into \"{TableName}\" table successfully ";
            IsRunning = false;
        }
        
        private bool IsReady()
        {
            return !(string.IsNullOrEmpty(ConnString) || string.IsNullOrEmpty(SelectedFolder) || string.IsNullOrEmpty(TableName)) && FolderData != null && FolderData.Files.Count > 0;
        }

    }
}
