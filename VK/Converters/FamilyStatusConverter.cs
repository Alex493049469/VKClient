using System;
using System.Globalization;
using System.Windows.Data;

namespace VK.Converters
{
    public class FamilyStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //1 — не женат/не замужем;
            //2 — есть друг/есть подруга;
            //3 — помолвлен/помолвлена;
            //4 — женат/замужем:
            //5 — всё сложно;
            //6 — в активном поиске;
            //7 — влюблён/влюблена;
            //0 — не указано.
            string status = "";
            switch ((int) value)
            {
                case 1:
                {
                    status = "не женат/не замужем";
                    break;
                }
                case 2:
                {
                    status = "есть друг/есть подруга";
                    break;
                }
                case 3:
                {
                    status = "помолвлен/помолвлена";
                    break;
                }
                case 4:
                {
                    status = "женат/замужем";
                    break;
                }
                case 5:
                {
                    status = "всё сложно";
                    break;
                }
                case 6:
                {
                    status = "в активном поиске";
                    break;
                }
                case 7:
                {
                    status = "влюблён/влюблена";
                    break;
                }
                case 8:
                {
                    status = "не указано";
                    break;
                }
            }
            return status;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}