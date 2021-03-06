﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Core.BaseControl
{
	[TemplatePart(Name = "PART_SCROLLVIEWER", Type = typeof(ScrollViewer))]
	public class LazyLoadListBox : ListBox
	{
		static LazyLoadListBox()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(LazyLoadListBox),
				new FrameworkPropertyMetadata(typeof(LazyLoadListBox)));
		}

		public static readonly DependencyProperty IsLazyLoadProperty = DependencyProperty.Register(
			"IsLazyLoad", typeof(bool), typeof(LazyLoadListBox), new PropertyMetadata(default(bool)));

		public static readonly DependencyProperty LoadCommandProperty = DependencyProperty.Register(
			"LoadCommand", typeof(ICommand), typeof(LazyLoadListBox), new PropertyMetadata(default(ICommand)));

		public static readonly DependencyProperty IsLoadingUpProperty = DependencyProperty.Register(
			"IsLoadingUp", typeof(bool), typeof(LazyLoadListBox), new PropertyMetadata(false));

		public bool IsLoadingUp
		{
			get { return (bool)GetValue(IsLoadingUpProperty); }
			set { SetValue(IsLoadingUpProperty, value); }
		}

		/// <summary>
		/// Команда выполняемая для подгрузки данных
		/// </summary>
		public ICommand LoadCommand
		{
			get { return (ICommand)GetValue(LoadCommandProperty); }
			set { SetValue(LoadCommandProperty, value); }
		}

		/// <summary>
		/// Если true то использовать линивую подгрузку
		/// </summary>
		public bool IsLazyLoad
		{
			get { return (bool)GetValue(IsLazyLoadProperty); }
			set { SetValue(IsLazyLoadProperty, value); }
		}

		#region Overrides of FrameworkElement

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			var scrollViewer = GetTemplateChild("PART_SCROLLVIEWER") as ScrollViewer;
			if (scrollViewer != null)
			{
				//scrollViewer.CanContentScroll = true;
				scrollViewer.ScrollChanged += scrollViewer_ScrollChanged;
			}
		}

		void scrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			if (!IsLazyLoad)
				return;

			if (LoadCommand == null)
				return;

			var scrollViewer = sender as ScrollViewer;
			if (scrollViewer == null)
				return;

			if (IsLoadingUp != true && scrollViewer.ScrollableHeight != 0 && scrollViewer.ScrollableHeight == scrollViewer.VerticalOffset)
			{
				LoadCommand.Execute(null);
				//this.UpdateLayout();
				//foreach (var VARIABLE in ItemsSource)
				//{
					
				//}
				//this.ScrollIntoView(item);
			}
			if (IsLoadingUp && scrollViewer.VerticalOffset == 0)
			{
				LoadCommand.Execute(null);
			}
			
		}

		#endregion
	}
}
