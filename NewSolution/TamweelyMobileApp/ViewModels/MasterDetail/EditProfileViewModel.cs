using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using TamweelyMobileApp.Helpers;
using TamweelyMobileApp.Resx;
using TamweelyMobileApp.Services.Interfaces;
using TamweelyMobileApp.Views;
using TamweelyMobileApp.Views.Popups;
using Xamarin.Forms;

namespace TamweelyMobileApp.ViewModels
{
    public class EditProfileViewModel : BaseViewModel
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
        public EditProfileViewModel(INavigation navigationService, IUserAuthenticate service)
        {
            _navigationService = navigationService;
            authService = service;
            this.InitializeCommand = new Command(this.InitializeData);
            this.SelectCountryCodeCommand = new Command(this.SelectCountryCode);
            this.CountryCodeSelectedCommand = new Command(this.CountryCodeSelected);
            this.SearchCountryCodesCommand = new Command(this.SearchCountryCodes);
            this.SaveCommand = new Command(this.SaveClicked);
            this.GoBackCommand = new Command(this.GoBackClicked);
            this.DeleteCommand = new Command(this.DeleteAccount);
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
        public Command SaveCommand { get; set; }
        public Command GoBackCommand { get; set; }
        public Command DeleteCommand { get; set; }
        #endregion

        #region methods
        private void InitializeData(object obj)
        {
            IsBusy = true;
            try
            {
                Name = GlobalSetting.Instance.userModel.FullName;
                Phone = GlobalSetting.Instance.userModel.MobileNo;
                var file = "TamweelyMobileApp.Helpers.CountryCodes.json";

                var assembly = typeof(App).GetTypeInfo().Assembly;

                List<CountryCode> data;

                using (var stream = assembly.GetManifestResourceStream(file))
                {
                    var serializer = new DataContractJsonSerializer(typeof(List<CountryCode>));
                    data = (List<CountryCode>)serializer.ReadObject(stream);
                    originalCountryCodesList = CountryCodes = data;
                }
                SelectedCountryCode = CountryCodes.Where(cc => cc.dial_code.Contains(GlobalSetting.Instance.userModel.CountryCode)).FirstOrDefault();
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
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new SelectCountryCodePopup(this, AuthPage.EditProfile)
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
        private async void SaveClicked(object obj)
        {
            IsBusy = true;
            await Task.Delay(200);
            if (string.IsNullOrEmpty(Name))
            {
                await (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.error, TamweelyResources.enter_name, TamweelyResources.ok);
                IsBusy = false;
                return;
            }
            if (string.IsNullOrEmpty(Phone) || Phone.Replace(" ", "").Length <= 8)
            {
                await (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.error, TamweelyResources.invalid_phone, TamweelyResources.ok);
                IsBusy = false;
                return;
            }
            var result = await authService.EditProfile(new Models.UserModel()
            {
                FullName = Name,
                MobileNo = Phone.Replace(" ", ""),
                CountryCode = SelectedCountryCode.dial_code.Replace("+", "")
            });
            if (result != null && result.Status == "success")
            {
                var profResult = await authService.GetProfile();
                if (profResult != null && profResult.Status == "success")
                {
                    await (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.success, result.Message, TamweelyResources.ok);
                    GlobalSetting.Instance.userModel = profResult.Data;

                    var db = await SQL.SQLiteDB.Instance;
                    await db.DeleteAllUsersAsync();
                    await db.SaveUserAsync(GlobalSetting.Instance.userModel);
                }
            }
            IsBusy = false;
        }
        private async void GoBackClicked(object obj)
        {
            // Do something
            await _navigationService.PopAsync();
        }
        private async void DeleteAccount(object obj)
        {
            // Do something
            IsBusy = true;
            try
            {
                var logout = await (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.delete + TamweelyResources.account, TamweelyResources.confirm_delete, TamweelyResources.ok, TamweelyResources.cancel);
                if (logout)
                {
                    await Task.Delay(200);
                    var user = GlobalSetting.Instance.userModel;
                    user.IsArchive = true;
                    var res = await authService.EditProfile(user);
                    if (res != null && res.Data != null)
                    {
                        var db = await SQL.SQLiteDB.Instance;
                        await db.DeleteAllUsersAsync();
                        await db.DeleteAllTokensAsync();
                        (Application.Current as App).MainPage = new NavigationPage(new LoginPage());
                    }
                }
            }
            catch(Exception ex)
            {

            }
            IsBusy = false;
        }
        #endregion


    }
}
