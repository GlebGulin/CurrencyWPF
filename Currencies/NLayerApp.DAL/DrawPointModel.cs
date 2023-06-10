using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace NLayerApp.DAL
{
    public class DrawPointModel : INotifyPropertyChanged
    {
        public string          colorName;
        public string          pointsNormalize;

        public string ColorName
        {
            get { return colorName; }
            set
            {
                colorName = value;
                OnPropertyChanged("ColorName");
            }
        }

        public string PointsNormalize
        {
            get { return pointsNormalize; }
            set
            {
                colorName = value;
                OnPropertyChanged("PointsNormalize");
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
