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
using TamweelyMobileApp.Models;
using TamweelyMobileApp.Resx;
using TamweelyMobileApp.Services.Interfaces;
using TamweelyMobileApp.Views;
using TamweelyMobileApp.Views.Popups;
using Xamarin.Forms;

namespace TamweelyMobileApp.ViewModels
{
    public class MobileVerificationViewModel : BaseViewModel
    {
        #region Fields
        private IUserAuthenticate authService;
        private TextAlignment textAlignment;
        private UserModel userModel;
        private bool isLoginPage;
        private bool busy = false;
        private string firstPin;
        private string secondPin;
        private string thirdPin;
        private string forthPin;
        private string selectedOTP;
        #endregion

        #region Constructor
        public MobileVerificationViewModel(INavigation navigationService, IUserAuthenticate service, UserModel userModel, bool isLoginPage = false)
        {
            this.userModel = userModel;
            this.isLoginPage = isLoginPage;
            _navigationService = navigationService;
            authService = service;
            Busy = IsBusy = false;
            this.InitializeCommand = new Command(this.InitializeData);
            this.VerifyCommand = new Command(this.VerifyClicked);
            this.GoBackCommand = new Command(this.GoBackClicked);
            this.isLoginPage = isLoginPage;
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
        public bool Busy
        {
            get
            {
                return this.busy;
            }

            set
            {
                if (this.busy == value)
                {
                    return;
                }

                this.busy = value;
                this.NotifyPropertyChanged();
            }
        }
        public string SecondPin
        {
            get
            {
                return this.secondPin;
            }

            set
            {
                if (this.secondPin == value)
                {
                    return;
                }

                this.secondPin = value;
                this.NotifyPropertyChanged();
            }
        }
        public string FirstPin
        {
            get
            {
                return this.firstPin;
            }

            set
            {
                if (this.firstPin == value)
                {
                    return;
                }

                this.firstPin = value;
                this.NotifyPropertyChanged();
            }
        }
        public string ThirdPin
        {
            get
            {
                return this.thirdPin;
            }

            set
            {
                if (this.thirdPin == value)
                {
                    return;
                }

                this.thirdPin = value;
                this.NotifyPropertyChanged();
            }
        }
        public string ForthPin
        {
            get
            {
                return this.forthPin;
            }

            set
            {
                if (this.forthPin == value)
                {
                    return;
                }

                this.forthPin = value;
                this.NotifyPropertyChanged();
            }
        }
        public string SelectedOTP
        {
            get
            {
                return this.selectedOTP;
            }

            set
            {
                if (this.selectedOTP == value)
                {
                    return;
                }

                this.selectedOTP = value;
                this.NotifyPropertyChanged();
            }
        }
        #endregion

        #region Command
        public Command InitializeCommand { get; set; }
        public Command VerifyCommand { get; set; }
        public Command GoBackCommand { get; set; }
        #endregion

        #region methods
        private void InitializeData(object obj)
        {
            IsBusy = true;
            try
            {
                
            }
            catch (Exception ex)
            {

            }

            IsBusy = false;
        }
        private async void VerifyClicked(object obj)
        {
            Busy = true;
            await Task.Delay(200);
            //if (string.IsNullOrEmpty(FirstPin) || FirstPin == " " ||
            //    string.IsNullOrEmpty(SecondPin) || SecondPin == " " ||
            //    string.IsNullOrEmpty(ThirdPin) || ThirdPin == " " ||
            //    string.IsNullOrEmpty(ForthPin) || ForthPin == " ")
            if(string.IsNullOrEmpty(SelectedOTP) || SelectedOTP.Length < 4)
            {
                await (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.error, TamweelyResources.enter_code, TamweelyResources.ok);
                Busy = false;
                return;
            }
            var result = await authService.VerifyOTP(new Models.UserModel()
            {
                GUID = userModel.GUID,
                MobileNo = userModel.MobileNo,
                CountryCode = userModel.CountryCode,
                //Code = FirstPin + SecondPin + ThirdPin + ForthPin,
                Code = SelectedOTP
            });
            if (result != null && result.Status == "success")
            {
                GlobalSetting.Instance.userModel = result.Data;
                GlobalSetting.Instance.tokenModel = result.Data.OauthToken;

                var db = await SQL.SQLiteDB.Instance;
                await db.DeleteAllUsersAsync();
                await db.DeleteAllTokensAsync();
                await db.SaveUserAsync(GlobalSetting.Instance.userModel);
                await db.SaveTokenAsync(GlobalSetting.Instance.tokenModel);

                (Application.Current as App).MainPage = new NavigationPage(new Views.MasterDetail.MasterDetailPage());
            }
            Busy = false;
        }
        private async void GoBackClicked(object obj)
        {
            // Do something
            await _navigationService.PopAsync();
        }
        #endregion


    }
}
