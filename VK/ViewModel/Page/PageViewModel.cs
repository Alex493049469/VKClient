using Core;
using VKAPI.Category;
using VKAPI.Model.UsersModel;

namespace VK.ViewModel.Page
{
	public class PageViewModel : PaneViewModel
    {
        //модель данных
        public UsersModel UserModel;
        //для доступа к пользователям
        Users vkusers = new Users();

        public PageViewModel()
        {
			Title = "Моя страница";
            LoadModel();
        }

        public async void LoadModel()
        {
            //грузим данные в модель
            UserModel = vkusers.Get("", "", Users.nameCase.nom);
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

        public string Birthday
        {
            get
            {
                return "День рождения: " + UserModel.response[0].bdate;
            }
        }

        public string Town
        {
            get
            {
                return "Город: " + UserModel.response[0].city.title;
            }
        }


        public string FamilyStatus
        {
            get
            {
                return "Семейное положение: Влюблен в " + UserModel.response[0].relation_partner.first_name + " " + UserModel.response[0].relation_partner.last_name;
            }
        }

        public string Education
        {
            get
            {
                return "Образование: " + UserModel.response[0].university_name;
            }
        }

        public string Status
        {
            get
            {
                return UserModel.response[0].status;
            }
            set
            {
                UserModel.response[0].status = value;
                RaisePropertyChanged();
            }
        }

        public string FullName
        {
            get
            {
                return UserModel.response[0].first_name + " " +UserModel.response[0].last_name;
            }
        }
    }
}
