using Plugin.Multilingual;
using System;
using System.Collections.Generic;
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
    public partial class LoginPage : ContentPage
    {
        LoginViewModel vm;
        public LoginPage()
        {
            InitializeComponent();
            initializeCulture();
            arrowBack.Source = ImageSource.FromResource("TamweelyMobileApp.Images.arrow back.png");
            arrowWhite.Source = ImageSource.FromResource("TamweelyMobileApp.Images.arrow white.png");
            vm = new LoginViewModel(Navigation, new UserAuthenticateService(new RequestProvider()));
            this.BindingContext = vm;
        }
        private async void initializeCulture()
        {
            var culture = CrossMultilingual.Current.CurrentCultureInfo;
            if (culture.Name.ToUpper() == "AR")
            {
                await arrowWhite.RotateTo(180);
            }
        }

        private void Login_Tapped(object sender, EventArgs e)
        {
            maskedEdit.Unfocus();
            vm.LoginCommand.Execute(null);
        }
    }
}