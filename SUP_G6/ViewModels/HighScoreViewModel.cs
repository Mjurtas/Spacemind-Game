using SUP_G6.Models;
using SUP_G6.Other;
using SUP_G6.ViewModels.BaseViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SUP_G6.ViewModels
{
    public class HighScoreViewModel : BaseViewModel.BaseViewModel
    {


        public HighScoreViewModel()
        {
            //SortHighScoreCommand = new RelayCommand(SortHighscore);
            SortByNameCommand = new RelayCommand(SortByName);
            SortByTimeCommand = new RelayCommand(SortByTime);
            SortByTriesCommand = new RelayCommand(SortByTries);
        }



        public ObservableCollection<GameResult> GetList { get; set; } = DataBaseLogic.GetGameResults();
        public ObservableCollection<GameResult> ListOfGameResults { get; set; } 
        //public event PropertyChangedEventHandler PropertyChanged;


        public ICommand SortByTimeCommand { get; set; }
        public ICommand SortByNameCommand { get; set; }
        public ICommand SortByTriesCommand { get; set; }





        public bool SortHighScoreStandard { get; set; } = false;
        public bool SortHighScoreByName { get; set; } = true;
        public bool SortHighScoreByTries { get; set; } = false;

        public ICommand SortHighScoreCommand { get; set; }
       
        public void SortByTime()
        {
            ListOfGameResults = new ObservableCollection<GameResult>(GetList.OrderBy(x => x.ElapsedTimeInSeconds));
        }

        public void SortByName()
        {
            ListOfGameResults = new ObservableCollection<GameResult>(GetList.OrderBy(x => x.PlayerName));
        }

        public void SortByTries()
        {
            ListOfGameResults = new ObservableCollection<GameResult>(GetList.OrderBy(x => x.Tries));
        }


        //public void SortHighscore()
        //{

        //    if (SortHighScoreStandard)
        //    {
        //        ListOfGameResults = new ObservableCollection<GameResult>(ListOfGameResults.OrderBy(x => x.ElapsedTimeInSeconds));
                
        //    }

        //    else if (SortHighScoreByName)
        //    {
        //        ListOfGameResults = new ObservableCollection<GameResult>(ListOfGameResults.OrderBy(x => x.PlayerName));

        //    }

        //    else
        //    {
        //        ListOfGameResults = new ObservableCollection<GameResult>(ListOfGameResults.OrderByDescending(x => x.Tries));

        //    }





        //}






    }
}
