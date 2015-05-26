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
using VKAPI.Model.DialogsModel;


namespace VKAPI
{
    public static class VkMessage
    {
        
        /// <summary>
        /// Возвращает список диалогов текущего пользователя.
        /// </summary>
        /// <returns></returns>
        public static DialogsModel GetDialogs(int count)
        {
            List<Message> liatMessages = new List<Message>();
            WebRequest reqGET =
                WebRequest.Create(
                   @"https://api.vk.com/method/messages.getDialogs?&v=5.29&access_token=" + VkMain.token);
            WebResponse resp = reqGET.GetResponse();
            Stream stream = resp.GetResponseStream();
            var sr = new StreamReader(stream);
            string s = sr.ReadToEnd();

            //десериализуем
            DialogsModel dialogsModel = JsonConvert.DeserializeObject<DialogsModel>(s);

            return dialogsModel;
        }

        /// <summary>
        ///     Асинхронно возвращает список диалогов текущего пользователя.
        /// </summary>
        /// <param name="countAudio"></param>
        /// <returns></returns>
        public static Task<DialogsModel> GetDialogsAsync(int count = 20)
        {
            return Task.Run(() =>
            {
                DialogsModel messageModel = GetDialogs(count);
                return messageModel;
            });
        }


        

        
    }
}