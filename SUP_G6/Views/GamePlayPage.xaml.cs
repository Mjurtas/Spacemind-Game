using SUP_G6.DataTypes;
using SUP_G6.Interface;
using SUP_G6.Models;
using SUP_G6.Other;
using SUP_G6.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Media;
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
        private GamePlayViewModel viewModel;
        
        public GamePlayPage(Player player, Level level)
        {
           
            InitializeComponent();
            viewModel = new GamePlayViewModel(player, level);
            DataContext = viewModel;
        }

        #region Variables

        int numberOfTries = 1;
        Panel currentGuessRow;
        Panel nextGuessRow;

        #endregion

        #region Helper function(s)

        public static int[] ExtractGuessFromUIPanel(Panel guessPanel)
        {
            UIElementCollection guessedPegs = guessPanel.Children;
            int[] guess = new int[4];

            foreach (MasterPeg peg in guessedPegs)
            {
                int colorId = peg.ColorId;
                int position = guessedPegs.IndexOf(peg);
                guess.SetValue(colorId, position);
            }
            return guess;
        }

        #endregion

        #region Handle Guesses

        private bool IsGuessDone(Panel currentPanel)
        {
            if (currentPanel.Children.Count == 4)
            {
                currentPanel.AllowDrop = false;
                currentPanel.Background = Brushes.LightGray;
                foreach (MasterPeg peg in currentPanel.Children)
                {
                    peg.IsEnabled = false;
                }
                return true;
            }
            return false;
        }

        private void MakeNextGuessAvailable(Panel GuessRow)
        {
            GuessRow.AllowDrop = true;
            GuessRow.Background = Brushes.LightYellow;
        }

        #endregion

        #region Drag and Drop

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
                                    case 7:
                                        peg = new Peg7();
                                        break;
                                    case 8:
                                        peg = new Peg8();
                                        break;

                                    default:
                                        peg = new Peg6();
                                        break;
                                }
                                if (_panel.Children.Count < 4)
                                {
                                    _panel.Children.Add(peg);
                                }
                                else
                                {
                                    _panel.Children.Clear();
                                    _panel.Children.Add(peg);
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Event Handler for Guess Button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GameLogic.NumbersOfTriesLeft(numberOfTries);
            switch (numberOfTries)
            {
                case 1:
                    currentGuessRow = stp1;
                    nextGuessRow = stp2;
                    break;
                case 2:
                    currentGuessRow = stp2;
                    nextGuessRow = stp3;
                    break;
                case 3:
                    currentGuessRow = stp3;
                    nextGuessRow = stp4;
                    break;
                case 4:
                    currentGuessRow = stp4;
                    nextGuessRow = stp5;
                    break;
                case 5:
                    currentGuessRow = stp5;
                    nextGuessRow = stp6;
                    break;
                case 6:
                    currentGuessRow = stp6;
                    nextGuessRow = stp7;
                    break;
                case 7:
                    currentGuessRow = stp7;
                    nextGuessRow = stp8;
                    break;
                case 8:
                    currentGuessRow = stp8;
                    nextGuessRow = stp9;
                    break;
                case 9:
                    currentGuessRow = stp9;
                    nextGuessRow = stp10;
                    break;
                case 10:
                    currentGuessRow = stp10;
                    break;
                default:
                    break;
            }
            if (IsGuessDone(currentGuessRow))
            {
                    viewModel.Guess = ExtractGuessFromUIPanel(currentGuessRow);
                    MakeNextGuessAvailable(nextGuessRow);
                    numberOfTries++;

            }
            else
            {
                MessageBox.Show($"{viewModel.ToMessageBox}");
            }
        }
        #endregion
    }
}