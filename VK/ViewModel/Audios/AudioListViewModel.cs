using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Core.Command;
using Microsoft.Win32;
using VKAPI;
using VKAPI.Core;
using VKAPI.Model.AudioModel;

namespace VK.ViewModel.Audios
{
	public class AudioListViewModel : PaneViewModel
	{
		//для доступа к данным песен
		readonly VkApi _vk = new VkApi();
		//ViewModel Аудиозапсей
		private ObservableCollection<AudioItemViewModel> _audioItemsViewModel;
		//для проигрывания аудио в фоне
		private IAudioPlayer _audioPlayer = new AudioPlayer();
		//состояние проигрывания
		private bool _isPaysed = false;

		public RelayCommand PlayAudioButtonClick { get; private set; }
		public RelayCommand PauseAudioButtonClick { get; private set; }
		public RelayCommand StopAudioButtonClick { get; private set; }
		public RelayCommand SaveAudioButtonClick { get; private set; }

		public AudioListViewModel()
		{
			Title = "Мои аудиозаписи";
			LoadAudio();
			PlayAudioButtonClick = new RelayCommand(PlayAudio);
			PauseAudioButtonClick = new RelayCommand(PauseAudio);
			StopAudioButtonClick = new RelayCommand(StopAudio);
			SaveAudioButtonClick = new RelayCommand(SaveAudio);

			_audioPlayer.OnEndAudio += NextAudioPlay;
			_audioPlayer.OnAudioPositionChanged += (sender, args) => RaisePropertyChanged("AudioPosition");

		}

		/// <summary>
		/// Загрузка моих аудиозаписей
		/// </summary>
		public async void LoadAudio()
		{
			await MyAudio(null);
		}

		private void Add(AudioModel audioModel, bool isMyAudio)
		{
			ObservableCollection<AudioItemViewModel> _item = new ObservableCollection<AudioItemViewModel>();
			foreach (var item in audioModel.response.items)
			{
				AudioItemViewModel itemAudio = new AudioItemViewModel()
				{
					Id = item.id,
					OwnerId = item.owner_id,
					IsMyItem = isMyAudio,
					Url = item.url,
					Title = item.title,
					Duration = item.duration,
					Artist = item.artist
				};

				_item.Add(itemAudio);
			}
			AudioItemsViewModel = _item;
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

		public float VolimePosition
		{
			get { return _audioPlayer.VolimePosition; }
			set
			{
				_audioPlayer.VolimePosition = value;
				RaisePropertyChanged();
			}
		}

		public double AudioPosition
		{
			get { return _audioPlayer.AudioPosition; }
			set
			{
				_audioPlayer.AudioPosition = value;
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
				_audioPlayer.Play(ItemSelected.Url);

				if (_isPaysed != true)
				{
					if (ItemPlaying != null)
					{
						ItemPlaying.IsPlay = false;
					}
					ItemSelected.IsPlay = true;
					ItemPlaying = ItemSelected;
				}
				_isPaysed = false;
			}
		}

		#endregion;

		#region Пауза
		private void PauseAudio()
		{
			_audioPlayer.Pause();
			_isPaysed = true;
		}
		#endregion;

		#region Стоп
		private void StopAudio()
		{
			_audioPlayer.Stop();
		}
		#endregion;

		#region Переключение песен

		public void NextAudioPlay(object sender, EventArgs eventArgs)
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
						_audioPlayer.Play(AudioItemsViewModel[i + 1].Url);
						ItemPlaying = AudioItemsViewModel[i + 1];
						break;
					}
					else
					{
						i = -1;
						AudioItemsViewModel[i + 1].IsPlay = true;
						_audioPlayer.Play(AudioItemsViewModel[i + 1].Url);
						ItemPlaying = AudioItemsViewModel[i + 1];
						break;
					}
				  
				}
			}
		}
		#endregion;

		#region Поиск аудиозаписей
		private AsyncDelegateCommand _searchAudio;
		public ICommand SearchAudioButtonClick => _searchAudio ?? (_searchAudio = new AsyncDelegateCommand(SearchAudio));

		private async Task SearchAudio(object o)
		{
			AudioItemsViewModel = null;
			AudioModel audioModel = await _vk.Audio.SearchAsync(SearchString);
			Add(audioModel, false);
		}
		#endregion;

		#region Мои аудиозаписи
		private AsyncDelegateCommand _myAudioAudio;
		public ICommand MyAudioButtonClick => _myAudioAudio ?? (_myAudioAudio = new AsyncDelegateCommand(MyAudio));

		private async Task MyAudio(object o)
		{
			AudioItemsViewModel = null;
			AudioModel audioModel = await _vk.Audio.GetAsync();
			Add(audioModel, true);
		}
		#endregion;

		#region Рекомендуемые
		private AsyncDelegateCommand _audioRecommended;
		public ICommand AudioRecommendedButtonClick => _audioRecommended ?? (_audioRecommended = new AsyncDelegateCommand(AudioRecommended));

		private async Task AudioRecommended(object o)
		{
			if (ItemSelected != null)
			{
				string targetAudio = ItemSelected.OwnerId + "_" + ItemSelected.Id;
				AudioItemsViewModel = null;
				AudioModel audioModel = await _vk.Audio.GetRecommendationsAsync(targetAudio);
				Add(audioModel, false);
			}
		}
		#endregion;

		#region Сохранение аудиозаписи

		private void SaveAudio()
		{
			var sfd = new SaveFileDialog {DefaultExt = ".mp3"};
			sfd.FileName = ItemSelected.FullNameAudio + "." + sfd.DefaultExt;
			if (sfd.ShowDialog() == true)
			{
				WebClient webClient = new WebClient();
				webClient.DownloadFileAsync(new Uri(ItemSelected.Url), sfd.FileName);
				webClient.DownloadFileCompleted += delegate {
					MessageBox.Show("Файл успешно сохранен!");
				};
			}
		}
		#endregion

		#region Добавление аудиозаписи
		private AsyncDelegateCommand _addAudio;
		public ICommand AddAudioButtonClick => _addAudio ?? (_addAudio = new AsyncDelegateCommand(AddAudio));

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
		public ICommand DeleteAudioButtonClick => _deleteAudio ?? (_deleteAudio = new AsyncDelegateCommand(DeleteAudio));

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
