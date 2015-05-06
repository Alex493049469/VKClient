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

namespace VK.Module.Message
{
    /// <summary>
    /// Логика взаимодействия для MessageControl.xaml
    /// </summary>
    public partial class MessageControl : UserControl
    {
        public UserModel userModel;
        public MessageModel messageModel;
        public MessageControl()
        {
            InitializeComponent();

            LoadMyMessage();
           
        }

        public async void LoadMyMessage()
        {
            userModel = await VkUser.GetAsync();
            messageModel = await VkMessage.GetDialogsAsync();

            ListMessage.ItemsSource = messageModel.Items;
           // MyPageButton.DataContext = userModel.user;
         
        }
    }
}
