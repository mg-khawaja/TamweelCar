using Plugin.Multilingual;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamweelyMobileApp.Helpers;
using TamweelyMobileApp.Models;
using TamweelyMobileApp.Resx;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TamweelyMobileApp.Views.MasterDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPage : ContentPage
    {
        public ObservableCollection<Language> Languages { get; }
        ViewModels.MasterDetailViewModel vm;
        public MasterDetailPage()
        {
            InitializeComponent();
            initializeCulture();

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

            vm = new ViewModels.MasterDetailViewModel(Navigation, new Services.Implementations.VehiclesService(new Services.Implementations.RequestProvider()));
            BindingContext = vm;
            List<string> list = new List<string>();
            list.Add(TamweelyResources.home);
            list.Add(TamweelyResources.edit_profile);
            list.Add(TamweelyResources.privacy_policy);
            list.Add(TamweelyResources.terms);
            list.Add(TamweelyResources.logout);
            listView.ItemsSource = list;

            //MessagingCenter.Subscribe<string>(this, "ToggleBalance", (sender) =>
            //{
            //    bottomNavigationDrawer.ToggleDrawer();
            //});
        }

        private async void PickerLanguages_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            var lang = e.Value as Language;
            var db = await SQL.SQLiteDB.Instance;
            if (lang != null)
            {
                comboboxImage.Source = lang.Flag;
                comboboxLabel.Text = lang.ShortName;
                if (CrossMultilingual.Current.CurrentCultureInfo.Name.ToUpper() != lang.ShortName.ToUpper())
                {
                    var culture = new CultureInfo(lang.ShortName);
                    TamweelyResources.Culture = culture;
                    CrossMultilingual.Current.CurrentCultureInfo = culture;
                    await db.SaveLanguageAsync(lang);
                    (Application.Current as App).MainPage = new NavigationPage(new MasterDetail.MasterDetailPage());
                }
            }
        }
        private void hamburger_Tapped(object sender, EventArgs e)
        {
            mainNavigationDrawer.ToggleDrawer();
        }
        private void NextBtn_Tapped(object sender, EventArgs e)
        {
            var par = (e as TappedEventArgs).Parameter;
            salary.Unfocus();
            al.Unfocus();
            ml.Unfocus();
            pl.Unfocus();
            cc.Unfocus();
            vm.CalcBalanceCommand.Execute(par);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.Name = GlobalSetting.Instance.userModel.FullName;
        }
        private async void initializeCulture()
        {
            var culture = CrossMultilingual.Current.CurrentCultureInfo;
            if (culture.Name.ToUpper() == "AR")
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                mainNavigationDrawer.Position = Syncfusion.SfNavigationDrawer.XForms.Position.Right;
                PickerLanguages.SelectedIndex = 1;
                await arrowWhite.RotateTo(180);
                if(Device.RuntimePlatform == Device.iOS)
                {
                    container.Padding = new Thickness(0, 15, 15, 0);
                }
                else
                {
                    container.Padding = new Thickness(15);
                }
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
                mainNavigationDrawer.Position = Syncfusion.SfNavigationDrawer.XForms.Position.Left;
                PickerLanguages.SelectedIndex = 0;
                if (Device.RuntimePlatform == Device.iOS)
                {
                    container.Padding = new Thickness(15, 15, 0, 0);
                }
                else
                {
                    container.Padding = new Thickness(15);
                }
            }
        }
        private async void DrawerItem_Tapped(object sender, EventArgs e)
        {
            var view = sender as StackLayout;
            if (view != null)
            {
                var label = view.Children[0] as Label;
                if (label != null)
                {
                    if (label.Text == TamweelyResources.home)
                    {
                        mainNavigationDrawer.ToggleDrawer();
                    }
                    else if (label.Text == TamweelyResources.edit_profile)
                    {
                        await Navigation.PushAsync(new EditProfilePage());
                        mainNavigationDrawer.ToggleDrawer();
                    }
                    else if (label.Text == TamweelyResources.privacy_policy)
                    {
                        await Navigation.PushAsync(new PrivacyPolicyPage());
                        mainNavigationDrawer.ToggleDrawer();
                    }
                    else if (label.Text == TamweelyResources.terms)
                    {
                        await Navigation.PushAsync(new TermsPage());
                        mainNavigationDrawer.ToggleDrawer();
                    }
                    else if (label.Text == TamweelyResources.logout)
                    {
                        var logout = await (Application.Current as App).MainPage.DisplayAlert(TamweelyResources._continue, TamweelyResources.confirm, TamweelyResources.ok, TamweelyResources.cancel);
                        if (logout)
                        {
                            var db = await SQL.SQLiteDB.Instance;
                            await db.DeleteAllUsersAsync();
                            await db.DeleteAllTokensAsync();
                            (Application.Current as App).MainPage = new NavigationPage(new LoginPage());
                        }

                    }
                }
            }
        }
    }
}
