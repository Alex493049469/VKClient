using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKAPI.Model.DialogsModel
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
    }

    public class Photo2
    {
        public int id { get; set; }
        public int album_id { get; set; }
        public int owner_id { get; set; }
        public int user_id { get; set; }
        public string photo_75 { get; set; }
        public string photo_130 { get; set; }
        public string photo_604 { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string text { get; set; }
        public int date { get; set; }
        public int post_id { get; set; }
        public string access_key { get; set; }
    }

    public class Attachment2
    {
        public string type { get; set; }
        public Photo2 photo { get; set; }
    }

    public class PostSource
    {
        public string type { get; set; }
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

    public class Reposts
    {
        public int count { get; set; }
        public int user_reposted { get; set; }
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

    public class Attachment
    {
        public string type { get; set; }
        public Photo photo { get; set; }
        public Wall wall { get; set; }
    }

    public class PushSettings
    {
        public int sound { get; set; }
        public int disabled_until { get; set; }
    }

    public class Message
    {
        public int id { get; set; }
        public int date { get; set; }
        public int @out { get; set; }
        public int user_id { get; set; }
        public int read_state { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public List<Attachment> attachments { get; set; }
        public int chat_id { get; set; }
        public List<int> chat_active { get; set; }
        public PushSettings push_settings { get; set; }
        public int users_count { get; set; }
        public int admin_id { get; set; }
        public string photo_50 { get; set; }
        public string photo_100 { get; set; }
        public string photo_200 { get; set; }
        public int? emoji { get; set; }
    }

    public class Item
    {
        public Message message { get; set; }
    }

    public class Response
    {
        public int count { get; set; }
        public List<Item> items { get; set; }
    }

    public class DialogsModel
    {
        public Response response { get; set; }
    }
}
