using SUP_G6.Models;
using SUP_G6.Other;
using SUP_G6.ViewModels.BaseViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;

namespace SUP_G6.ViewModels
{
    class GamePlayViewModel
    {

        private char[] secretcode;
        public char[] guess = 1234.ToString().ToCharArray();
        public char[] Guess { get; set; }
        public char[] SecretCode { get; set; }
        public int NumberOfTotallyWrongPegs { get; set; }
        public int NumberOfCorrectSymbolPegs { get; set; }
        public int NumberOfTotallyCorrectPegs { get; set; }
        public char[] feedbacklist;
        public Player player;

        public ICommand CheckFeedBackCommand { get; set; }
       
        
        
        public void SetFeedbackPegs(char [] feedback)
        {
            NumberOfTotallyWrongPegs = feedback[0];
            NumberOfCorrectSymbolPegs = feedback[1];
            NumberOfTotallyCorrectPegs = feedback[2];
        }

        public GamePlayViewModel(Player player)
        {
            this.player = player;
            SecretCode = GameLogic.GenerateSecretCode();
            CreateNewGameResult();

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

        }

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

        


       
    }
}
