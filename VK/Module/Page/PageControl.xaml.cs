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
using VK.Module.Friends;
using VKAPI;
using VKAPI.Model;
using VKAPI.Model.UsersModel;

namespace VK.Module.Page
{
    /// <summary>
    /// Логика взаимодействия для PageControl.xaml
    /// </summary>
    public partial class PageControl : UserControl
    {
        //модель данных
        public UsersModel userModel;
        //ссылка на таб контролл
        public TabControl tabControler;

        public PageControl(UsersModel um)
        {
            InitializeComponent();

           // tabControler = tc;
            userModel = um;
            BindModel();
        }

        /// <summary>
        /// привязки данных
        /// </summary>
        public void BindModel()
        {
            this.DataContext = userModel.response;

            FriendsButton.Content= "Друзья (" +userModel.response[0].counters.friends + ")";
            GroupButton.Content = "Группы (" + userModel.response[0].counters.groups + ")";
            PhotosButton.Content = "Фото (" + userModel.response[0].counters.photos + ")";
            AudiosButton.Content = "Аудио (" + userModel.response[0].counters.audios + ")";
            VideoButton.Content = "Видео (" + userModel.response[0].counters.videos + ")";

            BirthdayLabel.Content += " "+ userModel.response[0].bdate;
            TownLabel.Content += " " + userModel.response[0].city.title;

            //тут тоже необходимо использовать конвертер
            if (userModel.response[0].relation == 7)
            {
                FamilyStatusLabel.Content += " Влюблен в " + userModel.response[0].relation_partner.first_name +" "+ userModel.response[0].relation_partner.last_name;
            }

            EducationLabel.Content += " " + userModel.response[0].university_name;
 
        }

        private void FriendsButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void GroupButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PhotosButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AudiosButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void VideoButton_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
