using System.Collections.ObjectModel;


namespace VKAPI.Model.AudioModel
{
    public class AudioItem
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

	public class AudioResponse
    {
        public int count { get; set; }
		public ObservableCollection<AudioItem> items { get; set; }
    }

    public class AudioModel
    {
		public AudioResponse response { get; set; }
    }
}
