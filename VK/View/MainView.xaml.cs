using System.Windows;
using MahApps.Metro.Controls.Dialogs;
using VK.Properties;
using VK.ViewModel.Main;
using VKAPI;

namespace VK.View
{
	public partial class MainWindow 
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

		private void SettingsButton_Click(object sender, RoutedEventArgs e)
		{
			//testFlyout.IsOpen = true;
			//this.ShowMessageAsync("This is the title", "Some message");
		}
	}
}