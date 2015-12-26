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
using VKAPI.Model.UsersModel;


namespace VKAPI
{
    public class VkMessage : VkBase
    {
        
        /// <summary>
        /// Возвращает список диалогов текущего пользователя.
        /// </summary>
        /// <returns></returns>
        public DialogsModel GetDialogs(int count)
        {
            //используемый метод
            Method = "messages.getDialogs";

            //добавляем параметры если есть
            var parameters = new Dictionary<string, object>
            {
                {"count=", count}
            };
             AddParameters(parameters);
            //получаем данные в json
            string str = GetData();
            //десериализуем
            var dialogsModel = JsonConvert.DeserializeObject<DialogsModel>(str);

            return dialogsModel;
        }

        /// <summary>
        ///     Асинхронно возвращает список диалогов текущего пользователя.
        /// </summary>
        /// <param name="countAudio"></param>
        /// <returns></returns>
        public Task<DialogsModel> GetDialogsAsync(int count = 15)
        {
            return Task.Run(() =>
            {
                DialogsModel messageModel = GetDialogs(count);
                return messageModel;
            });
        }

        public void Get()
        {
            //используемый метод
            Method = "messages.get";

            //добавляем параметры если есть
            //var parameters = new Dictionary<string, object>
            //{
            //    {"count=", count}
            //};
            //AddParameters(parameters);
            //получаем данные в json
            string str = GetData();
            //десериализуем
            //DialogsModel dialogsModel = JsonConvert.DeserializeObject<DialogsModel>(str);

            //return dialogsModel;
        }
  
    }
}