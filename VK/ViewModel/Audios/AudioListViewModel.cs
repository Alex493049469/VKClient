using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using Core;
using NAudio.Gui;
using NAudio.Wave;
using VKAPI;
using VKAPI.Core;
using VKAPI.Model.AudioModel;

namespace VK.ViewModel.Audios
{
    class AudioListViewModel : BaseViewModel
    {
        //Модель данных аудиозаписей
        private AudioModel _audioModel;
        //для доступа к данным песен
        VkAudio vkaudio = new VkAudio();
        //ViewModel Аудиозапсей
        private ObservableCollection<AudioItemViewModel> _audioItemsViewModel;
        //одиночка - для проигрывания аудио в фоне
        // private AudioSingleton _audioSingleton = AudioSingleton.Instance;
        private AudioPlayer _audioPlayer = new AudioPlayer();
        //выделенная в данный момент позиция
        private AudioItemViewModel _item;
        private AudioItemViewModel _itemPlaying;
        //строка для поиска
        private string _searchString;

        public AudioListViewModel()
        {
            LoadAudio();
            AudioPlayer.OnTrackEnd += NextAudioPlay;
        }

        /// <summary>
        /// Загрузка моих аудиозаписей
        /// </summary>
        public async void LoadAudio()
        {
            AudioItemsViewModel = null;
            ObservableCollection<AudioItemViewModel> _item = new ObservableCollection<AudioItemViewModel>();
            _audioModel = await vkaudio.GetAsync();
            foreach (var item in _audioModel.response.items)
            {
                AudioItemViewModel itemAudio = new AudioItemViewModel() {Item = item};
                itemAudio.IsMyItem = true;
                _item.Add(itemAudio);
            }
            AudioItemsViewModel = _item;
        }

        /// <summary>
        /// ищет проигрываемую аудиозапись и выделяет ее цветом
        /// </summary>
        private void FingPlayingAudio()
        {
            for (int i = 0; i < AudioItemsViewModel.Count; i++)
            {
                if (AudioItemsViewModel[i].Url == AudioPlayer.LastPlayed)
                {
                    AudioItemsViewModel[i].IsPlay = true;
                    ItemPlaying = AudioItemsViewModel[i];
                }
                else
                {
                    AudioItemsViewModel[i].IsPlay = false;
                }
            }
        }

        #region Свойства
       
        public string SearchString
        {
            get { return _searchString; }
            set
            {
                _searchString = value;
                OnPropertyChanged();
            }
        }

        public AudioItemViewModel ItemPlaying
        {
            get { return _itemPlaying; }
            set
            {
                _itemPlaying = value;
                OnPropertyChanged();
            }
        }
        
        public ObservableCollection<AudioItemViewModel> AudioItemsViewModel
        {
            get { return _audioItemsViewModel; }
            set
            {
                if (_audioItemsViewModel == value)
                    return;

                _audioItemsViewModel = value;

                OnPropertyChanged();
            }
        }

        public AudioPlayer AudioPlayer
        {
            get { return _audioPlayer; }
            set
            {
                _audioPlayer = value;
                OnPropertyChanged();
            }
        }

        public AudioItemViewModel ItemSelected
        {
            get { return _item; }
            set
            {
                _item = value;
                OnPropertyChanged();
            }
        }
        #endregion;

        #region Плей
        private AsyncDelegateCommand _playAudio;
        public ICommand PlayAudioButtonClick
        {
            get
            {
                if (_playAudio == null)
                {
                    _playAudio = new AsyncDelegateCommand(PlayAudio);
                }
                return _playAudio;
            }
        }

        private async Task PlayAudio(object o)
        {
            if (ItemSelected != null)
            {
                AudioPlayer.Play(ItemSelected.Url);
                FingPlayingAudio();
            }
        }
        #endregion;

        #region Пауза
        private AsyncDelegateCommand _pauseAudio;
        public ICommand PauseAudioButtonClick
        {
            get
            {
                if (_pauseAudio == null)
                {
                    _pauseAudio = new AsyncDelegateCommand(PauseAudio);
                }
                return _pauseAudio;
            }
        }

        private async Task PauseAudio(object o)
        {
            AudioPlayer.Pause();
        }
        #endregion;

        #region Стоп
        private AsyncDelegateCommand _stopAudio;
        public ICommand StopAudioButtonClick
        {
            get
            {
                if (_stopAudio == null)
                {
                    _stopAudio = new AsyncDelegateCommand(StopAudio);
                }
                return _stopAudio;
            }
        }

        private async Task StopAudio(object o)
        {
            AudioPlayer.Stop();
        }
        #endregion;

        #region Отвечает за  переключение песен

        public void NextAudioPlay()
        {
            for (int i = 0; i < AudioItemsViewModel.Count; i++)
            {
                if (AudioItemsViewModel[i].IsPlay == true)
                {
                    AudioItemsViewModel[i].IsPlay = false;
                    
                    //проверяем не достигли ли конца списка
                    if (i + 1 < AudioItemsViewModel.Count)
                    {
                        AudioItemsViewModel[i + 1].IsPlay = true;
                        AudioPlayer.Play(AudioItemsViewModel[i + 1].Url);
                        ItemPlaying = AudioItemsViewModel[i + 1];
                        break;
                    }
                    else
                    {
                        i = -1;
                        AudioItemsViewModel[i + 1].IsPlay = true;
                        AudioPlayer.Play(AudioItemsViewModel[i + 1].Url);
                        ItemPlaying = AudioItemsViewModel[i + 1];
                        break;
                    }
                  
                }
            }
        }
        #endregion;

        #region Клик по кнопке поиск аудиозаписей
        private AsyncDelegateCommand _searchAudio;
        public ICommand SearchAudioButtonClick
        {
            get
            {
                if (_searchAudio == null)
                {
                    _searchAudio = new AsyncDelegateCommand(SearchAudio);
                }
                return _searchAudio;
            }
        }

        private async Task SearchAudio(object o)
        {
            AudioItemsViewModel = null;
            ObservableCollection<AudioItemViewModel> _item = new ObservableCollection<AudioItemViewModel>();
            _audioModel = await vkaudio.SearchAsync(SearchString);
            foreach (var item in _audioModel.response.items)
            {
                AudioItemViewModel itemAudio = new AudioItemViewModel() { Item = item };
                itemAudio.IsMyItem = false;
                _item.Add(itemAudio);
                //ConnectItemViewModel(itemAudio);
            }
            AudioItemsViewModel = _item;
        }
        #endregion;

        #region Клик по кнопке мои аудиозаписи
        private AsyncDelegateCommand _myAudioAudio;
        public ICommand MyAudioButtonClick
        {
            get
            {
                if (_myAudioAudio == null)
                {
                    _myAudioAudio = new AsyncDelegateCommand(MyAudio);
                }
                return _myAudioAudio;
            }
        }

        private async Task MyAudio(object o)
        {
            LoadAudio();
        }
        #endregion;

        #region Добавление аудиозаписи
        private AsyncDelegateCommand _addAudio;
        public ICommand AddAudioButtonClick
        {
            get
            {
                if (_addAudio == null)
                {
                    _addAudio = new AsyncDelegateCommand(AddAudio);
                }
                return _addAudio;
            }
        }

        private async Task AddAudio(object o)
        {
            for (int i = 0; i < AudioItemsViewModel.Count; i++)
            {
                if (AudioItemsViewModel[i].FullNameAudio == o.ToString())
                {
                    await vkaudio.AddAsync(AudioItemsViewModel[i].Item.id, AudioItemsViewModel[i].Item.owner_id);
                    break;
                }
            }
        }
        #endregion;

        #region Удаление аудиозаписи
        private AsyncDelegateCommand _deleteAudio;
        public ICommand DeleteAudioButtonClick
        {
            get
            {
                if (_deleteAudio == null)
                {
                    _deleteAudio = new AsyncDelegateCommand(DeleteAudio);
                }
                return _deleteAudio;
            }
        }
        private async Task DeleteAudio(object o)
        {
            for (int i = 0; i < AudioItemsViewModel.Count; i++)
            {
                if (AudioItemsViewModel[i].FullNameAudio == o.ToString())
                {
                    await vkaudio.DeleteAsync(AudioItemsViewModel[i].Item.id, AudioItemsViewModel[i].Item.owner_id);
                    AudioItemsViewModel.Remove(AudioItemsViewModel[i]);
                    break;
                }
            }

        }
        #endregion

    }
}
