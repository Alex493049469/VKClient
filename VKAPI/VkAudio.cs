using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VKAPI.Model.AudioModel;
using System.Reflection;
using System.Security.Policy;

namespace VKAPI
{
    /// <summary>
    ///     Класс для работы с аудиозаписями
    /// </summary>
    public class VkAudio
    {
        //формируемая строка необходимых пареметров
        private string _url;

        private string Url
        {
            get { return _url; }
            set
            {
                if (!string.IsNullOrEmpty(_url))
                {
                    _url += "&" + value;
                }
                else
                {
                    
                    _url += value;
                }
            }
        }

        /// <summary>
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
        public AudioModel Get(int ownerId, int albumId, string audioIds, int offset , int count)
        {
            _url = null;

            if (ownerId > 0)
            {
                Url = "owner_id=" + ownerId;
            }
            if (albumId > 0)
            {
                Url = "album_id=" + albumId;
            }
            if (!string.IsNullOrEmpty(audioIds))
            {
                Url = "audio_ids=" + audioIds;
            }
            if (offset > 0)
            {
                Url = "offset=" + offset;
            }
            if (count > 0)
            {
                Url = "count=" + count;
            }
           
            var reqGet =
                WebRequest.Create(@"https://api.vk.com/method/audio.get?" + Url +
                                  "&v=5.29&access_token=" + VkMain.token);
            var resp = reqGet.GetResponse();
            var stream = resp.GetResponseStream();
            var sr = new StreamReader(stream);
            var s = sr.ReadToEnd();

            //десериализуем
            var audioModel = JsonConvert.DeserializeObject<AudioModel>(s);

            return audioModel;
        }

        public Task<AudioModel> GetAsync(int ownerId = 0, int albumId = 0, string audioIds = null,  int offset = 0, int count =1000)
        {
            return Task.Run(() =>
            {
                var audioModel = Get(ownerId, albumId, audioIds, offset, count);
                return audioModel;
            });
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
        public AudioModel Search(string q, short autoComplete, short lyrics, short performerOnly, short sort, short searchOwn, int offset, int count)
        {
            _url = null;

            if (!string.IsNullOrEmpty(q))
            {
                Url = "q=" + q;
            }
            if (autoComplete > 0)
            {
                Url = "auto_complete=" + autoComplete;
            }
            if (lyrics > 0)
            {
                Url = "lyrics=" + lyrics;
            }
            if (performerOnly > 0)
            {
                Url = "performer_only=" + performerOnly;
            }
            if (sort > 0)
            {
                Url = "sort=" + sort;
            }
            if (searchOwn > 0)
            {
                Url = "search_own=" + searchOwn;
            }
            if (offset > 0)
            {
                Url = "offset=" + offset;
            }
            if (count > 0)
            {
                Url = "count=" + count;
            }


            var reqGet =
                WebRequest.Create(@"https://api.vk.com/method/audio.search?" + Url +
                                  "&v=5.29&access_token=" + VkMain.token);
            var resp = reqGet.GetResponse();
            var stream = resp.GetResponseStream();
            var sr = new StreamReader(stream);
            var s = sr.ReadToEnd();

            //десериализуем
            var audioModel = JsonConvert.DeserializeObject<AudioModel>(s);

            return audioModel;
        }

        public Task<AudioModel> SearchAsync(string q, short autoComplete=1, short lyrics = 0, short performerOnly =0, short sort =0, short searchOwn = 1, int offset =0, int count=300)
        {
            return Task.Run(() =>
            {
                var audioModel = Search(q, autoComplete, lyrics, performerOnly, sort, searchOwn, offset, count);
                return audioModel;
            });
        }

        /// <summary>
        ///     Копирует аудиозапись на страницу пользователя
        /// </summary>
        /// <param name="countAudio"></param>
        /// <returns></returns>
        public void Add(int audio_id, int owner_id)
        {
            var reqGET =
                WebRequest.Create(@"https://api.vk.com/method/audio.add?audio_id=" + audio_id + "&owner_id=" + owner_id +
                                  "&v=5.29&access_token=" + VkMain.token);
            var resp = reqGET.GetResponse();
            var stream = resp.GetResponseStream();
            var sr = new StreamReader(stream);
            var s = sr.ReadToEnd();
        }

        public Task AddAsync(int audio_id, int owner_id)
        {
            return Task.Run(() => { Add(audio_id, owner_id); });
        }

        /// <summary>
        ///   Удаляет аудиозапись со страницы пользователя
        /// </summary>
        /// <param name="countAudio"></param>
        /// <returns></returns>
        public void Delete(int audio_id, int owner_id)
        {
            var reqGET =
                WebRequest.Create(@"https://api.vk.com/method/audio.delete?audio_id=" + audio_id + "&owner_id=" +
                                  owner_id + "&v=5.29&access_token=" + VkMain.token);
            var resp = reqGET.GetResponse();
            var stream = resp.GetResponseStream();
            var sr = new StreamReader(stream);
            var s = sr.ReadToEnd();
        }

        /// <summary>
        ///    Удаляет аудиозапись со страницы пользователя
        /// </summary>
        /// <param name="countAudio"></param>
        /// <returns></returns>
        public Task DeleteAsync(int audio_id, int owner_id)
        {
            return Task.Run(() => { Delete(audio_id, owner_id); });
        }
    }
}