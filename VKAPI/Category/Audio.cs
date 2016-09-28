using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VKAPI.Handlers;
using VKAPI.Model;
using VKAPI.Model.AudioModel;

namespace VKAPI.Category
{
	/// <summary>
	///     Класс для работы с аудиозаписями
	/// </summary>
	public class Audio
	{
		///<summary>
		///    Возвращает список аудиозаписей пользователя или сообщества
		/// </summary>
		/// <param name="ownerId">идентификатор владельца аудиозаписей</param>
		/// <param name="albumId">идентификатор альбома с аудиозаписями</param>
		/// <param name="audioIds">
		///     Идентификаторы аудиозаписей, информацию о которых необходимо вернуть.(список положительных
		///     чисел, разделенных запятыми)
		/// </param>
		/// <param name="offset">смещение, необходимое для выборки определенного количества аудиозаписей. По умолчанию — 0. </param>
		/// <param name="count">количество аудиозаписей, информацию о которых необходимо вернуть. Максимальное значение — 6000</param>
		/// <returns></returns>
		public AudioModel Get(int ownerId = 0, int albumId = 0, string audioIds = "", int offset = 0, int count = 1000)
		{
			var parameters = new Dictionary<string, object>
			{
				{"owner_id=", ownerId},
				{"album_id=", albumId},
				{"audio_ids=", audioIds},
				{"offset=", offset},
				{"need_user=", 0},
				{"count=", count}
			};

			string str = VkRequest.GetData("audio.get", parameters);
			//десериализуем
			var audioModel = JsonConvert.DeserializeObject<AudioModel>(str);
			return audioModel;
		}

		public Task<AudioModel> GetAsync(int ownerId = 0, int albumId = 0, string audioIds = "",  int offset = 0, int count =1000)
		{
			return Task.Run(() => Get(ownerId, albumId, audioIds, offset, count));
		}


		public AudioModel GetRecommendations(string targetAudio)
		{
			var parameters = new Dictionary<string, object>
			{
				{"target_audio=", targetAudio},
			};

			string str = VkRequest.GetData("audio.getRecommendations", parameters);
			//десериализуем
			var audioModel = JsonConvert.DeserializeObject<AudioModel>(str);
			return audioModel;
		}

		public Task<AudioModel> GetRecommendationsAsync(string targetAudio)
		{
			return Task.Run(() => GetRecommendations(targetAudio));
		}

		/// <summary>
		/// Возвращает список аудиозаписей в соответствии с заданным критерием поиска. 
		/// </summary>
		/// <param name="q">текст поискового запроса</param>
		/// <param name="autoComplete">Если этот параметр равен 1, возможные ошибки в поисковом запросе будут исправлены. Например, при поисковом запросе Иуфедуы поиск будет осуществляться по строке Beatles. </param>
		/// <param name="lyrics">Если этот параметр равен 1, поиск будет производиться только по тем аудиозаписям, которые содержат тексты. </param>
		/// <param name="performerOnly">Если этот параметр равен 1, поиск будет осуществляться только по названию исполнителя. </param>
		/// <param name="sort">Вид сортировки. 2 — по популярности, 1 — по длительности аудиозаписи, 0 — по дате добавления. </param>
		/// <param name="searchOwn">1 – искать по аудиозаписям пользователя, 0 – не искать по аудиозаписям пользователя (по умолчанию). </param>
		/// <param name="offset">смещение, необходимое для выборки определенного подмножетсва аудиозаписей. По умолчанию — 0. </param>
		/// <param name="count">количество аудиозаписей, информацию о которых необходимо вернуть. максимальное значение 300</param>
		/// <returns></returns>
		public AudioModel Search(string q, int autoComplete, int lyrics, int performerOnly, int sort, int searchOwn, int offset, int count)
		{
			var parameters = new Dictionary<string, object>
			{
				{"q=", q},
				{"auto_complete=", autoComplete},
				{"lyrics=", lyrics},
				{"performer_only=", performerOnly},
				{"sort=", sort},
				{"search_own=", searchOwn},
				{"offset=", offset},
				{"count=", count}
			};

			string str = VkRequest.GetData("audio.search", parameters);
			//десериализуем
			var audioModel = JsonConvert.DeserializeObject<AudioModel>(str);
			return audioModel;
		}

		public Task<AudioModel> SearchAsync(string q, short autoComplete=1, short lyrics = 0, short performerOnly =0, short sort =0, short searchOwn = 1, int offset =0, int count=300)
		{
			return Task.Run(() => Search(q, autoComplete, lyrics, performerOnly, sort, searchOwn, offset, count));
		}

	   
		/// <summary>
		///  Копирует аудиозапись на страницу пользователя
		/// </summary>
		/// <param name="audioId">Идентификатор аудиозаписи</param>
		/// <param name="ownerId">идентификатор владельца аудиозаписи (пользователь или сообщество). </param>
		public void Add(int audioId, int ownerId)
		{
			var parameters = new Dictionary<string, object>
			{
				{"audio_id=", audioId},
				{"owner_id=", ownerId}
			
			};

			VkRequest.GetData("audio.add", parameters);
		}

		public Task AddAsync(int audioId, int ownerId)
		{
			return Task.Run(() => { Add(audioId, ownerId); });
		}

		/// <summary>
		/// Удаляет аудиозапись со страницы пользователя
		/// </summary>
		/// <param name="audioId"></param>
		/// <param name="ownerId"></param>
		public void Delete(int audioId, int ownerId)
		{
			var parameters = new Dictionary<string, object>
			{
				{"audio_id=", audioId},
				{"owner_id=", ownerId}
			
			};

			VkRequest.GetData("audio.delete", parameters);
		}

		public Task DeleteAsync(int audioId, int ownerId)
		{
			return Task.Run(() => { Delete(audioId, ownerId); });
		}

		public void Edit(int ownerId, int audioId, string artist, string title, string text, int genre_id, int? no_search=0)
		{
			var parameters = new Dictionary<string, object>
			{
				{"owner_id=", ownerId},
				{"audio_id=", audioId},
				{"artist=", artist},
				{"title=", title},
				{"genre_id=", genre_id},
				{"no_search=", no_search},
			};

			 var qwe = VkRequest.PostData("audio.edit", parameters, "&text = " + text);
		}

		public Task EditAsync(int ownerId, int audioId, string artist, string title, string text, int genre_id, int? no_search)
		{
			return Task.Run(() => Edit(ownerId, audioId, artist, title, text, genre_id, no_search));
		}

		public LyricsModel GetLyrics(int lyrics_id)
		{
			var parameters = new Dictionary<string, object>
			{
				{"lyrics_id=", lyrics_id}
			};

			string str = VkRequest.GetData("audio.getLyrics", parameters);
			//десериализуем
			var audioModel = JsonConvert.DeserializeObject<LyricsModel>(str);
			return audioModel;
		}

		public Task<LyricsModel> GetLyricsAsync(int lyrics_id)
		{
			return Task.Run(() => GetLyrics(lyrics_id));
		}
	}
}