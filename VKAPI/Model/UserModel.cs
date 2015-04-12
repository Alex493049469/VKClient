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

        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("items")]
        [XmlArrayItem("user", typeof(User))]
        public List<User> Items { get; set; }

    }
}
