using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EncodeFile.Helpers
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel Parent { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            field = value;
            OnPropertyChanged(propertyName);
        }


        private bool _isProcessing = false;
        public bool IsProcessing
        {
            get => Parent == null ? _isProcessing : Parent.IsProcessing;
            set { if (Parent == null) SetProperty(ref _isProcessing, value); else Parent.IsProcessing = value; }
        }


    }
}
