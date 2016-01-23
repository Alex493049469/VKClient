using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VKAPI;
using VKAPI.Model.LongPullModel;

namespace VK.Services
{
	public class VkNotifiedService
	{
		private VkApi _vk = new VkApi();

		private string Server;
		private int Ts;
		private string Key;

		public VkNotifiedService()
		{
			LongPool();
		}

		public async void LongPool(Updates updates = null)
		{
			if (updates == null)
			{
				var longPull = await _vk.Messages.GetLongPollServerAsync();
				Server = longPull.response.server;
				Ts = longPull.response.ts;
				Key = longPull.response.key;
			}
			else
			{
				Ts = updates.ts;
			}
			
			var reqGet = WebRequest.Create(@"http://" +Server + "?act=a_check&key=" + Key + "&ts=" + Ts + "&wait=25&mode=2");
			var resp = await reqGet.GetResponseAsync();
			var stream = resp.GetResponseStream();

			var sr = new StreamReader(stream);
			var str = sr.ReadToEnd();

			var updateModel = JsonConvert.DeserializeObject<Updates>(str);

			LongPool(updateModel);
		}



		public class Updates
		{
			public int ts { get; set; }
			public List<List<object>> updates { get; set; }
		}
		
	}
}
