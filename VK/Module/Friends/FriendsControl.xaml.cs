﻿using System;
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
using VKAPI.Model.FriendsModel;

namespace VK.Module.Friends
{
    /// <summary>
    /// Логика взаимодействия для FriendsControl.xaml
    /// </summary>
    public partial class FriendsControl : UserControl
    {
        //модель данных
        public FriendsModel friendsModel;
        //ссылка на таб контролл
        public TabControl tabControler;


        public FriendsControl( FriendsModel fm)
        {
            InitializeComponent();
            friendsModel = fm;
            //tabControler = tc;
            BindModel();
        }

        /// <summary>
        /// привязки данных
        /// </summary>
        public void BindModel()
        {
            ListFriend.ItemsSource = friendsModel.response.items;
        }
    }
}