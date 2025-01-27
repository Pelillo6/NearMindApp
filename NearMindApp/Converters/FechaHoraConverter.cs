using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NearMindApp.Converters
{
    public class FechaHoraConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime fechaHora)
            {
                if (fechaHora.Date == DateTime.Today)
                {
                    return fechaHora.ToString("HH:mm");
                }
                else
                {
                    return fechaHora.ToString("dd/MM/yyyy");
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
