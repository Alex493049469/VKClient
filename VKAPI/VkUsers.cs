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
    public class VkUsers : VkBase
    {

       //падеж для склонения имени и фамилии пользователя. 
       //Возможные значения: именительный – nom, родительный – gen, дательный – dat, винительный – acc, творительный – ins, предложный – abl. По умолчанию nom. 
       public enum nameCase
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
       /// <param name="userIds">идентификатор(ы) пользователя(ей), По умолчанию — идентификатор текущего пользователя. </param>
       /// <param name="fields">поля которые необходимо получить</param>
       /// <param name="nameCase">падеж в котором вернуть имя и фамилию пользователя(ей), По умолчанию nom. </param>
       /// <returns></returns>
       public UsersModel Get(string userIds, string fields, nameCase nameCase)
       {
           string Fieldsall = "sex, bdate, city, country, photo_50, photo_100, photo_200_orig, photo_200, photo_400_orig, photo_max, photo_max_orig, photo_id, online, online_mobile, domain, has_mobile, contacts, connections, site, education, universities, schools, can_post, can_see_all_posts, can_see_audio, can_write_private_message, status, last_seen, common_count, relation, relatives, counters, screen_name, maiden_name, timezone, occupation,activities, interests, music, movies, tv, books, games, about, quotes, personal, friends_status";
           //используемый метод
           Method = "users.get";
           //поля
           ClearParameters();
           //добавляем параметры
           AddParameter("user_ids=", userIds);
           AddParameter("fields=", fields.Length > 0 ? fields : Fieldsall);
           AddParameter("name_case=", nameCase.ToString());
           //получаем данные в json
           string str = GetData();
           //десериализуем
           UsersModel usersModel = JsonConvert.DeserializeObject<UsersModel>(str);
           return usersModel;
       }

       public UsersModel Get(string user_ids)
       {
            UsersModel userModel = Get(user_ids, "", nameCase.nom);
            return userModel;
       }


       public Task<UsersModel> GetAsync(string user_ids, string fields, nameCase nameCase)
       {
            return Task.Run(() =>
            {
                UsersModel userModel = Get(user_ids, fields, nameCase);
                return userModel;
            });
       }

       public Task<UsersModel> GetAsync(string user_ids)
       {
           return Task.Run(() =>
           {
               UsersModel userModel = Get(user_ids, "", nameCase.nom);
               return userModel;
           });
       }



    }
}
