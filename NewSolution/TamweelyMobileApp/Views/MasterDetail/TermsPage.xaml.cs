using Plugin.Multilingual;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using TamweelyMobileApp.Helpers;
using TamweelyMobileApp.Models;
using TamweelyMobileApp.Resx;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TamweelyMobileApp.Views.MasterDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TermsPage : ContentPage
	{
        public ObservableCollection<Language> Languages { get; }
		public TermsPage()
		{
			InitializeComponent ();
			initializeCulture();
			backButton.ImageSource = ImageSource.FromResource("TamweelyMobileApp.Images.arrowBack.png");
			
			BindingContext = this;
			
			webview.Source = GlobalSetting.Instance.Links[0];
		}
		private void initializeCulture()
		{
			var culture = CrossMultilingual.Current.CurrentCultureInfo;
			if (culture.Name.ToUpper() == "AR")
			{
				backButton.RotateTo(180);
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
    }
}