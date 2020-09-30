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

        #region Constructor
        public HighScoreViewModel()
        {
            ListOfGameResults = DataBaseLogic.GetGameResults(Level);
            SortByTimeCommand = new RelayCommand(SortByTime);
            SortByDiligentPlayersCommand = new RelayCommand(GetDiligentPlayers);
            ViewStartPageCommand = new RelayCommand(ViewStartPage);
            SortByEasyCommand = new RelayCommand(SetLevelFromRadioButton);
            SortByMediumCommand = new RelayCommand(SetLevelFromRadioButton);
            SortByHardCommand = new RelayCommand(SetLevelFromRadioButton);
        }
        #endregion 

        #region ICommands
        public ICommand SortByTimeCommand { get; set; }
        public ICommand SortByNameCommand { get; set; }
        public ICommand SortByTriesCommand { get; set; }
        public ICommand SortByDiligentPlayersCommand { get; set; }
        public ICommand ViewStartPageCommand { get; set; }
        public ICommand SortHighScoreCommand { get; set; }
        public ICommand SortByEasyCommand{ get; set; }
        public ICommand SortByMediumCommand { get; set; }
        public ICommand SortByHardCommand { get; set; }
        #endregion

        #region Properties

        #region ContentBindings
        //Button content properties
        public string ButtonTime { get; set; } = "score";
        public string ButtonTries { get; set; } = "tries";
        public string ButtonMostPlayed { get; set; } = "most played";
        public string ButtonBack { get; set; } = "back";


        //RadioButton content properties
        public string RadioButtonEasy { get; set; } = "easy";
        public string RadioButtonMedium { get; set; } = "medium";
        public string RadioButtonHard { get; set; } = "hard";


        public string HighscoreLabel { get; set; } = "highscore";   
        #endregion

        // Different conditions when sorting highscorelist
        public bool SortHighScoreStandard { get; set; } = false;
        public bool SortHighScoreByName { get; set; } = true;
        public bool SortHighScoreByTries { get; set; } = false;


         /*Set true or false on Easy/Medium/Hard and sorts highscore-lists
         based on user selection*/
        public Level Level { get; set; } = Level.Easy;

        public bool EasyRadioButton { get; set; } = true;
        public bool MediumRadioButton { get; set; } = false;
        public bool HardRadioButton { get; set; } = false;

        public Visibility DiligentSortingVisibility { get; set; } = Visibility.Collapsed;
        public Visibility ScoreSortingVisibility { get; set; } = Visibility.Visible;
        
        /* Recieves data from DataBase*/
        public ObservableCollection<IExistInDatabase> ListOfGameResults { get; set; }
        public ObservableCollection<IPlayer> ListOfDiligentPlayers { get; set; }
 
        #endregion

        #region HighScoreMethods    


        #region RadioButton Method - Sort by level

        public void SetLevelFromRadioButton()

            // Sets from which level the list should be sorted on, and if it should be sorted by time or diligent players.
            // Since these are two differens grids they switch from Visible to Collapsed based on the players choice.
        {
            if (ScoreSortingVisibility == Visibility.Visible)
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


            if (ScoreSortingVisibility == Visibility.Collapsed)
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
            ScoreSortingVisibility = Visibility.Visible;
            DiligentSortingVisibility = Visibility.Collapsed;
            SetLevelFromRadioButton();   
        }

        public void GetDiligentPlayers()
        {
            ScoreSortingVisibility = Visibility.Collapsed;
            DiligentSortingVisibility = Visibility.Visible;
            SetLevelFromRadioButton();
        }

        #endregion

        public void ViewStartPage()
        {
            var page = new StartPage();
            ((MainWindow)Application.Current.MainWindow).Main.Content = page;
        }
    }
    #endregion
}