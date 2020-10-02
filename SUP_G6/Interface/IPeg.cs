using SUP_G6.DataTypes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SUP_G6.Interface
{
    interface IPeg
    {
        #region Properties
        public int PegId { get; set; }
        public Level LevelVisibility { get; set; }
        bool IsEnabled { get; set; }
        #endregion

        #region Methods
        public void OnMouseMove();
        public Cursor GetCursor();
        public void OnGiveFeedback();
        public void OnDrop();
        public void OnDragOver();
        public void OnDragEnter();
        public void OnDragLeave();
        #endregion
    }
}
