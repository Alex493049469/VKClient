using System.Collections.ObjectModel;
using System.Linq;
using Core.Command;
using VK.DataAccess;
using VK.ViewModel.Main;
using VK.ViewModel.Messages;
using VKAPI;

namespace VK.ViewModel.Dialogs
{
	public class DialogListViewModel : PaneViewModel
	{
		//Commands
		public RelayCommand OpenMessagesCommand { get; private set; }
		public RelayCommand LoadCommand { get; set; }

		private readonly VkApi _vkApi;

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

		private readonly DialogRepository _dialogRepository;

		public DialogListViewModel(VkApi vkApi, DialogRepository dialogRepository)
		{
			_vkApi = vkApi;
			_dialogRepository = dialogRepository;
			Title = "Мои сообщения";
			CountDialog = 0;
			OpenMessagesCommand = new RelayCommand(OpenMessages);
			LoadCommand = new RelayCommand(LoadDialogs);
			LoadDialogs();
		}

		public void LoadDialogs()
		{
			if (DialogItemsViewModel != null && DialogItemsViewModel.Count == CountDialog) return;
			_dialogRepository.GetDialog();
			DialogItemsViewModel = _dialogRepository.DialogItemsViewModel;

			CountDialog = _dialogRepository.CountDialog;
		}

		public void OpenMessages()
		{
			MessageListViewModel messageViewModel;
			if (ItemSelected.ChatId == null)
			{
				messageViewModel = new MessageListViewModel(_vkApi, false, (int)ItemSelected.UserId);
			}
			else
			{
				messageViewModel = new MessageListViewModel(_vkApi, true, (int)ItemSelected.ChatId);
			}
			messageViewModel.Title = ItemSelected.Title;

			MainViewModel.This.ViewModels.Add(messageViewModel);
			MainViewModel.This.ActiveViewModel = MainViewModel.This.ViewModels.Last();
		}


	}
}
