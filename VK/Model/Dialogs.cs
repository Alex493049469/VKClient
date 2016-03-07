using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VK.Model
{
	class Dialogs
	{
		public int count { get; set; }
		public int unread_dialogs { get; set; }
		List<DialogItem> dialogs = new List<DialogItem>(); 
	}

	class DialogItem
	{
		public int unread { get; set; }
		List<Messages> mesages = new List<Messages>(); 
	}

	class Messages
	{
		
	}

}
