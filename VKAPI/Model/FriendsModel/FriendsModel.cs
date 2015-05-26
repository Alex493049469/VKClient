using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKAPI.Model.FriendsModel
{
    public class City
    {
        public int id { get; set; }
        public string title { get; set; }
    }

    public class Country
    {
        public int id { get; set; }
        public string title { get; set; }
    }

    public class LastSeen
    {
        public int time { get; set; }
        public int platform { get; set; }
    }

    public class RelationPartner
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
    }

    public class StatusAudio
    {
        public int id { get; set; }
        public int owner_id { get; set; }
        public string artist { get; set; }
        public string title { get; set; }
        public int duration { get; set; }
        public string url { get; set; }
        public int lyrics_id { get; set; }
        public int? album_id { get; set; }
        public int? genre_id { get; set; }
    }

    public class Item
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public int sex { get; set; }
        public string nickname { get; set; }
        public string domain { get; set; }
        public string bdate { get; set; }
        public City city { get; set; }
        public Country country { get; set; }
        public string photo_50 { get; set; }
        public string photo_100 { get; set; }
        public string photo_200_orig { get; set; }
        public int has_mobile { get; set; }

        public int online
        {
            get { throw new NotImplementedException(); }
            set { if (value == 1) OnlineString = "Online"; }
        }

        public string OnlineString { get; set; }
        public int can_post { get; set; }
        public int can_see_all_posts { get; set; }
        public int can_write_private_message { get; set; }
        public string mobile_phone { get; set; }
        public string status { get; set; }
        public LastSeen last_seen { get; set; }
        public int university { get; set; }
        public string university_name { get; set; }
        public int faculty { get; set; }
        public string faculty_name { get; set; }
        public int graduation { get; set; }
        public string education_form { get; set; }
        public string education_status { get; set; }
        public int relation { get; set; }
        public RelationPartner relation_partner { get; set; }
        public List<object> universities { get; set; }
        public string home_phone { get; set; }
        public string online_app { get; set; }
        public int? online_mobile { get; set; }
        public StatusAudio status_audio { get; set; }
        public string deactivated { get; set; }
    }

    public class Response
    {
        public int count { get; set; }
        public List<Item> items { get; set; }
    }

    public class FriendsModel
    {
        public Response response { get; set; }
    }
}
