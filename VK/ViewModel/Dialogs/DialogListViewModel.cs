using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using Core;
using Core.Command;
using VK.View;
using VK.ViewModel.Main;
using VK.ViewModel.Messages;
using VKAPI;
using VKAPI.Category;
using VKAPI.Model.DialogsModel;
using VKAPI.Model.UsersModel;

namespace VK.ViewModel.Dialogs
{
	class DialogListViewModel : BaseViewModel
	{
		//не очень красивое решение но пока так
		//ссылка на главную viewModel
		public MainViewModel _mainView;

		//индекс начала
		private int _index;
		//размер страницы
		private int _count = 25;
		//модель данных диалогов
		public DialogsModel _dialogModel;
		//Общее количество диалогов

		//Commands
		public RelayCommand OpenMessagesCommand { get; private set; }
		public RelayCommand LoadCommand { get; set; }

		public int CountDialog { get; set; }

		//для доступа к данным диалогов
		VkApi _vk = new VkApi();

		//ViewModel Диалогов
		private ObservableCollection<DialogItemViewModel> _dialogItemsViewModel;
		public ObservableCollection<DialogItemViewModel> DialogItemsViewModel
		{
			get { return _dialogItemsViewModel; }
			set
			{
				if (_dialogItemsViewModel == value)
					return;

				_dialogItemsViewModel = value;

				RaisePropertyChanged();
			}
		}

		public DialogItemViewModel ItemSelected { get; set; }

		public DialogListViewModel()
		{
			CountDialog = 0;
			OpenMessagesCommand = new RelayCommand(OpenMessages);
			LoadCommand = new RelayCommand(LoadDialogs);
			LoadDialogs();
		}

		public async void LoadDialogs()
		{
			if (DialogItemsViewModel != null && DialogItemsViewModel.Count == CountDialog) return;
			_dialogModel = await _vk.Messages.GetDialogsAsync(_count, _index);

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
					Attachment = item.message.attachments
				};

				itemsDialog.Add(itemDialog);
			}
			CountDialog = _dialogModel.response.count;

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
			UsersModel users = await _vk.Users.GetPhotoAsync(UserIds);
			UsersModel thisUser = await _vk.Users.GetAsync();
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
							item.Body = "Запись со стены";
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

			if (DialogItemsViewModel == null)
			{
				DialogItemsViewModel = itemsDialog;
			}
			else
			{
				itemsDialog.ToList().ForEach(DialogItemsViewModel.Add);
			}
			_index += _count;
			
		}

		public void OpenMessages()
		{
			var messagesView = new MessagesView();
			MessageListViewModel messageViewModel;
			if (ItemSelected.ChatId == null)
			{
				messageViewModel = new MessageListViewModel(false, (int)ItemSelected.UserId);
			}
			else
			{
				messageViewModel = new MessageListViewModel(true, (int)ItemSelected.ChatId);
			}

			messagesView.DataContext = messageViewModel;
			_mainView.ContentPanel = messagesView;
		}


	}
}
