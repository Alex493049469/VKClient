using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace VKAPI.Converters
{
   public class TimeConverter : IValueConverter
    {
       public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
       {
           int seconds = (int)value;
           return new TimeSpan(0, 0, System.Convert.ToInt32(seconds));
       }

       public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
       {
           return null;
       }
    }
}
