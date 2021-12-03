using FileEncryptor.Infrastructure.Commands;
using FileEncryptor.Infrastructure.Commands.Base;
using FileEncryptor.Services;
using FileEncryptor.Services.Interfaces;
using FileEncryptor.Views;

using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Xml.Linq;

namespace FileEncryptor.ViewModels
{
    internal class MainWindowViewModel : Base.ViewModelBase
    {

        private readonly IUserDialog _userDialog;
        private readonly IEncryptor _encryptor;
        private const string __encryptorDefaultFileSuffix = ".encript";
        private CancellationTokenSource  processCancelation;

        #region  double Progress Прогресс
        ///<summary> Прогресс
        private double _ProgressValue;
        ///<summary> Прогресс
        public double ProgressValue
        {
            get => _ProgressValue;
            set => Set(ref _ProgressValue, value, nameof(ProgressValue));
        }
        #endregion


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
        private double _Timer = 0;
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
        new LambdaCommand(OnEncryptCommandExecuted, CanEncryptCommandExecute);
        private async void OnEncryptCommandExecuted(object p)
        {
            var file = p as FileInfo ?? SelectedFile;
            if (file is null) return;
            var defaultFileName = file.FullName + __encryptorDefaultFileSuffix;
            if (!(_userDialog.SaveFile(
                "Сохранение файла", out var destanation_filePath, defaultFileName))) return;
            
            
            var timer = Stopwatch.StartNew();


            processCancelation = new CancellationTokenSource();
            var cancelToken = processCancelation.Token;
            DeactevateCommands(new ICommand[] { DecryptCommand, EncryptCommand, SelectFileCommand });
            var progress = new Progress<double>(p => ProgressValue = p);
            try
            {
                var encryptor_task = _encryptor.CryptAcync(
                    Methodd.Encrypd,
                    file.FullName,
                    destanation_filePath,
                    Password,
                    102400,
                    progress,
                    processCancelation.Token);
                await encryptor_task;
            }
            catch (OperationCanceledException)
            {
                _userDialog.Error("Шифратор", "Операция была отменена");

            }
            finally
            {
                processCancelation.Dispose();
                processCancelation = null;
            }

            ActivateCommands(new ICommand[] { DecryptCommand, EncryptCommand, SelectFileCommand });




            timer.Stop();
            //_userDialog.Information("Шифрование", $"Шифрование выполнено успешно {timer.Elapsed.TotalMilliseconds} мс");
            Timer = timer.Elapsed.TotalMilliseconds;


        }
        private bool CanEncryptCommandExecute(object p)
        {

            return (p is FileInfo file && file.Exists) && !string.IsNullOrWhiteSpace(Password);
        }
        #endregion

        #region Команда Decrypt
        private ICommand _DecryptCommand;
        /// <summary>"Описание"</summary>
        public ICommand DecryptCommand =>
        _DecryptCommand ??=
        new LambdaCommand(OnDecryptCommandExecuted, CanDecryptCommandExecute);
        private async void OnDecryptCommandExecuted(object p)
        {
            var file = p as FileInfo ?? SelectedFile;
            if (file is null) return;
            var defaultFileName = file.FullName.EndsWith(__encryptorDefaultFileSuffix) ?
                file.FullName.Substring(0, file.FullName.Length - __encryptorDefaultFileSuffix.Length) : file.FullName;
            if (!(_userDialog.SaveFile(
                "Сохранение файла", out var destanation_filePath, defaultFileName))) return;
            var timer = Stopwatch.StartNew();


            DeactevateCommands(new ICommand[] { DecryptCommand, EncryptCommand, SelectFileCommand });

            var action = new Action<double>(SetProgress);
            var progress = new Progress<double>(action);
            var progress1 = new Progress<double>(new Action<double>(p => ProgressValue = p));

            processCancelation = new CancellationTokenSource();
            var token = processCancelation.Token;
            
            var decryptionTask = _encryptor.CryptAcync(
                                                               mode: Methodd.DeCrypt,
                                                               sourcePath: file.FullName,
                                                               destinationPath: destanation_filePath,
                                                               password: Password,
                                                               bufferLenght: 102400,
                                                               progress: progress,
                                                               Cancel: token);
            bool success = false;
            try
            {
                success = await decryptionTask;
                Thread.Sleep(1000);
                processCancelation.Cancel();
            }
            catch (OperationCanceledException)
            {

                _userDialog.Information("Шифратор", "Операция была отменена");
            }
            finally
            {
                processCancelation.Dispose();
                processCancelation = null;
            }

            ActivateCommands(new ICommand[] { DecryptCommand, EncryptCommand, SelectFileCommand });


            timer.Stop();
            if (success)
            {
                //    _userDialog.Information("Шифрование", $"Дешифрирование успешно выполнено! за  {timer.Elapsed.TotalMilliseconds} мс");
                Timer = timer.ElapsedMilliseconds;
            }

            //  _userDialog.Error("Шифрование", "Дешифрирование выполнено с ошибкой! Указан неверный пароль");
        }
        private bool CanDecryptCommandExecute(object p)
        {
            return (p is FileInfo file && file.Exists || SelectedFile != null) && !string.IsNullOrWhiteSpace(Password);
        }
        #endregion


        #region Команда CancelCrypt
        private ICommand _CancelCryptCommand;
        /// <summary>"Описание"</summary>
        public ICommand CancelCryptCommand =>
        _CancelCryptCommand ??=
        new LambdaCommand(OnCancelCryptCommandExecuted, CanCancelCryptCommandExecute);
        private void OnCancelCryptCommandExecuted(object p) => processCancelation.Cancel();
        private bool CanCancelCryptCommandExecute(object p) => processCancelation != null;


        #endregion


        #region Команда OpenPhoneDialogCommand
        private ICommand _OpenPhoneDialogCommand;
        /// <summary>"Описание"</summary>
        public ICommand OpenPhoneDialogCommand =>
        _OpenPhoneDialogCommand ??=
        new LambdaCommand(OnOpenPhoneDialogCommandExecute, CanOpenPhoneDialogCommandExecuted);
        private void OnOpenPhoneDialogCommandExecute(object p)
        {
            var phoneWindow = new PhoneView();
            phoneWindow.Show();
        }
        private bool CanOpenPhoneDialogCommandExecuted(object p) => true;

        #endregion


        #endregion

        #region Конструктор
        public MainWindowViewModel(IUserDialog userDialog, IEncryptor encryptor)
        {
            this._userDialog = userDialog;
            this._encryptor = encryptor;
        }

        #endregion

        private void SetProgress(double p)
        {
            ProgressValue = p;
        }
        private bool ActivateCommands(ICommand[] lambdaCommands)
        {
            foreach (var command in lambdaCommands)
            {
                ((Command)command).Executable = true;
            }
            return true;
        }
        private bool DeactevateCommands(ICommand[] lambdaCommands)
        {
            foreach (var command in lambdaCommands)
            {
                ((Command)command).Executable = false;
            }
            return true;
        }

    }
}

