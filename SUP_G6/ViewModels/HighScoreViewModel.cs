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
        public ICommand SortByEasyCommand{ get; set; }
        public ICommand SortByMediumCommand { get; set; }
        public ICommand SortByHardCommand { get; set; }
        #endregion

        #region Properties
        //Button content properties
        public string ButtonTime { get; set; } = "score";
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
        public Level Level { get; set; } = Level.Easy;
        public string Sort { get; set; } = "time";
        public bool EasyRadioButton { get; set; } = true;
        public bool MediumRadioButton { get; set; } = false;
        public bool HardRadioButton { get; set; } = false;

        public bool DiligentSortingVisibility { get; set; } = false;
        public bool ScoreSortingVisibility { get; set; } = true;
        #endregion

        #region List of GameResults and Players
        /* Recieves data from DataBase*/
        public ObservableCollection<GameResult> ListOfGameResults { get; set; }
        public ObservableCollection<Player> ListOfDiligentPlayers { get; set; }
        public ObservableCollection<IExistInDatabase> HighScoreList { get; set; }
        #endregion

        public EventHandler RadioEvent { get; set; } 


        // Relays for ICommands
        public HighScoreViewModel()
        {
            ListOfGameResults = DataBaseLogic.GetGameResults(Level);
            
            //UpdateHighScoreList("GameResult");
            SortByTimeCommand = new RelayCommand(SortByTime);
      
            ShowDiligentPlayersCommand = new RelayCommand(GetDiligentPlayers);
            ViewStartPageCommand = new RelayCommand(ViewStartPage);
            SortByEasyCommand = new RelayCommand(SetLevelFromRadioButton);
            SortByMediumCommand = new RelayCommand(SetLevelFromRadioButton);
            SortByHardCommand = new RelayCommand(SetLevelFromRadioButton);
        }

        
        
    
        #region RadioButton Method - Sort by level

        public void SetLevelFromRadioButton()
        {
            if (ScoreSortingVisibility == true)
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
                ListOfGameResults = DataBaseLogic.GetGameResults(Level);
            }


            if (ScoreSortingVisibility == false)
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
                ListOfDiligentPlayers = DataBaseLogic.GetDiligentPlayersOnLevel(Level);
            }

        }

        #endregion

        #region Sorting HighScore Methods
        public void SortByTime()
        {
            ScoreSortingVisibility = true;
            DiligentSortingVisibility = false;
            SetLevelFromRadioButton();
            

        }

        public void GetDiligentPlayers()
        {

            ScoreSortingVisibility = false;
            DiligentSortingVisibility = true;
            SetLevelFromRadioButton();
            

        }



        /* Enables sorting of highscorelists. IExistInDataBase makes it possible to
         keep both GameResults and Players in same list since both classes inherit from
         the interface.*/

        //public string listType = "GameResult";

        //public void UpdateHighScoreList(string listType)
        //{
        //    HighScoreList = new ObservableCollection<IExistInDatabase>();
        //    HighScoreList.Clear();
        //    if (listType == "GameResult")
        //    {
        //        foreach (var gameResult in ListOfGameResults)
        //        {
        //            HighScoreList.Add(gameResult);
        //        }

        //    }
        //    else
        //    {
        //        foreach (var player in ListOfDiligentPlayers)
        //        {
        //            HighScoreList.Add(player);
        //        }

        //    }
        //}

        //public void UpdateHighSCoreAfterLevelRadioButton()
        //{
        //    SetLevelFromRadioButton();
        //    UpdateHighScoreList(listType);
        //    if (listType == "Player")
        //    {
        //        ListOfDiligentPlayers = DataBaseLogic.GetDiligentPlayersOnLevel(Level);
        //    }
        //    else
        //    {
        //        ListOfGameResults = DataBaseLogic.GetGameResults(Level);
        //    }
        //}
        #endregion

        public void ViewStartPage()
        {

            var page = new StartPage();
            ((MainWindow)Application.Current.MainWindow).Main.Content = page;
        }
    }
}