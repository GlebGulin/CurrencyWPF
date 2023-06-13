using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NLayerApp.DAL
{
    public class QuantityTopModel : INotifyPropertyChanged
    {
        public int id;
        public int val;
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public int Val
        {
            get { return val; }
            set
            {
                val = value;
                OnPropertyChanged("Val");
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
