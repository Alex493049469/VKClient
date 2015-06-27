using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Core;
using VKAPI.Core;
using VKAPI.Model.AudioModel;

namespace VK.ViewModel.Audio
{
    class AudioItemViewModel : BaseViewModel
    {
        //#region для удаления аудиозаписей
        //public event Action<AudioItemViewModel> ItemEvent = null;

        //private AsyncDelegateCommand _itemAction;
        //public ICommand AddandDeleteItemButtonClick
        //{
        //    get
        //    {
        //        if (_itemAction == null)
        //        {
        //            _itemAction = new AsyncDelegateCommand(AddandDeleteItem);
        //        }
        //        return _itemAction;
        //    }
        //}
        //private async Task AddandDeleteItem(object o)
        //{
        //    if (ItemEvent != null)
        //        ItemEvent(this);
        //}
        //#endregion

        private Item _item;
        public Item Item
        {
            get { return _item; }
            set
            {
                _item = value;
                OnPropertyChanged();
            }
        }

        public string Artist
        {
            get { return _item.artist; }
            set
            {
                _item.artist = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get { return _item.title; }
            set
            {
                _item.title = value;
                OnPropertyChanged();
            }
        }

        public int Duration
        {
            get { return _item.duration; }
            set
            {
                _item.duration = value;
                OnPropertyChanged();
            }
        }

        public string Url
        {
            get { return _item.url; }
            set
            {
                _item.url = value;
                OnPropertyChanged();
            }
        }

        public string FullNameAudio
        {
            get { return _item.artist + " - " + _item.title; }
           
        }

        //проигрывается ли в данный момент
        private bool _isPlay;
        public bool IsPlay
        {
            get { return _isPlay; }
            set
            {
                _isPlay = value;
                OnPropertyChanged();
            }
        }

        //флаг мои аудиозаписи или нет
        private bool _isMyItem;
        public bool IsMyItem
        {
            get { return _isMyItem; }
            set
            {
                _isMyItem = value;
                OnPropertyChanged();
            }
        }


    }
}
