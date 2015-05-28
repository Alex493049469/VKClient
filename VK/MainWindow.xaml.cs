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
using VK.Module.Friends;
using VK.Module.Message;
using VK.Module.Page;
using VK.Properties;
using VKAPI;
using VKAPI.Model;
using VKAPI.Model.AudioModel;
using VKAPI.Model.DialogsModel;
using VKAPI.Model.FriendsModel;
using VKAPI.Model.UsersModel;


namespace VK
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
        }

        private void MainWindow1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Bass.FreeMe();
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
                ClosableTab asdf = (ClosableTab)TabControler.Items[i];
                if (asdf.Name == name)
                {
                    finded = i;
                }
            }
            return finded;
        }

        private void AudioButton_Click(object sender, RoutedEventArgs e)
        {
            if (FindTab("MyAudio") == -1)
            {
                AudioControl Ac = new AudioControl();

                ClosableTab theTabItem = new ClosableTab();
                theTabItem.Name = "MyAudio";
                theTabItem.Title = "Мои аудиозаписи";
                theTabItem.Content = Ac;
                TabControler.Items.Add(theTabItem);
                theTabItem.Focus();
            }
            else
            {
                TabControler.SelectedIndex = FindTab("MyAudioItem");
            }

        }

        private void MyPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (FindTab("MyPage") == -1)
            {
                PageControl Ac = new PageControl();
                ClosableTab theTabItem = new ClosableTab();
                theTabItem.Name = "MyPage";
                theTabItem.Title = "Моя страница";
                theTabItem.Content = Ac;
                TabControler.Items.Add(theTabItem);
                theTabItem.Focus();
            }
            else
            {
                TabControler.SelectedIndex = FindTab("MyPage");
            }
        }


        private void MyMessageButton_Click(object sender, RoutedEventArgs e)
        {
            if (FindTab("MyMessage") == -1)
            {
                DialogControl Ac = new DialogControl();

                ClosableTab theTabItem = new ClosableTab();
                theTabItem.Name = "MyMessage";
                theTabItem.Title = "Мои сообщения";
                theTabItem.Content = Ac;
                TabControler.Items.Add(theTabItem);
                theTabItem.Focus();
            }
            else
            {
                TabControler.SelectedIndex = FindTab("MyMessage");
            }
        }

        private void MyFriendsButton_Click(object sender, RoutedEventArgs e)
        {
            if (FindTab("MyFriends") == -1)
            {
                FriendsControl Ac = new FriendsControl();

                ClosableTab theTabItem = new ClosableTab();
                theTabItem.Name = "MyFriends";
                theTabItem.Title = "Мои друзья";
                theTabItem.Content = Ac;
                TabControler.Items.Add(theTabItem);
                theTabItem.Focus();
            }
            else
            {
                TabControler.SelectedIndex = FindTab("MyFriends");
            }
        }



    }

}

        

      

      

        

       

        

    
       

