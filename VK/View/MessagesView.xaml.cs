using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace VK.View
{
	/// <summary>
	/// Логика взаимодействия для MessagesView.xaml
	/// </summary>
	public partial class MessagesView : UserControl
	{
		
		public MessagesView()
		{
			InitializeComponent();
			
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
