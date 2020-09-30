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
    /// Interaction logic for GamePlayPage.xaml
    /// </summary>
    public partial class GamePlayPage : Page
    {
        private GamePlayViewModel viewModel;

        public GamePlayPage(IPlayer player, Level level)
        {

            InitializeComponent();
            viewModel = new GamePlayViewModel(player, level);
            DataContext = viewModel;
            AddPanelList();
        }
        #region Variables
        List<Panel> Panels = new List<Panel>();
        List<Panel> currentGuessRow = new List<Panel>();
        List<Panel> nextGuessRow = new List<Panel>();
        #endregion

        #region Helper function(s)

        public void AddPanelList()
        {
            foreach (UIElement p in MasterGrid.Children)
            {
                if (p.GetType() == typeof(Grid))
                {
                    var q = (Panel)p;
                    if (q.Name.StartsWith('p'))
                    {
                        Panels.Add(q);
                    }
                }
            }       
                    //Panels.Add(p1);
                    //Panels.Add(p2);
                    //Panels.Add(p3);
                    //Panels.Add(p4);
                    //Panels.Add(p5);
                    //Panels.Add(p6);
                    //Panels.Add(p7);
                    //Panels.Add(p8);
                    //Panels.Add(p9);
                    //Panels.Add(p10);
                    //Panels.Add(p11);
                    //Panels.Add(p12);
                    //Panels.Add(p13);
                    //Panels.Add(p14);
                    //Panels.Add(p15);
                    //Panels.Add(p16);
                    //Panels.Add(p17);
                    //Panels.Add(p18);
                    //Panels.Add(p19);
                    //Panels.Add(p20);
                    //Panels.Add(p21);
                    //Panels.Add(p22);
                    //Panels.Add(p23);
                    //Panels.Add(p24);
                    //Panels.Add(p25);
                    //Panels.Add(p26);
                    //Panels.Add(p27);
                    //Panels.Add(p28);
                    //Panels.Add(p29);
                    //Panels.Add(p30);
                    //Panels.Add(p31);
                    //Panels.Add(p32);
                    //Panels.Add(p33);
                    //Panels.Add(p34);
                    //Panels.Add(p35);
                    //Panels.Add(p36);
                    //Panels.Add(p37);
                    //Panels.Add(p38);
                    //Panels.Add(p39);
                    //Panels.Add(p40);
                }

        public int[] NewExtraction()
        {
            Panel firstPanel = Panels[viewModel.NumberOfTries * 4];
            Panel secondPanel = Panels[viewModel.NumberOfTries * 4 + 1];
            Panel thirdPanel = Panels[viewModel.NumberOfTries * 4 + 2];
            Panel fourthPanel = Panels[viewModel.NumberOfTries * 4 + 3];
            List<UIElement> guessedPegs = new List<UIElement>();
            guessedPegs.Add(firstPanel.Children[0]);
            guessedPegs.Add(secondPanel.Children[0]);
            guessedPegs.Add(thirdPanel.Children[0]);
            guessedPegs.Add(fourthPanel.Children[0]);
            int[] guess = new int[4];

            foreach (IPeg peg in guessedPegs)
            {
                int colorId = peg.ColorId;
                int position = guessedPegs.IndexOf((UIElement)peg);
                guess.SetValue(colorId, position);
            }
            return guess;
        }

        public void DetermineActivePanel()
        {
            currentGuessRow.Clear();
            currentGuessRow.Add(Panels[viewModel.NumberOfTries * 4]);
            currentGuessRow.Add(Panels[viewModel.NumberOfTries * 4 + 1]);
            currentGuessRow.Add(Panels[viewModel.NumberOfTries * 4 + 2]);
            currentGuessRow.Add(Panels[viewModel.NumberOfTries * 4 + 3]);
            nextGuessRow.Clear();
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

        private void MakeNextGuessAvailable(List<Panel> GuessRow)
        {
            for (int i = 0; i < GuessRow.Count; i++)
            {
                GuessRow[i].AllowDrop = true;
                GuessRow[i].Background = Brushes.LightYellow;
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
                            if (_element is IPeg)
                            {
                                var colorId = ((IPeg)_element).ColorId;
                                IPeg peg;
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
                                _panel.Children.Clear();
                                _panel.Children.Add((UIElement)peg);
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Event Handler for Buttons
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