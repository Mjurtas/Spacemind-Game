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
        #region Properties
        public Player Player { get; set; }
        public Level Level { get; set; }
        public bool EasyRadioButton { get; set; } = true;
        public bool MediumRadioButton { get; set; } = false;
        public bool HardRadioButton { get; set; } = false;
        public string ButtonPlayGame { get; set; } = "play game";
        public string RadioButtonEasy { get; set; } = "easy";
        public string RadioButtonMedium { get; set; } = "medium";
        public string RadioButtonHard { get; set; } = "hard";
        #endregion

        #region ICommand
        public ICommand StartGameCommand { get; set; }
        #endregion

        public SelectLevelViewModel(Player player)
        {
            this.Player = player;
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
            if (Player != null)
            {
                CreateGame();
                Level level = Level;
                var page = new GamePlayPage(Player, level);
                ((MainWindow)Application.Current.MainWindow).Main.Content = page;
            }
        }
    }
}
