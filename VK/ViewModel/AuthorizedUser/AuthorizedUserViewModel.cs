using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using VKAPI;

namespace VK.ViewModel.AuthorizedUser
{
	class AuthorizedUserViewModel : BaseViewModel
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
			if (!string.IsNullOrEmpty(VkSettings.Token))
			{
				var user =  await _vkApi.Users.GetAsync();
				Photo = user.response[0].photo_100;
				Name = user.response[0].first_name + " " + user.response[0].last_name;
			}
		}
		


	}
}
