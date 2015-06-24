using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Core;
using VKAPI;
using VKAPI.Core;
using VKAPI.Model.AudioModel;

namespace VK.ViewModel.Audio
{
    class AudioListViewModel : BaseViewModel
    {
        //Модель данных аудиозаписей
        private AudioModel _audioViewModel;
        //для доступа к данным песен
        VkAudio vkaudio = new VkAudio();
        //ViewModel Аудиозапсей
        private ObservableCollection<AudioItemViewModel> _audioItemsViewModel;
        //одиночка - для проигрывания аудио в фоне
        private AudioSingleton _audioSingleton = AudioSingleton.Instance;

        public AudioListViewModel()
        {
            LoadAudio();
        }

        //для загрузки аудиозаписей друзей и др
        //public AudioListViewModel(int owner_id)
        //{
        //    LoadAudio();
        //}

        /// <summary>
        /// Загрузка моих аудиозаписей
        /// </summary>
        public async void LoadAudio()
        {
            AudioItemsViewModel = null;
            ObservableCollection<AudioItemViewModel> _item = new ObservableCollection<AudioItemViewModel>();
            _audioViewModel = await vkaudio.GetAsync();
            foreach (var item in _audioViewModel.response.items)
            {
                AudioItemViewModel itemAudio = new AudioItemViewModel() {Item = item};
                itemAudio.IsMyItem = true;
                _item.Add(itemAudio);
                ConnectItemViewModel(itemAudio);
            }
            AudioItemsViewModel = _item;

            //есле загрузки данных если у нас играла до песня то ищем ее в списке и помечаем как проигрываемую
            if (AudioSingleton.ItemPlaying != null)
            {
                FingPlayingAudio();
            }
        }

        //ищет проигрываемую аудиозапись и помечает ее как проигрываемую
        private void FingPlayingAudio()
        {
            for (int i = 0; i < AudioItemsViewModel.Count; i++)
            {
                if (AudioItemsViewModel[i].Url == AudioSingleton.ItemPlaying.Url)
                {
                     AudioSingleton.ItemPlaying = null;
                     AudioItemsViewModel[i].IsPlay = true;
                     AudioSingleton.ItemPlaying = AudioItemsViewModel[i];
                     
                   
                    break;
                }
            }
            

        }

        private string _searchString;
        public string SearchString
        {
            get { return _searchString; }
            set
            {
                _searchString = value;
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

        async void Audio_AddandDeleteItemEvent(AudioItemViewModel itemAudioViewModel)
        {
            //если моя аудиозапись то удаляем ее
            if (itemAudioViewModel.IsMyItem == true)
            {
                DisconnectItemViewModel(itemAudioViewModel);
                AudioItemsViewModel.Remove(itemAudioViewModel);
                await vkaudio.DeleteAsync(itemAudioViewModel.Item.id, itemAudioViewModel.Item.owner_id);
            }
            //если не моя то добавляем ее
            else
            {
                await vkaudio.AddAsync(itemAudioViewModel.Item.id, itemAudioViewModel.Item.owner_id);
            }
        }

        //подписываемся на событие об удалении или добавлении
        void ConnectItemViewModel(AudioItemViewModel itemAudioViewModel)
        {
            itemAudioViewModel.ItemEvent += Audio_AddandDeleteItemEvent;
        }

        //отписываемся
        void DisconnectItemViewModel(AudioItemViewModel itemAudioViewModel)
        {
            itemAudioViewModel.ItemEvent -= Audio_AddandDeleteItemEvent;
        }

        //синглтон класс для проигравания аудио
        public AudioSingleton AudioSingleton
        {
            get { return _audioSingleton; }
            set
            {
                _audioSingleton = value;
                OnPropertyChanged();
            }
        }

        //выделенная в данный момент позиция
        private AudioItemViewModel _item;
        public AudioItemViewModel ItemSelected
        {
            get { return _item; }
            set
            {
                _item = value;
                OnPropertyChanged();
            }
        }

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
                if (AudioSingleton.ItemPlaying != null)
                {
                    AudioSingleton.ItemPlaying.IsPlay = false; 
                }
                
                ItemSelected.IsPlay = true;
                AudioSingleton.Play(ItemSelected);

               //Для переключения стилей в плейлисте (Только для небольших списков)
               // AudioItemsTemplateSelector = null;
               // AudioItemsTemplateSelector = new AudioItemsTemplateSelector();
            }
        }

        

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
            _audioSingleton.Pause();
        }

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
            AudioSingleton.Stop();
        }

        private AsyncDelegateCommand _audioPositionChainge;
        public ICommand AudioPositionChainged
        {
            get
            {
                if (_audioPositionChainge == null)
                {
                    _audioPositionChainge = new AsyncDelegateCommand(AudioPositionChainge);
                }
                return _audioPositionChainge;
            }
        }

        private async Task AudioPositionChainge(object o)
        {
            //проверяем кончилась ли песня
            if (AudioSingleton.LengthAudio == Convert.ToInt32(AudioSingleton.AudioPosition))
            {
                NextAudioPlay();
            }
            AudioSingleton.Fast();
        }

        //находим играющий трек делаем его IsPlay = false
        //запускаем следующий по порядку
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
                        AudioSingleton.Play(AudioItemsViewModel[i + 1]);
                        break;
                    }
                    else
                    {
                        i = -1;
                        AudioItemsViewModel[i + 1].IsPlay = true;
                        AudioSingleton.Play(AudioItemsViewModel[i + 1]);
                        break;
                    }
                  
                }
            }
        }

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
            _audioViewModel = await vkaudio.SearchAsync(SearchString);
            foreach (var item in _audioViewModel.response.items)
            {
                AudioItemViewModel itemAudio = new AudioItemViewModel() { Item = item };
                itemAudio.IsMyItem = false;
                _item.Add(itemAudio);
                ConnectItemViewModel(itemAudio);
            }
            AudioItemsViewModel = _item;
        }

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
    }
}
