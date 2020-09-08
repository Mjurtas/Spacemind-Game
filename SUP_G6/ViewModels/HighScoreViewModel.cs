using SUP_G6.Models;
using SUP_G6.Other;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SUP_G6.ViewModels
{
    public class HighScoreViewModel
    {
        //public ObservableCollection<Player> Test { get; set; } = (ObservableCollection<Player>)DataBaseLogic.GetPlayers();
        public List<Player> Players { get; set; } = (List<Player>)DataBaseLogic.GetPlayers();

    }
}
