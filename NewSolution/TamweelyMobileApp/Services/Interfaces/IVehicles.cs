using System.Collections.Generic;
using System.Threading.Tasks;
using TamweelyMobileApp.Models;

namespace TamweelyMobileApp.Services.Interfaces
{
    public interface IVehicles
    {
        Task<Models.BaseListResponseModel<List<Models.AdModel>>> GetAds(string pageNo = null, string pageRow = null);
        Task<Models.BaseListResponseModel<List<Models.InstallmentSetting>>> GetInstallmentSettings(string pageNo = null, string pageRow = null);
        Task<Models.BaseListResponseModel<List<Models.VehicleModel>>> GetVehicles(string monthlyInstallment, string pageNo = null, 
            string pageRow = null, int makeId = 0, int highLow = 0);
        Task<List<string>> GetLinks();
        Task<BaseListResponseModel<List<InstallmentConfig>>> GetInstallmentsConfigurations();
    }
}
