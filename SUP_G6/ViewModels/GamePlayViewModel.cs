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
            SecretCode = GameLogic.GenerateSecretCode();
            CreateNewGameResult();
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
        }
        public int[] Guess { get; set; }
        //public List<object> GuessOne { get; set; }
        public int[] SecretCode { get; set; }
        public bool MediumLevel { get; set;  } = false;
        public bool HardLevel { get; set; } = false;
        public PegPosition[] feedbacklist;
        public Player player;
        private Stopwatch _stopWatch;
        public Level level;
        public ObservableCollection<PegPosition> feedbackPegs = new ObservableCollection<PegPosition>();
        public ObservableCollection<PegPosition> FeedbackPegs { get; set; }

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

        #region Feedback-pegs Propertys
        public PegPosition FeedbackPeg1 { get; set; }
        public PegPosition FeedbackPeg2 { get; set; }
        public PegPosition FeedbackPeg3 { get; set; }
        public PegPosition FeedbackPeg4 { get; set; }

        public bool FeedbackPeg1Visibility { get; set; } = false;
        public bool FeedbackPeg2Visibility { get; set; } = false;
        public bool FeedbackPeg3Visibility { get; set; } = false;
        public bool FeedbackPeg4Visibility { get; set; } = false;
        #endregion

        //public PegPosition FeedbackPeg
        //{
        //    get { return FeedbackPeg; }
        //    set { FeedbackPeg = PegPosition.CorrectColorAndPosition; }
        //}


        //public ICommand CheckFeedBackCommand { get; set; }

        public ICommand GuessCommand { get; set; }
      

        private void ExecuteGuess()
        {
            //nån kod här
            var feedback = GameLogic.Feedback(SecretCode, Guess);
            SetFeedbackPegs(feedback);

            //if (gameHasEnded)
            //{
            //    _stopWatch.Stop();
            //    GameResult results = new GameResult
            //    {
            //        PlayerId = player.Id,
            //        PlayerName = player.Name,
            //        Tries = NumberOfTries,
            //        ElapsedTicks = _stopWatch.ElapsedTicks,
            //        Level = level,
            //        Win = false
            //    };
            //    SaveToDatabase(gameresult);
            //}
        }

        public void SetFeedbackPegs(PegPosition[] feedback)
        {
            PegPosition feedback1;
            PegPosition feedback2;
            PegPosition feedback3;
            PegPosition feedback4;

            //Random rnd = new Random();
            //PegPosition array = array.OrderBy(x => rnd.Next()).ToArray();
            for (int i = 0; i < feedback.Length; i++)
            {
                feedbackPegs.Add(feedback[i]);
                feedback1 = feedback[0];
                feedback2 = feedback[1];
                feedback3 = feedback[2];
                feedback4 = feedback[3];

            }
          
            FeedbackPegs = feedbackPegs;
            for (int i = feedbackPegs.Count-4; i < feedbackPegs.Count; i++)
            {

                //FeedbackPegs[i] = feedback1;
                //FeedbackPegs[i] = feedback2;
                //FeedbackPegs[i] = feedback3;
                //FeedbackPegs[i] = feedback4;



                //switch ()
                //    {
                //        case 0:
                //            FeedbackPegs[i] = feedback[p];
                //            FeedbackPeg1Visibility = true;
                //            break;
                //        case 1:
                //            FeedbackPegs[i] = feedback[p];
                //            FeedbackPeg2Visibility = true;
                //            break;
                //        case 2:
                //            FeedbackPegs[i] = feedback[p];
                //            FeedbackPeg3Visibility = true;
                //            break;
                //        case 3:
                //            FeedbackPegs[i] = feedback[p];
                //            FeedbackPeg4Visibility = true;
                //            break;
                //        default:
                //            break;
                //    }

            }
        }
        public void SetGuess(Panel guessPanel)
        {
            UIElementCollection guessedPegs = guessPanel.Children;
            int[] guess = new int[4];

            foreach (MasterPeg peg in guessedPegs)
            {
                int colorId = peg.ColorId;
                int position = guessedPegs.IndexOf(peg);
                guess.SetValue(colorId, position);
            }
            Guess = guess;
        }

        #region VM till DB
        private void CreateNewGameResult()
        {
            GameResult gameResult = new GameResult()
            {
                PlayerId = player.Id,
                PlayerName = player.Name,
                Level = this.level
            };

            gameResult.GameId = DataBaseLogic.AddGameResult(gameResult);
        }
        #endregion





    }
}