using Core;
using VKAPI.Model.AudioModel;

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

        public string FullNameAudio
        {
            get { return Artist + " - " + Title; }
        }

        //проигрывается ли в данный момент
        public bool IsPlay { get; set; }

        //флаг мои аудиозаписи или нет
        public bool IsMyItem { get; set; }


    }
}
