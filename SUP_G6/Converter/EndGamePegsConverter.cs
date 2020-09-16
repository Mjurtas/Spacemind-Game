using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

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
                    return new SolidColorBrush(Colors.Red);
                case 2:
                    return new SolidColorBrush(Colors.Blue);
                case 3:
                    return new SolidColorBrush(Colors.Yellow);
                case 4:
                    return new SolidColorBrush(Colors.Green);
                case 5:
                    return new SolidColorBrush(Colors.Purple);
                case 6:
                    return new SolidColorBrush(Colors.Pink);
                case 7:
                    return new SolidColorBrush(Colors.LightBlue);
                case 8:
                    return new SolidColorBrush(Colors.Orange);
               
            };
            return new SolidColorBrush(Colors.White);
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
