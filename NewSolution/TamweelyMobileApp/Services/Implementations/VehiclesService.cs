using Newtonsoft.Json;
using Plugin.Multilingual;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TamweelyMobileApp.Helpers;
using TamweelyMobileApp.Models;
using TamweelyMobileApp.Resx;
using TamweelyMobileApp.Services.Interfaces;
using Xamarin.Forms;

namespace TamweelyMobileApp.Services.Implementations
{
    public class VehiclesService : IVehicles
    {
        private readonly IRequestProvider request;
        readonly Uri baseUri = new Uri(GlobalSetting.DefaultEndpoint);
        public VehiclesService(IRequestProvider requestProvider)
        {
            request = requestProvider;
        }
        public async Task<Models.BaseListResponseModel<List<Models.AdModel>>> GetAds(string pageNo = null, string pageRow = null)
        {
            Models.BaseListResponseModel<List<Models.AdModel>> Response = null;
            var uri = GlobalSetting.DefaultEndpoint + "LoadAdvertisement?languageCode=" + CrossMultilingual.Current.CurrentCultureInfo.Name.ToLower();
            if (!string.IsNullOrEmpty(pageNo))
            {
                uri += "&pageNo=" + pageNo;
            }
            if (!string.IsNullOrEmpty(pageRow))
            {
                uri += "&pageRow=" + pageRow;
            }
            try
            {
                Response = await request.GetAsync<BaseListResponseModel<List<Models.AdModel>>>(uri);
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
        public async Task<Models.BaseListResponseModel<List<Models.InstallmentSetting>>> GetInstallmentSettings(string pageNo = null, string pageRow = null)
        {
            Models.BaseListResponseModel<List<Models.InstallmentSetting>> Response = null;
            var uri = GlobalSetting.DefaultEndpoint + "CalculateInstallmentSetting?languageCode=" + CrossMultilingual.Current.CurrentCultureInfo.Name.ToLower();
            if (!string.IsNullOrEmpty(pageNo))
            {
                uri += "&pageNo=" + pageNo;
            }
            if (!string.IsNullOrEmpty(pageRow))
            {
                uri += "&pageRow=" + pageRow;
            }
            try
            {
                Response = await request.GetAsync<BaseListResponseModel<List<Models.InstallmentSetting>>>(uri);
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
        public async Task<Models.BaseListResponseModel<List<Models.VehicleModel>>> GetVehicles(string monthlyInstallment, 
            string pageNo = null, string pageRow = null,int makeId = 0, int highLow = 0)
        {
            Models.BaseListResponseModel<List<Models.VehicleModel>> Response = null;
            var uri = GlobalSetting.DefaultEndpoint + "LoadVehicle?languageCode=" + CrossMultilingual.Current.CurrentCultureInfo.Name.ToLower();
            if (!string.IsNullOrEmpty(pageNo))
            {
                uri += "&pageNo=" + pageNo;
            }
            if (!string.IsNullOrEmpty(pageRow))
            {
                uri += "&pageRow=" + pageRow;
            }
            if (!string.IsNullOrEmpty(monthlyInstallment))
            {
                uri += "&monthlyInstallment=" + monthlyInstallment;
            }
            if (makeId != 0)
            {
                uri += "&makeId=" + makeId;
            }
            if (highLow != 0)
            {
                uri += "&highlow=" + highLow;
            }
            try
            {
                Response = await request.GetAsync<BaseListResponseModel<List<Models.VehicleModel>>>(uri);
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
        public async Task<List<string>> GetLinks()
        {
            List<string> Response = null;
            var uri = GlobalSetting.DefaultEndpoint + "GetLinks";

            try
            {
                Response = await request.GetAsync<List<string>>(uri);
                if (Response == null)
                {
                    await (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.error, TamweelyResources.something, TamweelyResources.ok);
                }
            }
            catch (Exception ex)
            {
                Response = null;
                await (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.error, TamweelyResources.something, TamweelyResources.ok);
            }
            return Response;
        }
        public async Task<BaseListResponseModel<List<InstallmentConfig>>> GetInstallmentsConfigurations()
        {
            BaseListResponseModel<List<InstallmentConfig>> Response = null;
            var uri = GlobalSetting.DefaultEndpoint + "InstallmentConfiguration?pageNo=0&pageRow=10";

            try
            {
                Response = await request.GetAsync<BaseListResponseModel<List<InstallmentConfig>>>(uri);
                if (Response == null)
                {
                    await (Application.Current as App).MainPage.DisplayAlert(TamweelyResources.error, TamweelyResources.something, TamweelyResources.ok);
                }
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
