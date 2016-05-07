using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKAPI.Model
{

	public class Response
	{
		public int lyrics_id { get; set; }
		public string text { get; set; }
	}

	public class LyricsModel
	{
		public Response response { get; set; }
	}
}

