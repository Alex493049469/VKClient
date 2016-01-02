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
using VK.ViewModel.Main;
using VK.ViewModel.Page;
using VKAPI;


namespace VK
{
	public partial class MainWindow : Window
	{
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

			DataContext = new MainViewModel();
		}

	}
}