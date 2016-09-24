using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using VKAPI.Model.MessagesModel;

namespace VK.ViewModel.Messages
{
	public class MessageItemViewModel : BaseViewModel
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
		public List<Attachment> Attachments { get; set; }
		public string UserIdPhoto { get; set; }
		public string UserName { get; set; }
		public string GiftThumb_256 { get; set; }
		public string LastMessageUserName { get; set; }
		public string StickerPhoto_128 { get; set; }
		public string Photo { get; set; }
		public int PhotoWidth { get; set; }
		public int PhotoHeight { get; set; }
	}
}
