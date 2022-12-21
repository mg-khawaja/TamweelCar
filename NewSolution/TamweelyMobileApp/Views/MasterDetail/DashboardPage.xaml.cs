using Plugin.Multilingual;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamweelyMobileApp.Models;
using TamweelyMobileApp.Resx;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TamweelyMobileApp.Views.MasterDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : ContentView
    {
        ViewModels.MasterDetailViewModel vm;
        public ObservableCollection<Language> Languages { get; }
        public DashboardPage(ViewModels.MasterDetailViewModel viewModel)
        {
            InitializeComponent();
            initializeCulture();
            vm = viewModel;
            arrowBack.Source = ImageSource.FromResource("TamweelyMobileApp.Images.arrow back.png");
            arrowWhite.Source = ImageSource.FromResource("TamweelyMobileApp.Images.arrow white.png");
            hamburger.Source = ImageSource.FromResource("TamweelyMobileApp.Images.hamburger.png");
            imageDumy.Source = ImageSource.FromResource("TamweelyMobileApp.Images.imageDumy.png");
            Languages = new ObservableCollection<Language>()
            {
                new Language { DisplayName =  "English", ShortName = "en", Flag = "en.png" },
                new Language { DisplayName =  "Arabic", ShortName = "ar", Flag = "ar.png" }
            };
            PickerLanguages.DataSource = Languages;
            PickerLanguages.SelectionChanged += PickerLanguages_SelectionChanged;
            MessagingCenter.Subscribe<string>(this, "ToggleBalance", (sender) =>
            {
                navigationDrawer.ToggleDrawer();
            });
        }

        private void PickerLanguages_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            var lang = e.Value as Language;
            if(lang != null)
            {
                comboboxImage.Source = lang.Flag;
                comboboxLabel.Text = lang.ShortName;
                if (CrossMultilingual.Current.CurrentCultureInfo.Name.ToUpper() != lang.ShortName.ToUpper())
                {
                    var culture = new CultureInfo(lang.ShortName);
                    TamweelyResources.Culture = culture;
                    CrossMultilingual.Current.CurrentCultureInfo = culture;
                    (Application.Current as App).MainPage = new NavigationPage(new MasterDetail.MasterDetailPage());
                }
            }
            
        }
        private async void initializeCulture()
        {
            var culture = CrossMultilingual.Current.CurrentCultureInfo;
            if (culture.Name.ToUpper() == "AR")
            {
                PickerLanguages.SelectedIndex = 1;
                await arrowWhite.RotateTo(180);
            }
            else
            {
                PickerLanguages.SelectedIndex = 0;
            }
        }
        private void hamburger_Tapped(object sender, EventArgs e)
        {
            MessagingCenter.Send("ToggleDrawer", "ToggleDrawer");
        }
        private void NextBtn_Tapped(object sender, EventArgs e)
        {
            salary.Unfocus();
            al.Unfocus();
            ml.Unfocus();
            pl.Unfocus();
            cc.Unfocus();
            vm.CalcBalanceCommand.Execute(null);
        }
    }
}