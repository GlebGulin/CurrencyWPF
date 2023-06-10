using Currencies.Commands;
using Currencies.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLayerApp.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Currencies.ViewModels
{
    public class HistoryViewModel : ViewModelBase
    {
        public ICommand BackToCurrencies { get; }
        public ObservableCollection<HistoryItem> History { get; set; }
        public string Id { get; set; }
        public int currentSecond = 0;
        Random rd = new Random();
        public PointCollection LtPoint { get; set; }
        public string PointsNormalize { get; set; } = "10, 30, 20, 50, 30, 20, 40, 100";

        public DrawPointModel DrawModel { get; set; } 
        public HistoryViewModel(NavigationService<CurrenciesViewModel> getCurrencies)
        {
            BackToCurrencies = new NavigateCommand<CurrenciesViewModel>(getCurrencies);
        }
        public override async Task OnInitialized(object parameter)
        {
            FetchData(parameter.ToString());
            //DispatcherTimer timer = new DispatcherTimer();
            //timer.Tick += Timer_Tick;
            //timer.Interval = TimeSpan.FromMilliseconds(1500);
            //timer.Start();
            
            DrawGraphic();
        }

        public void DrawGraphic()
        {
            Timer_Tick();
            Timer_Tick();
            Timer_Tick();
            Timer_Tick();
            Timer_Tick();
            Timer_Tick();
            Timer_Tick();
            Timer_Tick();
            Timer_Tick();
            Timer_Tick();
            Timer_Tick();
            Timer_Tick();
            Timer_Tick();
            Timer_Tick();
            Timer_Tick();
            Timer_Tick();
            Timer_Tick();
            Timer_Tick();
            Timer_Tick();
            Timer_Tick();
            Timer_Tick();
            Timer_Tick();
            Timer_Tick();
            Timer_Tick();
            Timer_Tick();
            Timer_Tick();
            Timer_Tick();
            myModel = new DrawPointModel()
            {
                ColorName = "Blue",
                PointsNormalize = "10, 30, 20, 50, 30, 20, 40, 100, 50, 200, 60, 20, 70, 100"
            };
        }

        private void Timer_Tick()
        {
            currentSecond++;
            double x = currentSecond * 10;
            double y = rd.Next(1, 1000);
            LtPoint.Add(new Point(x, y));
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
                    LtPoint = new PointCollection(lst11.Count);
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
