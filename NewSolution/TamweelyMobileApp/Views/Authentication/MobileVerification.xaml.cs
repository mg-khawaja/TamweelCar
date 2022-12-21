using Plugin.Multilingual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamweelyMobileApp.Models;
using TamweelyMobileApp.Services.Implementations;
using TamweelyMobileApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TamweelyMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MobileVerification : ContentPage
    {
        MobileVerificationViewModel vm;
        public MobileVerification(UserModel user)
        {
            InitializeComponent();
            initializeCulture();
            this.Title = Resx.TamweelyResources.Mobile_Verification;
            backButton.ImageSource = ImageSource.FromResource("TamweelyMobileApp.Images.arrowBack.png");
            arrowBack.Source = ImageSource.FromResource("TamweelyMobileApp.Images.arrow back.png");
            arrowWhite.Source = ImageSource.FromResource("TamweelyMobileApp.Images.arrow white.png");

            vm = new MobileVerificationViewModel(Navigation, new UserAuthenticateService(new RequestProvider()), user);
            BindingContext = vm;
        }
        private async void initializeCulture()
        {
            var culture = CrossMultilingual.Current.CurrentCultureInfo;
            if (culture.Name.ToUpper() == "AR")
            {
                await arrowWhite.RotateTo(180);
                await backButton.RotateTo(180);
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //firstPin.Unfocus();
            //secondPin.Unfocus();
            //thirdPin.Unfocus();
            //forthPin.Unfocus();
            //vm.FirstPin = firstPin.Value?.ToString();
            //vm.SecondPin = secondPin.Value?.ToString();
            //vm.ThirdPin = thirdPin.Value?.ToString();
            //vm.ForthPin = forthPin.Value?.ToString();
            vm.SelectedOTP = controlOTP.SelectedOtp;
            vm.VerifyCommand.Execute(null);
        }

        private void backButton_Clicked(object sender, EventArgs e)
        {
        }
    }
}