using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Nemiro.OAuth;
using Nemiro.OAuth.Clients;

namespace VKAPI.Authorization
{
	/// <summary>
	/// Класс для авторизации в Контакте
	/// </summary>
    public class Auth
    {
		/// <summary>
		/// Авторизация в контакте для получения токена
		/// </summary>
		/// <param name="username">емейл или номер телефона</param>
		/// <param name="password">пароль</param>
		/// <returns>возвращает true если получение токена прошло успешно</returns>
	    public bool Authorization(string username, string password)
	    {
			var client = new VkontakteClient("2013444", "YlW2iVHKMZhhcjEqkx7b")
			{
				Username = username,
				Password = password,
				GrantType = GrantType.ClientCredentials,
				Scope = @"notify,friends,wall,messages,offline,status,audio,photos"
			};
			var accessToken = client.AccessToken;
			if (accessToken != "" || accessToken != null)
			{
				VkSettings.Token = accessToken.Value;
				return true;
			}
			else return false;
	    }
    }
}
