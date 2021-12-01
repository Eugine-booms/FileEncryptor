using FileEncryptor.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileEncryptor.Services
{
  internal static class Registrator
    {
        internal static IServiceCollection RegisterServices(this IServiceCollection services) => services
            .AddTransient<IUserDialog, UserDialogService>();
    }
}
