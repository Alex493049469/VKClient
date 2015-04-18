using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using VKAPI.Model;
using VKAPI.Utils;

namespace VKAPI
{
    /// <summary>
    ///     Класс для работы с Друзьями
    /// </summary>
    public static class VkFriends
    {
        public static List<User> ListFriends;

        /// <summary>
        ///     Грузит данные о друзьях
        /// </summary>
        /// <param name="CountAudio"></param>
        /// <returns></returns>
        public static FriendsModel Get()
        {
            WebRequest reqGET =
                WebRequest.Create(
                    @"https://api.vk.com/method/friends.get.xml?fields=online,photo_100&order=hints&v=5.29&access_token=" +
                    VkMain.token);
            WebResponse resp = reqGET.GetResponse();
            Stream stream = resp.GetResponseStream();
            var sr = new StreamReader(stream);
            string s = sr.ReadToEnd();

            //десериализуем
            FriendsModel userModel = Serializer<FriendsModel>.Deserialize(s);

            return userModel;
        }

        /// <summary>
        ///     Асинхронно грузит данные о друзьях
        /// </summary>
        /// <param name="CountAudio"></param>
        /// <returns></returns>
        public static Task<FriendsModel> GetAsync()
        {
            return Task.Run(() =>
            {
                FriendsModel userModel = Get();
                return userModel;
            });
        }
    }
}