using System;
using TamweelyMobileApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TamweelyMobileApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTk1ODM1QDMxMzkyZTM0MmUzMFVOaWFBUU9WL1hqMnFzMXlIZ0xZRG1xVE9QdzYwTWZjYS9leE5uWGRUNWc9");
            //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDI0NTk3QDMxMzkyZTMxMmUzMFViMzhrT2ZrSUZGclhrbklLUU9EVDBLSEZ2bVdGVjh6NXI2dVFRa3FEcUE9");
            //DependencyService.Register<MockDataStore>();
            MainPage = new NavigationPage(new ChooseLanguage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
