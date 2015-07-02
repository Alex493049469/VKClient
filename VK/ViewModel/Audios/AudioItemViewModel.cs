using Core;
using VKAPI.Model.AudioModel;

namespace VK.ViewModel.Audios
{
    class AudioItemViewModel : BaseViewModel
    {

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
