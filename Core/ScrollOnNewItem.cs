using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Threading;

namespace Core
{
	public class ScrollOnNewItem : Behavior<ListBox>
	{
		DispatcherTimer timer = new DispatcherTimer();
		protected override void OnAttached()
		{
			AssociatedObject.Loaded += OnLoaded;
			AssociatedObject.Unloaded += OnUnLoaded;
		}

		protected override void OnDetaching()
		{
			AssociatedObject.Loaded -= OnLoaded;
			AssociatedObject.Unloaded -= OnUnLoaded;
		}

		private void OnLoaded(object sender, RoutedEventArgs e)
		{
			timer.Interval = TimeSpan.FromMilliseconds(100);
			timer.Start();
			timer.Tick += (s, args) =>
			{
				var incc = AssociatedObject.ItemsSource as INotifyCollectionChanged;
				if (incc == null) return;

				incc.CollectionChanged += OnCollectionChanged;
				timer.Stop();
			};
			
		}

		private void OnUnLoaded(object sender, RoutedEventArgs e)
		{
			var incc = AssociatedObject.ItemsSource as INotifyCollectionChanged;
			if (incc == null) return;

			incc.CollectionChanged -= OnCollectionChanged;
		}

		private int count;
		private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.Action == NotifyCollectionChangedAction.Reset)
			{
				int itemsCount = AssociatedObject.Items.Count;
				if (itemsCount == 0)
					return;

				var scrollViewer = GetDescendantByType(AssociatedObject, typeof(ScrollViewer)) as ScrollViewer;
				if (scrollViewer != null)
				{
					count++;
					AssociatedObject.UpdateLayout();
					scrollViewer.ScrollToEnd();

					var incc = AssociatedObject.ItemsSource as INotifyCollectionChanged;
					if (incc == null) return;
					if(count > 2)
					incc.CollectionChanged -= OnCollectionChanged;
				}
					
			}
		}

		public static Visual GetDescendantByType(Visual element, Type type)
		{
			if (element == null)
			{
				return null;
			}
			if (element.GetType() == type)
			{
				return element;
			}
			Visual foundElement = null;
			if (element is FrameworkElement)
			{
				(element as FrameworkElement).ApplyTemplate();
			}
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
			{
				Visual visual = VisualTreeHelper.GetChild(element, i) as Visual;
				foundElement = GetDescendantByType(visual, type);
				if (foundElement != null)
				{
					break;
				}
			}
			return foundElement;
		}
	}
}

