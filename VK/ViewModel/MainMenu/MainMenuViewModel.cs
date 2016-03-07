using System.Windows.Controls;
using Core;
using Core.Command;
using VK.Services;
using VK.View;
using VK.ViewModel.Main;

namespace VK.ViewModel.MainMenu
{
	public class MainMenuViewModel : BaseViewModel
	{

		private readonly MainViewModel _content;

		//Views
		private UserControl _audiosView;
		private UserControl _friendsView;
		private UserControl _pageView;
		private UserControl _dialogsView;

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
			_content = content;

			OpenDialogsCommand = new RelayCommand(OpenDialogs);
			OpenAudioCommand = new RelayCommand(OpenAudios);
			OpenPageCommand = new RelayCommand(OpenMyPage);
			OpenFriendsCommand = new RelayCommand(OpenFriends);

			_dialogsView = new DialogsView();
		}

		private void OpenAudios()
		{
			if (_audiosView == null) _audiosView = new AudioView();
			_content.ContentPanel = _audiosView;
		}

		private void OpenMyPage()
		{
			if (_pageView == null) _pageView = new PageView();
			_content.ContentPanel = _pageView;
		}

		private void OpenDialogs()
		{
			if (_dialogsView == null) _dialogsView = new DialogsView();
			_content.ContentPanel = _dialogsView;
		}

		private void OpenFriends()
		{
			if (_friendsView == null) _friendsView = new FriendsView();
			_content.ContentPanel = _friendsView;
		}
	}
}
