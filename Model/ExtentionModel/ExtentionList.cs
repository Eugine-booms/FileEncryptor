using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace FileEncryptor.Model.ExtentionModel
{
    internal class ExtentionList<T> : List<T>, INotifyPropertyChanged  where T : INotifyPropertyChanged
    {
        public ExtentionList() : base()
        {
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public ExtentionList(List<T> list)
        {
            foreach (var item in list)
            {
                this.Add(item);
            }
        }
        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
       }
        public new void  Add(T item)
        {
            item.PropertyChanged += Item_PropertyChanged;
            base.Add(item);
        }
        public new bool Remove(T item)
        {
            item.PropertyChanged -= Item_PropertyChanged;
            return  base.Remove(item);
        }
        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e);
        }


        public Delegate[] getlist()
        {
           
            return PropertyChanged?.GetInvocationList();
        }
    }
}
