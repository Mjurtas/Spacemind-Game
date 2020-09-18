using SUP_G6.DataTypes;
using SUP_G6.Models;
using SUP_G6.ViewModels.BaseViewModel;
using SUP_G6.Views;
using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace SUP_G6.ViewModels
{
    class SelectLevelViewModel
    {
        public Player player { get; set; }
        public Level Level { get; set; }
        public bool EasyRadioButton { get; set; } = true;
        public bool MediumRadioButton { get; set; } = false;
        public bool HardRadioButton { get; set; } = false;
        public ICommand StartGameCommand{ get; set; }

        public SelectLevelViewModel(Player player)
        {
            this.player = player;
            StartGameCommand = new RelayCommand(StartGame);
        }

        public void CreateGame()
        {
            if (EasyRadioButton)
            {
                Level = Level.Easy;
            }
            else if (MediumRadioButton)
            {
                Level = Level.Medium;
            }
            else
            {
                Level = Level.Hard;
            }
        }

        public void StartGame()
        {
            if (player != null)
            {
                CreateGame();
                Level level = Level;
                var page = new GamePlayPage(player, level);
                ((MainWindow)Application.Current.MainWindow).Main.Content = page;
            }
        }
    }
}
