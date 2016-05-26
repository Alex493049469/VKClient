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
		private readonly IVkRequest _vkRequest;

		public Account(IVkRequest vkRequest)
		{
			_vkRequest = vkRequest;
		}

		/// <summary>
		/// Помечает текущего пользователя как online на 15 минут.
		/// </summary>
		/// <returns></returns>
		public bool SetOnline()
		{
			string str = _vkRequest.GetData("account.setOnline");
			return true;
		}

		/// <summary>
		/// Помечает текущего пользователя как offline. 
		/// </summary>
		/// <returns></returns>
		public bool SetOffline()
		{
			string str = _vkRequest.GetData("account.setOffline");
			return true;
		}


	}
}
