using SUP_G6.Models;
using SUP_G6.Other;
using SUP_G6.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SUP_G6.Views
{
    /// <summary>
    /// Interaction logic for GamePlayPage.xaml
    /// </summary>
    public partial class GamePlayPage : Page
    {
        public GamePlayPage(Player player)
        {
            
            InitializeComponent();
            DataContext = new GamePlayViewModel();
            TimeSpan timeSpan = new TimeSpan();

            GameResult gameResult = new GameResult()
            {
                PlayerId = player.Id,
                PlayerName = player.Name,
                Level = "Easy"
            };

            gameResult.GameId = DataBaseLogic.AddGameResult(gameResult);
        }
        private void MakeNextGuessAvailable(StackPanel GuessRow) 
        {
            GuessRow.AllowDrop = true;
            GuessRow.Background = Brushes.Azure;
        }

        private void panel_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("Object"))
            {
                // These Effects values are used in the drag source's
                // GiveFeedback event handler to determine which cursor to display.
                if (e.KeyStates == DragDropKeyStates.ControlKey)
                {
                    e.Effects = DragDropEffects.Copy;
                }
                else
                {
                    e.Effects = DragDropEffects.Move;
                }
            }
        }

        private void panel_Drop(object sender, DragEventArgs e)
        {
            // If an element in the panel has already handled the drop,
            // the panel should not also handle it.
            if (e.Handled == false)
            {
                Panel _panel = (Panel)sender;
                UIElement _element = (UIElement)e.Data.GetData("Object");

                if (_panel != null && _element != null)
                {
                    // Get the panel that the element currently belongs to,
                    // then remove it from that panel and add it the Children of
                    // the panel that its been dropped on.
                    Panel _parent = (Panel)VisualTreeHelper.GetParent(_element);

                    if (_parent != null)
                    {
                        if (e.AllowedEffects.HasFlag(DragDropEffects.Copy))
                        {
                            if (_element is MasterPeg)
                            {
                                var colorId = ((MasterPeg)_element).ColorId;
                                MasterPeg peg;
                                switch (colorId)
                                {
                                    case 1: 
                                        peg = new Peg1();
                                        break;
                                    case 2: 
                                        peg = new Peg2();
                                        break;
                                    case 3: 
                                        peg = new Peg3();
                                        break;
                                    case 4:
                                        peg = new Peg4();
                                        break;
                                    case 5:
                                        peg = new Peg5();
                                        break;
                                    case 6:
                                        peg = new Peg6();
                                        break;

                                    default:
                                        peg = new Peg6();
                                        break;
                                }
                                _panel.Children.Add(peg);
                            }
                            // set the value to return to the DoDragDrop call
                            e.Effects = DragDropEffects.Copy;
                        }
                        //else if (e.AllowedEffects.HasFlag(DragDropEffects.Move))
                        //{
                        //    _parent.Children.Remove(_element);
                        //    _panel.Children.Add(_element);
                        //    if (_element is MasterPeg)
                        //    {
                        //        var colorId = ((MasterPeg)_element).ColorId;
                        //    }

                        //    // set the value to return to the DoDragDrop call
                        //    e.Effects = DragDropEffects.Move;
                        //}
                    }
                }
            }

        }
    }
}
