using Core;
using VK.ViewModel.AuthorizedUser;
using VK.ViewModel.MainMenu;

namespace VK.ViewModel.Main
{
	class MainViewModel : BaseViewModel
	{
		//View models
		private BaseViewModel _authorizedUserViewModel;
		private BaseViewModel _mainMenuViewModel;

		//Binding Property
		private object _objectContent;
		public object ContentPanel
		{
			get { return _objectContent; }
			set
			{
				_objectContent = value;
				RaisePropertyChanged();
			}
		}

		public BaseViewModel AuthorizedUserViewModel
		{
			get { return _authorizedUserViewModel; }
			set
			{
				_authorizedUserViewModel = value;
				RaisePropertyChanged();
			}
		}

		public BaseViewModel MainMenuViewModel
		{
			get { return _mainMenuViewModel; }
			set
			{
				_mainMenuViewModel = value;
				RaisePropertyChanged();
			}
		}

		public MainViewModel()
		{
			MainMenuViewModel = new MainMenuViewModel(this);
			AuthorizedUserViewModel = new AuthorizedUserViewModel();
		}

	}
}
