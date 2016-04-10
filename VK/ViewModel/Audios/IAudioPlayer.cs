using System;

namespace VK.ViewModel.Audios
{
	public interface IAudioPlayer
	{
		float VolimePosition { get; set; }
		double AudioPosition { get; set; }

		void Play(string path);
		void Pause();
		void Stop();

		event EventHandler OnEndAudio;
		event EventHandler OnAudioPositionChanged;

	}
}