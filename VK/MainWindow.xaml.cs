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

        private async void AudioButton_Click(object sender, RoutedEventArgs e)
        {
            if (FindTab("MyAudio") == -1)
            {
                //грузим данные в модель
                AudioModel am = await VkAudio.GetAsync(1000);
                //передаем для ссылку на сам таб кантролл и модель данных
                AudioControl Ac = new AudioControl(TabControler, am);
                ClosableTab сt = new ClosableTab();
                сt.Name = "MyAudio";
                сt.Title = "Мои аудиозаписи";
                сt.Content = Ac;
                TabControler.Items.Add(сt);
                сt.Focus();
            }
            else
            {
                TabControler.SelectedIndex = FindTab("MyAudio");
            }

        }

        private async void MyPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (FindTab("MyPage") == -1)
            {
                //грузим данные в модель
                UsersModel userModel = await VkUsers.GetAsync("", "", VkUsers.name_case.nom);
                //передаем для ссылку на сам таб кантролл и модель данных
                PageControl Ac = new PageControl(TabControler, userModel);
                ClosableTab ct = new ClosableTab();
                ct.Name = "MyPage";
                ct.Title = "Моя страница";
                ct.Content = Ac;
                TabControler.Items.Add(ct);
                ct.Focus();
            }
            else
            {
                TabControler.SelectedIndex = FindTab("MyPage");
            }
        }


        private async void MyMessageButton_Click(object sender, RoutedEventArgs e)
        {
            if (FindTab("MyMessage") == -1)
            {
                //грузим данные в модель
                DialogsModel dm= await VkMessage.GetDialogsAsync();
                //передаем для ссылку на сам таб кантролл и модель данных
                DialogControl Ac = new DialogControl(TabControler, dm);
                ClosableTab ct = new ClosableTab();
                ct.Name = "MyMessage";
                ct.Title = "Мои сообщения";
                ct.Content = Ac;
                TabControler.Items.Add(ct);
                ct.Focus();
            }
            else
            {
                TabControler.SelectedIndex = FindTab("MyMessage");
            }
        }

        private async void MyFriendsButton_Click(object sender, RoutedEventArgs e)
        {
            if (FindTab("MyFriends") == -1)
            {
                //грузим данные в модель
                FriendsModel fm  = await VkFriends.GetAsync();
                //передаем для ссылку на сам таб кантролл и модель данных
                FriendsControl Ac = new FriendsControl(TabControler, fm);
                ClosableTab ct = new ClosableTab();
                ct.Name = "MyFriends";
                ct.Title = "Мои друзья";
                ct.Content = Ac;
                TabControler.Items.Add(ct);
                ct.Focus();
            }
            else
            {
                TabControler.SelectedIndex = FindTab("MyFriends");
            }
        }



    }

}

        

      

      

        

       

        

    
       

