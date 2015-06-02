using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKAPI.Converters;

namespace VKAPI.Model.AudioModel
{
    public class Item
    {
        public int id { get; set; }
        public int owner_id { get; set; }
        public string artist { get; set; }
        public string title { get; set; }

        public int duration { get; set; }

        public string url { get; set; }
        public int lyrics_id { get; set; }
        public int genre_id { get; set; }
        public int? no_search { get; set; }
    }

    public class Response
    {
        public int count { get; set; }
        public ObservableCollection<Item> items { get; set; }
    }

    public class AudioModel
    {
        public Response response { get; set; }
    }
}
