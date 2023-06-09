using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Currencies.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {

        public virtual async Task OnInitialized(object parameter) {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public string Params { get; set; }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void Dispose() { }
    }
}
