using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKAPI.Authorization
{
	/// <summary>
	/// Отвечает за построение Url для авторизации
	/// авторизацию
	/// и получение токена
	/// </summary>
	public class Auth
	{
		public enum Scope
		{
			notify, //(+1) 	Пользователь разрешил отправлять ему уведомления (для flash/iframe-приложений).
			friends, //(+2) 	Доступ к друзьям.
			photos, //(+4) 	Доступ к фотографиям.
			audio, //(+8) 	Доступ к аудиозаписям.
			video, //(+16) 	Доступ к видеозаписям.
			docs, //(+131072) 	Доступ к документам.
			notes, //(+2048) 	Доступ к заметкам пользователя.
			pages, //(+128) 	Доступ к wiki-страницам.
			//+256 	Добавление ссылки на приложение в меню слева.
			status, //(+1024) 	Доступ к статусу пользователя.
			offers, //(+32) 	Доступ к предложениям (устаревшие методы).
			questions, //(+64) 	Доступ к вопросам (устаревшие методы).
			wall, //(+8192) 	Доступ к обычным и расширенным методам работы со стеной. 
			//Данное право доступа по умолчанию недоступно для сайтов при использовании OAuth-авторизации (игнорируется при попытке авторизации).
			groups, //(+262144) 	Доступ к группам пользователя.
			messages, //(+4096) 	Доступ к расширенным методам работы с сообщениями (только для Standalone-приложений).
			email, //(+4194304) 	Доступ к email пользователя.
			notifications, //(+524288) 	Доступ к оповещениям об ответах пользователю.
			stats, //(+1048576) 	Доступ к статистике групп и приложений пользователя, администратором которых он является.
			ads, //(+32768) 	Доступ к расширенным методам работы с рекламным API.
			market, //(+134217728) 	Доступ к товарам.
			offline, //(+65536) 	Доступ к API в любое время (при использовании этой опции параметр expires_in, возвращаемый вместе с access_token, содержит 0 — токен бессрочный).
			nohttps  //Возможность осуществлять запросы к API без HTTPS. 
		}

		public enum Display
		{
			page,// — форма авторизации в отдельном окне;
			popup, // — всплывающее окно;
			mobile, // — авторизация для мобильных устройств (без использования Javascript)
		}

		private int _сlientId;
		private List<Scope> _scopeList = new List<Scope>();
		private string _redirectUri = "http://oauth.vk.com/blank.html";
		private string _version = "5.53";
		private Display _display = Display.popup;

		private string _authUrl = "https://oauth.vk.com/authorize";
		private string _responseType = "token";

		//для получения токена нужно вставить на страницу логин и пароль
		private string _login;
		private string _password;

		private string _token; 

		public string MakeAuthUrl()
		{
			if(_сlientId == 0)
				throw new Exception("Не указан СlientId");

			var tempScopeList = new List<string>();

			//если не задан список разрешения то указываем все
			if (_scopeList.Count == 0)
			{
				tempScopeList.AddRange(Enum.GetNames(typeof (Scope)));
			}
			else
			{
				tempScopeList.AddRange(_scopeList.Select(name => name.ToString()));
			}

			var scope = string.Join(",", tempScopeList);

			var vkAuthUrl = _authUrl + "?client_id=" + _сlientId + "&scope=" + scope + "&redirect_uri=" + _redirectUri + "&display=" + _display + "&response_type=" + _responseType + "&v=" + _version;
			return vkAuthUrl;
		}

		public Auth(int сlientId, List<Scope> scopeList, Display display, string redirectUri)
		{
			_сlientId = сlientId;
			_scopeList = scopeList;
			_display = display;
			_redirectUri = redirectUri;
		}

		public Auth(int сlientId)
		{
			_сlientId = сlientId;
		}

		public static void GetToken()
		{
			
		}

		public static void Authorization()
		{
			
		}

	}
}
