using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using MahApps.Metro.Controls.Dialogs;
using VK.ViewModel.Main;

namespace VK.Services
{
	public static class DialogService
	{
		private static readonly IDialogCoordinator DialogCoordinator = MahApps.Metro.Controls.Dialogs.DialogCoordinator.Instance;
		private static readonly BaseViewModel MainViewModel = ViewModel.Main.MainViewModel.This;

		public static async void ShowMessage(string title, string message)
		{
			await DialogCoordinator.ShowMessageAsync(MainViewModel, title, message);
		}

		public static Task<MessageDialogResult> ShowMessage(string title, string message, MessageDialogStyle style, MetroDialogSettings settings)
		{
			var result = DialogCoordinator.ShowMessageAsync(ViewModel.Main.MainViewModel.This, title, message, style, settings);
			return result;
		}


	}
}
