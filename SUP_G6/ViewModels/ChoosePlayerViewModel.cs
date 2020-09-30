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
using SUP_G6.Interface;

namespace SUP_G6.ViewModels
{
    //ViewModel to handle that the user selects player
    public class ChoosePlayerViewModel : BaseViewModel.BaseViewModel
    {
        #region Constructor

        public ChoosePlayerViewModel()
        {
            StartGameCommand = new RelayCommand(StartGame);
            BackButtonCommand = new RelayCommand(BackToStart);
            SearchPlayerCommand = new RelayCommand(SearchPlayer);
            ClearPlayerSearchCommand = new RelayCommand(ClearPlayerSearch);

        }

        #endregion

        #region Properties

        public ObservableCollection<IPlayer> Players { get; set; } = DataBaseLogic.GetPlayers();
        public Player Player { get; set; }
        public Level Level { get; set; }

        #region Content Bindings

        public string BackToStartButton { get; set; } = "back";
        public string PlayGameButton { get; set; } = "play game";
        public string SearchPlayerInput { get; set; } = "";
        public string ChoosePlayerLabel { get; set; } = "choose player";

        #endregion

        #endregion

        #region ICommand

        public ICommand BackButtonCommand { get; set; }
        public ICommand ClearPlayerSearchCommand { get; set; }
        public ICommand SearchPlayerCommand { get; set; }
        public ICommand StartGameCommand { get; set; }

        #endregion

        #region Search Player Methods

        private void SearchPlayer()
        {
            if (SearchPlayerInput != "")
            {
                SearchPlayerInput = SearchPlayerInput.ToLower();
                Players = DataBaseLogic.GetPlayersByName(SearchPlayerInput);
            }
        }

        private void ClearPlayerSearch()
        {
            SearchPlayerInput = "";
            Players = DataBaseLogic.GetPlayers();
        }

        #endregion

        #region Navigation Methods

        public void StartGame()
        {
            Player player = Player;

            if (player != null)
            {
                var page = new SelectLevelPage(player);
                ((MainWindow)Application.Current.MainWindow).Main.Content = page;
            }
            else
            {
                MessageBox.Show($"Listen to Yoda, choose a player..");
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
