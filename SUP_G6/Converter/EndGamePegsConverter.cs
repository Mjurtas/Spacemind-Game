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
            var color = value;
            switch (color)
            {
                case 1:
                    ImageBrush imageSource = new ImageBrush();
                    imageSource.ImageSource = new BitmapImage(new Uri(@".\Assets\Images\peg1av.png", UriKind.Relative));
                    return imageSource;
                case 2:
                    ImageBrush imageSource2 = new ImageBrush();
                    imageSource2.ImageSource = new BitmapImage(new Uri(@".\Assets\Images\peg2av.png", UriKind.Relative));
                    return imageSource2;
                case 3:
                    ImageBrush imageSource3 = new ImageBrush();
                    imageSource3.ImageSource = new BitmapImage(new Uri(@".\Assets\Images\peg3av.png", UriKind.Relative));
                    return imageSource3;
                case 4:
                    ImageBrush imageSource4 = new ImageBrush();
                    imageSource4.ImageSource = new BitmapImage(new Uri(@".\Assets\Images\peg4av.png", UriKind.Relative));
                    return imageSource4;
                case 5:
                    ImageBrush imageSource5 = new ImageBrush();
                    imageSource5.ImageSource = new BitmapImage(new Uri(@".\Assets\Images\peg5av.png", UriKind.Relative));
                    return imageSource5;
                case 6:
                    ImageBrush imageSource6 = new ImageBrush();
                    imageSource6.ImageSource = new BitmapImage(new Uri(@".\Assets\Images\peg6av.png", UriKind.Relative));
                    return imageSource6;
                case 7:
                    ImageBrush imageSource7 = new ImageBrush();
                    imageSource7.ImageSource = new BitmapImage(new Uri(@".\Assets\Images\peg4av.png", UriKind.Relative));
                    return imageSource7;
                case 8:
                    ImageBrush imageSource8 = new ImageBrush();
                    imageSource8.ImageSource = new BitmapImage(new Uri(@".\Assets\Images\peg4av.png", UriKind.Relative));
                    return imageSource8;
            };
            return new SolidColorBrush(Colors.White);
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
