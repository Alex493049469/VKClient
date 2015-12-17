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
        VkFriends _vkFriends = new VkFriends();
        //модель данных друзей
        private FriendsModel _friendViewModel;
        //ViewModel друзей
        private ObservableCollection<FriendItemViewModel> _friendItemsViewModel;
        //выделенная в данный момент позиция
        private FriendItemViewModel _itemSelected;

        public ObservableCollection<FriendItemViewModel> FriendsItemsViewModel
        {
            get { return _friendItemsViewModel; }
            set
            {
                if (_friendItemsViewModel == value)
                    return;
                _friendItemsViewModel = value;

                RaisePropertyChanged();
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
            _friendViewModel = await _vkFriends.GetAsync();
            foreach (var item in _friendViewModel.response.items)
            {
                FriendItemViewModel itemAudio = new FriendItemViewModel() { Item = item };
                _item.Add(itemAudio);
            }
            FriendsItemsViewModel = _item;
        }

        public FriendItemViewModel ItemSelected
        {
            get { return _itemSelected; }
            set
            {
                _itemSelected = value;
                RaisePropertyChanged();
            }
        }

    }
}
