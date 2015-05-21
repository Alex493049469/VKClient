using System;
using System.Xml.Serialization;

namespace VKAPI.Model
{
    public class User
    {
        //идентификатор пользователя
        [XmlElement("id")]
        public string Id { get; set; }

        //имя пользователя
        [XmlElement("first_name")]
        public string FirstName { get; set; }

        //фамилия пользователя
        [XmlElement("last_name")]
        public string LastName { get; set; }

        //1 — женский;
        //2 — мужской;
        //0 — пол не указан.
        /// <summary>
        ///     пол пользователя. Возможные значения:
        /// </summary>
        [XmlElement("sex")]
        public string Sex { get; set; }

        //дата рождения. 
        //Возвращается в формате DD.MM.YYYY 
        [XmlElement("bdate")]
        public string Bdate { get; set; }

        //информация о том, находится ли пользователь сейчас на сайте. 
        //Возвращаемые значения: 
        //1 — находится, 
        //0 — не находится. 
        //Если пользователь использует мобильное приложение либо мобильную версию сайта, 
        //возвращается дополнительное поле online_mobile, содержащее 1.
        //При этом, если используется именно приложение, дополнительно возвращается
        //поле online_app, содержащее его идентификатор.
        //флаг, может принимать значения 1 или 0
        [XmlElement("online")]
        public int Online
        {
            get { throw new NotImplementedException(); }
            set { if (value == 1) OnlineNorm = "Online"; }
        }

        public string OnlineNorm { get; set; }

        //статус пользователя.
        //Возвращается строка, содержащая текст статуса, расположенного в профиле под именем пользователя. 
        //Если у пользователя включена опция «Транслировать в статус играющую музыку», 
        //будет возвращено дополнительное поле status_audio, содержащее информацию о транслируемой композиции.
        [XmlElement("status")]
        public string Status { get; set; }

        //url квадратной фотографии пользователя, имеющей ширину 50 пикселей.
        //В случае отсутствия у пользователя фотографии возвращается http://vk.com/images/camera_c.gif
        [XmlElement("photo_50")]
        public string Photo50 { get; set; }

        [XmlElement("photo_100")]
        public string Photo100 { get; set; }

        [XmlElement("photo_200_orig")]
        public string Photo_200_orig { get; set; }
    }
}