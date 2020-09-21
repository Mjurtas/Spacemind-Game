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

namespace SUP_G6.ViewModels
{
    class GamePlayViewModel : BaseViewModel.BaseViewModel, INotifyPropertyChanged
    {
        #region constructor
        SoundPlayer snd;
        public GamePlayViewModel(Player player, Level level)
        {
            snd = new SoundPlayer(Properties.Resources.cantinaband);
            snd.PlayLooping();

            GuessCommand = new RelayCommand(ExecuteGuess);
            this.player = player;
            this.level = level;
            SetLevelVisibility();
            SecretCode = GameLogic.GenerateSecretCode(level);
            CreateTimer();

            RestartGameCommand = new RelayCommand(ReloadGamePlayPage);
            BackToStartCommand = new RelayCommand(GoBackToStartPage);

        }
        #endregion


        #region Public Properties
        public int[] Guess { get; set; }
        public int[] SecretCode { get; set; }
        public bool MediumLevel { get; set; } = false;
        public bool HardLevel { get; set; } = false;
       
        public int TimeLabel { get; set; } = 0;
        public Player player;
        public Level level;
        public string ToMessageBox { get; set; }
        public int NumberOfTries { get; set; } = 0;
        public bool WinPanelVisibility { get; set; } = false;
        public bool LosePanelVisibility { get; set; } = false;

        public string ButtonGuess { get; set; } = "guess!";
        public string ButtonReset { get; set; } = "reset";

        public ICommand RestartGameCommand { get; set; }
        public ICommand BackToStartCommand { get; set; }
        #endregion

        #region Feedback-pegs Properties
        public ObservableCollection<PegPosition> feedbackPegs = new ObservableCollection<PegPosition>();
        public ObservableCollection<PegPosition> FeedbackPegs { get; set; }
        public ObservableCollection<bool> feedbackPegsVisibility = new ObservableCollection<bool>();
        public ObservableCollection<bool> FeedbackPegsVisibility { get; set; }
        #endregion

        #region Set Level Visibility
        
        public void SetLevelVisibility()
        {
            if (level == Level.Medium)
            {
                MediumLevel = true;

            }
            if (level == Level.Hard)
            {
                MediumLevel = true;
                HardLevel = true;

            }
        }

        #endregion
        #region Timer
        DispatcherTimer dispatcherTimer;
        public void CreateTimer()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            TimeLabel++;
        }
        #endregion




        #region Command for Guess Button
        public ICommand GuessCommand { get; set; }
        public static object Stopwatch1 { get; private set; }

        private void ExecuteGuess()
        {
            //int[] testkod = new int[] { 1, 2, 3, 4 };

            if (Guess != null)
            {
              
                    var feedback = GameLogic.Feedback(SecretCode, Guess);
                    SetFeedbackPegs(feedback);
                    NumberOfTries += 1;           
            }
            else
            {
                ToMessageBox = "Du måste gissa minst fyra färger";
            }

          

        }

        #endregion

        #region Set and show feedback pegs

        public void SetFeedbackPegs(PegPosition[] feedback)
        {
            for (int i = 0; i < feedback.Length; i++)
            {
                feedbackPegs.Add(feedback[i]);
                feedbackPegsVisibility.Add(true);

            }
            FeedbackPegs = feedbackPegs;
            FeedbackPegsVisibility = feedbackPegsVisibility;
            int counter = 0;
            for (int i = FeedbackPegs.Count - 4; i < FeedbackPegs.Count; i++)
            {
                switch (counter)
                {
                    case 0:
                        FeedbackPegs[i] = feedback[0];
                        FeedbackPegsVisibility[i] = true;
                        break;
                    case 1:
                        FeedbackPegs[i] = feedback[1];
                        FeedbackPegsVisibility[i] = true;
                        break;
                    case 2:
                        FeedbackPegs[i] = feedback[2];
                        FeedbackPegsVisibility[i] = true;
                        break;
                    case 3:
                        FeedbackPegs[i] = feedback[3];
                        FeedbackPegsVisibility[i] = true;
                        break;
                    default:
                        break;
                }
                counter++;
            }

            CheckWin(feedbackPegs);
        }

        #endregion

        #region Check Win and EndPageNavigation

        public void CheckWin(ObservableCollection<PegPosition> feedbackPegs)
        {

            if (!feedbackPegs.Skip(NumberOfTries*4).Contains(PegPosition.CorrectColorWrongPosition) && !feedbackPegs.Skip(NumberOfTries * 4).Contains(PegPosition.TotallyWrong))
            {
                dispatcherTimer.Stop();
                CreateNewGameResult();
                snd = new SoundPlayer(Properties.Resources.win_fanfare);
                snd.Play();
                WinPanelVisibility = true;

            }

            else if (NumberOfTries == 10)
            {
                dispatcherTimer.Stop();
                snd.Stop();
                LosePanelVisibility = true;
            }


        }
        public void ReloadGamePlayPage()
        {
            var page = new GamePlayPage(player, level);
            ((MainWindow)Application.Current.MainWindow).Main.Content = page;
        }

        public void GoBackToStartPage()
        {
            snd = new SoundPlayer(Properties.Resources.starwars);
            snd.Play();
            var page = new StartPage();
            ((MainWindow)Application.Current.MainWindow).Main.Content = page;
        }
        #endregion
            

        #region DataBase Communication

        private void CreateNewGameResult()
        {
            GameResult gameResult = new GameResult()
            {
                PlayerId = player.Id,
                PlayerName = player.Name,
                Level = this.level,
                Win = true,
                ElapsedTimeInSeconds = (double)TimeLabel,
                Tries = this.NumberOfTries
                
            };

            gameResult.GameId = DataBaseLogic.AddGameResult(gameResult);
            
        }

        #endregion





    }
}