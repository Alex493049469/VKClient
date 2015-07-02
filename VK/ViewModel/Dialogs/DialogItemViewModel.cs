using System;
using System.Collections.Generic;
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
