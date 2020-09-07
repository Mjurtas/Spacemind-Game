using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SUP_G6.Other
{
     class GameLogic
    {
        Random random = new Random();
        public char[] SecretCode { get; set; }
        public char[] Guess { get; set; }
        public int NumberOfGuesses { get; set; } = 10;

        //public char[] GenerateSecretCode() 
        //{
        //    char[] generatedCode = new char[4];
        //    for (int i = 0; i < generatedCode.Length; i++)
        //    {
        //        int generatednumber = random.Next(1 - 7);
        //        generatedCode[i] = (char)generatednumber;
        //    }

        //    return generatedCode;
        //}

        public void GenerateSecretCode()
        {
            char[] generatedCode = new char[4];
            for (int i = 0; i < generatedCode.Length; i++)
            {
                int generatednumber = random.Next(1 - 7);
                generatedCode[i] = (char)generatednumber;
            }

            SecretCode = generatedCode;
        }

        private char [] Feedback(char [] secretCode, char [] guess)
        {

            char[] feedbackList = new char[3];
            int correctLetterLocation = 0;
            int correctLetter = 0;
            int totallyWrong = 0;

            if (guess == secretCode)
            {
                char [] win = { (char)0, (char)0, (char)4 };
                feedbackList = win;
                return feedbackList;
            }
            
            char[] clonedSecretNumber = secretCode;
            for (var i = 0; i < clonedSecretNumber.Length; i++)
            {
                if (guess[i] == clonedSecretNumber[i])
                {
                    correctLetterLocation++;
                    clonedSecretNumber[i] = ' ';
                }
            }

            char[] clonedSecret = clonedSecretNumber;

            for (var i = 0; i < secretCode.Length; i++)
            {
                if (clonedSecret.Contains(guess[i]))

                {
                    correctLetter++;
                    clonedSecretNumber[i] = ' ';
                }
            }

            int totalcorrect = correctLetter + correctLetterLocation;
            totallyWrong = 4 - totalcorrect;
            char[] finalList = { (char)totallyWrong, (char)correctLetter, (char)correctLetterLocation };
            feedbackList = finalList;
            return feedbackList;

        }
    }
}
