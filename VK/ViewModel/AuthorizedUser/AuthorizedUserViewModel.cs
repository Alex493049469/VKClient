using System.Windows;
using Core;
using VK.Properties;
using VK.Services;
using VK.View;
using VKAPI;
using System.Collections.ObjectModel;

namespace VK.ViewModel.AuthorizedUser
{
	public class AuthorizedUserViewModel : BaseViewModel
	{

		private readonly VkApi _vkApi;
		public string Photo { get; set; }
		public string Name { get; set; }

		public AuthorizedUserViewModel(VkApi vkApi)
		{
			_vkApi = vkApi;
			LoadUser();
		}

		public async void LoadUser()
		{
			if (Settings.Default.token != "")
			{
				VkSettings.Token = Settings.Default.token;

			}

			if (!string.IsNullOrEmpty(VkSettings.Token))
			{
				var user = _vkApi.Users.Get();
				Photo = user.response[0].photo_100;
				Name = user.response[0].first_name + " " + user.response[0].last_name;

				StartServices();
			}
		}

		public void StartServices()
		{
			//ManagerService.Instance.StartManagerService();
			//ManagerService.Instance.DialogService.GetDialog();
			//ManagerService.Instance.EventService.LongPool();
		}

	}
}
