using System.Collections.Generic;

namespace VKAPI.Handlers
{
	public interface IVkRequest
	{
		string GetData(string method, Dictionary<string, object> parameters = null);
		string PostData(string method, Dictionary<string, object> parameters, string data);
	}
}
