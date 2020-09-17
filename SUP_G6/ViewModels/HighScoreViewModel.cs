using SUP_G6.DataTypes;
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
            SortByNameCommand = new RelayCommand(SortByName);
            SortByTimeCommand = new RelayCommand(SortByTime);
            SortByTriesCommand = new RelayCommand(SortByTries);
        }



        public ObservableCollection<GameResult> GetList { get; set; } = DataBaseLogic.GetGameResults();
        public ObservableCollection<GameResult> ListOfGameResults { get; set; }
        public Level Level { get; set; }
        public bool EasyRadioButton { get; set; } = true;
        public bool MediumRadioButton { get; set; } = false;
        public bool HardRadioButton { get; set; } = false;



        public ICommand SortByTimeCommand { get; set; }
        public ICommand SortByNameCommand { get; set; }
        public ICommand SortByTriesCommand { get; set; }





        public bool SortHighScoreStandard { get; set; } = false;
        public bool SortHighScoreByName { get; set; } = true;
        public bool SortHighScoreByTries { get; set; } = false;

        public ICommand SortHighScoreCommand { get; set; }

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

        public void SortByTime()
        {
            SetLevelFromRadioButton();
            ListOfGameResults = new ObservableCollection<GameResult>(GetList.OrderBy(x => x.ElapsedTimeInSeconds).Where(x => x.Level == Level).Take(3));
        }

        public void SortByName()
        {
            SetLevelFromRadioButton();
            ListOfGameResults = new ObservableCollection<GameResult>(GetList.OrderBy(x => x.PlayerName).Where(x => x.Level == Level));
        }

        public void SortByTries()
        {
            SetLevelFromRadioButton();
            ListOfGameResults = new ObservableCollection<GameResult>(GetList.OrderBy(x => x.Tries).Where(x => x.Level == Level).Take(3));
        }
    }
}
