using Plugin.Multilingual;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using TamweelyMobileApp.Helpers;
using TamweelyMobileApp.Models;
using TamweelyMobileApp.Resx;
using TamweelyMobileApp.Views;
using Xamarin.Forms;

namespace TamweelyMobileApp.ViewModels
{
    public class ChooseLanguageViewModel : BaseViewModel
    {
        #region Fields
        private TextAlignment textAlignment;
        private Language selectedLanguage;
        private string phone;
        private List<Language> languages;
        private string password;
        private string phoneError;
        private string passwordError;
        #endregion

        #region Constructor
        public ChooseLanguageViewModel(INavigation navigationService)
        {
            _navigationService = navigationService;
            this.LoginCommand = new Command(this.LoginClicked);
            this.InitializeCommand = new Command(this.InitializeData);
            Languages = new List<Language>();
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
        public List<Language> Languages
        {
            get
            {
                return this.languages;
            }

            set
            {
                if (this.languages == value)
                {
                    return;
                }

                this.languages = value;
                this.NotifyPropertyChanged();
            }
        }
        public Language SelectedLanguage
        {
            get
            {
                return this.selectedLanguage;
            }

            set
            {
                if (this.selectedLanguage == value)
                {
                    return;
                }

                this.selectedLanguage = value;
                this.NotifyPropertyChanged();
            }
        }
        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (this.password == value)
                {
                    return;
                }

                this.password = value;
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
        public Command LoginCommand { get; set; }
        public Command InitializeCommand { get; set; }
        #endregion

        #region methods
        private async void InitializeData(object obj)
        {
            IsBusy = true;
            await Task.Delay(200);

            var db = await SQL.SQLiteDB.Instance;

            var user = await db.GetUserAsync();
            var lang = await db.GetLanguageAsync();
            if(lang != null)
            {
                var enCulture = new CultureInfo(lang.ShortName);
                TamweelyResources.Culture = enCulture;
                CrossMultilingual.Current.CurrentCultureInfo = enCulture;
            }
            else
            {
                var enCulture = new CultureInfo("ar");
                TamweelyResources.Culture = enCulture;
                CrossMultilingual.Current.CurrentCultureInfo = enCulture;
            }
            if(user != null)
            {
                GlobalSetting.Instance.userModel = user;
                GlobalSetting.Instance.tokenModel = await db.GetTokenAsync();
                (Application.Current as App).MainPage = new NavigationPage(new Views.MasterDetail.MasterDetailPage());
            }
            
            Languages = new List<Language>()
            {
                new Language { DisplayName =  "English", ShortName = "en", Flag = "ar.png" },
                new Language { DisplayName =  "Arabic", ShortName = "ar", Flag = "en.png" },
            };

            var culture = CrossMultilingual.Current.CurrentCultureInfo;
            if (culture.Name.ToUpper() == "AR")
            {
                SelectedLanguage = Languages[1];
            }
            else
            {
                SelectedLanguage = Languages[0];
                var enCulture = new CultureInfo("en");
                TamweelyResources.Culture = enCulture;
                CrossMultilingual.Current.CurrentCultureInfo = enCulture;
            }
            if(user == null)
            {
                IsBusy = false;
            }
        }
        private async void LoginClicked(object obj)
        {
            IsBusy = true;
            await Task.Delay(200);
            if (SelectedLanguage == null)
            {
                await (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.error, TamweelyResources.select_language, TamweelyResources.ok);
                IsBusy = false;
                return;
            }
            await _navigationService.PushAsync(new LoginPage());
            IsBusy = false;
        }

        #endregion


    }
}
