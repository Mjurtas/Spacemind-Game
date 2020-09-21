using System;
using System.Collections.Generic;
using System.Text;

namespace SUP_G6.Interface
{
    public interface IExistInDatabase
    {
        public string DisplayName { get; set; }
        public Int64 DisplayCount { get; set; }
    }
}