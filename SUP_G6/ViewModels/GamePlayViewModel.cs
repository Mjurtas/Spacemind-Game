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

namespace SUP_G6.ViewModels
{
    class GamePlayViewModel : BaseViewModel.BaseViewModel, INotifyPropertyChanged
    {
        public GamePlayViewModel(Player player, Level level)
        {

            GuessCommand = new RelayCommand(ExecuteGuess);
            this.player = player;
            this.level = level;
            SetLevelVisibility();
            SecretCode = GameLogic.GenerateSecretCode(level);
           
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
        }

        #region Public Propertys
        public int[] Guess { get; set; }
        //public List<object> GuessOne { get; set; }
        public int[] SecretCode { get; set; }
        public bool MediumLevel { get; set; } = false;
        public bool HardLevel { get; set; } = false;
        private Stopwatch _stopWatch;
        //public PegPosition[] feedbacklist;
        public Player player;
        public Level level;
        public string ToMessageBox { get; set; }
        public int NumberOfTries { get; set; } = 1;
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

        //public PegPosition FeedbackPeg
        //{
        //    get { return FeedbackPeg; }
        //    set { FeedbackPeg = PegPosition.CorrectColorAndPosition; }
        //}

        #region Command for Guess Button

        //public ICommand CheckFeedBackCommand { get; set; }

        public ICommand GuessCommand { get; set; }
        public static object Stopwatch1 { get; private set; }

        private void ExecuteGuess()
        {
            int[] testkod = new int[] { 1, 2, 3, 4 };
            
            if (Guess != null)
            {
                ToMessageBox = "";
                var feedback = GameLogic.Feedback(/*SecretCode, Guess*/testkod, Guess );
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
            //Random rnd = new Random();
            //PegPosition array = array.OrderBy(x => rnd.Next()).ToArray();
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
        //public void SetGuess(Panel guessPanel)
        //{
        //    UIElementCollection guessedPegs = guessPanel.Children;
        //    int[] guess = new int[4];

        //    foreach (MasterPeg peg in guessedPegs)
        //    {
        //        int colorId = peg.ColorId;
        //        int position = guessedPegs.IndexOf(peg);
        //        guess.SetValue(colorId, position);
        //    }
        //    Guess = guess;
        //}

        #endregion

        public void CheckWin (ObservableCollection<PegPosition> feedbackPegs)
        {
            
                if (!feedbackPegs.Contains(PegPosition.CorrectColorWrongPosition) || !feedbackPegs.Contains(PegPosition.TotallyWrong))
                {
                    _stopWatch.Stop();
                    CreateNewGameResult();
                }
                
            
        }
            
        #region VM till DB

        private void CreateNewGameResult()
        {
            GameResult gameResult = new GameResult()
            {
                PlayerId = player.Id,
                PlayerName = player.Name,
                Level = this.level,
                Win = true,
                ElapsedTimeInSeconds = _stopWatch.Elapsed.TotalSeconds,
                Tries = this.NumberOfTries
                
            };

            gameResult.GameId = DataBaseLogic.AddGameResult(gameResult);
            
        }

        #endregion





    }
}