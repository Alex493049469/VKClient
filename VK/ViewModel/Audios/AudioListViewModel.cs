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
        //выделенная в данный момент позиция
        private AudioItemViewModel _item;
        //строка для поиска
        private string _searchString;

        public AudioListViewModel()
        {
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += TimerOnTick;
            LoadAudio();
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

            //есле загрузки данных если у нас играла до песня то ищем ее в списке и помечаем как проигрываемую
            //if (AudioSingleton.ItemPlaying != null)
            //{
            //    FingPlayingAudio();
            //}
        }

        /// <summary>
        /// ищет проигрываемую аудиозапись и помечает ее как проигрываемую
        /// </summary>
        private void FingPlayingAudio()
        {
            for (int i = 0; i < AudioItemsViewModel.Count; i++)
            {
                //if (AudioItemsViewModel[i].Url == AudioSingleton.ItemPlaying.Url)
                //{
                //    AudioSingleton.ItemPlaying = null;
                //    AudioItemsViewModel[i].IsPlay = true;
                //    AudioSingleton.ItemPlaying = AudioItemsViewModel[i];

                //    break;
                //}
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

        //public AudioSingleton AudioSingleton
        //{
        //    get { return _audioSingleton; }
        //    set
        //    {
        //        _audioSingleton = value;
        //        OnPropertyChanged();
        //    }
        //}

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
                TryOpenInputFile(ItemSelected.Url);
                Stop();
                SliderPosition = 0;
                Play();
                //if (AudioSingleton.ItemPlaying != null)
                //{
                //    AudioSingleton.ItemPlaying.IsPlay = false; 
                //}

                //ItemSelected.IsPlay = true;
                //AudioSingleton.Play(ItemSelected);
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
           // _audioSingleton.Pause()
            
            Pause();
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
           // AudioSingleton.Stop();
            Stop();
        }
        #endregion;

        #region Отвечает за получение позиции проигрываемой песни и переключение песен
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
            //if (AudioSingleton.LengthAudio == Convert.ToInt32(AudioSingleton.AudioPosition))
            //{
            //    NextAudioPlay();
            //}
            //AudioSingleton.Fast();
        }

        public void NextAudioPlay()
        {
            for (int i = 0; i < AudioItemsViewModel.Count; i++)
            {
                if (AudioItemsViewModel[i].IsPlay == true)
                {
                    AudioItemsViewModel[i].IsPlay = false;
                    AudioItemsViewModel[i + 1].IsPlay = true;
                    
                    //проверяем не достигли ли конца списка
                    if (i + 1 < AudioItemsViewModel.Count)
                    {
                        //AudioItemsViewModel[i + 1].IsPlay = true;
                       // AudioSingleton.Play(AudioItemsViewModel[i + 1]);
                        break;
                    }
                    else
                    {
                        i = -1;
                        //AudioItemsViewModel[i + 1].IsPlay = true;
                      //  AudioSingleton.Play(AudioItemsViewModel[i + 1]);
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


        private string inputPath;
        private string defaultDecompressionFormat;
        private IWavePlayer wavePlayer;
        private AudioFileReader reader;
        private DispatcherTimer timer = new DispatcherTimer();
        private double sliderPosition;
        private string lastPlayed;
        private static float volimePosition;
        const double sliderMax = 10.0;
       

        public float VolimePosition
        {
            get { return volimePosition; }
            set
            {
                volimePosition = value;
                reader.Volume = volimePosition;
                OnPropertyChanged();
            }
        }

        public bool IsPlaying
        {
            get { return wavePlayer != null && wavePlayer.PlaybackState == PlaybackState.Playing; }
        }

        public bool IsStopped
        {
            get { return wavePlayer == null || wavePlayer.PlaybackState == PlaybackState.Stopped; }
        }

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            if (reader != null)
            {
                sliderPosition = Math.Min(sliderMax, reader.Position * sliderMax / reader.Length);
                OnPropertyChanged("SliderPosition");
            }
        }

        public double SliderPosition
        {
            get { return sliderPosition; }
            set
            {
                if (sliderPosition != value)
                {
                    sliderPosition = value;
                    if (reader != null)
                    {
                        var pos = (long)(reader.Length * sliderPosition/sliderMax);
                        reader.Position = pos; // media foundation will worry about block align for us
                       
                    }
                    OnPropertyChanged();
                }
            }
        }

        private bool TryOpenInputFile(string file)
        {
            bool isValid = false;

                using (var tempReader = new MediaFoundationReader(file))
                {
                    DefaultDecompressionFormat = tempReader.WaveFormat.ToString();
                    InputPath = file;
                    isValid = true;
                }
            

            return isValid;
        }

        public string DefaultDecompressionFormat
        {
            get { return defaultDecompressionFormat; }
            set
            {
                defaultDecompressionFormat = value;
                OnPropertyChanged("DefaultDecompressionFormat");
            }
        }

        public string InputPath
        {
            get { return inputPath; }
            set
            {
                if (inputPath != value)
                {
                    inputPath = value;
                    OnPropertyChanged("InputPath");
                }
            }
        }


        private void Stop()
        {
            if (wavePlayer != null)
            {
                wavePlayer.Stop();
            }
        }

        private void Pause()
        {
            if (wavePlayer != null)
            {
                wavePlayer.Pause();
                OnPropertyChanged("IsPlaying");
                OnPropertyChanged("IsStopped");
            }
        }

        private void Play()
        {
            if (wavePlayer == null)
            {
                CreatePlayer();
            }
            if (lastPlayed != inputPath && reader != null)
            {
                reader.Dispose();
                reader = null;
            }
            if (reader == null)
            {
                reader = new AudioFileReader(inputPath);
                lastPlayed = inputPath;
                wavePlayer.Init(reader);
            }
            wavePlayer.Play();
            OnPropertyChanged("IsPlaying");
            OnPropertyChanged("IsStopped");
            timer.Start();
        }

        private void CreatePlayer()
        {
            wavePlayer = new WaveOutEvent();
            wavePlayer.PlaybackStopped += WavePlayerOnPlaybackStopped;
        }

        private void WavePlayerOnPlaybackStopped(object sender, StoppedEventArgs stoppedEventArgs)
        {

            if (reader != null)
            {
                SliderPosition = 0;
                //reader.Position = 0;
                //timer.Stop();
            }
            
            OnPropertyChanged("IsPlaying");
            OnPropertyChanged("IsStopped");
        }


        public void Dispose()
        {
            if (wavePlayer != null)
            {
                wavePlayer.Dispose();
            }
            if (reader != null)
            {
                reader.Dispose();
            }
        }
    }
}
