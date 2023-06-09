using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLayerApp.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace Currencies.ViewModels
{
    public class HistoryViewModel : ViewModelBase
    {
        public ObservableCollection<HistoryItem> History { get; set; }
        public string Id { get; set; }
        public HistoryViewModel()
        {

        }
        public override async Task OnInitialized(object parameter)
        {
            FetchData(parameter.ToString());
        }

        private void FetchData(string Id)
        {
            var url = String.Format("{0}{1}{2}", "https://api.coincap.io/v2/assets/", Id, "/history?interval=d1");
            try
            {
                using (var client = new HttpClient())
                {
                    using var result = client.GetAsync(url);
                    string jsonString = result.Result.Content.ReadAsStringAsync().Result;
                    var jsonObj = (JObject)JsonConvert.DeserializeObject(jsonString);
                    var jsonArr = jsonObj.SelectToken("data");
                    List<HistoryItem> lst11 = JsonConvert.DeserializeObject<List<HistoryItem>>(jsonArr.ToString());
                    //History = JsonConvert.DeserializeObject<List<HistoryItem>>(jsonArr.ToString());
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
    }
}
