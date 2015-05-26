using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VKAPI.Model;
using VKAPI.Model.FriendsModel;



namespace VKAPI
{
    /// <summary>
    ///     Класс для работы с Друзьями
    /// </summary>
    public static class VkFriends
    {
        //список всех полей
        private static string FieldsAll = "nickname, domain, sex, bdate, city, country, timezone, photo_50, photo_100, photo_200_orig, has_mobile, contacts, education, online, relation, last_seen, status, can_write_private_message, can_see_all_posts, can_post, universities ";
        private static string FieldsStandart = "nickname, photo_50, photo_100, online, status";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static FriendsModel Get()
        {
            //формирование строки запроса
            string param;
            WebRequest reqGET =
                WebRequest.Create(
                    @"https://api.vk.com/method/friends.get?fields="+FieldsAll+"&order=hints&v=5.29&access_token=" +
                    VkMain.token);
            WebResponse resp = reqGET.GetResponse();
            Stream stream = resp.GetResponseStream();
            var sr = new StreamReader(stream);
            string s = sr.ReadToEnd();

            //десериализуем
            FriendsModel friendsModel = JsonConvert.DeserializeObject<FriendsModel>(s);

            return friendsModel;
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