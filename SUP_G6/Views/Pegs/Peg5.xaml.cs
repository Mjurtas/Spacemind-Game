using SUP_G6.DataTypes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SUP_G6
{
    /// <summary>
    /// Interaction logic for Peg1.xaml
    /// </summary>
    public partial class Peg5 : MasterPeg
    {
        public Peg5()
        {
            InitializeComponent();
            CreatePeg();
        }
        private void CreatePeg()
        {
            ImageBrush imageSource = new ImageBrush();
            imageSource.ImageSource = new BitmapImage(new Uri(@".\Assets\Images\peg5av.png", UriKind.Relative));
            Ellipse ellipse = new Ellipse
            {
                Fill = imageSource
            };

            PegId = 5;
            LevelVisibility = Level.Medium;
            master.Children.Add(ellipse);
        }
    }
}
