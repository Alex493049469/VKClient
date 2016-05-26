using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Core.Command;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using VK.Services;
using VK.View;
using VK.View.Audio;
using VK.ViewModel.Main;
using VKAPI;
using VKAPI.Core;
using VKAPI.Model.AudioModel;

namespace VK.ViewModel.Audios
{
	public class AudioListViewModel : PaneViewModel
	{
		//для доступа к данным песен
		private readonly VkApi _vkApi;
		//ViewModel Аудиозапсей
		private ObservableCollection<AudioItemViewModel> _audioItemsViewModel;
		//для проигрывания аудио в фоне
		private readonly IAudioPlayer _audioPlayer = new AudioPlayer();
		//состояние проигрывания
		private bool _isPaysed = false;
		//
		private readonly FlyoutViewModel _flyout;

		//public RelayCommand PlayAudioButtonClick { get; private set; }
		public RelayCommand PauseAudioButtonClick { get; private set; }
		public RelayCommand StopAudioButtonClick { get; private set; }
		public RelayCommand SaveAudioButtonClick { get; private set; }

		public RelayCommand CheckAllAudioButtonClick { get; private set; }
		public RelayCommand UnCheckAllAudioButtonClick { get; private set; }
		public RelayCommand SaveCheckedAudioButtonClick { get; private set; }

		public AudioListViewModel(VkApi vkApi, FlyoutViewModel flyout)
		{
			_vkApi = vkApi;
			_flyout = flyout;
			Title = "Мои аудиозаписи";
			LoadAudio();

			PauseAudioButtonClick = new RelayCommand(PauseAudio);
			StopAudioButtonClick = new RelayCommand(StopAudio);
			SaveAudioButtonClick = new RelayCommand(SaveAudio);

			CheckAllAudioButtonClick = new RelayCommand(CheckAll);
			UnCheckAllAudioButtonClick = new RelayCommand(UnCheckAll);
			SaveCheckedAudioButtonClick = new RelayCommand(SaveCheckedAudio);

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
				var itemAudio = new AudioItemViewModel(_vkApi, _flyout)
				{
					Id = item.id,
					OwnerId = item.owner_id,
					IsMyItem = isMyAudio,
					Url = item.url,
					Title = item.title,
					Duration = item.duration,
					Artist = item.artist,
					LyricsId = item.lyrics_id,
					GenreId = item.genre_id,
					NoSearch = item.no_search
				};

				_item.Add(itemAudio);
			}
			AudioItemsViewModel = _item;
		}

		private void CheckAll()
		{
			foreach (var item in AudioItemsViewModel)
			{
				item.Check = true;
			}
		}

		private void UnCheckAll()
		{
			foreach (var item in AudioItemsViewModel)
			{
				item.Check = false;
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
		private AsyncDelegateCommand _playAudioButtonClick;
		public ICommand PlayAudioButtonClick => _playAudioButtonClick ?? (_playAudioButtonClick = new AsyncDelegateCommand(PlayAudio));

		private async Task PlayAudio(object o)
		{
			//await Task.Run(() =>
			//{
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
			//});
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
			AudioModel audioModel = await _vkApi.Audio.SearchAsync(SearchString);
			Add(audioModel, false);
		}
		#endregion;

		#region Мои аудиозаписи
		private AsyncDelegateCommand _myAudioAudio;
		public ICommand MyAudioButtonClick => _myAudioAudio ?? (_myAudioAudio = new AsyncDelegateCommand(MyAudio));

		private async Task MyAudio(object o)
		{
			AudioItemsViewModel = null;
			AudioModel audioModel = await _vkApi.Audio.GetAsync();
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
				AudioModel audioModel = await _vkApi.Audio.GetRecommendationsAsync(targetAudio);
				Add(audioModel, false);
			}
		}
		#endregion;

		#region Сохранение аудиозаписей

		private void SaveAudio()
		{
			_flyout.CustomView = new SaveMultipleAudio() { DataContext = this };
			_flyout.Header = "Сохранение аудиозаписей";
			_flyout.Position = Position.Right;
			_flyout.IsModal = true;
			_flyout.Show();
		}

		private void SaveCheckedAudio()
		{
			var fbd = new FolderBrowserDialog {Description = "Выберите папку"};
			if (fbd.ShowDialog() == DialogResult.OK)
			{
				_flyout.Hide();
				foreach (var item in AudioItemsViewModel)
				{
					if (item.Check)
					{
						item.SaveCheckAudio(fbd.SelectedPath +"\\"+item.FullNameAudio + ".mp3");
					}
				}
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
				if (AudioItemsViewModel[i].Id == (int)o)
				{
					await _vkApi.Audio.AddAsync(AudioItemsViewModel[i].Id, AudioItemsViewModel[i].OwnerId);
					DialogService.ShowMessage("Оповещение", "Аудиозапись успешно добавлена!");
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
				if (AudioItemsViewModel[i].Id == (int)o)
				{
					var mySettings = new MetroDialogSettings()
					{
						AffirmativeButtonText = "Да",
						NegativeButtonText = "Нет"
					};

					var result = await DialogService.ShowMessage("Оповещение!", "Вы действительно хотите удалить аудиозапись: "+ AudioItemsViewModel[i].FullNameAudio + "?",
						MessageDialogStyle.AffirmativeAndNegative, mySettings);

					if (result == MessageDialogResult.Affirmative)
					{
						await _vkApi.Audio.DeleteAsync(AudioItemsViewModel[i].Id, AudioItemsViewModel[i].OwnerId);
						AudioItemsViewModel.Remove(AudioItemsViewModel[i]);
						DialogService.ShowMessage("Оповещение", "Аудиозапись успешно удалена!");
					}

					break;
				}
			}

		}
		#endregion

	}
}
