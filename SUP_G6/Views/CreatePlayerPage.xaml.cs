using SUP_G6.Models;
using SUP_G6.Other;
using SUP_G6.ViewModels;
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
    /// Interaction logic for CreatePlayerPage.xaml
    /// </summary>
    public partial class CreatePlayerPage : Page
    {
        public CreatePlayerPage()
        {
            InitializeComponent();
            DataContext = new CreatePlayerViewModel();
        }

        /*private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ChangePageToGamePlayPage();
        }
        private void ChangePageToGamePlayPage()
        {
            Player testPlayer = new Player();
            GamePlayPage page12 = new GamePlayPage(testPlayer);
            ((MainWindow)Application.Current.MainWindow).Main.Content = page12;
        }*/
    }
}

