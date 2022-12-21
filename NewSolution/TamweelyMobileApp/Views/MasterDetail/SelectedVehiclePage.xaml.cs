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
    public partial class SelectedVehiclePage : ContentPage
    {
        public SelectedVehicleViewModel vm { get; }
        public ObservableCollection<RotatorModel> imageCollection { get; }
        public ObservableCollection<Language> Languages { get; }
        public SelectedVehiclePage(VehicleModel vehicle, double balance)
        {
            try
            {
                InitializeComponent();

                initializeCulture();

                vm = new SelectedVehicleViewModel(new VehiclesService(new RequestProvider()), vehicle, Navigation, balance,
                    MonthsSlider, InterestSlider, InsuranceSlider, InstallmentSlider, DownPaySlider, LastPaySlider);
                BindingContext = vm;
                backButton.Source = ImageSource.FromResource("TamweelyMobileApp.Images.arrowBack.png");
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
            }
            catch (Exception ex)
            {

            }
        }
        private async void initializeCulture()
        {
            var culture = CrossMultilingual.Current.CurrentCultureInfo;
            if (culture.Name.ToUpper() == "AR")
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                await backButton.RotateTo(180);
                InstallmentSlider.WidthRequest = 235;
                Grid.SetColumn(MonthsMin, 2);
                Grid.SetColumn(InterestMin, 2);
                Grid.SetColumn(InsuranceMin, 2);
                InstallmentMin.IsVisible = false;
                InstallmentMinArabic.IsVisible = true;
                InstallmentMax.IsVisible = false;
                InstallmentMaxArabic.IsVisible = true;
                //Grid.SetColumn(InstallmentMin, 2);
                Grid.SetColumn(DownPayMin, 2);
                Grid.SetColumn(LastPayMin, 2);

                Grid.SetColumn(MonthsMax, 1);
                Grid.SetColumn(InterestMax, 1);
                Grid.SetColumn(InsuranceMax, 1);
                //Grid.SetColumn(InstallmentMax, 1);
                Grid.SetColumn(DownPayMax, 1);
                Grid.SetColumn(LastPayMax, 1);
            }
            else
            {
                InstallmentSlider.WidthRequest = 240;
                InstallmentMin.IsVisible = true;
                InstallmentMinArabic.IsVisible = false;
                InstallmentMax.IsVisible = true;
                InstallmentMaxArabic.IsVisible = false;
                this.FlowDirection = FlowDirection.LeftToRight;
            }

        }
        private async void backButton_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        private void filterButton_Tapped(object sender, EventArgs e)
        {
            //bottomNavigationDrawer.ToggleDrawer();
        }
        
        private void MonthsSlider_DragCompleted(object sender, EventArgs e)
        {
            vm.MonthsSelected = Convert.ToInt32(MonthsSlider.Value);
        }
        private void InterestSlider_DragCompleted(object sender, EventArgs e)
        {
            vm.InterestPercSelected = InterestSlider.Value;
        }
        private void InsuranceSlider_DragCompleted(object sender, EventArgs e)
        {
            vm.InsurancePercSelected = InsuranceSlider.Value;   
        }
        private void InstallmentSlider_DragCompleted(object sender, EventArgs e)
        {
            vm.InstallmentSelected = InstallmentSlider.Value;
        }
        private void DownPaySlider_DragCompleted(object sender, EventArgs e)
        {
            vm.DownPayPercSelected = DownPaySlider.Value;
        }
        private void LastPaySlider_DragCompleted(object sender, EventArgs e)
        {
            vm.LastPayPercSelected = LastPaySlider.Value;
        }
    }
}