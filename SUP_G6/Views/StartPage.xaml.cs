﻿using SUP_G6.ViewModels;
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

namespace SUP_G6.Views
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        StartViewModel viewModel;
        public StartPage()
        {
            InitializeComponent();
            viewModel = new StartViewModel();
            DataContext = viewModel;
        }        
    }
}
