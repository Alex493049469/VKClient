using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKAPI.Model.MessagesModel
{
	public class Photo
	{
		public int id { get; set; }
		public int album_id { get; set; }
		public int owner_id { get; set; }
		public string photo_75 { get; set; }
		public string photo_130 { get; set; }
		public string photo_604 { get; set; }
		public int width { get; set; }
		public int height { get; set; }
		public string text { get; set; }
		public int date { get; set; }
		public string access_key { get; set; }
		public string photo_807 { get; set; }
		public string photo_1280 { get; set; }
		public double? lat { get; set; }
		public double? @long { get; set; }
	}

	public class Attachment
	{
		public string type { get; set; }
		public Photo photo { get; set; }
	}

	public class FwdMessage
	{
		public int user_id { get; set; }
		public int date { get; set; }
		public string body { get; set; }
		public List<Attachment> attachments { get; set; }
	}

	public class Photo2
	{
		public int id { get; set; }
		public int album_id { get; set; }
		public int owner_id { get; set; }
		public string photo_75 { get; set; }
		public string photo_130 { get; set; }
		public string photo_604 { get; set; }
		public int width { get; set; }
		public int height { get; set; }
		public string text { get; set; }
		public int date { get; set; }
		public string access_key { get; set; }
		public string photo_807 { get; set; }
		public string photo_1280 { get; set; }
		public string photo_2560 { get; set; }
	}

	public class Video
	{
		public int id { get; set; }
		public int owner_id { get; set; }
		public string title { get; set; }
		public int duration { get; set; }
		public string description { get; set; }
		public int date { get; set; }
		public int views { get; set; }
		public int comments { get; set; }
		public string photo_130 { get; set; }
		public string photo_320 { get; set; }
		public string photo_800 { get; set; }
		public string access_key { get; set; }
	}

	public class Attachment2
	{
		public string type { get; set; }
		public Photo2 photo { get; set; }
		public Video video { get; set; }
	}

	public class Item
	{
		public int id { get; set; }
		public string body { get; set; }
		public int user_id { get; set; }
		public int from_id { get; set; }
		public int date { get; set; }
		public int read_state { get; set; }
		public int @out { get; set; }
		public int chat_id { get; set; }
		public List<FwdMessage> fwd_messages { get; set; }
		public int? emoji { get; set; }
		public List<Attachment2> attachments { get; set; }
	}

	public class Response
	{
		public int count { get; set; }
		public int unread { get; set; }
		public List<Item> items { get; set; }
	}

	public class RootObject
	{
		public Response response { get; set; }
	}

	public class MessagesModel
	{
		public Response response { get; set; }
	}
}
