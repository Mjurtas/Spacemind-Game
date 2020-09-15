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


       
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<GameResult> HighScoreStandard = new ObservableCollection<GameResult>(DataBaseLogic.GetGameResults());
        public ObservableCollection<GameResult> highScoreStandard { get; set; }

        public IOrderedEnumerable<GameResult> OrderedList { get; set; }


        public bool SortHighScoreStandard { get; set; } = false;
        public bool SortHighScoreByName { get; set; } = true;
        public bool SortHighScoreByTries { get; set; } = false;

        public ICommand SortHighScoreCommand { get; set; }

        public HighScoreViewModel()
        {
            SortHighScoreCommand = new RelayCommand(SortHighscore);
        }

        public void SortHighscore()
        {

           
            if (SortHighScoreStandard)
            {
                //this.OrderedList = HighScoreStandard;
            }

            else if (SortHighScoreByName)
            {
                this.OrderedList = HighScoreStandard.OrderBy(o => o.PlayerName);


            }

            else
            {
                this.OrderedList = HighScoreStandard.OrderByDescending(o => o.Tries);

            }

            



        }






    }
}
