using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VKAPI.Handlers;
using VKAPI.Model.AudioModel;

namespace VKAPItests
{
	public class RequestTest : IVkRequest
	{
		public string GetData(string method, Dictionary<string, object> parameters = null)
		{
			if (method == "audio.get")
			{
				return TestRequestGetAudio;
			}
			if (method == "audio.GetRecommendations")
			{
				return TestRequestGetAudio;
			}

			throw new NotImplementedException();
		}

		public string PostData(string method, Dictionary<string, object> parameters, string data)
		{
			throw new NotImplementedException();
		}

		string TestRequestGetAudio =
			@"{""response"":{""count"":604,""items"":[{""id"":456239053,""owner_id"":39773664,""artist"":""Hoobastank"",""title"":""The Reason"",""duration"":232,""date"":1464247919,""url"":""https:\/\/cs4-3v4.vk-cdn.net\/p1\/100212aaebde60.mp3?extra=dPoRzE2oCLIqsCqZZQMnrDT2wGAXNL46bhKFCng_90I0pODiT-quto15bTSrTkskpBsXWoK9qVr2GQ-BJwnQpo5aPW6b2JmBdYChLNq48mkd4GWxMA1Xmy5KDr5M9gy1qiqaH4kuHfoptw"",""lyrics_id"":4638805,""genre_id"":21},{""id"":456239052,""owner_id"":39773664,""artist"":""Hoobastank"",""title"":""The Reason (Acoustic Version)"",""duration"":230,""date"":1464245047,""url"":""https:\/\/cs4-4v4.vk-cdn.net\/p3\/f5d57e8ba9acc6.mp3?extra=YVLqwnvOchj4ce1jXBuTANf7UWPwn_thz4XseLJqH4ru-wx0M2mpirHY8Bw4mBb96lKygcV9BOGp8QvFskQbXi876J5G6k3Lfc7DGU0IAJoPW-7Z4D669Pq36-y8livHckJ4uyM-nDp4Xw"",""lyrics_id"":7121107,""genre_id"":1},{""id"":456239048,""owner_id"":39773664,""artist"":""Breaking Benjamin"",""title"":""Diary of Jane"",""duration"":203,""date"":1463925698,""url"":""https:\/\/cs4-3v4.vk-cdn.net\/p22\/49a6e4cecc8a67.mp3?extra=6xOY6Z2GuzGzpnKkIoiw5Fc0POjE9KeCkg_8JNrXoUN4k-EanqaeTTKYc7wbf34k56_Mf_N9ue-txzToyhNnUOrQ-5eXR9N5YbIqPJtdpkYKKfYRMhB6XZcniF2Z7oGcsWrhaLAZaSs4fg"",""lyrics_id"":1527155},{""id"":456239047,""owner_id"":39773664,""artist"":""Hoobastank"",""title"":""All About You"",""duration"":175,""date"":1463924363,""url"":""https:\/\/cs4-4v4.vk-cdn.net\/p6\/3101071f79e129.mp3?extra=7WxHRTVVD13XKxuCZTvnmKhaPAjb2C1Zo2oVTwCyzDjrLhaNEM3vss7aP7d8dGzyGb7ia_rXaBDoiADMfdUN0sUO8fCFtkOAViN2Q-kXBS3KzjXB2Vby03A2AWevOjvaZN3wYPI4YC8XOQ"",""lyrics_id"":1971239,""genre_id"":21},{""id"":456239037,""owner_id"":39773664,""artist"":""The Afters"",""title"":""Tonight"",""duration"":206,""date"":1463750195,""url"":""https:\/\/psv4.vk.me\/c4883\/u34682982\/audios\/a0f51ec8cc86.mp3?extra=83_ItSjhLuMJBlZahoOBzeq8Ka_zlBs_dbILp6xnH23gaQB6YCWrSyi5T9-pHhGWH4cn4vUgejdK1cdGH0Y3KixQG0mnIJNd0O0w9mZ83GjhBWBgHFUwRBtbIG9ju1GoSSrn7OrynSq-Dw"",""genre_id"":1},{""id"":456239036,""owner_id"":39773664,""artist"":""Tree Days Grace"",""title"":""Wake Up"",""duration"":206,""date"":1463747126,""url"":""https:\/\/psv4.vk.me\/c1057\/u6103033\/audios\/f4dbef3b39c4.mp3?extra=YyW8_BfwPBE7rPdyRCoq1sqNmYkwZwsrGmay-BcM6SOuXEku88KGrZt-wU3XT8c08huakYMpNQlFkGCQxPrfnQn2ANiwA6k0Qh77CTQd3yaAHc_s-ZgZ_aKnCpiM7G2eqPRFX50Lak8Ufw"",""lyrics_id"":122261,""genre_id"":21,""no_search"":1},{""id"":456239034,""owner_id"":39773664,""artist"":""Sia ♥"",""title"":""Unstoppable"",""duration"":218,""date"":1462709700,""url"":""https:\/\/cs4-2v4.vk-cdn.net\/p21\/acdd7461b70577.mp3?extra=QyXy8zoFxVl-WtdyD8KIyFyNGE2DmE2hFiG7vGZdQaCkCZdMx_SjhlHpVQOG1BI2zyXiTwKM_E8pjJlIT1wDjErQC6CEpHqMEJrmysJFQIu8whWRChNeoNvzrlMl16xQZuY1rSQE5HtQcA"",""lyrics_id"":331083609,""genre_id"":2},{""id"":456239030,""owner_id"":39773664,""artist"":""Die Antword"",""title"":""baby's on fire"",""duration"":168,""date"":1460393248,""url"":""https:\/\/cs4-4v4.vk-cdn.net\/p3\/fa50bd4301c359.mp3?extra=ErvXKFGmOAOpCHc_oRG7Ig8Rk82TidrmDfoFzKH1lMRGEChHQf8woI67XlXQTyJo7zXDJAYtJNTXfDoB9JMCA_JiYLFGRudhpL7w9CVPGA7s51sI7PXiNHD3ZrQq8Wws_m9tGBD-TzbZMg"",""genre_id"":3},{""id"":456239029,""owner_id"":39773664,""artist"":""DJ Selski"",""title"":""Хит лета 2011"",""duration"":227,""date"":1459534981,""url"":""https:\/\/cs4-4v4.vk-cdn.net\/p11\/75991975ef2a70.mp3?extra=2RSs616ZWJl0vg55-HW-H8LPnBJBM9cugpRKYWypLz2RxzbHdVLIjzNdn2eTreW5jDLLnXZKN4yKeNdR6EPcgE3bO__TMXwmFh3YmiCjzbpmeG98Xtt-VwGoQT0sll31dpO_XP2FLE62ng"",""genre_id"":2},{""id"":456239027,""owner_id"":39773664,""artist"":""клуб рай 2011"",""title"":""new mix"",""duration"":229,""date"":1459534457,""url"":""https:\/\/psv4.vk.me\/c4874\/u29924270\/audios\/ac0e46c1e536.mp3?extra=N1htdcv720ltaQchztRFxi2NY4NO67von3LLqX2j9_mmFc8tTVybrAN2k-L58LsO4JnXIxmp3QJpJcKAty9Jdj7h5uEZjTskSGfmha2ccf3mLfgFA5KFkX6qcvs0sOhDo8Z1VcnaQitqvA"",""genre_id"":18}]}}";

	}
}