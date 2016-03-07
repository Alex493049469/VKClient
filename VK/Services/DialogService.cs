﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Core;
using VK.ViewModel;
using VK.ViewModel.Dialogs;
using VKAPI;
using VKAPI.Model.DialogsModel;
using VKAPI.Model.UsersModel;

namespace VK.Services
{
	/// <summary>
	/// отвечает за получение диалогов и их обновление, оповещает об изменении диалогов VM
	/// </summary>
	public class DialogService
	{
		private EventsService _eventService;

		public  DialogService(EventsService eventService)
		{
			_eventService = eventService;
			_eventService.NewMessage += InstanceOnNewMessage;
		}

		//индекс начала
		private int _index;
		//размер страницы
		private int _count = 25;
		//модель данных диалогов
		private DialogsModel _dialogModel;
		//view model диалогов
		private ObservableCollection<DialogItemViewModel> _dialogsViewModel = new ObservableCollection<DialogItemViewModel>();

		public ObservableCollection<DialogItemViewModel> DialogItemsViewModel
		{
			get { return _dialogsViewModel; }
			set
			{
				if (_dialogsViewModel == value)
					return;

				_dialogsViewModel = value;

			}
		}

		public int CountDialog { get; set; }

		public int UnreadMessages
		{
			get { return _unreadMessages; }
			set
			{
				_unreadMessages = value;
				//ViewModelLocator.MainMenu.UnreadMessages = value;
			}
		}

		private int _unreadMessages;


		//для доступа к данным диалогов
		VkApi _vkApi = new VkApi();

		//событие на которое надо подписаться для оповещения что диалоги изменились и их нужно обновить в UI
		public delegate void MethodContainer();
		public event MethodContainer ChangeDialog;

		/// <summary>
		/// возвращает список диалогов
		/// </summary>
		/// <returns></returns>
		public async void GetDialog()
		{
			_dialogModel = await _vkApi.Messages.GetDialogsAsync(_count, _index);

			ObservableCollection<DialogItemViewModel> itemsDialog = new ObservableCollection<DialogItemViewModel>();
			foreach (var item in _dialogModel.response.items)
			{
				DialogItemViewModel itemDialog = new DialogItemViewModel()
				{
					Title = item.message.title,
					UserId = item.message.user_id,
					Body = item.message.body,
					GroupPhoto100 = item.message.photo_100,
					GroupPhoto200 = item.message.photo_200,
					GroupPhoto50 = item.message.photo_50,
					UserCount = item.message.users_count,
					ChatActive = item.message.chat_active,
					ReadState = item.message.read_state,
					Date = item.message.date,
					Out = item.message.@out,
					ChatId = item.message.chat_id,
					Attachment = item.message.attachments,
					Unread = item.unread
				};

				itemsDialog.Add(itemDialog);
			}
			CountDialog = _dialogModel.response.count;
			//UnreadMessages = _dialogModel.response.unread_dialogs;

			var qwe = from item in _dialogModel.response.items
				where item.unread > 0
				let ert =+ item.unread
				select ert;


			//UnreadDialogs = _dialogModel.response.items
			//если несколько пользователей то берем их из ChatActive если 1 то userId
			//собираем все их id 
			List<int> userIdList = new List<int>();
			foreach (var item in itemsDialog)
			{
				if (item.UserCount == 1 || item.UserCount == null)
				{
					if (!userIdList.Contains(item.UserId))
					{
						userIdList.Add(item.UserId);
					}
				}
				if (item.UserCount > 1)
				{
					foreach (var id in item.ChatActive)
					{
						if (!userIdList.Contains(id))
						{
							userIdList.Add(id);
						}
					}
				}
			}

			string UserIds = "";
			foreach (var id in userIdList)
			{
				if (UserIds == "")
				{
					UserIds += id;
				}
				else
				{
					UserIds += "," + id;
				}
			}

			//получаем всю необходимую информацию о пользовалелях кто в диалогах 
			UsersModel users = await _vkApi.Users.GetPhotoAsync(UserIds);
			UsersModel thisUser = await _vkApi.Users.GetAsync();
			//здесь в зависимости от количества собеседников подгружаем фотки
			foreach (var item in itemsDialog)
			{
				//проверка что в сообщении есть вложения
				//определяем тип вложения и пишем его в сообщение
				if (item.Attachment != null) // если ли вложения?
				{
					switch (item.Attachment[0].type)
					{
						case "photo":
							item.Body = "Фотография";
							break;
						case "audio":
							item.Body = "Аудиозапись";
							break;
						case "gift":
							item.Body = "Подарок";
							break;
						case "wall":
							item.Body = "Запись на стены";
							break;
						case "video":
							item.Body = "Видеозапись";
							break;
						case "sticker":
							item.Body = "Стикер";
							break;
						case "doc":
							item.Body = "Документ";
							break;
						case "link":
							item.Body = "Ссылка";
							break;
						default:
							item.Body = item.Attachment[0].type;
							break;
					}
				}

				if (item.ChatActive == null || item.UserCount == null)
				{
					var userTemp = users.response.Find(i => i.id == item.UserId);
					item.UserOnePhoto = userTemp.photo_100;
					item.Title = userTemp.first_name + " " + userTemp.last_name;
					if (item.Out == 1)
					{
						item.UserIdPhoto = thisUser.response[0].photo_100;
					}

					continue;
				}

				var user = users.response.Find(i => i.id == item.UserId);
				if (user != null)
				{
					item.UserIdPhoto = user.photo_100;
					item.LastMessageUserName = user.first_name + " " + user.last_name;
				}

				if (item.ChatActive.Count == 2)
				{
					item.UserOnePhoto = users.response.Find(i => i.id == item.ChatActive[0]).photo_100;
					item.UserTwoPhoto = users.response.Find(i => i.id == item.ChatActive[1]).photo_100;
					continue;
				}
				if (item.ChatActive.Count == 3)
				{
					item.UserOnePhoto = users.response.Find(i => i.id == item.ChatActive[0]).photo_100;
					item.UserTwoPhoto = users.response.Find(i => i.id == item.ChatActive[1]).photo_100;
					item.UserThreePhoto = users.response.Find(i => i.id == item.ChatActive[2]).photo_100;
					continue;
				}

				if (item.ChatActive.Count >= 4)
				{
					item.UserOnePhoto = users.response.Find(i => i.id == item.ChatActive[0]).photo_100;
					item.UserTwoPhoto = users.response.Find(i => i.id == item.ChatActive[1]).photo_100;
					item.UserThreePhoto = users.response.Find(i => i.id == item.ChatActive[2]).photo_100;
					item.UserFourPhoto = users.response.Find(i => i.id == item.ChatActive[3]).photo_100;

				}
			}

			if (_dialogsViewModel == null)
			{
				_dialogsViewModel = itemsDialog;
			}
			else
			{
				itemsDialog.ToList().ForEach(_dialogsViewModel.Add);
			}
			_index += _count;

		}

		private void InstanceOnNewMessage(EventsService.MessageNew message)
		{

			//for (int i = 0; i < DialogItemsViewModel.Count; i++)
			//{
			//	var item = DialogItemsViewModel[i];
			//	if (item.UserId == message.FromId || item.ChatId == message.FromId)
			//	{
			//		DialogItemsViewModel.RemoveAt(i);
			//		item.Body = message.Text;
			//		item.Date = message.Timestamp;
			//		DialogItemsViewModel.Insert(0, item);
			//	}
			//}
		}


		//класс так же получает ссылку на сервис messageService который будет оповещать о приходе нового сообщения
		//В ответ на это диалоги тоже должны будут отбновлены(Диалог в который пришло сообщение)

	}
}
