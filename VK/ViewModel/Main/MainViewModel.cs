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
		private ObservableCollection<BaseViewModel> _viewModels = new ObservableCollection<BaseViewModel>();
		ReadOnlyObservableCollection<BaseViewModel> _readonyViewModels = null;
		public ReadOnlyObservableCollection<BaseViewModel> ViewModels
		{
			get { return _readonyViewModels ?? (_readonyViewModels = new ReadOnlyObservableCollection<BaseViewModel>(_viewModels)); }
		}

		private BaseViewModel _activeViewModel = null;

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
		/// Активная viewModel
		public BaseViewModel ActiveViewModel
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

	}
}
