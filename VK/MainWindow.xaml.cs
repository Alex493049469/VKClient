using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using Core;
using VK.Properties;
using VK.View;
using VK.ViewModel.Audios;
using VK.ViewModel.Dialogs;
using VK.ViewModel.Friends;
using VK.ViewModel.Page;
using VKAPI;


namespace VK
{
	/// <summary>
	///Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private VkApi _vk = new VkApi();

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

		public MainWindow()
		{
			InitializeComponent();

			if (Settings.Default.token == "")
			{
				//скрываем главную форму если нет токена 
				Visibility = Visibility.Hidden;
				//отображаем окно авторизации
				var authorization = new AuthorizationWindow { main = this };
				//ссылка на главную форму
				authorization.Show();
			}
			else
			{
				VkSettings.Token = Settings.Default.token;
			}

			//Auth test = new Auth();
			//test.Authorization("89505810519", "Alex895098920646");

			//_vk.Auth.Authorization("89505810519", "Alex895098920646");
		}

		private void AudioButton_Click(object sender, RoutedEventArgs e)
		{
			if (_audiosView == null) _audiosView = new AudioView();
			if (_audiosViewModel == null)
			{
				_audiosViewModel = new AudioListViewModel();
				_audiosView.DataContext = _audiosViewModel;
			}
			Content.Content = _audiosView;
		}

		private void MyPageButton_Click(object sender, RoutedEventArgs e)
		{
			if (_pageView == null) _pageView = new PageView();
			if (_pageViewModel == null)
			{
				_pageViewModel = new PageViewModel();
				_pageView.DataContext = _pageViewModel;
			}
			Content.Content = _pageView;
		}

		private void MyMessageButton_Click(object sender, RoutedEventArgs e)
		{
			if (_dialogsView == null) _dialogsView = new DialogsView();
			if (_dialogsViewModel == null)
			{
				_dialogsViewModel = new DialogListViewModel();
				_dialogsView.DataContext = _dialogsViewModel;
			}
			//DialogViewLazy dvl = new DialogViewLazy();
			Content.Content = _dialogsView;
		}

		private void MyFriendsButton_Click(object sender, RoutedEventArgs e)
		{
			if (_friendsView == null) _friendsView = new FriendsView();
			if (_friendsViewModel == null)
			{
				_friendsViewModel = new FriendsListViewModel();
				_friendsView.DataContext = _friendsViewModel;
			}
			Content.Content = _friendsView;
		}

		private void SettingsButton_Click(object sender, RoutedEventArgs e)
		{
			
		}

	}
}