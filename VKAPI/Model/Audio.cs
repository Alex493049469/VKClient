using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKAPI.Model
{
    public class Audio
    {
        //идентификатор аудиозаписи
        public string id { get; set; }
        //идентификатор владельца аудиозаписи
        public string owner_id { get; set; }
        //исполнитель
        public string artist { get; set; }
        //название композиции
        public string title { get; set; }
        //длительность аудиозаписи в секундах
        //public string duration { get; set; }
        public TimeSpan duration { get; set; }
        //ссылка на mp3
        public string url { get; set; }
    }
}
