using Common;
using Currencies.Commands;
using Currencies.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLayerApp.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Currencies.ViewModels
{
    public class HistoryViewModel : ViewModelBase
    {
        public ICommand BackToCurrencies { get; }
        public ObservableCollection<HistoryItem> History { get; set; }
        public List<HistoryItem> HistoryList { get; set; } = new List<HistoryItem>();
        public List<double> CurrentValues { get; set; } = new List<double>();
        public string Id { get; set; }
        public int ValuesCount { get; set; }
        public int currentSecond = 0;
        Random rd = new Random();
        public PointCollection LtPoint { get; set; }
        private DrawPointModel _drawModel { get; set; }
        public DrawPointModel DrawModel
        {
            get { return _drawModel; }
            set
            {
                _drawModel = value;
                OnPropertyChanged("DrawModel");
            }
        }

        public HistoryViewModel(NavigationService<CurrenciesViewModel> getCurrencies)
        {
            BackToCurrencies = new NavigateCommand<CurrenciesViewModel>(getCurrencies);
        }
        public override async Task OnInitialized(object parameter)
        {
            FetchData(parameter.ToString());
            DrawGraphic();
        }

        public void DrawGraphic()
        {
            StringBuilder sb = new StringBuilder();
            double position = 0;
            DrawModel = new DrawPointModel()
            {
                ColorName = "Blue",
                PointsNormalize = "0.5, 30, 2, 50, 3.5, 20, 4.7, 100, 5.5, 200, 6.7, 20, 70.0009, 100,",
                Height = Constants.BasicHeigthPolygon,
                Width = Constants.BasicWithPolygon
            };
            DrawModel.ValueCount = ValuesCount;
            for (int i = 0; i < DrawModel.ValueCount; i++)
            {
                CurrentValues.Add(double.Parse(HistoryList[i].PriceUsd, System.Globalization.CultureInfo.InvariantCulture));
            }
            double max = CurrentValues.Max<double>();
            double min = CurrentValues.Min<double>();
            double difference = max - min;
            
            DrawModel.stepX = (double)(Constants.BasicWithPolygon / DrawModel.ValueCount);
            for (int i = 0; i < DrawModel.ValueCount; i++)
            {
                double curVal = Constants.BasicHeigthPolygon -((Constants.BasicHeigthPolygon * ((double.Parse(HistoryList[i].PriceUsd, System.Globalization.CultureInfo.InvariantCulture)) - min)) / difference);
                position += DrawModel.stepX;
                sb.Append(String.Format("{0}, {1},", position.ToString().Replace(',', '.'), curVal.ToString().Replace(',', '.')));
            }

            string Normal = sb.ToString();
            DrawModel.PointsNormalize = Normal.Remove(Normal.Length - 1);

            DrawModel.StartPeriod = HistoryList[0].Time;
            DrawModel.EndPeriod = HistoryList[HistoryList.Count-1].Time;
            
            DrawModel.MaxValue = max.ToString();
            DrawModel.MinValue = min.ToString();
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
                    HistoryList = JsonConvert.DeserializeObject<List<HistoryItem>>(jsonArr.ToString());
                    LtPoint = new PointCollection(HistoryList.Count);
                    ValuesCount = HistoryList.Count;
                    MakeListHistoryItems(HistoryList);
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

        private void MakeListHistoryItems(List<HistoryItem> items)
        {
            if(items.Count > 0)
            {
                History = new ObservableCollection<HistoryItem>();
                foreach (var item in items)
                {
                    History.Add(item);
                }
            }
        }
    }
}
