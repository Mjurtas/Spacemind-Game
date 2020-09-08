using System;
using System.Collections.Generic;
using System.Text;

namespace SUP_G6.ViewModels
{
    class GamePlayViewModel
    {
        public int NumberOfTotallyWrongPegs { get; set; }
        public int NumberOfCorrectSymbolPegs { get; set; }
        public int NumberOfTotallyCorrectPegs { get; set; }
        public void SetFeedbackPegs(char [] feedback)
        {
            NumberOfTotallyWrongPegs = feedback[0];
            NumberOfCorrectSymbolPegs = feedback[1];
            NumberOfTotallyCorrectPegs = feedback[2];
        }
    }
}
