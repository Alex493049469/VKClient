using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Core.Command;
using VKAPI;
using VKAPI.Model.MessagesModel;
using VKAPI.Model.UsersModel;

namespace VK.ViewModel.Messages
{
	class MessageListViewModel : PaneViewModel
	{
		//для доступа к данным диалогов
		private readonly VkApi _vkApi;

		//индекс начала
		private int _index;
		//размер страницы
		private int _count = 25;

		public RelayCommand LoadCommand { get; set; }

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

		private bool _isLazyLoad = false;
		public bool IsLazyLoad
		{
			get { return _isLazyLoad; }
			set
			{
				_isLazyLoad = value;
				RaisePropertyChanged();
			}
		}

		private bool _isChat;
		private int _id;

		public MessageListViewModel(VkApi vkApi, bool isChat, int id)
		{
			_vkApi = vkApi;
			Title = "Пепеписка с ";
			_isChat = isChat;
			_id = id;
			LoadCommand = new RelayCommand(LoadMessage);
			LoadMessage();
		}

		public async void LoadMessage()
		{
			MessagesModel _messageModel;
			if (_isChat)
			{
				_messageModel = await _vkApi.Messages.GetHistoryChatAsync(_id, _count, _index);
			}
			else
			{
				_messageModel = await _vkApi.Messages.GetHistoryUserAsync(_id, _count, _index);
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
			HashSet<int> userIdList = new HashSet<int>();

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

			string UserIds = String.Join(",", userIdList);
			
			//получаем всю необходимую информацию о пользовалелях кто в диалогах 
			UsersModel users = await _vkApi.Users.GetPhotoAsync(UserIds);

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

		

			if (IsLazyLoad == false)
			{
				itemsMessages.ToList().ForEach(MessageItemsViewModel.Add);
			}
			else
			{
				IsLazyLoad = false;
				for (int i = itemsMessages.Count-1; i > -1; i--)
				{
					MessageItemsViewModel.Insert(0, itemsMessages[i]);
				}
			}
		

			IsLazyLoad = true;
			_index += _count;
		}


	}
}
