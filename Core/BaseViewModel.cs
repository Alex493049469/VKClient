using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Core.Command;

namespace Core
{   
    [Magic]
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        #region MVVM related
        protected void RaisePropertyChanged([CallerMemberName]string propertyName = "") // волшебство .NET 4.5
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }

    public class MagicAttribute : Attribute { }
    public class NoMagicAttribute : Attribute { }
}