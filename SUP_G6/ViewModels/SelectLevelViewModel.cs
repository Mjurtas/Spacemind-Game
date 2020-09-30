using SUP_G6.DataTypes;
using SUP_G6.Interface;
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
        #region Constructor
        public SelectLevelViewModel(IPlayer player)
        {
            this.Player = player;
            StartGameCommand = new RelayCommand(StartGame);
            BackButtonCommand = new RelayCommand(BackToStart);
        }
        #endregion

        #region Properties
        public IPlayer Player { get; set; }
        public Level Level { get; set; }
        public bool EasyRadioButton { get; set; } = true;
        public bool MediumRadioButton { get; set; } = false;
        public bool HardRadioButton { get; set; } = false;

        #region ContentBindings
        public string ButtonPlayGame { get; set; } = "play game";
        public string RadioButtonEasy { get; set; } = "easy";
        public string RadioButtonMedium { get; set; } = "medium";
        public string RadioButtonHard { get; set; } = "hard";
        public string BackButton { get; set; } = "back to start";
        public string ChoosePlayerLabel { get; set; } = "choose your player";
        #endregion
        #endregion

        #region ICommand
        public ICommand StartGameCommand { get; set; }
        public ICommand BackButtonCommand { get; set; }

        #endregion

        #region SelectLevelMethods

        // Sets chosen level and passes it to GamePlayPage along with chosen player.
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
        private void BackToStart()
        {
            var page1 = new StartPage();
            ((MainWindow)Application.Current.MainWindow).Main.Content = page1;
        }
        #endregion
    }
}
