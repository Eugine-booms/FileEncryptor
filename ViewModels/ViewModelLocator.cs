using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace FileEncryptor.ViewModels
{
  internal class ViewModelLocator
    {
        public MainWindowViewModel MainViewModel => App.Host.Services.GetRequiredService<MainWindowViewModel>();
    }
}
