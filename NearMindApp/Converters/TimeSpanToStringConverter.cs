using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NearMindApp.Converters
{
    public class TimeSpanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeSpan timeSpan)
            {
                return timeSpan.ToString(@"hh\:mm"); // Formato 24 horas
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (TimeSpan.TryParse(value as string, out TimeSpan timeSpan))
            {
                return timeSpan;
            }
            return TimeSpan.Zero;
        }
    }
}
