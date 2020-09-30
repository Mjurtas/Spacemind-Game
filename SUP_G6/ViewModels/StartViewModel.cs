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

namespace SUP_G6.ViewModels
{
    public class StartViewModel : BaseViewModel.BaseViewModel
    {

        #region Constructor
        public StartViewModel()
        {
            CreatePlayerPageCommand = new RelayCommand(ChangePageToCreatePlayerPage);
            ChoosePlayerPageCommand = new RelayCommand(ChangePageToChoosePlayerPage);
            ViewHighScorePageCommand = new RelayCommand(ChangePageToHighScorePage);
            ViewGameRulesPageCommand = new RelayCommand(ChangePageToGameRulesPage);
        }
        #endregion

        #region Properties
        public string CreatePlayerButton { get; set; } = "create player";
        public string ChoosePlayerButton { get; set; } = "choose player";
        public string ViewHighScoreButton { get; set; } = "view highscore";
        public string ViewGameRulesButton { get; set; } = "game rules";
        public string SpaceMindLabel { get; set; } = "spacemind";


        #endregion

        #region ICommands
        public ICommand CreatePlayerPageCommand { get; set; }
        public ICommand ChoosePlayerPageCommand { get; set; }
        public ICommand ViewHighScorePageCommand { get; set; }
        public ICommand ViewGameRulesPageCommand { get; set; }
        #endregion

        #region ChangePageMethods

        public void ChangePageToCreatePlayerPage()
        {
            var page = new CreatePlayerPage();
           ((MainWindow)Application.Current.MainWindow).Main.Content = page;

        }

        public void ChangePageToChoosePlayerPage()
        {
            var page = new ChoosePlayerPage();
            ((MainWindow)Application.Current.MainWindow).Main.Content = page;
        }

        public void ChangePageToHighScorePage()
        {
            var page = new HighScorePage();
            ((MainWindow)Application.Current.MainWindow).Main.Content = page;
        }

        private void ChangePageToGameRulesPage()
        {
            var page = new GameRulesPage();
            ((MainWindow)Application.Current.MainWindow).Main.Content = page;
        }
        #endregion

    }
}
