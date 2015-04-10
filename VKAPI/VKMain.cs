using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace VKAPI
{
    public static class VkMain
    {
        //строка авторизации
        public static string vk_auth_url =
            "http://oauth.vk.com/authorize?client_id=2013444&scope=notify,friends,wall,messages,offline,status,audio,photos&redirect_uri=http://oauth.vk.com/blank.html&display=popup&response_type=token";

        //токен
        public static string token = "";
    }
}