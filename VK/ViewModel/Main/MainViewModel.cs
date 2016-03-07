using System.Collections.ObjectModel;
using Core;

namespace VK.ViewModel.Main
{
	public class MainViewModel : BaseViewModel
	{
		//Коллекция вкладок
		public ObservableCollection<BaseViewModel> _files = new ObservableCollection<BaseViewModel>();
		ReadOnlyObservableCollection<BaseViewModel> _readonyFiles = null;
		public ReadOnlyObservableCollection<BaseViewModel> Files
		{
			get { return _readonyFiles ?? (_readonyFiles = new ReadOnlyObservableCollection<BaseViewModel>(_files)); }
		}

		private BaseViewModel _activeDocument = null;

		/// <summary>
		/// Активная viewModel
		/// </summary>
		public BaseViewModel ActiveDocument
		{
			get { return _activeDocument; }
			set
			{
				if (_activeDocument != value)
				{
					_activeDocument = value;
					RaisePropertyChanged();
				}
			}
		}

	}
}
