using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NLayerApp.DAL
{
    public class Curency : INotifyPropertyChanged
    {
        private string id;
        private string rank;
        private string symbol;
        private string name;
        private string supply;
        private string maxSupply;
        private string marketCapUsd;
        private string volumeUsd24Hr;
        private string changePercent24Hr;
        private string vwap24Hr;
        private string explorer;
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public string Rank
        {
            get { return rank; }
            set
            {
                id = value;
                OnPropertyChanged("Rank");
            }
        }
        public string Symbol
        {
            get { return symbol; }
            set
            {
                id = value;
                OnPropertyChanged("Symbol");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                id = value;
                OnPropertyChanged("Name");
            }
        }

        public string Supply
        {
            get { return supply; }
            set
            {
                id = value;
                OnPropertyChanged("Supply");
            }
        }

        public string MaxSupply
        {
            get { return maxSupply; }
            set
            {
                id = value;
                OnPropertyChanged("MaxSupply");
            }
        }

        public string MarketCapUsd
        {
            get { return marketCapUsd; }
            set
            {
                id = value;
                OnPropertyChanged("MarketCapUsd");
            }
        }

        public string VolumeUsd24Hr
        {
            get { return volumeUsd24Hr; }
            set
            {
                id = value;
                OnPropertyChanged("VolumeUsd24Hr");
            }
        }

        public string ChangePercent24Hr
        {
            get { return changePercent24Hr; }
            set
            {
                id = value;
                OnPropertyChanged("ChangePercent24Hr");
            }
        }

        public string Vwap24Hr
        {
            get { return vwap24Hr; }
            set
            {
                id = value;
                OnPropertyChanged("Vwap24Hr");
            }
        }

        public string Explorer
        {
            get { return explorer; }
            set
            {
                id = value;
                OnPropertyChanged("Explorer");
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
