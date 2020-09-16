using SUP_G6.Interface;
using SUP_G6.Models;
using SUP_G6.Other;
using SUP_G6.ViewModels.BaseViewModel;
using SUP_G6.Views;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;


namespace SUP_G6.ViewModels
{
    public class CreatePlayerViewModel : BaseViewModel.BaseViewModel
    {
        public Player Player { get; set; }
        public ICommand CreatePlayerCommand { get; set; }
        public string Name { get; set; }


        public CreatePlayerViewModel()
        {

            CreatePlayerCommand = new RelayCommand(CreatePlayer);
        }
        


        public void CreatePlayer()
        {
            Player player = new Player
            {
                Name = this.Name
                
            };
            Player = player;
            DataBaseLogic.AddPlayer(player);

            var page = new ChoosePlayerPage();
            ((MainWindow)Application.Current.MainWindow).Main.Content = page;

        }

    }
}
