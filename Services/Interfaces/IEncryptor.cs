using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileEncryptor.Services.Interfaces
{
    internal interface IEncryptor
    {


        void Encrypt(string sourcePath, string DestinationPath, string password, int bufferLenght = 102400);
        bool Decrypt(string sourcePath, string DestinationPath, string password, int bufferLenght = 102400);

        Task EncryptAcync(string sourcePath, string DestinationPath, string password, int bufferLenght = 102400);
        Task <bool> DecryptAcync(string sourcePath, string DestinationPath, string password, int bufferLenght = 102400);
    }
}
