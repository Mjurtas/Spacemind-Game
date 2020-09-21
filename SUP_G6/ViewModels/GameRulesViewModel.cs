using SUP_G6.DataTypes;
using SUP_G6.Models;
using SUP_G6.Other;
using SUP_G6.ViewModels.BaseViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Media;
using System.Windows;
using System.Windows.Threading;
using SUP_G6.Views;
using SUP_G6.Interface;

namespace SUP_G6.ViewModels
{
    class GameRulesViewModel
    {
        public GameRulesViewModel()
        {
            ViewStartPageCommand = new RelayCommand(ViewStartPage);
        }

        #region Properties
        public string BackButton { get; set; } = "BACK";
        public string GameRules { get; set; } = "== How To Play ==\n \n" +
                                        "The computer will create a secret code consisting of four avatars. The goal of the game is to guess the secret code in 10 tries or less. The player may drag any of the coloured pegs to the game board to make a consisting of four avatars, and then press the Guess button. The computer will provide feedback on the guess with four smaller pegs to the right, one for each avatar:\n \n" +
                                        "1. A Green feedback peg means the avatar is the correct avatar and in the right position.\n" +
                                        "2. A Yellow feedback peg means the avatar is the correct avatar but in the wrong position.\n" +
                                        "3. A Red feedback peg means the avatar is neither the right avatar nor the correct position.\n \n" +
                                        "This will continue until you have spent all of the 10 guesses or until the code is cracked.\n \n" +
                                        "== Difficulty Levels ==\n \n" +
                                        "EASY\n" +
                                        "The secret code will consist of 4 avatars. \n \n" +
                                        "MEDIUM\n" +
                                        "The secret code will consist of 6 avatars. \n \n" +
                                        "HARD\n" +
                                        "The secret code will consist of 8 avatars. \n \n" +
                                        "== Start Menu ==\n \n" +
                                        "CREATE PLAYER\n" +
                                        "To create a new player, simply click “CREATE PLAYER”, type a name in the textbox, and then click “CREATE”.\n \n" +
                                        "CHOOSE PLAYER\n" +
                                        "Back for more? To play with an already existing player, click “CHOOSE PLAYER”, select a player from the list, a difficulty setting, and then click “PLAY GAME”.\n \n" +
                                        "VIEW HIGHSCORE\n" +
                                        "This page will present the best 3 players of any category.\n \n" +
                                        "GAME RULES\n" +
                                        "This page.";
        #endregion

        #region ICommand
        public ICommand ViewStartPageCommand { get; set; }
        #endregion

        public void ViewStartPage()
        {

            var page = new StartPage();
            ((MainWindow)Application.Current.MainWindow).Main.Content = page;
        }
    }

}
