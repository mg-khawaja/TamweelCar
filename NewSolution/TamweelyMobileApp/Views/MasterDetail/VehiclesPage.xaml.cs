using Plugin.Multilingual;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamweelyMobileApp.Models;
using TamweelyMobileApp.Services.Implementations;
using TamweelyMobileApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TamweelyMobileApp.Views.MasterDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VehiclesPage : ContentPage
    {
        public VehiclesViewModel vm { get; }
        public ObservableCollection<RotatorModel> imageCollection { get; }
        public ObservableCollection<Language> Languages { get; }
        public VehiclesPage(double balance)
        {
            InitializeComponent();
            initializeCulture();

            vm = new VehiclesViewModel(new VehiclesService(new RequestProvider()), new UserAuthenticateService(new RequestProvider()), 
                balance, Navigation, bottomNavigationDrawer);
            BindingContext = vm;
            backButton.Source = ImageSource.FromResource("TamweelyMobileApp.Images.arrowBack.png");
            filterButton.Source = ImageSource.FromResource("TamweelyMobileApp.Images.filterIcon.png");

            imageCollection = new ObservableCollection<RotatorModel>()
            {
                new RotatorModel("mainLogo.png"),
                new RotatorModel("mainLogo.png"),
                new RotatorModel("mainLogo.png"),
                new RotatorModel("mainLogo.png"),
            };

            Languages = new ObservableCollection<Language>()
            {
                new Language { DisplayName =  "English", ShortName = "en" },
                new Language { DisplayName =  "Arabic", ShortName = "ar" },
                new Language { DisplayName =  "English", ShortName = "en" },
                new Language { DisplayName =  "Arabic", ShortName = "ar" }
            };
            vm.LoadMoreItemsCommand.Execute(listView);
        }
        private async void initializeCulture()
        {
            var culture = CrossMultilingual.Current.CurrentCultureInfo;
            if (culture.Name.ToUpper() == "AR")
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                await backButton.RotateTo(180);
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        private async void backButton_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void filterButton_Tapped(object sender, EventArgs e)
        {
            bottomNavigationDrawer.ToggleDrawer();
        }
    }
}