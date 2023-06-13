using Common;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NLayerApp.DAL
{
    public class PeriodTimeModel : INotifyPropertyChanged
    {
        public PeriodType periodType;
        public string     name;

        public PeriodType PeriodType
        {
            get { return periodType; }
            set
            {
                periodType = value;
                OnPropertyChanged("PeriodType");
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
