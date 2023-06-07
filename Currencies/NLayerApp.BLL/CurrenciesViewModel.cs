using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLayerApp.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Windows;

namespace NLayerApp.BLL
{
    public class CurrenciesViewModel : INotifyPropertyChanged
    {
        private Curency currency;
        public ObservableCollection<Curency> Curencies { get; set; }
        
        public CurrenciesViewModel()
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
            catch(Exception ex)
            {
                var error = ex.Message;
                MessageBoxButton button = MessageBoxButton.YesNoCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;
                result = MessageBox.Show(error, null, button, icon, MessageBoxResult.Yes);
            }
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
