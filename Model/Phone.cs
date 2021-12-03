using System;

namespace FileEncryptor.Model
{
    internal class Phone : Base.BaseModel
    {
        #region  string  Title название
        ///<summary> название
        private string  _Title;
        ///<summary> название
        public string  Title
        {
            get => _Title;
            set => Set(ref _Title, value, nameof(Title));
        }
        #endregion

        #region  string  Company производитель
        ///<summary> производитель
        private string  _Company;
        ///<summary> производитель
        public string  Company
        {
            get => _Company;
            set => Set(ref _Company, value, nameof(Company));
        }
        #endregion

        #region  int Price Цена     
        ///<summary> Цена
        private int _Price;
        ///<summary> Цена
        public int Price
        {
            get => _Price;
            set => Set(ref _Price, value, nameof(Price));
        }
        #endregion
        


    }
}
