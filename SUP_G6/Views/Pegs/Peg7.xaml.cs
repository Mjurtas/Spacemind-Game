﻿using System;
using SUP_G6.DataTypes;
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
    public partial class Peg7 : MasterPeg
    {
        public Peg7()
        {
            InitializeComponent();
            CreatePeg();
        }
        private void CreatePeg()
        {
            Ellipse ellipse = new Ellipse
            {
                Fill = Brushes.LightBlue,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            ColorId = 7;
            LevelVisibility = Level.Hard;
            master.Children.Add(ellipse);
        }
    }
}