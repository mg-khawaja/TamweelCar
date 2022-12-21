using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TamweelyMobileApp.Droid
{
    public static class LocaleUtil
    {
        public static void SetLocale(Context context, Locale locale)
        {
            Locale.Default = locale;
            Configuration configuration = context.Resources.Configuration;
            configuration.SetLocale(locale);
            configuration.SetLayoutDirection(locale);
            context.CreateConfigurationContext(configuration);
        }
    }
}