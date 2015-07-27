using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKAPI.Model.UsersModel
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

    public class Counters
    {
        public int albums { get; set; }
        public int videos { get; set; }
        public int audios { get; set; }
        public int notes { get; set; }
        public int photos { get; set; }
        public int groups { get; set; }
        public int gifts { get; set; }
        public int friends { get; set; }
        public int online_friends { get; set; }
        public int user_photos { get; set; }
        public int user_videos { get; set; }
        public int followers { get; set; }
        public int subscriptions { get; set; }
        public int pages { get; set; }
    }

    public class Occupation
    {
        public string type { get; set; }
        public int id { get; set; }
        public string name { get; set; }
    }

    public class RelationPartner
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
    }

    public class Personal
    {
        public int political { get; set; }
        public string religion { get; set; }
        public string inspired_by { get; set; }
        public int people_main { get; set; }
        public int life_main { get; set; }
        public int smoking { get; set; }
        public int alcohol { get; set; }
    }

    public class University
    {
        public int id { get; set; }
        public int country { get; set; }
        public int city { get; set; }
        public string name { get; set; }
        public int faculty { get; set; }
        public string faculty_name { get; set; }
        public int chair { get; set; }
        public string chair_name { get; set; }
        public int graduation { get; set; }
        public string education_form { get; set; }
        public string education_status { get; set; }
    }

    public class School
    {
        public string id { get; set; }
        public int country { get; set; }
        public int city { get; set; }
        public string name { get; set; }
        public int year_to { get; set; }
        public string @class { get; set; }
    }

    public class Relative
    {
        public int id { get; set; }
        public string type { get; set; }
    }

    public class Response
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public int sex { get; set; }
        public string domain { get; set; }
        public string screen_name { get; set; }
        public string bdate { get; set; }
        public City city { get; set; }
        public Country country { get; set; }
        public int timezone { get; set; }
        public string photo_50 { get; set; }
        public string photo_100 { get; set; }
        public string photo_200 { get; set; }
        public string photo_max { get; set; }
        public string photo_200_orig { get; set; }
        public string photo_400_orig { get; set; }
        public string photo_max_orig { get; set; }
        public string photo_id { get; set; }
        public int has_mobile { get; set; }
        public int online { get; set; }
        public string online_app { get; set; }
        public int online_mobile { get; set; }
        public int can_post { get; set; }
        public int can_see_all_posts { get; set; }
        public int can_see_audio { get; set; }
        public int can_write_private_message { get; set; }
        public string mobile_phone { get; set; }
        public string home_phone { get; set; }
        public string skype { get; set; }
        public string site { get; set; }
        public string status { get; set; }
        public LastSeen last_seen { get; set; }
        public int common_count { get; set; }
        public Counters counters { get; set; }
        public Occupation occupation { get; set; }
        public int university { get; set; }
        public string university_name { get; set; }
        public int faculty { get; set; }
        public string faculty_name { get; set; }
        public int graduation { get; set; }
        public string education_form { get; set; }
        public string education_status { get; set; }
        public int relation { get; set; }
        public RelationPartner relation_partner { get; set; }
        //public Personal personal { get; set; }
        public string interests { get; set; }
        public string music { get; set; }
        public string activities { get; set; }
        public string movies { get; set; }
        public string tv { get; set; }
        public string books { get; set; }
        public string games { get; set; }
        public ObservableCollection<University> universities { get; set; }
        public ObservableCollection<School> schools { get; set; }
        public string about { get; set; }
        public ObservableCollection<Relative> relatives { get; set; }
        public string quotes { get; set; }
    }

    public class UsersModel
    {
        public ObservableCollection<Response> response { get; set; }
    }
}
