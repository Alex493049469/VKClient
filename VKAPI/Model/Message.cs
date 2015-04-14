using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace VKAPI.Model
{
    public class Message
    {
        //идентификатор сообщения (не возвращается для пересланных сообщений).
        [XmlElement("id")]
        public string Id { get; set; }
        //дата отправки сообщения в формате unixtime.
        [XmlElement("date")]
        public double Date
        {
            get { throw new NotImplementedException(); }
            set { DateNorm = ConvertUnixTimeToDateTime(value); }
        }

        public DateTime DateNorm { get; set; }

        //тип сообщения (0 — полученное, 1 — отправленное, не возвращается для пересланных сообщений).
        [XmlElement("out")]
        public int Out { get; set; }
        //идентификатор пользователя, в диалоге с которым находится сообщение.
        [XmlElement("user_id")]
        public int UserId { get; set; }
        //статус сообщения (0 — не прочитано, 1 — прочитано, не возвращается для пересланных сообщений).
        [XmlElement("read_state")]
        public int ReadState { get; set; }
        //заголовок сообщения или беседы.
        [XmlElement("title")]
        public string Title { get; set; }
        //текст сообщения.
        [XmlElement("body")]
        public string Body { get; set; }
        //идентификатор беседы
        [XmlElement("chat_id")]
        public int ChatId { get; set; }
        //идентификаторы авторов последних сообщений беседы.
        [XmlArray("chat_active")]
        [XmlArrayItem("item")]
        public List<int> ChatActive { get; set; }
        //количество участников беседы.
        [XmlElement("users_count")]
        public int UsersCount { get; set; }
        //идентификатор создателя беседы.
        [XmlElement("admin_id")]
        public int AdminId { get; set; }

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
    }
}