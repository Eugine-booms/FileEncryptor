using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileEncryptor.Services.Interfaces
{
    interface IUserDialog
    {
        bool OpenFile(string title, out string selectdFile, string filter = "Все файлы (*.*)|*.*");
        bool OpenFiles(string title, out IEnumerable <string> selectdFiles, string filter = "Все файлы (*.*)|*.*");

        bool SaveFile(string title, out string selectdFile, string defaultFileName=null, string filter = "Все файлы (*.*)|*.*");

        void Information(string title, string message);
        void Warning(string title, string message);
        void Error(string title, string message);
    }
}
