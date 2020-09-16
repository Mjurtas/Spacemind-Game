﻿using SUP_G6.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SUP_G6.Models
{
    public class Player : IPlayer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
