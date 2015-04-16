using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VKAPI.Model
{
    [XmlRoot("response")]
   public class UserModel
    {
         [XmlElement("user")]
        public User user { get; set; }

    }
}
