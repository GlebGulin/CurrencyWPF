using Currencies.Commands;
using Currencies.Services;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLayerApp.DAL;
using System;
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
        public string Id { get; set; }
        private Curency _сurency { get; set; }
        public Curency Curency
        {
            get { return _сurency; }
            set
            {
                _сurency = value;
                OnPropertyChanged("Curency");
            }
        }
        public CurrencyDetailViewModel(NavigationService<CurrenciesViewModel> getCurrencies) 
        {
            
            BackToCurrencies = new NavigateCommand<CurrenciesViewModel>(getCurrencies);           
        }

        public override async Task OnInitialized(object parameter)
        {

            Id = parameter.ToString();
            try
            {
                using (var client = new HttpClient())
                {
                    using var result = client.GetAsync("https://api.coincap.io/v2/assets/ethereum");
                    string jsonString = result.Result.Content.ReadAsStringAsync().Result;
                    var jsonObj = (JObject)JsonConvert.DeserializeObject(jsonString);
                    var jsonData = jsonObj.SelectToken("data");
                    Curency = JsonConvert.DeserializeObject<Curency>(jsonData.ToString());
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
                var destinationurl = _сurency.Explorer;
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
    }
}
