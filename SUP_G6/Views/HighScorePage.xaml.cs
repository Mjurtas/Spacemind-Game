using SUP_G6.ViewModels;
using System;
using System.Collections.Generic;
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

namespace SUP_G6.Views
{
    /// <summary>
    /// Interaction logic for HighScorePage.xaml
    /// </summary>
    public partial class HighScorePage : Page
    {
        public HighScorePage()
        {
            InitializeComponent();
            DataContext = new HighScoreViewModel();
                    
        }

        public async void ButtonFeedback(Button button)
        {
            await Task.Delay(500);
            button.FontSize = 20;
        }

        private void TimeButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            btn.FontSize = 40;
            ButtonFeedback(btn);
        }

        private void TriesButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            btn.FontSize = 40;
            ButtonFeedback(btn);
        }

        private void MostPlayedButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            btn.FontSize = 40;
            ButtonFeedback(btn);
        }
    }
}
