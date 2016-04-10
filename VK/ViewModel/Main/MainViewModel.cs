using System.Collections.ObjectModel;
using System.Linq;
using Core;
using Core.Command;
using VK.DataAccess;
using VK.Services;
using VK.ViewModel.Audios;
using VK.ViewModel.Dialogs;
using VK.ViewModel.Friends;
using VK.ViewModel.Page;

namespace VK.ViewModel.Main
{
	public class MainViewModel : BaseViewModel
	{
		//Коллекция ViewModel
		private readonly ObservableCollection<PaneViewModel> _viewModels = new ObservableCollection<PaneViewModel>();
		public ObservableCollection<PaneViewModel> ViewModels => _viewModels;

		//Количество непрочитанных сообщений
		private int _unreadMessages;
		public int UnreadMessages
		{
			get { return _unreadMessages; }
			set
			{
				_unreadMessages = value;
				RaisePropertyChanged();
			}
		}
		// Активная viewModel
		private PaneViewModel _activeViewModel;
		public PaneViewModel ActiveViewModel
		{
			get { return _activeViewModel; }
			set
			{
				if (_activeViewModel != value)
				{
					_activeViewModel = value;
					RaisePropertyChanged();
				}
			}
		}

		public static MainViewModel This { get; } = new MainViewModel();

		//repositories
		DialogRepository _dialogRepository;
		EventsService _eventService;

		//Commands
		public RelayCommand OpenDialogsCommand { get; private set; }
		public RelayCommand OpenAudioCommand { get; private set; }
		public RelayCommand OpenPageCommand { get; private set; }
		public RelayCommand OpenFriendsCommand { get; private set; }

		private AudioListViewModel _audioList;
		private DialogListViewModel _dialogList;

		public MainViewModel()
		{
			OpenDialogsCommand = new RelayCommand(OpenDialogs);
			OpenAudioCommand = new RelayCommand(OpenAudios);
			OpenPageCommand = new RelayCommand(OpenMyPage);
			OpenFriendsCommand = new RelayCommand(OpenFriends);
		}

		private void OpenAudios()
		{
			if (_audioList == null)
				_audioList = new AudioListViewModel();

			if (_viewModels.Contains(_audioList))
			{
				ActiveViewModel = _audioList;
			}
			else
			{
				_viewModels.Add(_audioList);
				ActiveViewModel = _viewModels.Last();
			}
		}

		private void OpenMyPage()
		{
			_viewModels.Add(new PageViewModel());
			ActiveViewModel = _viewModels.Last();
		}

		private void OpenDialogs()
		{
			if (_dialogList == null)
			{
				_eventService = new EventsService();
				//_eventService.LongPool();
				_dialogRepository = new DialogRepository(_eventService);

				_dialogList = new DialogListViewModel(_dialogRepository);
			}
			if (_viewModels.Contains(_dialogList))
			{
				ActiveViewModel = _dialogList;
			}
			else
			{
				_viewModels.Add(_dialogList);
				ActiveViewModel = _viewModels.Last();
			}
		}

		private void OpenFriends()
		{
			_viewModels.Add(new FriendsListViewModel());
			ActiveViewModel = _viewModels.Last();
		}

		internal void Close(PaneViewModel fileToClose)
		{
			_viewModels.Remove(fileToClose);
		}

	}
}
