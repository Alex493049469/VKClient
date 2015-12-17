using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public Photo photo { get; set; }
    }

    public class PostSource
    {
        public string type { get; set; }
    }

    public class CopyHistory
    {
        public int id { get; set; }
        public int owner_id { get; set; }
        public int from_id { get; set; }
        public int date { get; set; }
        public string post_type { get; set; }
        public string text { get; set; }
        public int signer_id { get; set; }
        public List<Attachment2> attachments { get; set; }
        public PostSource post_source { get; set; }
    }

    public class PostSource2
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

    public class Doc
    {
        public int id { get; set; }
        public int owner_id { get; set; }
        public string title { get; set; }
        public int size { get; set; }
        public string ext { get; set; }
        public string url { get; set; }
        public int date { get; set; }
        public string photo_100 { get; set; }
        public string photo_130 { get; set; }
        public string access_key { get; set; }
    }

    public class Attachment3
    {
        public string type { get; set; }
        public Doc doc { get; set; }
    }

    public class Wall
    {
        public int id { get; set; }
        public int from_id { get; set; }
        public int to_id { get; set; }
        public int date { get; set; }
        public string post_type { get; set; }
        public string text { get; set; }
        public List<CopyHistory> copy_history { get; set; }
        public PostSource2 post_source { get; set; }
        public Comments comments { get; set; }
        public Likes likes { get; set; }
        public Reposts reposts { get; set; }
        public List<Attachment3> attachments { get; set; }
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
        public string photo_640 { get; set; }
        public string access_key { get; set; }
    }

    public class Attachment
    {
        public string type { get; set; }
        public Wall wall { get; set; }
        public Video video { get; set; }
    }

    public class PushSettings
    {
        public int sound { get; set; }
        public int disabled_until { get; set; }
    }

    public class Message
    {
        //идентификатор сообщения (не возвращается для пересланных сообщений).
        public int id { get; set; }
        //дата отправки сообщения в формате unixtime.
        public int date { get; set; }
        //тип сообщения (0 — полученное, 1 — отправленное, не возвращается для пересланных сообщений).
        public int @out { get; set; }
        //идентификатор пользователя, в диалоге с которым находится сообщение.
        public int user_id { get; set; }
        //статус сообщения (0 — не прочитано, 1 — прочитано, не возвращается для пересланных сообщений).
        public int read_state { get; set; }
        //заголовок сообщения или беседы.
        public string title { get; set; }
        //текст сообщения.
        public string body { get; set; }
        //массив медиа-вложений
        public List<Attachment> attachments { get; set; }
        //идентификатор беседы.
        public int? chat_id { get; set; }
        //идентификаторы авторов последних сообщений беседы.
        public List<int> chat_active { get; set; }
        //настройки уведомлений для беседы, если они есть. sound и disabled_until
        public PushSettings push_settings { get; set; }
        //количество участников беседы.
        public int? users_count { get; set; }
        //идентификатор создателя беседы.
        public int? admin_id { get; set; }
        //url копии фотографии беседы шириной 50px.
        public string photo_50 { get; set; }
        //url копии фотографии беседы шириной 100px.
        public string photo_100 { get; set; }
        //url копии фотографии беседы шириной 200px.
        public string photo_200 { get; set; }
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
