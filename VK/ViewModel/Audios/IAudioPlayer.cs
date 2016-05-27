using System;
using System.Threading.Tasks;

namespace VK.ViewModel.Audios
{
	public interface IAudioPlayer
	{
		float VolimePosition { get; set; }
		double AudioPosition { get; set; }

		Task Play(string path);
		void Pause();
		void Stop();

		event EventHandler OnEndAudio;
		event EventHandler OnAudioPositionChanged;

	}
}