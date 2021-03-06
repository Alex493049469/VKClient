﻿using Core;
using Core.Command;
using VK.ViewModel.Main;

namespace VK.ViewModel
{
	public class PaneViewModel : BaseViewModel
	{
		//Имя вкладки (Друзья, Моя Страница, переписка с Васей и тд.)
		private string _title = null;
		public string Title
		{
			get { return _title; }
			set
			{
				if (_title != value)
				{
					_title = value;
					RaisePropertyChanged();
				}
			}
		}

		public RelayCommand CloseTabCommand { get; private set; }

		public PaneViewModel()
	    {
			CloseTabCommand = new RelayCommand(CloseTab);
	    }

		#region Закрытие вкладки

		public virtual void CloseTab()
		{
			MainViewModel.This.Close(this);
		}
		#endregion
	}
}
