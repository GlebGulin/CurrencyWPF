using Currencies.HostBuilders;
using Currencies.Services;
using Currencies.Stores;
using Currencies.ViewModels;
using Currencies.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Currencies
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly IHost _host;
        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .AddViewModels()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<MainWindow>();
                    //services.AddSingleton<CurrencyDetailView>();
                    //services.AddSingleton<CurrenciesViewModel>();
                    //services.AddSingleton<CurrencyDetailViewModel>();
                    //services.AddSingleton<NavigationService<CurrenciesViewModel>>();
                    //services.AddSingleton<NavigationService<CurrencyDetailViewModel>>();
                    services.AddSingleton<NavigationStore>();

                    services.AddSingleton(s => new MainWindow()
                    {
                        DataContext = s.GetRequiredService<MainViewModel>()
                    });
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            NavigationService<CurrenciesViewModel> navigationService = _host.Services.GetRequiredService<NavigationService<CurrenciesViewModel>>();
            navigationService.Navigate(null);

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();

            base.OnExit(e);
        }
    }
}
