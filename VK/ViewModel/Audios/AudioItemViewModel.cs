using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Core;
using Core.Command;
using VK.View;
using VKAPI;
using VKAPI.Core;

namespace VK.ViewModel.Audios
{

	public class AudioItemViewModel : BaseViewModel
	{
		public int Id { get; set; }
		public int OwnerId { get; set; }
		public string Artist { get; set; }
		public string Title { get; set; }
		public int Duration { get; set; }
		public string Url { get; set; }
		public int LyricsId { get; set; }
		public int GenreId { get; set; }
		public int? NoSearch { get; set; }
		public string Text { get; set; }

		public ObservableCollection<Genre> Genres
		{
			get { return _genres; }
			set { _genres = value; }
		}

		public Genre SelectedGenre { get; set; }


		public string FullNameAudio => Artist + " - " + Title;

		//проигрывается ли в данный момент
		public bool IsPlay { get; set; }

		//флаг мои аудиозаписи или нет
		public bool IsMyItem { get; set; }

		ObservableCollection<Genre> _genres = new ObservableCollection<Genre>
			{
				new Genre() {Id = 1, Value = "Rock"},
				new Genre() {Id = 2, Value = "Pop"},
				new Genre() {Id = 3, Value = "Rap & Hip - Hop"},
				new Genre() {Id = 4, Value = "Easy Listening"},
				new Genre() {Id = 5, Value = "Dance & House"},
				new Genre() {Id = 6, Value = "Instrumental"},
				new Genre() {Id = 7, Value = "Metal"},
				new Genre() {Id = 21, Value = "Alternative"},
				new Genre() {Id = 8, Value = "Dubstep"},
				new Genre() {Id = 1001, Value = "Jazz & Blues"},
				new Genre() {Id = 10, Value = "Drum & Bass"},
				new Genre() {Id = 11, Value = "Trance"},
				new Genre() {Id = 12, Value = "Chanson"},
				new Genre() {Id = 13, Value = "Ethnic"},
				new Genre() {Id = 14, Value = "Acoustic & Vocal"},
				new Genre() {Id = 15, Value = "Reggae"},
				new Genre() {Id = 16, Value = "Classical"},
				new Genre() {Id = 17, Value = "Indie Pop"},
				new Genre() {Id = 19, Value = "Speech"},
				new Genre() {Id = 22, Value = "Electropop & Disco"},
				new Genre() {Id = 18, Value = "Other"}
			};

		private AsyncDelegateCommand _editAudioButtonClick;
		public RelayCommand SaveAudioButtonClick { get; private set; }

		readonly VkApi _vk = new VkApi();

		public AudioItemViewModel()
		{
			SaveAudioButtonClick = new RelayCommand(SaveAudio);
		}

		private void SaveAudio()
		{
			_vk.Audio.EditAsync(OwnerId, Id, Artist, Title, Text, GenreId, NoSearch);
		}

		public ICommand EditAudioButtonClick => _editAudioButtonClick ?? (_editAudioButtonClick = new AsyncDelegateCommand(EditAudio));
		private async Task EditAudio(object o)
		{
			if(LyricsId != 0 && string.IsNullOrEmpty(Text))
			{
				var lyricsModel = await _vk.Audio.GetLyricsAsync(LyricsId);
				Text = lyricsModel.response.text;
			}
			SelectedGenre = _genres.First(genre => genre.Id == GenreId);

			EditAudioView eav = new EditAudioView {DataContext = this};
			eav.Show();
		}

		public class Genre
		{
			public int Id { get; set; }
			public string Value { get; set; }
		}
	}
}
