using SUP_G6.ViewModels.BaseViewModel;
using SUP_G6.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows;
using System.Windows.Input;
using static System.Windows.Application;

namespace SUP_G6.ViewModels
{
    public class MainWindowViewModel : BaseViewModel.BaseViewModel
    {
        SoundPlayer starwarsMainTheme;
        SoundPlayer CantinaBand;
        SoundPlayer imperialMarch;
        List<SoundPlayer> soundList = new List<SoundPlayer>();
        int trackCounter = 0;

       

        public MainWindowViewModel()
        {
            
            
         
            PopulateSongList();
            soundList[trackCounter].PlayLooping();
            SoundOffCommand = new RelayCommand(SoundOff);
            SoundOnCommand = new RelayCommand(SoundOn);
            PreviousSongCommand = new RelayCommand(PrevSong);
            NextSongCommand = new RelayCommand(NextSong);
        }

        #region Public Properties
        public Visibility TurnSoundOff { get; set; } = Visibility.Visible;
        public Visibility TurnSoundOn { get; set; } = Visibility.Collapsed;
        #endregion

        #region ICommands
        public ICommand SoundOffCommand { get; set; }
        public ICommand SoundOnCommand { get; set; }
        public ICommand PreviousSongCommand { get; set; }
        public ICommand NextSongCommand { get; set; }
        #endregion


        #region SoundPlayer-logic
        private void PopulateSongList()
        {
            soundList.Add(starwarsMainTheme = new SoundPlayer(Properties.Resources.starwars));
            soundList.Add(CantinaBand = new SoundPlayer(Properties.Resources.cantinaband));
            soundList.Add(imperialMarch = new SoundPlayer(Properties.Resources.imperial));
        }
        private void SoundOff()
        {
            soundList[trackCounter].Stop();
            TurnSoundOff = Visibility.Collapsed;
            TurnSoundOn = Visibility.Visible;
        }
        private void SoundOn()
        {
            soundList[trackCounter].Play();
            TurnSoundOn = Visibility.Collapsed;
            TurnSoundOff = Visibility.Visible;
        }


        private void NextSong()
        {
            var lastIndex = soundList.Count() - 1;
            if (TurnSoundOn == Visibility.Visible)
            {
                if (trackCounter == (int)lastIndex)
                {
                    trackCounter = 0;
                    
                }

                else
                {
                    trackCounter++;
                   
                }

            }

            else
            {
                if (trackCounter == (int)lastIndex)
                {
                    trackCounter = 0;
                    soundList[trackCounter].Play();
                }

                else
                {
                    trackCounter++;
                    soundList[trackCounter].Play();
                }
            }


        }

        private void PrevSong()
        {
            if (TurnSoundOn == Visibility.Visible)
            {
                if (trackCounter == 0)
                {
                    trackCounter = soundList.Count();
                    trackCounter--;
                    
                }

                else
                {
                    trackCounter--;
                }
            }

            else
            {
                if (trackCounter == 0)
                {
                    trackCounter = soundList.Count();
                    trackCounter--;
                    soundList[trackCounter].Play();


                }

                else
                {
                    trackCounter--;
                    soundList[trackCounter].Play();
                }

            }

        }
        #endregion



    }
}

