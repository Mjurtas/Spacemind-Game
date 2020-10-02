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
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using System.IO;
using System.Windows.Resources;

namespace SUP_G6.Views
{
    /// <summary>
    /// Interaction for view specific logic in GamePlayPage.xaml
    /// </summary>
    public partial class GamePlayPage : Page
    {
        private GamePlayViewModel viewModel;

        #region Fields

        List<Panel> Panels = new List<Panel>();
        List<Panel> currentGuessRow = new List<Panel>();
        List<Panel> nextGuessRow = new List<Panel>();

        #endregion

        #region Constructor

        public GamePlayPage(IPlayer player, Level level)
        {
            InitializeComponent();
            viewModel = new GamePlayViewModel(player, level);
            DataContext = viewModel;
            AddPanelsToList();
        }

        #endregion

        #region Helper functions

        public void AddPanelsToList()
        {

            /* Checks all childrenitems in Main Grid a.k.a "MasterGrid".
             If the type is Grid, it's casted to a Panel and check the x:name.
            If it starts with "p" it gets added to the Panels-list. */ 
            foreach (UIElement p in MasterGrid.Children)
            {
                if (p.GetType() == typeof(Grid))
                {
                    var panel = (Panel)p;
                    if (panel.Name.StartsWith('p'))
                    {
                        Panels.Add(panel);
                    }
                }
            }
        }
        
        public int[] NewExtraction()
        {
            List<UIElement> guessedPegs = new List<UIElement>();

            /* New list to get the current row's guesses.
             * Returns array with what ID the peg has, and in which panel the peg is placed */

            guessedPegs.Add(currentGuessRow[0].Children[0]);
            guessedPegs.Add(currentGuessRow[1].Children[0]);
            guessedPegs.Add(currentGuessRow[2].Children[0]);
            guessedPegs.Add(currentGuessRow[3].Children[0]);

            int[] guess = new int[4];

            foreach (IPeg peg in guessedPegs)
            {
                int pegId = peg.PegId;
                int position = guessedPegs.IndexOf((UIElement)peg);
                guess.SetValue(pegId, position);
            }
            return guess;
        }

        public void DetermineActivePanel()
        {
            currentGuessRow.Clear();

            /* NumberOfTries is set in ViewModel, and is multiplied by 4 to check which row that should be active. */
            //Adds the last 4 Panels that the player has manipulated to currentGuessRow
            currentGuessRow.Add(Panels[viewModel.NumberOfTries * 4]);
            currentGuessRow.Add(Panels[viewModel.NumberOfTries * 4 + 1]);
            currentGuessRow.Add(Panels[viewModel.NumberOfTries * 4 + 2]);
            currentGuessRow.Add(Panels[viewModel.NumberOfTries * 4 + 3]);

            nextGuessRow.Clear();

            //If the player has any tries left, the next 4 Panels that the player may manipulate gets added to nextGuessRow
            if (viewModel.NumberOfTries < 9) 
            {
                nextGuessRow.Add(Panels[viewModel.NumberOfTries * 4 + 4]);
                nextGuessRow.Add(Panels[viewModel.NumberOfTries * 4 + 5]);
                nextGuessRow.Add(Panels[viewModel.NumberOfTries * 4 + 6]);
                nextGuessRow.Add(Panels[viewModel.NumberOfTries * 4 + 7]);
            }
        }

        #endregion

        #region Handle Guesses

        private bool IsGuessDone(List<Panel> currentPanel)
        {
            /* Checks if there are one children in each panel in the currentPanel. If so, the guess is submitted and the pabel is disabled for additional pegplacements.*/
            for (int i = 0; i < currentPanel.Count; i++)
            {
                if (currentPanel[0].Children.Count > 0 && currentPanel[1].Children.Count > 0 && currentPanel[2].Children.Count > 0 && currentPanel[3].Children.Count > 0)
                {
                    currentPanel[i].AllowDrop = false;
                    currentPanel[i].Background = Brushes.LightGray;
                    foreach (IPeg peg in currentPanel[i].Children)
                    {
                        peg.IsEnabled = false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private void MakeNextGuessAvailable(List<Panel> nextGuessRow)
        {

            // Is called when "IsGuessDown()" returns true and enables the next row for drag N drop.
            for (int i = 0; i < nextGuessRow.Count; i++)
            {
                nextGuessRow[i].AllowDrop = true;
                nextGuessRow[i].Background = Brushes.LightYellow;
            }
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
            //If an element in the panel has already handled the drop, the panel should not also handle it.
            if (e.Handled == false)
            {
                Panel _panel = (Panel)sender;
                UIElement _element = (UIElement)e.Data.GetData("Object");

                if (_panel != null && _element != null)
                {
                    //Get the panel that the element currently belongs to and add the children on the panel that its been dropped on.
                    Panel _parent = (Panel)VisualTreeHelper.GetParent(_element);

                    if (_parent != null)
                    {
                        if (e.AllowedEffects.HasFlag(DragDropEffects.Copy))
                        {
                            if (_element is IPeg)
                            {
                                var pegId = ((IPeg)_element).PegId;
                                IPeg peg;
                                switch (pegId)
                                {
                                    case 1:
                                        peg = (IPeg)new Peg1();
                                        break;
                                    case 2:
                                        peg = (IPeg)new Peg2();
                                        break;
                                    case 3:
                                        peg = (IPeg)new Peg3();
                                        break;
                                    case 4:
                                        peg = (IPeg)new Peg4();
                                        break;
                                    case 5:
                                        peg = (IPeg)new Peg5();
                                        break;
                                    case 6:
                                        peg = (IPeg)new Peg6();
                                        break;
                                    case 7:
                                        peg = (IPeg)new Peg7();
                                        break;
                                    case 8:
                                        peg = (IPeg)new Peg8();
                                        break;

                                    default:
                                        peg = (IPeg)new Peg6();
                                        break;
                                }
                                _panel.Children.Clear();
                                _panel.Children.Add((UIElement)peg);
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Event Handler for Guess Button
        private void GuessButton_Click(object sender, RoutedEventArgs e)
        {
            DetermineActivePanel();
            if (IsGuessDone(currentGuessRow))
            {
                viewModel.Guess = NewExtraction();
                MakeNextGuessAvailable(nextGuessRow);
            }
            else
            {
                MessageBox.Show($"{viewModel.ToMessageBox}");
            }
        }
        #endregion
    }
}