using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SUP_G6.Interface
{
   public interface INotifyPropertyChanged
    {
        event PropertyChangedEventHandler PropertyChanged;

        
    }
}
