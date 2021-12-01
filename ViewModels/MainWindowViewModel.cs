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


        #region Команда Encrypt
        private ICommand _EncryptCommand;
        /// <summary>"Описание"</summary>
        public ICommand EncryptCommand =>
        _EncryptCommand ??=
        new LambdaCommand(OnEncryptCommandExecute, CanEncryptCommandExecuted);
        private void OnEncryptCommandExecute(object p)
        {
            if (!(p is FileInfo file)) return;
        }
        private bool CanEncryptCommandExecuted(object p)
        {
            return p is FileInfo file && file.Exists && !string.IsNullOrWhiteSpace(Password);
        }
        #endregion

        #region Команда Decrypt
        private ICommand _DecryptCommand;
        /// <summary>"Описание"</summary>
        public ICommand DecryptCommand =>
        _DecryptCommand ??=
        new LambdaCommand(OnDecryptCommandExecute, CanDecryptCommandExecuted);
        private void OnDecryptCommandExecute(object p)
        {
            if (!(p is FileInfo file)) return;
        }
        private bool CanDecryptCommandExecuted(object p)
        {
            return p is FileInfo file && file.Exists && !string.IsNullOrWhiteSpace(Password);
        }
        #endregion


        #endregion


        #endregion

        #region Конструктор
        public MainWindowViewModel(IUserDialog userDialog)
        {
            this._userDialog = userDialog;
        }
       
        #endregion
    }
}

