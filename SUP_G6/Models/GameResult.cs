using SUP_G6.DataTypes;
using SUP_G6.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SUP_G6.Models
{
    public class GameResult : IExistInDatabase
    {
        public int GameId { get; set; }

        public int PlayerId { get; set; }

        public string PlayerName { get; set; }

        public int Tries { get; set; } = 0;

        public double ElapsedTimeInSeconds { get; set; }

        public long ElapsedTicks { get; set; }

        public Level Level { get; set; }

        public bool Win { get; set; }
        public string DisplayName { get; set; }
        public Int64 DisplayCount { get; set; }
        public int Score { get; internal set; }

        public void CalculateScore()
        {
            Score = 10000 - Tries * 1000 + (int)Math.Round(ElapsedTimeInSeconds) * 7;
        }

        public override string ToString()
        {
            return $" {PlayerName} \t\t{ElapsedTimeInSeconds}  \t\t {Tries}  ";
        }


    }
}