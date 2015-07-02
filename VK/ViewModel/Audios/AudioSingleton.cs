using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using Core;
using Un4seen.Bass;
using VK.Properties;

namespace VK.ViewModel.Audios
{
    class AudioSingleton : Singleton<AudioSingleton>,  INotifyPropertyChanged
    {
        //флаг для работы перемотки
        private bool onFast = false;
        //музыкальный поток
        private int stream;
        //таймер для отображения позиции трека
        private DispatcherTimer timer = null;
        //проигрываемая модель
        private AudioItemViewModel _itemPlaying;

        public AudioItemViewModel ItemPlaying
        {
            get { return _itemPlaying; }
            set
            {
                _itemPlaying = value;
                OnPropertyChanged();
            }
        }

        /// Вызовет защищенный конструктор класса Singleton
        private AudioSingleton()
        {
            //загрузка положения бегунка громкости
            VolimePosition = Settings.Default.volime;
        }

        //сохранение настроек
        private void SaveSettings()
        {
            Settings.Default.volime = VolimePosition;
            Settings.Default.Save();
        }

        //длина аудио
        private int _lengthAudio = 1;
        public int LengthAudio
        {
            get { return _lengthAudio; }
            set
            {
                _lengthAudio = value;
                OnPropertyChanged();
            }
        }

        //позиция аудио
        private double _audioPosition = 0;
        public double AudioPosition 
        {
            get { return _audioPosition; }
            set
            {
                _audioPosition = value;
                OnPropertyChanged();
            }
        }

        //позиция громкости
        private double _volimePosition;
        public double VolimePosition 
        {
            get { return _volimePosition; }
            set
            {
                _volimePosition = value;
                Bass.BASS_ChannelSetAttribute(stream, BASSAttribute.BASS_ATTRIB_VOL, (float)VolimePosition / 100);
                SaveSettings();
                OnPropertyChanged();
            }
        }
       
        /// <summary>
        /// Начать воспроизведение трека
        /// </summary>
        public void Play(AudioItemViewModel itemPlaying)
        {
           
            ItemPlaying = itemPlaying;
            
            Bass.BASS_StreamFree(stream);
            Bass.BASS_Start();

            stream = Bass.BASS_StreamCreateURL(ItemPlaying.Url, 0, BASSFlag.BASS_DEFAULT, null, IntPtr.Zero);

            if (stream != 0 && Bass.BASS_ChannelPlay(stream, false))
            {
                Bass.BASS_ChannelSetAttribute(stream, BASSAttribute.BASS_ATTRIB_VOL, (float)VolimePosition / 100);
                //длина аудио
                LengthAudio = int.Parse(((Bass.BASS_ChannelBytes2Seconds(stream, Bass.BASS_ChannelGetLength(stream)).ToString().Split(',', '.'))[0]));
                AudioPosition = 0;
                timerStart();
            }
        }

        private void timerStart()
        {
            timer = new DispatcherTimer(DispatcherPriority.Normal);  // если надо, то в скобках указываем приоритет, например DispatcherPriority.Render
            timer.Tick += new EventHandler(TimerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            onFast = true;
            int d = Absolution(int.Parse(((Bass.BASS_ChannelBytes2Seconds(stream, Bass.BASS_ChannelGetPosition(stream)).ToString().Split(',', '.'))[0])));
            AudioPosition = d;
            onFast = false;
            
        }
        private int Absolution(int arg)
        {
            if (arg < 0)
            {
                return 0;
            }
            else
            {
                return arg;
            }
        }

        public void Pause()
        {
            if (Bass.BASS_ChannelIsActive(stream) == BASSActive.BASS_ACTIVE_PLAYING)
            {
                Bass.BASS_Pause();
            }
            else
            {
                Bass.BASS_Start();
            }
        }

        public void Stop()
        {
            Bass.BASS_StreamFree(stream);
        }

        public void Fast()
        {
            double TrackCurrPos = Math.Round(Bass.BASS_ChannelBytes2Seconds(stream, Bass.BASS_ChannelGetPosition(stream, 0)));
            
            if (AudioPosition != TrackCurrPos && AudioPosition != TrackCurrPos - 1)
            {
                if (!onFast)
                {
                    Bass.BASS_ChannelSetPosition(stream, AudioPosition);
                }
            } 
        }

        #region MVVM related
        protected void OnPropertyChanged([CallerMemberName]string propertyName = "") // волшебство .NET 4.5
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
