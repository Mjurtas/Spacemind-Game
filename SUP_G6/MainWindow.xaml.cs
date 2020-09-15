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
        SoundPlayer snd;
        public MainWindow()
        {
            InitializeComponent();
            var page = new StartPage();
            Main.Content = page;

            //var myPlayer = new System.Media.SoundPlayer();
            //myPlayer.SoundLocation = @"c:\Music\starwars.wav";
            //myPlayer.PlayLooping();

            snd = new SoundPlayer(Properties.Resources.starwars);
            snd.Play();

        }


    }
}
