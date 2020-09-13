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

namespace SUP_G6.ViewModels
{
    class GamePlayViewModel : BaseViewModel.BaseViewModel, INotifyPropertyChanged
    {
        public GamePlayViewModel(Player player, string level)
        {
            this.player = player;
            this.level = level;
            SecretCode = GameLogic.GenerateSecretCode();
            CreateNewGameResult();


            _stopWatch = new Stopwatch();
            _stopWatch.Start();
        }
        public int[] Guess { get; set; }
        public List<object> GuessOne { get; set; }
        public int[] SecretCode { get; set; }
        public int NumberOfTotallyWrongPegs { get; set; }
        public int NumberOfCorrectSymbolPegs { get; set; }
        public int NumberOfTotallyCorrectPegs { get; set; }
        public PegPosition[] feedbacklist;
        public Player player;
        private Stopwatch _stopWatch;
        private readonly string level;
        public Panel MyProperty { get; set; }

        public PegPosition FeedbackPeg1 { get; set; }
        public PegPosition FeedbackPeg2 { get; set; }
        public PegPosition FeedbackPeg3 { get; set; }
        public PegPosition FeedbackPeg4 { get; set; }
        public bool FeedbackPeg1Visibility { get; set; } = false;
        public bool FeedbackPeg2Visibility { get; set; } = false;
        public bool FeedbackPeg3Visibility { get; set; } = false;
        public bool FeedbackPeg4Visibility { get; set; } = false;
        //public PegPosition FeedbackPeg
        //{
        //    get { return FeedbackPeg; }
        //    set { FeedbackPeg = PegPosition.CorrectColorAndPosition; }
        //}


        //public ICommand CheckFeedBackCommand { get; set; }

        public ICommand GuessCommand
        {
            get
            {
                return new RelayCommand(ExecuteGuess);
            }
        }

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
            for (int i = 0; i < feedback.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        FeedbackPeg1 = feedback[i];
                        FeedbackPeg1Visibility = true;
                        break;
                    case 1:
                        FeedbackPeg2 = feedback[i];
                        FeedbackPeg2Visibility = true;
                        break;
                    case 2:
                        FeedbackPeg3 = feedback[i];
                        FeedbackPeg3Visibility = true;
                        break;
                    case 3:
                        FeedbackPeg4 = feedback[i];
                        FeedbackPeg4Visibility = true;
                        break;
                    default:
                        break;
                }
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
                Level = "Easy"
            };

            gameResult.GameId = DataBaseLogic.AddGameResult(gameResult);
        }
        #endregion





    }
}