using System.ComponentModel;
using System.Windows;
using System.Windows.Interop;
using VK.Properties;
using VK.View;
using VK.ViewModel.Audios;
using VK.ViewModel.Dialogs;
using VK.ViewModel.Friends;
using VK.ViewModel.Page;
using VKAPI;
using Xceed.Wpf.AvalonDock.Layout;

namespace VK
{
    /// <summary>
    ///     Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AudioListViewModel audiolist;
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
         }

        /// <summary>
        ///     загрузка настроек
        /// </summary>
        public void LoadSettings()
        {

        }

        private void MainWindow1_Closing(object sender, CancelEventArgs e)
        {

        }

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
                //создаем viewmodel если ее еще нет
                
                if (audiolist == null)
                {
                    audiolist = new AudioListViewModel();
                }

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
            if (FindTab("MyPage") == -1)
            {
                //создаем view
                var pv = new PageView();
                //создаем viewmodel
                var page = new PageViewModel();
                pv.DataContext = page;
                //помещаем view на вкладку
                var ld = new LayoutDocument();
                ld.Title = "Моя страница";
                ld.ContentId = "MyPage";
                ld.Content = pv;
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
            //    var dm = await VkMessage.GetDialogsAsync();
            //    //передаем для ссылку на сам таб кантролл и модель данных
            //    var Ac = new DialogControl(dm);
            //    var ld = new LayoutDocument();
            //    ld.Title = "Мои сообщения";
            //    ld.ContentId = "MyMessage";
            //    ld.Content = Ac;
            //    LayoutDocumentP.Children.Add(ld);
            //    LayoutDocumentP.Children[FindTab("MyMessage")].IsSelected = true;
            //}
            //else
            //{
            //    LayoutDocumentP.Children[FindTab("MyMessage")].IsSelected = true;
            //}

            //создаем view
            var dv = new DialogsView();
            //создаем viewmodel
            var dialoglist = new DialogListViewModel();
            dv.DataContext = dialoglist;
            //помещаем view на вкладку
            var ld = new LayoutDocument();
            ld.Title = "Мои сообщения";
            ld.ContentId = "MyMessage";
            ld.Content = dv;
            LayoutDocumentP.Children.Add(ld);
            LayoutDocumentP.Children[FindTab("MyMessage")].IsSelected = true;
        }

        private async void MyFriendsButton_Click(object sender, RoutedEventArgs e)
        {
            if (FindTab("MyFriends") == -1)
            {
                //создаем view
                var fv = new FriendsView();
                var friendlist = new FriendsListViewModel();
                fv.DataContext = friendlist;
                //помещаем view на вкладку
                var ld = new LayoutDocument();
                ld.Title = "Мои друзья";
                ld.ContentId = "MyFriends";
                ld.Content = fv;
                LayoutDocumentP.Children.Add(ld);
                LayoutDocumentP.Children[FindTab("MyFriends")].IsSelected = true;
            }
            else
            {
                LayoutDocumentP.Children[FindTab("MyFriends")].IsSelected = true;
            }
            
        }
    }
}