using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SUP_G6.ViewModels.BaseViewModel
{
    //This class exists to separate the programmes objects from the execution logic itself
    class RelayCommand : ICommand
    {
        #region Fields

        private Action action;

        #endregion

        #region Constructor
        public RelayCommand(Action action)
        {
            this.action = action;
        }
        #endregion

        #region Event Handlers

        public event EventHandler CanExecuteChanged;

        #endregion

        #region Execution Methods
        //CanExecute will always return true
        ///<param name="parameter"></param>
        /// <returns></returns>

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action();
        }
        #endregion
    }
}

