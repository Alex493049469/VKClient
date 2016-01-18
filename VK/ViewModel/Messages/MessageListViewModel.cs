using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using VK.ViewModel.Dialogs;
using VKAPI;
using VKAPI.Model.MessagesModel;
using VKAPI.Model.UsersModel;

namespace VK.ViewModel.Messages
{
	class MessageListViewModel : BaseViewModel
	{
		//для доступа к данным диалогов
		VkApi _vk = new VkApi();

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

		public MessageListViewModel(bool isChat, int id)
		{
			LoadMessage(isChat, id);
		}

		public async void LoadMessage(bool isChat, int id)
		{
			MessagesModel _messageModel;
			if (isChat)
			{
				_messageModel = await _vk.Messages.GetHistoryChatAsync(id, 20);
			}
			else
			{
				_messageModel = await _vk.Messages.GetHistoryUserAsync(id, 20);
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
					if (item.attachments[0].type == "photo")
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

		}


	}
}
