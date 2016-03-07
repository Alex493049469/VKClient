using System.Linq;
using System.Windows.Controls;
using Core;
using Core.Command;
using VK.Services;
using VK.View;
using VK.ViewModel.Audios;
using VK.ViewModel.Dialogs;
using VK.ViewModel.Friends;
using VK.ViewModel.Main;
using VK.ViewModel.Page;

namespace VK.ViewModel.MainMenu
{
	public class MainMenuViewModel : BaseViewModel
	{
		//вернуть всю эту логику в MainViewModel
		private readonly MainViewModel _mainViewModel;

		//Commands
		public RelayCommand OpenDialogsCommand { get; private set; }
		public RelayCommand OpenAudioCommand { get; private set; }
		public RelayCommand OpenPageCommand { get; private set; }
		public RelayCommand OpenFriendsCommand { get; private set; }

		//property
		public int UnreadMessages
		{
			get { return _unreadMessages; }
			set
			{
				_unreadMessages = value;
				RaisePropertyChanged();
			}
		}

		private int _unreadMessages;

		public MainMenuViewModel(MainViewModel content)
		{
			_mainViewModel = content;

			OpenDialogsCommand = new RelayCommand(OpenDialogs);
			OpenAudioCommand = new RelayCommand(OpenAudios);
			OpenPageCommand = new RelayCommand(OpenMyPage);
			OpenFriendsCommand = new RelayCommand(OpenFriends);

		}

		private void OpenAudios()
		{

			_mainViewModel._files.Add(new AudioListViewModel());
			_mainViewModel.ActiveDocument = _mainViewModel._files.Last();
		}

		private void OpenMyPage()
		{

			_mainViewModel._files.Add(new PageViewModel());
			_mainViewModel.ActiveDocument = _mainViewModel._files.Last();
		}

		private void OpenDialogs()
		{

			_mainViewModel._files.Add(new DialogListViewModel());
			_mainViewModel.ActiveDocument = _mainViewModel._files.Last();
		}

		private void OpenFriends()
		{

			_mainViewModel._files.Add(new FriendsListViewModel());
			_mainViewModel.ActiveDocument = _mainViewModel._files.Last();
		}
	}
}
