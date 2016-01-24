using System;
using System.Globalization;
using System.Windows.Data;

namespace VK.Converters
{
   public class DateConverter : IValueConverter
	{
	   public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	   {
		   int temp = (int)value;

		   double timestamp = (double)temp;
		   DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
		   dateTime = dateTime.AddSeconds(timestamp);
		   dateTime = dateTime.ToLocalTime();

		   string date = dateTime.ToLongDateString();
		   string time = dateTime.ToShortTimeString();

		   var difference = DateTime.Today.Date.Subtract(dateTime.Date);
		   if (difference.Days == 0)
		   {
			   date = "сегодня в";
		   }
		   if (difference.Days == 1)
		   {
			   date = "вчера в";
		   }

		   string printDate = date + " " + time;
		   return printDate;
	   }


	   public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	   {
		   throw new NotImplementedException();
	   }
	}
}
