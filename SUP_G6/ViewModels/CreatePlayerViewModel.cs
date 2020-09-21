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
        public string Name { get; set; }
        public Player Player { get; set; }
        public string CreatePlayerButton { get; set; } = "create player";
        public string BackButton { get; set; } = "back";

        public ICommand CreatePlayerCommand { get; set; }
        public ICommand BackButtonCommand { get; set; }


        public CreatePlayerViewModel()
        {

            CreatePlayerCommand = new RelayCommand(CreatePlayer);
            BackButtonCommand = new RelayCommand(BackToStart);
        }

        private void BackToStart()
        {
            var page1 = new StartPage();
            ((MainWindow)Application.Current.MainWindow).Main.Content = page1;
        }

        public void CreatePlayer()
        {
            // limits på namn. Längd, tecken, finns redan i listan? UNIQUE try catch?

            Player player = new Player
            {
                
            Name = this.Name
                
            };
            Player = player;
            Player.Id = DataBaseLogic.AddPlayer(player);
            


            var page = new SelectLevelPage(player);
            ((MainWindow)Application.Current.MainWindow).Main.Content = page;

        }

    }
}
