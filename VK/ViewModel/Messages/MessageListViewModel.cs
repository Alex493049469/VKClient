using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VKAPI;
using VKAPI.Model.MessagesModel;
using VKAPI.Model.UsersModel;

namespace VK.ViewModel.Messages
{
	class MessageListViewModel : PaneViewModel
	{
		//для доступа к данным диалогов
		VkApi _vk = new VkApi();

		//индекс начала
		private int _index;
		//размер страницы
		private int _count = 25;

		//Properties
		private ObservableCollection<MessageItemViewModel> _messageItemsViewModel = new ObservableCollection<MessageItemViewModel>();
		public ObservableCollection<MessageItemViewModel> MessageItemsViewModel
		{
			get { return _messageItemsViewModel; }
			set
			{
				if (_messageItemsViewModel == value)
					return;

				_messageItemsViewModel = value;

				RaisePropertyChanged();
			}
		}

		private bool _isChat;
		private int _id;

		public MessageListViewModel(bool isChat, int id)
		{
			Title = "Пепеписка с ";
			_isChat = isChat;
			_id = id;
			LoadMessage();
		}

		public async void LoadMessage()
		{
			MessagesModel _messageModel;
			if (_isChat)
			{
				_messageModel = await _vk.Messages.GetHistoryChatAsync(_id, _count, _index);
			}
			else
			{
				_messageModel = await _vk.Messages.GetHistoryUserAsync(_id, _count, _index);
			}

			_messageModel.response.items.Reverse();
			ObservableCollection<MessageItemViewModel> itemsMessages = new ObservableCollection<MessageItemViewModel>();
			foreach (var item in _messageModel.response.items)
			{
				MessageItemViewModel itemMessage = new MessageItemViewModel()
				{
					UserId = item.user_id,
					Id = item.id,
					Body = item.body,
					ChatId = item.chat_id,
					Date = item.date,
					FromId = item.from_id,
					Out = item.@out,
					Attachments = item.attachments,
					Emoji = item.emoji,
					FwdMessages = item.fwd_messages,
					ReadState = item.read_state,
				};

				if (item.attachments != null)
				{
					if (item.attachments[0].type == "gift")
					{
						itemMessage.GiftThumb_256 = item.attachments[0].gift.thumb_256;
					}
					if (item.attachments[0].type == "sticker")
					{
						itemMessage.StickerPhoto_128 = item.attachments[0].sticker.photo_128;
					}
					if (item.attachments[0].type == "Photo")
					{
						itemMessage.Photo = item.attachments[0].photo.photo_604;
						itemMessage.PhotoHeight = Convert.ToInt32(item.attachments[0].photo.height*0.7);
						itemMessage.PhotoWidth = Convert.ToInt32(item.attachments[0].photo.width*0.7);
					}
				}

				itemsMessages.Add(itemMessage);
			}
			

			//если несколько пользователей то берем их из ChatActive если 1 то userId
			//собираем все их id 
			List<int> userIdList = new List<int>();

			foreach (var item in itemsMessages)
			{
				if (item.UserId == item.FromId)
				{
					userIdList.Add(item.UserId);
				}
				else
				{
					userIdList.Add(item.FromId);
				}
			}

			string UserIds = "";
			foreach (var userId in userIdList)
			{
				if (UserIds == "")
				{
					UserIds += userId;
				}
				else
				{
					UserIds += ","+ userId;
				}
			}
			//получаем всю необходимую информацию о пользовалелях кто в диалогах 
			UsersModel users = await _vk.Users.GetPhotoAsync(UserIds);

			foreach (var item in itemsMessages)
			{
				if (item.UserId == item.FromId)
				{
					var userTemp = users.response.Find(i => i.id == item.UserId);
					item.UserIdPhoto = userTemp.photo_100;
					item.UserName = userTemp.first_name + " " + userTemp.last_name;
					item.LastMessageUserName = userTemp.first_name + " отправил подарок ";
				}
				else
				{
					var userTemp = users.response.Find(i => i.id == item.FromId);
					item.UserIdPhoto = userTemp.photo_100;
					item.UserName = userTemp.first_name + " " + userTemp.last_name;
					item.LastMessageUserName = userTemp.first_name + " отправил подарок ";
				}

				
			}

			itemsMessages.ToList().ForEach(MessageItemsViewModel.Add);
			_index += _count;
		}


	}
}
