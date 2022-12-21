using Newtonsoft.Json;
using Plugin.Multilingual;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TamweelyMobileApp.Helpers;
using TamweelyMobileApp.Resx;
using TamweelyMobileApp.Services.Interfaces;
using Xamarin.Forms;

namespace TamweelyMobileApp.Services.Implementations
{
    public class UserAuthenticateService : IUserAuthenticate
    {
        private readonly IRequestProvider request;
        readonly Uri baseUri = new Uri(GlobalSetting.DefaultEndpoint);
        public UserAuthenticateService(IRequestProvider requestProvider)
        {
            request = requestProvider;
        }
        public async Task<Models.BaseResponseModel<Models.UserModel>> SignIn(string phone, string phoneCode)
        {
            Models.BaseResponseModel<Models.UserModel> Response = null;

            var uri = GlobalSetting.DefaultEndpoint + "SignIn?languageCode=" + CrossMultilingual.Current.CurrentCultureInfo.Name.ToLower();
            if (!string.IsNullOrEmpty(phone))
            {
                uri += "&mobileNo=" + phone;
            }
            if (!string.IsNullOrEmpty(phoneCode))
            {
                uri += "&countryCode=" + phoneCode;
            }
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(""));
                Response = await request.PostAsync<Models.BaseResponseModel<Models.UserModel>>(uri, content);
                if (Response != null && Response.Status != "success")
                {
                    await (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.alert, Response.Message, TamweelyResources.ok);
                }
            }
            catch (Exception ex)
            {
                Response = null;
                await (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.error, TamweelyResources.something, TamweelyResources.ok);
            }
            return Response;
        }
        public async Task<Models.BaseResponseModel<Models.UserModel>> SignUp(Models.UserModel user)
        {
            Models.BaseResponseModel<Models.UserModel> Response = null;

            var uri = GlobalSetting.DefaultEndpoint + "SignUp?languageCode=" + CrossMultilingual.Current.CurrentCultureInfo.Name.ToLower();
            
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(user));
                Response = await request.PostAsync<Models.BaseResponseModel<Models.UserModel>>(uri, content);
                if (Response != null && Response.Status != "success")
                {
                    await (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.alert, Response.Message, TamweelyResources.ok);
                }
            }
            catch (Exception ex)
            {
                Response = null;
                await (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.error, TamweelyResources.something, TamweelyResources.ok);
            }
            return Response;
        }
        public async Task<Models.BaseResponseModel<Models.UserModel>> VerifyOTP(Models.UserModel user)
        {
            Models.BaseResponseModel<Models.UserModel> Response = null;

            var uri = GlobalSetting.DefaultEndpoint + "VerifyOTP?languageCode=" + CrossMultilingual.Current.CurrentCultureInfo.Name.ToLower();

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(user));
                Response = await request.PostAsync<Models.BaseResponseModel<Models.UserModel>>(uri, content);
                if (Response != null && Response.Status != "success")
                {
                    await (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.alert, Response.Message, TamweelyResources.ok);
                }
            }
            catch (Exception ex)
            {
                Response = null;
                await (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.error, TamweelyResources.something, TamweelyResources.ok);
            }
            return Response;
        }
        public async Task<Models.BaseResponseModel<Models.UserModel>> EditProfile(Models.UserModel user)
        {
            Models.BaseResponseModel<Models.UserModel> Response = null;

            var uri = GlobalSetting.DefaultEndpoint + "EditProfile?languageCode=" + CrossMultilingual.Current.CurrentCultureInfo.Name.ToLower();

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(user));
                Response = await request.PostAsync<Models.BaseResponseModel<Models.UserModel>>(uri, content);
                if (Response != null && Response.Status != "success")
                {
                    await (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.alert, Response.Message, TamweelyResources.ok);
                }
            }
            catch (Exception ex)
            {
                Response = null;
                await (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.error, TamweelyResources.something, TamweelyResources.ok);
            }
            return Response;
        }
        public async Task<Models.BaseResponseModel<Models.UserModel>> GetProfile()
        {
            Models.BaseResponseModel<Models.UserModel> Response = null;

            var uri = GlobalSetting.DefaultEndpoint + "GetUserProfileDetail?languageCode=" + CrossMultilingual.Current.CurrentCultureInfo.Name.ToLower();

            try
            {
                //var content = new StringContent(JsonConvert.SerializeObject(""));
                Response = await request.GetAsync<Models.BaseResponseModel<Models.UserModel>>(uri);
                if (Response != null && Response.Status != "success")
                {
                    await (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.alert, Response.Message, TamweelyResources.ok);
                }
            }
            catch (Exception ex)
            {
                Response = null;
                await (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.error, TamweelyResources.something, TamweelyResources.ok);
            }
            return Response;
        }
        public async Task<List<Models.Make>> GetMakeList()
        {
            List<Models.Make> Response = null;

            var uri = GlobalSetting.DefaultEndpoint + "MakeList?languageCode=" + CrossMultilingual.Current.CurrentCultureInfo.Name.ToLower();

            try
            {
                //var content = new StringContent(JsonConvert.SerializeObject(""));
                Response = await request.GetAsync<List<Models.Make>>(uri);
                //if (Response != null && Response.Status != "success")
                //{
                //    await (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.alert, Response.Message, TamweelyResources.ok);
                //}
            }
            catch (Exception ex)
            {
                Response = null;
                await (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.error, TamweelyResources.something, TamweelyResources.ok);
            }
            return Response;
        }
    }
}
