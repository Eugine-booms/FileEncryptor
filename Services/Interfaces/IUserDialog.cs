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
    }
}
