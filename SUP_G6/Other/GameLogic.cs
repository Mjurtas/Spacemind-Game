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
            char[] generatedCode = random.Next(1, 9999).ToString().ToCharArray();


            return generatedCode;
        }

        public static bool[] Feedback(int[] secretCode, int[] guess)
        {
            bool win = false;
            bool[] feedbackList = new bool[4];


            if (guess == secretCode)
            {
                feedbackList = new bool[] { true, true, true, true };
                win = true;
                return feedbackList;
            }

            int[] clonedSecretNumber = secretCode;
            for (var i = 0; i < clonedSecretNumber.Length; i++)
            {
                if (guess[i] == clonedSecretNumber[i])
                {
                    feedbackList[i] = true;
                    int numToRemove = clonedSecretNumber[i];
                    clonedSecretNumber = clonedSecretNumber.Where(val => val != numToRemove).ToArray();
                }
            }

            int[] clonedSecret = clonedSecretNumber;

            for (var i = 0; i < secretCode.Length; i++)
            {
                if (clonedSecret.Contains(guess[i]))

                {
                    feedbackList[i] = true;
                    int numToRemove = clonedSecret[i];
                    clonedSecret = clonedSecret.Where(val => val != numToRemove).ToArray();
                }
            }
            return feedbackList;

        }
        public static int NumbersOfTriesLeft(int numberOfGuesses)
        {
            numberOfGuesses = 10 - numberOfGuesses;
            return numberOfGuesses;
        }
    }
}
