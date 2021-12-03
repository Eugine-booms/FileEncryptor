using FileEncryptor.Model;
using FileEncryptor.Model.ExtentionModel;
using FileEncryptor.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FileEncryptor.ViewModels
{
    internal class PhoneViewModel:Base.ViewModelBase
    {


        #region  Delegate[] Delegat ""
        ///<summary> ""
        private List<Delegate> _Delegat;
        ///<summary> ""
        public List<Delegate> Delegat

        {
            get => _Delegat;
            set => Set(ref _Delegat, value, nameof(Delegat));
        }
        #endregion


        #region  Phone SelectedPhone Выбранный телефон
        ///<summary> Выбранный телефон
        private Phone _SelectedPhone;
        ///<summary> Выбранный телефон
        public Phone SelectedPhone
        {
            get => _SelectedPhone;
            set => Set(ref _SelectedPhone, value, nameof(SelectedPhone));
        }
        #endregion
        #region  string  Title
        ///<summary> ""
        private string  _Title="Телефоны";
        ///<summary> ""
        public string  Title
        {
            get => _Title;
            set => Set(ref _Title, value, nameof(Title));
        }
        #endregion
        #region  List<Phone> TelephoneList Список телефонов
        ///<summary> Список телефонов
        private ExtentionList<Phone> _TelephoneList;
        private readonly ISever sever;

        ///<summary> Список телефонов
        public ExtentionList<Phone>  TelephoneList
        {
            get => _TelephoneList;
            set
            {
                Set(ref _TelephoneList, value, nameof(TelephoneList));
                
            }
        }
        #endregion

        public PhoneViewModel(ISever sever)
        {
            this.sever = sever;
            var rnd = new Random();
                var list= Enumerable.Range(
                1,
                10)
                .Select(p => new Phone()
                {
                    Title = $"Phone {p}",
                    Company = $"Company {p}",
                    Price = p * rnd.Next(5000, 50000)
                }).ToList();
            TelephoneList = new ExtentionList<Phone> (list);
            
            //-Работает только так
            TelephoneList.PropertyChanged += TelephoneList_PropertyChanged; 
               


        }

        private void TelephoneList_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            sever.Save(TelephoneList.ToList());
            var list = new List<Delegate>();
            foreach (var item in TelephoneList)
            {
               list.AddRange(item.getlist().ToList());
            }
            list.AddRange(TelephoneList.getlist());
            Delegat = list;
        }
    }
}
