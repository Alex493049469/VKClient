using System;
using System.Threading.Tasks;
using System.Windows.Threading;
using Core;
using NAudio.Wave;
using VK.Properties;
using VKAPI.Model.AudioModel;

namespace VK.ViewModel.Audios
{
	public class AudioPlayer : BaseViewModel, IDisposable
	{
		private IWavePlayer _wavePlayer;
		private AudioFileReader _reader;
		private readonly DispatcherTimer _timer = new DispatcherTimer();
		private double _sliderPosition;
		private static float _volimePosition;
		const double SliderMax = 10.0;

		private Task<AudioFileReader> Reader;

		public delegate void MethodContainer();
		public event MethodContainer OnTrackEnd;

		//сохранение настроек
		private void SaveSettings()
		{
			Settings.Default.volime = _volimePosition;
			Settings.Default.Save();
		}

		private void LoadSettings()
		{
			 //загрузка положения бегунка громкости
			VolimePosition = Settings.Default.volime;
		}

		public AudioPlayer()
		{
			_timer.Interval = TimeSpan.FromMilliseconds(100);
			_timer.Tick += TimerOnTick;
			LoadSettings();
		}

		public float VolimePosition
		{
			get { return _volimePosition; }
			set
			{
				_volimePosition = value;
				if(_reader != null)
				_reader.Volume = _volimePosition;
				SaveSettings();
				RaisePropertyChanged();
			}
		}

		private void TimerOnTick(object sender, EventArgs eventArgs)
		{
			if (_reader != null)
			{
				_sliderPosition = Math.Min(SliderMax, _reader.Position * SliderMax / _reader.Length);
				if (_reader.Position >= (_reader.Length-30000) && _wavePlayer.PlaybackState != PlaybackState.Stopped)
				{
					_timer.Stop();
					OnTrackEnd?.Invoke();
				}
				RaisePropertyChanged("SliderPosition");
			}
		}

		public double SliderPosition
		{
			get { return _sliderPosition; }
			set
			{
				if (_sliderPosition != value)
				{
					_sliderPosition = value;
					if (_reader != null)
					{
						var pos = (long)(_reader.Length * _sliderPosition / SliderMax);
						_reader.Position = pos; // media foundation will worry about block align for us
					}
					RaisePropertyChanged();
				}
			}
		}

		public void Stop()
		{
			_wavePlayer?.Stop();
		}

		public void Pause()
		{
			_wavePlayer?.Pause();
		}

		public void Play(string path)
		{
			if (_wavePlayer == null)
			{
				CreatePlayer();
			}
			if (_wavePlayer.PlaybackState == PlaybackState.Paused)
			{
				_wavePlayer.Play();
				return;
			}
			if (_reader != null)
			{
				Stop();
				_reader.Dispose();
				_reader = null;
			}
			if (_reader == null)
			{
				//здесь происходит зависание если трек не доступен
				Reader = GetReader(path);

				_reader = Reader.Result; 
				//_reader = new AudioFileReader(path);
				_wavePlayer.Init(_reader);
			}
			if (_wavePlayer.PlaybackState == PlaybackState.Playing)
			{
				Stop();

			}
			_reader.Volume = _volimePosition;
			_wavePlayer.Play();

			_timer.Start();
		}

		public Task<AudioFileReader> GetReader(string path)
		{
			return Task.Run(() => new AudioFileReader(path));
		}


		private void CreatePlayer()
		{
			_wavePlayer = new WaveOutEvent();
			_wavePlayer.PlaybackStopped += WavePlayerOnPlaybackStopped;
		}

		private void WavePlayerOnPlaybackStopped(object sender, StoppedEventArgs stoppedEventArgs)
		{
			if (_reader != null)
			{
				SliderPosition = 0;
			}
		}

		public void Dispose()
		{
			_wavePlayer?.Dispose();
			_reader?.Dispose();
		}
	   
	}
}
