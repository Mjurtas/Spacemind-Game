using Microsoft.Win32;
using SUP_G6.ViewModels;
using SUP_G6.Views;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;
using System.Media;
using System.IO;

namespace SUP_G6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SoundPlayer starwarsMainTheme;
        SoundPlayer CantinaBand;
        SoundPlayer imperialMarch;
        List<SoundPlayer> soundList = new List<SoundPlayer>();

        int trackCounter = 0;
        
        public MainWindow()
        {
            InitializeComponent();
            var page = new StartPage();
            Main.Content = page;

            //var myPlayer = new System.Media.SoundPlayer();
            //myPlayer.SoundLocation = @"c:\Music\starwars.wav";
            //myPlayer.PlayLooping();

            soundList.Add (starwarsMainTheme = new SoundPlayer(Properties.Resources.starwars));
            soundList.Add(CantinaBand = new SoundPlayer(Properties.Resources.cantinaband));
            soundList.Add(imperialMarch = new SoundPlayer(Properties.Resources.imperial));

            soundList[trackCounter].PlayLooping();

           
        }

        

        
        bool sound = true;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            if (sound == true)
            {
                soundList[trackCounter].Stop();
                sound = false;
                
            }

            else if (sound == false)
            {
                soundList[trackCounter].PlayLooping();
                sound = true;
               
            }
        }
        private void btnSoundOff_Click(object sender, RoutedEventArgs e)
        {
            soundList[trackCounter].Stop();
            btnTurnSoundOff.Visibility = Visibility.Collapsed;
            btnTurnSoundOn.Visibility = Visibility.Visible;

        }
        private void btnSoundOn_Click(object sender, RoutedEventArgs e)
        {
            soundList[trackCounter].Play();
            btnTurnSoundOn.Visibility = Visibility.Collapsed;
            btnTurnSoundOff.Visibility = Visibility.Visible;

        }






        private void btnNextSong_Click(object sender, RoutedEventArgs e)
        {
            var lastIndex = soundList.Count() - 1;
            if (trackCounter == (int)lastIndex)
            {
                trackCounter = 0;
                soundList[trackCounter].PlayLooping();
            }

            else
            {
                trackCounter++;
                soundList[trackCounter].PlayLooping();
            }

        }

        private void btnPrevSong_Click(object sender, RoutedEventArgs e)
        {
            if (trackCounter == 0)
            {
                trackCounter = soundList.Count();
                trackCounter--;
                soundList[trackCounter].PlayLooping();
            }

            else
            {
                trackCounter--;
                soundList[trackCounter].PlayLooping();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
