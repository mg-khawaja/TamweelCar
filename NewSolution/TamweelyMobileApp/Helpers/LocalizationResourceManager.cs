using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using TamweelyMobileApp.Resx;
using Xamarin.Essentials;

namespace TamweelyMobileApp.Helpers
{
    public class LocalizationResourceManager : INotifyPropertyChanged
    {
        private const string LanguageKey = nameof(LanguageKey);

        public static LocalizationResourceManager Instance { get; } = new LocalizationResourceManager();

        private LocalizationResourceManager()
        {
            SetCulture(new CultureInfo(Preferences.Get(LanguageKey, CurrentCulture.TwoLetterISOLanguageName)));
        }

        public string this[string text]
        {
            get
            {
                return TamweelyResources.ResourceManager.GetString(text, TamweelyResources.Culture);
            }
        }

        public void SetCulture(CultureInfo language)
        {
            Thread.CurrentThread.CurrentUICulture = language;
            TamweelyResources.Culture = language;
            Preferences.Set(LanguageKey, language.TwoLetterISOLanguageName);

        }

        public string GetValue(string text, string ResourceId)
        {
            ResourceManager resourceManager = new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly);
            return resourceManager.GetString(text, CultureInfo.CurrentCulture);
        }

        public CultureInfo CurrentCulture => TamweelyResources.Culture ?? Thread.CurrentThread.CurrentUICulture;

        public event PropertyChangedEventHandler PropertyChanged;

        public void Invalidate()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
