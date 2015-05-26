using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VKAPI.Model;
using VKAPI.Model.AudioModel;


namespace VKAPI
{
    /// <summary>
    ///     Класс для работы с аудиозаписями
    /// </summary>
    public static class VkAudio
    {
        /// <summary>
        ///     Грузит указанное количество аудиозаписей
        /// </summary>
        /// <param name="countAudio"></param>
        /// <returns></returns>
        public static AudioModel Get(int countAudio)
        {
            WebRequest reqGET =
                WebRequest.Create(@"https://api.vk.com/method/audio.get?count=" + countAudio +
                                  "&v=5.29&access_token=" + VkMain.token);
            WebResponse resp = reqGET.GetResponse();
            Stream stream = resp.GetResponseStream();
            var sr = new StreamReader(stream);
            string s = sr.ReadToEnd();

            //десериализуем
            AudioModel audioModel = JsonConvert.DeserializeObject<AudioModel>(s);

            return audioModel;
        }

        /// <summary>
        ///     Асинхронно грузит указанное количество аудиозаписей
        /// </summary>
        /// <param name="countAudio"></param>
        /// <returns></returns>
        public static Task<AudioModel> GetAsync(int countAudio)
        {
            return Task.Run(() =>
            {
                AudioModel audioModel = Get(countAudio);
                return audioModel;
            });
        }

        /// <summary>
        ///     Ищет аудиозаписи
        /// </summary>
        /// <param name="countAudio"></param>
        /// <returns></returns>
        public static AudioModel Search(string text, int countAudio)
        {
            WebRequest reqGET =
                WebRequest.Create(@"https://api.vk.com/method/audio.search?q=" + text + "&count=" + countAudio +
                                  "&v=5.29&access_token=" + VkMain.token);
            WebResponse resp = reqGET.GetResponse();
            Stream stream = resp.GetResponseStream();
            var sr = new StreamReader(stream);
            string s = sr.ReadToEnd();

            //десериализуем
            AudioModel audioModel = JsonConvert.DeserializeObject<AudioModel>(s);

            return audioModel;
        }

        /// <summary>
        ///     Асинхронно ищет аудиозаписи
        /// </summary>
        /// <param name="countAudio"></param>
        /// <returns></returns>
        public static Task<AudioModel> SearchAsync(string text, int countAudio = 300)
        {
            return Task.Run(() =>
            {
                AudioModel audioModel = Search(text, countAudio);
                return audioModel;
            });
        }
    }
}