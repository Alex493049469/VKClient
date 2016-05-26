using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.ExtendedCollection;
using VK.ViewModel.Dialogs;
using VKAPI;
using VKAPI.Model.DialogsModel;
using VKAPI.Model.UsersModel;

namespace VK.DataAccess
{
	public class DialogProvider : IItemsProvider<DialogItemViewModel>
	{
		public class Customer
		{
			/// <summary>
			/// Gets or sets the id.
			/// </summary>
			/// <value>The id.</value>
			public int Id { get; set; }

			/// <summary>
			/// Gets or sets the name.
			/// </summary>
			/// <value>The name.</value>
			public string Name { get; set; }

			/// <summary>
			/// Some dummy data to give the instance a bigger memory footprint.
			/// </summary>
			private byte[] data = new byte[100];
		}

		private readonly int _count = 1000;
		private readonly int _fetchDelay;

		public int FetchCount()
		{
			return _count;
		}
		VkApi _vkApi = new VkApi();

		public IList<DialogItemViewModel> FetchRange(int startIndex, int count)
		{
			Trace.WriteLine("FetchRange: " + startIndex + "," + count);
			Thread.Sleep(_fetchDelay);

			List<DialogItemViewModel> list = new List<DialogItemViewModel>();
			for (int i = startIndex; i < startIndex + count; i++)
			{
				DialogItemViewModel customer = new DialogItemViewModel { Body = "Test", Title = i.ToString() };
				list.Add(customer);
			}
			return list;

			//DialogsModel _dialogModel = _vkApi.Messages.GetDialogs(count, startIndex);

			//List<DialogItemViewModel> itemsDialog = new List<DialogItemViewModel>();
			//foreach (var item in _dialogModel.response.items)
			//{
			//	DialogItemViewModel itemDialog = new DialogItemViewModel()
			//	{
			//		Title = item.message.title,
			//		UserId = item.message.user_id,
			//		Body = item.message.body,
			//		GroupPhoto100 = item.message.photo_100,
			//		GroupPhoto200 = item.message.photo_200,
			//		GroupPhoto50 = item.message.photo_50,
			//		UserCount = item.message.users_count,
			//		ChatActive = item.message.chat_active,
			//		ReadState = item.message.read_state,
			//		Date = item.message.date,
			//		Out = item.message.@out,
			//		ChatId = item.message.chat_id,
			//		Attachment = item.message.attachments,
			//		Unread = item.unread
			//	};

			//	itemsDialog.Add(itemDialog);
			//}
			////CountDialog = _dialogModel.response.count;

			////если несколько пользователей то берем их из ChatActive если 1 то userId
			////собираем все их id 
			//HashSet<int> userIdList = new HashSet<int>();
			//foreach (var item in itemsDialog)
			//{
			//	if (item.UserCount == 1 || item.UserCount == null)
			//	{
			//		if (!userIdList.Contains(item.UserId))
			//		{
			//			userIdList.Add(item.UserId);
			//		}
			//	}
			//	if (item.UserCount > 1)
			//	{
			//		foreach (var id in item.ChatActive)
			//		{
			//			if (!userIdList.Contains(id))
			//			{
			//				userIdList.Add(id);
			//			}
			//		}
			//	}
			//}

			//string userIds = String.Join(",", userIdList);

			////получаем всю необходимую информацию о пользовалелях кто в диалогах 
			//UsersModel users =  _vkApi.Users.GetPhoto(userIds);
			//UsersModel thisUser = _vkApi.Users.Get();
			////здесь в зависимости от количества собеседников подгружаем фотки
			//foreach (var item in itemsDialog)
			//{
			//	//проверка что в сообщении есть вложения
			//	//определяем тип вложения и пишем его в сообщение
			//	if (item.Attachment != null) // если ли вложения?
			//	{
			//		switch (item.Attachment[0].type)
			//		{
			//			case "photo":
			//				item.Body = "Фотография";
			//				break;
			//			case "audio":
			//				item.Body = "Аудиозапись";
			//				break;
			//			case "gift":
			//				item.Body = "Подарок";
			//				break;
			//			case "wall":
			//				item.Body = "Запись на стены";
			//				break;
			//			case "video":
			//				item.Body = "Видеозапись";
			//				break;
			//			case "sticker":
			//				item.Body = "Стикер";
			//				break;
			//			case "doc":
			//				item.Body = "Документ";
			//				break;
			//			case "link":
			//				item.Body = "Ссылка";
			//				break;
			//			default:
			//				item.Body = item.Attachment[0].type;
			//				break;
			//		}
			//	}

			//	if (item.ChatActive == null || item.UserCount == null)
			//	{
			//		var userTemp = users.response.Find(i => i.id == item.UserId);
			//		item.UserOnePhoto = userTemp.photo_100;
			//		item.Title = userTemp.first_name + " " + userTemp.last_name;
			//		if (item.Out == 1)
			//		{
			//			item.UserIdPhoto = thisUser.response[0].photo_100;
			//		}

			//		continue;
			//	}

			//	var user = users.response.Find(i => i.id == item.UserId);
			//	if (user != null)
			//	{
			//		item.UserIdPhoto = user.photo_100;
			//		item.LastMessageUserName = user.first_name + " " + user.last_name;
			//	}

			//	if (item.ChatActive.Count == 2)
			//	{
			//		item.UserOnePhoto = users.response.Find(i => i.id == item.ChatActive[0]).photo_100;
			//		item.UserTwoPhoto = users.response.Find(i => i.id == item.ChatActive[1]).photo_100;
			//		continue;
			//	}
			//	if (item.ChatActive.Count == 3)
			//	{
			//		item.UserOnePhoto = users.response.Find(i => i.id == item.ChatActive[0]).photo_100;
			//		item.UserTwoPhoto = users.response.Find(i => i.id == item.ChatActive[1]).photo_100;
			//		item.UserThreePhoto = users.response.Find(i => i.id == item.ChatActive[2]).photo_100;
			//		continue;
			//	}

			//	if (item.ChatActive.Count >= 4)
			//	{
			//		item.UserOnePhoto = users.response.Find(i => i.id == item.ChatActive[0]).photo_100;
			//		item.UserTwoPhoto = users.response.Find(i => i.id == item.ChatActive[1]).photo_100;
			//		item.UserThreePhoto = users.response.Find(i => i.id == item.ChatActive[2]).photo_100;
			//		item.UserFourPhoto = users.response.Find(i => i.id == item.ChatActive[3]).photo_100;

			//	}
			//}

			//if (_dialogsViewModel == null)
			//{
			//	_dialogsViewModel = itemsDialog;
			//}
			//else
			//{
			//	itemsDialog.ToList().ForEach(_dialogsViewModel.Add);
			//}
			//_index += _count;
			//return itemsDialog;
		}
	}
}
