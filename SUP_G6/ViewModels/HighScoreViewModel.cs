
using SUP_G6.DataTypes;
using SUP_G6.Models;
using SUP_G6.Other;
using SUP_G6.ViewModels.BaseViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Media;
using System.Windows;
using System.Windows.Threading;
using SUP_G6.Views;
using SUP_G6.Interface;

namespace SUP_G6.ViewModels
{
    public class HighScoreViewModel : BaseViewModel.BaseViewModel, INotifyPropertyChanged
    {
        #region ICommands
        public ICommand SortByTimeCommand { get; set; }
        public ICommand SortByNameCommand { get; set; }
        public ICommand SortByTriesCommand { get; set; }
        public ICommand ShowDiligentPlayersCommand { get; set; }
        public ICommand ViewStartPageCommand { get; set; }
        public ICommand SortHighScoreCommand { get; set; }
        #endregion

        #region Properties
        //Button content properties
        public string ButtonTime { get; set; } = "time";
        public string ButtonTries { get; set; } = "tries";
        public string ButtonMostPlayed { get; set; } = "most played";
        public string ButtonBack { get; set; } = "back";

        //RadioButton content properties
        public string RadioButtonEasy { get; set; } = "easy";
        public string RadioButtonMedium { get; set; } = "medium";
        public string RadioButtonHard { get; set; } = "hard";


        // Different conditions when sorting highscorelist
        public bool SortHighScoreStandard { get; set; } = false;
        public bool SortHighScoreByName { get; set; } = true;
        public bool SortHighScoreByTries { get; set; } = false;


        // Set true or false on Easy/Medium/Hard and sorts highscore-lists
        /* based on user selection*/
        public Level Level { get; set; }
        public bool EasyRadioButton { get; set; } = true;
        public bool MediumRadioButton { get; set; } = false;
        public bool HardRadioButton { get; set; } = false;

        #endregion

        #region List of GameResults and Players
        /* Recieves data from DataBase*/

        public ObservableCollection<GameResult> GetList { get; set; } = DataBaseLogic.GetGameResults();
        public ObservableCollection<GameResult> ListOfGameResults { get; set; }
        public ObservableCollection<Player> ListOfDiligentPlayers { get; set; } = DataBaseLogic.GetDiligentPlayers();
        public ObservableCollection<IExistInDatabase> HighScoreList { get; set; }
        #endregion

        // Relays for ICommands
        public HighScoreViewModel()
        {
            SortByNameCommand = new RelayCommand(SortByName);
            SortByTimeCommand = new RelayCommand(SortByTime);
            SortByTriesCommand = new RelayCommand(SortByTries);
            ShowDiligentPlayersCommand = new RelayCommand(GetDiligentPlayers);
            ViewStartPageCommand = new RelayCommand(ViewStartPage);            
        }

        #region RadioButton Method - Sort by level

        public void SetLevelFromRadioButton()
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

        #endregion

        #region Sorting HighScore Methods

        /* Enables sorting of highscorelists. IExistInDataBase makes it possible to
         keep both GameResults and Players in same list since both classes inherit from
         the interface.*/



        public void SortByTime()
        {
            SetLevelFromRadioButton();
            ListOfGameResults = new ObservableCollection<GameResult>(GetList.OrderBy(x => x.ElapsedTimeInSeconds).Where(x => x.Level == Level).Take(3));
            HighScoreList = new ObservableCollection<IExistInDatabase>();
            HighScoreList.Clear();
            foreach (var gameResult in ListOfGameResults)
            {
                HighScoreList.Add(gameResult);
            }
        }

        public void SortByName()
        {
            SetLevelFromRadioButton();
            ListOfGameResults = new ObservableCollection<GameResult>(GetList.OrderBy(x => x.PlayerName).Where(x => x.Level == Level));
            HighScoreList = new ObservableCollection<IExistInDatabase>();
            HighScoreList.Clear();
            foreach (var gameResult in ListOfGameResults)
            {
                HighScoreList.Add(gameResult);
            }
        }

        public void SortByTries()
        {
            SetLevelFromRadioButton();
            ListOfGameResults = new ObservableCollection<GameResult>(GetList.OrderBy(x => x.Tries).Where(x => x.Level == Level).Take(3));
            HighScoreList = new ObservableCollection<IExistInDatabase>();
            HighScoreList.Clear();
            foreach (var gameResult in ListOfGameResults)
            {
                HighScoreList.Add(gameResult);
            }
        }
        public void GetDiligentPlayers()
        {
            HighScoreList = new ObservableCollection<IExistInDatabase>();
            HighScoreList.Clear();
            foreach (var player in ListOfDiligentPlayers)
            {
                HighScoreList.Add(player);
            }
        }
        #endregion

        public void ViewStartPage()
        {
            
            var page = new StartPage();
            ((MainWindow)Application.Current.MainWindow).Main.Content = page;
        }
    }
}
