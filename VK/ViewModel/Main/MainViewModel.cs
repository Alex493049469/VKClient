using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Core;
using Core.Command;
using VK.Properties;
using VK.View;
using VK.ViewModel.Audios;
using VK.ViewModel.Dialogs;
using VK.ViewModel.Friends;
using VK.ViewModel.Page;
using VKAPI;

namespace VK.ViewModel.Main
{
	class MainViewModel : BaseViewModel
	{
		//View models
		private BaseViewModel _audiosViewModel;
		private BaseViewModel _friendsViewModel;
		private BaseViewModel _pageViewModel;
		private BaseViewModel _dialogsViewModel;

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

		//Binding Property
		public object ContentPanel { get; set; }
		
		public MainViewModel()
		{
			OpenDialogsCommand = new RelayCommand(OpenDialogs);
			OpenAudioCommand = new RelayCommand(OpenAudios);
			OpenPageCommand = new RelayCommand(OpenMyPage);
			OpenFriendsCommand = new RelayCommand(OpenFriends);
		}

		private void OpenAudios()
		{
			if (_audiosView == null) _audiosView = new AudioView();
			if (_audiosViewModel == null)
			{
				_audiosViewModel = new AudioListViewModel();
				_audiosView.DataContext = _audiosViewModel;
			}
			ContentPanel = _audiosView;
		}

		private void OpenMyPage()
		{
			if (_pageView == null) _pageView = new PageView();
			if (_pageViewModel == null)
			{
				_pageViewModel = new PageViewModel();
				_pageView.DataContext = _pageViewModel;
			}
			ContentPanel = _pageView;
		}

		private void OpenDialogs()
		{
			if (_dialogsView == null) _dialogsView = new DialogsView();
			if (_dialogsViewModel == null)
			{
				_dialogsViewModel = new DialogListViewModel();
				_dialogsView.DataContext = _dialogsViewModel;
			}
			ContentPanel = _dialogsView;
		}

		private void OpenFriends()
		{
			if (_friendsView == null) _friendsView = new FriendsView();
			if (_friendsViewModel == null)
			{
				_friendsViewModel = new FriendsListViewModel();
				_friendsView.DataContext = _friendsViewModel;
			}
			ContentPanel = _friendsView;
		}

		private void OpenSettings()
		{
			//для доступа к данным диалогов
			VkApi _vk = new VkApi();
			var qwe =_vk.Messages.GetHistory(130, 20);
		}
	}
}
