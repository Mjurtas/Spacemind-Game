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
using System.Threading.Tasks;

namespace SUP_G6.ViewModels
{
    class GamePlayViewModel : BaseViewModel.BaseViewModel, INotifyPropertyChanged
    {
        #region constructor
      
        public GamePlayViewModel(Player player, Level level)
        {

            CreateTimerForCountDown();
            GuessCommand = new RelayCommand(ExecuteGuess);
            this.player = player;
            this.level = level;
            SetLevelVisibility();
            SecretCode = GameLogic.GenerateSecretCode(level);
            

            RestartGameCommand = new RelayCommand(ReloadGamePlayPage);
            BackToStartCommand = new RelayCommand(GoBackToStartPage);
            ResetButtonCommand = new RelayCommand(ResetButton);
            EndGameCommand = new RelayCommand(EndGame);
        }
        #endregion


        #region Public Properties
        SoundPlayer snd;
        public int[] Guess { get; set; } 
        public int[] SecretCode { get; set; }
        public bool MediumLevel { get; set; } = false;
        public bool HardLevel { get; set; } = false;       
        public double TimeLabel { get; set; } = 0;
       
        public Player player;
        public Level level;
        public string ToMessageBox { get; set; } = "You must use 4 avatars for acceptable guess";
        public int NumberOfTries { get; set; } = 0;
        public bool WinPanelVisibility { get; set; } = false;
        public bool LosePanelVisibility { get; set; } = false;
        public static object Stopwatch1 { get; private set; }
        public string LabelTries { get; set; } = "tries";
        public string LabelTime { get; set; } = "time";
        public string ButtonGuess { get; set; } = "guess!";
        public string ButtonDelete { get; set; } = "delete";
        public string ButtonEndGame { get; set; } = "exit";
        public int CountDownLabel { get; set; } = 3;
        public bool IsGuessButtonEnabled { get; set; } = true;
        public int ScoreTimerCount { get; set; } = 0; // Updates every 50ms
        public int DefaultScore { get; set; } = 10000; 
        public int TotalScore { get; set; }
        public int EndGameScorePresenter { get; set; }

        public Visibility GamePlayPageVisibility { get; set; } = Visibility.Collapsed;
        public Visibility CountDownLabelVisibility { get; set; } = Visibility.Visible;


        #endregion

        #region ICommands        
        public ICommand RestartGameCommand { get; set; }
        public ICommand BackToStartCommand { get; set; }
        public ICommand GuessCommand { get; set; }
        public ICommand ResetButtonCommand { get; set; }
        public ICommand EndGameCommand { get; set; }

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
        DispatcherTimer scoreTimer;
        DispatcherTimer countDownTimer;


        // Timer for setting time in game.
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

        //Timer for animation in endgamepage.

        public void CreateTimerForScore()
        {
            if (WinPanelVisibility == false)
            {
                scoreTimer = new DispatcherTimer();
                scoreTimer.Tick += new EventHandler(ScoreTimer_Tick);
                scoreTimer.Interval = new TimeSpan(0, 0, 0, 0, 415);
                scoreTimer.Start();
            }

            else
            {
                scoreTimer.Tick += new EventHandler(ScorePresenter_Tick);
                scoreTimer.Interval = new TimeSpan(0, 0, 0, 0, 20);
                scoreTimer.Start();
            }
        }

        private void ScoreTimer_Tick(object sender, EventArgs e)
        {
            ScoreTimerCount++;
        }

        private async void ScorePresenter_Tick(object sender, EventArgs e)
        {
            TotalScore = DefaultScore - (NumberOfTries * ScoreTimerCount);

            while (EndGameScorePresenter < TotalScore)
            {
                await Task.Delay(1);
                EndGameScorePresenter++;

            }
        }


        // Timer for countdown pre-gameplay.
        public void CreateTimerForCountDown()
        {
            countDownTimer = new DispatcherTimer();
            countDownTimer.Tick += new EventHandler(countDownTimer_Tick);
            countDownTimer.Interval = new TimeSpan(0, 0, 1);
            countDownTimer.Start();
        }

        private void countDownTimer_Tick(object sender, EventArgs e)
        {
            CountDownLabel--;
            if (CountDownLabel < 0)
            {
                CountDownLabelVisibility = Visibility.Collapsed;
                countDownTimer.Stop();
                GamePlayPageVisibility = Visibility.Visible;
                CreateTimer();
                CreateTimerForScore();
            }

           
            

            
        }

       

       
        #endregion



        #region Command for Guess Button

        private void ExecuteGuess()
        {
            if (Guess != null)
            {
                var feedback = GameLogic.Feedback(SecretCode, Guess);
                SetFeedbackPegs(feedback);
                NumberOfTries += 1;
                Guess = null;
            }
            else
            {
                ToMessageBox = "You must use 4 avatars for acceptable guess";
            }
        }

        public async void EnableButton()
        {
            IsGuessButtonEnabled = false;
            await Task.Delay(2000);
            IsGuessButtonEnabled = true;
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
                
                snd = new SoundPlayer(Properties.Resources.win_fanfare);
                snd.Play();
                WinPanelVisibility = true;
                CreateTimerForScore();
                CreateNewGameResult();

            }

            else if (NumberOfTries == 10)
            {
                dispatcherTimer.Stop();
                
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
                ElapsedTimeInSeconds = TimeLabel,
                Tries = NumberOfTries,
                TotalScore = this.TotalScore
            };

            gameResult.GameId = DataBaseLogic.AddGameResult(gameResult);
            
        }

        #endregion

        #region Reset Button Method
        private void ResetButton()
        {

        }
        #endregion

        #region End Game Method
        public void EndGame()
        {
            if (MessageBox.Show("Do you want to end the game and return to the Start Page?", "End Game", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                var page = new StartPage();
                ((MainWindow)Application.Current.MainWindow).Main.Content = page;
            }
        }
            
          
        #endregion





    }
}