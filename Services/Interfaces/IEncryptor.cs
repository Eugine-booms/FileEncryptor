using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileEncryptor.Services.Interfaces
{
    internal interface IEncryptor
    {


      //  void Encrypt(string sourcePath, string DestinationPath, string password, int bufferLenght = 102400);
      //  bool Decrypt(string sourcePath, string DestinationPath, string password, int bufferLenght = 102400);

        //Task EncryptAcync(string sourcePath,
        //                  string DestinationPath,
        //                  string password,
        //                  int bufferLenght = 102400,
        //                  IProgress<double> progress = null,
        //                  CancellationToken Cancel = default);
        //Task <bool> DecryptAcync(string sourcePath,
        //                         string DestinationPath,
        //                         string password,
        //                         int bufferLenght = 102400,
        //                         IProgress<double> progress = null,
        //                         CancellationToken Cancel = default);
        Task<bool> CryptAcync(
           Methodd mode,
           string sourcePath,
           string destinationPath,
           string password,
           int bufferLenght = 102400,
           IProgress<double> progress = null,
           CancellationToken Cancel = default);
    }
}
