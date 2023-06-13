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
        public ICommand ChoosePeriod => new Command(() => { ChoosePeriodDetail(); });
        private string сurrentPriceUsd;
        public string CurrentPriceUsd
        {
            get { return сurrentPriceUsd; }
            set
            {
                сurrentPriceUsd = value;
                OnPropertyChanged("CurrentPriceUsd");
            }
        }
        public ObservableCollection<HistoryItem> History { get; set; }
        public List<HistoryItem> HistoryList { get; set; } = new List<HistoryItem>();
        public List<double> CurrentValues { get; set; } = new List<double>();
        public ObservableCollection<PeriodTimeModel> DefaultPeriods { get; set; }
        private PeriodTimeModel _selPeriodModel;
        public PeriodTimeModel SelPeriodModel
        {
            get { return _selPeriodModel; }
            set
            {
                _selPeriodModel = value;
                OnPropertyChanged("SelPeriodModel");
            }
        }
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
            DefaultPeriods = new ObservableCollection<PeriodTimeModel>()
            {
                new PeriodTimeModel(){ PeriodType = PeriodType.d1, Name = "Daily last year"},
                new PeriodTimeModel(){ PeriodType = PeriodType.m1, Name = "Last day (every 1 minute)"},
                new PeriodTimeModel(){ PeriodType = PeriodType.m5, Name = "Last 5 day (every 5 minute)"},
                new PeriodTimeModel(){ PeriodType = PeriodType.m15, Name = "Last 7 day (every 15 minute)"},
                new PeriodTimeModel(){ PeriodType = PeriodType.h1, Name = "Last month (every hour)"},
                new PeriodTimeModel(){ PeriodType = PeriodType.h2, Name = "Last 2 months (every 2 hours)"},
                new PeriodTimeModel(){ PeriodType = PeriodType.h6, Name = "Last 6 months (every 6 hours)"},
                new PeriodTimeModel(){ PeriodType = PeriodType.h12, Name = "Last year (every 12 hours)"},
            };
            SelPeriodModel = DefaultPeriods[0];
        }
        public override async Task OnInitialized(object parameter)
        {
            Id = parameter.ToString();
            FetchData(Id, _selPeriodModel.PeriodType);
            DrawGraphic();
        }

        public void DrawGraphic()
        {
            StringBuilder sb = new StringBuilder();
            double position = 0;
            DrawModel = new DrawPointModel()
            {
                ColorName = "Blue",
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
            DateTime dateSt;
            DateTime.TryParse(DrawModel.StartPeriod, out dateSt);
            DateTime dateEnd;
            DateTime.TryParse(DrawModel.StartPeriod, out dateEnd);
            DrawModel.StartPeriod = dateSt.ToString(Constants.DisplayDateFormat);
            DrawModel.EndPeriod = dateEnd.ToString(Constants.DisplayDateFormat);
            DrawModel.MaxValue = max.ToString();
            DrawModel.MinValue = min.ToString();

            CurrentPriceUsd = HistoryList[HistoryList.Count - 1].PriceUsd;
        }

        private void ChoosePeriodDetail()
        {
            FetchData(Id, _selPeriodModel.PeriodType);
            DrawGraphic();
        }

        private void FetchData(string Id, PeriodType period)
        {
            var url = String.Format("{0}{1}{2}{3}{4}", Constants.ApiBaseUrl, "/", Id, "/history?interval=", period.ToString());
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
