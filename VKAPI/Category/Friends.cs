using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VKAPI.Handlers;
using VKAPI.Model.FriendsModel;

namespace VKAPI.Category
{
	/// <summary>
	///     Класс для работы с Друзьями
	/// </summary>
	public class Friends
	{
		//список всех полей
		private string FieldsAll = "nickname, domain, sex, bdate, city, country, timezone, photo_50, photo_100, photo_200_orig, has_mobile, contacts, education, online, relation, last_seen, status, can_write_private_message, can_see_all_posts, can_post, universities ";
		private string FieldsStandart = "nickname, photo_50, photo_100, online, status";

		public FriendsModel Get()
		{
			VkRequest.Method = "friends.get";
			var parameters = new Dictionary<string, object>
			{
				{"fields=", FieldsStandart},
				{"order=", "hints"},
			};
			VkRequest.AddParameters(parameters);
			string str = VkRequest.GetData();
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