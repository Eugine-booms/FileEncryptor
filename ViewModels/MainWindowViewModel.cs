using FileEncryptor.Infrastructure.Commands;
using FileEncryptor.Services.Interfaces;

using System.Diagnostics;
using System.IO;
using System.Windows.Input;

namespace FileEncryptor.ViewModels
{
    internal class MainWindowViewModel : Base.ViewModelBase
    {

        private readonly IUserDialog _userDialog;
        private readonly IEncryptor _encryptor;
        private const string __encryptorDefaultFileSuffix=".encript";

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


        #region  double Timer Таймер
        ///<summary> Таймер
        private double _Timer=0;
        ///<summary> Таймер
        public double Timer
        {
            get => _Timer;
            set => Set(ref _Timer, value, nameof(Timer));
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
            var selected_file = new FileInfo(filePath);
            SelectedFile = selected_file.Exists ? selected_file : null;
        }





        #endregion

        #region Команда Encrypt
        private ICommand _EncryptCommand;
        /// <summary>"Описание"</summary>
        public ICommand EncryptCommand =>
        _EncryptCommand ??=
        new LambdaCommand(OnEncryptCommandExecute, CanEncryptCommandExecuted);
        private void OnEncryptCommandExecute(object p)
        {
            var file = p as FileInfo ?? SelectedFile;
            if (file is null) return;

            var defaultFileName = file.FullName + __encryptorDefaultFileSuffix;
            if (!(_userDialog.SaveFile(
                "Сохранение файла", out var destanation_filePath, defaultFileName))) return;
            var timer = Stopwatch.StartNew();
                _encryptor.Encrypt(file.FullName, destanation_filePath, Password);
            timer.Stop();
            _userDialog.Information("Шифрование", $"Шифрование выполнено успешно {timer.Elapsed.TotalMilliseconds} мс");
            Timer = timer.Elapsed.TotalMilliseconds;


        }
        private bool CanEncryptCommandExecuted(object p)
        {

            return (p is FileInfo file && file.Exists) && !string.IsNullOrWhiteSpace(Password);
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
            var file = p as FileInfo ?? SelectedFile;
            if (file is null) return;
            var defaultFileName = file.FullName.EndsWith(__encryptorDefaultFileSuffix) ? 
                file.FullName.Substring(0, file.FullName.Length-__encryptorDefaultFileSuffix.Length) : file.FullName;
            if (!(_userDialog.SaveFile(
                "Сохранение файла", out var destanation_filePath, defaultFileName))) return;
            var timer = Stopwatch.StartNew();
            var success=_encryptor.Decrypt(file.FullName, destanation_filePath, Password);
            timer.Stop();
            if (success)
            {
                _userDialog.Information("Шифрование", $"Дешифрирование успешно выполнено! за  {timer.Elapsed.TotalMilliseconds} мс");
            Timer = timer.ElapsedMilliseconds;
            }
            else
                _userDialog.Error("Шифрование", "Дешифрирование выполнено с ошибкой! Указан неверный пароль");
        }
        private bool CanDecryptCommandExecuted(object p)
        {
            return (p is FileInfo file && file.Exists || SelectedFile != null) && !string.IsNullOrWhiteSpace(Password);
        }
        #endregion

        #endregion

        #region Конструктор
        public MainWindowViewModel(IUserDialog userDialog, IEncryptor encryptor)
        {
            this._userDialog = userDialog;
            this._encryptor = encryptor;
        }

        #endregion
    }
}

