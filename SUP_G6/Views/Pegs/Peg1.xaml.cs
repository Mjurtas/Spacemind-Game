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
    public partial class Peg1 : MasterPeg
    {
        public Peg1()
        {
            InitializeComponent();
            CreatePeg();
        }

        private void CreatePeg()
        {
            Ellipse ellipse = new Ellipse
            {
                Fill = Brushes.Red,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            ColorId = 1;
            LevelVisibility = Level.Easy;
            master.Children.Add(ellipse);
        }
        //private void CreateRedPeg()
        //{
        //    Ellipse ellipse = new Ellipse
        //    {
        //        Fill = Brushes.Red,
        //        Stroke = Brushes.Black,
        //        StrokeThickness = 2
        //    };
        //    ColorId = 1;
        //    master.Children.Add(ellipse);
        //}
        //private void CreateBluePeg()
        //{
        //    Ellipse ellipse = new Ellipse
        //    {
        //        Fill = Brushes.Blue,
        //        Stroke = Brushes.Black,
        //        StrokeThickness = 2
        //    };
        //    ColorId = 1;
        //    master.Children.Add(ellipse);
        //}
        //private void CreateYellowPeg()
        //{
        //    Ellipse ellipse = new Ellipse
        //    {
        //        Fill = Brushes.Yellow,
        //        Stroke = Brushes.Black,
        //        StrokeThickness = 2
        //    };
        //    ColorId = 3;
        //    master.Children.Add(ellipse);
        //}
        //private void CreateGreenPeg()
        //{
        //    Ellipse ellipse = new Ellipse
        //    {
        //        Fill = Brushes.Green,
        //        Stroke = Brushes.Black,
        //        StrokeThickness = 2
        //    };
        //    ColorId = 4;
        //    master.Children.Add(ellipse);
        //}
        //private void CreatePurplePeg()
        //{
        //    Ellipse ellipse = new Ellipse
        //    {
        //        Fill = Brushes.Purple,
        //        Stroke = Brushes.Black,
        //        StrokeThickness = 2
        //    };
        //    ColorId = 5;
        //    master.Children.Add(ellipse);
        //}
        //private void CreatePinkPeg()
        //{
        //    Ellipse ellipse = new Ellipse
        //    {
        //        Fill = Brushes.Pink,
        //        Stroke = Brushes.Black,
        //        StrokeThickness = 2
        //    };
        //    ColorId = 6;
        //    master.Children.Add(ellipse);
        //}
    }
}
