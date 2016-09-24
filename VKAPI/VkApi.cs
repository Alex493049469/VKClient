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
		private readonly IVkRequest _vkRequest;


		//здесь необходимо проверять есть ли токен
		public VkApi(IVkRequest vkRequest)
		{
			_vkRequest = vkRequest;
			Audio = new Audio(_vkRequest);
			Friends = new Friends(_vkRequest);
			Messages = new Messages(_vkRequest);
			Users = new Users(_vkRequest);
			Account = new Account(_vkRequest);
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
