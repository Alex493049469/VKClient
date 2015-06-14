using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Core;
using VK.Annotations;
using VKAPI;
using VKAPI.Model.FriendsModel;

namespace VK.ViewModel
{
    class FriendsListViewModel : BaseViewModel
    {
        private FriendsModel _friendViewModel;

        public FriendsModel Friends
        {
            get { return _friendViewModel; }
            set
            {
                _friendViewModel = value;
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
            Friends = await VkFriends.GetAsync();
        }

    }
}
