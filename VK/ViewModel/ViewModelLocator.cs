/*
  In App.xaml:
  <Application.Resources>
	  <vm:ViewModelLocator xmlns:vm="clr-namespace:VK"
						   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using VK.ViewModel.Audios;
using VK.ViewModel.AuthorizedUser;
using VK.ViewModel.Dialogs;
using VK.ViewModel.Friends;
using VK.ViewModel.Main;
using VK.ViewModel.MainMenu;
using VK.ViewModel.Page;


namespace VK.ViewModel
{
	/// <summary>
	/// This class contains static references to all the view models in the
	/// application and provides an entry point for the bindings.
	/// </summary>
	public class ViewModelLocator
	{
		/// <summary>
		/// Initializes a new instance of the ViewModelLocator class.
		/// </summary>
		public ViewModelLocator()
		{
			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

			////if (ViewModelBase.IsInDesignModeStatic)
			////{
			////    // Create design time view services and models
			////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
			////}
			////else
			////{
			////    // Create run time view services and models
			////    SimpleIoc.Default.Register<IDataService, DataService>();
			////}

			SimpleIoc.Default.Register<MainViewModel>();
			SimpleIoc.Default.Register<DialogListViewModel>();
			SimpleIoc.Default.Register<AuthorizedUserViewModel>();
			SimpleIoc.Default.Register<MainMenuViewModel>();
			SimpleIoc.Default.Register<AudioListViewModel>();
			SimpleIoc.Default.Register<PageViewModel>();
			SimpleIoc.Default.Register<FriendsListViewModel>();
		}

		public static MainViewModel Main
		{
			get
			{
				return ServiceLocator.Current.GetInstance<MainViewModel>();
			}
		}

		public DialogListViewModel Dialog
		{
			get
			{
				return ServiceLocator.Current.GetInstance<DialogListViewModel>();
			}
		}

		public AuthorizedUserViewModel AuthorizeUser
		{
			get
			{
				return ServiceLocator.Current.GetInstance<AuthorizedUserViewModel>();
			}
		}

		public static MainMenuViewModel MainMenu
		{
			get
			{
				return ServiceLocator.Current.GetInstance<MainMenuViewModel>();
			}
		}

		public AudioListViewModel AudioList
		{
			get
			{
				return ServiceLocator.Current.GetInstance<AudioListViewModel>();
			}
		}

		public PageViewModel Page
		{
			get
			{
				return ServiceLocator.Current.GetInstance<PageViewModel>();
			}
		}

		public FriendsListViewModel FriendsList
		{
			get
			{
				return ServiceLocator.Current.GetInstance<FriendsListViewModel>();
			}
		}
		
		public static void Cleanup()
		{
			// TODO Clear the ViewModels
		}
	}
}