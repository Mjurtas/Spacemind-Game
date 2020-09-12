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
     
        public string Level { get; set; }

        public bool Win { get; set; }

        public override string ToString()
        {
            return $"Name: {PlayerName} Tries: {Tries} Time: {GetTime()} {Level}";
        }

        private object GetTime()
        {
            var minutes = new DateTime(ElapsedTicks).Minute;

            var seconds = new DateTime(ElapsedTicks).Second;

            return $"{minutes} {seconds}";
            //TimeSpan t = TimeSpan.FromSeconds(ElapsedTimeInSeconds);
            //if (ElapsedTimeInSeconds <= 60)
            //{
            //    string answer = string.Format("{0:D2}s",
            //                    t.Seconds);
            //    return answer;
            //}


            //else
            //{
            //    string answer = string.Format("{0:D2}m:{1:D2}s",
            //                    t.Minutes,
            //                    t.Seconds);
            //    return answer;
            //}
        }
    }
}