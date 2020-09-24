using SUP_G6.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SUP_G6.Models
{
    public class Player : IPlayer, IExistInDatabase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Int64 NumberOfGamesPlayed { get; set; }
        public string DisplayName { get; set; }
        public Int64 DisplayCount { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
