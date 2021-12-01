using FileEncryptor.Services.Interfaces;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace FileEncryptor.Services
{
    class UserDialogService : IUserDialog
    {
       
        public bool OpenFile(string title, out string selectdFile, string filter = "Все файлы (*.*)|*.*")
        {
            var file_dialog = new OpenFileDialog()
            {
                Title = title,
                Filter = filter
            };
            if (file_dialog.ShowDialog() !=true)
            {
                selectdFile = null;
                return false;
            }
            selectdFile = file_dialog.FileName;
            return true;
        }

        public bool OpenFiles(string title, out IEnumerable<string> selectdFiles, string filter = "Все файлы (*.*)|*.*")
        {
            var file_dialog = new OpenFileDialog()
            {
                Title = title,
                Filter = filter
            };
            if (file_dialog.ShowDialog() != true)
            {
                selectdFiles = Enumerable.Empty<string>();
                return false;
            }
            selectdFiles = file_dialog.FileNames;
            return true;
        }

        public bool SaveFile(string title, out string selectdFile, string defaultFileName = null, string filter = "Все файлы (*.*)|*.*")
        {
            
            
            var file_dialog = new SaveFileDialog()
            {
                Title = title,
                Filter = filter
            };
            if (defaultFileName != null)
                file_dialog.FileName = defaultFileName;
            if (file_dialog.ShowDialog() != true)
            {
                selectdFile = null;
                return false;
            }
            selectdFile = file_dialog.FileName;
            return true;
        }

        public void Warning(string title, string message) => MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
        public void Error(string title, string message) => MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        public void Information(string title, string message) => MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);

    }
}
