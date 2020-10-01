using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SUP_G6.Converter
{
    class EndGamePegsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageBrush imageSource = new ImageBrush();
            var pegId = value;
            switch (pegId)
            {
                case 1:
                     
                    imageSource.ImageSource = new BitmapImage(new Uri(@".\Assets\Images\peg1av.png", UriKind.Relative));
                    return imageSource;
                case 2:
                    
                    imageSource.ImageSource = new BitmapImage(new Uri(@".\Assets\Images\peg2av.png", UriKind.Relative));
                    return imageSource;
                case 3:
                    
                    imageSource.ImageSource = new BitmapImage(new Uri(@".\Assets\Images\peg3av.png", UriKind.Relative));
                    return imageSource;
                case 4:
                    
                    imageSource.ImageSource = new BitmapImage(new Uri(@".\Assets\Images\peg4av.png", UriKind.Relative));
                    return imageSource;
                case 5:
                    
                    imageSource.ImageSource = new BitmapImage(new Uri(@".\Assets\Images\peg5av.png", UriKind.Relative));
                    return imageSource;
                case 6:
                   
                    imageSource.ImageSource = new BitmapImage(new Uri(@".\Assets\Images\peg6av.png", UriKind.Relative));
                    return imageSource;
                case 7:
                    
                    imageSource.ImageSource = new BitmapImage(new Uri(@".\Assets\Images\peg7av.png", UriKind.Relative));
                    return imageSource;
                case 8:
                    
                    imageSource.ImageSource = new BitmapImage(new Uri(@".\Assets\Images\peg8av.png", UriKind.Relative));
                    return imageSource;
            };
            return new SolidColorBrush(Colors.White);
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
