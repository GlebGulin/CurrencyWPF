using Currencies.Stores;
using Currencies.ViewModels;
using System;
using System.Threading.Tasks;

namespace Currencies.Services
{
    public class NavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public NavigationService(NavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate(object parameters)
        {
                       
            _navigationStore.CurrentViewModel = _createViewModel();
            Task.Run(async () => await _navigationStore.CurrentViewModel.OnInitialized(parameters));
        }
    }
}
