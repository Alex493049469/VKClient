using System;
using System.Xml.Serialization;

namespace VKAPI.Model
{
    public class Audio
    {
        //идентификатор аудиозаписи
        [XmlElement("id")]
        public int Id { get; set; }

        //идентификатор владельца аудиозаписи
        [XmlElement("owner_id")]
        public int OwnerId { get; set; }

        //исполнитель
        [XmlElement("artist")]
        public string Artist { get; set; }

        //название композиции
        [XmlElement("title")]
        public string Title { get; set; }

        //длительность аудиозаписи в секундах
        //public string duration { get; set; }
        [XmlElement("duration")]
        public int Duration
        {
            get { throw new NotImplementedException(); }
            set { DurationNorm = new TimeSpan(0, 0, Convert.ToInt32(value)); }
        }

        public TimeSpan DurationNorm { get; set; }

        //ссылка на mp3
        [XmlElement("url")]
        public string Url { get; set; }

        //идентификатор текста аудиозаписи (если доступно). положительное число
        [XmlElement("lyrics_id")]
        public int LyricsId { get; set; }

        //идентификатор альбома, в котором находится аудиозапись (если присвоен). положительное число
        [XmlElement("album_id")]
        public int AlbumId { get; set; }

        //идентификатор жанра из списка аудио жанров. положительное число
        [XmlElement("genre_id")]
        public int GenreId { get; set; }
    }
}