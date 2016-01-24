using Core;
using VK.Properties;
using VKAPI;

namespace VK.ViewModel.AuthorizedUser
{
	public class AuthorizedUserViewModel : BaseViewModel
	{
		VkApi _vkApi = new VkApi();
		public string Photo { get; set; }
		public string Name { get; set; }

		public AuthorizedUserViewModel()
		{
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
				var user =  _vkApi.Users.Get();
				Photo = user.response[0].photo_100;
				Name = user.response[0].first_name + " " + user.response[0].last_name;
			}
		}


	}
}
