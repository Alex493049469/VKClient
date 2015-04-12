using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VKAPI.Model;


namespace VKAPI
{
    /// <summary>
    /// Класс для работы с аудиозаписями
    /// </summary>
    public static class VkAudio
    {
        public static List<Audio> ListAudios;

        /// <summary>
        /// Грузит указанное количество аудиозаписей
        /// </summary>
        /// <param name="CountAudio"></param>
        /// <returns></returns>
        public static List<Audio> GetMyAudios(int CountAudio)
        {
            ListAudios = new List<Audio>();

            System.Net.WebRequest reqGET =
                System.Net.WebRequest.Create(@"https://api.vk.com/method/audio.get.xml?count=" + CountAudio + "&v=5.29&access_token=" + VkMain.token);
            System.Net.WebResponse resp = reqGET.GetResponse();
            System.IO.Stream stream = resp.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(stream);
            string s = sr.ReadToEnd();

            
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(s);
            XmlNodeList bookNodes = doc.GetElementsByTagName("audio");

            foreach (XmlNode bookNode in bookNodes)
            {
                XmlNode id = bookNode["id"];
                XmlNode owner_id = bookNode["owner_id"];
                XmlNode artist = bookNode["artist"];
                XmlNode title = bookNode["title"];
                XmlNode duration = bookNode["duration"];
                XmlNode url = bookNode["url"];

                Audio trek = new Audio();

                trek.Id = id.InnerText;
                trek.OwnerId = owner_id.InnerText;
                trek.Artist = artist.InnerText;
                trek.Title = title.InnerText;
                trek.Duration = new TimeSpan(0, 0, Convert.ToInt32(duration.InnerText)); ;
                trek.Url = url.InnerText;
               
                ListAudios.Add(trek);
            }
            return ListAudios;
        }

        /// <summary>
        /// Асинхронно грузит указанное количество аудиозаписей
        /// </summary>
        /// <param name="CountAudio"></param>
        /// <returns></returns>
        public static Task<List<Audio>> GetMyAudiosAsync(int CountAudio)
        {
            return Task.Run(() =>
            {
                ListAudios = GetMyAudios(CountAudio);
                return ListAudios;
            });
        }

        /// <summary>
        /// Ищет аудиозаписи 
        /// </summary>
        /// <param name="countAudio"></param>
        /// <returns></returns>
        public static List<Audio> SearchAudios(string text, int countAudio)
        {
            ListAudios = new List<Audio>();

            System.Net.WebRequest reqGET =
                System.Net.WebRequest.Create(@"https://api.vk.com/method/audio.search.xml?q=" + text + "&count=" + countAudio + "&v=5.29&access_token=" + VkMain.token);
            System.Net.WebResponse resp = reqGET.GetResponse();
            System.IO.Stream stream = resp.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(stream);
            string s = sr.ReadToEnd();


            XmlDocument doc = new XmlDocument();
            doc.LoadXml(s);
            XmlNodeList bookNodes = doc.GetElementsByTagName("audio");

            foreach (XmlNode bookNode in bookNodes)
            {
                XmlNode id = bookNode["id"];
                XmlNode owner_id = bookNode["owner_id"];
                XmlNode artist = bookNode["artist"];
                XmlNode title = bookNode["title"];
                XmlNode duration = bookNode["duration"];
                XmlNode url = bookNode["url"];

                Audio trek = new Audio();

                trek.Id = id.InnerText;
                trek.OwnerId = owner_id.InnerText;
                trek.Artist = artist.InnerText;
                trek.Title = title.InnerText;
                trek.Duration = new TimeSpan(0, 0, Convert.ToInt32(duration.InnerText)); ;
                trek.Url = url.InnerText;

                ListAudios.Add(trek);
            }
            return ListAudios;
        }

        /// <summary>
        /// Асинхронно ищет аудиозаписи
        /// </summary>
        /// <param name="countAudio"></param>
        /// <returns></returns>
        public static Task<List<Audio>> SearchAudiosAsync(string text, int countAudio=300)
        {
            return Task.Run(() =>
            {
                ListAudios = SearchAudios(text, countAudio);
                return ListAudios;
            });
        }
        
    }
}