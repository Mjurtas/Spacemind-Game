using SUP_G6.Interface;
using SUP_G6.Models;
using SUP_G6.Other;
using SUP_G6.ViewModels.BaseViewModel;
using SUP_G6.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SUP_G6.ViewModels
{
    public class CreatePlayerViewModel : BaseViewModel.BaseViewModel
    {
        private Player player;

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
            Player = this.player;
            DataBaseLogic.AddPlayer(player);
            
 
        }

    }
}
