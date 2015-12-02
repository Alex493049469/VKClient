using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace VKAPI
{
    public abstract class VkBase
    {
        //базовый Url
        private string _baseUrl = @"https://api.vk.com/method/";
        ///вызываемый метод
        protected string Method;
        ////список необходимых полей
        //protected string Fields;
        //список необходимых параметров параметр = значение
        protected Dictionary<string, string> Parameters = new Dictionary<string, string>();
        //версия Api
        protected string ApiVersion = "v=5.29";
        //Сфомированная строка запроса
        protected string Url;

        /// <summary>
        /// Отправка запроса и получение ответа в строку
        /// </summary>
        /// <returns></returns>
        protected string GetData()
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
        public string GenerateRequest()
        {
            string url = _baseUrl + Method + "?";
            if (Parameters.Count > 0)
            {
                url += ConvertToStringParameters();
            }
            url += "&" + ApiVersion + "&access_token=" + VkMain.token;
            return url;
        }

        /// <summary>
        /// Добавление параметров, параметры = 0 или "" не добавляются
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="value"></param>
        private void AddParameter(string parameter, object value)
        {
            if (value != null)
            {
                Type type = value.GetType();

                if (type.Name == "Int32")
                {
                    int val = Convert.ToInt32(value);
                    if (val > 0)
                    {
                        Parameters.Add(parameter, value.ToString());
                    }
                }

                if (type.Name == "String")
                {
                    string val = Convert.ToString(value);
                    if (val.Length > 0)
                    {
                        Parameters.Add(parameter, value.ToString());
                    }
                }
            }
        }

        protected void AddParameters(Dictionary<string, object> parameters)
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
        private void ClearParameters()
        {
            Parameters.Clear();
        }

        /// <summary>
        /// Преобразование списка параметров в строку
        /// </summary>
        /// <returns></returns>
        private string ConvertToStringParameters()
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
