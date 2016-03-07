using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKAPI.Model.LongPullModel
{
	public class Response
	{
		public string key { get; set; }
		public string server { get; set; }
		public int ts { get; set; }
		public int pts { get; set; }
	}

	public class LongPullModel
	{
		public Response response { get; set; }
	}
}
