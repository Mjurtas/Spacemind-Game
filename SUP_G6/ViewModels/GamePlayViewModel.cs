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
using SUP_G6.Interface;
using System.Threading;

namespace SUP_G6.ViewModels
{
    //ViewModel for the actual GamePlay
    class GamePlayViewModel : BaseViewModel.BaseViewModel, INotifyPropertyChanged
    {
        #region Fields

        SoundPlayer snd;

        DispatcherTimer dispatcherTimer;
        DispatcherTimer scoreTimer;
        DispatcherTimer countDownTimer;

        public IPlayer player;
        public Level level;
        public IGameResult gameResult;

        public ObservableCollection<bool> feedbackPegsVisibility = new ObservableCollection<bool>();
        public ObservableCollection<PegPosition> feedbackPegs = new ObservableCollection<PegPosition>();

        #endregion

        #region Constructor

        public GamePlayViewModel(IPlayer player, Level level)
        {

            CreateTimerForCountDown();
            GuessCommand = new RelayCommand(ExecuteGuess);
            this.player = player;
            this.level = level;
            SetLevelVisibility();
            SecretCode = GameLogic.GenerateSecretCode(level);
            

            RestartGameCommand = new RelayCommand(ReloadGamePlayPage);
            BackToStartCommand = new RelayCommand(GoBackToStartPage);
            EndGameCommand = new RelayCommand(EndGame);
            ShowHintCommand = new RelayCommand(ShowHint);
        }

        private void ShowHint()
        {
            switch (HelpPanelVisibility)
            {
                case Visibility.Visible:
                    HelpPanelVisibility = Visibility.Collapsed;
                    break;
                case Visibility.Collapsed:
                    HelpPanelVisibility = Visibility.Visible;
                    break;

            }
        }
        #endregion

        #region Properties

        public int[] Guess { get; set; } 
        public int[] SecretCode { get; set; }

        public int CountDownLabel { get; set; } = 3;
        public int EndGameScorePresenter { get; set; }
        public int NumberOfTries { get; set; } = 0;
        public int ScoreTimerCount { get; set; } = 0; // Updates every 50ms
        public double TotalScore { get; set; }

        public double TimeLabel { get; set; } = 0;

        public bool MediumLevel { get; set; } = false;
        public bool HardLevel { get; set; } = false;
        public bool WinPanelVisibility { get; set; } = false;
        public bool LosePanelVisibility { get; set; } = false;
        public bool IsGuessButtonEnabled { get; set; } = true;
        public bool Win { get; set; }

        public static object Stopwatch1 { get; private set; }

        #region Content Bindings

        public string ToMessageBox { get; set; } = "You must use 4 avatars for acceptable guess";
        public string LabelTries { get; set; } = "tries";
        public string LabelTime { get; set; } = "time";
        public string ButtonGuess { get; set; } = "guess!";
        public string ButtonDelete { get; set; } = "delete";
        public string ButtonEndGame { get; set; } = "exit";
        public string HintsLabel { get; set; } = "hints";
        public string GreenPegHint { get; set; } = ": Right avatar and right spot, good job!";
        public string YellowPegHint { get; set; } = ": Right avatar, but wrong spot!";
        public string BlackPegHint { get; set; } = ": No way José, try again!";
        public string RestartGameButton { get; set; } = "restart game";
        public string BackToStartButton { get; set; } = "back to start";
        public string GameOverLabel { get; set; } = "game over";
        public string ScoreLabel { get; set; } = "score: ";
        public string TimeTextLabel { get; set; } = "time";
        public string TriesLabel { get; set; } = "tries";
        public string SecretCodeLabel { get; set; } = "The secretcode was:";
        public string CongratsLabel { get; set; } = "congratulations";

        #endregion

        public Visibility GamePlayPageVisibility { get; set; } = Visibility.Collapsed;
        public Visibility CountDownLabelVisibility { get; set; } = Visibility.Visible;
        public Visibility HelpPanelVisibility { get; set; } = Visibility.Collapsed;

        public ObservableCollection<PegPosition> FeedbackPegs { get; set; }
        public ObservableCollection<bool> FeedbackPegsVisibility { get; set; }

        #endregion

        #region ICommands       

        public ICommand RestartGameCommand { get; set; }
        public ICommand BackToStartCommand { get; set; }
        public ICommand GuessCommand { get; set; }
        public ICommand EndGameCommand { get; set; }
        public ICommand ShowHintCommand { get; set; }

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

        #region Timer Methods

        //Timer for setting time in game
        public void CreateTimer()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        //Updating the time property
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            TimeLabel++;
        }

        //Timer for calculating the score
        public void CreateTimerForScore()
        {
                scoreTimer = new DispatcherTimer();
                scoreTimer.Tick += new EventHandler(ScoreTimer_Tick);
                scoreTimer.Interval = new TimeSpan(0, 0, 0, 0, 415);
                scoreTimer.Start();
        }

        //Updating the timer that is used to calculate the score
        private void ScoreTimer_Tick(object sender, EventArgs e)
        {
            ScoreTimerCount++;
        }

        //Timer for animating the score in Win Panel
        public void CreateTimerForPresentingScore()
        {
            scoreTimer.Tick += new EventHandler(ScorePresenter_Tick);
            scoreTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            scoreTimer.Start();
        }
    
        //Updating the property that is binded in Win Panel until it matches TotalScore
        private void ScorePresenter_Tick(object sender, EventArgs e)
        {

            if (EndGameScorePresenter < TotalScore)
            {
                EndGameScorePresenter += 33;
            }
            
            

        }

        //Timer for countdown pre-gameplay.
        public void CreateTimerForCountDown()
        {
            countDownTimer = new DispatcherTimer();
            countDownTimer.Tick += new EventHandler(countDownTimer_Tick);
            countDownTimer.Interval = new TimeSpan(0, 0, 1);
            countDownTimer.Start();
        }

        //Updating the Count Down Property
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

        #region Methods triggered by GuessCommand

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

        #region Method for setting FeedbackPegs

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

        #region Win/Lose Panel Methods

        //Checks if the game is over and triggers the suitable panel (win or lose)
        public void CheckWin(ObservableCollection<PegPosition> feedbackPegs)
        {
            //If the last 4 FeedbackPegs only contains CorrectColorAndPosition then the player has won the game
            if (!feedbackPegs.Skip(NumberOfTries*4).Contains(PegPosition.CorrectColorWrongPosition) && !feedbackPegs.Skip(NumberOfTries * 4).Contains(PegPosition.TotallyWrong))
            {
                dispatcherTimer.Stop();
                scoreTimer.Stop();
                SetTotalScore();
                Win = true;
                CreateNewGameResult();
                snd = new SoundPlayer(Properties.Resources.win_fanfare);
                snd.Play();
                WinPanelVisibility = true;
                CreateTimerForPresentingScore();
            }

            //If the player has guessed 10 times (NumberOfTries starts at 0) then the player has lost the game
            else if (NumberOfTries == 9)
            {
                dispatcherTimer.Stop();                
                LosePanelVisibility = true;
                Win = false;
                CreateNewGameResult();
            }
        }

        private void SetTotalScore()
        {
           TotalScore = 10000 - (((NumberOfTries + 1) * ScoreTimerCount) * 0.7);
        }

        #region Win/Lose Panel Navigation Methods

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

        #endregion

        #region DataBase Communication Methods

        private void CreateNewGameResult()

        {
            IGameResult gameResult = new GameResult()
            {
                PlayerId = player.Id,
                PlayerName = player.Name,
                Level = this.level,
                Win = Win,
                ElapsedTimeInSeconds = TimeLabel,
                Tries = NumberOfTries + 1,
                TotalScore = Math.Round(this.TotalScore, 0)
            };

            gameResult.GameId = DataBaseLogic.AddGameResult((GameResult)gameResult);
            
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