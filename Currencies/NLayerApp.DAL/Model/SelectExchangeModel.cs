using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NLayerApp.DAL
{
    public class SelectExchangeModel : INotifyPropertyChanged
    {
        public string priceUsd;
        public string name;
        public string PriceUsd
        {
            get { return priceUsd; }
            set
            {
                priceUsd = value;
                OnPropertyChanged("PriceUsd");
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
