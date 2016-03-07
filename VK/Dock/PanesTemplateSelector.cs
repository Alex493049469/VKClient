using System.Windows;
using System.Windows.Controls;
using VK.ViewModel.Audios;
using VK.ViewModel.Dialogs;
using VK.ViewModel.Friends;
using VK.ViewModel.Messages;
using VK.ViewModel.Page;
using Xceed.Wpf.AvalonDock.Layout;

namespace VK.Dock
{
    class PanesTemplateSelector : DataTemplateSelector
    {

        public DataTemplate AudioViewTemplate
        {
            get;
            set;
        }

		public DataTemplate DialogsViewTemplate
		{
			get;
			set;
		}

		public DataTemplate FriendsViewTemplate
		{
			get;
			set;
		}		
		
		public DataTemplate MessageViewTemplate
		{
			get;
			set;
		}

		public DataTemplate PageViewTemplate
		{
			get;
			set;
		}


        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {

	        if (item is AudioListViewModel)
		        return AudioViewTemplate;

			if (item is DialogListViewModel)
				return DialogsViewTemplate;
			
			if (item is FriendsListViewModel)
				return FriendsViewTemplate;

			if (item is MessageListViewModel)
				return MessageViewTemplate;

			if (item is PageViewModel)
		        return PageViewTemplate;

      

            return base.SelectTemplate(item, container);
        }
    }
}
