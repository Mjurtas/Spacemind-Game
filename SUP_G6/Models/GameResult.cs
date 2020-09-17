using SUP_G6.DataTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SUP_G6.Models
{
    public class GameResult
    {
        public int GameId { get; set; }

        public int PlayerId { get; set; }

        public string PlayerName { get; set; }

        public int Tries { get; set; } = 0;

        public double ElapsedTimeInSeconds { get; set; }

        public long ElapsedTicks { get; set; }
     
        public Level Level { get; set; }

        public bool Win { get; set; }

        public override string ToString()
        {
            return $"Name: {PlayerName}              Tries: {Tries}                 Time: {ElapsedTimeInSeconds}             Level  {Level}";
        }


    }
}