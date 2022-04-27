using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace RgbToHexDesktopConverter.Converters
{
    public class RgbAddConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = Color.FromRgb((byte)values[0], (byte)values[1], (byte)values[2]);
            return color.R.ToString() + "U+002C" + color.G.ToString() + "U+002C" + color.B.ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
