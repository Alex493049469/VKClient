using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using VK.ViewModel.Friends;
using VKAPI;
using VKAPI.Model.UsersModel;

namespace VK.ViewModel.Page
{
    class PageViewModel : BaseViewModel
    {
        //модель данных
        public UsersModel UserModel;

        public PageViewModel()
        {
            LoadModel();
        }

        public async void LoadModel()
        {
            //грузим данные в модель
             UserModel =  VkUsers.Get("", "", VkUsers.name_case.nom);

           


            //this.DataContext = userModel.response;

            //FriendsButton.Content = "Друзья (" + userModel.response[0].counters.friends + ")";
            //GroupButton.Content = "Группы (" + userModel.response[0].counters.groups + ")";
            //PhotosButton.Content = "Фото (" + userModel.response[0].counters.photos + ")";
            //AudiosButton.Content = "Аудио (" + userModel.response[0].counters.audios + ")";
            //VideoButton.Content = "Видео (" + userModel.response[0].counters.videos + ")";

            //BirthdayLabel.Content += " " + userModel.response[0].bdate;
            //TownLabel.Content += " " + userModel.response[0].city.title;

            ////тут тоже необходимо использовать конвертер
            //if (userModel.response[0].relation == 7)
            //{
            //    FamilyStatusLabel.Content += " Влюблен в " + userModel.response[0].relation_partner.first_name + " " + userModel.response[0].relation_partner.last_name;
            //}

            //EducationLabel.Content += " " + userModel.response[0].university_name;

        }

 
        public string CountFriends
        {
            get
            {
                if (UserModel != null)
                {
                    return "Друзья (" + UserModel.response[0].counters.friends + ")";
                }
                return "Друзья()";
            }
            
        }

        public string CountGroups
        {
            get
            {
                if (UserModel != null)
                {
                    return "Группы (" + UserModel.response[0].counters.groups + ")";
                }
                return "Группы()";
            }
        }

        public string CountPhotos
        {
            get
            {
                if (UserModel != null)
                {
                    return "Фото (" + UserModel.response[0].counters.photos + ")";
                }
                return "Фото()";
            }
        }

        public string CountAudios
        {
            get
            {
                if (UserModel != null)
                {
                    return "Аудио (" + UserModel.response[0].counters.audios + ")";
                }
                return "Аудио()";
            }
        }

        public string CountVideos
        {
            get
            {
                if (UserModel != null)
                {
                    return "Видео (" + UserModel.response[0].counters.videos + ")";
                }
                return "Видео()";
            }
        }

        public string Photo
        {
            get
            {
               
                    return UserModel.response[0].photo_200_orig;
                
            }
        }
    }
}
