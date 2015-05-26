using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json.Linq;
using VKAPI.Model;

namespace VKAPI
{
   public static class VkLongPool
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
                   @"http://" + longPoll.Server + "?act=a_check&key=" + longPoll.Key + "&ts=" + longPoll.Ts + "&wait=25&mode=2");
            WebResponse resp = await reqGET.GetResponseAsync();
            Stream stream = resp.GetResponseStream();
            var sr = new StreamReader(stream);
            string s = sr.ReadToEnd();

            JObject test = JObject.Parse(s);

            //var m = JsonConvert.DeserializeObject<T>(s);
            //{"ts":1833857877,"updates":[[61,37736503,1]]}


        }
    }
}
