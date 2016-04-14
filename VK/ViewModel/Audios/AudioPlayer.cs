using System;
using System.Threading.Tasks;
using System.Windows.Threading;
using Core;
using NAudio.Wave;
using VK.Properties;

namespace VK.ViewModel.Audios
{
	public class AudioPlayer : IAudioPlayer, IDisposable
	{
		private IWavePlayer _wavePlayer;
		private AudioFileReader _reader;
		private readonly DispatcherTimer _timer = new DispatcherTimer();
		private double _audioPosition;
		private static float _volimePosition;
		private const double SliderMax = 10.0;

		private Task<AudioFileReader> Reader;

		public event EventHandler OnEndAudio;
		public event EventHandler OnAudioPositionChanged;

		private void SaveSettings()
		{
			Settings.Default.volime = _volimePosition;
			Settings.Default.Save();
		}

		private void LoadSettings()
		{
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
			}
		}

		private void TimerOnTick(object sender, EventArgs eventArgs)
		{
			if (_reader != null)
			{
				OnAudioPositionChanged?.Invoke(this, null);
				_audioPosition = Math.Min(SliderMax, _reader.Position * SliderMax / _reader.Length);
				if (_reader.Position >= (_reader.Length-30000) && _wavePlayer.PlaybackState != PlaybackState.Stopped)
				{
					_timer.Stop();
					OnEndAudio?.Invoke(this, null);
				}
			}
		}

		public double AudioPosition
		{
			get { return _audioPosition; }
			set
			{
				if (_audioPosition != value)
				{
					_audioPosition = value;
					if (_reader != null)
					{
						var pos = (long)(_reader.Length * _audioPosition / SliderMax);
						_reader.Position = pos; // media foundation will worry about block align for us
					}
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
				//Reader = GetReader(path);

				//_reader = Reader.Result; 
				_reader = new AudioFileReader(path);
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

		private Task<AudioFileReader> GetReader(string path)
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
				AudioPosition = 0;
			}
		}

		public void Dispose()
		{
			_wavePlayer?.Dispose();
			_reader?.Dispose();
		}
	   
	}
}
