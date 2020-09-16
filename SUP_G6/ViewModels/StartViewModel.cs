using SUP_G6.ViewModels.BaseViewModel;
using SUP_G6.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace SUP_G6.ViewModels
{
    public class StartViewModel : BaseViewModel.BaseViewModel
    {
        public StartViewModel()
        {
            //HighScores = new ObservableCollection<HighScore>();
            //HighScores.Add(new HighScore());
            
         
        }

      

        //public ObservableCollection<HighScore> HighScores { get; set; }
        public string CreatePlayerButton { get; set; } = "create player";
        public string ChoosePlayerButton { get; set; } = "choose player";
        public string ViewHighScoreButton { get; set; } = "view highscore";
        public string ViewGameRulesButton { get; set; } = "game rules";

        public event PropertyChangedEventHandler PropertyChanged;

       


       

        

        
    }
}
