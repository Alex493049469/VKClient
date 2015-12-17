using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using VKAPI;
using VKAPI.Model.DialogsModel;
using VKAPI.Model.UsersModel;

namespace VK.ViewModel.Dialogs
{
    class DialogListViewModel : BaseViewModel
    {
        //модель данных диалогов
        public DialogsModel _dialogModel;

        //ViewModel Диалогов
        private ObservableCollection<DialogItemViewModel> _dialogItemsViewModel = new ObservableCollection<DialogItemViewModel>();

        //для доступа к данным диалогов
        VkMessage vkMessage = new VkMessage();
        //для подгрузки фотографий пользователей к диалогам
        VkUsers vkUsers = new VkUsers();

        public DialogListViewModel()
        {
            LoadDialogs();
        }

        public async void LoadDialogs()
        {
            DialogItemsViewModel = null;
            _dialogModel = await vkMessage.GetDialogsAsync();

           ObservableCollection<DialogItemViewModel> itemsDialog = new ObservableCollection<DialogItemViewModel>();
            foreach (var item in _dialogModel.response.items)
            {
                DialogItemViewModel itemDialog = new DialogItemViewModel()
                {
                    Title = item.message.title,
                    UserId = item.message.user_id,
                    Body = item.message.body,
                    GroupPhoto100 = item.message.photo_100,
                    GroupPhoto200 = item.message.photo_200,
                    GroupPhoto50 = item.message.photo_50,
                    UserCount = item.message.users_count,
                    ChatActive = item.message.chat_active,
                    ReadState = item.message.read_state,
                    Date = item.message.date
                  
                };

                itemsDialog.Add(itemDialog);
            }
            //если несколько пользователей то берем их из ChatActive если 1 то userId
            //собираем все их id 
            List<int> UserIdList = new List<int>();
            foreach (var item in itemsDialog)
            {
                if (item.UserCount == 1 || item.UserCount == null)
                {
                    if (!UserIdList.Contains(item.UserId))
                    {
                        UserIdList.Add(item.UserId);
                    }
                }
                if (item.UserCount > 1 )
                {
                    foreach (var id in item.ChatActive)
                    {
                        if (!UserIdList.Contains(id))
                        {
                            UserIdList.Add(id);
                        }
                    }
                }
            }

            string UserIds = "";
            foreach (var id in UserIdList)
            {
                if (UserIds == "")
                {
                    UserIds += id;
                }
                else
                {
                    UserIds += "," + id;
                }
            }
            //получаем всю необходимую информацию о пользовалелях кто в диалогах 
            UsersModel users = await vkUsers.GetPhotoAsync(UserIds);
           
            //здесь в зависимости от количества собеседников подгружаем фотки
            foreach (var item in itemsDialog)
            {
                if (item.ChatActive == null || item.UserCount == null)
                {
                    var userTemp = users.response.Find(i => i.id == item.UserId);
                    item.UserOnePhoto = userTemp.photo_100;
                    item.Title = userTemp.first_name + " " + userTemp.last_name;
                    continue;
                }

                var user = users.response.Find(i => i.id == item.UserId);
                if (user != null)
                    item.UserIdPhoto = user.photo_100;
                if (item.ChatActive.Count == 2)
                {
                    item.UserOnePhoto = users.response.Find(i => i.id == item.ChatActive[0]).photo_100;
                    item.UserTwoPhoto = users.response.Find(i => i.id == item.ChatActive[1]).photo_100;
                    continue;
                }
                if (item.ChatActive.Count == 3)
                {
                    item.UserOnePhoto = users.response.Find(i => i.id == item.ChatActive[0]).photo_100;
                    item.UserTwoPhoto = users.response.Find(i => i.id == item.ChatActive[1]).photo_100;
                    item.UserThreePhoto = users.response.Find(i => i.id == item.ChatActive[2]).photo_100;
                    continue;
                }
               
                if (item.ChatActive.Count >= 4)
                {
                    item.UserOnePhoto = users.response.Find(i => i.id == item.ChatActive[0]).photo_100;
                    item.UserTwoPhoto = users.response.Find(i => i.id == item.ChatActive[1]).photo_100;
                    item.UserThreePhoto = users.response.Find(i => i.id == item.ChatActive[2]).photo_100;
                    item.UserFourPhoto = users.response.Find(i => i.id == item.ChatActive[3]).photo_100;

                }
            }

            DialogItemsViewModel = itemsDialog;
        }

        public ObservableCollection<DialogItemViewModel> DialogItemsViewModel
        {
            get { return _dialogItemsViewModel; }
            set
            {
                if (_dialogItemsViewModel == value)
                    return;

                _dialogItemsViewModel = value;

                RaisePropertyChanged();
            }
        }

        public DialogItemViewModel ItemSelected { get; set; }

    }
}
