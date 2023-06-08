using Currencies.Commands;
using Currencies.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLayerApp.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Currencies.ViewModels
{
    public class CurrenciesViewModel : ViewModelBase
    {
        public ICommand GetDetailCommand { get; }
        private Curency selectedCurrency;
        public ObservableCollection<Curency> Curencies { get; set; }
        public Curency SelectedCurrency
        {
            get { return selectedCurrency; }
            set
            {
                selectedCurrency = value;
                OnPropertyChanged("SelectedCurrency");
            }
        }
        public CurrenciesViewModel(NavigationService<CurrencyDetailViewModel> getDetailCurrency)
        {
            GetDetailCommand = new NavigateCommand<CurrencyDetailViewModel>(getDetailCurrency);
            FetchData();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
                var id = this.SelectedCurrency.Id;
            }
            GetDetailCommand.Execute(this.SelectedCurrency.Id);
        }

        private void FetchData()
        {
            List<CurrencyTemp> models = null;
            try
            {
                using (var client = new HttpClient())
                {
                    using var result = client.GetAsync("https://api.coincap.io/v2/assets?limit=10");
                    string jsonString = result.Result.Content.ReadAsStringAsync().Result;
                    var jsonObj = (JObject)JsonConvert.DeserializeObject(jsonString);
                    var jsonArr = jsonObj.SelectToken("data");
                    List<CurrencyTemp> lst11 = JsonConvert.DeserializeObject<List<CurrencyTemp>>(jsonArr.ToString());
                    models = JsonConvert.DeserializeObject<List<CurrencyTemp>>(jsonArr.ToString());
                }
                if (models.Count != 0)
                {
                    Curencies = new ObservableCollection<Curency>();
                    foreach (var model in models)
                    {
                        var cur = new Curency()
                        {
                            Id = model.Id,
                            Name = model.Name,
                            Supply = model.Supply,
                            Rank = model.Rank,
                            Symbol = model.Symbol,
                            MaxSupply = model.MaxSupply,
                            MarketCapUsd = model.MarketCapUsd,
                            VolumeUsd24Hr = model.VolumeUsd24Hr,
                            PriceUsd = model.PriceUsd,
                            ChangePercent24Hr = model.ChangePercent24Hr,
                            Vwap24Hr = model.Vwap24Hr,
                            Explorer = model.Explorer
                        };
                        Curencies.Add(cur);
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MessageBoxButton button = MessageBoxButton.YesNoCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;
                result = MessageBox.Show(error, null, button, icon, MessageBoxResult.Yes);
            }
        }
    }
}
