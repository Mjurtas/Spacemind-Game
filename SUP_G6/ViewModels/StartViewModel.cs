using SUP_G6.ViewModels.BaseViewModel;
using SUP_G6.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace SUP_G6.ViewModels
{
    public class StartViewModel : INotifyPropertyChanged
    {
        public StartViewModel()
        {
            //HighScores = new ObservableCollection<HighScore>();
            //HighScores.Add(new HighScore());
            ViewHighScorePage = new RelayCommand(ViewHighScore);
        }
        //public ObservableCollection<HighScore> HighScores { get; set; }
        public string CreatePlayerButton { get; set; } = "CREATE PLAYER";
        public string ChoosePlayerButton { get; set; } = "CHOOSE PLAYER";
        public string ViewHighScoreButton { get; set; } = "VIEW HIGHSCORE";

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ViewHighScorePage { get; set; }

        

        public void ViewHighScore()
        {
            var page = new HighScorePage();
        }
    }
}
