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

        public static PegPosition[] Feedback(int[] secretCode, int[] guesses)
        {
            secretCode = new int[]{ 2, 1, 1, 5};
            // Kolla att varje gissning existerar i secretCode exakt en gång
            var guessValidator = new GuessValidator(secretCode);
            List<Tuple<int, PegPosition>> results = new List<Tuple<int, PegPosition>>();

            var correctGuesses = guessValidator.FindTotallyCorrects(guesses);
            foreach (var guess in correctGuesses)
            {
                //secrets.Add(new Secret(guess.Color, guess.Index));
                results.Add(new Tuple<int, PegPosition>(guess.Index, PegPosition.CorrectColorAndPosition));
            }

            var totallyWrongs = guessValidator.FindTotallyWrongs(guesses);
            foreach (var guess in totallyWrongs)
            {
                //secrets.Add(new Secret(guess.Color, guess.Index));
                results.Add(new Tuple<int, PegPosition>(guess.Index, PegPosition.TotallyWrong));
            }

            var wrongPositioned = guessValidator.FindWrongPositioned(guesses);
            foreach (var guess in wrongPositioned)
            {
                results.Add(new Tuple<int, PegPosition>(guess.Index, PegPosition.CorrectColorWrongPosition));
            }

            return results
                .OrderBy(item => item.Item1)
                .Select(item => item.Item2)
                .ToArray();
        }


        public static int NumbersOfTriesLeft(int numberOfGuesses)
        {
            numberOfGuesses = 10 - numberOfGuesses;
            return numberOfGuesses;
        }
    }
}