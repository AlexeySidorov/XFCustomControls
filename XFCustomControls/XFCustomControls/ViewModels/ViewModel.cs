using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XFCustomControls.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void SetProperty<T>(ref T backValue, T value, [CallerMemberName] String propertyName = "")
        {
            backValue = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
