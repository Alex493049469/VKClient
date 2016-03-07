using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Core;
using Core.Command;
using VK.Services;
using VK.View;
using VK.ViewModel.Messages;
using VKAPI;
using VKAPI.Model.DialogsModel;
using VKAPI.Model.UsersModel;

namespace VK.ViewModel.Dialogs
{
	public class DialogListViewModel : BaseViewModel
	{
		//Commands
		public RelayCommand OpenMessagesCommand { get; private set; }
		public RelayCommand LoadCommand { get; set; }
		
		//Общее количество диалогов
		public int CountDialog { get; set; }

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

		public void LoadDialogs()
		{
			if (DialogItemsViewModel != null && DialogItemsViewModel.Count == CountDialog) return;
			DialogItemsViewModel = ManagerService.Instance.DialogService.DialogItemsViewModel;
			
			
			ManagerService.Instance.DialogService.GetDialog();


			//ManagerService.Instance.EventService.LongPool();
			CountDialog = ManagerService.Instance.DialogService.CountDialog;
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
			ViewModelLocator.Main.ContentPanel = messagesView;

		}


	}
}
