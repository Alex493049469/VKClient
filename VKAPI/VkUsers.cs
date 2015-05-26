using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VKAPI.Model;
using VKAPI.Model.UsersModel;

namespace VKAPI
{
   public static class VkUsers
    {
       private static string url = @"https://api.vk.com/method/users.get?";
       private static string fields = "";
       private static string apiVersion = "&v=5.29";

       private static string FieldsAll = "sex, bdate, city, country, photo_50, photo_100, photo_200_orig, photo_200, photo_400_orig, photo_max, photo_max_orig, photo_id, online, online_mobile, domain, has_mobile, contacts, connections, site, education, universities, schools, can_post, can_see_all_posts, can_see_audio, can_write_private_message, status, last_seen, common_count, relation, relatives, counters, screen_name, maiden_name, timezone, occupation,activities, interests, music, movies, tv, books, games, about, quotes, personal, friends_status";

       //параметры
       private static List<string> param = new List<string>();
       

       //падеж для склонения имени и фамилии пользователя. 
       //Возможные значения: именительный – nom, родительный – gen, дательный – dat, винительный – acc, творительный – ins, предложный – abl. По умолчанию nom. 
       public enum name_case
       {
           nom,
           gen,
           dat,
           acc,
           ins,
           abl
       }

       
       /// <summary>
        /// Возвращает расширенную информацию о пользователях.
       /// </summary>
        /// <param name="user_ids">идентификатор(ы) пользователя(ей), По умолчанию — идентификатор текущего пользователя. </param>
       /// <param name="fields">поля которые необходимо получить</param>
        /// <param name="nameCase">падеж в котором вернуть имя и фамилию пользователя(ей), По умолчанию nom. </param>
       /// <returns></returns>
       public static UsersModel Get(string user_ids, string fields, name_case nameCase)
        {
           if (user_ids != "")
           {
               param.Add("user_ids=" + user_ids);
           }
           if (fields != "")
           {
               param.Add("fields=" + fields);
           }
           if (nameCase == name_case.nom)
           {
               param.Add(nameCase.ToString());
           }

           string query = "";
           for (int i = 0; i < param.Count; i++)
           {
               if (param[i] != "")
               {
                   if (query.Length > 0)
                   {
                       query += "&" + param[i];
                   }
                   else
                   {
                       query = param[i];
                   }
               }
           }

            WebRequest reqGET = WebRequest.Create(@"https://api.vk.com/method/users.get?fields="+FieldsAll+"&v=5.29&access_token=" + VkMain.token);
            WebResponse resp = reqGET.GetResponse();
            Stream stream = resp.GetResponseStream();
            var sr = new StreamReader(stream);
            string s = sr.ReadToEnd();

            //десериализуем
           UsersModel usersModel = JsonConvert.DeserializeObject<UsersModel>(s);

           return usersModel;
        }


       public static Task<UsersModel> GetAsync(string user_ids, string fields, name_case nameCase)
        {
            return Task.Run(() =>
            {
                UsersModel userModel = Get(user_ids, fields, nameCase);
                return userModel;
            });
        }

       //public static 
    }
}
