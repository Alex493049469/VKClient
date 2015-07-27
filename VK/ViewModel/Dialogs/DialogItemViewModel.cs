using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using VKAPI.Model.DialogsModel;

namespace VK.ViewModel.Dialogs
{
    class DialogItemViewModel : BaseViewModel
    {

        private Message _message;
        public Message Item
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get { return _message.title; }
            set
            {
                _message.title = value;
                OnPropertyChanged();
            }
        }

        public string Body
        {
            get { return _message.body; }
            set
            {
                _message.body = value;
                OnPropertyChanged();
            }
        }

        public string GroupPhotos
        {
            get { return _message.photo_100; }
            set
            {
                _message.photo_100 = value;
                OnPropertyChanged();
            }
        }

        //ид с кем переписка(есть только если переписка с 1ц)
        public int UserId
        {
            get { return _message.user_id; }
            set
            {
                _message.user_id = value;
                OnPropertyChanged();
            }
        }

        private string _userIdPhoto;
        public string UserIdPhoto
        {
            get { return _userIdPhoto; }
            set
            {
                _userIdPhoto = value;
                OnPropertyChanged();
            }
        }


        //public ObservableCollection<int> ChatActive
        //{
        //    get { return _message.chat_active; }
        //    set
        //    {
        //        _message.chat_active = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private ObservableCollection<string> _chatActivePhoto;
        //public ObservableCollection<string> ChatActivePhoto
        //{
        //    get { return _chatActivePhoto; }
        //    set
        //    {
        //        _chatActivePhoto = value;
        //        OnPropertyChanged();
        //    }
        //}

        public int UserCount
        {
            get { return _message.users_count; }
            set
            {
                _message.users_count = value;
                OnPropertyChanged();
            }
        }
    }
}
