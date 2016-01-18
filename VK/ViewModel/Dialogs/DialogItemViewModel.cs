using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using VKAPI.Model.DialogsModel;

namespace VK.ViewModel.Dialogs
{
	 class DialogItemViewModel : BaseViewModel
	{
		public string Title { get; set; }

		public string Body { get; set; }

		public List<int> ChatActive { get; set; }

		public int ReadState { get; set; }
		public int Date { get; set; }

		public string GroupPhoto50 { get; set; }
		public string GroupPhoto100 { get; set; }
		public string GroupPhoto200 { get; set; }

		//ид с кем переписка(есть только если переписка с 1ц)
		public int UserId { get; set; }
		public string UserIdPhoto { get; set; }

		public string UserOnePhoto { get; set; }

		public string UserTwoPhoto { get; set; }

		public string UserThreePhoto { get; set; }

		public string UserFourPhoto { get; set; }

		public int? UserCount { get; set; }

		public int Out { get; set; }

		public string LastMessageUserName { get; set; }
		public int? ChatId { get; set; }

		public List<Attachment> Attachment { get; set; }
	}
}
