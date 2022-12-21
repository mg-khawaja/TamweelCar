using System;
using System.Globalization;
using Xamarin.Forms;
namespace TamweelyMobileApp.Helpers
{
    public class DblToMillionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                double val = double.Parse(value.ToString());
                var res = val.ToString("#,#", CultureInfo.InvariantCulture);
                if (string.IsNullOrEmpty(res))
                    return 0;
                return res;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
