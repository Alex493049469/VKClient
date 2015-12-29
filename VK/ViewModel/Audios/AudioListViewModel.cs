using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Odbc;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Core;
using Core.Command;
using Microsoft.Win32;
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
        VkApi _vk = new VkApi();
        //ViewModel Аудиозапсей
        private ObservableCollection<AudioItemViewModel> _audioItemsViewModel;
        //для проигрывания аудио в фоне
        private AudioPlayer _audioPlayer = new AudioPlayer();

        public RelayCommand PlayAudioButtonClick { get; private set; }
        public RelayCommand PauseAudioButtonClick { get; private set; }
        public RelayCommand StopAudioButtonClick { get; private set; }
        public RelayCommand SaveAudioButtonClick { get; private set; }
        public AudioListViewModel()
        {
            LoadAudio();
            PlayAudioButtonClick = new RelayCommand(PlayAudio);
            PauseAudioButtonClick = new RelayCommand(PauseAudio);
            StopAudioButtonClick = new RelayCommand(StopAudio);
            SaveAudioButtonClick = new RelayCommand(SaveAudio);
            AudioPlayer.OnTrackEnd += NextAudioPlay;
        }

        /// <summary>
        /// Загрузка моих аудиозаписей
        /// </summary>
        public async void LoadAudio()
        {
            AudioItemsViewModel = null;
            ObservableCollection<AudioItemViewModel> _item = new ObservableCollection<AudioItemViewModel>();
			_audioModel = await _vk.Audio.GetAsync();
            foreach (var item in _audioModel.response.items)
            {
                AudioItemViewModel itemAudio = new AudioItemViewModel()
                {
                    Id = item.id,
                    OwnerId = item.owner_id,
                    IsMyItem = true,
                    Url = item.url,
                    Title = item.title,
                    Duration = item.duration,
                    Artist = item.artist
                };

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

        public string SearchString { get; set; }

        public AudioItemViewModel ItemPlaying { get; set; }
        
        public ObservableCollection<AudioItemViewModel> AudioItemsViewModel
        {
            get { return _audioItemsViewModel; }
            set
            {
                if (_audioItemsViewModel == value)
                    return;

                _audioItemsViewModel = value;

                RaisePropertyChanged();
            }
        }

        public AudioPlayer AudioPlayer
        {
            get { return _audioPlayer; }
            set
            {
                _audioPlayer = value;
                RaisePropertyChanged();
            }
        }

        public AudioItemViewModel ItemSelected { get; set; }
        #endregion;

        #region Плей
        private void PlayAudio()
        {
             if (ItemSelected != null)
             {
                 AudioPlayer.Play(ItemSelected.Url);
                 FingPlayingAudio();
             }
        }

        #endregion;

        #region Пауза
        private void PauseAudio()
        {
            AudioPlayer.Pause();
        }
        #endregion;

        #region Стоп
        private void StopAudio()
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
            _audioModel = await _vk.Audio.SearchAsync(SearchString);
            foreach (var item in _audioModel.response.items)
            {
                AudioItemViewModel itemAudio = new AudioItemViewModel()
                {
                    Id = item.id,
                    OwnerId = item.owner_id,
                    IsMyItem = false,
                    Url = item.url,
                    Title = item.title,
                    Duration = item.duration,
                    Artist = item.artist
                };
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

        #region Клик по кнопке рекомендуемые
        private AsyncDelegateCommand _audioRecommended;
        public ICommand AudioRecommendedButtonClick
        {
            get
            {
                if (_audioRecommended == null)
                {
                    _audioRecommended = new AsyncDelegateCommand(AudioRecommended);
                }
                return _audioRecommended;
            }
        }

        private async Task AudioRecommended(object o)
        {
            if (ItemSelected != null)
            {
                string targetAudio = ItemSelected.OwnerId + "_" + ItemSelected.Id;
                AudioItemsViewModel = null;
                ObservableCollection<AudioItemViewModel> _item = new ObservableCollection<AudioItemViewModel>();
                _audioModel = await _vk.Audio.GetRecommendationsAsync(targetAudio);
                foreach (var item in _audioModel.response.items)
                {
                    AudioItemViewModel itemAudio = new AudioItemViewModel()
                    {
                        Id = item.id,
                        OwnerId = item.owner_id,
                        IsMyItem = false,
                        Url = item.url,
                        Title = item.title,
                        Duration = item.duration,
                        Artist = item.artist
                    };
                    _item.Add(itemAudio);
                }
                AudioItemsViewModel = _item;
            }
        }
        #endregion;

        #region Клик по кнопке сохранить аудиозапись

        private void SaveAudio()
        {
            var sfd = new SaveFileDialog();
            sfd.DefaultExt = ".mp3";
            sfd.FileName = ItemSelected.FullNameAudio + "." + sfd.DefaultExt;
            if (sfd.ShowDialog() == true)
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFileAsync(new Uri(ItemSelected.Url), sfd.FileName);
                webClient.DownloadFileCompleted += delegate(object sender, AsyncCompletedEventArgs args)
                {
                    MessageBox.Show("Файл успешно сохранен!");
                };
            }
        }
        #endregion

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
                    await _vk.Audio.AddAsync(AudioItemsViewModel[i].Id, AudioItemsViewModel[i].OwnerId);
                    MessageBox.Show("Аудиозапись успешно добавлена!");
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
                    await _vk.Audio.DeleteAsync(AudioItemsViewModel[i].Id, AudioItemsViewModel[i].OwnerId);
                    if (AudioItemsViewModel.Remove(AudioItemsViewModel[i]))
                    {
                        MessageBox.Show("Аудиозапись успешно удалена!");
                    }
                    break;
                }
            }

        }
        #endregion

    }
}
