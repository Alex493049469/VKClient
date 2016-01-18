using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VKAPI.Handlers;
using VKAPI.Model.DialogsModel;
using VKAPI.Model.ErrorModel;
using VKAPI.Model.MessagesModel;

namespace VKAPI.Category
{
	public class Messages 
	{
		
		/// <summary>
		/// Возвращает список диалогов текущего пользователя.
		/// </summary>
		/// <returns></returns>
		public DialogsModel GetDialogs(int count, int offset = 0)
		{
			//используемый метод
			VkRequest.Method = "messages.getDialogs";

			//добавляем параметры если есть
			var parameters = new Dictionary<string, object>
			{
				{"count=", count},
				{"offset=", offset}
			};
			VkRequest.AddParameters(parameters);
			//получаем данные в json
			string str = VkRequest.GetData();
			//десериализуем
			
			var dialogsModel = JsonConvert.DeserializeObject<DialogsModel>(str);
			//if (dialogsModel == null)
			//{
			//	var ErrorHandler = new ErrorHandlerRequest(JsonConvert.DeserializeObject<ErrorModel>(str));
				
			//}

			return dialogsModel;
		}

		/// <summary>
		///     Асинхронно возвращает список диалогов текущего пользователя.
		/// </summary>
		/// <param name="countAudio"></param>
		/// <returns></returns>
		public Task<DialogsModel> GetDialogsAsync(int count, int offset = 0)
		{
			return Task.Run(() =>
			{
				DialogsModel messageModel = GetDialogs(count, offset);
				return messageModel;
			});
		}

		public void GetChat(string chatId, string fields)
		{
			//используемый метод
			VkRequest.Method = "messages.getChat";

			//добавляем параметры если есть
			var parameters = new Dictionary<string, object>
			{
				{"chat_id=", chatId},
				{"fields=", fields}
			};
			VkRequest.AddParameters(parameters);
			//получаем данные в json
			string str = VkRequest.GetData();
			//десериализуем
			//DialogsModel dialogsModel = JsonConvert.DeserializeObject<DialogsModel>(str);

			//return dialogsModel;
		}

		public MessagesModel GetHistoryChat(int IdChat, int count, int rev = 0)
		{
			//используемый метод
			VkRequest.Method = "messages.getHistory";

			//добавляем параметры если есть
			var parameters = new Dictionary<string, object>
			{
				{"chat_id=", IdChat},
				{"count=", count},
				{"rev=", rev}
			};
			VkRequest.AddParameters(parameters);
			//получаем данные в json
			string str = VkRequest.GetData();
			//десериализуем
			MessagesModel messagesModel = JsonConvert.DeserializeObject<MessagesModel>(str);

			return messagesModel;
		}

		public Task<MessagesModel> GetHistoryChatAsync(int IdChat, int count, int rev = 0)
		{
			return Task.Run(() =>
			{
				MessagesModel messageModel = GetHistoryChat(IdChat, count, rev);
				return messageModel;
			});
		}

		public MessagesModel GetHistoryUser(int IdUser, int count, int rev = 0)
		{
			//используемый метод
			VkRequest.Method = "messages.getHistory";

			//добавляем параметры если есть
			var parameters = new Dictionary<string, object>
			{
				{"user_id=", IdUser},
				{"count=", count},
				{"rev=", rev}
			};
			VkRequest.AddParameters(parameters);
			//получаем данные в json
			string str = VkRequest.GetData();
			//десериализуем
			MessagesModel messagesModel = JsonConvert.DeserializeObject<MessagesModel>(str);

			return messagesModel;
		}

		public Task<MessagesModel> GetHistoryUserAsync(int IdUser, int count, int rev = 0)
		{
			return Task.Run(() =>
			{
				MessagesModel messageModel = GetHistoryUser(IdUser, count, rev);
				return messageModel;
			});
		}
  
	}
}