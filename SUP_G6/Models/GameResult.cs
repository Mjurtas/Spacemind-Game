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

        public DateTime Time_start { get; set; } = DateTime.Now;

        public DateTime Time_end { get; set; }

        public double ElapsedTimeInSeconds => (Time_end - Time_start).TotalSeconds;

        public string Level { get; set; }

        public bool Win { get; set; } = false;

        public override string ToString()
        {
            return $"Name: {PlayerName} Tries: {Tries} Time: {GetTime()} {Level}";
        }

        private object GetTime()
        {
            throw new NotImplementedException();
        }
    }
}
