using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Core
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region MVVM related
        protected void OnPropertyChanged([CallerMemberName]string propertyName = "") // волшебство .NET 4.5
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}