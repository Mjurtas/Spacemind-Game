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
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
            DataContext = new StartViewModel();
        }



        public void ChangePage()
        {
            var page1 = new HighScorePage();
            ((MainWindow)Application.Current.MainWindow).Main.Content = page1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChangePage();
        }

        public void ChangeToChoosePlayerPage()
        {
            var page = new ChoosePlayerPage();
            ((MainWindow)Application.Current.MainWindow).Main.Content = page;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ChangeToChoosePlayerPage();
        }
    }
}
