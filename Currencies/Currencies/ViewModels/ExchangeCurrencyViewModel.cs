using Currencies.Commands;
using Currencies.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Currencies.ViewModels
{
    public class ExchangeCurrencyViewModel : ViewModelBase
    {
        public ICommand BackToCurrencies { get; }
        public ExchangeCurrencyViewModel(NavigationService<CurrenciesViewModel> getCurrencies)
        {
            BackToCurrencies = new NavigateCommand<CurrenciesViewModel>(getCurrencies);
        }
    }
}
