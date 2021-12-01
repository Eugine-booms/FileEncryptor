using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Markup;

namespace FileEncryptor.ViewModels.Base
{
   internal abstract class  ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

      protected void OnPropertyChange([CallerMemberName] string PropertyName=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

       protected virtual bool Set<T>(ref T fild, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(fild, value)) return false;
            fild = value;
            OnPropertyChange(PropertyName);
            return true;
        }
       
    }
}
