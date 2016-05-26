using System.Windows;
using System.Windows.Controls;
using Core;
using MahApps.Metro.Controls;

namespace VK.ViewModel.Main
{
	public class FlyoutViewModel : BaseViewModel
	{
		public string Header { get; set; }
		public Visibility Visible { get; set; } = Visibility.Hidden;
		public Position Position { get; set; }
		public bool IsModal { get; set; }
		public bool IsOpen { get; set; }
		public ContentControl CustomView { get; set; }

		public void Hide()
		{
			IsOpen = false;
		}

		public void Show()
		{
			IsOpen = true;
		}

	}
}
