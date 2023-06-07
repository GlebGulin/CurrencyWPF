using System;
using System.ComponentModel;
using System.Windows;

namespace NLayerApp.BLL
{
    public class CurrencyDetailViewModel : INotifyPropertyChanged
    {
        public CurrencyDetailViewModel()
        {
            try
            {

            }
            catch(Exception ex)
            {
                MessageBoxButton button = MessageBoxButton.YesNoCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;
                result = MessageBox.Show(ex.Message, null, button, icon, MessageBoxResult.Yes);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
