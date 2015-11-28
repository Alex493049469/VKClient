using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Threading;
using Core;
using NAudio.Wave;
using VK.Properties;

namespace VK.ViewModel.Audios
{
    public class AudioPlayer : IDisposable, INotifyPropertyChanged
    {
        private IWavePlayer wavePlayer;
        private AudioFileReader reader;
        private DispatcherTimer timer = new DispatcherTimer();
        private double sliderPosition;
        private string lastPlayed;
        private static float volimePosition;
        const double sliderMax = 10.0;

        public delegate void MethodContainer();
        public event MethodContainer OnTrackEnd;

        //сохранение настроек
        private void SaveSettings()
        {
            Settings.Default.volime = volimePosition;
            Settings.Default.Save();
        }

        private void LoadSettings()
        {
             //загрузка положения бегунка громкости
            VolimePosition = Settings.Default.volime;
        }

        public AudioPlayer()
        {
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += TimerOnTick;
            LoadSettings();
        }

        public string LastPlayed
        {
            get { return lastPlayed; }
        }

        public float VolimePosition
        {
            get { return volimePosition; }
            set
            {
                volimePosition = value;
                if(reader != null)
                reader.Volume = volimePosition;
                SaveSettings();
                OnPropertyChanged();
            }
        }

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            if (reader != null)
            {
                sliderPosition = Math.Min(sliderMax, reader.Position * sliderMax / reader.Length);
                if (reader.Position >= (reader.Length-30000) && wavePlayer.PlaybackState != PlaybackState.Stopped)
                {
                    timer.Stop();
                    if (OnTrackEnd != null) OnTrackEnd();
                    //Thread.Sleep(100);
                }
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
                        var pos = (long)(reader.Length * sliderPosition / sliderMax);
                        reader.Position = pos; // media foundation will worry about block align for us
                    }
                    OnPropertyChanged();
                }
            }
        }

        public void Stop()
        {
            if (wavePlayer != null)
            {
                wavePlayer.Stop();
            }
        }

        public void Pause()
        {
            if (wavePlayer != null)
            {
                wavePlayer.Pause();
            }
        }

        public void Play(string path)
        {
            if (wavePlayer == null)
            {
                CreatePlayer();
            }
            if (wavePlayer.PlaybackState == PlaybackState.Paused)
            {
                wavePlayer.Play();
                return;
            }
            if (lastPlayed != path && reader != null)
            {
                Stop();
                reader.Dispose();
                reader = null;
            }
            if (reader == null)
            {
                reader = new AudioFileReader(path);
                lastPlayed = path;
                wavePlayer.Init(reader);
            }
            if (wavePlayer.PlaybackState == PlaybackState.Playing)
            {
                Stop();

            }
            reader.Volume = volimePosition;
            wavePlayer.Play();

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
            }
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
