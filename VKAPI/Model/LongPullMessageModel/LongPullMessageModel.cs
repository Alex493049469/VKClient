using System.Collections.Generic;

namespace VKAPI.Model.LongPullMessageModel
{
	public class PushSettings
	{
		public int sound { get; set; }
		public int disabled_until { get; set; }
	}

	public class Item
	{
		public int id { get; set; }
		public int date { get; set; }
		public int @out { get; set; }
		public int user_id { get; set; }
		public int read_state { get; set; }
		public string title { get; set; }
		public string body { get; set; }
		public int? chat_id { get; set; }
		public List<int?> chat_active { get; set; }
		public PushSettings push_settings { get; set; }
		public int? users_count { get; set; }
		public int? admin_id { get; set; }
		public string photo_50 { get; set; }
		public string photo_100 { get; set; }
		public string photo_200 { get; set; }
	}

	public class Messages
	{
		public int count { get; set; }
		public List<Item> items { get; set; }
	}

	public class Profile
	{
		public int id { get; set; }
		public string first_name { get; set; }
		public string last_name { get; set; }
		public int sex { get; set; }
		public string screen_name { get; set; }
		public string photo { get; set; }
		public string photo_medium_rec { get; set; }
		public int online { get; set; }
		public string online_app { get; set; }
		public int? online_mobile { get; set; }
	}

	public class Chat
	{
		public int id { get; set; }
		public string type { get; set; }
		public string title { get; set; }
		public int admin_id { get; set; }
		public List<int> users { get; set; }
		public string photo_50 { get; set; }
		public string photo_100 { get; set; }
		public string photo_200 { get; set; }
	}

	public class Response
	{
		public List<List<int>> history { get; set; }
		public Messages messages { get; set; }
		public List<Profile> profiles { get; set; }
		public List<Chat> chats { get; set; }
		public int new_pts { get; set; }
	}

	public class LongPullMessageModel
	{
		public Response response { get; set; }
	}
}
