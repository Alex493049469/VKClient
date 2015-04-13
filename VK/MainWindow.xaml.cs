using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace VK
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //музыкальный поток
        int stream;
        //источники данных
        public AudioModel audioModel;
        public UserModel userModel;
        //прогресс загрузки трека 
        public double PosDownload { get; set; }
        //таймер для отображения позиции трека
        private DispatcherTimer timer = null;

        //заготовки для сохранения файлов
        private FileStream _fs = null;
        private DOWNLOADPROC _myDownloadProc;
        private byte[] _data; // local data buffer

        public MainWindow()
        {
            InitializeComponent();
            //проверяем есть ли токен в настройках
            if (Settings.Default.token == "")
            {
                //скрываем главную форму если нет токена 
                this.Visibility = Visibility.Hidden;
                //отображаем окно авторизации
                AuthorizationWindow authorization = new AuthorizationWindow();
                //ссылка на главную форму
                authorization.main = this;
                authorization.Show();
            }
                //если токен есть то все в порядке вызываем дальнейшую инициализацию приложения
            else
            {
                VkMain.token = Settings.Default.token;
                LoadSettings();
            }
            //
        }

        /// <summary>
        /// загрузка настроек
        /// </summary>
        public void LoadSettings()
        {
            //загрузка положения бегунка громкости
            Volime.Value = Settings.Default.volime;
            //инициализация библиотеки bass для проигрывания аудио
            Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, new System.Windows.Interop.WindowInteropHelper(this).Handle);

            LoadFriends();
            LoadMyMusic();
        }

        //сохранение настроек
        public void SaveSettings()
        {
            Settings.Default.volime = Volime.Value;
            Settings.Default.Save();
        }

        //загружаем список друзей
        public async void LoadFriends()
        {
            userModel = await VkFriends.GetMyFriendsAsync();
            ListFriend.ItemsSource = userModel.Items;
        }

        //загрузка моих аудиозаписей
        public async void LoadMyMusic()
        {
            audioModel = await VkAudio.GetMyAudiosAsync(1000);

            ListAudio.ItemsSource = audioModel.Items;
        }

        //загрузка моих аудиозаписей
        public async void FindMusic()
        {
            
            audioModel = await VkAudio.SearchAudiosAsync(TextAudio.Text);

            ListAudio.ItemsSource = audioModel.Items;
        }

        private void ListAudio_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Play(ListAudio.SelectedIndex);
           
        }

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
           
            TrekState.Value = d;

            TrekState.SelectionEnd = Absolution(int.Parse(((Bass.BASS_StreamGetFilePosition(stream, BASSStreamFilePosition.BASS_FILEPOS_DOWNLOAD).ToString().Split(',', '.'))[0])));
            //TrekState.SelectionEnd = Bass.BASS_StreamGetFilePosition(stream, BASSStreamFilePosition.BASS_FILEPOS_DOWNLOAD);
           // Bass.BASS_StreamGetFilePosition(stream, BASSStreamFilePosition.BASS_FILEPOS_DOWNLOAD);
           //автоматическое переключение песен после завершения предыдущей
            if (TrekState.Value == TrekState.Maximum)
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

        private void ListAudio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

      
        private void TrekState_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TrekState.Value = (int)TrekState.Maximum * e.GetPosition(TrekState).X / TrekState.Width;
            Bass.BASS_ChannelSetPosition(stream, (double)TrekState.Value);
        }

      
        private void TrekState_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
           double TrackCurrPos = Math.Round(Bass.BASS_ChannelBytes2Seconds(stream, Bass.BASS_ChannelGetPosition(stream, 0)));
            if (TrekState.Value != TrackCurrPos && TrekState.Value != TrackCurrPos - 1)
            {
                Bass.BASS_ChannelSetPosition(stream, TrekState.Value);
            }
           
            
        }

        /// <summary>
        /// Начать воспроизведение трека
        /// </summary>
        public void Play(int index)
        {
            
            //if (stream != 0)
            //{
            //    stream = 0;
            Bass.BASS_StreamFree(stream);
            //    Bass.BASS_MusicFree(stream);
            //    Bass.BASS_SampleFree(stream);
            //    Bass.BASS_SampleStop(stream);
            //    Bass.BASS_Stop();
            //}

            Bass.BASS_Start();

            if (ListAudio.SelectedIndex < 0)
                return;
           // _myDownloadProc = new DOWNLOADPROC(MyDownload);
            stream = Bass.BASS_StreamCreateURL(audioModel.Items[index].Url, 0, BASSFlag.BASS_DEFAULT, null, IntPtr.Zero);

            if (stream != 0 && Bass.BASS_ChannelPlay(stream, false))
            {
                Bass.BASS_ChannelSetAttribute(stream, BASSAttribute.BASS_ATTRIB_VOL, (float)Volime.Value / 100);
                TrekState.Maximum = int.Parse(((Bass.BASS_ChannelBytes2Seconds(stream, Bass.BASS_ChannelGetLength(stream)).ToString().Split(',', '.'))[0]));
                TrekState.SelectionEnd = 0;
                timerStart();
            }
        }

        private void MyDownload(IntPtr buffer, int length, IntPtr user)
        {
            if (_fs == null)
            {
                // create the file
               // _fs = File.OpenWrite("output.mp3");
            }
            if (buffer == IntPtr.Zero)
            {
                // finished downloading
                _fs.Flush();
                _fs.Close();
            }
            else
            {
                // increase the data buffer as needed 
                if (_data == null || _data.Length < length)
                    _data = new byte[length];
                // copy from managed to unmanaged memory
                Marshal.Copy(buffer, _data, 0, length);
                // write to file
               // _fs.Write(_data, 0, length);
            }
        }

       

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Play(ListAudio.SelectedIndex);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
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

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Bass.BASS_StreamFree(stream);
        }

        private void Volime_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Bass.BASS_ChannelSetAttribute(stream, BASSAttribute.BASS_ATTRIB_VOL, (float)Volime.Value/100);
        }

        private void MainWindow1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveSettings();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadMyMusic();
        }

        private async void CheckOnline_Checked(object sender, RoutedEventArgs e)
        {
            userModel = await VkFriends.GetMyFriendsAsync();
            userModel.Items = userModel.Items.FindAll(user => user.OnlineNorm == "Online");
            ListFriend.ItemsSource = userModel.Items;

        }

        private async void CheckOnline_Unchecked(object sender, RoutedEventArgs e)
        {
            userModel = await VkFriends.GetMyFriendsAsync();
            ListFriend.ItemsSource = userModel.Items;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            FindMusic();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            VkMessage.GetDialogs();
        }

        

      

      

        

       

        

    
       
    }
}
