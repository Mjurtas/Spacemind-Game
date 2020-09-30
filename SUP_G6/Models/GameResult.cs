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

        public int Tries { get; set; }

        public double ElapsedTimeInSeconds { get; set; }

        public double TotalScore { get; set; }

        public Level Level { get; set; }

        public bool Win { get; set; }

        public string DisplayName { get; set; }

        public Int64 DisplayCount { get; set; }
   
     



     


    }
}