using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NLayerApp.DAL
{
    public class Currency : INotifyPropertyChanged
    {
        private string id;
        private string rank;
        private string symbol;
        private string name;
        private string supply;
        private string maxSupply;
        private string marketCapUsd;
        private string volumeUsd24Hr;
        private string priceUsd;
        private string changePercent24Hr;
        private string vwap24Hr;
        private string explorer;

        [JsonProperty("explorer")]
        public string Explorer
        {
            get { return explorer; }
            set
            {
                explorer = value;
                OnPropertyChanged("Explorer");
            }
        }

        [JsonProperty("vwap24Hr")]
        public string Vwap24Hr
        {
            get { return vwap24Hr; }
            set
            {
                vwap24Hr = value;
                OnPropertyChanged("Vwap24Hr");
            }
        }

        [JsonProperty("changePercent24Hr")]
        public string ChangePercent24Hr
        {
            get { return changePercent24Hr; }
            set
            {
                changePercent24Hr = value;
                OnPropertyChanged("ChangePercent24Hr");
            }
        }

        [JsonProperty("priceUsd")]
        public string PriceUsd
        {
            get { return priceUsd; }
            set
            {
                priceUsd = value;
                OnPropertyChanged("PriceUsd");
            }
        }

        [JsonProperty("volumeUsd24Hr")]
        public string VolumeUsd24Hr
        {
            get { return volumeUsd24Hr; }
            set
            {
                volumeUsd24Hr = value;
                OnPropertyChanged("VolumeUsd24Hr");
            }
        }

        [JsonProperty("marketCapUsd")]
        public string MarketCapUsd
        {
            get { return marketCapUsd; }
            set
            {
                marketCapUsd = value;
                OnPropertyChanged("MarketCapUsd");
            }
        }

        [JsonProperty("maxSupply")]
        public string MaxSupply
        {
            get { return maxSupply; }
            set
            {
                maxSupply = value;
                OnPropertyChanged("MaxSupply");
            }
        }

        [JsonProperty("supply")]
        public string Supply
        {
            get { return supply; }
            set
            {
                supply = value;
                OnPropertyChanged("Supply");
            }
        }

        [JsonProperty("name")]
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        [JsonProperty("symbol")]
        public string Symbol
        {
            get { return symbol; }
            set
            {
                symbol = value;
                OnPropertyChanged("Symbol");
            }
        }

        [JsonProperty("rank")]
        public string Rank
        {
            get { return rank; }
            set
            {
                rank = value;
                OnPropertyChanged("Rank");
            }
        }


        [JsonProperty("id")]
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
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
