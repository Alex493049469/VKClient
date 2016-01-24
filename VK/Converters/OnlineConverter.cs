using System;
using System.Globalization;
using System.Windows.Data;

namespace VK.Converters
{
    public class OnlineConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int online = (int)value;
            if (online == 1)
            {
                return "Online";
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
