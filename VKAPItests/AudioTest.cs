using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VKAPI;
using VKAPI.Model.AudioModel;

namespace VKAPItests
{
	[TestClass]
	public class AudioTest
	{
		private readonly VkApi _vkApi = new VkApi(new RequestTest());

		[TestMethod]
		public void TestAudioGet()
		{
			AudioModel qwe =  _vkApi.Audio.Get();
			Assert.AreEqual(qwe.response.count, 604);
			Assert.AreEqual(qwe.response.items.Count, 10);
		}
	}
}
