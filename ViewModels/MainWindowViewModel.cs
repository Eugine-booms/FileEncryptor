using System;
using System.Collections.Generic;
using System.Text;

namespace FileEncryptor.ViewModels
{
   internal class MainWindowViewModel : Base.ViewModelBase
    {


        #region  string Title Заголовок Окна
        ///<summary> Заголовок Окна
        private string _Title = "Стартовое Окно";
        ///<summary> Заголовок Окна
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value, nameof(Title));
        }
        #endregion





    }
}

