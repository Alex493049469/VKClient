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
    public class VkFriends : VkBase
    {
        //список всех полей
        private string FieldsAll = "nickname, domain, sex, bdate, city, country, timezone, photo_50, photo_100, photo_200_orig, has_mobile, contacts, education, online, relation, last_seen, status, can_write_private_message, can_see_all_posts, can_post, universities ";
        private string FieldsStandart = "nickname, photo_50, photo_100, online, status";

        public FriendsModel Get()
        {
            Method = "friends.get";
            var parameters = new Dictionary<string, object>
            {
                {"fields=", FieldsStandart},
                {"order=", "hints"},
            };
            AddParameters(parameters);
            string str = GetData();
            FriendsModel friendsModel = JsonConvert.DeserializeObject<FriendsModel>(str);
            return friendsModel;
        }

        /// <summary>
        ///     Асинхронно грузит данные о друзьях
        /// </summary>
        /// <param name="CountAudio"></param>
        /// <returns></returns>
        public Task<FriendsModel> GetAsync()
        {
            return Task.Run(() =>
            {
                FriendsModel userModel = Get();
                return userModel;
            });
        }
    }
}