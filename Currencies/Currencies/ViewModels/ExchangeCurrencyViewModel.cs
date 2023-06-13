using Currencies.Commands;
using Currencies.Services;
using MvvmHelpers.Commands;
using NLayerApp.DAL;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Currencies.ViewModels
{
    public class ExchangeCurrencyViewModel : ViewModelBase
    {
        public ICommand BackToCurrencies { get; }
        public ICommand ChangeCurrency => new Command(() => { ChangeCurrencyToOther(); });
        public ObservableCollection<SelectExchangeModel> ChooseExchange { get; set; }
        private SelectExchangeModel _selCurrency;
        public SelectExchangeModel SelCurrency
        {
            get { return _selCurrency; }
            set
            {
                _selCurrency = value;
                OnPropertyChanged("SelCurrency");
            }
        }
        private string inputValue;
        public string InputValue
        {
            get { return inputValue; }
            set
            {
                inputValue = value;
                OnPropertyChanged("InputValue");
            }
        }

        public string _currencyName;
        public string CurrencyName
        {
            get { return _currencyName; }
            set
            {
                _currencyName = value;
                OnPropertyChanged("CurrencyName");
            }
        }
        public string _currencyPriceUsd;
        public string CurrencyPriceUsd
        {
            get { return _currencyPriceUsd; }
            set
            {
                _currencyPriceUsd = value;
                OnPropertyChanged("CurrencyPriceUsd");
            }
        }

        private string outValue;
        public string OutValue
        {
            get { return outValue; }
            set
            {
                outValue = value;
                OnPropertyChanged("OutValue");
            }
        }
        public ExchangeCurrencyViewModel(NavigationService<CurrenciesViewModel> getCurrencies)
        {
            BackToCurrencies = new NavigateCommand<CurrenciesViewModel>(getCurrencies);
        }

        public override async Task OnInitialized(object parameter)
        {
            var param = parameter as ParameterModel;
            CurrencyPriceUsd = param.CurrentPrice;
            CurrencyName = param.CurrentName;
            ChooseExchange = new ObservableCollection<SelectExchangeModel>();
            foreach (var currency in param.Currencies)
            {
                ChooseExchange.Add(new SelectExchangeModel() { PriceUsd = currency.PriceUsd, Name = currency.Name });
            }
            SelCurrency = ChooseExchange[0];
            CalculateExchange();
        }
        private void CalculateExchange()
        {
            try
            {
                float currentPriceUsd = float.Parse(CurrencyPriceUsd, CultureInfo.InvariantCulture.NumberFormat);
                float selectedCurrencyPriceusd = float.Parse(SelCurrency.PriceUsd, CultureInfo.InvariantCulture.NumberFormat);
                float convertedInputVal = float.Parse(InputValue, CultureInfo.InvariantCulture.NumberFormat) / currentPriceUsd;
            }
            catch(Exception ex)
            {

            }
        }

        private void ChangeCurrencyToOther()
        {
            CalculateExchange();
        }
    }
}
