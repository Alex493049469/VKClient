using Core;
using VK.ViewModel.AuthorizedUser;
using VK.ViewModel.MainMenu;

namespace VK.ViewModel.Main
{
	public class MainViewModel : BaseViewModel
	{
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

	}
}
