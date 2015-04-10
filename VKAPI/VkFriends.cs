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
    /// Класс для работы с Друзьями
    /// </summary>
    public static class VkFriends
    {
        public static List<User> ListFriends;

        /// <summary>
        /// Грузит данные о друзьях
        /// </summary>
        /// <param name="CountAudio"></param>
        /// <returns></returns>
        public static List<User> GetMyFriends()
        {
            ListFriends = new List<User>();

            System.Net.WebRequest reqGET =
                System.Net.WebRequest.Create(@"https://api.vk.com/method/friends.get.xml?fields=online,photo_100&order=hints&v=5.29&access_token=" + VkMain.token);
            System.Net.WebResponse resp = reqGET.GetResponse();
            System.IO.Stream stream = resp.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(stream);
            string s = sr.ReadToEnd();


            XmlDocument doc = new XmlDocument();
            doc.LoadXml(s);
            XmlNodeList bookNodes = doc.GetElementsByTagName("user");

            foreach (XmlNode bookNode in bookNodes)
            {
                XmlNode id = bookNode["id"];
                XmlNode first_name = bookNode["first_name"];
                XmlNode last_name = bookNode["last_name"];
                XmlNode photo_100 = bookNode["photo_100"];
                XmlNode online = bookNode["online"];
                             

                User friend = new User();

                friend.id = id.InnerText;
                friend.first_name = first_name.InnerText;
                friend.last_name = last_name.InnerText;
                friend.photo_100 = photo_100.InnerText;
                if (online.InnerText == "1") friend.online = "Online";
                
                

                ListFriends.Add(friend);
            }
            return ListFriends;
        }

        /// <summary>
        /// Асинхронно грузит данные о друзьях
        /// </summary>
        /// <param name="CountAudio"></param>
        /// <returns></returns>
        public static Task<List<User>> GetMyFriendsAsync()
        {
            return Task.Run(() =>
            {
                ListFriends = GetMyFriends();
                return ListFriends;
            });
        }
    }
}
