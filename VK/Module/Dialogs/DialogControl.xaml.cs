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
using VKAPI.Model.DialogsModel;
using VKAPI.Model.UsersModel;

namespace VK.Module.Message
{
    /// <summary>
    /// Логика взаимодействия для MessageControl.xaml
    /// </summary>
    public partial class DialogControl : UserControl
    {
        public UsersModel userModel;
        public DialogsModel messageModel;
        public DialogControl()
        {
            InitializeComponent();

            LoadMyMessage();
           
        }

        public async void LoadMyMessage()
        {
            userModel = await VkUsers.GetAsync("", "sex", VkUsers.name_case.nom);
            messageModel = await VkMessage.GetDialogsAsync();

            ListMessage.ItemsSource = messageModel.response.items;
           // MyPageButton.DataContext = userModel.user;
         
        }
    }
}
