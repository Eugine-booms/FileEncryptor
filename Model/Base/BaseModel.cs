using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace FileEncryptor.Model.Base
{
    abstract class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName=null)
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propertyName));
        }
        protected bool Set<T>(ref T fild, T value, [CallerMemberName] string propertyName =null)
        {
            if (Equals(fild, value)) return false;
            fild = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        public Delegate[] getlist()
        {
            return PropertyChanged?.GetInvocationList();
        }
    }
}
