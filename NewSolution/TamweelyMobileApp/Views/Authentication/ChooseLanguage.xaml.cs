using Plugin.Multilingual;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TamweelyMobileApp.Models;
using TamweelyMobileApp.Resx;
using TamweelyMobileApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TamweelyMobileApp.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChooseLanguage : ContentPage
    {
        ChooseLanguageViewModel vm;

        public ChooseLanguage()
        {
            InitializeComponent();
            vm = new ChooseLanguageViewModel(Navigation);
            BindingContext = vm;
            initializeCulture();
            arrowBack.Source = ImageSource.FromResource("TamweelyMobileApp.Images.arrow back.png");
            arrowWhite.Source = ImageSource.FromResource("TamweelyMobileApp.Images.arrow white.png");

            
            PickerLanguages.SelectionChanged += PickerLanguages_SelectionChanged;
        }

        private async void PickerLanguages_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            var language = vm.Languages[PickerLanguages.SelectedIndex];
            var db = await SQL.SQLiteDB.Instance;
            try
            {
                if(CrossMultilingual.Current.CurrentCultureInfo.Name.ToUpper() != language.ShortName.ToUpper())
                {
                    var culture = new CultureInfo(language.ShortName);
                    TamweelyResources.Culture = culture;
                    CrossMultilingual.Current.CurrentCultureInfo = culture;
                    await db.SaveLanguageAsync(language);
                    (Application.Current as App).MainPage = new NavigationPage(new ChooseLanguage());
                }
            }
            catch (Exception)
            {
            }
        }
        private async void initializeCulture()
        {

            var culture = CrossMultilingual.Current.CurrentCultureInfo;
            if (culture.Name.ToUpper() == "AR")
            {
                await arrowWhite.RotateTo(180);
            }
        }
    }
}