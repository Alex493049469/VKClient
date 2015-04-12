using System;

namespace VKAPI.Model
{
    public class Audio
    {
        //идентификатор аудиозаписи
        public string Id { get; set; }
        //идентификатор владельца аудиозаписи
        public string OwnerId { get; set; }
        //исполнитель
        public string Artist { get; set; }
        //название композиции
        public string Title { get; set; }
        //длительность аудиозаписи в секундах
        //public string duration { get; set; }
        public TimeSpan Duration { get; set; }
        //ссылка на mp3
        public string Url { get; set; }
        //идентификатор текста аудиозаписи (если доступно). положительное число
        public int LyricsId { get; set; }
        //идентификатор альбома, в котором находится аудиозапись (если присвоен). положительное число
        public int AlbumId  { get; set; }
        //идентификатор жанра из списка аудио жанров. положительное число
        public int GenreId { get; set; }
        	
    }
}