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
using VK.Module.Audio;
using VK.Module.Message;
using VK.Module.Page;
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
        private int stream;
        //источники данных
        public AudioModel audioModel;
        public FriendsModel friendsModel;
        public MessageModel messageModel;
        public UserModel userModel;


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

            //инициализация библиотеки bass для проигрывания аудио
            Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT,
                new System.Windows.Interop.WindowInteropHelper(this).Handle);
            LoadFriends();

        }



        //загружаем список друзей
        public async void LoadFriends()
        {
            friendsModel = await VkFriends.GetAsync();
            ListFriend.ItemsSource = friendsModel.Items;
        }





        private void ListAudio_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {


        }







        private void ListAudio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }





        private void TrekState_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {



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



        private void MainWindow1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            Bass.FreeMe();
        }



        private async void CheckOnline_Checked(object sender, RoutedEventArgs e)
        {
            friendsModel = await VkFriends.GetAsync();
            friendsModel.Items = friendsModel.Items.FindAll(user => user.OnlineNorm == "Online");
            ListFriend.ItemsSource = friendsModel.Items;

        }

        private async void CheckOnline_Unchecked(object sender, RoutedEventArgs e)
        {
            friendsModel = await VkFriends.GetAsync();
            ListFriend.ItemsSource = friendsModel.Items;
        }



        private async void Button_Click_5(object sender, RoutedEventArgs e)
        {
          
        }

        private void AudioButton_Click(object sender, RoutedEventArgs e)
        {

            AudioControl Ac = new AudioControl();

            TabItem ti = new TabItem();
            ti.Header = "Аудиозаписи";
            ti.Name = "MyAudioItem";
            ti.Content = Ac;

            //если вкладки нет то добавляем если есть то делаем ее активной

            if (FindTab("MyAudioItem") == -1)
            {
                TabControler.Items.Add(ti);
                TabControler.SelectedIndex = TabControler.Items.Count - 1;
            }
            else
            {
                TabControler.SelectedIndex = FindTab("MyAudioItem");
            }


        }
        /// <summary>
        /// находит вкладку с именем и весли найдена возвращает индекс
        /// </summary>
        /// <returns></returns>
        public int FindTab(string name)
        {
            int finded = -1;
            for (int i = 0; i < TabControler.Items.Count; i++)
            {
                TabItem asdf = (TabItem)TabControler.Items[i];
                if (asdf.Name == name )
                {
                    finded = i;
                }
            }
            return finded;
        }

        private void MyPageButton_Click(object sender, RoutedEventArgs e)
        {
            PageControl Ac = new PageControl();

            TabItem ti = new TabItem();
            ti.Header = "Моя страница";
            ti.Name = "MyPage";
            ti.Content = Ac;

            //если вкладки нет то добавляем если есть то делаем ее активной

            if (FindTab("MyPage") == -1)
            {
                TabControler.Items.Add(ti);
                TabControler.SelectedIndex = TabControler.Items.Count - 1;
            }
            else
            {
                TabControler.SelectedIndex = FindTab("MyPage");
            }
        }

        private void MyMessageButton_Click(object sender, RoutedEventArgs e)
        {
            MessageControl Ac = new MessageControl();

            TabItem ti = new TabItem();
            ti.Header = "Сообщения";
            ti.Name = "MyMessage";
            ti.Content = Ac;

            //если вкладки нет то добавляем если есть то делаем ее активной

            if (FindTab("MyMessage") == -1)
            {
                TabControler.Items.Add(ti);
                TabControler.SelectedIndex = TabControler.Items.Count - 1;
            }
            else
            {
                TabControler.SelectedIndex = FindTab("MyMessage");
            }
        }



    }

}

        

      

      

        

       

        

    
       

