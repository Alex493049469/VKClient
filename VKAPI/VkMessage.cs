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
        public DialogsModel Get(int count)
        {
            //используемый метод
            Method = "messages.getDialogs";
            //поля
            ClearParameters();
            //добавляем параметры если есть
             AddParameter("count=", count);

            //получаем данные в json
            string str = GetData();
            //десериализуем
            DialogsModel dialogsModel = JsonConvert.DeserializeObject<DialogsModel>(str);

            //далее сразу подгружаем фото пользователя или группы
            string ids = "";
            foreach (var item in dialogsModel.response.items)
            {
                if (ids.Length == 0)
                {
                    ids = item.message.user_id.ToString();
                }
                else
                {
                    ids += "," + item.message.user_id.ToString();
                }
            }


            VkUsers vkusers = new VkUsers();
            UsersModel um = vkusers.Get(ids, "", VkUsers.nameCase.nom);

            for (int i = 0; i < dialogsModel.response.items.Count; i++)
            {
                if (dialogsModel.response.items[i].message.photo_100 ==null)
                dialogsModel.response.items[i].message.photo_100 = um.response[i].photo_100;
            }
            
           
            return dialogsModel;
        }

        /// <summary>
        ///     Асинхронно возвращает список диалогов текущего пользователя.
        /// </summary>
        /// <param name="countAudio"></param>
        /// <returns></returns>
        public Task<DialogsModel> GetAsync(int count = 10)
        {
            return Task.Run(() =>
            {
                DialogsModel messageModel = Get(count);
                return messageModel;
            });
        }


        

        
    }
}