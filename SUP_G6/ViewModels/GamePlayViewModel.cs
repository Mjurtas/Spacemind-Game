using SUP_G6.Models;
using SUP_G6.Other;
using SUP_G6.ViewModels.BaseViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace SUP_G6.ViewModels
{
    class GamePlayViewModel
    {

        private int[] secretcode;
        public int[] gues;
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

        //private PegPosition FeedBackPeg;

        public PegPosition FeedBackPeg
        {
            get { return FeedBackPeg; }
            set { FeedBackPeg = PegPosition.TotallyWrong; }
        }


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

            if (gameHasEnded)
            {
                _stopWatch.Stop();
                GameResult results = new GameResult
                {
                    PlayerId = player.Id,
                    PlayerName = player.Name,
                    Tries = NumberOfTries,
                    ElapsedTicks = _stopWatch.ElapsedTicks,
                    Level = level,
                    Win = false
                };
                SaveToDatabase(gameresult);
            }
        }

        public void SetFeedbackPegs(char[] feedback)
        {
            NumberOfTotallyWrongPegs = feedback[0];
            NumberOfCorrectSymbolPegs = feedback[1];
            NumberOfTotallyCorrectPegs = feedback[2];
        }

        public GamePlayViewModel(Player player, string level)
        {
            this.player = player;
            this.level = level;
            SecretCode = GameLogic.GenerateSecretCode();
            CreateNewGameResult();

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
        }

        private void ThisWillBeEdited()
        {
            //GameLogic.Feedback(secretCode, CompareGuessWithSecretCode(currentGuessRow));
            //CompareGuessWithSecretCode(currentGuessRow);
        }
        private int[] CompareGuessWithSecretCode(Panel guessPanel)
        {
            UIElementCollection guessedPegs = guessPanel.Children;
            int[] guess = new int[4];

            foreach (MasterPeg peg in guessedPegs)
            {
                int colorId = peg.ColorId;
                int position = guessedPegs.IndexOf(peg);
                guess.SetValue(colorId, position);
            }
            return guess;
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