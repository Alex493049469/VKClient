using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Core;
using VKAPI.Model.FriendsModel;

namespace VK.ViewModel.Friends
{
    class FriendItemViewModel : BaseViewModel
    {
        private Item _item;
        public Item Item
        {
            get { return _item; }
            set
            {
                _item = value;
                RaisePropertyChanged();
            }
        }


        public string FirstName
        {
            get { return _item.first_name; }
            set
            {
                _item.first_name = value;
                RaisePropertyChanged();
            }
        }

        public string LastName
        {
            get { return _item.last_name; }
            set
            {
                _item.last_name = value;
                RaisePropertyChanged();
            }
        }

        public int Online
        {
            get { return _item.online; }
            set
            {
                _item.online = value;
                RaisePropertyChanged();
            }
        }

        public string Photo100
        {
            get { return _item.photo_100; }
            set
            {
                _item.photo_100 = value;
                RaisePropertyChanged();
            }
        }

        private BitmapImage _image;
        public  BitmapImage Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                RaisePropertyChanged();
            }
        }


    }
}
