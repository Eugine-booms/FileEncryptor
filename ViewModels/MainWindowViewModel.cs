using FileEncryptor.Infrastructure.Commands;
using FileEncryptor.Services;
using FileEncryptor.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;

namespace FileEncryptor.ViewModels
{
    internal class MainWindowViewModel : Base.ViewModelBase
    {

        private readonly IUserDialog _userDialog;
        #region  string Password Пароль
        ///<summary> Пароль
        private string _Passvord = "123";
        ///<summary> Пароль
        public string Password
        {
            get => _Passvord;
            set => Set(ref _Passvord, value, nameof(Password));
        }
        #endregion


        #region  FileInfo SelectedFile Выбранный файл
        ///<summary> Выбранный файл
        private FileInfo _SelectedFile;
        ///<summary> Выбранный файл
        public FileInfo SelectedFile
        {
            get => _SelectedFile;
            set => Set(ref _SelectedFile, value, nameof(SelectedFile));
        }
        #endregion



        #region  string Title Заголовок Окна
        ///<summary> Заголовок Окна
        private string _Title = "Шифратор";
        ///<summary> Заголовок Окна
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value, nameof(Title));
        }
        #endregion

        #region Команды

        #region Команда выбора файла
        private ICommand _selectFileCommand;


        public ICommand SelectFileCommand =>
            _selectFileCommand ??=
            new LambdaCommand(OnSelectFileCommandExecute);
        private void OnSelectFileCommandExecute()
        {
            if (!_userDialog.OpenFile("Выбор файла", out var filePath)) return;
            var selected_file= new FileInfo(filePath);
            SelectedFile = selected_file.Exists ? selected_file : null;


        }


        #endregion
        #endregion

        #region Конструктор
        public MainWindowViewModel(IUserDialog userDialog)
        {
            this._userDialog = userDialog;
        }

        public MainWindowViewModel()
        {
            this._userDialog = new UserDialogService();
        }
        #endregion
    }
}

