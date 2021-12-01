using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace FileEncryptor.ViewModels
{
  internal class ViewModelLocator
    {
        public MainWindowViewModel MainViewModel => App.Services.GetRequiredService<MainWindowViewModel>();
    }
}
