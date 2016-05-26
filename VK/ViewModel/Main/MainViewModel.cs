using System.Collections.ObjectModel;
using System.Linq;
using Core;
using Core.Command;
using VK.DataAccess;
using VK.Services;
using VK.ViewModel.Audios;
using VK.ViewModel.AuthorizedUser;
using VK.ViewModel.Dialogs;
using VK.ViewModel.Friends;
using VK.ViewModel.Page;
using VKAPI;
using VKAPI.Handlers;

namespace VK.ViewModel.Main
{
	public class MainViewModel : BaseViewModel
	{
		//для доступа к данным диалогов
		private readonly VkApi _vkApi = new VkApi(new VkRequest());

		//Коллекция ViewModel
		private readonly ObservableCollection<PaneViewModel> _viewModels = new ObservableCollection<PaneViewModel>();
		public ObservableCollection<PaneViewModel> ViewModels => _viewModels;

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

		private AuthorizedUserViewModel _authorizedUserViewModel;
		public AuthorizedUserViewModel AuthorizedUserViewModel
		{
			get { return _authorizedUserViewModel; }
			set
			{
				if (_authorizedUserViewModel != value)
				{
					_authorizedUserViewModel = value;
					RaisePropertyChanged();
				}
			}
		}

		//выезжающая панелька
		//прокидывается во вью модели для вызова своих окон
		//в идеале должна вызываться через сервис
		private FlyoutViewModel _flyout = new FlyoutViewModel();
		public FlyoutViewModel Flyout
		{
			get { return _flyout; }
			set
			{
				_flyout = value;
				RaisePropertyChanged();
			}
		}

		public static MainViewModel This { get; } = new MainViewModel();

		//repositories
		DialogRepository _dialogRepository;
		//service
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

			AuthorizedUserViewModel = new AuthorizedUserViewModel(_vkApi);
		}

		private void OpenAudios()
		{
			if (_audioList == null)
				_audioList = new AudioListViewModel(_vkApi, Flyout);

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
			_viewModels.Add(new PageViewModel(_vkApi));
			ActiveViewModel = _viewModels.Last();
		}

		private void OpenDialogs()
		{
			if (_dialogList == null)
			{
				_eventService = new EventsService(_vkApi);
				_eventService.LongPool();
				_dialogRepository = new DialogRepository(_vkApi, _eventService);

				_dialogList = new DialogListViewModel(_vkApi,_dialogRepository);
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
			_viewModels.Add(new FriendsListViewModel(_vkApi));
			ActiveViewModel = _viewModels.Last();
		}

		internal void Close(PaneViewModel fileToClose)
		{
			_viewModels.Remove(fileToClose);
		}

	}
}
