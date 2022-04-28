using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UserInfoDesktopDisplayer.Converters
{
    public class DateTimeNullableStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime? date = value as DateTime?;

            if (!date.HasValue)
                return string.Empty;

            return date.Value.ToString("MM/dd/yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string dateString = value as string;

            if (DateTime.TryParse(dateString, out DateTime date))
                return date;

            return (DateTime?)null;
        }
    }
}
