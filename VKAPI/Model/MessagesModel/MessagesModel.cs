using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKAPI.Model.MessagesModel
{
	public class Sticker
	{
		public int id { get; set; }
		public int product_id { get; set; }
		public string photo_64 { get; set; }
		public string photo_128 { get; set; }
		public string photo_256 { get; set; }
		public string photo_352 { get; set; }
		public string photo_512 { get; set; }
		public int width { get; set; }
		public int height { get; set; }
	}

	public class Gift
	{
		public int id { get; set; }
		public string thumb_256 { get; set; }
		public string thumb_96 { get; set; }
		public string thumb_48 { get; set; }
	}

	public class Link
	{
		public string url { get; set; }
		public string title { get; set; }
		public string description { get; set; }
	}

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
		public Sticker sticker { get; set; }
		public Gift gift { get; set; }
		public Link link { get; set; }
		public Wall wall { get; set; }
		public Video video { get; set; }
	}

	public class Wall
	{
		public int id { get; set; }
		public int from_id { get; set; }
		public int to_id { get; set; }
		public int date { get; set; }
		public string post_type { get; set; }
		public string text { get; set; }
		public List<Attachment2> attachments { get; set; }
		public PostSource post_source { get; set; }
		public Comments comments { get; set; }
		public Likes likes { get; set; }
		public Reposts reposts { get; set; }
	}

	public class PostSource
	{
		public string type { get; set; }
	}

	public class Reposts
	{
		public int count { get; set; }
		public int user_reposted { get; set; }
	}

	public class Comments
	{
		public int count { get; set; }
		public int can_post { get; set; }
	}

	public class Likes
	{
		public int count { get; set; }
		public int user_likes { get; set; }
		public int can_like { get; set; }
		public int can_publish { get; set; }
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
		public List<Attachment> attachments { get; set; }
	}

	public class Response
	{
		public int count { get; set; }
		public int unread { get; set; }
		public List<Item> items { get; set; }
	}

	public class MessagesModel
	{
		public Response response { get; set; }
	}

}
