using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VKAPI.Handlers;
using VKAPI.Model.AudioModel;

namespace VKAPI.Category
{
	/// <summary>
	/// Действия с аккаунтом пользователя
	/// </summary>
	public class Account
	{	
		/// <summary>
		/// Помечает текущего пользователя как online на 15 минут.
		/// </summary>
		/// <returns></returns>
		public bool SetOnline()
		{
			string str = VkRequest.GetData("account.setOnline");
			return true;
		}

		/// <summary>
		/// Помечает текущего пользователя как offline. 
		/// </summary>
		/// <returns></returns>
		public bool SetOffline()
		{
			string str = VkRequest.GetData("account.setOffline");
			return true;
		}


	}
}
