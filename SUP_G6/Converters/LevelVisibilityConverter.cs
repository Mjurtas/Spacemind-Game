//using SUP_G6.DataTypes;
//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Text;
//using System.Windows;
//using System.Windows.Data;

//namespace SUP_G6.Converter
//{
//    class LevelVisibilityConverter : IValueConverter
//    {
//        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            var level = (bool)value;

//            switch (level)
//            {
//                case true:
//                    return Visibility.Visible;
//                case false:
//                    return Visibility.Collapsed;
//            }
//        }
//        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            throw new NotImplementedException();
//        }

//    }
//}
