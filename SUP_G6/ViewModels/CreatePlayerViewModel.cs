using SUP_G6.Interface;
using SUP_G6.Models;
using SUP_G6.Other;
using SUP_G6.ViewModels.BaseViewModel;
using SUP_G6.Views;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Npgsql;

namespace SUP_G6.ViewModels
{
    //ViewModel that handles the users wish to create a player
    public class CreatePlayerViewModel 
    {
        #region Constructor

        public CreatePlayerViewModel()
        {

            CreatePlayerCommand = new RelayCommand(CreatePlayer);
            BackButtonCommand = new RelayCommand(BackToStart);
        }

        #endregion

        #region Properties

        public IPlayer Player { get; set; }
        public string Name { get; set; }

        #region Content Bindings

        public string BackButton { get; set; } = "back";
        public string CreatePlayerButton { get; set; } = "create player";
        public string CreatePlayerLabel { get; set; } = "create player";
        public string NameLabel { get; set; } = "name";

        #endregion

        #endregion

        #region ICommand

        public ICommand BackButtonCommand { get; set; }
        public ICommand CreatePlayerCommand { get; set; }

        #endregion

        #region Create Player Method

        public void CreatePlayer()
        {
            if (Name.Length > 9)
            {
                MessageBox.Show($"You hit the maximum length! Baby Yoda says you need a shorter name");
            }
            if (Name != null)
            {
                IPlayer player = new Player
                {
                    Name = Name.ToLower()  //The font convert text to ToUpper but doesnt render capitalized letters well in inputsstrings.
                };

                Player = player;

                try
                {
                    Player.Id = DataBaseLogic.AddPlayer(player);
                    ToChooseLevelPage(player);
                }
                catch (PostgresException error)
                {
                    if (error.SqlState == "23505")
                    {
                        MessageBox.Show($"The name {player.Name} already exists. Pick another nickname you must.");
                        Name = "";
                    }
                }
            }
        }

        #endregion

        #region Navigation Methods

        private void BackToStart()
        {
            var page1 = new StartPage();
            ((MainWindow)Application.Current.MainWindow).Main.Content = page1;
        }

        private void ToChooseLevelPage(IPlayer player)
        {
            var page = new SelectLevelPage(player);
            ((MainWindow)Application.Current.MainWindow).Main.Content = page;
        }

        #endregion
    }
}
