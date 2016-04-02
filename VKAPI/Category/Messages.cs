using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VKAPI.Handlers;
using VKAPI.Model.DialogsModel;
using VKAPI.Model.LongPullMessageModel;
using VKAPI.Model.LongPullModel;
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
			var parameters = new Dictionary<string, object>
			{
				{"count=", count},
				{"offset=", offset}
			};

			string data = VkRequest.GetData("messages.getDialogs", parameters);

			var dialogsModel = JsonConvert.DeserializeObject<DialogsModel>(data);

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
			var parameters = new Dictionary<string, object>
			{
				{"chat_id=", chatId},
				{"fields=", fields}
			};

			string str = VkRequest.GetData("messages.getChat", parameters);
			//десериализуем
			//DialogsModel dialogsModel = JsonConvert.DeserializeObject<DialogsModel>(str);

			//return dialogsModel;
		}

		public MessagesModel GetHistoryChat(int IdChat, int count, int offset, int rev = 0)
		{
			var parameters = new Dictionary<string, object>
			{
				{"chat_id=", IdChat},
				{"count=", count},
				{"rev=", rev},
				{"offset=", offset}
			};

			//получаем данные в json
			string str = VkRequest.GetData("messages.getHistory", parameters);
			//десериализуем
			MessagesModel messagesModel = JsonConvert.DeserializeObject<MessagesModel>(str);

			return messagesModel;
		}

		public Task<MessagesModel> GetHistoryChatAsync(int IdChat, int count, int offset, int rev = 0)
		{
			return Task.Run(() =>
			{
				MessagesModel messageModel = GetHistoryChat(IdChat, count, offset, rev);
				return messageModel;
			});
		}

		public MessagesModel GetHistoryUser(int IdUser, int count, int offset, int rev = 0)
		{
			var parameters = new Dictionary<string, object>
			{
				{"user_id=", IdUser},
				{"count=", count},
				{"rev=", rev},
				{"offset=", offset}
			};
			//получаем данные в json
			string str = VkRequest.GetData("messages.getHistory", parameters);
			//десериализуем
			MessagesModel messagesModel = JsonConvert.DeserializeObject<MessagesModel>(str);

			return messagesModel;
		}

		public Task<MessagesModel> GetHistoryUserAsync(int IdUser, int count, int offset, int rev = 0)
		{
			return Task.Run(() =>
			{
				MessagesModel messageModel = GetHistoryUser(IdUser, count, offset, rev);
				return messageModel;
			});
		}

		public LongPullModel GetLongPollServer(bool useSsl = false, bool needPts = true)
		{
			var parameters = new Dictionary<string, object>
			{
				{"use_ssl=", useSsl},
				{"need_pts=", needPts}
			};

			string str = VkRequest.GetData("messages.getLongPollServer", parameters);
			LongPullModel longPullModel = JsonConvert.DeserializeObject<LongPullModel>(str);
			return longPullModel;
		}

		public Task<LongPullModel> GetLongPollServerAsync(bool useSsl = false, bool needPts = false)
		{
			return Task.Run(() =>
			{
				LongPullModel messageModel = GetLongPollServer(useSsl, needPts);
				return messageModel;
			});
		}

		public LongPullMessageModel GetLongPollHistory(int ts, int pts, int max_msg_id = 0, int preview_length = 0, int onlines = 0)
		{
			var parameters = new Dictionary<string, object>
			{
				{"ts=", ts},
				{"pts=", pts},
				{"max_msg_id=", max_msg_id},
				{"preview_length=", preview_length},
				{"onlines=", onlines}
			};

			string str = VkRequest.GetData("messages.getLongPollHistory", parameters);
			//заменить на нужную модель данных
			LongPullMessageModel longPullModel = JsonConvert.DeserializeObject<LongPullMessageModel>(str);
			return longPullModel;
		}

		public Task<LongPullMessageModel> GetLongPollHistoryAsynk(int ts, int pts, int max_msg_id=0, int preview_length=0, int onlines=0)
		{
			return Task.Run(() =>
			{
				LongPullMessageModel messageModel = GetLongPollHistory(ts, pts, max_msg_id, preview_length, onlines);
				return messageModel;
			});
		}

		//public LongPullModel GetById(int message_id)
		//{
		//	var parameters = new Dictionary<string, object>
		//	{
		//		{"message_id=", message_id}
		//	};

		//	string str = VkRequest.GetData("messages.getById", parameters);

		//	LongPullModel longPullModel = JsonConvert.DeserializeObject<LongPullModel>(str);
		//	return longPullModel;
		//}

	}
}