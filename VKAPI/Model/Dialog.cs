using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VKAPI.Model
{
    public class Dialog
    {
        //если =1 значит тут есть не прочитанное сообщение
        [XmlElement("unread")]
        public int Unread { get; set; }
        [XmlElement("message")]
        public Message message { get; set; }
    }
}
