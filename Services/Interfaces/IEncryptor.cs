using System;
using System.Collections.Generic;
using System.Text;

namespace FileEncryptor.Services.Interfaces
{
    internal interface IEncryptor
    {


        void Encrypt(string sourcePath, string DestinationPath, string password, int bufferLenght = 102400);
        bool Decrypt(string sourcePath, string DestinationPath, string password, int bufferLenght = 102400);
    }
}
