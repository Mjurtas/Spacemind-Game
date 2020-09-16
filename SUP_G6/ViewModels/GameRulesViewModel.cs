using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace SUP_G6.ViewModels
{
    class GameRulesViewModel
    {
        public string BackButton { get; set; } = "BACK";
        public string GameRules { get; set; } = "== How To Play ==\n \n" +
                                                "The computer will create a secret code consisting of four colours. The goal of the game is to guess the secret code in 10 tries or less. The player may drag any of the coloured pegs to the game board to make a four-colour guess, and then press the Guess button. The computer will provide feedback to the guess with four smaller pegs to the right, one for each player peg:\n \n" +
                                                "1. A Green feedback peg means a player peg is the right colour and in the right position.\n" +
                                                "2. A Yellow feedback peg means a player peg is a right color but in the wrong position.\n" +
                                                "3. A Red feedback peg means a player peg is neither the right colour nor the right position.\n \n" +
                                                "This will continue until you have spent all of the 10 guesses or until the code is cracked.\n \n" +
                                                "== Difficulty Levels ==\n \n" +
                                                "EASY\n" + 
                                                "The secret code will consist of 4 colours. \n \n" +
                                                "MEDIUM\n" +
                                                "The secret code will consist of 6 colours. \n \n" +
                                                "HARD\n" +
                                                "The secret code will consist of 8 colours. \n \n" +
                                                "== Start Menu ==\n \n" +
                                                "CREATE PLAYER\n" +
                                                "To create a new player, simply click “CREATE PLAYER”, type a name in the textbox, and then click “CREATE”.\n \n" +
                                                "CHOOSE PLAYER\n" +
                                                "Back for more? To play with an already existing player, click “CHOOSE PLAYER”, select a player from the list, a difficulty setting, and then click “PLAY GAME”.\n \n" +
                                                "VIEW HIGHSCORE\n" +
                                                "This page will present the best 3 players of any category.\n \n" +
                                                "GAME RULES\n" +
                                                "This page.";
        

    }

}
