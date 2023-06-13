using Common;
using Currencies.Commands;
using Currencies.Services;
using MvvmHelpers.Commands;
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
        public ICommand SelectTopQuatity => new Command(() => { SelectTopQuatityChanged(); });
        public ICommand ChooseCurrency => new Command(() => { ChooseCurrencyDetail(); });
        private Currency selectedCurrency;
        public ObservableCollection<QuantityTopModel> DefaultQuantity { get; set; }
        private QuantityTopModel _selQuantityTopModel;
        public QuantityTopModel SelQuantityTopModel
        {
            get { return _selQuantityTopModel; }
            set
            {
                _selQuantityTopModel = value;
                OnPropertyChanged("SelQuantityTopModel");
            }
        }
        public ObservableCollection<Currency> Curencies { get; set; } = new ObservableCollection<Currency>();

        public Currency SelectedCurrency
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
            DefaultQuantity = new ObservableCollection<QuantityTopModel>()
            {
                new QuantityTopModel(){ Id = 10, Val = 10},
                new QuantityTopModel(){ Id = 15, Val = 15},
                new QuantityTopModel(){ Id = 25, Val = 25},
                new QuantityTopModel(){ Id = 50, Val = 50},
                new QuantityTopModel(){ Id = 100, Val = 100}
            };
            SelQuantityTopModel = new QuantityTopModel() { Id = 10, Val = 10 };
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
        }

        private void FetchData()
        {
            List<CurrencyTemp> models = null;
            if (_selQuantityTopModel is null)
            {
                _selQuantityTopModel = new QuantityTopModel() { Id = 10, Val = 10 };
            }
            var url = String.Format("{0}{1}{2}", Constants.ApiBaseUrl, "?limit=", _selQuantityTopModel.Val.ToString());
            try
            {
                using (var client = new HttpClient())
                {
                    using var result = client.GetAsync(url);
                    string jsonString = result.Result.Content.ReadAsStringAsync().Result;
                    var jsonObj = (JObject)JsonConvert.DeserializeObject(jsonString);
                    var jsonArr = jsonObj.SelectToken("data");
                    List<CurrencyTemp> lst11 = JsonConvert.DeserializeObject<List<CurrencyTemp>>(jsonArr.ToString());
                    models = JsonConvert.DeserializeObject<List<CurrencyTemp>>(jsonArr.ToString());
                }
                if (models.Count != 0)
                {
                    Curencies.Clear();
                    foreach (var model in models)
                    {
                        var cur = new Currency()
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

        private void SelectTopQuatityChanged()
        {
            FetchData();
        }

        private void ChooseCurrencyDetail()
        {
            GetDetailCommand.Execute(this.SelectedCurrency.Id);
        }
    }
}
