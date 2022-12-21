using Plugin.Multilingual;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TamweelyMobileApp.Services.Implementations;
using TamweelyMobileApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TamweelyMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        SignUpPageViewModel vm;
        public SignUpPage()
        {
            InitializeComponent();
            initializeCulture();
            backButton.ImageSource = ImageSource.FromResource("TamweelyMobileApp.Images.arrowBack.png");
            arrowBack.Source = ImageSource.FromResource("TamweelyMobileApp.Images.arrow back.png");
            arrowWhite.Source = ImageSource.FromResource("TamweelyMobileApp.Images.arrow white.png");
            vm = new SignUpPageViewModel(Navigation, new UserAuthenticateService(new RequestProvider()));
            this.BindingContext = vm;
        }
        private async void initializeCulture()
        {
            var data = CultureInfo.GetCultures(CultureTypes.AllCultures);
            var culture = CrossMultilingual.Current.CurrentCultureInfo;
            if (culture.Name.ToUpper() == "AR")
            {
                await arrowWhite.RotateTo(180);
                await backButton.RotateTo(180);
            }
        }
        private void Signup_Tapped(object sender, EventArgs e)
        {
            nameEntry.Unfocus();
            phoneEntry.Unfocus();
            vm.SignUpCommand.Execute(null);
        }
    }
}