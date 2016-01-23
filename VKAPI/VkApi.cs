using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKAPI.Category;

namespace VKAPI
{
	/// <summary>
	/// Фасад для доступа к методам VkApi
	/// </summary>
	public class VkApi
	{
		public VkApi()
		{
			Audio = new Audio();
			Friends = new Friends();
			Messages = new Messages();
			Users = new Users();
			Account = new Account();
		}

		public Account Account { get; set; }
		public Audio Audio { get; private set; }
		public Friends Friends{ get; private set; }
		public Messages Messages{ get; private set; }
		public Users Users{ get; private set; }


	}
}
