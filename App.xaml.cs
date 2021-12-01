using FileEncryptor.Services;
using FileEncryptor.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

namespace FileEncryptor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IHost __host;
        public static IHost Host => __host ??= Programm.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();
        internal static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.RegisterServices().RegisterViewModel();
        }

        public static IServiceProvider Services => Host.Services;
        protected  override async void OnStartup(StartupEventArgs e)
        {
            var host = Host;   //создает хост

            base.OnStartup(e);   //выполняются операции по иниализации приложения 

           await host.RunAsync().ConfigureAwait(false);            //запускаем наш хост
        }

        protected  override async void OnExit(ExitEventArgs e)
        {
            var host = Host;
            
            base.OnExit(e);
            using (Host)                                          //получаем перехват ошибок в осинхронном коде и поконцу вызываем Dispose
                await host.StopAsync().ConfigureAwait(false);
            
        }
    }
}
