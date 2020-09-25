using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using SUP_G6.Models;

namespace SUP_G6.Other
{
    public static class GameLogic
    {
        private static Random random = new Random();
        public static int[] GenerateSecretCode(DataTypes.Level level)
        {
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

            //skapar listan med feedback
            PegPosition[] feedbackList = new PegPosition[4];

            //om den hemliga koden stämmer med gissningen helt så körs denna kod
            if (guess == secretCode)
            {
                feedbackList = new PegPosition[] { PegPosition.CorrectColorAndPosition, PegPosition.CorrectColorAndPosition, PegPosition.CorrectColorAndPosition, PegPosition.CorrectColorAndPosition };
                //gameResult.Win = true;
                return feedbackList;
            }

            //om den hemliga koden inte är helt rätt behövs dessa variabler
            bool[] handled = { false, false, false, false };
            List<int> listWithSecretCode = new List<int>();
            foreach (int num in secretCode)
            {
                listWithSecretCode.Add(num);
            }

            for (int i = 0; i < secretCode.Length; i++)
            {
                //om gissningen är rätt plupp på rätt plats körs denna kod
                if (secretCode[i] == guess[i])
                {
                    feedbackList[i] = PegPosition.CorrectColorAndPosition;

                    //visar att gissningen på index "i" är hanterad
                    handled[i] = true;

                    //tar bort gisningen från listan över korrekta värden för att gissningen är hanterad men behåller listans indexering för resterande värden
                    listWithSecretCode.RemoveAt(i);
                    listWithSecretCode.Insert(i, 0);

                }
                //om gissningen är helt fel körs denna kod
                else if (secretCode[i] != guess[i] && !secretCode.Contains(guess[i]))
                {
                    feedbackList[i] = PegPosition.TotallyWrong;
                    handled[i] = true;
                }
                //denna kod körs om gissningen inte kan uteslutas som helt fel eller helt rätt. Dvs: gissningen hanteras inte i denna loop
                else
                {
                    feedbackList[i] = PegPosition.TotallyWrong;
                }
            }
            //ny loop för att hantera de ännu ej hanterade gissningarna
            for (int i = 0; i < secretCode.Length; i++)
            {
                //onödigt att loopa en gång till om alla gissningar redan är hanterade
                if (!handled.Contains(false))
                {
                    break;
                }
                //är gissningen inte hanterad än körs denna kod
                if (!handled[i])
                {
                    //kontrollerar hur många av gissningen som fortfarande finns i listan av korrekta värden
                    int countSameCode = listWithSecretCode.FindAll(x => x == guess[i]).Count();

                    //om gissningen finns kvar i korrekta värden körs denna kod
                    if (countSameCode > 0)
                    {
                        feedbackList[i] = PegPosition.CorrectColorWrongPosition;

                        //tar bort värdet ur listan och struntar i indexeringen iom att den inte behövs längre
                        listWithSecretCode.Remove(guess[i]);
                        handled[i] = true;
                    }
                }
            }
            Array.Sort(feedbackList);
            return feedbackList;
        }

        public static int NumbersOfTriesLeft(int numberOfGuesses)
        {
            numberOfGuesses = 10 - numberOfGuesses;
            return numberOfGuesses;
        }       

    }
}