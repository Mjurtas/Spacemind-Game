using SUP_G6.Models;
using SUP_G6.Other;
using SUP_G6.ViewModels.BaseViewModel;
using SUP_G6.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace SUP_G6.ViewModels
{
    public class ChoosePlayerViewModel
    {
        public List<Player> Players { get; set; } = (List<Player>)DataBaseLogic.GetPlayers();

        public ICommand CreateEasyGameButtonCommand { get; set;  }        
        public ICommand CreateMediumGameButtonGameCommand { get; set; }
        public ICommand CreateHardGameButtonCommand { get; set; }

        public bool EasyButton { get; set;  }
        public string MediumButton { get; set; } = "MEDIUM";
        public string HardButton { get; set; } = "HARD";


        public ChoosePlayerViewModel()
        {
            //CreateGameCommand = new RelayCommand(CreateGame);
            //CreateMediumGameCommand = new RelayCommand(CreateMediumGame);
            //CreateHardGameCommand = new RelayCommand(CreateHardGame);

            CreateEasyGameButtonCommand = new RelayCommand(CreateEasyGame);
        }

        private void CreateEasyGame()
        {
            

        }
    }
}
