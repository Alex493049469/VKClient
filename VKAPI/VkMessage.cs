using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VKAPI.Model;

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

        public static void GetDialogs()
        {
            List<Message> liatMessages = new List<Message>();
            WebRequest reqGET =
                WebRequest.Create(
                   @"https://api.vk.com/method/messages.getDialogs.xml?v=5.29&access_token=" + VkMain.token);
            WebResponse resp = reqGET.GetResponse();
            Stream stream = resp.GetResponseStream();
            var sr = new StreamReader(stream);
            string s = sr.ReadToEnd();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(s);
            XmlNodeList bookNodes = doc.GetElementsByTagName("message");

            foreach (XmlNode bookNode in bookNodes)
            {
                XmlNode id = bookNode["id"];
                XmlNode date = bookNode["date"];
                XmlNode Out = bookNode["out"];
                XmlNode user_id = bookNode["user_id"];
                XmlNode read_state = bookNode["read_state"];
                XmlNode title = bookNode["title"];
                XmlNode body = bookNode["body"];
                XmlNode chat_id = bookNode["chat_id"];
                XmlNode users_count = bookNode["users_count"];
                XmlNode admin_id = bookNode["admin_id"];

                Message trek = new Message();

                trek.Id = id.InnerText;
                trek.Date = ConvertUnixTimeToDateTime(Convert.ToDouble(date.InnerText));
                trek.Out = Convert.ToDouble(Out.InnerText);
                trek.UserId = Convert.ToInt32(user_id.InnerText);
                trek.ReadState = Convert.ToInt32(read_state.InnerText);
                trek.Title = title.InnerText;
                trek.Body = body.InnerText;
                if (chat_id != null)
                trek.ChatId = Convert.ToInt32(chat_id.InnerText);
                if (users_count != null)
                trek.UsersCount = Convert.ToInt32(users_count.InnerText);
                if (admin_id != null)
                trek.AdminId = Convert.ToInt32(admin_id.InnerText);



                //       <message>
                // <id>42273</id>
                // <date>1428815619</date>
                // <out>0</out>
                // <user_id>7954808</user_id>
                // <read_state>1</read_state>
                // <title>Миша, Алексей, Александр</title>
                // <body>Ахахахха</body>
                // <chat_id>114</chat_id>
                // <chat_active list="true">
                //  <item>89415824</item>
                //  <item>7954808</item>
                //  <item>159619107</item>
                // </chat_active>
                // <push_settings>
                //  <sound>1</sound>
                //  <disabled_until>18</disabled_until>
                // </push_settings>
                // <users_count>4</users_count>
                // <admin_id>89415824</admin_id>
                //</message>

                liatMessages.Add(trek);
            }

            
        }

        public static DateTime ConvertUnixTimeToDateTime(double timestamp)
        {
            // This is an example of a UNIX timestamp for the date/time 11-04-2005 09:25.
            //double timestamp = 1113211532;

            // First make a System.DateTime equivalent to the UNIX Epoch.
            System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);

            // Add the number of seconds in UNIX timestamp to be converted.
            dateTime = dateTime.AddSeconds(timestamp);

            // The dateTime now contains the right date/time so to format the string,
            // use the standard formatting methods of the DateTime object.
            string printDate = dateTime.ToShortDateString() + " " + dateTime.ToShortTimeString();

            // Print the date and time
            System.Console.WriteLine(printDate);
            return dateTime;
        }

        
    }
}