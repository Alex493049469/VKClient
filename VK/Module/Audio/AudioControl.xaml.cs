using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Un4seen.Bass;
using VK.Properties;
using VKAPI;
using VKAPI.Model;
using VKAPI.Model.AudioModel;

namespace VK.Module.Audio
{
    /// <summary>
    /// Логика взаимодействия для AudioControl.xaml
    /// </summary>
    public partial class AudioControl : UserControl
    {
        //музыкальный поток
        int stream;
        //прогресс загрузки трека 
        public double PosDownload { get; set; }
        //таймер для отображения позиции трека
        private DispatcherTimer timer = null;
        //источник данных
        public AudioModel audioModel;
        //ссылка на таб контролл
        public TabControl tabControler;

        public AudioControl(TabControl tc, AudioModel am)
        {
            InitializeComponent();

            //загрузка положения бегунка громкости
            Volime.Value = Settings.Default.volime;
            audioModel = am;
            tabControler = tc;
            BindModel();
        }

        //сохранение настроек
        public void SaveSettings()
        {
            Settings.Default.volime = Volime.Value;
            Settings.Default.Save();
        }

        /// <summary>
        /// привязки данных
        /// </summary>
        public void BindModel()
        {
            ListAudio.ItemsSource = audioModel.response.items;
        }

        //поиск аудиозаписей
        public async void FindMusic()
        {
            audioModel = await VkAudio.SearchAsync(TextAudio.Text);

            ListAudio.ItemsSource = audioModel.response.items;
        }

        /// <summary>
        /// Начать воспроизведение трека
        /// </summary>
        public void Play(int index)
        {
            Bass.BASS_StreamFree(stream);
            Bass.BASS_Start();

            if (ListAudio.SelectedIndex < 0)
                return;
            // _myDownloadProc = new DOWNLOADPROC(MyDownload);
            stream = Bass.BASS_StreamCreateURL(audioModel.response.items[index].url, 0, BASSFlag.BASS_DEFAULT, null, IntPtr.Zero);

            if (stream != 0 && Bass.BASS_ChannelPlay(stream, false))
            {
                Bass.BASS_ChannelSetAttribute(stream, BASSAttribute.BASS_ATTRIB_VOL, (float)Volime.Value / 100);
                AudioPosition.Maximum = int.Parse(((Bass.BASS_ChannelBytes2Seconds(stream, Bass.BASS_ChannelGetLength(stream)).ToString().Split(',', '.'))[0]));
                AudioPosition.SelectionEnd = 0;
                timerStart();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void timerStart()
        {
            timer = new DispatcherTimer(DispatcherPriority.Normal);  // если надо, то в скобках указываем приоритет, например DispatcherPriority.Render
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            timer.Start();
        }

        private void timerTick(object sender, EventArgs e)
        {
            // TrekState.Value = Bass.BASS_ChannelGetPosition(stream, 0);

            //TrekState.Maximum = Bass.BASS_ChannelGetLength(stream, 0) - 1;

            // sGauge2.MaxValue:=BASS_StreamGetFilePosition(stream, BASS_FILEPOS_END);
            // sGauge2.Progress:=BASS_StreamGetFilePosition(stream, BASS_FILEPOS_DOWNLOAD);
            int d = Absolution(int.Parse(((Bass.BASS_ChannelBytes2Seconds(stream, Bass.BASS_ChannelGetPosition(stream)).ToString().Split(',', '.'))[0])));

            AudioPosition.Value = d;

            AudioPosition.SelectionEnd = Absolution(int.Parse(((Bass.BASS_StreamGetFilePosition(stream, BASSStreamFilePosition.BASS_FILEPOS_DOWNLOAD).ToString().Split(',', '.'))[0])));
            //TrekState.SelectionEnd = Bass.BASS_StreamGetFilePosition(stream, BASSStreamFilePosition.BASS_FILEPOS_DOWNLOAD);
            // Bass.BASS_StreamGetFilePosition(stream, BASSStreamFilePosition.BASS_FILEPOS_DOWNLOAD);
            //автоматическое переключение песен после завершения предыдущей
            if (AudioPosition.Value == AudioPosition.Maximum)
            {
                if (ListAudio.SelectedIndex + 1 != ListAudio.Items.Count)
                {

                    ListAudio.SelectedIndex = ListAudio.SelectedIndex + 1;
                    Play(ListAudio.SelectedIndex);
                }
                else
                {
                    ListAudio.SelectedIndex = 0;
                    Play(ListAudio.SelectedIndex);

                }

            }
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

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            Play(ListAudio.SelectedIndex);
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
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

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            Bass.BASS_StreamFree(stream);
        }

        private void AudioPosition_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double TrackCurrPos = Math.Round(Bass.BASS_ChannelBytes2Seconds(stream, Bass.BASS_ChannelGetPosition(stream, 0)));
            if (AudioPosition.Value != TrackCurrPos && AudioPosition.Value != TrackCurrPos - 1)
            {
                Bass.BASS_ChannelSetPosition(stream, AudioPosition.Value);
            }
        }

        private void Volime_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Bass.BASS_ChannelSetAttribute(stream, BASSAttribute.BASS_ATTRIB_VOL, (float)Volime.Value / 100);
            SaveSettings();
        }

        private void ListAudio_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Play(ListAudio.SelectedIndex);
        }

        private void FindAudioButton_Click(object sender, RoutedEventArgs e)
        {
            FindMusic();
        }
    }
}
