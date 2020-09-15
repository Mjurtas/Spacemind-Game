using System;
using System.Collections.Generic;
using System.Linq;

namespace SUP_G6.Other
{
    internal class GuessValidator
    {
        private Secret[] secretCode;

        public GuessValidator(int[] secretCode)
        {
            this.secretCode = new Secret[secretCode.Length];
            for (int i = 0; i < secretCode.Length; i++)
            {
                this.secretCode[i] = new Secret(secretCode[i], i);
            }
        }

        //internal PegPosition Insert(int color, int index)
        //{
        //    bool totallyCorrect = secretCode[index].Color == color;
        //    if (totallyCorrect)
        //    {
        //        secretCode[index].HasBeenMatched = true;
        //        return PegPosition.CorrectColorAndPosition;
        //    }

        //    bool totallyWrong = !secretCode.Any(secret => secret.Color == color);
        //    if (totallyWrong)
        //        return PegPosition.TotallyWrong;
        //    else
        //    {
        //        var sameColorPeg = secretCode

        //    };
        //}

        internal List<GuessResult> FindTotallyCorrects(int[] guesses)
        {
            var results = new List<GuessResult>();

            for (int i = 0; i < guesses.Length; i++)
            {
                bool totallyCorrect = secretCode[i].Color == guesses[i];
                if (totallyCorrect)
                {
                    var color = guesses[i];
                    var index = i;
                    secretCode[index].IsHandled = true;
                    results.Add(new GuessResult(color, index));
                }
            }

            return results;
        }

        internal List<GuessResult> FindTotallyWrongs(int[] guesses)
        {
            var results = new List<GuessResult>();
            var unhandledSecrets = secretCode.Where(code => !code.IsHandled);
            var unhandledGuesses = guesses.Where(guess => !guess.IsHandled);
            for (int i = 0; i < guesses.Length; i++)
            {
                bool totallyWrong = !unhandledSecrets.Any(secret => secret.Color == guesses[i]);
                if (totallyWrong)
                {
                    var color = guesses[i];
                    var index = i;
                    results.Add(new GuessResult(color, index));
                }
            }

            return results;
        }

        internal List<GuessResult> FindWrongPositioned(int[] guesses)
        {
            var results = new List<GuessResult>();
            for (int i = 0; i < guesses.Length; i++)
            {
                int color = guesses[i];
                int index = i;
                bool totallyCorrect = FindTotallyCorrects(guesses).Any(guess => guess.Color == color && guess.Index == index);
                bool totallyWrong = FindTotallyWrongs(guesses).Any(guess => guess.Color == color && guess.Index == index);

                if (!totallyCorrect && !totallyWrong)
                {
                    results.Add(new GuessResult(color, index));
                }
            }
            return results;
        }
    }
}