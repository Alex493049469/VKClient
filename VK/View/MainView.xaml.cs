using System.Windows;
using VK.Properties;
using VK.ViewModel.Main;
using VKAPI;

namespace VK.View
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			this.DataContext = MainViewModel.This;

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
		}

	}
}