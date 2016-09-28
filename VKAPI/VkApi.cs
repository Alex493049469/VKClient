using System.Security.Cryptography.X509Certificates;
using VKAPI.Authorization;
using VKAPI.Category;
using VKAPI.Handlers;

namespace VKAPI
{
	/// <summary>
	/// Фасад для доступа к методам VkApi
	/// </summary>
	public class VkApi
	{
		//здесь необходимо проверять есть ли токен
		public VkApi()
		{
			Audio = new Audio();
			Friends = new Friends();
			Messages = new Messages();
			Users = new Users();
			Account = new Account();
		}

		public void Authorize(int  clientId)
		{
			Auth = new Auth(clientId);
		}



		public Account Account { get; private set; }
		public Audio Audio { get; private set; }
		public Friends Friends{ get; private set; }
		public Messages Messages{ get; private set; }
		public Users Users{ get; private set; }
		public Auth Auth { get; private set; }

	}

}
