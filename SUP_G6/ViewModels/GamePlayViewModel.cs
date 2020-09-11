using SUP_G6.Other;
using SUP_G6.ViewModels.BaseViewModel;
using System;
using System.Collections.Generic;
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

        public ICommand CheckFeedBackCommand { get; set; }
       
        
        
        public void SetFeedbackPegs(char [] feedback)
        {
            NumberOfTotallyWrongPegs = feedback[0];
            NumberOfCorrectSymbolPegs = feedback[1];
            NumberOfTotallyCorrectPegs = feedback[2];
        }

        public GamePlayViewModel(char [] _secretCode)
        {
            SecretCode = _secretCode;

            CheckGuess();

        }



        public void CheckGuess()
        {
           
        }


       
    }
}
