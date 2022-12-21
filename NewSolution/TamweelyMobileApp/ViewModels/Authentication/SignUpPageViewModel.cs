using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using TamweelyMobileApp.Helpers;
using TamweelyMobileApp.Resx;
using TamweelyMobileApp.Services.Interfaces;
using TamweelyMobileApp.Views;
using TamweelyMobileApp.Views.Popups;
using Xamarin.Forms;

namespace TamweelyMobileApp.ViewModels
{
    public class SignUpPageViewModel : BaseViewModel
    {
        #region Fields
        private string phone;
        private TextAlignment textAlignment;
        private IUserAuthenticate authService;
        private CountryCode selectedCountryCode;
        private List<CountryCode> countryCodes;
        private List<CountryCode> originalCountryCodesList;
        private string name;
        private string phoneError;
        private string passwordError;
        #endregion

        #region Constructor
        public SignUpPageViewModel(INavigation navigationService, IUserAuthenticate service)
        {
            _navigationService = navigationService;
            authService = service;
            this.InitializeCommand = new Command(this.InitializeData);
            this.SelectCountryCodeCommand = new Command(this.SelectCountryCode);
            this.CountryCodeSelectedCommand = new Command(this.CountryCodeSelected);
            this.SearchCountryCodesCommand = new Command(this.SearchCountryCodes);
            this.SignUpCommand = new Command(this.SignUpClicked);
            this.GoBackCommand = new Command(this.GoBackClicked);
            InitializeCommand.Execute(null);
        }
        #endregion

        #region property
        private INavigation _navigationService { get; }
        public TextAlignment TextAlignment
        {
            get
            {
                return this.textAlignment;
            }

            set
            {
                if (this.textAlignment == value)
                {
                    return;
                }

                this.textAlignment = value;
                this.NotifyPropertyChanged();
            }
        }
        public CountryCode SelectedCountryCode
        {
            get
            {
                return this.selectedCountryCode;
            }

            set
            {
                if (this.selectedCountryCode == value)
                {
                    return;
                }

                this.selectedCountryCode = value;
                this.NotifyPropertyChanged();
            }
        }
        public List<CountryCode> CountryCodes
        {
            get
            {
                return this.countryCodes;
            }

            set
            {
                if (this.countryCodes == value)
                {
                    return;
                }

                this.countryCodes = value;
                this.NotifyPropertyChanged();
            }
        }
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (this.name == value)
                {
                    return;
                }

                this.name = value;
                this.NotifyPropertyChanged();
            }
        }
        public string Phone
        {
            get
            {
                return this.phone;
            }

            set
            {
                if (this.phone == value)
                {
                    return;
                }

                this.phone = value;
                this.NotifyPropertyChanged();
            }
        }
        public string PhoneError
        {
            get
            {
                return this.phoneError;
            }

            set
            {
                if (this.phoneError == value)
                {
                    return;
                }

                this.phoneError = value;
                this.NotifyPropertyChanged();
            }
        }
        public string PasswordError
        {
            get
            {
                return this.passwordError;
            }

            set
            {
                if (this.passwordError == value)
                {
                    return;
                }

                this.passwordError = value;
                this.NotifyPropertyChanged();
            }
        }
        #endregion

        #region Command
        public Command InitializeCommand { get; set; }
        public Command SelectCountryCodeCommand { get; set; }
        public Command CountryCodeSelectedCommand { get; set; }
        public Command SearchCountryCodesCommand { get; set; }
        public Command SignUpCommand { get; set; }
        public Command GoBackCommand { get; set; }
        #endregion

        #region methods
        private void InitializeData(object obj)
        {
            IsBusy = true;
            try
            {
                var file = "TamweelyMobileApp.Helpers.CountryCodes.json";

                var assembly = typeof(App).GetTypeInfo().Assembly;

                List<CountryCode> data;

                using (var stream = assembly.GetManifestResourceStream(file))
                {
                    var serializer = new DataContractJsonSerializer(typeof(List<CountryCode>));
                    data = (List<CountryCode>)serializer.ReadObject(stream);
                    originalCountryCodesList = CountryCodes = data;
                }
                SelectedCountryCode = CountryCodes.Where(cc => cc.dial_code == "+966").FirstOrDefault();
            }
            catch (Exception ex)
            {

            }

            IsBusy = false;
        }
        private void SearchCountryCodes(object obj)
        {
            try
            {
                var str = obj as string;
                if (string.IsNullOrEmpty(str))
                {
                    CountryCodes = originalCountryCodesList;
                    return;
                }
                CountryCodes = originalCountryCodesList.Where(occ => occ.name.ToLower().Contains(str.ToLower()) || occ.dial_code.ToLower().Contains(str.ToLower())).ToList();
            }
            catch (Exception ex)
            {

            }
        }
        private async void SelectCountryCode(object obj)
        {
            IsBusy = true;
            try
            {
                CountryCodes = originalCountryCodesList;
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new SelectCountryCodePopup(this,AuthPage.Signup)
                {
                    BindingContext = this
                });
            }
            catch (Exception ex)
            {

            }

            IsBusy = false;
        }
        private async void CountryCodeSelected(object obj)
        {
            IsBusy = true;
            var item = obj as CountryCode;
            if (item != null)
            {
                SelectedCountryCode = item;
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
            }
            IsBusy = false;
        }
        private async void SignUpClicked(object obj)
        {
            IsBusy = true;
            await Task.Delay(200);
            if (string.IsNullOrEmpty(Name))
            {
                await (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.error, TamweelyResources.enter_name, TamweelyResources.ok);
                IsBusy = false;
                return;
            }
            if (string.IsNullOrEmpty(Phone) || Phone.Replace(" ","").Length <= 8)
            {
                await (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.error, TamweelyResources.invalid_phone, TamweelyResources.ok);
                IsBusy = false;
                return;
            }
            var code = SelectedCountryCode.dial_code.Replace("+", "");
            var result = await authService.SignUp(new Models.UserModel()
            {
                FullName = Name,
                MobileNo = Phone.Replace(" ",""),
                CountryCode = SelectedCountryCode.dial_code.Replace("+", "")
            });
            if (result != null && result.Status == "success")
            {
                GlobalSetting.Instance.userModel = result.Data;
                await _navigationService.PushAsync(new MobileVerification(result.Data));
            }
            IsBusy = false;
        }
        private async void GoBackClicked(object obj)
        {
            // Do something
            await _navigationService.PopAsync();
        }
        #endregion


    }
}
