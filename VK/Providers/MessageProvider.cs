using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Core;
using VK.ViewModel.Messages;
using VKAPI;
using VKAPI.Model.MessagesModel;
using VKAPI.Model.UsersModel;

namespace VK.Providers
{
	public class MessageProvider : IItemsProvider<MessageItemViewModel>
	{
		private readonly int _count;
		private bool _isChat;
		private int _id;
		private readonly VkApi _vkApi;

		public MessageProvider(int count, int id, bool isChat, VkApi vkApi)
		{
			_count = count;
			_id = id;
			_isChat = isChat;
			_vkApi = vkApi;
		}

		public int FetchCount()
		{
			return _count;
		}

		private UsersModel users;
		public Task<List<MessageItemViewModel>> FetchRange(int startIndex, int count)
		{
			return Task.Run( () =>
			{
				
				MessagesModel _messageModel;
				if (_isChat)
				{
					_messageModel =  _vkApi.Messages.GetHistoryChat(_id, count, startIndex, 1);
				}
				else
				{
					_messageModel =  _vkApi.Messages.GetHistoryUser(_id, count, startIndex, 1);
				}

				//_messageModel.response.items.Reverse();
				List<MessageItemViewModel> itemsMessages = new List<MessageItemViewModel>();
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
						if (item.attachments[0].type.ToLower() == "photo")
						{
							itemMessage.Photo = item.attachments[0].photo.photo_604;
							itemMessage.PhotoHeight = Convert.ToInt32(item.attachments[0].photo.height * 0.7);
							itemMessage.PhotoWidth = Convert.ToInt32(item.attachments[0].photo.width * 0.7);
						}
						if (item.attachments[0].type == "video")
						{
							itemMessage.Photo = item.attachments[0].video.photo_130;
;
						}
					}

					itemsMessages.Add(itemMessage);
				}

				if (users == null)
				{
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
					users = _vkApi.Users.GetPhoto(UserIds);
				}
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
				if (_isChat)
					users = null;
				return itemsMessages;

			});
		}
	}
}
