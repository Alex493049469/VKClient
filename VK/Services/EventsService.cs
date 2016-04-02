using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using VKAPI;
using VKAPI.Model.LongPullMessageModel;
using VKAPI.Model.LongPullModel;

namespace VK.Services
{
	/// <summary>
	/// Сервис оповещает о наборе текста, новых сообщениях и тд.
	/// </summary>
	public class EventsService
	{
		private VkApi _vk = new VkApi();

		LongPullModel lpm;

		int ts;
		int pts;

		//событие на которое надо подписаться для оповещения что пришло новое сообщение
		public delegate void MethodContainer(LongPullMessageModel message);
		public event MethodContainer NewMessage;

		public async void LongPool(Updates updates = null)
		{
			if (updates == null)
			{
				lpm = await _vk.Messages.GetLongPollServerAsync();
			}
			else
			{
				lpm.response.ts = updates.ts;
				lpm.response.pts = updates.pts;
			}

			var reqGet = WebRequest.Create(@"http://" + lpm.response.server + "?act=a_check&key=" + lpm.response.key + "&ts=" + lpm.response.ts + "&wait=25&mode=32");
			var resp = await reqGet.GetResponseAsync();
			var stream = resp.GetResponseStream();

			var sr = new StreamReader(stream);
			var str = sr.ReadToEnd();

			Updates updateModel = JsonConvert.DeserializeObject<Updates>(str);

			if (updates != null)
			{
				ts = updates.ts;
				pts = updates.pts;
			}
			else
			{
				ts = lpm.response.ts;
				pts = lpm.response.pts;
			}

			DetectTypeEvent(updateModel);

			LongPool(updateModel);
		}

		private async void DetectTypeEvent(Updates updates)
		{
			foreach (var update in updates.updates)
			{
				int codeTypeEvent = Convert.ToInt32(update[0]);

				if (codeTypeEvent == 61)
				{
					//MessageBox.Show("Петрович набирает сообщение");
				}

				//4,$message_id,$flags,$from_id,$timestamp,$subject,$text,$attachments
				if (codeTypeEvent == 4)
				{
					//получаем новые сообщения
					LongPullMessageModel longPullMessageModel = await _vk.Messages.GetLongPollHistoryAsynk(ts, pts);

					var message = new MessageNew
					{
						TypeEvent = Convert.ToInt32(update[0]),
						MessageId = Convert.ToInt32(update[1]),
						Flags = Convert.ToInt32(update[2]),
						FromId = Convert.ToInt32(update[3]),
						Timestamp = Convert.ToInt32(update[4]),
						Subject = Convert.ToString(update[5]),
						Text = Convert.ToString(update[6])
					};

					NewMessageWindow newMessage = new NewMessageWindow(longPullMessageModel);
					newMessage.Show();

					if (NewMessage != null) NewMessage(longPullMessageModel);
				}
			}
		}

		public class MessageNew
		{
			public int TypeEvent { get; set; }
			public int MessageId { get; set; }
			public int Flags { get; set; }
			public int FromId { get; set; }
			public int Timestamp { get; set; }
			public string Subject { get; set; }
			public string Text { get; set; }

		}

		public class Updates
		{
			public int ts { get; set; }
			public int pts { get; set; }
			public List<List<object>> updates { get; set; }
		}
		
	}

}
