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
        public ICommand SortHighScoreListByEasyCommand { get; set; }
        public ICommand SortHighScoreListByMediumCommand { get; set; }
        public ICommand SortHighScoreListByHardCommand { get; set; }
        #endregion

        #region Properties
        //Button content properties
        public string ButtonTime { get; set; } = "time";
        public string ButtonTries { get; set; } = "tries";
        public string ButtonMostPlayed { get; set; } = "most played";
        public string ButtonBack { get; set; } = "back";
        public string Unit1 { get; set; } = "seconds";
        public string Unit2 { get; set; } = "tries";

        //RadioButton content properties
        public string RadioButtonEasy { get; set; } = "easy";
        public string RadioButtonMedium { get; set; } = "medium";
        public string RadioButtonHard { get; set; } = "hard";

        //Labels over HighScoreTable content properties
        public string HighScoreColumn1 { get; set; } = "Name";
        public string HighScoreColumn2 { get; set; } = "Time";
        public string HighScoreColumn3 { get; set; } = "Tries";

        // Different conditions when sorting highscorelist
        public bool SortHighScoreStandard { get; set; } = false;
        public bool SortHighScoreByName { get; set; } = true;
        public bool SortHighScoreByTries { get; set; } = false;


        // Set true or false on Easy/Medium/Hard and sorts highscore-lists
        /* based on user selection*/
        public Level Level { get; set; } = Level.Easy;
        public string Sort { get; set; } = "time";
        public bool EasyRadioButton { get; set; } = true;
        public bool MediumRadioButton { get; set; } = false;
        public bool HardRadioButton { get; set; } = false;

        public bool DiligentWrapPanel { get; set; } = false;
        public bool ScoreWrapPanel { get; set; } = false;
        #endregion

        #region List of GameResults and Players
        /* Recieves data from DataBase*/
        public ObservableCollection<GameResult> ListOfGameResults { get; set; }
        public ObservableCollection<Player> ListOfDiligentPlayers { get; set; }
        public ObservableCollection<IExistInDatabase> HighScoreList { get; set; }
        #endregion

        // Relays for ICommands
        public HighScoreViewModel()
        {
            ListOfGameResults = DataBaseLogic.GetGameResultsBy(Level, Sort);
            UpdateHighScoreList("GameResult");
            SortByTimeCommand = new RelayCommand(SortByTime);
            SortByTriesCommand = new RelayCommand(SortByTries);
            ShowDiligentPlayersCommand = new RelayCommand(GetDiligentPlayers);
            ViewStartPageCommand = new RelayCommand(ViewStartPage);
            SortHighScoreListByEasyCommand = new RelayCommand(UpdateHighSCoreAfterLevelRadioButton);
            SortHighScoreListByMediumCommand = new RelayCommand(UpdateHighSCoreAfterLevelRadioButton);
            SortHighScoreListByHardCommand = new RelayCommand(UpdateHighSCoreAfterLevelRadioButton);
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

        public string listType = "GameResult";

        public void UpdateHighScoreList(string listType)
        {
            HighScoreList = new ObservableCollection<IExistInDatabase>();
            HighScoreList.Clear();
            if (listType == "GameResult")
            {
                foreach (var gameResult in ListOfGameResults)
                {
                    HighScoreList.Add(gameResult);
                }
                HighScoreColumn2 = "Time";
                HighScoreColumn3 = "Tries";
            }
            else
            {
                foreach (var player in ListOfDiligentPlayers)
                {
                    HighScoreList.Add(player);
                }
                HighScoreColumn2 = "Games played";
                HighScoreColumn3 = " ";
            }
        }
        public void SortByTime()
        {
            ScoreWrapPanel = true;
            DiligentWrapPanel = false;
            Unit1 = "seconds";
            Unit2 = "tries";
            SetLevelFromRadioButton();
            Sort = "time";
            listType = "GameResult";
            ListOfGameResults = DataBaseLogic.GetGameResultsBy(Level, Sort);
            UpdateHighScoreList(listType);
        }

        public void SortByTries()
        {
            Unit1 = "seconds";
            Unit2 = "tries";
            SetLevelFromRadioButton();
            Sort = "tries";
            listType = "GameResult";
            ListOfGameResults = DataBaseLogic.GetGameResultsBy(Level, Sort);
            UpdateHighScoreList(listType);
        }
        public void GetDiligentPlayers()
        {
            ScoreWrapPanel = false;
            DiligentWrapPanel = true;
            Unit1 = " played";
            Unit2 = "times";
            SetLevelFromRadioButton();
            listType = "Player";
            ListOfDiligentPlayers = DataBaseLogic.GetDiligentPlayersOnLevel(Level);
            UpdateHighScoreList(listType);
        }

        public void UpdateHighSCoreAfterLevelRadioButton()
        {
            SetLevelFromRadioButton();
            UpdateHighScoreList(listType);
            if (listType == "Player")
            {
                ListOfDiligentPlayers = DataBaseLogic.GetDiligentPlayersOnLevel(Level);
            }
            else
            {
                ListOfGameResults = DataBaseLogic.GetGameResultsBy(Level, Sort);
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