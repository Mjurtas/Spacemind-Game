using SUP_G6.DataTypes;
using SUP_G6.Models;
using SUP_G6.Other;
using SUP_G6.ViewModels.BaseViewModel;
using SUP_G6.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace SUP_G6.ViewModels
{
    public class ChoosePlayerViewModel : BaseViewModel.BaseViewModel
    {
        #region Properties
        public ObservableCollection<Player> Players { get; set; } = DataBaseLogic.GetPlayers();
        public Player Player { get; set; }
        public Level Level { get; set; }
        public string PlayGameButton { get; set; } = "play game";
        public string BackToStartButton { get; set; } = "back";
        public string SearchPlayerInput { get; set; } = "";
        #endregion

        #region ICommand
        public ICommand StartGameCommand { get; set; }
        public ICommand BackButtonCommand { get; set; }
        public ICommand SearchPlayerCommand { get; set; }
        public ICommand ClearPlayerSearchCommand { get; set; }
        #endregion

        public ChoosePlayerViewModel()
        {         
            StartGameCommand = new RelayCommand(StartGame);
            BackButtonCommand = new RelayCommand(BackToStart);
            SearchPlayerCommand = new RelayCommand(SearchPlayer);
            ClearPlayerSearchCommand = new RelayCommand(ClearPlayerSearch);

        }

        private void ClearPlayerSearch()
        {
            SearchPlayerInput = "";
            Players  = DataBaseLogic.GetPlayers();

        }

        private void SearchPlayer()
        {
            if (SearchPlayerInput != "")
            {
                Players = DataBaseLogic.GetPlayersByName(SearchPlayerInput);
            }
            
        }

        public void StartGame()
        {
            Player player = Player;

            if (player != null)
            {               
                var page = new SelectLevelPage(player);
                ((MainWindow) Application.Current.MainWindow).Main.Content = page;
            }
            
            // ELSE SATS-om man ej väljer spelare
        }
        private void BackToStart()
        {
            var page1 = new StartPage();
            ((MainWindow)Application.Current.MainWindow).Main.Content = page1;
        }
    }
}
