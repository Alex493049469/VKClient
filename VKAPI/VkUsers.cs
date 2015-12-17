﻿using System;
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
           string FieldsAll = "sex, bdate, city, country, photo_50, photo_100, photo_200_orig, photo_200, photo_400_orig, photo_max, photo_max_orig, photo_id, online, online_mobile, domain, has_mobile, contacts, connections, site, education, universities, schools, can_post, can_see_all_posts, can_see_audio, can_write_private_message, status, last_seen, common_count, relation, relatives, counters, screen_name, maiden_name, timezone, occupation,activities, interests, music, movies, tv, books, games, about, quotes, personal, friends_status";
           //используемый метод
           Method = "users.get";

           //добавляем параметры
           var parameters = new Dictionary<string, object>
           {
               {"user_ids=", userIds},
               {"fields=", fields.Length > 0 ? fields : FieldsAll},
               {"name_case=", nameCase.ToString()}
           };
           AddParameters(parameters);
           //получаем данные в json
           string str = GetData();
           //десериализуем
           UsersModel usersModel = JsonConvert.DeserializeObject<UsersModel>(str);
           return usersModel;
       }

       public Task<UsersModel> GetAsync(string user_ids)
       {
           return Task.Run(() =>
           {
               UsersModel userModel = Get(user_ids, "", nameCase.nom);
               return userModel;
           });
       }

       public UsersModel GetPhoto(string userIds)
       {
           string fieldsPhoto = "photo_50, photo_100, photo_200_orig, photo_200, photo_400_orig, photo_max, photo_max_orig, photo_id";
          //используемый метод
           Method = "users.get";

           //добавляем параметры
           var parameters = new Dictionary<string, object>
           {
               {"user_ids=", userIds},
               {"fields=", fieldsPhoto},
           };
           AddParameters(parameters);
           //получаем данные в json
           string str = GetData();
           //десериализуем
           UsersModel usersModel = JsonConvert.DeserializeObject<UsersModel>(str);
           return usersModel;
       }

       public Task<UsersModel> GetPhotoAsync(string user_ids)
       {
           return Task.Run(() =>
           {
               UsersModel userModel = GetPhoto(user_ids);
               return userModel;
           });
       }




    }
}
