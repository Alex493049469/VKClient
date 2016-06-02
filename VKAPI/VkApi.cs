using System.Security.Cryptography.X509Certificates;
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

		public VkApi(IVkRequest vkRequest)
		{
			_vkRequest = vkRequest;
			Audio = new Audio(_vkRequest);
			Friends = new Friends(_vkRequest);
			Messages = new Messages(_vkRequest);
			Users = new Users(_vkRequest);
			Account = new Account(_vkRequest);
		}

		public Account Account { get; private set; }
		public Audio Audio { get; private set; }
		public Friends Friends{ get; private set; }
		public Messages Messages{ get; private set; }
		public Users Users{ get; private set; }

	}

}
