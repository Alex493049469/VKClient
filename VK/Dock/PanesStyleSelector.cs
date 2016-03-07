using System.Windows.Controls;
using System.Windows;
using VK.ViewModel.Audios;
using VK.ViewModel.Dialogs;
using VK.ViewModel.Friends;
using VK.ViewModel.Messages;
using VK.ViewModel.Page;

namespace AvalonDock.MVVMTestApp
{
    class PanesStyleSelector : StyleSelector
    {

        public Style AudioStyle
        {
            get;
            set;
        }

		public Style DialogStyle
		{
			get;
			set;
		}

		public Style FriendsStyle
		{
			get;
			set;
		}

		public Style MessagesStyle
		{
			get;
			set;
		}

		public Style PageStyle
		{
			get;
			set;
		}


        public override System.Windows.Style SelectStyle(object item, System.Windows.DependencyObject container)
        {

			if (item is AudioListViewModel)
				return AudioStyle;

			if (item is DialogListViewModel)
				return DialogStyle;

			if (item is FriendsListViewModel)
				return FriendsStyle;

			if (item is MessageListViewModel)
				return MessagesStyle;

			if (item is PageViewModel)
				return PageStyle;

            return base.SelectStyle(item, container);
        }
    }
}
