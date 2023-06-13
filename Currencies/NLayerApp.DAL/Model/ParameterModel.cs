using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.DAL
{
    public class ParameterModel : INotifyPropertyChanged
    {
        private string id;
        private List<CurrencyTemp> currencies;
        private string currentPrice;
        private string currentName;

        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string CurrentPrice
        {
            get { return currentPrice; }
            set
            {
                currentPrice = value;
                OnPropertyChanged("CurrentPrice");
            }
        }

        public string CurrentName
        {
            get { return currentName; }
            set
            {
                currentName = value;
                OnPropertyChanged("CurrentName");
            }
        }

        public List<CurrencyTemp> Currencies
        {
            get { return currencies; }
            set
            {
                currencies = value;
                OnPropertyChanged("Currencies");
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
