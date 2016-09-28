using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VK.ViewModel.Dialogs;
using VK.ViewModel.Messages;

namespace VK.DataTemplateSelectors
{
	class MessageDataTemplateSelector : System.Windows.Controls.DataTemplateSelector
	{
		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			FrameworkElement element = container as FrameworkElement;

			if (element != null && item != null && item is MessageItemViewModel)
			{
				MessageItemViewModel message = item as MessageItemViewModel;

				if (message.Attachments == null)
				{
					return element.FindResource("MessagesTemplate") as DataTemplate;
				}

				if (message.Attachments[0].type == "gift")
				{
					return element.FindResource("MessagesGift") as DataTemplate;
				}
				if (message.Attachments[0].type == "sticker")
				{
					return element.FindResource("MessagesSticker") as DataTemplate;
				}
				if (message.Attachments[0].type == "photo")
				{
					return element.FindResource("MessagesPhoto") as DataTemplate;
				}
				if (message.Attachments[0].type == "video")
				{
					return element.FindResource("MessagesVideo") as DataTemplate;
				}

			}

			return null;
		}
	}
}
