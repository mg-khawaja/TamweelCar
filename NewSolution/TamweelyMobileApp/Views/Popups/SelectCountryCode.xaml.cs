using TamweelyMobileApp.Helpers;
using TamweelyMobileApp.ViewModels;
using Xamarin.Forms;

namespace TamweelyMobileApp.Views.Popups
{
    public partial class SelectCountryCodePopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        object vm;
        AuthPage page;
        public SelectCountryCodePopup(object viewmodel, AuthPage page)
        {
            InitializeComponent();
            vm = viewmodel;
            this.page = page;
            if(Application.Current.RequestedTheme == OSAppTheme.Dark)
            {
                this.BackgroundColor = Color.Black;
                frame.BackgroundColor = Color.Black;
                frame.BorderColor = Color.White;
                label.TextColor = Color.White;
            }
        }
        private void filterText_TextChanged(object sender, TextChangedEventArgs e)
        {
            switch (page)
            {
                case AuthPage.Login:
                    (vm as LoginViewModel).SearchCountryCodesCommand.Execute(e.NewTextValue);
                    break;
                case AuthPage.Signup:
                    (vm as SignUpPageViewModel).SearchCountryCodesCommand.Execute(e.NewTextValue);
                    break;
                case AuthPage.EditProfile:
                    (vm as EditProfileViewModel).SearchCountryCodesCommand.Execute(e.NewTextValue);
                    break;
                default:
                    break;
            }
        }

        private void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            switch (page)
            {
                case AuthPage.Login:
                    (vm as LoginViewModel).CountryCodeSelectedCommand.Execute(e.Item);
                    break;
                case AuthPage.Signup:
                    (vm as SignUpPageViewModel).CountryCodeSelectedCommand.Execute(e.Item);
                    break;
                case AuthPage.EditProfile:
                    (vm as EditProfileViewModel).CountryCodeSelectedCommand.Execute(e.Item);
                    break;
                default:
                    break;
            }
        }
    }
}