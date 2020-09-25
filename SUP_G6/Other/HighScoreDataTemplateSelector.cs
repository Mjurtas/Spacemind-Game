using SUP_G6.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SUP_G6.Other
{
    public class HighScoreDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            if (element != null && item != null && item is GameResult)
            {
                return element.FindResource("GameResultDataTemplate") as DataTemplate;
            }

            return element.FindResource("DiligentPlayerDataTemplate") as DataTemplate;
        }
    }
}
