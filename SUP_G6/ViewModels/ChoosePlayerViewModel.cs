using SUP_G6.Models;
using SUP_G6.Other;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SUP_G6.ViewModels
{
    class ChoosePlayerViewModel
    {
        public List<Player> Players { get; set; } = (List<Player>)DataBaseLogic.GetPlayers();
        public ICommand CurrentPlayer { get; set; }






        
    }
}
