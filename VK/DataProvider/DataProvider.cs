using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VK.ViewModel.Dialogs;
using VKAPI.Model.DialogsModel;

namespace VK.DataProvider
{
	public static class DataProvider
	{
		private static ObjectCache _cache = MemoryCache.Default;

		public static void SetMessage(string name, ObservableCollection<DialogItemViewModel> model)
		{
			//string fileContents = _cache["filecontents"] as string;

			CacheItemPolicy policy = new CacheItemPolicy();

			
			_cache.Set(name, model, policy);
			

			var fileContents = _cache[name] as ObservableCollection<DialogItemViewModel>;
			//JsonConvert.DeserializeObject<ObjectCache>(str);

			var qwe = JsonConvert.SerializeObject(fileContents);
			//if (fileContents == null)
			//{
			//	CacheItemPolicy policy = new CacheItemPolicy();
			//	policy.AbsoluteExpiration =
			//		DateTimeOffset.Now.AddSeconds(10.0);

			//	List<string> filePaths = new List<string>();
			//	filePaths.Add("c:\\cache\\cacheText.txt");

			//	policy.ChangeMonitors.Add(new
			//		HostFileChangeMonitor(filePaths));

			//	// Fetch the file contents.
			//	//fileContents = File.ReadAllText("c:\\cache\\cacheText.txt") + "\n" + DateTime.Now.ToString();

			//	_cache.Set("filecontents", fileContents, policy);

			//}
			//MessageBox.Show(fileContents);
		}


	}
}
