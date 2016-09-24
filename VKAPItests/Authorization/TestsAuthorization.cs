using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VKAPI.Authorization;


namespace VKAPItests.Authorization
{
	[TestClass]
	public class TestsAuthorization
	{

		[TestMethod]
		public void TestMakeAuthUrl()
		{
			string resultUrl = @"https://oauth.vk.com/authorize?client_id=2013444&scope=notify,friends,photos,audio,video,docs,notes,pages,status,offers,questions,wall,groups,messages,email,notifications,stats,ads,market,offline,nohttps&redirect_uri=http://oauth.vk.com/blank.html&display=popup&response_type=token&v=5.53";
			var auth = new Auth(2013444);
			var str = auth.MakeAuthUrl();
			Assert.AreEqual(resultUrl, str);
		}

		[TestMethod]
		public void TestMakeAuthUrl2()
		{
			string resultUrl = @"https://oauth.vk.com/authorize?client_id=2013444&scope=pages,audio,friends&redirect_uri=http://oauth.vk.com/blank.html&display=page&response_type=token&v=5.53";
			var auth = new Auth(2013444, new List<Auth.Scope>() { Auth.Scope.pages, Auth.Scope.audio, Auth.Scope.friends }, Auth.Display.page, "http://oauth.vk.com/blank.html");
			var str = auth.MakeAuthUrl();
			Assert.AreEqual(resultUrl, str);
		}
	}
}
