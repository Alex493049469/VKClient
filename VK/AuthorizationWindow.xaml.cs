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
using System.Windows.Shapes;
using VK.Properties;
using VK.View;
using VKAPI;

namespace VK
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public MainWindow main;
        public AuthorizationWindow()
        {
            InitializeComponent();
            //загружаем страницу авторизации
            BrowserAuth.Navigate(VkSettings.vk_auth_url);
        }

        //Событие срабатывает при полной загрузке странице и автоматом ищет токен если токен найден то закрываем форму и отображает главное окно программы
        private void BrowserAuth_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            //получаем url и парсим его 
            string url = Convert.ToString(BrowserAuth.Source);

            //если в url лежит access token значит пользователь авторизован
            //парсим и сохраняем токен
            if (url.Contains("access_token="))
            {
                int i, j;
                i = url.IndexOf("access_token=");
                j = url.IndexOf("expires_in");

                url = url.Remove(j);
                url = url.Remove(0, i + 13);

                Settings.Default.token = url;
                Settings.Default.Save();
                VkSettings.Token = Settings.Default.token;

                main.Visibility = Visibility.Visible;
                this.Close();
            }
        }

        //если закрываем форму и нет токена то закрываем все приложение
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Settings.Default.token == "")
            {
                main.Close();
            }
        }


    }
}
