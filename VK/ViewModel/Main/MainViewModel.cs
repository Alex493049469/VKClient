using System.Collections.ObjectModel;
using System.Linq;
using Core;
using Core.Command;
using VK.ViewModel.Audios;
using VK.ViewModel.Dialogs;
using VK.ViewModel.Friends;
using VK.ViewModel.Page;

namespace VK.ViewModel.Main
{
	public class MainViewModel : BaseViewModel
	{
		//Коллекция ViewModel
		private ObservableCollection<PaneViewModel> _viewModels = new ObservableCollection<PaneViewModel>();
		public ObservableCollection<PaneViewModel> ViewModels
		{
			get { return _viewModels; }
		}

		//property
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
		private PaneViewModel _activeViewModel = null;
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

		static MainViewModel _this = new MainViewModel();

		public static MainViewModel This
		{
			get { return _this; }
		}

		//Commands
		public RelayCommand OpenDialogsCommand { get; private set; }
		public RelayCommand OpenAudioCommand { get; private set; }
		public RelayCommand OpenPageCommand { get; private set; }
		public RelayCommand OpenFriendsCommand { get; private set; }


		public MainViewModel()
		{
			OpenDialogsCommand = new RelayCommand(OpenDialogs);
			OpenAudioCommand = new RelayCommand(OpenAudios);
			OpenPageCommand = new RelayCommand(OpenMyPage);
			OpenFriendsCommand = new RelayCommand(OpenFriends);
		}

		private void OpenAudios()
		{
			_viewModels.Add(new AudioListViewModel());
			ActiveViewModel = _viewModels.Last();
		}

		private void OpenMyPage()
		{
			_viewModels.Add(new PageViewModel());
			ActiveViewModel = _viewModels.Last();
		}

		private void OpenDialogs()
		{
			_viewModels.Add(new DialogListViewModel());
			ActiveViewModel = _viewModels.Last();
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

		private PaneViewModel FindViewModel(string title)
		{
			if (_viewModels == null) return null;

			return _viewModels.FirstOrDefault(viewModel => viewModel.Title == title);
		}

	}
}
