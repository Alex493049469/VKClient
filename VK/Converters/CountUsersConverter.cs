using System;
using System.Globalization;
using System.Windows.Data;

namespace VK.Converters
{
	class CountUsersConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var val = (int) value;
			switch (val)
			{
				case 2:
				case 3:
				case 4:
					return val + " участника";
				case 5:
				case 6:
				case 7:
				case 8:
				case 9:
				case 10:
					return val + " участников";
			}
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
