using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace SUP_G6.Converter
{
    class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var pegPosition = (PegPosition)value;

            switch (pegPosition)
            {
                case PegPosition.CorrectColorAndPosition:
                    return new SolidColorBrush(Colors.Green);
                case PegPosition.CorrectColorWrongPosition:
                    return new SolidColorBrush(Colors.Yellow);
                case PegPosition.TotallyWrong:
                    return new SolidColorBrush(Colors.Black); /*new SolidColorBrush(Colors.Red);*/
            };
            return new SolidColorBrush(Colors.Blue);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
