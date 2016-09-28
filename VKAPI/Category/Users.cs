using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VKAPI.Handlers;
using VKAPI.Model.UsersModel;

namespace VKAPI.Category
{
	public class Users 
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
	   public UsersModel Get(string userIds ="", string fields = "", nameCase nameCase = nameCase.nom)
	   {
		   string FieldsAll = "sex, bdate, city, country, photo_50, photo_100, photo_200_orig, photo_200, photo_400_orig, photo_max, photo_max_orig, photo_id, online, online_mobile, domain, has_mobile, contacts, connections, site, education, universities, schools, can_post, can_see_all_posts, can_see_audio, can_write_private_message, status, last_seen, common_count, relation, relatives, counters, screen_name, maiden_name, timezone, occupation,activities, interests, music, movies, tv, books, games, about, quotes, personal, friends_status";

		   //добавляем параметры
		   var parameters = new Dictionary<string, object>
		   {
			   {"user_ids=", userIds},
			   {"fields=", fields.Length > 0 ? fields : FieldsAll},
			   {"name_case=", nameCase.ToString()}
		   };

		   string str = VkRequest.GetData("users.get", parameters);
		   //десериализуем
		   UsersModel usersModel = JsonConvert.DeserializeObject<UsersModel>(str);
		   return usersModel;
	   }

	   public Task<UsersModel> GetAsync(string user_ids = "")
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

		   //добавляем параметры
		   var parameters = new Dictionary<string, object>
		   {
			   {"user_ids=", userIds},
			   {"fields=", fieldsPhoto},
		   };

		   string str = VkRequest.GetData("users.get", parameters);
		   //десериализуем
		   UsersModel usersModel = JsonConvert.DeserializeObject<UsersModel>(str);
		   if (usersModel.response == null)
		   {
				str = VkRequest.GetData("users.get", parameters);
			}
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
