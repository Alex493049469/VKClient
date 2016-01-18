using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKAPI.Model.ErrorModel;

namespace VKAPI.Handlers
{
	internal class ErrorHandlerRequest
	{
		public ErrorHandlerRequest(ErrorModel error)
		{
			if (error != null)
			{
				VkSettings.Token = null;
			
			}
		}
	}
}
