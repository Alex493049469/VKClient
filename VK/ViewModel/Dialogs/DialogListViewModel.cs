using System.Collections.ObjectModel;
using System.Linq;
using Core.Command;
using VK.Services;
using VK.ViewModel.Main;
using VK.ViewModel.Messages;

namespace VK.ViewModel.Dialogs
{
	public class DialogListViewModel : PaneViewModel
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
			Title = "Мои сообщения";
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
			MessageListViewModel messageViewModel;
			if (ItemSelected.ChatId == null)
			{
				messageViewModel = new MessageListViewModel(false, (int)ItemSelected.UserId);
			}
			else
			{
				messageViewModel = new MessageListViewModel(true, (int)ItemSelected.ChatId);
			}

			MainViewModel.This.ViewModels.Add(messageViewModel);
			MainViewModel.This.ActiveViewModel = MainViewModel.This.ViewModels.Last();

		}


	}
}
