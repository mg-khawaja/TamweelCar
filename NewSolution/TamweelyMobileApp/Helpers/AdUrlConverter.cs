using System;
using System.Globalization;
using Xamarin.Forms;
namespace TamweelyMobileApp.Helpers
{
    public class AdUrlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value as string;
            if (str != null)
            {
                return GlobalSetting.Instance.AdLink + str;
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
