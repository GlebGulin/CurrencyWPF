using Currencies.Services;
using Currencies.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Currencies.HostBuilders
{
    public static class ViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<CurrenciesViewModel>();
                services.AddSingleton<Func<CurrenciesViewModel>>((s) => () => s.GetRequiredService<CurrenciesViewModel>());
                services.AddSingleton<NavigationService<CurrenciesViewModel>>();

                services.AddTransient<CurrencyDetailViewModel>();
                services.AddSingleton<Func<CurrencyDetailViewModel>>((s) => () => s.GetRequiredService<CurrencyDetailViewModel>());
                services.AddSingleton<NavigationService<CurrencyDetailViewModel>>();

                services.AddTransient<HistoryViewModel>();
                services.AddSingleton<Func<HistoryViewModel>>((s) => () => s.GetRequiredService<HistoryViewModel>());
                services.AddSingleton<NavigationService<HistoryViewModel>>();

                services.AddSingleton<MainViewModel>();
            });

            return hostBuilder;
        }
    }
}
