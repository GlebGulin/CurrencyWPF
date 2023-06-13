using Currencies.Commands;
using Currencies.Services;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLayerApp.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Currencies.ViewModels
{
    public class CurrencyDetailViewModel : ViewModelBase
    {
        public ICommand OpenURL => new Command(() => { OpenURLCurrency(); });
        public ICommand BackToCurrencies { get; }
        public ICommand HistoryCurrency { get; }
        public ICommand ExchangeCurrency { get; }
        public ICommand OpenHistoryCurrency => new Command(() => { OpenHistory(); });
        public ICommand OpenExchangeCurrency => new Command(() => { OpenExchange(); });
        private List<CurrencyTemp> lst { get; set; } = new List<CurrencyTemp>();
        private ParameterModel parameter { get; set; } = new ParameterModel();
        public string Id { get; set; }
        private Currency _сurrency { get; set; }
        public Currency Currency
        {
            get { return _сurrency; }
            set
            {
                _сurrency = value;
                OnPropertyChanged("Currency");
            }
        }
        public CurrencyDetailViewModel(NavigationService<CurrenciesViewModel> getCurrencies, 
                                       NavigationService<HistoryViewModel>    getHistoryCurrency,
                                       NavigationService<ExchangeCurrencyViewModel> getExchangeCurrency) 
        {
            
            BackToCurrencies  = new NavigateCommand<CurrenciesViewModel>(getCurrencies); 
            HistoryCurrency   = new NavigateCommand<HistoryViewModel>(getHistoryCurrency);
            ExchangeCurrency  = new NavigateCommand<ExchangeCurrencyViewModel>(getExchangeCurrency);
        }

        public override async Task OnInitialized(object parameter)
        {
            var param = parameter as ParameterModel;
            Id = param.Id;
            lst = param.Currencies;
            var url = String.Format("{0}{1}", "https://api.coincap.io/v2/assets/", Id);
            try
            {
                using (var client = new HttpClient())
                {
                    using var result = client.GetAsync(url);
                    string jsonString = result.Result.Content.ReadAsStringAsync().Result;
                    var jsonObj = (JObject)JsonConvert.DeserializeObject(jsonString);
                    var jsonData = jsonObj.SelectToken("data");
                    Currency = JsonConvert.DeserializeObject<Currency>(jsonData.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBoxButton button = MessageBoxButton.YesNoCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;
                result = MessageBox.Show(ex.Message, null, button, icon, MessageBoxResult.Yes);
            }
        }
       
        private async Task OpenURLCurrency()
        {
            try
            {
                //await Browser.OpenAsync(_сurency.Explorer, BrowserLaunchMode.SystemPreferred);
                var destinationurl = _сurrency.Explorer;
                var sInfo = new System.Diagnostics.ProcessStartInfo(destinationurl)
                {
                    UseShellExecute = true,
                };
                System.Diagnostics.Process.Start(sInfo);
            }
            catch (Exception ex)
            {
                MessageBoxButton button = MessageBoxButton.YesNoCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;
                result = MessageBox.Show(ex.Message, null, button, icon, MessageBoxResult.Yes);
            }
        }

        private void OpenHistory()
        {
            parameter.Id = this.Id;
            HistoryCurrency.Execute(parameter.Id);
        }

        private void OpenExchange()
        {
            parameter.Id = this.Id;
            parameter.Currencies = this.lst;
            parameter.CurrentPrice = this._сurrency.PriceUsd;
            parameter.CurrentName = this._сurrency.Name;
            ExchangeCurrency.Execute(parameter);
        }
    }
}
