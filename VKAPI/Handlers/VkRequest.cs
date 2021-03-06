﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VKAPI.Handlers
{
	/// <summary>
	/// Отвечает за формирование и отправку запроса Vk Api
	/// </summary>
	public static class VkRequest 
	{
		//Так же необходимо реализовать таймер который будет контроллировать чтоб не уходило более 3 запросов в секунду

		//базовый Url
		private static string _baseUrl = @"https://api.vk.com/method/";

		//версия Api
		private static string ApiVersion = "v=5.52";
		static readonly object _locker = new object();

		private static Dictionary<string, DateTime> _queue = new Dictionary<string, DateTime>();
		/// <summary>
		/// Отправка запроса и получение ответа в строку
		/// </summary>
		/// <returns></returns>
		public static string GetData(string method, Dictionary<string, object> parameters = null)
		{
			lock (_locker)
			{
				string url = GenerateRequest(method, ClearEmptyParameters(parameters));
				//чтоб не превычать максимальную частоту запросов к VK api (3 раза в секунду)
				while (_queue.Count > 1)
					Thread.Sleep(300);
				var item = url + DateTime.Now;
				_queue.Add(item, DateTime.Now);

				var webRequest = WebRequest.Create(url);
				var response = webRequest.GetResponse();
				var stream = response.GetResponseStream();
				var sr = new StreamReader(stream);
				var data = sr.ReadToEnd();

				response.Close();
				stream.Close();
				sr.Close();
				_queue.Remove(item);
				return data;
			}
		}

		public static string PostData(string method, Dictionary<string, object> parameters, string data)
		{
			string url = GenerateRequest(method, ClearEmptyParameters(parameters));
			WebRequest request = WebRequest.Create(url);
			request.Method = "POST";
			request.Timeout = 100000;
			request.ContentType = "application/x-www-form-urlencoded";
			byte[] sentData = Encoding.GetEncoding(1251).GetBytes(data);
			request.ContentLength = sentData.Length;
			Stream sendStream = request.GetRequestStream();
			sendStream.Write(sentData, 0, sentData.Length);
			sendStream.Close();

			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			// Получить содержимое потока, возвращаемого сервером
			Stream dataStream = response.GetResponseStream();
			// Открыть поток, используя StreamReader для легкого доступа
			StreamReader reader = new StreamReader(dataStream);
			// Прочитать содержимое
			string responseFromServer = reader.ReadToEnd();
			// Закрыть все
			reader.Close();
			dataStream.Close();
			response.Close();

			return responseFromServer;
		}

		/// <summary>
		/// склеивание всех параметров в строку запроса
		/// </summary>
		/// <returns></returns>
		private static string GenerateRequest(string method, Dictionary<string, object> parameters)
		{
			string url = _baseUrl + method + "?";
			if (parameters != null && parameters.Count > 0)
			{
				url += ConvertToStringParameters(parameters);
			}
			url += "&" + ApiVersion + "&access_token=" + VkSettings.Token;
			return url;
		}

		/// <summary>
		/// принимает список параметров, удаляет пустые и возвращает результат
		/// </summary>
		/// <param name="parameters"></param>
		/// <returns></returns>
		private static Dictionary<string, object> ClearEmptyParameters(Dictionary<string, object> parameters)
		{
			Dictionary<string, object> param = new Dictionary<string, object>();

			if (parameters == null) return null;
			foreach (var parameter in parameters)
			{
				if (parameter.Value != null)
				{
					if (parameter.Value is int)
					{
						int valInt = (int) parameter.Value;
						if (valInt != 0)
						{
							param.Add(parameter.Key, parameter.Value);
						}
					}

					if (parameter.Value is string)
					{
						var valStr = (string) parameter.Value;
						if (!string.IsNullOrEmpty(valStr))
						{
							param.Add(parameter.Key, parameter.Value);
						}
					}
				}
			}
			return param;
		}

		/// <summary>
		/// Преобразование списка параметров в строку
		/// </summary>
		/// <returns></returns>
		private static string ConvertToStringParameters(Dictionary<string, object> parameters)
		{
			string param = "";

			foreach (var item in parameters)
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
