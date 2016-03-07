﻿/************************************************************************

   AvalonDock

   Copyright (C) 2007-2013 Xceed Software Inc.

   This program is provided to you under the terms of the New BSD
   License (BSD) as published at http://avalondock.codeplex.com/license 

   For more features, controls, and fast professional support,
   pick up AvalonDock in Extended WPF Toolkit Plus at http://xceed.com/wpf_toolkit

   Stay informed: follow @datagrid on Twitter or Like facebook.com/datagrids

  **********************************************************************/

using System;
using System.Windows.Data;
using VK.ViewModel.Audios;
using VK.ViewModel.Dialogs;
using VK.ViewModel.Friends;
using VK.ViewModel.Messages;
using VK.ViewModel.Page;

namespace VK.Converters
{
    class ActiveDocumentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
			if (value is AudioListViewModel)
				return value;

			if (value is DialogListViewModel)
				return value;

			if (value is FriendsListViewModel)
				return value;

			if (value is MessageListViewModel)
				return value;

			if (value is PageViewModel)
				return value;

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
			if (value is AudioListViewModel)
				return value;

			if (value is DialogListViewModel)
				return value;

			if (value is FriendsListViewModel)
				return value;

			if (value is MessageListViewModel)
				return value;

			if (value is PageViewModel)
				return value;

            return Binding.DoNothing;
        }
    }
}
