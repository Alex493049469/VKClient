using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using VKAPI;
using VKAPI.Model.DialogsModel;

namespace VK.ViewModel.Dialogs
{
    class DialogListViewModel : BaseViewModel
    {
        //модель данных
        public DialogsModel _dialogModel;

        //ViewModel Диалогов
        private ObservableCollection<DialogItemViewModel> _dialogItemsViewModel;


        public DialogListViewModel()
        {
            LoadDialogs();
        }

        public async void LoadDialogs()
        {
            DialogItemsViewModel = null;
            ObservableCollection<DialogItemViewModel> _item = new ObservableCollection<DialogItemViewModel>();
            _dialogModel = await VkMessage.GetDialogsAsync();
            foreach (var item in _dialogModel.response.items)
            {
                DialogItemViewModel itemAudio = new DialogItemViewModel() { Item = item.message };
                _item.Add(itemAudio);
            }
            DialogItemsViewModel = _item;
        }

        public ObservableCollection<DialogItemViewModel> DialogItemsViewModel
        {
            get { return _dialogItemsViewModel; }
            set
            {
                if (_dialogItemsViewModel == value)
                    return;

                _dialogItemsViewModel = value;

                OnPropertyChanged();
            }
        }

    }
}
