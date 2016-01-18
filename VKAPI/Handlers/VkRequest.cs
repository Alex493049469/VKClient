using System.Collections.Generic;
using System.IO;
using System.Net;

namespace VKAPI.Handlers
{
	/// <summary>
	/// Отвечает за формирование и отправка запроса к Vk Api
	/// </summary>
	internal static class VkRequest
	{
		//Так же необходимо реализовать таймер который будет контроллировать чтоб не уходило более 3 запросов в секунду
		//базовый Url
		private static string _baseUrl = @"https://api.vk.com/method/";
		///вызываемый метод
		public static string Method;
		//список необходимых параметров параметр = значение
		internal static Dictionary<string, string> Parameters = new Dictionary<string, string>();
		//версия Api
		internal static string ApiVersion = "v=5.29";
		//Сфомированная строка запроса
		internal static string Url;

		/// <summary>
		/// Отправка запроса и получение ответа в строку
		/// </summary>
		/// <returns></returns>
		internal static string GetData()
		{
			Url = GenerateRequest();

			var reqGet = WebRequest.Create(Url);
			var resp = reqGet.GetResponse();
			var stream = resp.GetResponseStream();
			var sr = new StreamReader(stream);
			var str = sr.ReadToEnd();

			//возвращаем данные
			return str;
		}

		/// <summary>
		/// склеивание всех параметров в строку запроса
		/// </summary>
		/// <returns></returns>
		public static string GenerateRequest()
		{
			string url = _baseUrl + Method + "?";
			if (Parameters.Count > 0)
			{
				url += ConvertToStringParameters();
			}
			url += "&" + ApiVersion + "&access_token=" + VkSettings.Token;
			return url;
		}

		/// <summary>
		/// Добавление параметров, параметры = 0 или "" не добавляются
		/// </summary>
		/// <param name="parameter"></param>
		/// <param name="value"></param>
		private static void AddParameter(string parameter, object value)
		{
			if (value != null)
			{
				if (value is int)
				{
					int valInt = (int)value;
					if (valInt != 0)
					{
						Parameters.Add(parameter, value.ToString());
					}
				}

				if (value is string)
				{
					var valStr = (string)value;
					if (valStr.Length > 0)
					{
						Parameters.Add(parameter, value.ToString());
					}
				}
			}
		}

		internal static void AddParameters(Dictionary<string, object> parameters)
		{
			ClearParameters();
			foreach (var parameter in parameters)
			{
				AddParameter(parameter.Key, parameter.Value);
			}
		}

		/// <summary>
		/// Удаление параметров
		/// </summary>
		private static void ClearParameters()
		{
			Parameters.Clear();
		}

		/// <summary>
		/// Преобразование списка параметров в строку
		/// </summary>
		/// <returns></returns>
		private static string ConvertToStringParameters()
		{
			string param = "";

			foreach (var item in Parameters)
			{
				if (param.Length > 0)
				{
					param += "&" + item.Key + item.Value;
				}
				else
				{
					param = item.Key + item.Value;
				}
			}
			return param;
		}


	}
}
