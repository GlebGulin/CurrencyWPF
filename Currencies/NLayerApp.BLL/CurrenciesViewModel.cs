using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLayerApp.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace NLayerApp.BLL
{
    public class CurrenciesViewModel : INotifyPropertyChanged
    {
        private Curency currency;
        public ObservableCollection<Curency> Curencies { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public CurrenciesViewModel()
        {
            List<Curency> model = null;
            //var client = new HttpClient();
            //var task = client.GetAsync("https://api.coincap.io/v2/assets?limit=10")
            //  .ContinueWith((taskwithresponse) =>
            //  {
            //      var response = taskwithresponse.Result;
            //      string jsonString = response.Content.ReadAsStringAsync();
            //      dynamic jsonObj = JsonConvert.DeserializeObject(jsonString);

            //      Console.WriteLine(jsonString);
            //      jsonString.Wait();
            //      model = JsonConvert.DeserializeObject<List<Curency>>(jsonString.Result);

            //  });
            //task.Wait();

            using (var client = new HttpClient())
            {
                using var result = client.GetAsync("https://api.coincap.io/v2/assets?limit=10");
                string jsonString = result.Result.Content.ReadAsStringAsync().Result;
                dynamic jsonObj2 = JsonConvert.DeserializeObject(jsonString);
                JObject jsonObj = JObject.Parse(jsonString);
                string name = (string)(Object)jsonObj["data"].ToString();
                model = JsonConvert.DeserializeObject<List<Curency>>(name);
            }
            var a = model;
        }
    }
}
