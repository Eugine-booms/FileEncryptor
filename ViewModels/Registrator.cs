using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileEncryptor.ViewModels
{
   static class  Registrator
    {
        internal static IServiceCollection RegisterViewModel(this IServiceCollection servises) => servises
            .AddSingleton<MainWindowViewModel>();
    }
}
