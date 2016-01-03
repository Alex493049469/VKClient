using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using VKAPI.Model.MessagesModel;

namespace VK.ViewModel.Messages
{
	class MessageItemViewModel : BaseViewModel
	{
		public int Id { get; set; }
		public string Body { get; set; }
		public int UserId { get; set; }
		public int FromId { get; set; }
		public int Date { get; set; }
		public int ReadState { get; set; }
		public int Out { get; set; }
		public int ChatId { get; set; }
		public List<FwdMessage> FwdMessages { get; set; }
		public int? Emoji { get; set; }
		public List<Attachment2> Attachments { get; set; }
		public string UserIdPhoto { get; set; }
		public string UserName { get; set; }
	}
}
