using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SUP_G6.Other
{
     public static class GameLogic
    {

        public static char[] GenerateSecretCode()
        {
            Random random = new Random();
            char[] generatedCode = new char[3];
            for (int i = 0; i < generatedCode.Length; i++)
            {
                int generatednumber = random.Next(1, 7);
                generatedCode[i] = (char)generatednumber;
            }

            return generatedCode;
        }

        public static char [] Feedback(char [] secretCode, char [] guess)
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
        public static int NumbersOfTriesLeft(int numberOfGuesses)
        {
            numberOfGuesses = 10 - numberOfGuesses;
            return numberOfGuesses;
        }
    }
}
