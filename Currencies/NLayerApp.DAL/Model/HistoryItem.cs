using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NLayerApp.DAL
{
    public class HistoryItem : INotifyPropertyChanged
    {
        private string priceUsd;
        private string time;
        private string date;
        private string circulatingSupply;

        public string PriceUsd
        {
            get { return priceUsd; }
            set
            {
                priceUsd = value;
                OnPropertyChanged("PriceUsd");
            }
        }

        public string Time
        {
            get { return time; }
            set
            {
                time = value;
                OnPropertyChanged("Time");
            }
        }

        public string Date
        {
            get { return date; }
            set
            {
                time = value;
                OnPropertyChanged("Date");
            }
        }

        public string CirculatingSupply
        {
            get { return circulatingSupply; }
            set
            {
                circulatingSupply = value;
                OnPropertyChanged("CirculatingSupply");
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
