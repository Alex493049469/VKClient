using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace VKAPI.Converters
{
   public class DateConverter : IValueConverter
    {
       public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
       {

           double timestamp = (double)value;
           DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
           dateTime = dateTime.AddSeconds(timestamp);

           string printDate = dateTime.ToShortDateString() + " " + dateTime.ToShortTimeString();
           return dateTime;
       }

       public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
       {
           throw new NotImplementedException();
       }
    }
}
