using SUP_G6.Models;
using SUP_G6.Other;
using System;
using System.Collections.Generic;
using System.Text;

namespace SUP_G6.ViewModels
{
    class ChoosePlayerViewModel
    {
        public List<Player> Players { get; set; } = (List<Player>)DataBaseLogic.GetPlayers();
    }
}
