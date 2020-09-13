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

namespace SUP_G6.ViewModels
{
    class GamePlayViewModel : BaseViewModel.BaseViewModel, INotifyPropertyChanged
    {
        public GamePlayViewModel(Player player, Level level)
        {

            GuessCommand = new RelayCommand(ExecuteGuess);
            this.player = player;
            this.level = level;
            SetLevelVisibility();
            SecretCode = GameLogic.GenerateSecretCode(level);
            CreateNewGameResult();
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
        }
        public int[] Guess { get; set; }
        //public List<object> GuessOne { get; set; }
        public int[] SecretCode { get; set; }
        public bool MediumLevel { get; set;  } = false;
        public bool HardLevel { get; set; } = false;
        public PegPosition[] feedbacklist;
        public Player player;
        private Stopwatch _stopWatch;
        public Level level;

        #region Feedback-pegs Propertys
        public ObservableCollection<PegPosition> feedbackPegs = new ObservableCollection<PegPosition>();
        public ObservableCollection<PegPosition> FeedbackPegs { get; set; }
        public ObservableCollection<bool> feedbackPegsVisibility = new ObservableCollection<bool>();
        public ObservableCollection<bool> FeedbackPegsVisibility { get; set; }
        #endregion

        public void SetLevelVisibility()
        { 
            if (level == Level.Medium)
            {
                MediumLevel = true;

            }
            if (level == Level.Hard)
            {
                MediumLevel = true;
                HardLevel = true;

            }
        }

        //public PegPosition FeedbackPeg
        //{
        //    get { return FeedbackPeg; }
        //    set { FeedbackPeg = PegPosition.CorrectColorAndPosition; }
        //}


        //public ICommand CheckFeedBackCommand { get; set; }

        public ICommand GuessCommand { get; set; }
      

        private void ExecuteGuess()
        {
            //nån kod här
            var feedback = GameLogic.Feedback(SecretCode, Guess);
            SetFeedbackPegs(feedback);

            //if (gameHasEnded)
            //{
            //    _stopWatch.Stop();
            //    GameResult results = new GameResult
            //    {
            //        PlayerId = player.Id,
            //        PlayerName = player.Name,
            //        Tries = NumberOfTries,
            //        ElapsedTicks = _stopWatch.ElapsedTicks,
            //        Level = level,
            //        Win = false
            //    };
            //    SaveToDatabase(gameresult);
            //}
        }

        public void SetFeedbackPegs(PegPosition[] feedback)
        {
            //Random rnd = new Random();
            //PegPosition array = array.OrderBy(x => rnd.Next()).ToArray();
            for (int i = 0; i < feedback.Length; i++)
            {
                feedbackPegs.Add(feedback[i]);
                feedbackPegsVisibility.Add(true);

            }
            FeedbackPegs = feedbackPegs;
            FeedbackPegsVisibility = feedbackPegsVisibility;
            int counter = 0;
            for (int i = FeedbackPegs.Count-4; i < FeedbackPegs.Count; i++)
            {
                switch (counter)
                {
                    case 0:
                        FeedbackPegs[i] = feedback[0];
                        FeedbackPegsVisibility[i] = true;
                        break;
                    case 1:
                        FeedbackPegs[i] = feedback[1];
                        FeedbackPegsVisibility[i] = true;
                        break;
                    case 2:
                        FeedbackPegs[i] = feedback[2];
                        FeedbackPegsVisibility[i] = true;
                        break;
                    case 3:
                        FeedbackPegs[i] = feedback[3];
                        FeedbackPegsVisibility[i] = true;
                        break;
                    default:
                        break;
                }
                counter++;
            }
        }
        public void SetGuess(Panel guessPanel)
        {
            UIElementCollection guessedPegs = guessPanel.Children;
            int[] guess = new int[4];

            foreach (MasterPeg peg in guessedPegs)
            {
                int colorId = peg.ColorId;
                int position = guessedPegs.IndexOf(peg);
                guess.SetValue(colorId, position);
            }
            Guess = guess;
        }

        #region VM till DB
        private void CreateNewGameResult()
        {
            GameResult gameResult = new GameResult()
            {
                PlayerId = player.Id,
                PlayerName = player.Name,
                Level = this.level
            };

            gameResult.GameId = DataBaseLogic.AddGameResult(gameResult);
        }
        #endregion





    }
}