using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SUP_G6.Other
{
    public static class GameLogic
    {
        public static int[] GenerateSecretCode(DataTypes.Level level)
        {
            Random random = new Random();
            int numberOfDifferentValues;
            switch (level)
            {
                case DataTypes.Level.Easy:
                    numberOfDifferentValues = 5;
                    break;
                case DataTypes.Level.Medium:
                    numberOfDifferentValues = 7;
                    break;
                case DataTypes.Level.Hard:
                    numberOfDifferentValues = 9;
                    break;
                default:
                    numberOfDifferentValues = 7;
                    break;
            }
            int[] generatedCode = new int[4];
            for (int i = 0; i < generatedCode.Length; i++)
            {
                int generatednumber = random.Next(1, numberOfDifferentValues);
                generatedCode[i] = generatednumber;
            }

            return generatedCode;
        }

        public static PegPosition[] Feedback(int[] secretCode, int[] guess)
        {
            bool win = false;
            PegPosition[] feedbackList = new PegPosition[4];

            if (guess == secretCode)
            {
                feedbackList = new PegPosition[] { PegPosition.CorrectColorAndPosition, PegPosition.CorrectColorAndPosition, PegPosition.CorrectColorAndPosition, PegPosition.CorrectColorAndPosition };
                win = true;
                return feedbackList;
            }
            List<int> clonedCode = new List<int>();
            foreach (int id in secretCode)
            {
                clonedCode.Add(id);
            }
            for (int i = 0; i < clonedCode.Count; i++)
            {
                if (clonedCode[i] == guess[i])
                {
                    feedbackList[i] = PegPosition.CorrectColorAndPosition;
                    clonedCode.RemoveAt(i);
                    clonedCode.Insert(i, 0);
                }
                else if (clonedCode.Contains(guess[i]))
                {
                    feedbackList[i] = PegPosition.CorrectColorWrongPosition;
                    clonedCode.RemoveAt(i);
                    clonedCode.Insert(i, 0);
                }
                else
                {
                    feedbackList[i] = PegPosition.TotallyWrong;
                    clonedCode.RemoveAt(i);
                    clonedCode.Insert(i, 0);
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