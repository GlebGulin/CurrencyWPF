using Common.Core;
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

namespace NLayerApp.BLL.ViewModels
{
    public class CurrencyDetailViewModel : ViewModelBase
    {
        public ICommand OpenURL => new Command(() => { OpenURLCurrency(); });
        private Curency _сurency { get; set; }
        public Curency Curency
        {
            get { return _сurency; }
            set
            {
                _сurency = value;
                OnPropertyChanged("_сurency");
            }
        }
        public CurrencyDetailViewModel() 
        {
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
            catch(Exception ex)
            {
                MessageBoxButton button = MessageBoxButton.YesNoCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;
                result = MessageBox.Show(ex.Message, null, button, icon, MessageBoxResult.Yes);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
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
