using FileEncryptor.Services.Interfaces;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
