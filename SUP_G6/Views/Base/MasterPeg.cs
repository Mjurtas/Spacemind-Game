using SUP_G6.DataTypes;
using SUP_G6.Interface;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Resources;

namespace SUP_G6
{
    public class MasterPeg : UserControl,  IPeg
    {
        #region Properties
        public int ColorId { get; set; }
        public Level LevelVisibility { get; set; }
        #endregion

        #region Constructor
        public MasterPeg()
        {
            Height = 50;
            Name = "Peg";
            Width = 50;
        }
        #endregion

        #region Drag and drop methods
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Package the data.
                DataObject data = new DataObject();
                //data.SetData(DataFormats.StringFormat, circleUI.Fill.ToString());
                //data.SetData("Double", circleUI.Height);
                data.SetData("Object", this);

                // Inititate the drag-and-drop operation.
                DragDrop.DoDragDrop(this, data, DragDropEffects.Copy | DragDropEffects.Move);
                
            }
        }

        public Cursor GetCursor()
        {
            StreamResourceInfo sriCurs;
            switch (ColorId)
            {
                case 1:
                    sriCurs = Application.GetResourceStream(new Uri("Views/Cursors/peg1av.cur", UriKind.Relative));
                    break;
                case 2:
                    sriCurs = Application.GetResourceStream(new Uri("Views/Cursors/peg2av.cur", UriKind.Relative));
                    break;
                case 3:
                    sriCurs = Application.GetResourceStream(new Uri("Views/Cursors/peg3av.cur", UriKind.Relative));
                    break;
                case 4:
                    sriCurs = Application.GetResourceStream(new Uri("Views/Cursors/peg4av.cur", UriKind.Relative));
                    break;
                case 5:
                    sriCurs = Application.GetResourceStream(new Uri("Views/Cursors/peg5av.cur", UriKind.Relative));
                    break;
                case 6:
                    sriCurs = Application.GetResourceStream(new Uri("Views/Cursors/peg6av.cur", UriKind.Relative));
                    break;
                case 7:
                    sriCurs = Application.GetResourceStream(new Uri("Views/Cursors/peg7av.cur", UriKind.Relative));
                    break;
                case 8:
                    sriCurs = Application.GetResourceStream(new Uri("Views/Cursors/peg8av.cur", UriKind.Relative));
                    break;
                default:
                    sriCurs = Application.GetResourceStream(new Uri("Views/Cursors/peg1av.cur", UriKind.Relative));
                    break;
            }
            Cursor peg = new Cursor(sriCurs.Stream);
            return peg;
        }

        protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
        {
            base.OnGiveFeedback(e);
            // These Effects values are set in the drop target's
            // DragOver event handler.
            if (e.Effects.HasFlag(DragDropEffects.Copy))
            {
                Mouse.SetCursor(GetCursor());
            }
            else if (e.Effects.HasFlag(DragDropEffects.Move))
            {
                Mouse.SetCursor(GetCursor());
            }
            else
            {
                Mouse.SetCursor(GetCursor());
            }
            e.Handled = true;
        }

        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);

            // If the DataObject contains string data, extract it.
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                string dataString = (string)e.Data.GetData(DataFormats.StringFormat);

                // If the string can be converted into a Brush,
                // convert it and apply it to the ellipse.
                BrushConverter converter = new BrushConverter();
                if (converter.IsValid(dataString))
                {
                    Brush newFill = (Brush)converter.ConvertFromString(dataString);
                    //circleUI.Fill = newFill;

                    // Set Effects to notify the drag source what effect
                    // the drag-and-drop operation had.
                    // (Copy if CTRL is pressed; otherwise, move.)
                    if (e.KeyStates.HasFlag(DragDropKeyStates.ControlKey))
                    {
                        e.Effects = DragDropEffects.Copy;
                    }
                    else
                    {
                        e.Effects = DragDropEffects.Move;
                    }
                }
            }
            e.Handled = true;
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            base.OnDragOver(e);
            e.Effects = DragDropEffects.None;

            // If the DataObject contains string data, extract it.
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                string dataString = (string)e.Data.GetData(DataFormats.StringFormat);

                // If the string can be converted into a Brush, allow copying or moving.
                BrushConverter converter = new BrushConverter();
                if (converter.IsValid(dataString))
                {
                    // Set Effects to notify the drag source what effect
                    // the drag-and-drop operation will have. These values are
                    // used by the drag source's GiveFeedback event handler.
                    // (Copy if CTRL is pressed; otherwise, move.)
                    if (e.KeyStates.HasFlag(DragDropKeyStates.ControlKey))
                    {
                        e.Effects = DragDropEffects.Copy;
                    }
                    else
                    {
                        e.Effects = DragDropEffects.Move;
                    }
                }
            }
            e.Handled = true;
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);
            // Save the current Fill brush so that you can revert back to this value in DragLeave.
            //            _previousFill = circleUI.Fill;

            // If the DataObject contains string data, extract it.
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                string dataString = (string)e.Data.GetData(DataFormats.StringFormat);

                // If the string can be converted into a Brush, convert it.
                BrushConverter converter = new BrushConverter();
                if (converter.IsValid(dataString))
                {
                    Brush newFill = (Brush)converter.ConvertFromString(dataString.ToString());
                    //       circleUI.Fill = newFill;
                }
            }
        }

        protected override void OnDragLeave(DragEventArgs e)
        {
            base.OnDragLeave(e);
            // Undo the preview that was applied in OnDragEnter.
            //    circleUI.Fill = _previousFill;
        }

        #endregion

        #region Error handling for interface IPeg
        public void OnMouseMove()
        {
            throw new NotImplementedException();
        }

        public void OnGiveFeedback()
        {
            throw new NotImplementedException();
        }

        public void OnDrop()
        {
            throw new NotImplementedException();
        }

        public void OnDragOver()
        {
            throw new NotImplementedException();
        }

        public void OnDragEnter()
        {
            throw new NotImplementedException();
        }

        public void OnDragLeave()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
