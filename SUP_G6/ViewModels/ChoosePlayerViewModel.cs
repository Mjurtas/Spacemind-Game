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
        public string Level { get; set; }
        public bool EasyRadioButton { get; set; } = true;
        public bool MediumRadioButton { get; set; } = false;
        public bool HardRadioButton { get; set; } = false;

        public ChoosePlayerViewModel()
        {
            //CreateGameCommand = new RelayCommand(CreateGame);
            //CreateMediumGameCommand = new RelayCommand(CreateMediumGame);
            //CreateHardGameCommand = new RelayCommand(CreateHardGame);
            

        }

        public void CreateGame()
        {
            if (EasyRadioButton)
            {
                Level = "Easy";
            }
            else if (MediumRadioButton)
            {
                Level = "Medium";
            }
            else
            {
                Level = "Hard";
            }

        }
    }
}
