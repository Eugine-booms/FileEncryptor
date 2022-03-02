using FileEncryptor.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace FileEncryptor.Services
{
    internal class Rfc2898Encryptor : IEncryptor

        
    {
        private static readonly byte[] __salt1 =
        {
            0x26, 0xdc, 0xff, 0x00,
            0xad, 0xed, 0x7a, 0xee,
            0xc5, 0xfe, 0x07, 0xaf,
            0x4d, 0x08, 0x22, 0x3c
        };

        private static ICryptoTransform GetEncryptor(string password, byte[] slat = null)
        {
            var pdb = new Rfc2898DeriveBytes(password, slat??__salt1);
            var algorithm = Rijndael.Create();
            algorithm.Key = pdb.GetBytes(32);
            algorithm.IV = pdb.GetBytes(16);
            return algorithm.CreateEncryptor();
        }

        private static ICryptoTransform GetDecryptor(string password, byte[] slat = null)
        {
            var pdb = new Rfc2898DeriveBytes(password, slat?? __salt1);
            var algorithm = Rijndael.Create();
            algorithm.Key = pdb.GetBytes(32);
            algorithm.IV = pdb.GetBytes(16);
            return algorithm.CreateDecryptor();
        }
        public void Encrypt(string sourcePath, string DestinationPath, string password, int bufferLenght = 102400)
        {
            var encryptor = GetEncryptor(password);
            using var destination_encrypted = File.Create(DestinationPath, bufferLenght);
            using var destanation = new CryptoStream(destination_encrypted, encryptor, CryptoStreamMode.Write);
            using var source = File.OpenRead(sourcePath);
            int reader;
            var buffer = new byte[bufferLenght];
            do
            {
                Thread.Sleep(1);
                reader = source.Read(buffer);
                destanation.Write(buffer, 0, reader);
            } while (reader>0);
            destanation.FlushFinalBlock();
        }
        public bool Decrypt(string sourcePath, string DestinationPath, string password, int bufferLenght = 102400)
        {
            var decryptor = GetDecryptor(password);
            using var destination_decrypted = File.Create(DestinationPath, bufferLenght);
            using var destination = new CryptoStream(destination_decrypted, decryptor, CryptoStreamMode.Write);
            using var encryptor_source = File.OpenRead(sourcePath);
            var buffer = new byte[bufferLenght];
            int reader;
            do
            {
                Thread.Sleep(2);
                reader = encryptor_source.Read(buffer, 0, bufferLenght);
                destination.Write(buffer, 0, reader);
            } while (reader>0);
            try
            {
                destination.FlushFinalBlock();
            }
            catch (CryptographicException)
            {
                return false;
                
            }

            return true;
        }
    }
}
