using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VKAPI.Model;
using VKAPI.Utils;

namespace VKAPI
{
    public static class VkMessage
    {
        public static LongPoll GetLongPollServer()
        {
            WebRequest reqGET =
                WebRequest.Create(
                    @"https://api.vk.com/method/messages.getLongPollServer.xml?use_ssl=0&access_token=" +
                    VkMain.token);
            WebResponse resp = reqGET.GetResponse();
            Stream stream = resp.GetResponseStream();
            var sr = new StreamReader(stream);
            string s = sr.ReadToEnd();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(s);
            XmlNodeList bookNodes = doc.GetElementsByTagName("response");


            LongPoll longPoll = new LongPoll();

            foreach (XmlNode bookNode in bookNodes)
            {
                XmlNode key = bookNode["key"];
                XmlNode server = bookNode["server"];
                XmlNode ts = bookNode["ts"];

                longPoll.Key = key.InnerText;
                longPoll.Server = server.InnerText;
                longPoll.Ts = Convert.ToInt32(ts.InnerText);
              
            }
            return longPoll;
        }

        public async static void ConnectLongPollServer()
        {
            //получаем параметры для подключения к серверу
            LongPoll longPoll = GetLongPollServer();
            WebRequest reqGET =
               WebRequest.Create(
                   @"http://"+longPoll.Server+"?act=a_check&key="+longPoll.Key+"&ts="+longPoll.Ts+"&wait=25&mode=2");
            WebResponse resp = await reqGET.GetResponseAsync();
            Stream stream = resp.GetResponseStream();
            var sr = new StreamReader(stream);
            string s = sr.ReadToEnd();

            JObject test = JObject.Parse(s);

            //var m = JsonConvert.DeserializeObject<T>(s);
            //{"ts":1833857877,"updates":[[61,37736503,1]]}


        }
        /// <summary>
        /// Возвращает список диалогов текущего пользователя.
        /// </summary>
        /// <returns></returns>
        public static MessageModel GetDialogs(int count)
        {
            List<Message> liatMessages = new List<Message>();
            WebRequest reqGET =
                WebRequest.Create(
                   @"https://api.vk.com/method/messages.getDialogs.xml?&v=5.29&access_token=" + VkMain.token);
            WebResponse resp = reqGET.GetResponse();
            Stream stream = resp.GetResponseStream();
            var sr = new StreamReader(stream);
            string s = sr.ReadToEnd();

            //десериализуем
            MessageModel messageModel = Serializer<MessageModel>.Deserialize(s);

            return messageModel;
        }

        /// <summary>
        ///     Асинхронно возвращает список диалогов текущего пользователя.
        /// </summary>
        /// <param name="countAudio"></param>
        /// <returns></returns>
        public static Task<MessageModel> GetDialogsAsync(int count =20)
        {
            return Task.Run(() =>
            {
                MessageModel messageModel = GetDialogs(count);
                return messageModel;
            });
        }


        

        
    }
}