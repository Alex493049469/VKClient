using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Core;
using VKAPI;
using VKAPI.Model.FriendsModel;

namespace VK.ViewModel.Friends
{
    class FriendsListViewModel : BaseViewModel
    {
        VkFriends vkFriends = new VkFriends();
        //модель данных друзей
        private FriendsModel _friendViewModel;
        //ViewModel друзей
        private ObservableCollection<FriendItemViewModel> _friendItemsViewModel;

        public ObservableCollection<FriendItemViewModel> FriendsItemsViewModel
        {
            get { return _friendItemsViewModel; }
            set
            {
                if (_friendItemsViewModel == value)
                    return;
                _friendItemsViewModel = value;

                OnPropertyChanged();
            }
        }
      
        public FriendsListViewModel()
        {
            LoadFriends();
        }

        /// <summary>
        /// Загрузка данных о друзьях
        /// </summary>
        public async  void LoadFriends()
        {
            FriendsItemsViewModel = null;
            ObservableCollection<FriendItemViewModel> _item = new ObservableCollection<FriendItemViewModel>();
            _friendViewModel = await vkFriends.GetAsync();
            foreach (var item in _friendViewModel.response.items)
            {
                FriendItemViewModel itemAudio = new FriendItemViewModel() { Item = item };
                _item.Add(itemAudio);
            }
            FriendsItemsViewModel = _item;
        }

    }
}
