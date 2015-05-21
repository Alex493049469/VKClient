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
using VKAPI;
using VKAPI.Model;

namespace VK.Module.Page
{
    /// <summary>
    /// Логика взаимодействия для PageControl.xaml
    /// </summary>
    public partial class PageControl : UserControl
    {
        public UserModel userModel;

        public PageControl()
        {
            InitializeComponent();

            LoadUserInfo();
        }

        /// <summary>
        /// грущит информацию о текущем пользователе
        /// </summary>
        public async void LoadUserInfo()
        {
            userModel = await VkUser.GetAsync();
            AvatarImage.DataContext = userModel.user;

        }

    }
}
