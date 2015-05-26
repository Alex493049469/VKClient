using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKAPI.Converters
{
    public static class Converter
    {
        /// <summary>
        /// преобразование даты в человеческую
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime ConvertUnixTimeToDateTime(double timestamp)
        {

            System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);

            dateTime = dateTime.AddSeconds(timestamp);

            string printDate = dateTime.ToShortDateString() + " " + dateTime.ToShortTimeString();

            System.Console.WriteLine(printDate);
            return dateTime;
        }

        /// <summary>
        /// переводит секунды в формат "часы : минуты : секунды"
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static TimeSpan ConvertSecondsToTime(int seconds)
        {
            return new TimeSpan(0, 0, Convert.ToInt32(seconds));
        }
    }
}
