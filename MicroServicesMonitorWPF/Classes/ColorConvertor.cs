using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Color = System.Windows.Media.Color;
using DrawingColor = System.Drawing.Color;

namespace MicroServicesMonitorWPF.Classes
{
    [ValueConversion(typeof(DrawingColor), typeof(Brush))]
    public class ColorConvertor:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return DependencyProperty.UnsetValue;
            }

            var color = (DrawingColor)value;
            return new SolidColorBrush(ToMediaColor(color));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return DependencyProperty.UnsetValue;
            }
            var brush = (SolidColorBrush)value;
            var color = brush.Color;

            return ToColor(color);
        }

        public static Color ToMediaColor(DrawingColor color)
        {
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public static DrawingColor ToColor(Color color)
        {
            return DrawingColor.FromArgb(color.A, color.R, color.G, color.B);
        }
    }
}