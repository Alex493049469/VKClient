using System.ComponentModel;
using System.Windows;
using System.Windows.Interop;
using Un4seen.Bass;
using VK.Module.Message;
using VK.Module.Page;
using VK.Properties;
using VK.View;
using VK.ViewModel.Audio;
using VKAPI;
using Xceed.Wpf.AvalonDock.Layout;

namespace VK
{
    /// <summary>
    ///     Логика взаимодействия для MainWindow.xaml
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
                Visibility = Visibility.Hidden;
                //отображаем окно авторизации
                var authorization = new AuthorizationWindow();
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
        ///     загрузка настроек
        /// </summary>
        public void LoadSettings()
        {
            //инициализация библиотеки bass для проигрывания аудио
            Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT,
                new WindowInteropHelper(this).Handle);
        }

        private void MainWindow1_Closing(object sender, CancelEventArgs e)
        {
            Bass.FreeMe();
        }

        /// <summary>
        ///     находит вкладку с именем и весли найдена возвращает индекс
        /// </summary>
        /// <returns></returns>
        //public int FindTab(string name)
        //{
        //    int finded = -1;
        //    for (int i = 0; i < TabControler.Items.Count; i++)
        //    {
        //        ClosableTab asdf = (ClosableTab)TabControler.Items[i];
        //        if (asdf.Name == name)
        //        {
        //            finded = i;
        //        }
        //    }
        //    return finded;
        //}
        public int FindTab(string name)
        {
            var finded = -1;
            for (var i = 0; i < LayoutDocumentP.ChildrenCount; i++)
            {
                if (LayoutDocumentP.Children[i].ContentId == name)
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
                //создаем view
                var fv = new AudioView();
                //создаем viewmodel
                var audiolist = new AudioListViewModel();
                fv.DataContext = audiolist;
                //помещаем view на вкладку
                var ld = new LayoutDocument();
                ld.Title = "Мои аудиозаписи";
                ld.ContentId = "MyAudio";
                ld.Content = fv;
                LayoutDocumentP.Children.Add(ld);
                LayoutDocumentP.Children[FindTab("MyAudio")].IsSelected = true;
            }
            else
            {
                LayoutDocumentP.Children[FindTab("MyAudio")].IsSelected = true;
            }
        }

        private async void MyPageButton_Click(object sender, RoutedEventArgs e)
        {
            //if (FindTab("MyPage") == -1)
            //{
            //    //грузим данные в модель
            //    UsersModel userModel = await VkUsers.GetAsync("", "", VkUsers.name_case.nom);
            //    //передаем для ссылку на сам таб кантролл и модель данных
            //    PageControl Ac = new PageControl(TabControler, userModel);
            //    ClosableTab ct = new ClosableTab();
            //    ct.Name = "MyPage";
            //    ct.Title = "Моя страница";
            //    ct.Content = Ac;
            //    TabControler.Items.Add(ct);
            //    ct.Focus();
            //}
            //else
            //{
            //    TabControler.SelectedIndex = FindTab("MyPage");
            //}
            if (FindTab("MyPage") == -1)
            {
                //грузим данные в модель
                var userModel = await VkUsers.GetAsync("", "", VkUsers.name_case.nom);
                //передаем для ссылку на сам таб кантролл и модель данных
                var Ac = new PageControl(userModel);

                var ld = new LayoutDocument();
                ld.Title = "Моя страница";
                ld.ContentId = "MyPage";
                ld.Content = Ac;
                LayoutDocumentP.Children.Add(ld);
                LayoutDocumentP.Children[FindTab("MyPage")].IsSelected = true;
            }
            else
            {
                LayoutDocumentP.Children[FindTab("MyPage")].IsSelected = true;
            }
        }

        private async void MyMessageButton_Click(object sender, RoutedEventArgs e)
        {
            //if (FindTab("MyMessage") == -1)
            //{
            //    //грузим данные в модель
            //    DialogsModel dm= await VkMessage.GetDialogsAsync();
            //    //передаем для ссылку на сам таб кантролл и модель данных
            //    DialogControl Ac = new DialogControl(TabControler, dm);
            //    ClosableTab ct = new ClosableTab();
            //    ct.Name = "MyMessage";
            //    ct.Title = "Мои сообщения";
            //    ct.Content = Ac;
            //    TabControler.Items.Add(ct);
            //    ct.Focus();
            //}
            //else
            //{
            //    TabControler.SelectedIndex = FindTab("MyMessage");
            //}

            if (FindTab("MyMessage") == -1)
            {
                //грузим данные в модель
                var dm = await VkMessage.GetDialogsAsync();
                //    //передаем для ссылку на сам таб кантролл и модель данных
                var Ac = new DialogControl(dm);

                var ld = new LayoutDocument();
                ld.Title = "Мои сообщения";
                ld.ContentId = "MyMessage";
                ld.Content = Ac;
                LayoutDocumentP.Children.Add(ld);
                LayoutDocumentP.Children[FindTab("MyMessage")].IsSelected = true;
            }
            else
            {
                LayoutDocumentP.Children[FindTab("MyMessage")].IsSelected = true;
            }
        }

        private async void MyFriendsButton_Click(object sender, RoutedEventArgs e)
        {
            //if (FindTab("MyFriends") == -1)
            //{
            //    //грузим данные в модель
            //    FriendsModel fm  = await VkFriends.GetAsync();
            //    //передаем для ссылку на сам таб кантролл и модель данных
            //    FriendsControl Ac = new FriendsControl(TabControler, fm);
            //    ClosableTab ct = new ClosableTab();
            //    ct.Name = "MyFriends";
            //    ct.Title = "Мои друзья";
            //    ct.Content = Ac;
            //    TabControler.Items.Add(ct);
            //    ct.Focus();
            //}
            //else
            //{
            //    TabControler.SelectedIndex = FindTab("MyFriends");
            //}

            //if (FindTab("MyFriends") == -1)
            //{
            //    //грузим данные в модель
            //    FriendsModel fm  = await VkFriends.GetAsync();
            //    //передаем для ссылку на сам таб кантролл и модель данных
            //    FriendsControl Ac = new FriendsControl(fm);

            //    LayoutDocument ld = new LayoutDocument();
            //    ld.Title = "Мои друзья";
            //    ld.ContentId = "MyFriends";
            //    ld.Content = Ac;
            //    LayoutDocumentP.Children.Add(ld);
            //    LayoutDocumentP.Children[FindTab("MyFriends")].IsSelected = true;
            //}
            //else
            //{
            //    LayoutDocumentP.Children[FindTab("MyFriends")].IsSelected = true;
            //}


            //создаем view
            var fv = new FriendsView();
            //помещаем view на вкладку
            var ld = new LayoutDocument();
            ld.Title = "Мои друзья";
            ld.ContentId = "MyFriends";
            ld.Content = fv;
            LayoutDocumentP.Children.Add(ld);
            LayoutDocumentP.Children[FindTab("MyFriends")].IsSelected = true;
        }
    }
}