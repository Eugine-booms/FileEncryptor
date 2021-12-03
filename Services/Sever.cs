using FileEncryptor.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace FileEncryptor.Services
{
    internal class Sever : ISever
    {
        public void Save(object obj)
        {
            MessageBox.Show($"Я сохранил {obj}", "сохранятель", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
