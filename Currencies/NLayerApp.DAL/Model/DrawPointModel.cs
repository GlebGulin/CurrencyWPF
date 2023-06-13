using Common;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace NLayerApp.DAL
{
    public class DrawPointModel : INotifyPropertyChanged
    {
        public string     colorName;
        public string     pointsNormalize;
        public double     width;
        public double     height;
        public double     stepX;
        public double     stepY;
        public string     maxValue;
        public string     minValue;
        public int        valueCount;
        public string     startPeriod;
        public string     endPeriod;
        public PeriodType period;

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
                pointsNormalize = value;
                OnPropertyChanged("PointsNormalize");
            }
        }

        public double Width
        {
            get { return width; }
            set
            {
                width = value;
                OnPropertyChanged("Width");
            }
        }

        public double Height
        {
            get { return height; }
            set
            {
                height = value;
                OnPropertyChanged("Height");
            }
        }

        public double StepX
        {
            get { return stepX; }
            set
            {
                stepX = value;
                OnPropertyChanged("StepX");
            }
        }

        public double StepY
        {
            get { return stepY; }
            set
            {
                stepY = value;
                OnPropertyChanged("StepY");
            }
        }

        public string MaxValue
        {
            get { return maxValue; }
            set
            {
                maxValue = value;
                OnPropertyChanged("MaxValue");
            }
        }

        public string MinValue
        {
            get { return minValue; }
            set
            {
                minValue = value;
                OnPropertyChanged("MinValue");
            }
        }

        public int ValueCount
        {
            get { return valueCount; }
            set
            {
                valueCount = value;
                OnPropertyChanged("ValueCount");
            }
        }

        public string StartPeriod
        {
            get { return startPeriod; }
            set
            {
                startPeriod = value;
                OnPropertyChanged("StartPeriod");
            }
        }

        public string EndPeriod
        {
            get { return endPeriod; }
            set
            {
                endPeriod = value;
                OnPropertyChanged("EndPeriod");
            }
        }

        public PeriodType Period
        {
            get { return period; }
            set
            {
                period = value;
                OnPropertyChanged("Period");
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
