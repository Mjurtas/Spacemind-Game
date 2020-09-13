using SUP_G6.Models;
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
    /// Interaction logic for ChoosePlayerPage.xaml
    /// </summary>
    /// 

 
    public partial class ChoosePlayerPage : Page
    {
        ChoosePlayerViewModel viewModel;
        public ChoosePlayerPage()
        {
            InitializeComponent();
            viewModel = new ChoosePlayerViewModel();
            DataContext = viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChangePageToGamePlayPage();
        }

        private void ChangePageToGamePlayPage()
        {
            Player player = (Player)playerListbox.SelectedItem;
            viewModel.CreateGame();
            string level = viewModel.Level;
            var page = new GamePlayPage(player, level);
            ((MainWindow)Application.Current.MainWindow).Main.Content = page;
        }
    }
}
