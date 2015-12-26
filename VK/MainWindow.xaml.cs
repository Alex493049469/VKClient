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
	///     Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		//View models
		private BaseViewModel audiosViewModel;
		private BaseViewModel friendsViewModel;
		private BaseViewModel pageViewModel;
		private BaseViewModel dialogsViewModel;

		//Views
		private UserControl audiosView;
		private UserControl friendsView;
		private UserControl pageView;
		private UserControl dialogsView;

		public MainWindow()
		{
			InitializeComponent();
			if (Settings.Default.token == "")
			{
				//скрываем главную форму если нет токена 
				Visibility = Visibility.Hidden;
				//отображаем окно авторизации
				var authorization = new AuthorizationWindow();
				//ссылка на главную форму
				authorization.main = this;
				authorization.Show();
			}
			else
			{
				VkMain.token = Settings.Default.token;
			}
		 }

		private void AudioButton_Click(object sender, RoutedEventArgs e)
		{
			if (audiosView == null) audiosView = new AudioView();
			if (audiosViewModel == null)
			{
				audiosViewModel = new AudioListViewModel();
				audiosView.DataContext = audiosViewModel;
			}
			Content.Content = audiosView;
		}

		private void MyPageButton_Click(object sender, RoutedEventArgs e)
		{
			if (pageView == null) pageView = new PageView();
			if (pageViewModel == null)
			{
				pageViewModel = new PageViewModel();
				pageView.DataContext = pageViewModel;
			}
			Content.Content = pageView;
		}

		private void MyMessageButton_Click(object sender, RoutedEventArgs e)
		{
			if (dialogsView == null) dialogsView = new DialogsView();
			if (dialogsViewModel == null)
			{
				dialogsViewModel = new DialogListViewModel();
				dialogsView.DataContext = dialogsViewModel;
			}
			Content.Content = dialogsView;
		}

		private void MyFriendsButton_Click(object sender, RoutedEventArgs e)
		{
			if (friendsView == null) friendsView = new FriendsView();
			if (friendsViewModel == null)
			{
				friendsViewModel = new FriendsListViewModel();
				friendsView.DataContext = friendsViewModel;
			}
			Content.Content = friendsView;
		}

	}
}