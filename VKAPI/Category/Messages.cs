using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VKAPI.Model.DialogsModel;

namespace VKAPI.Category
{
    public class Messages : VkBase
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
        public Task<DialogsModel> GetDialogsAsync(int count = 50)
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