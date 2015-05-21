using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VKAPI.Model;
using VKAPI.Utils;

namespace VKAPI
{
   public static class VkUser
    {
        //public static List<User> ListFriends;

        /// <summary>
        ///     Грузит данные о друзьях
        /// </summary>
        /// <param name="CountAudio"></param>
        /// <returns></returns>
       public static UserModel Get()
        {
            WebRequest reqGET =
                WebRequest.Create(
                    @"https://api.vk.com/method/users.get.xml?fields=sex,bdate,online,photo_100,photo_50,photo_200_orig&v=5.29&access_token=" +
                    VkMain.token);
            WebResponse resp = reqGET.GetResponse();
            Stream stream = resp.GetResponseStream();
            var sr = new StreamReader(stream);
            string s = sr.ReadToEnd();

            //десериализуем
            UserModel userModel = Serializer<UserModel>.Deserialize(s);

            return userModel;
        }

        /// <summary>
        ///     Асинхронно грузит данные о друзьях
        /// </summary>
        /// <param name="CountAudio"></param>
        /// <returns></returns>
       public static Task<UserModel> GetAsync()
        {
            return Task.Run(() =>
            {
                UserModel userModel = Get();
                return userModel;
            });
        }
    }
}
