using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Core;
using Core.Command;
using VK.Providers;
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
		private PageCollection<MessageItemViewModel> _messageItemsViewModel;
		public PageCollection<MessageItemViewModel> MessageItemsViewModel
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
			//LoadCommand = new RelayCommand(LoadMessage);
			LoadMessage();
		}

		public async void LoadMessage()
		{
			MessagesModel _tempMessageModel;
			if (_isChat)
			{
				_tempMessageModel = await _vkApi.Messages.GetHistoryChatAsync(_id, 1, 0);
			}
			else
			{
				_tempMessageModel = await _vkApi.Messages.GetHistoryUserAsync(_id, 1, 0);
			}

			MessageProvider provider = new MessageProvider(_tempMessageModel.response.count, _id, _isChat, _vkApi);
			MessageItemsViewModel = new PageCollection<MessageItemViewModel>(provider, 50);

		}


	}
}
