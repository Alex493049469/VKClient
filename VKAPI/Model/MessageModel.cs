using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VKAPI.Model
{
     [XmlRoot("response")]
   public class MessageModel
    {
        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("items")]
        [XmlArrayItem("dialog",typeof(Dialog))]

        public List<Dialog> Items { get; set; }

    }

     public class Dialog
     {
         [XmlElement("message")]
         public Message message { get; set; }
     }
}
