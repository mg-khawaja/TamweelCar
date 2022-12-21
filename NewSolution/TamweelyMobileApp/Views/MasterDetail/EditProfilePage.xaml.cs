using Plugin.Multilingual;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using TamweelyMobileApp.Models;
using TamweelyMobileApp.Resx;
using TamweelyMobileApp.Services.Implementations;
using TamweelyMobileApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TamweelyMobileApp.Views.MasterDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfilePage : ContentPage
    {
        EditProfileViewModel vm;
        public EditProfilePage()
        {
            InitializeComponent();
            initializeCulture();
            backButton.ImageSource = ImageSource.FromResource("TamweelyMobileApp.Images.arrowBack.png");
            arrowBack.Source = ImageSource.FromResource("TamweelyMobileApp.Images.arrow back.png");
            arrowWhite.Source = ImageSource.FromResource("TamweelyMobileApp.Images.arrow white.png");
            vm = new EditProfileViewModel(Navigation,new UserAuthenticateService(new RequestProvider()));
            BindingContext = vm;
            //var str = TamweelyResources.delete_account;
            //deleteBtn.Text = str;
        }
        private async void initializeCulture()
        {
            var culture = CrossMultilingual.Current.CurrentCultureInfo;
            if (culture.Name.ToUpper() == "AR")
            {
                await arrowWhite.RotateTo(180);
                await backButton.RotateTo(180);
                this.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        private void backButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void NextBtn_Tapped(object sender, EventArgs e)
        {
            nameEntry.Unfocus();
            phoneEntry.Unfocus();
            vm.SaveCommand.Execute(null);
        }
    }
}