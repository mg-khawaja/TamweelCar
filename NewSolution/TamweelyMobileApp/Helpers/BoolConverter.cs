using System;
using System.Globalization;
using Xamarin.Forms;
namespace TamweelyMobileApp.Helpers
{
    public class BoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if ((value as bool?)==true)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
