using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Core;
using MahApps.Metro.Controls;

namespace VK.ViewModel.Main
{
	public class FlyoutViewModel : BaseViewModel
	{
		public string Header { get; set; }
		public bool Visible { get; set; }
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
